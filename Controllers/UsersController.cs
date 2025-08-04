using Microsoft.AspNetCore.Mvc;
using _net_taskApiSimple.DTOs;
using _net_taskApiSimple.Interfaces;

namespace _net_taskApiSimple.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<UserResponseDto>> GetUsers()
    {
        return _service.GetAll();
    }

    [HttpPost]
    public ActionResult<UserResponseDto> CreateUser(CreateUserDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdUser = _service.Create(dto);
        return CreatedAtAction(nameof(GetUsers), new { id = createdUser.Id }, createdUser);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
    
        if (string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Password))
            return BadRequest("Username ve Password zorunludur.");
    
        var token = _service.Login(dto.Username, dto.Password);
        if (token == null) return Unauthorized();
    
        return Ok(new { Token = token });
    }
}
