using WebApi.Application.TokenOperations;
using WebApi.Application.TokenOperations.Models;
using WebApi.DBOperations;

namespace WebApi.Application.UserOperations.Commands.RefreshTokenCommand;

public class RefreshTokenCommand
{
    public string RefreshToken { get; set; }
    private readonly IBookStoreDbContext _context;
    private readonly IConfiguration _configuration;

    public RefreshTokenCommand(IBookStoreDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public Token Handle()
    {
        var user = _context.Users.FirstOrDefault(u => u.RefreshToken == RefreshToken && u.RefreshTokenExpireDate > DateTime.Now);
        if (user is not null)
        {
            // Create Token
            TokenHandler tokenHandler = new(_configuration);
            Token token = tokenHandler.CreateAccessToken(user);

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
            _context.SaveChanges();
            return token;
        }
        else
            throw new InvalidOperationException("No valid refresh token was found!");
    }
}