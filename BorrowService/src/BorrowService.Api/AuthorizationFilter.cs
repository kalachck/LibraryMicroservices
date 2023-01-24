using Hangfire.Dashboard;

namespace BorrowService.Api
{
    public class AuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context) => true;
    }
}
