using System;
using System.Collections.Generic;

#nullable disable

namespace TravelprojectApi.DataAccess.Models
{
    public partial class TravelDatum
    {
        public TravelDatum()
        {
            FlightItineraries = new HashSet<FlightItinerary>();
        }

        public long Id { get; set; }
        public long? TravelId { get; set; }
        public string PassengerName { get; set; }
        public DateTimeOffset? Birthdate { get; set; }
        public string Passport { get; set; }
        public string Email { get; set; }

        public virtual Travel Travel { get; set; }
        public virtual ICollection<FlightItinerary> FlightItineraries { get; set; }
    }
}
