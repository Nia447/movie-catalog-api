using CatalogFilms.Data.Models;
using CatalogFilms.Data.Models.DTO;

namespace CatalogFilms.Services
{
    public interface IConverterService
    {
        MovieDTO ConvertMovieToMovieDTO(Movie movie);
        List<GenreDTO> ConvertListGenreToGenreDTO(ICollection<Genre> genres);
        GenreDTO ConvertGenreToGenreDTO(Genre genre);
        List<ReviewShortDTO> ConvertListReviewToReviewShortDTO(ICollection<Review> reviews);
        ReviewShortDTO ConvertReviewToReviewShortDTO(Review review);
        List<ReviewDTO> ConvertListReviewToReviewDTO(ICollection<Review> reviews);
        ReviewDTO ConvertReviewToReviewDTO(Review review);
        UserShortDTO ConvertUserToUserShortDTO(User user);
    }
    public class ConverterService : IConverterService
    {
        public MovieDTO ConvertMovieToMovieDTO(Movie movie)
        {
            return new MovieDTO
            {
                Country = movie.Country,
                Id = movie.Id,
                Name = movie.Name,
                Poster = movie.Poster,
                Year = movie.Year,
                Genres = ConvertListGenreToGenreDTO(movie.Genres),
                Reviews = ConvertListReviewToReviewShortDTO(movie.Reviews)
            };
        }

        public List<GenreDTO> ConvertListGenreToGenreDTO(ICollection<Genre> genres)
        {
            var genresDTO = new List<GenreDTO>();
            foreach (var genre in genres)
            {
                genresDTO.Add(ConvertGenreToGenreDTO(genre));
            }
            return genresDTO;
        }

        public GenreDTO ConvertGenreToGenreDTO(Genre genre)
        {
            return new GenreDTO
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }

        public List<ReviewShortDTO> ConvertListReviewToReviewShortDTO(ICollection<Review> reviews)
        {
            var reviewShortDTO = new List<ReviewShortDTO>();
            foreach (var review in reviews)
            {
                reviewShortDTO.Add(ConvertReviewToReviewShortDTO(review));
            }
            return reviewShortDTO;
        }

        public ReviewShortDTO ConvertReviewToReviewShortDTO(Review review)
        {
            return new ReviewShortDTO
            {
                Id = review.Id,
                Rating = review.Rating
            };
        }

        public List<ReviewDTO> ConvertListReviewToReviewDTO(ICollection<Review> reviews)
        {
            var reviewDTO = new List<ReviewDTO>();
            foreach (var review in reviews)
            {
                reviewDTO.Add(ConvertReviewToReviewDTO(review));
            }
            return reviewDTO;
        }

        public ReviewDTO ConvertReviewToReviewDTO(Review review)
        {
            return new ReviewDTO
            {
                Id = review.Id,
                Rating = review.Rating,
                CreateDateTime = review.CreateDateTime,
                IsAnonymous = review.IsAnonymous,
                ReviewText = review.ReviewText,
                Author = ConvertUserToUserShortDTO(review.Author)
            };
        }

        public UserShortDTO ConvertUserToUserShortDTO(User user)
        {
            if (user == null)
                return null;

            return new UserShortDTO
            {
                UserId = user.Id,
                Avatar = user.AvatarLink,
                NickName = user.UserName
            };
        }
    }
}
