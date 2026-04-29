using System;

namespace Society_Management_System.Models
{
    public class Member
    {
        public int MemberId { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Gender { get; set; }

        public DateTime DOB { get; set; }

        public int TotalMembers { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Image { get; set; }
    }
}