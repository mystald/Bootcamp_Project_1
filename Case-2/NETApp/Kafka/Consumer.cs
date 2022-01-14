using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace NETApp.Kafka
{
    public class Consumer
    {
        private ConsumerConfig config;
        public IConsumer<string, string> consumer;

        public Consumer()
        {
            config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "twittor",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                ClientId = Dns.GetHostName(),
            };

            consumer = new ConsumerBuilder<string, string>(config).Build();
        }
    }
}