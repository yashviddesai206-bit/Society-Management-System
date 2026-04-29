using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCITEY_MANAGEMENT.Models
{
    public class House
    {
        public int HouseId { get; set; }
        public int SocietyId { get; set; }
        public string HouseNumber { get; set; }
        public string BlockNumber { get; set; }
        public string HouseType { get; set; }
        public string Details { get; set; }
        public string Status { get; set; }
    }
}