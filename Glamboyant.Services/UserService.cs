using Glamboyant.Data;
using Glamboyant.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glamboyant.Services
{
    //public class UserService
    //{
    //    private readonly Guid _userId;

    //    public UserService(Guid userID)
    //    {
    //        _userId = userID;
    //    }

    //    public bool CreateUserService(UserCreate model)
    //    {
    //        var entity =
    //            new User()
    //            {
    //                OwnerID = _userId,
    //                FirstName = model.FirstName,
    //                LastName = model.LastName
    //            };

    //        using (var u = new ApplicationDbContext())
    //        {
    //            u.Users.Add(entity);
    //            return u.SaveChanges() == 1;
    //        }
    //    }

    //    public IEnumerable<UserListItem> GetUsers()
    //    {
    //        using(var u = new ApplicationDbContext())
    //        {
    //            var query =
    //                u
    //                    .Users
    //                    .Where(e => e.OwnerID == _userId)
    //                    .Select(
    //                        e =>
    //                            new UserListItem
    //                            {
    //                                UserID = e.UserID,
    //                                FirstName = e.FirstName,
    //                                LastName = e.LastName
    //                            }
    //                    );
    //            return query.ToArray();
    //        }
    //    }

    //    public UserDetail GetUserByID(int id)
    //    {
    //        using (var u = new ApplicationDbContext())
    //        {
    //            var entity =
    //                u
    //                    .Users
    //                    .Single(e => e.UserID == id && e.OwnerID == _userId);
    //            return
    //                new UserDetail
    //                {
    //                    UserID = entity.UserID,
    //                    FirstName = entity.FirstName,
    //                    LastName = entity.LastName
    //                };
    //        }
    //    }

    //    public bool UpdateUserService(UserEdit model)
    //    {
    //        using(var u = new ApplicationDbContext())
    //        {
    //            var entity =
    //                u
    //                    .Users
    //                    .Single(e => e.UserID == model.UserID && e.OwnerID == _userId);

    //            entity.FirstName = model.FirstName;
    //            entity.LastName = model.LastName;

    //            return u.SaveChanges() == 1;
    //        }
    //    }

    //    public bool DeleteUserService(int userID)
    //    {
    //        using (var u = new ApplicationDbContext())
    //        {
    //            var entity =
    //                u
    //                    .Users
    //                    .Single(e => e.UserID == userID && e.OwnerID == _userId);

    //            u.Users.Remove(entity);

    //            return u.SaveChanges() == 1;
    //        }
    //    }
    //}
}
