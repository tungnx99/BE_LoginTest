using AutoMapper;
using Common.Paganation;
using Data;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Repository;
using Service.Repository.User;
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
        private readonly IRepository<User> dataRepository;

        public UserService(IMapper mapper, ShopDbContext shopDbContext, IRepository<User> dataRepository)
        {
            this._mapper = mapper;
            this.shhopDbContext = shopDbContext;
            this.dataRepository = dataRepository;
        }
        public UserDTO GetUser(string obj)
        {
            using (ShopDbContext shopDbContext = this.shhopDbContext)
            {
                //var users = shopDbContext.Users.ToList();
                //var user = users.FirstOrDefault(t => t.Id == obj);
                var user = dataRepository.Find(obj);

                if (user != null)
                {
                    return _mapper.Map<User, UserDTO>(user);
                }
            }

            return null;
        }

        public Paganation<UserDTO> GetUsers(SerachPaganationDTO<UserDTO> userPaganationDTO)
        {
            if (userPaganationDTO == null)
            {
                return new Paganation<UserDTO>();
            }

            using (ShopDbContext shopDbContext = this.shhopDbContext)
            {
                var matchUsers = shopDbContext.Users
                    .Where(it => it.UserName.Contains(userPaganationDTO.Search.UserName))
                    .OrderBy(it => it.Type)
                    .ThenBy(it => it.UserName)
                    .Take(userPaganationDTO.Take)
                    .Skip(userPaganationDTO.Skip);
                var userDtos = _mapper.Map<List<User>, List<UserDTO>>(matchUsers.ToList());

                var result = _mapper.Map<SerachPaganationDTO<UserDTO>, Paganation<UserDTO>>(userPaganationDTO);
                result.Data = userDtos;

                // todo: Check code below
                var users = shopDbContext.Users.ToList(); // Todo: Very bad!!!
                var data = users.ToList();
                if (userPaganationDTO.Search != null)
                {
                    data = data.Where(t =>
                    t.UserName.Contains(userPaganationDTO.Search.UserName ?? "")
                ).OrderBy(t => t.Type).ThenBy(t => t.UserName).ToList();
                }
                result.Data = _mapper
                    .Map<List<User>, List<UserDTO>>(
                        data
                        .Take(userPaganationDTO.Take)
                        .Skip(userPaganationDTO.Skip)
                        .ToList());
                result.TotalItems = data.Count;
                return result;
            }
        }
    }
}
