using AutoMapper;
using Honey.DB.Repository.User;
using Honey.Domain.Dto.User;
using Honey.Domain.Dto.User.Update;
using Honey.Domain.Interfaces;

namespace Honey.BL.Authentication.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService
        (
            IUserRepository userRepository,
            IMapper mapper
        )
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserUpdateResponseDto> Edit(UserUpdateRequestDto data)
        {
            var result = new UserUpdateResponseDto();
            var user = _userRepository.GetOne(u => u.UserName == data.UserName);
            if (user == null)
            {
                //result.ErrorType = ErrorType.NotFound;
                return result;
            }
            
            var userWithNewUserName = _userRepository.GetOne(u => u.UserName == data.NewUserName);
            if (userWithNewUserName != null && data.UserName != data.NewUserName || data.Age is <= 0 or > 120)
            {
                //result.ErrorType = ErrorType.BadRequest;
                return result;
            }

            user.UserName = data.NewUserName;
            user.Firstname = data.Firstname;
            user.Name = data.Name;
            user.Lastname = data.Lastname;
            user.Age = data.Age;
            user.DateUpdated = DateTime.Now;

            result = _mapper.Map<UserUpdateResponseDto>(await _userRepository.Update(user));
            return result;
        }

        public async Task<UserModelResponseDto> GetByUserName(string name)
        {
            var result = new UserModelResponseDto();
            var user = _userRepository.GetOne(u => u.UserName == name);
            if (user == null)
            {
                //result.ErrorType = ErrorType.NotFound;
                return result;
            }

            result = _mapper.Map<UserModelResponseDto>(user);
            return result;
        }
        
        public async Task<UserModelResponseDto> GetById(Guid id)
        {
            var result = new UserModelResponseDto();
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                //result.ErrorType = ErrorType.NotFound;
                return result;
            }

            result = _mapper.Map<UserModelResponseDto>(user);
            return result;
        }

        public async Task<UserModelResponseDto> Delete(Guid id)
        {
            var result = new UserModelResponseDto();
            var user = await _userRepository.Delete(id);
            if (user == null)
            {
                //result.ErrorType = ErrorType.NotFound;
                return result;
            }

            result = _mapper.Map<UserModelResponseDto>(user);
            return result;
        }
    }
}