using MediatR;
using Orbit.Application.DTOs.Family;


namespace Orbit.Application.Features.Family.Queries
{
    public class GetAllFamilyQuery : IRequest<List<FamilyResponseDto>>
    {
    }
}
