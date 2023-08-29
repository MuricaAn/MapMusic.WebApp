using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MapMusic.BusinessLogic.Implementation.Organizer.Models;
using MapMusic.Entities.Entities;

namespace MapMusic.BusinessLogic.Implementation.Organizer.Mappings
{
    public class ToOrganizerRequestShow : Profile
    {
        public ToOrganizerRequestShow()
        {
            CreateMap<OrganizerRequest, OrganizerRequestShow>();
            //CreateMap<List<OrganizerRequest>, List<OrganizerRequestShow>>()
            //    .ForMember(x => x.);

        }
    }
}
