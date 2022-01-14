using System;

namespace LoggingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var consumer = new Consumer().consumer;

            consumer.Subscribe("Logs");

            while (true)
            {
                var consumeResult = consumer.Consume();

                Console.WriteLine($"Event Detected: Key: {consumeResult.Message.Key} Value: {consumeResult.Message.Value}");
            }
        }
    }
}
