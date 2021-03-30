using AutoMapper;
using Common.Paganation;
using Data;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.EntityFramework;
using System.Collections.Generic;
using System.Linq;
namespace Service.Users
{
    public class UserService : IUserService
    {
        private IMapper _mapper;
        private readonly IRepository<User> dataRepository;

        public UserService(IMapper mapper, IRepository<User> dataRepository)
        {
            _mapper = mapper;
            this.dataRepository = dataRepository;
        }

        public Paganation<UserDTO> GetUsers(SerachPaganationDTO<UserDTO> paganation)
        {
            if (paganation == null)
            {
                return new Paganation<UserDTO>();
            }

            //using (ShopDbContext shopDbContext = this.shhopDbContext)
            //{

            var result = _mapper.Map<SerachPaganationDTO<UserDTO>, Paganation<UserDTO>>(paganation);

            var matchUsers = dataRepository.Queryable()
                .Where(it => paganation.Search == null || it.UserName.Contains(paganation.Search.UserName))
                .OrderBy(it => it.Role)
                .ThenBy(it => it.UserName);

            var userDTOs = _mapper.Map<List<User>, List<UserDTO>>(
                matchUsers
                .Take(paganation.Take)
                .Skip(paganation.Skip)
                .ToList()
            );

            result.InputData(totalItems: matchUsers.Count(),data: userDTOs);

            return result;
        }
        //}
    }
}
