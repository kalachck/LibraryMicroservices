using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data.Common;

namespace LibrarySevice.Api.HealthChecks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly string _connectionString;

        public DatabaseHealthCheck(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("LibraryConnection");
        }

        public async  Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync(cancellationToken);
                    var command = connection.CreateCommand();
                    command.CommandText = "select 1";
                    await command.ExecuteNonQueryAsync(cancellationToken);

                }
                catch (DbException ex)
                {
                    return new HealthCheckResult(context.Registration.FailureStatus, ex.Message);
                }
            }

            return HealthCheckResult.Healthy();
        }
    }
}
