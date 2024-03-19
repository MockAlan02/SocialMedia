using SocialMedia.Core.Entities;

namespace SocialMedia.Core.Interfaces
{
    public interface ISecurityService
    {
        Task<Security> LoginByCredentials(UserLogin login);
        Task RegisterUser(Security user);
    }
}