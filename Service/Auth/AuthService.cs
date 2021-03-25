using AutoMapper;
using Common;
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
            {
                throw new Exception(Constants.Account.InvalidAuthInfoMsg);
            }
            //if (data == null)
            //    return false;

            var users = _mapper.Map<UserLogin, Domain.Entities.User>(data);
            var account = shopDbContext.Users.Any(a => a.UserName == users.UserName && a.Password == users.Password);
            if (!account)
            {
                throw new Exception(Constants.Account.InvalidAuthInfoMsg);
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
