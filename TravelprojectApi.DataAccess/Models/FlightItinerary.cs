using System;
using System.Collections.Generic;

#nullable disable

namespace TravelprojectApi.DataAccess.Models
{
    public partial class FlightItinerary
    {
        public long Id { get; set; }
        public long? TravelDataId { get; set; }
        public string OriginAirpot { get; set; }
        public string DestinationAirport { get; set; }
        public DateTimeOffset? DateDeparture { get; set; }
        public DateTimeOffset? DateArraival { get; set; }
        public string FlightCode { get; set; }
        public string Aeroline { get; set; }

        public virtual TravelDatum TravelData { get; set; }
    }
}
