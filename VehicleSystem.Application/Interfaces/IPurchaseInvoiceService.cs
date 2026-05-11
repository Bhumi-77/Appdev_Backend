using VehicleSystem.Application.DTOs;

namespace VehicleSystem.Application.Interfaces.Services
{
    public interface IPurchaseInvoiceService
    {
        Task<ApiResponse<List<PurchaseInvoiceResponseDto>>> GetAllAsync();
        Task<ApiResponse<PurchaseInvoiceResponseDto>> GetByIdAsync(int id);
        Task<ApiResponse<PurchaseInvoiceResponseDto>> CreateAsync(PurchaseInvoiceRequestDto dto);
    }
}