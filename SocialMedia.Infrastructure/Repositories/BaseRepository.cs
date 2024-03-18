using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;

namespace SocialMedia.Infrastructure.Repositories
{
    //This class is for implement All the consult every Repository should do
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SocialMediaContext _context;

        private protected readonly DbSet<T> _entities;
        public BaseRepository(SocialMediaContext context)
        {
            _context = context;
            
            //We make a generic implementation of set data,
            //this can be post or user is dependable what we want it
            _entities = context.Set<T>();  
        }

        public async Task Add(T entity)
        {
          await _entities.AddAsync(entity); 
        }

        public async Task Delete(int id)
        {
            var currentEntity = await GetById(id);
            _entities.Remove(currentEntity);   
        }

        public IEnumerable<T> GetAll()
        {
            //Aprende consultas Diferida
            return  _entities.AsEnumerable();
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
         
        }
    }
}
