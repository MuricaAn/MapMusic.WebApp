using MapMusic.Common.DTOs;
using MapMusic.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Base
{
    public class BaseService
    {
        public readonly UnitOfWork UnitOfWork;
        public readonly CurrentUserDTO CurrentUser;

        public BaseService(ServiceDependencies serviceDependencies)
        {
            UnitOfWork = serviceDependencies.UnitOfWork;
            CurrentUser = serviceDependencies.CurrentUser;
        }
    }
}
