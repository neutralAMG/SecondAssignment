using Microsoft.IdentityModel.Tokens;
using SecondAssignment.Application.Contracts;
using SecondAssignment.Application.Core;
using SecondAssignment.Application.Dtos;
using SecondAssignment.Application.Models;
using SecondAssignment.Database.Entities;
using SecondAssignment.Infraestructure.Interfaces;
using SecondAssignment.Infraestructure.Utils.ILoggerConcrete;


namespace SecondAssignment.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IConcreteLogger _logger;

        public GenreService(IGenreRepository genreRepository, IConcreteLogger logger)
        {
            _genreRepository = genreRepository;
            _logger = logger;
        }
        public async Task<Result<GenreModel>> Get(Guid id)
        {
            Result<GenreModel> result = new();
            try
            {
                Genre genre = await _genreRepository.GetById(id);

                if (genre is null)
                {
                    result.IsSucces = false;
                    result.Message = "Error finding the Genres";
                    return result;
                }

                result.Data = new()
                {
                    GenreId = genre.GenreId,
                    Name = genre.Name,
                    Series = genre.Series.Select(se => new SeriesModel
                    {
                        SeriesId = se.SeriesId,
                        Name = se.Name,
                        Description = se.Description,
                        ImgUrlPath = se.ImgUrlPath,
                        VideoUrlPath = se.VideoUrlPath,
                        PrimaryGenre = new GenreModel { Name = se.PrimaryGenre.Name },
                        SecundaryGenre = new GenreModel { Name = se.SecundaryGenre.Name },
                        Producer = new ProducerModel { Name = se.Producer.Name },
                    }).ToList()
                };
                result.Message = "Genre get was succesfull";

            }
            catch (Exception ex)
            {
                result.IsSucces = false;
                result.Message = "Error finding the Genre";
                _logger.LogCritical(result.Message + ex.ToString());
                return result;
                throw;
            }

            return result;
        }

        public async Task<Result<List<GenreModel>>> GetAll()
        {
            Result<List<GenreModel>> result = new();
            try
            {
                var genres = await _genreRepository.GetAll();
                result.Data = genres.Select(ge => new GenreModel
                {
                    Name = ge.Name,
                    GenreId = ge.GenreId,
                    //Series = ge.Series.Select(se => new SeriesModel
                    //{
                    //    SeriesId = se.SeriesId,
                    //    Name = se.Name,
                    //    Description = se.Description,
                    //    ImgUrlPath = se.ImgUrlPath,
                    //    VideoUrlPath = se.VideoUrlPath,
                    //    PrimaryGenre = new GenreModel { Name = se.PrimaryGenre.Name },
                    //    SecundaryGenre = new GenreModel { Name = se.SecundaryGenre.Name },
                    //    Producer = new ProducerModel { Name = se.Producer.Name },
                    //}).ToList()
                }).ToList();

                if (result.Data is null)
                {
                    result.IsSucces = false;
                    result.Message = "Error finding the Genres";
                    return result;
                }
                result.Message = "Genres were get succesfully";
            }
            catch (Exception ex)
            {
                result.IsSucces = false;
                result.Message = "Error finding the Genres";
                _logger.LogCritical(result.Message + ex.ToString());
                return result;
                throw;
            }
            return result;
        }

        public async Task<Result<GenreModel>> Save(SaveGenresDtos Savedto)
        {
            Result<GenreModel> result = new();
            try
            {
                var ValidatedResult = validate(Savedto);

                if (!ValidatedResult.IsSucces)
                {
                    result.IsSucces = ValidatedResult.IsSucces;
                    result.Message = ValidatedResult.Message;
                    return result;
                }

                await _genreRepository.Save(new Genre
                {
                    Name = Savedto.Name,
                });

                result.Message = "Genre was saved Succesfully";
            }
            catch (Exception ex)
            {
                result.IsSucces = false;
                result.Message = "Error saving the Genre";
                _logger.LogCritical(result.Message + ex.ToString());
                return result;
                throw;
            }
            return result;
        }

        public async Task<Result<GenreModel>> Update(UpdateGenresDtos Updatedto)
        {
            Result<GenreModel> result = new();
            try
            {
                var ValidatedResult = validate(Updatedto);

                if (!ValidatedResult.IsSucces)
                {
                    result.IsSucces = ValidatedResult.IsSucces;
                    result.Message = ValidatedResult.Message;
                    return result;
                }

                await _genreRepository.Update(new Genre
                {
                    GenreId = Updatedto.GenreId,
                    Name = Updatedto.Name,
                });

                result.Message = "Genre was updated Succesfully";
            }
            catch (Exception ex)
            {
                result.IsSucces = false;
                result.Message = "Error updating the Genre";
                _logger.LogCritical(result.Message + ex.ToString());
                return result;
                throw;
            }
            return result;
        }
        public async Task<Result<GenreModel>> Delete(Guid id)
        {
            Result<GenreModel> result = new();
            try
            {
                Genre genreToBeDeleted = await _genreRepository.GetById(id);

                await _genreRepository.Delete(genreToBeDeleted);

                result.Message = "Genre was deleted Succesfully";
            }
            catch (Exception ex)
            {
                result.IsSucces = false;
                result.Message = "Error deleting the Genre";
                _logger.LogCritical(result.Message + ex.ToString());
                return result;
                throw;
            }
            return result;
        }
        private Result<GenreModel> validate(BaseGenresDtos baseGenresDtos)
        {
            Result<GenreModel> result = new();
            if (baseGenresDtos is null)
            {
                result.IsSucces = false;
                result.Message = "Error saving the Genre";
                return result;
            }

            if (baseGenresDtos.Name.IsNullOrEmpty())
            {
                result.IsSucces = false;
                result.Message = "the name is a mandatory field";
                return result;
            }
            return result;
        }
    }
}
