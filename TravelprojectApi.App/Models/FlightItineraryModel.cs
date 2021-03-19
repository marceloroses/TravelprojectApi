using System;
using System.Collections.Generic;
using System.Text;

namespace TravelprojectApi.DataAccess.Models
{
    public class FlightItineraryModel
    {
        public long Id { get; set; }
        public long? TravelDataId { get; set; }
        public string OriginAirpot { get; set; }
        public string DestinationAirport { get; set; }
        public DateTimeOffset? DateDeparture { get; set; }
        public DateTimeOffset? DateArraival { get; set; }
        public string FlightCode { get; set; }
        public string Aeroline { get; set; }



    }
}
