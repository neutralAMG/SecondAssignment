
using Microsoft.EntityFrameworkCore;
using SecondAssignment.Database.Context;
using SecondAssignment.Database.Entities;
using SecondAssignment.Infraestructure.Core;
using SecondAssignment.Infraestructure.Interfaces;

namespace SecondAssignment.Infraestructure.Repositories
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(SecondAssignmentContext context) : base(context)
        {
        }


        public override async Task<Genre> GetById(Guid id)
        {
            return (await context.Genres.Include(ge => ge.Series).FirstOrDefaultAsync( g => g.GenreId == id))!;
        }

        public override async Task<List<Genre>> GetAll()
        {
            return await context.Genres.Include(ge => ge.Series).ToListAsync()!;
        }

        public override async Task Save(Genre entity)
        {
            
            try
            {
                if (await Exits(ge => ge.Name == entity.Name)) return;

                await base.Save(entity);

            }
            catch 
            {
                throw;
            }
        }

        public override async Task Update(Genre entity)
        {
            Genre ToBeUpdatedGenre = await GetById(entity.GenreId);
            try
            {
                ToBeUpdatedGenre.Name = entity.Name;
                await base.Update(ToBeUpdatedGenre);

            }
            catch 
            {
                throw;
            }
        }
        public override async Task Delete(Genre entity)
        {
            Genre ToBeDeletedGemre = await GetById(entity.GenreId);
            try
            {
                await base.Delete(ToBeDeletedGemre);

            }
            catch 
            {
                throw;
            }
        }
    }
}
