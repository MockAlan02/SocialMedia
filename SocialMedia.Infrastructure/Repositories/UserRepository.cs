namespace SocialMedia.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using SocialMedia.Core.Entities;
    using SocialMedia.Core.Interfaces;
    using SocialMedia.Infrastructure.Data;

    public class UserRepository(SocialMediaContext socialMediaContext) : IUserRepository
    {
        private readonly SocialMediaContext _context = socialMediaContext;

        public async Task<IEnumerable<User>> GetUsers() => await _context.Users.ToListAsync();

        public async Task<User> GetUser(int id) => await _context.Users.FirstOrDefaultAsync(x => x.IdUser == id)!;

        public async Task<User> Insert(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }

}
