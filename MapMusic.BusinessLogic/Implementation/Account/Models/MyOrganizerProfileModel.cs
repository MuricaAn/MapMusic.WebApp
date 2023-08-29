using MapMusic.Entities.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.Account.Models
{
    public class MyOrganizerProfileModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public Photo ProfilePhoto { get; set; }
        public IFormFile NewProfilePhoto { get; set; }
        public int ProfilePhotoId { get; set; }
        public string Description { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
