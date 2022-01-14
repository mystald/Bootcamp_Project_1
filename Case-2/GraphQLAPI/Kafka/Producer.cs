using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace GraphQLAPI.Kafka
{
    public class Producer
    {
        private ProducerConfig _config;
        private IProducer<string, string> _producer;
        private IAdminClient _adminClient;

        public Producer()
        {
            _config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                ClientId = Dns.GetHostName(),

            };

            _producer = new ProducerBuilder<string, string>(_config).Build();
            _adminClient = new AdminClientBuilder(_config).Build();
        }

        public bool SendMessage(string topic, string key, string message)
        {
            var succeed = false;

            _producer.Produce(topic, new Message<string, string>
            {
                Key = $"{key}-{System.DateTime.Now}",
                Value = message
            }, (deliveryReport) =>
                {
                    if (deliveryReport.Error.Code != ErrorCode.NoError)
                    {
                        throw new Exception($"Failed to deliver message: {deliveryReport.Error.Reason}");
                    }

                    else
                    {
                        Console.WriteLine($"Produced message to: {deliveryReport.TopicPartitionOffset}");
                        succeed = true;
                    }
                });

            return succeed;
        }

        public async Task CreateTopics(string topic)
        {
            using (var _adminClient = new AdminClientBuilder(_config).Build())
            {
                try
                {
                    await _adminClient.CreateTopicsAsync(
                        new List<TopicSpecification>
                        {
                            new TopicSpecification
                            {
                                Name = topic,
                                NumPartitions = 1,
                                ReplicationFactor = 1
                            }
                        }
                    );
                }
                catch (CreateTopicsException e)
                {
                    if (e.Results[0].Error.Code != ErrorCode.TopicAlreadyExists)
                    {
                        throw new Exception($"An error occured creating topic {topic}: {e.Results[0].Error.Reason}");
                    }
                    else
                    {
                        Console.WriteLine("Topic already exists");
                    }
                }
            }
        }
    }
}