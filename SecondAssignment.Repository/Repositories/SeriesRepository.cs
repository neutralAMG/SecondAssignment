
using Microsoft.EntityFrameworkCore;
using SecondAssignment.Database.Context;
using SecondAssignment.Database.Entities;
using SecondAssignment.Infraestructure.Core;
using SecondAssignment.Infraestructure.Interfaces;

namespace SecondAssignment.Infraestructure.Repositories
{
    public class SeriesRepository : BaseRepository<Series>, ISeriesRepository
    {
        public SeriesRepository(SecondAssignmentContext context) : base(context)
        {
        }


        public override async Task<Series> GetById(Guid id)
        {
            return await  context.Series.Include(se => se.PrimaryGenre).Include(se => se.SecundaryGenre)
                .Include(se => se.Producer).FirstOrDefaultAsync(se => se.SeriesId == id)!;
        }

        public override async Task<List<Series>> GetAll()
        {
            return await context.Series.Include(se => se.PrimaryGenre).Include(se => se.SecundaryGenre)
                .Include(se => se.Producer).ToListAsync();
        }

        public async Task<List<Series>> GetByGenreId(Guid genreId)
        {
            return await  context.Series.Include(se => se.PrimaryGenre).Include(se => se.SecundaryGenre)
                .Include(se => se.Producer).Where(se => se.PrimaryGenreId == genreId || se.SecundaryGenreId == genreId).ToListAsync();
        }

        public async Task<List<Series>> GetByProducerId(Guid producerId)
        {
            return await context.Series.Include(se => se.PrimaryGenre).Include(se => se.SecundaryGenre)
                .Include(se => se.Producer).Where(se => se.ProducerId == producerId).ToListAsync();
        }

        public override async Task Save(Series entity)
        {
            try
            {
                if ( await Exits(se => se.Name == entity.Name)) {
                    return;
                }
            await base.Save(entity);

            }
            catch 
            {
                throw;
            }

        }

        public override async Task Update(Series entity)
        {
            Series ToBeUpdatetdSeries = await GetById(entity.SeriesId);

            try
            {

                    ToBeUpdatetdSeries.Name = entity.Name;
                    ToBeUpdatetdSeries.ImgUrlPath = entity.ImgUrlPath;
                    ToBeUpdatetdSeries.PrimaryGenreId = entity.PrimaryGenreId;
                    ToBeUpdatetdSeries.SecundaryGenreId = entity.SecundaryGenreId;
                    ToBeUpdatetdSeries.VideoUrlPath = entity.VideoUrlPath;
                    ToBeUpdatetdSeries.ProducerId = entity.ProducerId;
               
              await base.Update(ToBeUpdatetdSeries);
            }
            catch 
            {
                throw;
            }

        }

        public override async Task Delete(Series entity)
        {

            Series ToBeDeletedSeries = await GetById(entity.SeriesId);
            try
            {

                await base.Delete(ToBeDeletedSeries);
            }
            catch 
            {
                throw;
            }

        }

        public async Task<Series> GetSeriesByName(string name)
        {
            return await context.Series.Include(se => se.PrimaryGenre).Include(se => se.SecundaryGenre)
             .Include(se => se.Producer).FirstOrDefaultAsync(se => se.Name == name);
        }
    }
}
