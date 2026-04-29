using System.Collections.Generic;

namespace Society_Management_System.Models
{
    public class ProfileViewModel
    {
        public Member Member { get; set; }

        public List<Complaint> Complaints { get; set; }
    }
}