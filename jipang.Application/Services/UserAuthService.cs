using AutoMapper;
using jipang.Application.DTOs.IN;
using jipang.Application.DTOs.OUT;
using jipang.Application.Interfaces;
using jipang.Domain.Entities;
using jipang.Infrastructure.Data;
using jipang.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jipang.Application.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IPasswordHashService _passwordHashService;
        private readonly IJwtService _jwtService;
        private readonly IUserAuthRepo _userAuthRepo;
        private readonly IMapper _mapper;
        private readonly int saltiness = 70;
        private readonly int nIterations = 10101;

        public UserAuthService(IPasswordHashService passwordHashService, IJwtService jwtService, IUserAuthRepo userAuthRepo, IMapper mapper)
        {
            _passwordHashService = passwordHashService;
            _userAuthRepo = userAuthRepo;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<UserDtoOut> LoginUser(string username, string password)
        {
            try
            {
                int salt = Convert.ToInt32(saltiness);
                int iterations = Convert.ToInt32(nIterations);
                string Token = "";

                if (username != null && password != null)
                {
                    var user = await _userAuthRepo.GetByUsername(username);

                    if (user == null)
                    {
                        return new UserDtoOut
                        {
                            Id = 0,
                            FirstName = "",
                            LastName = "",
                            Username = "",
                            RoleId = 0,
                            Token = "",
                            Message = "User not found."
                        };
                    }

                    var HashedPassword = _passwordHashService.HashPassword(password, user.Salt, iterations, salt);

                    if (user.Hash != HashedPassword)
                    {
                        return new UserDtoOut
                        {
                            Id = 0,
                            FirstName = "",
                            LastName = "",
                            Username = "",
                            RoleId = 0,
                            Token = "",
                            Message = "Incorrect username/password."
                        };
                    }

                    var userDto = _mapper.Map<UserDtoIn>(user);
                    Token = _jwtService.GenerateToken(userDto);
                    return new UserDtoOut
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Username = user.Username,
                        RoleId = user.RoleId,
                        Token = Token,
                        Message = "Login Successful"
                    };
                }
                else
                {
                    return new UserDtoOut
                    {
                        Id = 0,
                        FirstName = "",
                        LastName = "",
                        Username = "",
                        RoleId = 0,
                        Token = "",
                        Message = "Incorrect username/password."
                    };
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UserDtoOut> LogoutUser(string username)
        {
            try
            {
                if (username != null)
                {
                    var user = await _userAuthRepo.GetByUsername(username);

                    if (user == null)
                    {
                        return new UserDtoOut();
                    }

                    return new UserDtoOut
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Username = user.Username,
                        RoleId = user.RoleId,
                        Token = "",
                        Message = "Logout Successful"
                    };
                }
                else
                {
                    return new UserDtoOut();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
