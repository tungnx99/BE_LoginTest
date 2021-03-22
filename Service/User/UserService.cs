using AutoMapper;
using Data;
using Domain.Common;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Users
{
    public class UserService : IUserService
    {
        private IMapper _mapper;
        private ShopDbContext shhopDbContext;
        public UserService(IMapper mapper, ShopDbContext shopDbContext)
        {
            this._mapper = mapper;
            this.shhopDbContext = shopDbContext;
        }
        public UserDTO GetUser(string obj)
        {
            using(ShopDbContext shopDbContext = this.shhopDbContext)
            {
                var users = shopDbContext.Users.ToList();
                var user = users.FirstOrDefault(t => t.Id == obj);

                if (user != null)
                {
                    return _mapper.Map<User, UserDTO>(user);
                }
            }
            
            return null;
        }

        public Paganation<UserDTO> GetUsers(SerachPaganationDTO<UserDTO> userPaganationDTO)
        {
            if (userPaganationDTO != null)
            {
                using (ShopDbContext shopDbContext = this.shhopDbContext)
                {
                    var users = shopDbContext.Users.ToList();
                    var result = _mapper.Map<SerachPaganationDTO<UserDTO>, Paganation<UserDTO>>(userPaganationDTO);
                    var data = users.Where(t =>
                        t.UserName.Contains(userPaganationDTO.Search.UserName ?? "")
                    ).OrderBy(t => t.Type).ThenBy(t => t.UserName).ToList();
                    result.Data = _mapper.Map<List<User>, List<UserDTO>>(data);
                    result.TotalItems = users.Count;
                    return result;
                }
            }
            return new Paganation<UserDTO>();
        }

        public bool Login(UserLogin data)
        {
            var result = false;
            //if (data == null)
            //    return false;
            using (ShopDbContext shopDbContext = this.shhopDbContext)
            {
                var users = shopDbContext.Users.ToList();
                var account = users.Any(a => a.UserName == data.UserName && a.Password == data.Password);
                result = account;
            }
            return result;
        }
    }
}
