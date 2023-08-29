using MapMusic.Common.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MapMusic.BusinessLogic.Implementation.Account.Models
{
    public class RegisterArtistModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordVerification { get; set; }
        public string StageName { get; set; }
        public int ArtistType { get; set; }
        public int PhotoId { get; set; }
        public List<SelectListItem> ArtistTypeList { get; set; }
    }
}
