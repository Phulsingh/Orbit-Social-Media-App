using Orbit.Application.DTOs.Family;


namespace Orbit.Application.Interfaces
{
    public interface IFamilyService
    {
        Task<FamilyResponseDto> CreateFamilyAsync(int userId, CreateFamilyDto dto);
        Task<FamilyResponseDto> GetFamilyAsync(int familyId, int userId);
        Task<List<FamilyMemberDto>> GetMembersAsync(int familyId);
        Task<string> GenerateInviteCodeAsync(int familyId, int adminUserId);
        Task<FamilyResponseDto> JoinFamilyAsync(int userId, JoinFamilyDto dto);
        Task<bool> ApproveMemberAsync(int familyId, int memberId, int adminUserId);
        Task<bool> RemoveMemberAsync(int familyId, int memberId, int adminUserId);
        Task<List<FamilyResponseDto>> GetAllFamilyAsync();
    }
}
