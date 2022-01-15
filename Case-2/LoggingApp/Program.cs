using System;
using LoggingApp.Kafka;

namespace LoggingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var consumerObj = new Consumer();
            using (var consumer = consumerObj.consumer)
            {
                consumer.Subscribe("loggings");
                Console.WriteLine("Listening Logs...");

                try
                {
                    while (true)
                    {
                        var consumeResult = consumer.Consume();

                        Console.WriteLine($"Event Detected: Key: {consumeResult.Message.Key} Value: {consumeResult.Message.Value}");
                    }
                }
                catch (OperationCanceledException)
                {
                }
                finally
                {
                    consumer.Close();
                }
            }
        }
    }
}
