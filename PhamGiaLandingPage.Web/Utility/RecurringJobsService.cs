using Hangfire;
using Hangfire.Annotations;
using Hangfire.Server;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PhamGiaLandingPage.Web.Services.Extra.Repository;
using PhamGiaLandingPage.Web.Services.FlashSale.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.Utility
{
    internal class RecurringJobsService : BackgroundService
    {
        private readonly IBackgroundJobClient _backgroundJobs;
        private readonly IRecurringJobManager _recurringJobs;
        private readonly ILogger<RecurringJobScheduler> _logger;
        private readonly IFlashSaleRepository _flashSale;
        private readonly IExtraRepository _extra;

        public RecurringJobsService(
            [NotNull] IBackgroundJobClient backgroundJobs,
            [NotNull] IRecurringJobManager recurringJobs,
            [NotNull] ILogger<RecurringJobScheduler> logger,
            IFlashSaleRepository flashSale,
            IExtraRepository extra)
        {
            _backgroundJobs = backgroundJobs ?? throw new ArgumentNullException(nameof(backgroundJobs));
            _recurringJobs = recurringJobs ?? throw new ArgumentNullException(nameof(recurringJobs));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _flashSale = flashSale;
            _extra = extra;
        }

        //public IRecurringJobManager RecurringJobs => _recurringJobs;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                //_backgroundJobs.Enqueue<Services>(x => x.LongRunning(JobCancellationToken.Null));
                //_backgroundJobs.Enqueue(() => )
                //_recurringJobs.RemoveIfExists("minutely");
                _recurringJobs.AddOrUpdate("minutely", () => _flashSale.AutoUpdateFlashSale(), Cron.Minutely);
                _recurringJobs.AddOrUpdate("backup", () => _extra.AutoBackupDatabase(), Cron.Weekly);


            }
            catch (Exception e)
            {
                _logger.LogError("An exception occurred while creating recurring jobs.", e);
            }

            return Task.CompletedTask;
        }
    }
}
