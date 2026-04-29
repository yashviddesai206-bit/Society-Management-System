using Microsoft.AspNetCore.Mvc;
using Society_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Society_Management_System.Controllers
{
    public class UserController : Controller
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\OneDrive\Documents\Society Management System.mdf;Integrated Security=True;Connect Timeout=30");

        // DASHBOARD
        public IActionResult Dashboard()
        {
            // SESSION CHECK
            if (HttpContext.Session.GetString("Role") != "User")
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }
        public IActionResult MyComplaints()
        {
            List<Complaint> list = new List<Complaint>();

            int memberId = Convert.ToInt32(HttpContext.Session.GetString("MemberId"));

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Complaint WHERE MemberId=@MemberId", con);
            cmd.Parameters.AddWithValue("@MemberId", memberId);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Complaint c = new Complaint();

                c.ComplaintId = Convert.ToInt32(dr["ComplaintId"]);
                c.MemberId = Convert.ToInt32(dr["MemberId"]);
                c.ComplaintText = dr["ComplaintText"].ToString();
                c.Status = dr["Status"].ToString();
                c.ComplaintDate = Convert.ToDateTime(dr["ComplaintDate"]);

                list.Add(c);
            }

            con.Close();

            return View(list);
        }
        // VIEW PROFILE
        public IActionResult ViewProfile()
        {
            if (HttpContext.Session.GetString("MemberId") == null)
                return RedirectToAction("Login", "Account");

            int memberId = Convert.ToInt32(HttpContext.Session.GetString("MemberId"));
            Member m = new Member();

            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Member WHERE MemberId=@id", con);
            cmd.Parameters.AddWithValue("@id", memberId);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                m.MemberId = (int)dr["MemberId"];
                m.Name = dr["Name"].ToString();
                m.Email = dr["Email"].ToString();
                m.Mobile = dr["Mobile"].ToString();
                m.Gender = dr["Gender"].ToString();
                m.DOB = Convert.ToDateTime(dr["DOB"]);
                m.TotalMembers = (int)dr["TotalMembers"];
                m.Image = dr["Image"].ToString();
            }

            dr.Close();
            con.Close();

            return View(m);
        }

        // ADD COMPLAINT PAGE
        public IActionResult AddComplaint()
        {
            if (HttpContext.Session.GetString("MemberId") == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        // SAVE COMPLAINT
        [HttpPost]
        public IActionResult AddComplaint(Complaint c)
        {
            if (HttpContext.Session.GetString("MemberId") == null)
                return RedirectToAction("Login", "Account");

            int memberId = Convert.ToInt32(HttpContext.Session.GetString("MemberId"));

            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Complaint(MemberId,ComplaintText,Status,ComplaintDate) VALUES(@MemberId,@ComplaintText,'Pending',GETDATE())", con);

            cmd.Parameters.AddWithValue("@MemberId", memberId);
            cmd.Parameters.AddWithValue("@ComplaintText", c.ComplaintText);

            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("ViewComplaint");
        }

        // VIEW COMPLAINT
        public IActionResult ViewComplaint()
        {
            if (HttpContext.Session.GetString("MemberId") == null)
                return RedirectToAction("Login", "Account");

            int memberId = Convert.ToInt32(HttpContext.Session.GetString("MemberId"));
            List<Complaint> list = new List<Complaint>();

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Complaint WHERE MemberId=@id", con);
            cmd.Parameters.AddWithValue("@id", memberId);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Complaint c = new Complaint();

                c.ComplaintId = Convert.ToInt32(dr["ComplaintId"]);
                c.MemberId = Convert.ToInt32(dr["MemberId"]);
                c.ComplaintText = dr["ComplaintText"].ToString();
                c.Status = dr["Status"].ToString();
                c.ComplaintDate = Convert.ToDateTime(dr["ComplaintDate"]);

                list.Add(c);
            }

            dr.Close();
            con.Close();

            return View("MyComplaints", list);
        }

        // DELETE COMPLAINT
        public IActionResult DeleteComplaint(int id)
        {
            if (HttpContext.Session.GetString("MemberId") == null)
                return RedirectToAction("Login", "Account");

            int memberId = Convert.ToInt32(HttpContext.Session.GetString("MemberId"));

            con.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM Complaint WHERE ComplaintId=@id AND MemberId=@mid", con);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@mid", memberId);

            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("ViewComplaint");
        }

        // EDIT COMPLAINT GET
        public IActionResult EditComplaint(int id)
        {
            if (HttpContext.Session.GetString("MemberId") == null)
                return RedirectToAction("Login", "Account");

            int memberId = Convert.ToInt32(HttpContext.Session.GetString("MemberId"));
            Complaint c = new Complaint();

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Complaint WHERE ComplaintId=@id AND MemberId=@mid", con);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@mid", memberId);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                c.ComplaintId = Convert.ToInt32(dr["ComplaintId"]);
                c.ComplaintText = dr["ComplaintText"].ToString();
                c.Status = dr["Status"].ToString();
            }

            dr.Close();
            con.Close();

            return View(c);
        }

        // EDIT COMPLAINT POST
        [HttpPost]
        public IActionResult EditComplaint(Complaint c)
        {
            if (HttpContext.Session.GetString("MemberId") == null)
                return RedirectToAction("Login", "Account");

            int memberId = Convert.ToInt32(HttpContext.Session.GetString("MemberId"));

            con.Open();

            SqlCommand cmd = new SqlCommand("UPDATE Complaint SET ComplaintText=@text WHERE ComplaintId=@id AND MemberId=@mid", con);

            cmd.Parameters.AddWithValue("@text", c.ComplaintText);
            cmd.Parameters.AddWithValue("@id", c.ComplaintId);
            cmd.Parameters.AddWithValue("@mid", memberId);

            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("ViewComplaint");
        }

        // LOGOUT
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}