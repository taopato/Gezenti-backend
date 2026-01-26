using Gezenti.Application.Services.Repositories;
using Gezenti.Core.Utilities.Abstrak;
using Gezenti.Core.Utilities.Concrete;
using Gezenti.Domain.Entities;
using Gezenti.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Gezenti.Persistence.Repositories
{
    public class EfPlaceRepository : IPlaceService
    {
        private readonly GezentiDbContext _context;

        public EfPlaceRepository(GezentiDbContext context)
        {
            _context = context;
        }

        public async Task<IDataResult<List<Place>>> GetAllPlacesAsync()
        {
            var places = await _context.Places
                .Include(p => p.PlaceCategories)
                    .ThenInclude(pc => pc.Category)
                .Include(p => p.UserFavorites)
                .ToListAsync();

            if (places != null && places.Any())
            {
                return new SuccessDataResult<List<Place>>(places, "Yerler başarıyla getirildi.");
            }

            return new ErrorDataResult<List<Place>>(new List<Place>(), "Yer bulunamadı.");
        }

        public async Task<IDataResult<Place>> GetPlaceByIdAsync(int placeId)
        {
            var place = await _context.Places
                .Include(p => p.PlaceCategories)
                    .ThenInclude(pc => pc.Category)
                .Include(p => p.UserFavorites)
                .Include(p => p.UserComments)
                .FirstOrDefaultAsync(p => p.Id == placeId);

            if (place != null)
            {
                return new SuccessDataResult<Place>(place, "Yer başarıyla getirildi.");
            }

            return new ErrorDataResult<Place>(null!, "Yer bulunamadı.");
        }

        public async Task<IDataResult<List<Place>>> GetPlacesByCategoryAsync(int categoryId)
        {
            var places = await _context.Places
                .Include(p => p.PlaceCategories)
                    .ThenInclude(pc => pc.Category)
                .Include(p => p.UserFavorites)
                .Where(p => p.PlaceCategories.Any(pc => pc.CategoryId == categoryId))
                .ToListAsync();

            if (places != null && places.Any())
            {
                return new SuccessDataResult<List<Place>>(places, "Kategoriye ait yerler başarıyla getirildi.");
            }

            return new ErrorDataResult<List<Place>>(new List<Place>(), "Bu kategoriye ait yer bulunamadı.");
        }

        public async Task<IResult> AddPlaceAsync(Place place)
        {
            await _context.Places.AddAsync(place);
            
            if (place.PlaceCategories != null && place.PlaceCategories.Any())
            {
                await _context.PlaceCategories.AddRangeAsync(place.PlaceCategories);
            }

            await _context.SaveChangesAsync();
            return new SuccessResult("Yer başarıyla eklendi.");
        }

        public async Task<IResult> UpdatePlaceAsync(Place place)
        {
            var existingPlace = await _context.Places
                .Include(p => p.PlaceCategories)
                .FirstOrDefaultAsync(p => p.Id == place.Id);

            if (existingPlace == null)
            {
                return new ErrorResult("Güncellenecek yer bulunamadı.");
            }

            existingPlace.PlaceName = place.PlaceName;
            existingPlace.City = place.City;
            existingPlace.Region = place.Region;
            existingPlace.Latitude = place.Latitude;
            existingPlace.Longitude = place.Longitude;
            existingPlace.Description = place.Description;
            existingPlace.UpdatedAt = DateTime.UtcNow;

            if (place.PlaceCategories != null && place.PlaceCategories.Any())
            {
                _context.PlaceCategories.RemoveRange(existingPlace.PlaceCategories);
                await _context.PlaceCategories.AddRangeAsync(place.PlaceCategories);
            }

            _context.Places.Update(existingPlace);
            await _context.SaveChangesAsync();
            return new SuccessResult("Yer başarıyla güncellendi.");
        }

        public async Task<IResult> DeletePlaceAsync(int placeId)
        {
            var place = await _context.Places
                .Include(p => p.PlaceCategories)
                .FirstOrDefaultAsync(p => p.Id == placeId);

            if (place == null)
            {
                return new ErrorResult("Silinecek yer bulunamadı.");
            }

            if (place.PlaceCategories != null && place.PlaceCategories.Any())
            {
                _context.PlaceCategories.RemoveRange(place.PlaceCategories);
            }

            _context.Places.Remove(place);
            await _context.SaveChangesAsync();
            return new SuccessResult("Yer başarıyla silindi.");
        }
    }
}
