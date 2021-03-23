using AutoMapper;
using Data;
using Domain.DTOs;
using Service.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Service.Auth
{
    public class AuthService : IAuthService
    {
        private ShopDbContext shopDbContext;
        private IMapper _mapper; // Remove unused code
        private IJwtManager _jwtManager;
        private readonly IUserService userService;

        public AuthService(ShopDbContext shopDbContext, IMapper mapper, IJwtManager jwtManager, IUserService userService)
        {
            this.shopDbContext = shopDbContext;
            _mapper = mapper;
            _jwtManager = jwtManager;
            this.userService = userService;
        }
        public string Login(UserLogin data)
        {
            if (data.UserName == null)
                throw new Exception("User is not found!");
            //if (data == null)
            //    return false;
            using (ShopDbContext shopDbContext = this.shopDbContext)
            {
                //var user = userService.GetUserByUserName(data.UserName);
                //if(user == null || user.Password != data.Password)
                //{
                //    throw new Exception("Invalid password");
                //}

                // Todo: Check and implement service
                var users = shopDbContext.Users.ToList();
                var account = users.Any(a => a.UserName == data.UserName && a.Password == data.Password);
                if (!account)
                    throw new Exception("Invalid password"); // Todo: Must add braces
            }

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, data.UserName)
            };

            // Generate JWT token
            var token = _jwtManager.GenerateToken(claims, DateTime.Now);
            return token;
        }
    }
}
