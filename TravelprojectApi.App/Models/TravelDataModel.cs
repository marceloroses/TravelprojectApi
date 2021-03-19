using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelprojectApi.DataAccess.Models;

namespace TravelprojectApi.App.Models
{
    public class TravelDataModel
    {
        public long Id { get; set; }
        public long? TravelId { get; set; }
        public string PassengerName { get; set; }
        public DateTimeOffset? Birthdate { get; set; }
        public string Passport { get; set; }
        public string Email { get; set; }

        public List<FlightItineraryModel> FlightItineraries { get; set; }
    }
}
