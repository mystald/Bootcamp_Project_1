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
            var _userRole = new DALUserRole(_appDbContext);
            var _twittor = new DALTwittor(_appDbContext);
            //var _comment = new DALComment(_appDbContext);

            var consumerObj = new Consumer();

            using (var consumer = consumerObj.consumer)
            {
                Console.WriteLine("Connected");

                var topics = new string[] { "user", "userrole", "twittor" };

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
                                var userObj = JsonSerializer.Deserialize<User>(value);
                                switch (operation)
                                {
                                    case "insert":
                                        await _user.Insert(userObj);
                                        break;

                                    case "update":
                                        await _user.Update(userObj);
                                        break;

                                    case "delete":
                                        await _user.Delete(userObj);
                                        break;
                                }
                                break;

                            case "userrole":
                                var userRoleObj = JsonSerializer.Deserialize<UserRole>(value);
                                switch (operation)
                                {
                                    case "insert":
                                        await _userRole.Insert(userRoleObj);
                                        break;

                                    case "delete":
                                        await _userRole.Delete(userRoleObj);
                                        break;
                                }
                                break;

                            case "twittor":
                                var twittorObj = JsonSerializer.Deserialize<Twittor>(value);
                                switch (operation)
                                {
                                    case "insert":
                                        await _twittor.Insert(twittorObj);
                                        break;

                                    case "update":
                                        await _twittor.Update(twittorObj);
                                        break;

                                    case "delete":
                                        await _twittor.Delete(twittorObj);
                                        break;
                                }

                                break;

                                /*case "comment":
                                    var commentObj = JsonSerializer.Deserialize<Comment>(value);
                                    switch (operation)
                                    {
                                        case "insert":
                                            await _comment.Insert(commentObj);
                                            break;

                                        case "update":
                                            await _comment.Update(commentObj);
                                            break;

                                        case "delete":
                                            await _comment.Delete(commentObj);
                                            break;
                                    }

                                    break;*/
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
