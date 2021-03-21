using AutoMapper;
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
        private List<User> users;
        public UserService(IMapper mapper)
        {
            this._mapper = mapper;
            users = User.GetList();
        }
        public UserDTO GetUser(string obj)
        {
            var user = users.FirstOrDefault(t => t.Id == obj);

            if (user != null)
            {
                return _mapper.Map<User, UserDTO>(user);
            }
            return null;
        }

        public Paganation<UserDTO> GetUsers(SerachPaganationDTO<UserDTO> userPaganationDTO)
        {
            if (userPaganationDTO != null)
            {
                var result = _mapper.Map<SerachPaganationDTO<UserDTO>, Paganation<UserDTO>>(userPaganationDTO);
                var data = users.Where(t =>
                t.UserName.Contains(userPaganationDTO.Search.UserName) &&
                t.Type.Contains(userPaganationDTO.Search.Type)
                    ).OrderBy(t => t.Type).ThenBy(t => t.UserName).ToList();
                result.Data = _mapper.Map<List<User>, List<UserDTO>>(data);
                result.TotalItems = users.Count;
                return result;
            }
            return new Paganation<UserDTO>();
        }

        public bool Login(UserLogin data)
        {
            //if (data == null)
            //    return false;
            var accounts = User.GetList();
            var account = accounts.Any(a => a.UserName == data.UserName && a.Password == data.Password);
            return account;
        }
    }
}
