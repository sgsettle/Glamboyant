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
        private readonly string _userID;

        public ReviewService(string userID)
        {
            _userID = userID;
        }

        public bool CreateReview(HttpPostedFileBase file, ReviewCreate model)
        {
            model.Image = ConvertToBytes(file);

            var entity =
                new Review()
                {
                    UserID = _userID,
                    Rating = model.Rating,
                    Text = model.Text,
                    Image = model.Image
                };

            using (var apt = new ApplicationDbContext())
            {
                apt.Reviews.Add(entity);
                return apt.SaveChanges() == 1;
            }
        }        

        public IEnumerable<ReviewListItem> GetReviews()
        {
            using (var rvw = new ApplicationDbContext())
            {
                var query =
                    rvw
                        .Reviews
                        .Where(e => e.UserID == _userID)
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
                        .Single(e => e.ReviewID == id && e.UserID == _userID);
                return
                    new ReviewDetail
                    {
                        ReviewID = entity.ReviewID,
                        Rating = entity.Rating,
                        Text = entity.Text,
                        Image = entity.Image,
                        UserID = entity.UserID
                    };
            }
        }

        public bool UpdateReview(HttpPostedFileBase file, ReviewEdit model)
        {
            model.Image = ConvertToBytes(file);

            using (var rvw = new ApplicationDbContext())
            {
                var entity =
                    rvw
                        .Reviews
                        .Single(e => e.ReviewID == model.ReviewID && e.UserID == _userID);

                entity.Rating = model.Rating;
                entity.Text = model.Text;
                entity.Image = model.Image;

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
                        .Single(e => e.ReviewID == reviewID && e.UserID == _userID);

                rvw.Reviews.Remove(entity);

                return rvw.SaveChanges() == 1;
            }
        }

        public byte[] GetImageFromDB(int id)
        {
            using (var rvw = new ApplicationDbContext())
            {
                var q = from temp in rvw.Reviews where temp.ReviewID == id select temp.Image;
                //var t = rvw.Reviews.
                //    Where(e => e.ReviewID == id)
                //    .Select(e => e.Image);
                byte[] cover = q.First();
                return cover;
            }
        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }
    }
}
