using Glamboyant.Data;
using Glamboyant.Models.HairServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glamboyant.Services
{
    public class HairServiceService
    {
        private readonly Guid _userId;

        public HairServiceService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateHairService(HairServiceCreate model)
        {
            var entity =
                new HairService()
                {
                    OwnerID = _userId,
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price
                };

            using (var hs = new ApplicationDbContext())
            {
                hs.HairServices.Add(entity);
                return hs.SaveChanges() == 1;
            }
        }

        public IEnumerable<HairServiceListItem> GetHairServices()
        {
            using (var hs = new ApplicationDbContext())
            {
                var query =
                    hs
                        .HairServices
                        .Where(e =>  e.OwnerID == _userId)
                        .Select(
                            e =>
                                new HairServiceListItem
                                {
                                    HairServiceID = e.HairServiceId,
                                    Name = e.Name,
                                    Description = e.Description,
                                    Price = e.Price
                                }
                            );
                return query.ToArray();
            }
        }
    }
}
