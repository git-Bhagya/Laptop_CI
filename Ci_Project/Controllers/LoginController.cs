using Ci_Project.Entities.Models;
using Ci_Project.Entities.ViewModels;
using Ci_Project.Repository.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ci_Project.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginRepository _loginRepository;
        public LoginController(ILoginRepository LoginRepository)
        {
            _loginRepository = LoginRepository;
        }
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
            {
                List<User> getUserList = _loginRepository.Login();
                var validUser = getUserList.Where(x => x.Email == loginVM.Email);

                if (validUser != null)
                {
                    if (ModelState.IsValid)
                    {

                        var validEmail = validUser.Where(u => u.Email == loginVM.Email && u.Password == loginVM.Password).FirstOrDefault();
                        if (validEmail != null)
                        {
                            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, validEmail.Email) },
                            CookieAuthenticationDefaults.AuthenticationScheme);
                            identity.AddClaim(new Claim(ClaimTypes.Name, validEmail.FirstName));
                            identity.AddClaim(new Claim(ClaimTypes.Surname, validEmail.LastName));
                            identity.AddClaim(new Claim(ClaimTypes.Sid, Convert.ToString(validEmail.UserId)));
                            var principal = new ClaimsPrincipal(identity);
                            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                            HttpContext.Session.SetString("EmailId", validEmail.Email);


                            TempData["success"] = "login is succesful.";
                            return RedirectToAction("Platform", "Home");
                        }
                        else
                        {
                            TempData["Error"] = "Incorrect password ";
                        }
                    }
                }
                else
                {
                    TempData["Error"] = "Email is not register yet Register First";
                }

                return View();
            }
        }
    
}
