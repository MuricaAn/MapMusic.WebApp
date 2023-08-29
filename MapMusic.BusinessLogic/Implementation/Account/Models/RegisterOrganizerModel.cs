using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.Account.Models
{
    public class RegisterOrganizerModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordVerification { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public int PhotoId { get; set; }
    }
}
