using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakeAHike.Models
{
    public class Park
    {
        public int Id { get; set; }
        public string ParkName { get; set; }
        public string Description { get; set; }
        public string ContactInfo { get; set; }
        public string ImageUrl { get; set; }
        public string Address { get; set; }
        public string WebsiteLink { get; set; }
    }
}
