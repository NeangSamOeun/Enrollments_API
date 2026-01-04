using ProductWebAPI.Models.Dashboard;

public interface IDashboardRepository
{
    Task<DashboardDto> GetDashboardAsync();
}