using MassTransit.MessageData.Configuration;

namespace WhisperSpeechRecognition.DTO
{

    public class ConfigurationDTO
    {
        private readonly IConfiguration _config;
        public ConfigurationDTO()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                ?? "Production";

            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())

                // appsettings base
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)

                // appsettings por ambiente (Development, Staging, Production…)
                .AddJsonFile(
                    $"appsettings.{environment}.json",
                    optional: true,
                    reloadOnChange: true)

                // launchSettings (uso local / tooling)
                .AddJsonFile(
                    "Properties/launchSettings.json",
                    optional: true,
                    reloadOnChange: true)

                // variáveis de ambiente sempre por último
                .AddEnvironmentVariables()

                .Build();
        }
        public IConfiguration GetCofiguration()
            => _config;

        public class RabbitMq
        {
            [ConfigurationKeyName("Host")]
            public string HostName { get; set; }
            public ushort Port { get; set; }
            [ConfigurationKeyName("Username")]
            public string UserName { get; set; }
            public string Password { get; set; }
        }
        public RabbitMq RabbitMqConfig => _config.GetSection("RabbitMq").Get<RabbitMq>();

    }
}
