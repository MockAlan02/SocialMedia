using FluentValidation.AspNetCore;
using SocialMedia.Infrastructure.Filters;
using SocialMedia.Infrastructure.Mapping;
using SocialMedia.Infrastructure.Extensions;


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


//Adding Extension Options Configuration
builder.Services.AddOptions(builder.Configuration);

//Configure Personalizada en appsettings, mapping values from
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

//Database Connection
builder.Services.AddDbContexts(builder.Configuration);
//Services
builder.Services.AddServices();


builder.Services.AddControllers().AddNewtonsoftJson(option =>
{
    //It just to void a Loop 
    option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    option.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
});

//Cookies service
builder.Services.AddAuthenticationService(builder.Configuration);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ValidationFilter>();
}).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ValidationFilter>());
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
