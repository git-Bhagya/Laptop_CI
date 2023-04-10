using Ci_Project.Entities.ViewModels;
using Ci_Project.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ci_Project.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userrepository;

        public UserController(IUserRepository userRepository)
        {
            _userrepository = userRepository;
        }
        public IActionResult UserProfile()
        {
            var identity = User.Identity as ClaimsIdentity;
            var email = identity?.FindFirst(ClaimTypes.Email)?.Value;
            var x = _userrepository.getprofile(email);
            //_userrepository.getprofile(email);
            return View(x);
        }

        //userDetails
        public IActionResult SaveDetails(UserViewModel userView)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;

            var email = identity?.FindFirst(ClaimTypes.Email)?.Value;
            _userrepository.SaveUserDetails(userView,Convert.ToInt32(uid));
            return RedirectToAction("UserProfile", "User");
        }

        //change avatar
        public IActionResult changeAvatar(IFormFile imageFile)
        {
            var identity = User.Identity as ClaimsIdentity;
            var email = identity?.FindFirst(ClaimTypes.Email)?.Value;
            //var x = _userrepository.changeAva(image, email);
            
            return RedirectToAction("UserProfile", "User");
        }
        //change pwd
        public IActionResult changePassword(UserViewModel userView)
        {
            var identity = User.Identity as ClaimsIdentity;
            var email = identity?.FindFirst(ClaimTypes.Email)?.Value;
            var x = _userrepository.changePass(userView, email);
            if (x == true)
            {
                TempData["success"] = "Password changed successfully";
            }
            else
            {
                TempData["Error"] = "Incorrect old password";
            }
            return RedirectToAction("UserProfile", "User");
        }

        //get city
        public JsonResult getCityList(int id)
        {
            var cityList = _userrepository.GetCityList(id);
            return Json(cityList);
        }
        //get country
        public JsonResult getCountryList()
        {
            var countryList = _userrepository.GetCountryList();
            return Json(countryList);
        }
    }
}
