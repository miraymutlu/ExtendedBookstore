using AutoMapper;
using WebApi.Application.TokenOperations;
using WebApi.Application.TokenOperations.Models;
using WebApi.DBOperations;

namespace WebApi.Application.UserOperations.Commands.CreateTokenCommand;

public class CreateTokenCommand
{
    public CreateTokenModel Model { get; set; }
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public CreateTokenCommand(IBookStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
    {
        _context = dbContext;
        _mapper = mapper;
        _configuration = configuration;
    }

    public Token Handle()
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == Model.Email && u.Password == Model.Password);
        if (user is not null)
        {
            // Create Token
            TokenHandler tokenHandler = new TokenHandler(_configuration);
            Token token = tokenHandler.CreateAccessToken(user);

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
            _context.SaveChanges();
            return token;
        }
        else
            throw new InvalidOperationException("Wrong username and password!");
    }

    public class CreateTokenModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}