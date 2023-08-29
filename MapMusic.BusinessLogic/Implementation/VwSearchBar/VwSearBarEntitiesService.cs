using MapMusic.BusinessLogic.Base;
using MapMusic.BusinessLogic.Implementation.Event.Models;
using MapMusic.BusinessLogic.Implementation.VwSearchBar.Models;
using MapMusic.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.VwSearchBar
{
    public class VwSearBarEntitiesService : BaseService
    {
        public VwSearBarEntitiesService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
        }

        public List<VwSearchBarEntitiesModel> GetEntitiesForSearchBar(string search)
        {
            var entities = new List<VwSearcheableEntity>();
            if (string.IsNullOrEmpty(search))
            {
                entities = UnitOfWork.VwSearcheableEntities.Get().Take(3).ToList();
            }
            else
            {
                entities = UnitOfWork.VwSearcheableEntities.Get().Where(x => x.Name.ToLower().Contains(search.ToLower())).Take(3).ToList();
            }
            var eventSearchBarModels = new List<VwSearchBarEntitiesModel>();
            var i = 0;
            foreach (var @entity in entities)
            {
                eventSearchBarModels.Add(new VwSearchBarEntitiesModel
                {
                    Id = @entity.Id,
                    Text = @entity.Name,
                    ProfilePhoto = @entity.ProfilePhoto,
                    EntityType = @entity.EntityType
                });
            }
            return eventSearchBarModels;
        }
    }
}
