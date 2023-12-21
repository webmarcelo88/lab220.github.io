using Lab200.Entities;
using Lab200.Helpers;
using Lab200.Interfaces.Repositories;
using Lab200.Interfaces.Services;

namespace Lab200.Services;
public class VmUserService : IVmUserService
{
    private readonly IVmUserRepository _vmUserRepository;

    public VmUserService(IVmUserRepository vmUserRepository)
    {
        _vmUserRepository = vmUserRepository;
    }

    public async Task<int> CreateVmUserAsync(VmUser user)
    {
        var vmUserExists = await _vmUserRepository.DoesUserExist(user.Login);
        if (vmUserExists)
            return 0;

        user.Pass = Encoders.Encode64(user.Pass);

        var addUser = await _vmUserRepository.CreateVmUserAsync(user);
        return addUser;
    }

    public async Task<int> DeleteVmUserAsync(VmUser vmUser)
    {
        vmUser.IsDeleted = true;
        vmUser.IsActive = false;

        var isDeleted = await _vmUserRepository.DeleteVmUserAsync(vmUser);
        return isDeleted;
    }

    public async Task<List<VmUser>> GetAllVmUsersAsync(int? clientId)
    {
        var vmUsers = await _vmUserRepository.GetAllVmUsersAsync(clientId);
        return vmUsers;
    }

    public async Task<VmUser?> GetVmUserByIdAsync(int vmUserId)
    {
        var vmUser = await _vmUserRepository.GetVmUserByIdAsync(vmUserId);
        return vmUser;
    }

    public async Task<int> UpdateVmUserAsync(VmUser user)
    {
        var dbVmUser = await GetVmUserByIdAsync(user.Id);
        if (dbVmUser != null)
        {
            if (dbVmUser.Pass != Encoders.Encode64(user.Pass))
                user.Pass = Encoders.Encode64(user.Pass);
            else
                user.Pass = dbVmUser.Pass;
        }

        var updateVmUser = await _vmUserRepository.UpdateVmUserAsync(user);
        return updateVmUser;
    }
}