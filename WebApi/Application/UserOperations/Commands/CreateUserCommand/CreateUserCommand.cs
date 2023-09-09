using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.UserOperations.Commands.CreateUserCommand;

public class CreateUserCommand
{
    public CreateUserModel Model { get; set; }
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateUserCommand(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        var user = _context.Users.SingleOrDefault(u => u.Email == Model.Email);
        if (user is not null)
            throw new InvalidOperationException("User already exists.");

        user = _mapper.Map<User>(Model);
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public class CreateUserModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}