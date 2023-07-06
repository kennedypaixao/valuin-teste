using System;
using Microsoft.Owin;
using Owin;
using Hangfire;
using Hangfire.SqlServer;
using System.Collections.Generic;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(Edesoft.ERP.Hangfire.App_Start.Startup))]

namespace Edesoft.ERP.Hangfire.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseHangfireAspNet(GetHangfireServers);
            app.UseHangfireDashboard();

            // Let's also create a sample background job
            BackgroundJob.Enqueue(() => PrimeiroJobHangfire());
            RecurringJob.AddOrUpdate("Job recorrente - Edesoft MVC", () => PrimeiroJobHangfireRecorrente(), Cron.Minutely);
            // ...other configuration logic
        }

        private IEnumerable<IDisposable> GetHangfireServers()
        {
            GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(System.Configuration.ConfigurationManager.AppSettings["EdesoftDatabaseHangfire"], new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true,
                    PrepareSchemaIfNecessary = true
                });

            yield return new BackgroundJobServer(new BackgroundJobServerOptions
            {
                WorkerCount = 2,
                Queues = new[] { "recurring", "background" },
                ServerName = "Edesoft.Job.Server",
            });
        }

        public async Task PrimeiroJobHangfire()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Meu Primeiro Job Hangfire - Edesoft MVC.");
            });
        }

        public async Task PrimeiroJobHangfireRecorrente()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Meu Primeiro Job Hangfire RECORRENTE.");
            });
        }
    }
}