using Ci_Project.Entities.Models;
using Ci_Project.Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ci_Project.Repository.Interface
{
    public interface IUserRepository
    {
        public UserViewModel getprofile(string email);
        public void SaveUserDetails(UserViewModel userView , int uid);
        public bool changePass(UserViewModel userView, string email);
        public bool changeAva(IFormFile image, string email);
        public List<Country> GetCountryList();
        public List<City> GetCityList(int id);

    }
}
