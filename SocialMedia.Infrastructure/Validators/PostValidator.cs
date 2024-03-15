using FluentValidation;
using Microsoft.AspNetCore.Rewrite;
using SocialMedia.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Validators
{
    public class PostValidator : AbstractValidator<PostDto>
    {
        public PostValidator()
        {
            RuleFor(post => post.Description)
                .NotNull()
                .Length(10, 500);
            RuleFor(post => post.Date)
                .NotEmpty();
            RuleFor(post => post.IdUser)
                .NotNull();
            RuleFor(post => post.Image)
                .NotNull().Length(10, 40);    
        }
    }
}
