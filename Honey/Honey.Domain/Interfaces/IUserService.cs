using Honey.Domain.Dto.User;
using Honey.Domain.Dto.User.Update;

namespace Honey.Domain.Interfaces
{
    public interface IUserService
    {
        
        Task<UserUpdateResponseDto> Edit(UserUpdateRequestDto data);
        
        Task<UserModelResponseDto> GetByUserName(string name);
        
        Task<UserModelResponseDto> GetById(Guid id);
        
        Task<UserModelResponseDto> Delete(Guid id);
    }
}