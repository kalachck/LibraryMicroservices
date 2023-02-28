using BorrowService.Borrowings.Options;
using BorrowService.Hangfire.Options;
using BorrowService.RabbitMq.Options;

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

            builder.Services.Configure<RabbitOptions>(
                builder.Configuration.GetSection(RabbitOptions.RabbitData));

            return builder;
        }
    }
}
