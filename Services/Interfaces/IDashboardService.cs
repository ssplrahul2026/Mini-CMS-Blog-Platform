using MiniCMS.ViewModels;

namespace MiniCMS.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDashboardDataAsync();
    }
}