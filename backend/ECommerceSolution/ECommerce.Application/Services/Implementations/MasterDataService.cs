using AutoMapper;
using ECommerce.Application.DTOs.Common;
using ECommerce.Application.DTOs.MasterData;
using ECommerce.Application.Services.Interfaces;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;

namespace ECommerce.Application.Services.Implementations
{
    public class MasterDataService : IMasterDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MasterDataService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<MasterColorDto>>> GetAllColorsAsync()
        {
            try
            {
                var colors = await _unitOfWork.MasterColors.GetAllAsync();
                
                // Sort the colors by SortOrder before returning
                var sortedColors = colors.OrderBy(c => c.SortOrder);
                var colorDtos = _mapper.Map<IEnumerable<MasterColorDto>>(sortedColors);

                return ApiResponse<IEnumerable<MasterColorDto>>.SuccessResponse(colorDtos);
            }
            catch (Exception)
            {
                return ApiResponse<IEnumerable<MasterColorDto>>.ErrorResponse("An error occurred while retrieving colors");
            }
        }

        public async Task<ApiResponse<IEnumerable<MasterSizeDto>>> GetAllSizesAsync()
        {
            try
            {
                var sizes = await _unitOfWork.MasterSizes.GetAllAsync();
                
                // Sort the sizes by SortOrder before returning
                var sortedSizes = sizes.OrderBy(s => s.SortOrder);
                var sizeDtos = _mapper.Map<IEnumerable<MasterSizeDto>>(sortedSizes);

                return ApiResponse<IEnumerable<MasterSizeDto>>.SuccessResponse(sizeDtos);
            }
            catch (Exception)
            {
                return ApiResponse<IEnumerable<MasterSizeDto>>.ErrorResponse("An error occurred while retrieving sizes");
            }
        }

        public async Task<ApiResponse<MasterColorDto>> GetColorByIdAsync(int id)
        {
            var color = await _unitOfWork.MasterColors.GetByIdAsync(id);
            if (color == null) return ApiResponse<MasterColorDto>.ErrorResponse("Color not found");
            return ApiResponse<MasterColorDto>.SuccessResponse(_mapper.Map<MasterColorDto>(color));
        }

        public async Task<ApiResponse<MasterColorDto>> CreateColorAsync(CreateMasterColorDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name)) return ApiResponse<MasterColorDto>.ErrorResponse("Color name is required");
            if (await _unitOfWork.MasterColors.AnyAsync(c => c.Name.ToLower() == dto.Name.ToLower()))
                return ApiResponse<MasterColorDto>.ErrorResponse("Color name already exists");

            var color = _mapper.Map<MasterColor>(dto);
            await _unitOfWork.MasterColors.AddAsync(color);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<MasterColorDto>.SuccessResponse(_mapper.Map<MasterColorDto>(color), "Color created successfully");
        }

        public async Task<ApiResponse<MasterColorDto>> UpdateColorAsync(int id, UpdateMasterColorDto dto)
        {
            var color = await _unitOfWork.MasterColors.GetByIdAsync(id);
            if (color == null) return ApiResponse<MasterColorDto>.ErrorResponse("Color not found");

            if (string.IsNullOrWhiteSpace(dto.Name)) return ApiResponse<MasterColorDto>.ErrorResponse("Color name is required");
            if (color.Name.ToLower() != dto.Name.ToLower() && await _unitOfWork.MasterColors.AnyAsync(c => c.Name.ToLower() == dto.Name.ToLower()))
                return ApiResponse<MasterColorDto>.ErrorResponse("Color name already exists");

            _mapper.Map(dto, color);
            _unitOfWork.MasterColors.Update(color);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<MasterColorDto>.SuccessResponse(_mapper.Map<MasterColorDto>(color), "Color updated successfully");
        }

        public async Task<ApiResponse<bool>> DeleteColorAsync(int id)
        {
            var color = await _unitOfWork.MasterColors.GetByIdAsync(id);
            if (color == null) return ApiResponse<bool>.ErrorResponse("Color not found");

            _unitOfWork.MasterColors.Remove(color);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(true, "Color deleted successfully");
        }

        public async Task<ApiResponse<MasterSizeDto>> GetSizeByIdAsync(int id)
        {
            var size = await _unitOfWork.MasterSizes.GetByIdAsync(id);
            if (size == null) return ApiResponse<MasterSizeDto>.ErrorResponse("Size not found");
            return ApiResponse<MasterSizeDto>.SuccessResponse(_mapper.Map<MasterSizeDto>(size));
        }

        public async Task<ApiResponse<MasterSizeDto>> CreateSizeAsync(CreateMasterSizeDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name)) return ApiResponse<MasterSizeDto>.ErrorResponse("Size name is required");
            if (await _unitOfWork.MasterSizes.AnyAsync(s => s.Name.ToLower() == dto.Name.ToLower()))
                return ApiResponse<MasterSizeDto>.ErrorResponse("Size name already exists");

            var size = _mapper.Map<MasterSize>(dto);
            await _unitOfWork.MasterSizes.AddAsync(size);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<MasterSizeDto>.SuccessResponse(_mapper.Map<MasterSizeDto>(size), "Size created successfully");
        }

        public async Task<ApiResponse<MasterSizeDto>> UpdateSizeAsync(int id, UpdateMasterSizeDto dto)
        {
            var size = await _unitOfWork.MasterSizes.GetByIdAsync(id);
            if (size == null) return ApiResponse<MasterSizeDto>.ErrorResponse("Size not found");

            if (string.IsNullOrWhiteSpace(dto.Name)) return ApiResponse<MasterSizeDto>.ErrorResponse("Size name is required");
            if (size.Name.ToLower() != dto.Name.ToLower() && await _unitOfWork.MasterSizes.AnyAsync(s => s.Name.ToLower() == dto.Name.ToLower()))
                return ApiResponse<MasterSizeDto>.ErrorResponse("Size name already exists");

            _mapper.Map(dto, size);
            _unitOfWork.MasterSizes.Update(size);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<MasterSizeDto>.SuccessResponse(_mapper.Map<MasterSizeDto>(size), "Size updated successfully");
        }

        public async Task<ApiResponse<bool>> DeleteSizeAsync(int id)
        {
            var size = await _unitOfWork.MasterSizes.GetByIdAsync(id);
            if (size == null) return ApiResponse<bool>.ErrorResponse("Size not found");

            _unitOfWork.MasterSizes.Remove(size);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(true, "Size deleted successfully");
        }
    }
}
