using System;

namespace AudioRecord.Api.DTO
{
    
    public class ConfigurationDTO
    {
        public class RabbitMqConfig
        {
            [ConfigurationKeyName("Host")]
            public string HostName { get; set; }
            public ushort Port { get; set; }
            [ConfigurationKeyName("Username")]
            public string UserName { get; set; }
            public string Password { get; set; }
        }
        public static RabbitMqConfig GetRabbitMqConfig(IConfiguration config) 
            => config.GetSection("RabbitMq").Get<RabbitMqConfig>()
                ?? throw new ArgumentNullException("Não foi possível encontrar a configuração do rabbitMq");
        public class FileStorageConfig
        {
            [ConfigurationKeyName("BasePath")]
            public string BasePath { get; set; }
            public DirectoryInfo BasePathDir => new DirectoryInfo(BasePath);
            public string RawAudioPath { get; set; }

            public DirectoryInfo RawAudioPathDir=> new DirectoryInfo(Path.Combine( BasePath , RawAudioPath));
            public string ConvertedAudioPath { get; set; } 
            public DirectoryInfo ConvertedAudioDir => new DirectoryInfo(Path.Combine(BasePath, ConvertedAudioPath));

        }
        public static FileStorageConfig GetFileStorageConfig(IConfiguration config) 
            => config.GetSection("FileStorage").Get<FileStorageConfig>()
                ?? throw new ArgumentNullException("Não foi possível encontrar a configuração de FileStorage");



    }
}
