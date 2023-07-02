using Application.Activities;
using Application.Activities.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Application;
using Application.Interfaces;
using Infrastructure.Security;
using Infrastructure.Photos;
using Application.Core;

namespace API.Extensions
{
    //Static because we don't anyone to implement this class.
    public static class ApplicationServiceExtensions
    {
        //The parameter is what we want to extend.
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            //CORS -- Allowing React Request
            services.AddCors(opt =>
            {
                opt.AddPolicy(
                    "CorsPolicy",
                    policy =>
                    {
                        policy
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .WithOrigins("http://localhost:3000");
                    }
                );
            });

            //Mediatr Service
            services.AddMediatR(typeof(List.Handler));
            //AutoMapper Service
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            //Fluent Services
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<Create>();
            services.AddHttpContextAccessor();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IPhotoAccessor, PhotoAccessor>();
            services.Configure<CloudinarySettings>(config.GetSection("Cloudinary"));

            return services;
        }
    }
}


//This is the hub for services to be used in this API