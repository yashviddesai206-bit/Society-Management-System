using Microsoft.AspNetCore.Mvc;
using Society_Management_System.Models;
using System;
using System.Data.SqlClient;

namespace Society_Management_System.Controllers
{
    public class AccountController : Controller
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\OneDrive\Documents\Society Management System.mdf;Integrated Security=True;Connect Timeout=30");

        // LOGIN PAGE
        public IActionResult Login()
        {
            return View();
        }

        // LOGIN POST
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                ViewBag.Error = "Please enter Email and Password";
                return View();
            }

            // ADMIN LOGIN
            if (model.Email == "admin@gmail.com" && model.Password == "admin123")
            {
                HttpContext.Session.SetString("Role", "Admin");
                HttpContext.Session.SetString("Email", model.Email);

                return RedirectToAction("Dashboard", "Admin");
            }

            try
            {
                con.Open();

                string query = "SELECT MemberId, Name FROM Member WHERE Email=@Email AND Password=@Password";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Password", model.Password);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    HttpContext.Session.SetString("Role", "User");
                    HttpContext.Session.SetString("MemberId", dr["MemberId"].ToString());
                    HttpContext.Session.SetString("UserName", dr["Name"].ToString());
                    HttpContext.Session.SetString("UserEmail", model.Email);

                    dr.Close();
                    con.Close();

                    return RedirectToAction("Dashboard", "User");
                }

                dr.Close();
                con.Close();

                ViewBag.Error = "Invalid Email or Password";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // LOGOUT
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}