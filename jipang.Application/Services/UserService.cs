using AutoMapper;
using jipang.Application.DTOs.IN;
using jipang.Application.DTOs.OUT;
using jipang.Application.Interfaces;
using jipang.Domain.Entities;
using jipang.Infrastructure.Interfaces;

namespace jipang.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IPasswordHashService _passwordHashService;
        private IMapper _mapper;
        private readonly int saltiness = 70;
        private readonly int nIterations = 10101;

        public UserService(IUserRepo userRepo, IPasswordHashService passwordHashService, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _passwordHashService = passwordHashService;
        }

        public async Task<UserDtoOut> CreateUser(UserDtoIn userDtoIn)
        {
            int salt = Convert.ToInt32(saltiness);
            int iterations = Convert.ToInt32(nIterations);

            var strSalt = _passwordHashService.GenerateSalt(salt);
            var HashedPassword = _passwordHashService.HashPassword(userDtoIn.Password, strSalt, iterations, salt);

            var user = new Users
            {
                Username = userDtoIn.Username,
                FirstName = userDtoIn.FirstName,
                LastName = userDtoIn.LastName,
                Hash = HashedPassword,
                Salt = strSalt,
                RoleId = userDtoIn.RoleId,
            };

            await _userRepo.Add(user);
            return _mapper.Map<UserDtoOut>(user);
        }

        public async Task<UserDtoOut> GetUserById(int id)
        { 
            var user = await _userRepo.GetById(id);
            return _mapper.Map<UserDtoOut>(user);
        }

        public async Task<IEnumerable<UserDtoOut>> GetAllUsers()
        { 
            var users = await _userRepo.GetAll();
            return _mapper.Map<IEnumerable<UserDtoOut>>(users);
        }

        public async Task<bool> UpdateUser(UserDtoIn userDtoIn)
        {
            var existingUser = await _userRepo.GetById(userDtoIn.Id);
            int salt = Convert.ToInt32(saltiness);
            int iterations = Convert.ToInt32(nIterations);

            if (existingUser == null) 
            {
                return false;
            }

            if (userDtoIn.Username != string.Empty)
            {
                existingUser.Username = userDtoIn.Username;
            }

            if (userDtoIn.Password != string.Empty)
            {
                var strSalt = _passwordHashService.GenerateSalt(salt);
                var HashedPassword = _passwordHashService.HashPassword(userDtoIn.Password, strSalt, iterations, salt);
                existingUser.Hash = HashedPassword;
                existingUser.Salt = strSalt;
            }

            if (userDtoIn.FirstName != string.Empty)
            {
                existingUser.FirstName = userDtoIn.FirstName;
            }

            if (userDtoIn.LastName != string.Empty)
            {
                existingUser.LastName = userDtoIn.LastName;
            }

            if (userDtoIn.RoleId != 0)
            {
                existingUser.RoleId = userDtoIn.RoleId;
            }

            _userRepo.Update(existingUser);

            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var existingUser = await _userRepo.GetById(id);

            if (existingUser == null)
            {
                return false;
            }

            _userRepo.Delete(existingUser);
            return true;
        }
    }
}
