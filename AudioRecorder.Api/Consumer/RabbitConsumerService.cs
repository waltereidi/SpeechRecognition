using AudioRecorder.Api.Services;
using RabbitMQ.Client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
namespace AudioRecorder.Api.Consumer
{
    public class RabbitConsumerService
    {
        private IConnection _connection;
        private IChannel _channel;
        public RabbitConsumerService()
        {
            var _channel = RabbitMqConnectionSingleton.CreateChannelAsync().Result;
        }


    }
}
