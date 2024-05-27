

using SecondAssignment.Application.Contracts;
using SecondAssignment.Application.Core;
using SecondAssignment.Application.Dtos;
using SecondAssignment.Application.Models;
using SecondAssignment.Database.Entities;
using SecondAssignment.Infraestructure.Interfaces;
using SecondAssignment.Infraestructure.Utils.ILoggerConcrete;


namespace SecondAssignment.Application.Services
{
    public class ProducerService : IProducerService
    {

        private readonly IProducerRepository _producerRepository;
        private readonly IConcreteLogger _logger;

        public ProducerService(IProducerRepository producerRepository, IConcreteLogger logger)
        {

            _producerRepository = producerRepository;
            _logger = logger;
        }

        public async Task<Result<ProducerModel>> Get(Guid id)
        {
            Result<ProducerModel> result = new();
            try
            {
                Producer SelectedProducer = await _producerRepository.GetById(id);

                if (SelectedProducer == null)
                {
                    result.IsSucces = false;
                    result.Message = "Error finding the Producer";
                    return result;
                }

                result.Data = new ProducerModel
                {
                    ProducersId = SelectedProducer.ProducersId,
                    Name = SelectedProducer.Name,
                    //Series = SelectedProducer.Series.Select(se => new SeriesModel
                    //{
                    //    SeriesId = se.SeriesId,
                    //    Name = se.Name,
                    //    Description = se.Description,
                    //    ImgUrlPath = se.ImgUrlPath,
                    //    VideoUrlPath = se.VideoUrlPath,
                    //    PrimaryGenre = new GenreModel { Name = se.PrimaryGenre.Name },
                    //    SecundaryGenre = new GenreModel { Name = se.SecundaryGenre.Name },
                    //    Producer = new ProducerModel { Name = se.Producer.Name },

                    //}).ToList(),
                };
                result.Message = "success finding the Producer";
            }
            catch (Exception ex)
            {
                result.IsSucces = false;
                result.Message = "Error finding the Producer";
                _logger.LogCritical(result.Message + ex.ToString());
                return result;
                throw;
            }

            return result;
        }

        public async Task<Result<List<ProducerModel>>> GetAll()
        {
            Result<List<ProducerModel>> result = new();
            try
            {
                var producers = await _producerRepository.GetAll();
                result.Data = producers.Select(pr =>

                new ProducerModel

                {
                    ProducersId = pr.ProducersId,

                    Name = pr.Name,

                    //Series = pr.Series?.Select(se => new SeriesModel
                    //{
                    //    SeriesId = se.SeriesId,
                    //    Name = se.Name,
                    //    Description = se.Description,
                    //    ImgUrlPath = se.ImgUrlPath,
                    //    VideoUrlPath = se.VideoUrlPath,
                    //    PrimaryGenre = new GenreModel { Name = se.PrimaryGenre.Name },
                    //    SecundaryGenre = new GenreModel { Name = se.SecundaryGenre.Name },
                    //    Producer = new ProducerModel { Name = se.Producer.Name },

                    //}).ToList(),

                }

                ).ToList();


                if (result.Data == null)
                {
                    result.IsSucces = false;
                    result.Message = "Error finding the Producers";

                    return result;
                }
                result.Message = "success finding the Producers";

            }
            catch (Exception ex)
            {
                result.IsSucces = false;
                result.Message = "Error finding the Producers";
                _logger.LogCritical(result.Message + ex.ToString());
                return result;
                throw;
            }

            return result;
        }

        public async Task<Result<ProducerModel>> Save(SaveProducersDto SaveDto)
        {
            Result<ProducerModel> result = new();
            try
            {
                if (SaveDto is null)
                {
                    result.IsSucces = false;
                    result.Message = "Error saving the Producer";
                    return result;
                }
                await _producerRepository.Save(new Producer
                {
                    Name = SaveDto.Name,
                });

                result.Message = "Producer was saved succesfully";

            }
            catch (Exception ex)
            {
                result.IsSucces = false;
                result.Message = "Error saving the Producer";
                _logger.LogCritical(result.Message + ex.ToString());
                return result;
                throw;
            }
            return result;
        }

        public async Task<Result<ProducerModel>> Update(UpdateProducersDto UpdateDto)
        {
            Result<ProducerModel> result = new();
            try
            {
                if (UpdateDto is null)
                {
                    result.IsSucces = false;
                    result.Message = "Error finding the producer";
                    return result;
                }

                await _producerRepository.Update(new Producer
                {
                    ProducersId = UpdateDto.ProducersId,
                    Name = UpdateDto.Name,
                });

                result.Message = "Succesfully updated the producer";
            }
            catch (Exception ex)
            {
                result.IsSucces = false;
                result.Message = "Error updating the producer";
                _logger.LogCritical(result.Message + ex.ToString());
                return result;
            }
            return result;
        }
        public async Task<Result<ProducerModel>> Delete(Guid id)
        {
            Result<ProducerModel> result = new();
            try
            {
                Producer producerDeleted = await _producerRepository.GetById(id);
                if (producerDeleted is null)
                {
                    result.IsSucces = false;
                    result.Message = "Error finding the producer";
                    return result;
                }

                await _producerRepository.Delete(producerDeleted);

                result.Message = "Succesfully deleted the producer";
            }
            catch (Exception ex)
            {
                result.IsSucces = false;
                result.Message = "Error Deleted the producer";
                _logger.LogCritical(result.Message + ex.ToString());
                return result;
            }
            return result;
        }
    }
}
