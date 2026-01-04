using ProductWebAPI.Models.Dashboard;

public interface IDashboardService
{
    Task<DashboardDto> GetDashboardAsync();
}