using Ci_Project.Entities.Data;
using Ci_Project.Entities.Models;
using Ci_Project.Entities.ViewModels;
using Ci_Project.Repository.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ci_Project.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly CiPlatformContext _db;
        public UserRepository(CiPlatformContext db)
        {
            _db = db;
        }
        public UserViewModel getprofile(string email)
        {
            var city = _db.Cities.ToList();
            var country = _db.Countries.ToList();
            var skill = _db.Skills.ToList();
            var user = _db.Users.Where(x=>x.Email == email).FirstOrDefault();
            UserViewModel uvm = new UserViewModel()
            {
                Users = _db.Users.ToList(),
                Cities = city,
                Countries = country,
                SkillList = skill,

                Name = user.FirstName,
                Surname = user.LastName,
                EmployeeId = user.EmployeeId,
                Title = user.Title,
                Department = user.Department,
                ProfileText = user.ProfileText,
                WhyIVolunteer = user.WhyIVolunteer,
                //city = _db.Cities.Where(x => x.CityId == user.CityId).Select(x => x.Name).FirstOrDefault(),
                //country = _db.Countries.Where(x => x.CountryId == user.CountryId).Select(x => x.Name).FirstOrDefault(),
                Status = user.Status,
                LinkedInURL = user.LinkedInUrl,
                Avatar = user.Avatar
            };

            return uvm;
        }

        //User Profile save details
        //public string SaveUserDetails(UserViewModel userView,string email)
        //{


        //    data.FirstName = userView.name;
        //    data.LastName = surname;
        //    data.EmployeeId = empId;
        //    data.Title = title;
        //    data.Department = deptName;
        //    data.ProfileText = profile;
        //    data.WhyIVolunteer = whyVol;
        //    data.CityId = _db.Cities.Where(x => x.Name == city).Select(x => x.CityId).FirstOrDefault();
        //    data.CountryId = _db.Countries.Where(x => x.Name == country).Select(x => x.CountryId).FirstOrDefault();
        //    data.Status = availability;
        //    data.LinkedInUrl = linkedIn;
        //    data.UpdatedAt = DateTime.Now;
        //    //data.ManagerDetails = mng_detail;

        //    _db.Update(data);
        //    _db.SaveChanges();
        //    return "Success";
        //}
        public void SaveUserDetails(UserViewModel userView, int uid)
        {

            var alreadyExitUser = _db.Users.Where(x => x.UserId == Convert.ToInt32(uid)).FirstOrDefault();


            alreadyExitUser.FirstName = userView.Name;
            alreadyExitUser.LastName = userView.Surname;
            alreadyExitUser.EmployeeId = userView.EmployeeId;
            alreadyExitUser.Title = userView.Title;
            alreadyExitUser.Department = userView.Department;
            alreadyExitUser.ProfileText = userView.ProfileText;
            alreadyExitUser.WhyIVolunteer = userView.WhyIVolunteer;
            alreadyExitUser.LinkedInUrl = userView.LinkedInURL;
            //alreadyExitUser.Manager = userView.ManagerDetail;


            _db.SaveChanges();




        }

        //change ava
        public bool changeAva(IFormFile image, string email)
        {
            var ava = image;
            var user = _db.Users.Where(x => x.Email == email).FirstOrDefault();

            //if(user.Avatar != ava)
            //{
            //    user.Avatar = ava;
            //    _db.Update(user);
            //    _db.SaveChanges();
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return true;
        }

        //change pwd
        public bool changePass(UserViewModel userView, string email)
        {
            var oldP = userView.OldPwd;
            var user = _db.Users.Where(x => x.Email == email).FirstOrDefault();

            if (user.Password == oldP && user.Password != userView.NewPwd)
            {
                user.Password = userView.NewPwd;
                _db.Update(user);
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        //get city and country
        public List<Country> GetCountryList()
        {
            var countryList = _db.Countries.ToList();
            return countryList;
        }
        public List<City> GetCityList(int id)
        {
            var cityList = _db.Cities.Where(x => x.CountryId == id).ToList();
            return cityList;
        }
    }
}
