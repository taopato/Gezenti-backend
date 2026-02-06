using Gezenti.Application.Services.Repositories;
using Gezenti.Core.Utilities.Abstrak;
using Gezenti.Core.Utilities.Concrete;
using Gezenti.Domain.Entities;
using Gezenti.Persistence.Contexts.Mongo;
using Microsoft.EntityFrameworkCore;

namespace Gezenti.Persistence.Repositories
{
    public class MongoUserActivityRepository : IUserActivityRepository
    {
        private readonly GezentiMongoDbContext _context;

        public MongoUserActivityRepository(GezentiMongoDbContext context)
        {
            _context = context;
        }

        public async Task<IResult> AddAsync(UserActivity userActivity)
        {
            try
            {
                if (userActivity == null)
                    return new ErrorResult("Kullanıcı aktivitesi boş olamaz.");

                // MongoDB'de ObjectId formatında eski veriler olabilir, bu yüzden maxId sorgusunu güvenli hale getiriyoruz
                int maxId = 0;
                try
                {
                    var maxIdResult = await _context.User_logs
                        .OrderByDescending(x => x.Id)
                        .Select(x => x.Id)
                        .FirstOrDefaultAsync();
                    maxId = maxIdResult;
                }
                catch
                {
                    // ObjectId formatındaki eski veriler varsa, maxId'yi 0 olarak bırak
                    maxId = 0;
                }

                userActivity.Id = maxId > 0 ? maxId + 1 : 1;
                userActivity.CreatedAt = DateTime.UtcNow;
                await _context.User_logs.AddAsync(userActivity);
                await _context.SaveChangesAsync();

                return new SuccessResult("Kullanıcı aktivitesi başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Kullanıcı aktivitesi eklenirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<IResult> AddRangeAsync(List<UserActivity> userActivities)
        {
            try
            {
                if (userActivities == null || !userActivities.Any())
                    return new ErrorResult("Kullanıcı aktiviteleri boş olamaz.");

                // MongoDB'de ObjectId formatında eski veriler olabilir, bu yüzden maxId sorgusunu güvenli hale getiriyoruz
                int maxId = 0;
                try
                {
                    var maxIdResult = await _context.User_logs
                        .OrderByDescending(x => x.Id)
                        .Select(x => x.Id)
                        .FirstOrDefaultAsync();
                    maxId = maxIdResult;
                }
                catch
                {
                    // ObjectId formatındaki eski veriler varsa, maxId'yi 0 olarak bırak
                    maxId = 0;
                }

                int currentId = maxId > 0 ? maxId : 0;

                foreach (var activity in userActivities)
                {
                    currentId++;
                    activity.Id = currentId;
                    activity.CreatedAt = DateTime.UtcNow;
                }

                await _context.User_logs.AddRangeAsync(userActivities);
                await _context.SaveChangesAsync();

                return new SuccessResult($"{userActivities.Count} adet kullanıcı aktivitesi başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Kullanıcı aktiviteleri eklenirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<IDataResult<UserActivity?>> GetByIdAsync(int id)
        {
            try
            {
                var activity = await _context.User_logs
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (activity != null)
                    return new SuccessDataResult<UserActivity?>(activity, "Kullanıcı aktivitesi başarıyla getirildi.");

                return new ErrorDataResult<UserActivity?>(null, "Kullanıcı aktivitesi bulunamadı.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<UserActivity?>(null, $"Kullanıcı aktivitesi getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<IDataResult<List<UserActivity>>> GetAllAsync()
        {
            try
            {
                var activities = await _context.User_logs
                    .OrderByDescending(x => x.CreatedAt)
                    .ToListAsync();

                return new SuccessDataResult<List<UserActivity>>(activities, $"{activities.Count} adet kullanıcı aktivitesi getirildi.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<UserActivity>>(new List<UserActivity>(), $"Kullanıcı aktiviteleri getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<IDataResult<List<UserActivity>>> GetByUserIdAsync(string userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                    return new ErrorDataResult<List<UserActivity>>(new List<UserActivity>(), "Kullanıcı ID boş olamaz.");

                var activities = await _context.User_logs
                    .Where(x => x.UserId == userId)
                    .OrderByDescending(x => x.CreatedAt)
                    .ToListAsync();

                return new SuccessDataResult<List<UserActivity>>(activities, $"{activities.Count} adet kullanıcı aktivitesi getirildi.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<UserActivity>>(new List<UserActivity>(), $"Kullanıcı aktiviteleri getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<IDataResult<List<UserActivity>>> GetByPlaceIdAsync(string placeId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(placeId))
                    return new ErrorDataResult<List<UserActivity>>(new List<UserActivity>(), "Yer ID boş olamaz.");

                var activities = await _context.User_logs
                    .Where(x => x.PlaceId == placeId)
                    .OrderByDescending(x => x.CreatedAt)
                    .ToListAsync();

                return new SuccessDataResult<List<UserActivity>>(activities, $"{activities.Count} adet kullanıcı aktivitesi getirildi.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<UserActivity>>(new List<UserActivity>(), $"Kullanıcı aktiviteleri getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<IDataResult<List<UserActivity>>> GetByCityAsync(string city)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(city))
                    return new ErrorDataResult<List<UserActivity>>(new List<UserActivity>(), "Şehir adı boş olamaz.");

                var activities = await _context.User_logs
                    .Where(x => x.City == city)
                    .OrderByDescending(x => x.CreatedAt)
                    .ToListAsync();

                return new SuccessDataResult<List<UserActivity>>(activities, $"{activities.Count} adet kullanıcı aktivitesi getirildi.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<UserActivity>>(new List<UserActivity>(), $"Kullanıcı aktiviteleri getirilirken hata oluştu: {ex.Message}");
            }
        }

        public IQueryable<UserActivity> Query()
        {
            return _context.User_logs.AsQueryable();
        }

        public async Task<IResult> UpdateAsync(UserActivity userActivity)
        {
            try
            {
                if (userActivity == null)
                    return new ErrorResult("Kullanıcı aktivitesi boş olamaz.");

                var existingActivity = await _context.User_logs
                    .FirstOrDefaultAsync(x => x.Id == userActivity.Id);

                if (existingActivity == null)
                    return new ErrorResult("Güncellenecek kullanıcı aktivitesi bulunamadı.");

                userActivity.UpdatedAt = DateTime.UtcNow;
                _context.User_logs.Update(userActivity);
                await _context.SaveChangesAsync();

                return new SuccessResult("Kullanıcı aktivitesi başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Kullanıcı aktivitesi güncellenirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<IResult> UpdateRangeAsync(List<UserActivity> userActivities)
        {
            try
            {
                if (userActivities == null || !userActivities.Any())
                    return new ErrorResult("Kullanıcı aktiviteleri boş olamaz.");

                foreach (var activity in userActivities)
                {
                    activity.UpdatedAt = DateTime.UtcNow;
                }

                _context.User_logs.UpdateRange(userActivities);
                await _context.SaveChangesAsync();

                return new SuccessResult($"{userActivities.Count} adet kullanıcı aktivitesi başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Kullanıcı aktiviteleri güncellenirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            try
            {
                var activity = await _context.User_logs
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (activity == null)
                    return new ErrorResult("Silinecek kullanıcı aktivitesi bulunamadı.");

                _context.User_logs.Remove(activity);
                await _context.SaveChangesAsync();

                return new SuccessResult("Kullanıcı aktivitesi başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Kullanıcı aktivitesi silinirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<IResult> DeleteAsync(UserActivity userActivity)
        {
            try
            {
                if (userActivity == null)
                    return new ErrorResult("Kullanıcı aktivitesi boş olamaz.");

                _context.User_logs.Remove(userActivity);
                await _context.SaveChangesAsync();

                return new SuccessResult("Kullanıcı aktivitesi başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Kullanıcı aktivitesi silinirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<IResult> DeleteByUserIdAsync(string userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                    return new ErrorResult("Kullanıcı ID boş olamaz.");

                var activities = await _context.User_logs
                    .Where(x => x.UserId == userId)
                    .ToListAsync();

                if (!activities.Any())
                    return new ErrorResult("Silinecek kullanıcı aktivitesi bulunamadı.");

                _context.User_logs.RemoveRange(activities);
                await _context.SaveChangesAsync();

                return new SuccessResult($"{activities.Count} adet kullanıcı aktivitesi başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Kullanıcı aktiviteleri silinirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<IResult> DeleteByPlaceIdAsync(string placeId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(placeId))
                    return new ErrorResult("Yer ID boş olamaz.");

                var activities = await _context.User_logs
                    .Where(x => x.PlaceId == placeId)
                    .ToListAsync();

                if (!activities.Any())
                    return new ErrorResult("Silinecek kullanıcı aktivitesi bulunamadı.");

                _context.User_logs.RemoveRange(activities);
                await _context.SaveChangesAsync();

                return new SuccessResult($"{activities.Count} adet kullanıcı aktivitesi başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Kullanıcı aktiviteleri silinirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<IResult> DeleteRangeAsync(List<UserActivity> userActivities)
        {
            try
            {
                if (userActivities == null || !userActivities.Any())
                    return new ErrorResult("Kullanıcı aktiviteleri boş olamaz.");

                _context.User_logs.RemoveRange(userActivities);
                await _context.SaveChangesAsync();

                return new SuccessResult($"{userActivities.Count} adet kullanıcı aktivitesi başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Kullanıcı aktiviteleri silinirken hata oluştu: {ex.Message}");
            }
        }
    }
}

