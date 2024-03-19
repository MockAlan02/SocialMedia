using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
namespace SocialMedia.Core.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SecurityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Security> LoginByCredentials(UserLogin login)
        {
            return await _unitOfWork.SecurityRepository.LoginByCredentials(login);
        }
        public async Task RegisterUser(Security user)
        {
            await _unitOfWork.SecurityRepository.Add(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
