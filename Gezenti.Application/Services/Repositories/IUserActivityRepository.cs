using Gezenti.Core.Utilities.Abstrak;
using Gezenti.Domain.Entities;

namespace Gezenti.Application.Services.Repositories
{
    public interface IUserActivityRepository
    {
        // Create
        Task<IResult> AddAsync(UserActivity userActivity);
        Task<IResult> AddRangeAsync(List<UserActivity> userActivities);

        // Read
        Task<IDataResult<UserActivity?>> GetByIdAsync(int id);
        Task<IDataResult<List<UserActivity>>> GetAllAsync();
        Task<IDataResult<List<UserActivity>>> GetByUserIdAsync(string userId);
        Task<IDataResult<List<UserActivity>>> GetByPlaceIdAsync(string placeId);
        Task<IDataResult<List<UserActivity>>> GetByCityAsync(string city);
        IQueryable<UserActivity> Query();

        // Update
        Task<IResult> UpdateAsync(UserActivity userActivity);
        Task<IResult> UpdateRangeAsync(List<UserActivity> userActivities);

        // Delete
        Task<IResult> DeleteAsync(int id);
        Task<IResult> DeleteAsync(UserActivity userActivity);
        Task<IResult> DeleteByUserIdAsync(string userId);
        Task<IResult> DeleteByPlaceIdAsync(string placeId);
        Task<IResult> DeleteRangeAsync(List<UserActivity> userActivities);
    }
}

