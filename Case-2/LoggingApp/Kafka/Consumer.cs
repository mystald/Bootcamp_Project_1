using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace LoggingApp.Kafka
{
    public class Consumer
    {
        public IConsumer<string, string> consumer;

        public Consumer()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "Project2",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            consumer = new ConsumerBuilder<string, string>(config).Build();
        }
    }
}