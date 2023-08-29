using MapMusic.Common.DTOs;
using MapMusic.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Base
{
    public class ServiceDependencies
    {
        public UnitOfWork UnitOfWork { get; set; }
        public CurrentUserDTO CurrentUser { get; set; }

        public ServiceDependencies(UnitOfWork unitOfWork, CurrentUserDTO currentUser)
        {
            UnitOfWork = unitOfWork;
            CurrentUser = currentUser;
        }
    }
}
