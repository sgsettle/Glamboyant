using Glamboyant.Data;
using Glamboyant.Models.ReviewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Glamboyant.Services
{
    public class ReviewService
    {
        private readonly Guid _userID;
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public ReviewService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateReview(HttpPostedFileBase file, ReviewCreate model)
        {
            model.Image = ConvertToBytes(file);

            var entity =
                new Review()
                {
                    Rating = model.Rating,
                    Text = model.Text,
                    Image = model.Image,
                    UserID = model.UserID
                };

            using (var apt = new ApplicationDbContext())
            {
                apt.Reviews.Add(entity);
                return apt.SaveChanges() == 1;
            }
        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        public IEnumerable<ReviewListItem> GetReviews()
        {
            using (var rvw = new ApplicationDbContext())
            {
                var query =
                    rvw
                        .Reviews
                        .Where(e => e.User.OwnerID == _userID)
                        .Select(
                            e =>
                                new ReviewListItem
                                {
                                    ReviewID = e.ReviewID,
                                    Rating = e.Rating,
                                    Text = e.Text,
                                    Image = e.Image,
                                    UserID = e.UserID
                                }
                        );

                return query.ToArray();
            }
        }

        public ReviewDetail GetReviewByID(int id)
        {
            using (var rvw = new ApplicationDbContext())
            {
                var entity =
                    rvw
                        .Reviews
                        .Single(e => e.ReviewID == id && e.User.OwnerID == _userID);
                return
                    new ReviewDetail
                    {
                        ReviewID = entity.ReviewID,
                        Rating = entity.Rating,
                        Text = entity.Text,
                        UserID = entity.UserID
                    };
            }
        }

        public bool UpdateReview(ReviewEdit model)
        {
            using (var rvw = new ApplicationDbContext())
            {
                var entity =
                    rvw
                        .Reviews
                        .Single(e => e.ReviewID == model.ReviewID && e.User.OwnerID == _userID);

                entity.Rating = model.Rating;
                entity.Text = model.Text;

                return rvw.SaveChanges() == 1;
            }
        }

        public bool DeleteReview(int reviewID)
        {
            using (var rvw = new ApplicationDbContext())
            {
                var entity =
                    rvw
                        .Reviews
                        .Single(e => e.ReviewID == reviewID && e.User.OwnerID == _userID);

                rvw.Reviews.Remove(entity);

                return rvw.SaveChanges() == 1;
            }
        }
    }
}
