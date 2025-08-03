using _net_taskApiSimple.DTOs;
using _net_taskApiSimple.Interfaces;
using _net_taskApiSimple.Models;
using _net_taskApiSimple.Data;
using AutoMapper;

namespace _net_taskApiSimple.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UserService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<UserResponseDto> GetAll()
    {
        var users = _context.Users.ToList();
        return _mapper.Map<List<UserResponseDto>>(users);
    }

    public UserResponseDto Create(CreateUserDto dto)
    {
        var user = new User { Username = dto.Username };
        _context.Users.Add(user);
        _context.SaveChanges();

        return _mapper.Map<UserResponseDto>(user);
    }
}
