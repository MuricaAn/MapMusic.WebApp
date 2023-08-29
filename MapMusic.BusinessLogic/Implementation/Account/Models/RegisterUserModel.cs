using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.Account.Models
{
    public class RegisterUserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordVerification { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public int PhotoId { get; set; }
    }
}
