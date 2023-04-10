using Ci_Project.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ci_Project.Entities.ViewModels
{
    public class UserViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? EmployeeId { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public string ProfileText { get; set; }
        public string WhyIVolunteer { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public IEnumerable<Country> Countries { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public string Status { get; set; }
        public string LinkedInURL { get; set; }
        public IEnumerable<Mission> Missions { get; set; }
        
        public string Avatar { get; set; }
        public Skill Skill { get; set; }
        public IEnumerable<Skill> SkillList { get; set; }

        //change pwd
        public string OldPwd { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength = 6)]
        [RegularExpression("([a-z]|[A-Z]|[0-9]|[\\W]){4}[a-zA-Z0-9\\W]{3,11}", ErrorMessage = "Invalid password format")]
        [NotMapped]
        public string NewPwd { get; set; }

        public long cityId { get; set; }
        public long countryId { get; set; }
    }
}
