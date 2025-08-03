using _net_taskApiSimple.DTOs;

namespace _net_taskApiSimple.Interfaces;

public interface IUserService
{
    List<UserResponseDto> GetAll();
    UserResponseDto Create(CreateUserDto dto);
}
