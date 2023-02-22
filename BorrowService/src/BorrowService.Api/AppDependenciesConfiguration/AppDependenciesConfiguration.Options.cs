using BorrowService.Borrowings.Options;
using BorrowService.Hangfire.Options;

namespace BorrowService.Api.AppDependenciesConfiguration
{
    public static partial class AppDependenciesConfiguration
    {
        public static WebApplicationBuilder AddOptions(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<CommunicationOptions>(
                builder.Configuration.GetSection(CommunicationOptions.CommunicationUrls));

            builder.Services.Configure<MailOptions>(
                builder.Configuration.GetSection(MailOptions.MailData));

            return builder;
        }
    }
}
