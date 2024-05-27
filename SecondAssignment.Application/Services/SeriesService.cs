using SecondAssignment.Application.Contracts;
using SecondAssignment.Application.Core;
using SecondAssignment.Application.Dtos;
using SecondAssignment.Application.Models;
using SecondAssignment.Database.Entities;
using SecondAssignment.Infraestructure.Interfaces;
using SecondAssignment.Infraestructure.Utils.ILoggerConcrete;

namespace SecondAssignment.Application.Services
{
    public class SeriesService : ISeriesService
    {
        private readonly ISeriesRepository _seriesRepository;

        private readonly IConcreteLogger _Logger;

        public SeriesService(ISeriesRepository seriesRepository, IConcreteLogger logger)
        {
            _seriesRepository = seriesRepository;
            _Logger = logger;
        }
        public async Task<Result<List<SeriesModel>>> GetAll()
        {
            Result<List<SeriesModel>> result = new();
            try
            {
                var series = await _seriesRepository.GetAll();
                result.Data = series.Select(se =>
                new SeriesModel
                {
                    SeriesId = se.SeriesId,

                    Name = se.Name,

                    Description = se.Description,

                    ImgUrlPath = se.ImgUrlPath,

                    VideoUrlPath = se.VideoUrlPath,

                    PrimaryGenre = new GenreModel()
                    {
                        Name = se.PrimaryGenre.Name,
                        GenreId = se.PrimaryGenre.GenreId
                    },
                    SecundaryGenre = new GenreModel()
                    {
                        Name = se.SecundaryGenre.Name,
                        GenreId = se.SecundaryGenre.GenreId
                    },
                    Producer = new ProducerModel()
                    {
                        Name = se.Producer.Name,
                        ProducersId = se.Producer.ProducersId
                    },
                }

                ).ToList();


                if (result.Data == null)
                {
                    result.IsSucces = false;
                    result.Message = "Error finding the series";
                    return result;
                }


            }
            catch (Exception ex)
            {
                result.IsSucces = false;
                result.Message = "Error finding the series";
                _Logger.LogCritical(result.Message + ex.ToString());
                return result;
                throw;
            }

            return result;
        }

        public async  Task<Result<SeriesModel>> Get(Guid id)
        {
            Result<SeriesModel> result = new();
            try
            {
                Series seriesResult = await _seriesRepository.GetById(id);

                if (seriesResult == null)
                {
                    result.IsSucces = false;
                    result.Message = "Error finding the series";
                    return result;
                }


                result.Data = new()
                {
                    SeriesId = seriesResult.SeriesId,

                    Name = seriesResult.Name,

                    Description = seriesResult.Description,

                    ImgUrlPath = seriesResult.ImgUrlPath,

                    VideoUrlPath = seriesResult.VideoUrlPath,

                    PrimaryGenre = new GenreModel()
                    {
                        Name = seriesResult.PrimaryGenre.Name,
                        GenreId = seriesResult.PrimaryGenre.GenreId
                    },
                    SecundaryGenre = new GenreModel()
                    {
                        Name = seriesResult.SecundaryGenre.Name,
                        GenreId = seriesResult.SecundaryGenre.GenreId
                    },
                    Producer = new ProducerModel()
                    {
                        Name = seriesResult.Producer.Name,
                        ProducersId = seriesResult.Producer.ProducersId
                    },


                };

                result.Message = "Succes fiding the series";
            }
            catch (Exception ex)
            {
                result.IsSucces = false;
                result.Message = "Error finding the series";
                _Logger.LogCritical(result.Message + ex.ToString());
                return result;
                throw;
            }
            return result;
        }

        public async Task<Result<SeriesModel>> Save(SaveSeriesDto SaveDto)
        {
            Result<SeriesModel> result = new();
            try
            {
                if (SaveDto is null)
                {
                    result.IsSucces = false;
                    result.Message = "Error finding the series";
                    return result;
                }
                //Add validations 

              await _seriesRepository.Save(new Series
                {
                    Name = SaveDto.Name,
                    Description = SaveDto.Description,
                    ImgUrlPath = SaveDto.ImgUrlPath,
                    VideoUrlPath = SaveDto.VideoUrlPath,
                    ProducerId = SaveDto.ProducerId,
                    PrimaryGenreId = SaveDto.PrimaryGenreId,
                    SecundaryGenreId = SaveDto.SecundaryGenreId
                });
                //Probably should change this

                result.Message = "Series Saved without problems";

            }
            catch (Exception ex)
            {
                result.IsSucces = false;
                result.Message = "Error saving the series";
                _Logger.LogCritical(result.Message + ex.ToString());
                return result;
                throw;
            }
            return result;
        }
        public async Task<Result<SeriesModel>> Update(UpdateSeriesDto UpdateDto)
        {
            Result<SeriesModel> result = new();
            if (UpdateDto is null)
            {
                result.IsSucces = false;
                result.Message = "Error finding the series";
                return  result;
            }
            try
            {
                await _seriesRepository.Update(new Series
                {
                    SeriesId = UpdateDto.SeriesId,
                    Name = UpdateDto.Name,
                    Description = UpdateDto.Description,
                    ImgUrlPath = UpdateDto.ImgUrlPath,
                    VideoUrlPath = UpdateDto.VideoUrlPath,
                    ProducerId = UpdateDto.ProducerId,
                    PrimaryGenreId = UpdateDto.PrimaryGenreId,
                    SecundaryGenreId = UpdateDto.SecundaryGenreId
                });

                result.Message = "Series was successfully updated";
            }
            catch (Exception ex)
            {
                result.IsSucces = false;
                result.Message = "Error updating the series";
                _Logger.LogCritical(result.Message + ex.ToString());
                return result;
                throw;
            }
            return result;
        }
        public async Task<Result<SeriesModel>> Delete(Guid id)
        {
            Result<SeriesModel> result = new();
            try
            {
                Series DeletedSeries = await _seriesRepository.GetById(id);
                if (DeletedSeries == null)
                {
                    result.IsSucces = false;
                    result.Message = "series not found";
                    return result;
                }
                await _seriesRepository.Delete(DeletedSeries);

                result.Message = "Succes deleting the series";
            }
            catch (Exception ex)
            {
                result.IsSucces = false;
                result.Message = "Error deleting the series";
                _Logger.LogCritical(result.Message + ex.ToString());
                return result;
                throw;
            }
            return result;
        }

        public async Task<Result< SeriesModel>> GetSeriesByName(string name)
        {
            Result<SeriesModel> result = new();
            try
            {
                Series seriesResult = await _seriesRepository.GetSeriesByName(name);

                if (seriesResult == null)
                {
                    result.IsSucces = false;
                    result.Message = "Error finding the series";
                    return result;
                }


                result.Data = new()
                {
                    SeriesId = seriesResult.SeriesId,

                    Name = seriesResult.Name,

                    Description = seriesResult.Description,

                    ImgUrlPath = seriesResult.ImgUrlPath,

                    VideoUrlPath = seriesResult.VideoUrlPath,

                    PrimaryGenre = new GenreModel()
                    {
                        Name = seriesResult.PrimaryGenre.Name,
                        GenreId = seriesResult.PrimaryGenre.GenreId
                    },
                    SecundaryGenre = new GenreModel()
                    {
                        Name = seriesResult.SecundaryGenre.Name,
                        GenreId = seriesResult.SecundaryGenre.GenreId
                    },
                    Producer = new ProducerModel()
                    {
                        Name = seriesResult.Producer.Name,
                        ProducersId = seriesResult.Producer.ProducersId
                    },


                };

                result.Message = "Succes fiding the series";
            }
            catch (Exception ex)
            {
                result.IsSucces = false;
                result.Message = "Error finding the series";
                _Logger.LogCritical(result.Message + ex.ToString());
                return result;
                throw;
            }
            return result;
        }

        public async Task<Result<List<SeriesModel>>> GetByGenreId(Guid genreId)
        {
            Result<List<SeriesModel>> result = new();
            try
            {
                var series = await _seriesRepository.GetByGenreId(genreId);
                result.Data = series.Select(se =>
                new SeriesModel
                {
                    SeriesId = se.SeriesId,

                    Name = se.Name,

                    Description = se.Description,

                    ImgUrlPath = se.ImgUrlPath,

                    VideoUrlPath = se.VideoUrlPath,

                    PrimaryGenre = new GenreModel()
                    {
                        Name = se.PrimaryGenre.Name,
                        GenreId = se.PrimaryGenre.GenreId
                    },
                    SecundaryGenre = new GenreModel()
                    {
                        Name = se.SecundaryGenre.Name,
                        GenreId = se.SecundaryGenre.GenreId
                    },
                    Producer = new ProducerModel()
                    {
                        Name = se.Producer.Name,
                        ProducersId = se.Producer.ProducersId
                    },
                }

                ).ToList();


                if (result.Data == null)
                {
                    result.IsSucces = false;
                    result.Message = "Error finding the series";
                    return result;
                }


            }
            catch (Exception ex)
            {
                result.IsSucces = false;
                result.Message = "Error finding the series";
                _Logger.LogCritical(result.Message + ex.ToString());
                return result;
                throw;
            }

            return result;
        }

        public async Task<Result<List<SeriesModel>>> GetByProducerId(Guid producerId)
        {
            Result<List<SeriesModel>> result = new();
            try
            {
                var series = await _seriesRepository.GetByProducerId(producerId);
                result.Data = series.Select(se =>
                new SeriesModel
                {
                    SeriesId = se.SeriesId,

                    Name = se.Name,

                    Description = se.Description,

                    ImgUrlPath = se.ImgUrlPath,

                    VideoUrlPath = se.VideoUrlPath,

                    PrimaryGenre = new GenreModel()
                    {
                        Name = se.PrimaryGenre.Name,
                        GenreId = se.PrimaryGenre.GenreId
                    },
                    SecundaryGenre = new GenreModel()
                    {
                        Name = se.SecundaryGenre.Name,
                        GenreId = se.SecundaryGenre.GenreId
                    },
                    Producer = new ProducerModel()
                    {
                        Name = se.Producer.Name,
                        ProducersId = se.Producer.ProducersId
                    },
                }

                ).ToList();


                if (result.Data == null)
                {
                    result.IsSucces = false;
                    result.Message = "Error finding the series";
                    return result;
                }


            }
            catch (Exception ex)
            {
                result.IsSucces = false;
                result.Message = "Error finding the series";
                _Logger.LogCritical(result.Message + ex.ToString());
                return result;
                throw;
            }

            return result;
        }
    }
}
