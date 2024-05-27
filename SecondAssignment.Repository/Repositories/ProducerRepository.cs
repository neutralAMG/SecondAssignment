using Microsoft.EntityFrameworkCore;
using SecondAssignment.Database.Context;
using SecondAssignment.Database.Entities;
using SecondAssignment.Infraestructure.Core;
using SecondAssignment.Infraestructure.Interfaces;

namespace SecondAssignment.Infraestructure.Repositories
{
    public class ProducerRepository : BaseRepository<Producer>, IProducerRepository
    {
        public ProducerRepository(SecondAssignmentContext context) : base(context)
        {
            
        }

        public override async Task<Producer> GetById(Guid id)
        {
            return await context.Producers.Include(pr => pr.Series).FirstOrDefaultAsync(pr => pr.ProducersId == id)!;
        }

        public override async Task<List<Producer>> GetAll()
        {
            return await context.Producers.Include(pr => pr.Series).ToListAsync()!;
        }

        public override async Task Save(Producer entity)
        {
            try
            {
                if (await Exits(pr => pr.Name == entity.Name)) return;
                await base.Save(entity);
            }
            catch 
            {
                throw;
            }
        }

        public override async Task Update(Producer entity)
        {
            Producer ToBeUpdatedProducer = await GetById(entity.ProducersId);
            try
            {
                ToBeUpdatedProducer.Name = entity.Name;
                

                await base.Update(ToBeUpdatedProducer);

            }
            catch 
            {
                throw;
            }
        }       
        
        public override async Task Delete(Producer entity)
        {
            Producer ToBeDeletedProducer = await GetById(entity.ProducersId);
            try
            {

                await base.Delete(ToBeDeletedProducer);

            }
            catch 
            {
                throw;
            }
        }
    }
}
