using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Services;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Filters;
using SocialMedia.Infrastructure.Interfaces;
using SocialMedia.Infrastructure.Mapping;
using SocialMedia.Infrastructure.Options;
using SocialMedia.Infrastructure.Repositories;
using SocialMedia.Infrastructure.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 

builder.Services.AddControllers(options =>
{
    //Adding Personalize HandleError
    options.Filters.Add<GlobalExceptionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configure Personalizada en appsettings, mapping values from
//appsetting section adding data to call PaginationOptions
builder.Services.Configure<PaginationOptions>(builder.Configuration.GetSection("Pagination"));

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

//Interfaces Services
builder.Services.AddScoped<IPostService, PostService>();
//This is a generic repository using pattern repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
//Using pattern UnitOfWork with repository pttern
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddSingleton<IUriService>(provider =>
{
    var accessor = provider.GetRequiredService<IHttpContextAccessor>();
    var request = accessor?.HttpContext!.Request;
    var absoluteUri = string.Concat(request!.Scheme, "://", request.Host.ToUriComponent());
    return new UriService(absoluteUri);
});

//Database Connection
builder.Services.AddDbContext<SocialMediaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

builder.Services.AddControllers().AddNewtonsoftJson(option =>
{
    //It just to void a Loop 
    option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    option.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
});

//Mejorar
builder.Services.AddMvc(option =>
{
    //Validatori filter
    option.Filters.Add<ValidationFilter>();
}).AddFluentValidation(option =>
{
    option.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
