using Infinite.OnlineMovieTicketBookingSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infinite.OnlineMovieTicketBookingSystem.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IConfiguration _configuration;

        public AccountsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
                    var result = await client.PostAsJsonAsync("Accounts/Login", login);
                    if (result.IsSuccessStatusCode)
                    {
                        string token = await result.Content.ReadAsAsync<string>();
                        HttpContext.Session.SetString("token", token);
                        return RedirectToAction("Index", "Movies");
                      
                    }
                    ModelState.AddModelError("", "Invalid Username or Password");
                }
            }
            return View(login);
        }
        [HttpGet]
        public async Task <IActionResult> LoginName() 
        { 
           using(var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization=new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",HttpContext.Session.GetString("token"));
                client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
                var result = await client.GetAsync("Accounts/GetName");
                if(result.IsSuccessStatusCode)
                {
                     var name= await result.Content.ReadAsStringAsync();
                    ViewBag.Name = name;
                    return PartialView("_LoginPartial");
                }
                else
                {
                    return BadRequest();
                }

            }
        }
        [HttpPost]
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("token");
            return RedirectToAction("Index", "Home");

        }





        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [Route("Accounts/signup")]
        public IActionResult SignUp(SignUpViewModel signUp)
        {

            if (ModelState.IsValid)
            {
                return RedirectToAction("Login", "Accounts");
            }
            else
            {
                return View(signUp);
            }
        }
    }
}
