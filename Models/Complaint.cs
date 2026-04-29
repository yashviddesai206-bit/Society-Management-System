using System;

namespace Society_Management_System.Models
{
    public class Complaint
    {
        public int ComplaintId { get; set; }

        public int MemberId { get; set; }

        public string? MemberName { get; set; }

        public string? ComplaintText { get; set; }

        public string? Status { get; set; }

        public DateTime ComplaintDate { get; set; }

        public string? AdminMessage { get; set; }
    }
}