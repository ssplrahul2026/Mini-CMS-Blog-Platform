using MiniCMS.ViewModels;

namespace MiniCMS.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        Task<DashboardViewModel> GetDashboardDataAsync();
    }
}