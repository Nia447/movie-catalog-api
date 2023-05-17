using CatalogFilms.Data;
using CatalogFilms.Data.Models;
using CatalogFilms.Data.Models.DTO;

namespace CatalogFilms.Services
{
    public interface IReviewService
    {
        bool DoesUserHaveReview(string jwt, Guid movieId);
        bool IsReviewUsers(string jwt, Guid reviewId);
        bool AddReview(string jwt, Guid movieId, ReviewModifyDTO reviewShortDTO);
        bool EditReview(Guid reviewId, ReviewModifyDTO reviewShortDTO);
        bool DeleteReview(Guid reviewId, Guid movieId);
    }
    public class ReviewService : IReviewService
    {
        private ApplicationDbContext _context;
        private ITokenService _tokenService;

        public ReviewService(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public bool DoesUserHaveReview(string jwt, Guid movieId)
        {
            var user = _tokenService.GetUserWithJwt(jwt);
            var movie = _context.Movies.FirstOrDefault(x => x.Id == movieId);

            if (user == null)
                return false;

            var check = _context.Reviews.FirstOrDefault(x => x.Author.Id == user.Id && x.MovieToReview == movie);

            if (check != null)
                return true;
            return false;
        }

        public bool IsReviewUsers(string jwt, Guid reviewId)
        {
            var user = _tokenService.GetUserWithJwt(jwt);

            if (user == null)
                return false;

            if (_context.Reviews.FirstOrDefault(x => x.Id == reviewId) != null)
                return true;
            return false;
        }

        public bool AddReview(string jwt, Guid movieId, ReviewModifyDTO reviewModifyDTO)
        {
            var user = _tokenService.GetUserWithJwt(jwt);
            var movie = _context.Movies.FirstOrDefault(x => x.Id == movieId);


            if (user == null || movie == null)
            {
                return false;
            }

            try
            {
                var review = new Review
                {
                    IsAnonymous = reviewModifyDTO.IsAnonymous,
                    ReviewText = reviewModifyDTO.ReviewText,
                    Rating = reviewModifyDTO.Rating,
                    Author = user,
                    MovieToReview = movie,
                    CreateDateTime = DateTime.Now,
                };

                movie.Reviews.Add(review);
                _context.Reviews.Add(review);
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditReview(Guid reviewId, ReviewModifyDTO reviewModifyDTO)
        {
            var review = _context.Reviews.FirstOrDefault(x => x.Id == reviewId);

            if (review == null)
                return false;

            try
            {
                review.ReviewText = reviewModifyDTO.ReviewText;
                review.Rating = reviewModifyDTO.Rating;
                review.IsAnonymous = reviewModifyDTO.IsAnonymous;

                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteReview(Guid reviewId, Guid movieId)
        {
            var review = _context.Reviews.FirstOrDefault(x => x.Id == reviewId);
            var movie = _context.Movies.FirstOrDefault(x => x.Id == movieId);


            if (review == null || movie == null)
                return false;

            try
            {
                movie.Reviews.Remove(review);
                _context.Reviews.Remove(review);
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
