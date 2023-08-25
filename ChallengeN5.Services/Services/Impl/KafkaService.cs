using System;
using System.Collections.Generic;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using ChallengeN5.Repositories.Dto;

namespace ChallengeN5.Services.Services.Impl
{
    public class KafkaService : IKafkaService
    {
        private readonly ProducerConfig _producerConfig;

        public KafkaService(IConfiguration configuration)
        {
            _producerConfig = new ProducerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"]
            };
        }

        public void ProduceOperationMessage(KafkaMessageDto messageDto)
        {
            using var producer = new ProducerBuilder<Null, string>(_producerConfig).Build();

            var message = new Message<Null, string>
            {
                Value = JsonSerializer.Serialize(messageDto)
            };

            producer.Produce("operaciones", message);
        }
    }

}
