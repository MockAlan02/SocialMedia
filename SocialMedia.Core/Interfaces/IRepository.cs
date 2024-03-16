using SocialMedia.Core.Entities;

namespace SocialMedia.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        public Task<T> GetById(int id);
        public Task<IEnumerable<T>> GetAll();
        public Task Add(T entity);
        public Task Update(T entity);
        public Task Delete(int id);

    }
}
