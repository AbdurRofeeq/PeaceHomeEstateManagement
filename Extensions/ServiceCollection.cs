using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore; 
using PeaceHomeEstateManagement.Context;
using PeaceHomeEstateManagement.Contract.Repository;
using PeaceHomeEstateManagement.Contract.Service;
using PeaceHomeEstateManagement.Implementation.Repository;
using PeaceHomeEstateManagement.Implementation.Service;

namespace PeaceHomeEstateManagement.Extensions
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddContext(this IServiceCollection services, string connectionString)
        {
            return services
                  .AddDbContext<ApplicationContext>(a => a.UseMySQL(connectionString));
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                   .AddScoped<IAmenitiesRepository, AmenitiesRepository>()
                   .AddScoped<IPropertyRepository, PropertyRepository>()
                   .AddScoped<IPropertyTypeRepository, PropertyTypeRepository>()
                   .AddScoped<IUserRepository, UserRepository>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                   .AddScoped<IAmenitiesService, AmenitiesService>()
                   .AddScoped<IPropertyService, PropertyService>()
                   .AddScoped<IPropertyTypeService, PropertyTypeService>()
                   .AddScoped<IUserService, UserService>()
                   .AddScoped<ICloudinaryService, CloudinaryService>();
        }
    }
}