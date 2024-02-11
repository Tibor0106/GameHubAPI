namespace GameHubAPI
{
    using Microsoft.Extensions.Hosting;
    using System.Net.Http;
    public class HeartBeatSettings
    {
        public TimeSpan Frequency { get; set; }
        public string Target { get; set; }
    }
    public class HeartBeatChecker : BackgroundService
    {
        private readonly ILogger<HeartBeatChecker> Logger;
        private readonly HeartBeatSettings HeartBeatCheckerSettings;
        public HeartBeatChecker(ILogger<HeartBeatChecker> logger, HeartBeatSettings pingSettings)
        {
            Logger = logger;
            HeartBeatCheckerSettings = pingSettings;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(HeartBeatCheckerSettings.Frequency, stoppingToken);

                try
                {
                    HttpClient httpClient = new HttpClient();
                    using HttpResponseMessage response = await httpClient.GetAsync(HeartBeatCheckerSettings.Target);
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex.Message);
                }
            }
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
