using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Society_Management_System.Models;
using SOCITEY_MANAGEMENT.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Society_Management_System.Controllers
{
    public class AdminController : Controller
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\OneDrive\Documents\Society Management System.mdf;Integrated Security=True;Connect Timeout=30");

        #region SOCIETY MODULE

        public IActionResult AddSociety()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSociety(Society model)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO Society(SocietyName,City,PinCode,TotalHouses,Address,Image) VALUES(@SocietyName,@City,@PinCode,@TotalHouses,@Address,@Image)", con);

            cmd.Parameters.AddWithValue("@SocietyName", model.SocietyName);
            cmd.Parameters.AddWithValue("@City", model.City);
            cmd.Parameters.AddWithValue("@PinCode", model.PinCode);
            cmd.Parameters.AddWithValue("@TotalHouses", model.TotalHouses);
            cmd.Parameters.AddWithValue("@Address", model.Address);
            cmd.Parameters.AddWithValue("@Image", model.Image ?? "");

            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("ViewSocieties");
        }

        public IActionResult ViewSocieties()
        {
            List<Society> list = new List<Society>();

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Society", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Society s = new Society();

                s.SocietyId = Convert.ToInt32(dr["SocietyId"]);
                s.SocietyName = dr["SocietyName"].ToString();
                s.City = dr["City"].ToString();
                s.PinCode = dr["PinCode"].ToString();
                s.TotalHouses = Convert.ToInt32(dr["TotalHouses"]);
                s.Address = dr["Address"].ToString();
                s.Image = dr["Image"].ToString();

                list.Add(s);
            }

            dr.Close();
            con.Close();

            return View(list);
        }
        public IActionResult EditSociety(int id)
        {
            Society s = new Society();

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Society WHERE SocietyId=@id", con);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                s.SocietyId = Convert.ToInt32(dr["SocietyId"]);
                s.SocietyName = dr["SocietyName"].ToString();
                s.City = dr["City"].ToString();
                s.PinCode = dr["PinCode"].ToString();
                s.TotalHouses = Convert.ToInt32(dr["TotalHouses"]);
                s.Address = dr["Address"].ToString();
                s.Image = dr["Image"].ToString();
            }

            dr.Close();
            con.Close();

            return View(s);
        }
        [HttpPost]
        public IActionResult EditSociety(Society s)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("UPDATE Society SET SocietyName=@SocietyName,City=@City,PinCode=@PinCode,TotalHouses=@TotalHouses,Address=@Address,Image=@Image WHERE SocietyId=@SocietyId", con);

            cmd.Parameters.AddWithValue("@SocietyId", s.SocietyId);
            cmd.Parameters.AddWithValue("@SocietyName", s.SocietyName);
            cmd.Parameters.AddWithValue("@City", s.City);
            cmd.Parameters.AddWithValue("@PinCode", s.PinCode);
            cmd.Parameters.AddWithValue("@TotalHouses", s.TotalHouses);
            cmd.Parameters.AddWithValue("@Address", s.Address);
            cmd.Parameters.AddWithValue("@Image", s.Image ?? "");

            cmd.ExecuteNonQuery();

            con.Close();

            return RedirectToAction("ViewSocieties");
        }
        public IActionResult DeleteSociety(int id)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM Society WHERE SocietyId=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            con.Close();

            return RedirectToAction("ViewSocieties");
        }

        #endregion


        #region MEMBER MODULE

        public IActionResult AddMember()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMember(Member model)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO Member(Name,Password,Gender,DOB,TotalMembers,Email,Mobile,Image) VALUES(@Name,@Password,@Gender,@DOB,@TotalMembers,@Email,@Mobile,@Image)", con);

            cmd.Parameters.AddWithValue("@Name", model.Name);
            cmd.Parameters.AddWithValue("@Password", model.Password);
            cmd.Parameters.AddWithValue("@Gender", model.Gender);
            cmd.Parameters.AddWithValue("@DOB", model.DOB);
            cmd.Parameters.AddWithValue("@TotalMembers", model.TotalMembers);
            cmd.Parameters.AddWithValue("@Email", model.Email);
            cmd.Parameters.AddWithValue("@Mobile", model.Mobile);
            cmd.Parameters.AddWithValue("@Image", model.Image ?? "");

            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("ViewMembers");
        }

        public IActionResult ViewMembers()
        {
            List<Member> list = new List<Member>();

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Member", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Member m = new Member();

                m.MemberId = Convert.ToInt32(dr["MemberId"]);
                m.Name = dr["Name"].ToString();
                m.Password = dr["Password"].ToString();
                m.Gender = dr["Gender"].ToString();
                m.DOB = Convert.ToDateTime(dr["DOB"]);
                m.TotalMembers = Convert.ToInt32(dr["TotalMembers"]);
                m.Email = dr["Email"].ToString();
                m.Mobile = dr["Mobile"].ToString();
                m.Image = dr["Image"].ToString();

                list.Add(m);
            }

            dr.Close();
            con.Close();

            return View(list);
        }

        public IActionResult DeleteMember(int id)
        {
            con.Open();

            SqlCommand cmd1 = new SqlCommand("DELETE FROM Complaint WHERE MemberId=@id", con);
            cmd1.Parameters.AddWithValue("@id", id);
            cmd1.ExecuteNonQuery();

            SqlCommand cmd2 = new SqlCommand("DELETE FROM Member WHERE MemberId=@id", con);
            cmd2.Parameters.AddWithValue("@id", id);
            cmd2.ExecuteNonQuery();

            con.Close();

            return RedirectToAction("ViewMembers");
        }
        public IActionResult EditMember(int id)
        {
            Member m = new Member();

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Member WHERE MemberId=@id", con);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                m.MemberId = Convert.ToInt32(dr["MemberId"]);
                m.Name = dr["Name"].ToString();
                m.Password = dr["Password"].ToString();
                m.Gender = dr["Gender"].ToString();
                m.DOB = Convert.ToDateTime(dr["DOB"]);
                m.TotalMembers = Convert.ToInt32(dr["TotalMembers"]);
                m.Email = dr["Email"].ToString();
                m.Mobile = dr["Mobile"].ToString();
                m.Image = dr["Image"].ToString();
            }

            dr.Close();
            con.Close();

            return View(m);
        }

        [HttpPost]
        public IActionResult EditMember(Member m)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("UPDATE Member SET Name=@Name,Password=@Password,Gender=@Gender,DOB=@DOB,TotalMembers=@TotalMembers,Email=@Email,Mobile=@Mobile WHERE MemberId=@MemberId", con);

            cmd.Parameters.AddWithValue("@MemberId", m.MemberId);
            cmd.Parameters.AddWithValue("@Name", m.Name);
            cmd.Parameters.AddWithValue("@Password", m.Password);
            cmd.Parameters.AddWithValue("@Gender", m.Gender);
            cmd.Parameters.AddWithValue("@DOB", m.DOB);
            cmd.Parameters.AddWithValue("@TotalMembers", m.TotalMembers);
            cmd.Parameters.AddWithValue("@Email", m.Email);
            cmd.Parameters.AddWithValue("@Mobile", m.Mobile);

            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("ViewMembers");
        }
        #endregion


        #region HOUSE MODULE

        public IActionResult AddHouse()
        {
            List<SelectListItem> societyList = new List<SelectListItem>();

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT SocietyId FROM Society", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                societyList.Add(new SelectListItem
                {
                    Value = dr["SocietyId"].ToString(),
                    Text = dr["SocietyId"].ToString()
                });
            }

            dr.Close();
            con.Close();

            ViewBag.SocietyList = societyList;

            return View();
        }

        [HttpPost]
        public IActionResult AddHouse(House h)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO House(SocietyId,HouseNumber,BlockNumber,HouseType,Details,Status) VALUES(@SocietyId,@HouseNumber,@BlockNumber,@HouseType,@Details,@Status)", con);

            cmd.Parameters.AddWithValue("@SocietyId", h.SocietyId);
            cmd.Parameters.AddWithValue("@HouseNumber", h.HouseNumber);
            cmd.Parameters.AddWithValue("@BlockNumber", h.BlockNumber);
            cmd.Parameters.AddWithValue("@HouseType", h.HouseType);
            cmd.Parameters.AddWithValue("@Details", h.Details);
            cmd.Parameters.AddWithValue("@Status", h.Status);

            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("ViewHouses");
        }

        public IActionResult ViewHouses()
        {
            List<House> list = new List<House>();

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM House", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                House h = new House();

                h.HouseId = Convert.ToInt32(dr["HouseId"]);
                h.SocietyId = Convert.ToInt32(dr["SocietyId"]);
                h.HouseNumber = dr["HouseNumber"].ToString();
                h.BlockNumber = dr["BlockNumber"].ToString();
                h.HouseType = dr["HouseType"].ToString();
                h.Details = dr["Details"].ToString();
                h.Status = dr["Status"].ToString();

                list.Add(h);
            }

            dr.Close();
            con.Close();

            return View(list);
        }

        public IActionResult DeleteHouse(int id)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM House WHERE HouseId=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            con.Close();

            return RedirectToAction("ViewHouses");
        }

        // -------- EDIT HOUSE --------

        public IActionResult EditHouse(int id)
        {
            House h = new House();

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM House WHERE HouseId=@id", con);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                h.HouseId = Convert.ToInt32(dr["HouseId"]);
                h.SocietyId = Convert.ToInt32(dr["SocietyId"]);
                h.HouseNumber = dr["HouseNumber"].ToString();
                h.BlockNumber = dr["BlockNumber"].ToString();
                h.HouseType = dr["HouseType"].ToString();
                h.Details = dr["Details"].ToString();
                h.Status = dr["Status"].ToString();
            }

            dr.Close();
            con.Close();

            return View(h);
        }

        [HttpPost]
        public IActionResult EditHouse(House h)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("UPDATE House SET SocietyId=@SocietyId,HouseNumber=@HouseNumber,BlockNumber=@BlockNumber,HouseType=@HouseType,Details=@Details,Status=@Status WHERE HouseId=@HouseId", con);

            cmd.Parameters.AddWithValue("@SocietyId", h.SocietyId);
            cmd.Parameters.AddWithValue("@HouseNumber", h.HouseNumber);
            cmd.Parameters.AddWithValue("@BlockNumber", h.BlockNumber);
            cmd.Parameters.AddWithValue("@HouseType", h.HouseType);
            cmd.Parameters.AddWithValue("@Details", h.Details);
            cmd.Parameters.AddWithValue("@Status", h.Status);
            cmd.Parameters.AddWithValue("@HouseId", h.HouseId);

            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("ViewHouses");
        }

        #endregion


        #region DASHBOARD

        public IActionResult Dashboard()
        {
            con.Open();

            SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM Society", con);
            ViewBag.TotalSociety = cmd1.ExecuteScalar();

            SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM House", con);
            ViewBag.TotalHouse = cmd2.ExecuteScalar();

            SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) FROM Member", con);
            ViewBag.TotalMember = cmd3.ExecuteScalar();

            SqlCommand cmd4 = new SqlCommand("SELECT COUNT(*) FROM Complaint", con);
            ViewBag.TotalComplaint = cmd4.ExecuteScalar();

            con.Close();

            return View();
        }

        #endregion


        #region COMPLAINT MODULE

        public IActionResult ViewComplaints()
        {
            List<Complaint> list = new List<Complaint>();

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Complaint", con);
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

            return View(list);
        }

        public IActionResult EditComplaint(int id)
        {
            Complaint c = new Complaint();

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Complaint WHERE ComplaintId=@id", con);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                c.ComplaintId = Convert.ToInt32(dr["ComplaintId"]);
                c.MemberId = Convert.ToInt32(dr["MemberId"]);
                c.ComplaintText = dr["ComplaintText"].ToString();
                c.Status = dr["Status"].ToString();
            }

            dr.Close();
            con.Close();

            return View(c);
        }

        [HttpPost]
        public IActionResult EditComplaint(Complaint c)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("UPDATE Complaint SET Status=@Status WHERE ComplaintId=@ComplaintId", con);

            cmd.Parameters.AddWithValue("@Status", c.Status);
            cmd.Parameters.AddWithValue("@ComplaintId", c.ComplaintId);

            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("ViewComplaints");
        }

        #endregion
    }
}