using System;
using System.Collections.Generic;
using AutoMapper;
using FoltDelivery.Infrastructure.Authorization;
using FoltDelivery.DTO;
using FoltDelivery.Repository;
using FoltDelivery.Exception;
using FoltDelivery.Model.Enums;
using FoltDelivery.Domain.Aggregates.Customer;

namespace FoltDelivery.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private IJwtUtils _jwtUtils;
        private IMapper _mapper;
        public UserService(IUserRepository userRepository, IJwtUtils jwtUtils, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public List<User> GetAllUsers()
        {
            return new List<User>(_userRepository.GetAll());
        }

        public User GetById(Guid id)
        {
            return _userRepository.Get(id);
        }

        public User GetUsingCredentials(string username, string password)
        {
            return _userRepository.GetUsingCredentials(username, password);
        }

        public AuthenticateResponseDTO Authenticate(AuthenticateRequestDTO model)
        {
            var user = _userRepository.GetByUsername(model.Username);


            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                throw new AppException("Username or password is incorrect");

            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponseDTO(user, jwtToken);
        }

        public User Register(UserDTO userDTO)
        {
            if (_userRepository.GetByUsername(userDTO.Username) != null)
                throw new AppException("Username '" + userDTO.Username + "' is already taken");

            var user = _mapper.Map<UserDTO, User>(userDTO);
            if (user.Role == Role.Admin)
            {
                user.Confirmed = true;
            }
            else
            {
                user.Confirmed = false;
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);

            return _userRepository.Insert(user);
        }


    }
}
