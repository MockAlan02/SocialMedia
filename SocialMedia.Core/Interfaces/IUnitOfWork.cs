using SocialMedia.Core.Entities;

namespace SocialMedia.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository PostRepository {  get; }
        IRepository<User> UserRepository {  get; }
        IRepository<Comments> CommentRepository {  get; }
        ISecurityRepository SecurityRepository {  get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
