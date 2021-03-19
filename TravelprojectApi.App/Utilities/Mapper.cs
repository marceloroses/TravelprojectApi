using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelprojectApi.App.Models;
using TravelprojectApi.DataAccess.Models;

namespace TravelprojectApi.App.Utilities
{
    public static class Mapper
    {
        public static TravelModel Map(Travel travel) 
        {
            return new TravelModel() { 
                Id = travel.Id, 
                Name = travel.Name 
            };
        }
        public static TravelDatum Map(TravelDataModel travelData)
        {

            return new TravelDatum() { 
                TravelId = travelData.TravelId, 
                Birthdate = travelData.Birthdate, 
                Email = travelData.Email , 
                PassengerName = travelData.PassengerName, 
                Passport = travelData.Passport,  
                FlightItineraries = travelData.FlightItineraries?.Select(f => Map(f)).ToList()
            };
        }

        public static TravelDataModel Map(TravelDatum travelData)
        {

            return new TravelDataModel()
            {
                Id = travelData.Id,
                TravelId = travelData.TravelId,
                Birthdate = travelData.Birthdate,
                Email = travelData.Email,
                PassengerName = travelData.PassengerName,
                Passport = travelData.Passport,
                FlightItineraries = travelData.FlightItineraries.Select(f => Map(f)).ToList()
            };
        }

        public static FlightItinerary Map(FlightItineraryModel flightItineraryParam)
        {

            return new FlightItinerary() { 
                TravelDataId = flightItineraryParam.TravelDataId, 
                Aeroline= flightItineraryParam.Aeroline, 
                DateArraival = flightItineraryParam.DateArraival, 
                DateDeparture = flightItineraryParam.DateDeparture, 
                DestinationAirport = flightItineraryParam.DestinationAirport,
                FlightCode = flightItineraryParam.FlightCode,
                OriginAirpot = flightItineraryParam.OriginAirpot 
            };
        }

        internal static FlightItineraryModel Map(FlightItinerary flightItineraryParam)
        {
            return new FlightItineraryModel()
            {
                Id = flightItineraryParam.Id,
                TravelDataId = flightItineraryParam.TravelDataId,
                Aeroline = flightItineraryParam.Aeroline,
                DateArraival = flightItineraryParam.DateArraival,
                DateDeparture = flightItineraryParam.DateDeparture,
                DestinationAirport = flightItineraryParam.DestinationAirport,
                FlightCode = flightItineraryParam.FlightCode,
                OriginAirpot = flightItineraryParam.OriginAirpot
            };
        }
    }
}
