using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DTOs;
using MoviesAPI.Exceptions;
using MoviesAPI.Services;
using System.Net;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("Movies")]
    public class MovieController : ControllerBase
    {

        private readonly ILogger<MovieController> _logger;
        private readonly IMovieRepo _movieRepo;

        public MovieController(ILogger<MovieController> logger, IMovieRepo movieRepo)
        {
            _logger = logger;
            _movieRepo = movieRepo;
        }

        [HttpGet("{id}", Name = "GetMovie")]
        public IActionResult GetMovie(int id)
        {
            try
            {
                var movie = _movieRepo.GetMovieById(id);

                return Ok(movie);
            }
            catch (MovieNotFoundException ex)
            {
                return new NotFoundObjectResult($"Movie with ID {ex.MovieId} was not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred while getting movie with ID {id}");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet(Name = "GetAllMovies")]
        public IActionResult GetAllMovies()
        {
            try
            {
                var movies = _movieRepo.SearchMovies(null);

                return Ok(movies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while getting all movies");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost(Name = "CreateMovie")]
        public IActionResult CreateMovie(CreateMovieRequestDTO request)
        {
            try
            {
                var movie = _movieRepo.CreateMovie(request);

                return Ok(movie);
            }
            catch (DuplicateMovieExistsException ex)
            {
                return Conflict($"A movie with the Title \"{request.Title}\" already exists");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while creating movie");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPatch("{id}", Name = "UpdateMovie")]
        public IActionResult UpdateMovie(int id, UpdateMovieRequestDTO request)
        {
            try
            {
                _movieRepo.UpdateMovie(id, request);

                return Ok();
            }
            catch (MovieNotFoundException ex)
            {
                return new NotFoundObjectResult($"Movie with ID {ex.MovieId} was not found");
            }
            catch (DuplicateMovieExistsException ex)
            {
                return Conflict($"A movie with the Title \"{request.Title}\" already exists");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred while updating movie with ID {id}");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}", Name = "DeleteMovie")]
        public IActionResult DeleteMovie(int id)
        {
            try
            {
                _movieRepo.DeleteMovie(id);

                return Ok();
            }
            catch (MovieNotFoundException ex)
            {
                return new NotFoundObjectResult($"Movie with ID {ex.MovieId} was not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred while deleting movie with ID {id}");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("Search", Name = "SearchMovies")]
        public IActionResult SearchMovies(string query)
        {
            try
            {
                var movies = _movieRepo.SearchMovies(query);

                return Ok(movies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while searching movies");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("Rate", Name = "RateMovie")]
        public IActionResult RateMovie(MovieRatingRequestDto request)
        {
            try
            {
                _movieRepo.SetUserVoteOnMovie(request.MovieId, request.UserId, request.Vote);

                return Ok();
            }
            catch (MovieNotFoundException ex)
            {
                return new NotFoundObjectResult($"Movie with ID {ex.MovieId} was not found");
            }
            catch (UserNotFoundException ex)
            {
                return new NotFoundObjectResult($"User with Id {ex.UserId} was not found. Please register one using the /User endpoint and use the GUID it returns if you are not registered");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while rating movie");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
