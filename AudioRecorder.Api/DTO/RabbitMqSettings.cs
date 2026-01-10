namespace AudioRecorder.Api.DTO
{
    public class RabbitMqSettingsDTO
    {
        public string Host { get; set; } = "";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Queue { get; set; } = "";
    }
}
