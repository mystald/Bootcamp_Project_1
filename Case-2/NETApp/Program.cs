using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NETApp.Data;
using NETApp.Kafka;
using NETApp.Models;

namespace NETApp
{
    class Program
    {
        private static case2twittorContext _appDbContext;

        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddDbContext<case2twittorContext>(options =>
                options.UseSqlServer("Server=localhost,1433; Initial Catalog=case2-twittor; User ID=twittor; Password=twittor"));
            var serviceProvider = services.BuildServiceProvider();
            _appDbContext = serviceProvider.GetService<case2twittorContext>();

            /*=================================================*/

            var _user = new DALUser(_appDbContext);

            var consumerObj = new Consumer();

            using (var consumer = consumerObj.consumer)
            {
                Console.WriteLine("Connected");

                var topics = new string[] { "user" };

                consumer.Subscribe(topics);

                try
                {
                    while (true)
                    {
                        var consumeResult = consumer.Consume();

                        Console.WriteLine($"Consumed message: Topic: {consumeResult.Topic} Key: {consumeResult.Message.Key} Value: {consumeResult.Message.Value}");

                        var entity = consumeResult.Topic;
                        var operation = consumeResult.Message.Key.Split("-")[0];
                        var value = consumeResult.Message.Value;

                        switch (entity)
                        {
                            case "user":
                                var valueObj = JsonSerializer.Deserialize<User>(value);
                                switch (operation)
                                {
                                    case "insert":
                                        await _user.Insert(valueObj);
                                        break;

                                    case "update":
                                        await _user.Update(valueObj);
                                        break;

                                    case "delete":
                                        await _user.Delete(valueObj);
                                        break;
                                }
                                break;
                        }
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
