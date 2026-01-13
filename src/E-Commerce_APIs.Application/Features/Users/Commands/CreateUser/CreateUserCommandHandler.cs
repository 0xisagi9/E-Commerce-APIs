using AutoMapper;
using E_Commerce_APIs.Shared.Interfaces;
using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Infrastructure.Persistence.UnitOfWork;
using MediatR;
using System.Runtime;

namespace E_Commerce_APIs.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var User = _mapper.Map<User>(request);
        CreateUserValidator validator = new();
        var result = await validator.ValidateAsync(request);
        if (result.Errors.Any())
            throw new Exception("Error in User Creation");
        await _unitOfWork.User.AddAsync(User);
        await _unitOfWork.CompleteAsync();
        return User.Id;
    }
}
