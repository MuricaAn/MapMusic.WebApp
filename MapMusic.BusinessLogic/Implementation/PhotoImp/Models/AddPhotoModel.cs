using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.PhotoImp.Models
{
    public class AddPhotoModel
    {
        public IFormFile Content { get; set; }
    }
}
