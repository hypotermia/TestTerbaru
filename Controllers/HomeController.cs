using Dapper;
using Microsoft.AspNetCore.Mvc;
using OTPSettings.Models;
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
            var parameters = new
            {
                id = Guid.NewGuid(),
                checklistAllWa = whatsappChecked,
                checklistAllEmail = emailChecked,
                withdrawlWa = withdrawlwaChecked,
                withdrawlEmail = withdrawlChecked,
                ForgotPasswordWa = forgotPasswordwaChecked,
                ForgotPasswordEmail = forgotPasswordChecked,
                ResetPasswordWa = resetPasswordwaChecked,
                ResetPassword = resetPasswordChecked
            };
            using(var connection = new SqlConnection(connString))
            {
                var insert = connection.Execute(sp_update, parameters);
            }
            return RedirectToAction("Index");
        }

    }
}