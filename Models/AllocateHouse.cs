using System;

namespace Society_Management_System.Models
{
    public class AllocateHouse
    {
        public int AllocateId { get; set; }
        public int MemberId { get; set; }
        public int HouseId { get; set; }
        public DateTime AllocateDate { get; set; }

        // For display in View
        public string MemberName { get; set; }
        public string HouseNumber { get; set; }
    }
}