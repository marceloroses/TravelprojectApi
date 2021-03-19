using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TravelprojectApi.App.Models;
using TravelprojectApi.App.Utilities;
using TravelprojectApi.Common;
using TravelprojectApi.DataAccess.Models;

namespace TravelprojectApi.App.Controllers
{
    [ApiController]    
    public class TravelController : ControllerBase
    {
        private readonly ILogger<TravelController> _logger;
        private readonly IConfiguration Config;

        public TravelController(ILogger<TravelController> logger, IConfiguration config)
        {
            _logger = logger;
            Config = config;
        }

        [Route("[controller]/GetTravels")]
        public IActionResult GetTravels()
        {
            try
            {
                var travelDb = new TravelDBContext();
                var travels = travelDb.Travels;
                return Ok(travels.Select(t => Mapper.Map(t)).ToArray());

            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        /// <summary>
        /// Inserta el viaje y manda por email
        /// </summary>
        /// <param name="travelDataInput"></param>
        /// <returns></returns>
        [Route("[controller]/InsertUpdateTravelData")]
        [HttpPost]
        public IActionResult InsertUpdateTravelData([FromBody]TravelDataModel travelDataInput)
        {
            try
            {
                var travelDb = new TravelDBContext();
                if (travelDb.TravelData.Where(td => td.TravelId == travelDataInput.TravelId).Any())
                {
                    //Si no esta la persona (unico por pasaporte) entonces lo agrego, sino lo actualizo
                    if (!travelDb.TravelData.Where(td => td.TravelId == travelDataInput.TravelId && td.Passport == travelDataInput.Passport).Any())
                    {
                        //Agregar
                        travelDb.TravelData.Add(Mapper.Map(travelDataInput));
                        travelDb.SaveChanges();
                        SendEmailToCompleteInsertOrUpdate(travelDataInput);
                        return Ok();
                    }
                    else
                    {
                        //Actualizar
                        var traveldataToUpdate = travelDb.TravelData.Where(td => td.TravelId == travelDataInput.TravelId && td.Passport == travelDataInput.Passport).SingleOrDefault();
                        traveldataToUpdate.PassengerName = travelDataInput.PassengerName;
                        traveldataToUpdate.Birthdate= travelDataInput.Birthdate;
                        traveldataToUpdate.Email = travelDataInput.Email;
                        travelDb.SaveChanges();

                        SendEmailToCompleteInsertOrUpdate(travelDataInput);

                        return Ok();
                    }
                }
                else
                {
                    return Ok("No existe el viaje");
                }

            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        /// <summary>
        /// Borra un viaje
        /// </summary>
        /// <param name="travelDataInput"></param>
        /// <returns></returns>
        [Route("[controller]/DeleteTravelData")]
        [HttpDelete]
        public IActionResult DeleteTravelData([FromBody]TravelDataDeleteInputParamModel travelDataInput)
        {
            try
            {
                var travelDb = new TravelDBContext();
                travelDb.TravelData.Remove(new TravelDatum() { Id = travelDataInput.TravelDataId});               
                travelDb.SaveChanges();
                return Ok();

            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        /// <summary>
        /// Obtiene los datos y el itinerario buscando por pasaporte, email o si la busqueda es nula por todos
        /// </summary>
        /// <param name="searchItirneraryParam"></param>
        /// <returns></returns>
        [Route("[controller]/GetItineraryByTraveller")]
        public IActionResult GetItineraryByTraveller([FromBody]ItineraryByTravellerInputParamModel searchItirneraryParam)
        {
            try
            {
                var travelDb = new TravelDBContext();
                var TravelerFlight = travelDb.TravelData.Include(g => g.FlightItineraries);
                
                if (searchItirneraryParam.Email  == null && searchItirneraryParam.Passport == null)
                {
                    return Ok(TravelerFlight.Select(t => Mapper.Map(t)).ToArray());
                }
                if (searchItirneraryParam.Email != null && searchItirneraryParam.Passport == null)
                {
                    //Busca por email
                    return Ok(TravelerFlight.Where(tf => tf.Email == searchItirneraryParam.Email).Select(t => Mapper.Map(t)).ToArray());
                }
                if (searchItirneraryParam.Email == null && searchItirneraryParam.Passport != null)
                {
                    return Ok(TravelerFlight.Where(tf => tf.Passport == searchItirneraryParam.Passport).Select(t => Mapper.Map(t)).ToArray());
                }
                if (searchItirneraryParam.Email != null && searchItirneraryParam.Passport != null)
                {
                    return Ok(TravelerFlight.Where(tf => tf.Email == searchItirneraryParam.Email && tf.Passport== searchItirneraryParam.Passport).Select(t => Mapper.Map(t)).ToArray());
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }




        private void SendEmailToCompleteInsertOrUpdate(TravelDataModel travelDataInput) 
        {
            try
            {
                string bodyHtml = JsonSerializer.Serialize(travelDataInput);
                var emailModel = new EmailModel(travelDataInput.Email, // To  
                    "Intinerario de viaje.", // Subject  
                    bodyHtml, // Message  
                    true // IsBodyHTML  
                );
                var smtpSection = Config.GetSection("SMTP");
                if (smtpSection != null)
                {
                    EmailHelper emailHelper = new EmailHelper(
                        smtpSection.GetSection("Host").Value,
                        smtpSection.GetSection("Port").Value,
                        smtpSection.GetSection("From").Value,
                        smtpSection.GetSection("Alias").Value,
                        smtpSection.GetSection("Password").Value);
                    emailHelper.SendEmail(emailModel);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }



}