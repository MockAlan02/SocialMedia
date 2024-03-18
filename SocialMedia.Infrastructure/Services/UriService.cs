using SocialMedia.Core.QueryFilters;
using SocialMedia.Infrastructure.Interfaces;
namespace SocialMedia.Infrastructure.Services
{
    public class UriService(string baseUri) : IUriService
    {
        private readonly string _baseUri = baseUri;
        public Uri GetPostPaginationUri(PostQueryFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
    }
}
