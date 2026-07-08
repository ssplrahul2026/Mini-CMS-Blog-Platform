using MiniCMS.Repositories.Interfaces;
using MiniCMS.Services.Interfaces;
using MiniCMS.ViewModels;

namespace MiniCMS.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync()
        {
            return await _dashboardRepository.GetDashboardDataAsync();
        }
    }
}