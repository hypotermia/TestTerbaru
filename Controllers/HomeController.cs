using Dapper;
using Microsoft.AspNetCore.Mvc;
using OTPSettings.Models;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace OTPSettings.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        string connString = "Server=localhost;Database=TestOTP;Trusted_Connection=True;;MultipleActiveResultSets=true";
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public IActionResult ClickMe(CheckboxDataModel model)
        {
            // Access the checkbox values as boolean variables
            bool emailChecked = model.Email;
            bool whatsappChecked = model.Whatsapp;
            bool withdrawlChecked = false;
            bool forgotPasswordChecked = false;
            bool resetPasswordChecked= false ;
            bool withdrawlwaChecked = false;
            bool forgotPasswordwaChecked = false;
            bool resetPasswordwaChecked = false;
            if (emailChecked == true)
            {
                withdrawlChecked = true;
                forgotPasswordChecked = true;
                resetPasswordChecked = true;
            }
            else if (whatsappChecked == true)
            {
                withdrawlwaChecked = true;
                forgotPasswordwaChecked = true;
                resetPasswordwaChecked = true;
            }
            else
            {
                withdrawlChecked = model.Withdrawl;
                withdrawlwaChecked = model.WithdrawlWA;
                forgotPasswordChecked = model.ForgotPassword;
                forgotPasswordwaChecked = model.ForgotPasswordWA;
                resetPasswordChecked = model.ResetPassword;
                resetPasswordwaChecked = model.ResetPasswordWA;
            }

            string sp_update = "UpdateOTP";
            var parameters = new DynamicParameters(); // Using Dapper's DynamicParameters to create parameterized queries

            // Add parameters to the DynamicParameters object
            parameters.Add("id", Guid.NewGuid(), DbType.Guid);
            parameters.Add("checklsitAllWa", whatsappChecked, DbType.Boolean);
            parameters.Add("checklistAllEmail", emailChecked, DbType.Boolean);
            parameters.Add("withdrawlWa", withdrawlwaChecked, DbType.Boolean);
            parameters.Add("withdrawlEmail", withdrawlChecked, DbType.Boolean);
            parameters.Add("ForgotPasswordWa", forgotPasswordwaChecked, DbType.Boolean);
            parameters.Add("ForgotPasswordEmail", forgotPasswordChecked, DbType.Boolean);
            parameters.Add("ResetPasswordWa", resetPasswordwaChecked, DbType.Boolean);
            parameters.Add("ResetPassword", resetPasswordChecked, DbType.Boolean);

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                var result = connection.Execute(sp_update, parameters, commandType: CommandType.StoredProcedure);
                
            }

            return RedirectToAction("Index");
        }

    }
}