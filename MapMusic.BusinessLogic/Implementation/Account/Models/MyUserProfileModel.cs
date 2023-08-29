using MapMusic.Entities.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.Account.Models
{
    public class MyUserProfileModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ProfilePhotoId { get; set; }
        public IFormFile NewProfilePhoto { get; set; }
        public string CurrentPassword { get; set; } 
        public string NewPassword { get; set; }
    }
}
