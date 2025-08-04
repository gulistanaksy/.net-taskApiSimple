using _net_taskApiSimple.DTOs;
using _net_taskApiSimple.Interfaces;
using _net_taskApiSimple.Models;
using _net_taskApiSimple.Data;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using _net_taskApiSimple.Helpers;

namespace _net_taskApiSimple.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly JwtSettings _jwtSettings;

    public UserService(AppDbContext context, IMapper mapper, IOptions<JwtSettings> jwtSettings)
    {
        _context = context;
        _mapper = mapper;
        _jwtSettings = jwtSettings.Value;
    }

    public List<UserResponseDto> GetAll()
    {
        var users = _context.Users.ToList();
        return _mapper.Map<List<UserResponseDto>>(users);
    }

    public UserResponseDto Create(CreateUserDto dto)
    {
        // Hash işlemi
        var passwordHash = ComputeSha256Hash(dto.Password);

        var user = new User
        {
            Username = dto.Username,
            PasswordHash = passwordHash
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return _mapper.Map<UserResponseDto>(user);
    }

    // Yardımcı metod
    private static string ComputeSha256Hash(string rawData)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }

    public string? Login(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            return null;
        var passwordHash = ComputeSha256Hash(password);
        var user = _context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == passwordHash);
        if (user == null) return null;
    
        // Token üret
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
    
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            }),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
    
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
