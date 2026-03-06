using ECommerce.Application.DTOs.Common;
using ECommerce.Application.DTOs.MasterData;

namespace ECommerce.Application.Services.Interfaces
{
    public interface IMasterDataService
    {
        Task<ApiResponse<IEnumerable<MasterColorDto>>> GetAllColorsAsync();
        Task<ApiResponse<MasterColorDto>> GetColorByIdAsync(int id);
        Task<ApiResponse<MasterColorDto>> CreateColorAsync(CreateMasterColorDto dto);
        Task<ApiResponse<MasterColorDto>> UpdateColorAsync(int id, UpdateMasterColorDto dto);
        Task<ApiResponse<bool>> DeleteColorAsync(int id);

        Task<ApiResponse<IEnumerable<MasterSizeDto>>> GetAllSizesAsync();
        Task<ApiResponse<MasterSizeDto>> GetSizeByIdAsync(int id);
        Task<ApiResponse<MasterSizeDto>> CreateSizeAsync(CreateMasterSizeDto dto);
        Task<ApiResponse<MasterSizeDto>> UpdateSizeAsync(int id, UpdateMasterSizeDto dto);
        Task<ApiResponse<bool>> DeleteSizeAsync(int id);
    }
}
