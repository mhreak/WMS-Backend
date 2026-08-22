using WMS.Domain.Entities.Users;

namespace WMS.Application.Common.Profile.Interfaces;

public interface IUserProfileRepository
{
    Task<User?> GetUserWithProfileAsync(Guid userId, CancellationToken cancellationToken = default);
    Task UpdateAvatarAsync(Guid userId, Guid fileAssetId, CancellationToken cancellationToken = default);
    Task UpdateCoverAsync(Guid userId, Guid fileAssetId, CancellationToken cancellationToken = default);
}
