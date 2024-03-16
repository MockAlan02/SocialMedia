using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    //This class is for implement All the consult every Repository should do
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SocialMediaContext _context;

        private readonly DbSet<T> _entities;
        public BaseRepository(SocialMediaContext context)
        {
            _context = context;
            
            //We make a generic implementation of set data,
            //this can be post or user is dependable what we want it
            _entities = context.Set<T>();  
        }

        public async Task Add(T entity)
        {
          _entities.Add(entity);
         await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var currentEntity = await GetById(id);
            _entities.Remove(currentEntity);
            await _context.SaveChangesAsync();
           
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            //Aprende consultas Diferida
            return await _entities.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task Update(T entity)
        {
            _entities.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
