using Lab200.Entities;

namespace Lab200.Interfaces.Services;
public interface IVmUserService
{
  Task<int> CreateVmUserAsync(VmUser user);
  Task<int> UpdateVmUserAsync(VmUser user);
  Task<List<VmUser>> GetAllVmUsersAsync(int? clientId);
  Task<VmUser?> GetVmUserByIdAsync(int vmUserId);
  Task<int> DeleteVmUserAsync(VmUser user);
}