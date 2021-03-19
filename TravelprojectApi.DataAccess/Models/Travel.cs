using System;
using System.Collections.Generic;

#nullable disable

namespace TravelprojectApi.DataAccess.Models
{
    public partial class Travel
    {
        public Travel()
        {
            TravelData = new HashSet<TravelDatum>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TravelDatum> TravelData { get; set; }
    }
}
