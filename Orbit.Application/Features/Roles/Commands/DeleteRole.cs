using Orbit.Application.DTOs;
using MediatR;


namespace Orbit.Application.Features.Roles.Commands;
public record DeleteRoleCommand(int Id) : IRequest<bool>;