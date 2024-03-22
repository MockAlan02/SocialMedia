using FluentValidation;
using SocialMedia.Core.Dtos;

namespace SocialMedia.Infrastructure.Validators
{
    public class PostValidator : AbstractValidator<PostDto>
    {
        public PostValidator()
        {
            RuleFor(post => post.Description)
                .NotNull().WithMessage("No puede ser nulo")
                .Length(10, 500).WithMessage("Longitud invalida");
            RuleFor(post => post.Date)
                .NotEmpty();
            RuleFor(post => post.IdUser)
                .NotNull();
            RuleFor(post => post.Image)
                .NotNull().Length(10, 40);
        }
    }
}
