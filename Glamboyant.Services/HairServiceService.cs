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
        private readonly string _userId;

        public HairServiceService(string userId)
        {
            _userId = userId;
        }

        public bool CreateHairService(HairServiceCreate model)
        {
            var entity =
                new HairService()
                {
                    OwnerID = _userId,
                    ServiceType = model.ServiceType,
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
                        //.Where(e =>  e.OwnerID == _userId)
                        .Select(
                            e =>
                                new HairServiceListItem
                                {
                                    HairServiceID = e.HairServiceId,
                                    ServiceType = e.ServiceType,
                                    Name = e.Name,
                                    Description = e.Description,
                                    Price = e.Price
                                }
                            );
                return query.ToArray();
            }
        }

        public HairServiceDetail GetServiceByID(int id)
        {
            using (var hs = new ApplicationDbContext())
            {
                var entity =
                    hs
                        .HairServices
                        .Single(e => e.HairServiceId == id && e.OwnerID == _userId);
                return
                    new HairServiceDetail
                    {
                        HairServiceID = entity.HairServiceId,
                        ServiceType = entity.ServiceType,
                        Name = entity.Name,
                        Description = entity.Description,
                        Price = entity.Price
                    };
            }
        }

        public bool UpdateHairService(HairServiceEdit model)
        {
            using(var hs = new ApplicationDbContext())
            {
                var entity =
                    hs
                        .HairServices
                        .Single(e => e.HairServiceId == model.HairServiceID && e.OwnerID == _userId);

                entity.ServiceType = model.ServiceType;
                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.Price = model.Price;

                return hs.SaveChanges() == 1;
            }
        }

        public bool DeleteHairService(int hairServiceID)
        {
            using (var hs = new ApplicationDbContext())
            {
                var entity =
                    hs
                        .HairServices
                        .Single(e => e.HairServiceId == hairServiceID && e.OwnerID == _userId);

                hs.HairServices.Remove(entity);

                return hs.SaveChanges() == 1;
            }
        }
    }
}
