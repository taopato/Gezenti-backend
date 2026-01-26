using Gezenti.Core.Utilities.Abstrak;
using Gezenti.Domain.Entities;

namespace Gezenti.Application.Services.Repositories
{
    public interface IPlaceService 
    {
        Task<IDataResult<List<Place>>> GetAllPlacesAsync();

        Task<IDataResult<Place>> GetPlaceByIdAsync(int placeId);

        Task<IDataResult<List<Place>>> GetPlacesByCategoryAsync(int categoryId);

        Task<IResult> AddPlaceAsync(Place place);

        Task<IResult> UpdatePlaceAsync(Place place);

        Task<IResult> DeletePlaceAsync(int placeId);
    }
}
