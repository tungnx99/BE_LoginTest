using Microsoft.Extensions.DependencyInjection;
using Service.Auth;
using Service.Product;
using Service.Repository;
using Service.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public static class ConfigureDi
    {
        public static void Setup(IServiceCollection services)
        {
            //scoped
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtManager, JwtManager>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
