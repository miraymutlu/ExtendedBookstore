using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Application.TokenOperations.Models;
using WebApi.Application.UserOperations.Commands.CreateTokenCommand;
using WebApi.Application.UserOperations.Commands.CreateUserCommand;
using WebApi.DBOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class UserController : ControllerBase
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    public UserController(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateUserCommand.CreateUserModel newUser)
    {
        CreateUserCommand command = new CreateUserCommand(_context, _mapper);
        command.Model = newUser;
        command.Handle();

        return Ok();
    }

    [HttpPost]
    public ActionResult<Token> CreateToken([FromBody] login)
    {
        CreateTokenCommand command = new CreateTokenCommand(_context, _mapper);,
        command.Model = login;
        var token = command.Handle();
        return token;
    }
}