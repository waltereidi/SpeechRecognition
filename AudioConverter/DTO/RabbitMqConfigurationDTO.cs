namespace AudioConverter.DTO
{
    
    public class ConfigurationDTO
    {
        private readonly IConfiguration _config;
        public ConfigurationDTO() 
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(
                    path: "Properties/launchSettings.json",
                    optional: false,
                    reloadOnChange: true)
                .Build();
        }

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
