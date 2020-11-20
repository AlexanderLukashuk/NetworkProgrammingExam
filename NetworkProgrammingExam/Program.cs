using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NetworkProgrammingExam
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string input = String.Empty;
            int menu;
            int postNumber = 1;
            string pathGET = "https://jsonplaceholder.typicode.com/posts/";
            string pathPOST = "https://jsonplaceholder.typicode.com/posts/";
            string postTitle;
            string postBody;
            Console.WriteLine("Выберите опцию:");
            Console.WriteLine("1) Создать пост");
            Console.WriteLine("2) Получить список постов");
            Console.WriteLine("3) Получить пост по номеру");
            Console.WriteLine("4) Выход");
            input = Console.ReadLine();
            menu = int.Parse(input);
            List<Post> posts = new List<Post>();

            while (menu != 4)
            {
                switch (menu)
                {
                    case 1:
                        {
                            var postRequest = WebRequest.Create(pathPOST);
                            postRequest.Method = "POST";

                            Console.Write("Введите заголовок: ");
                            postTitle = Console.ReadLine();
                            Console.Write("Введите текст поста: ");
                            postBody = Console.ReadLine();

                            Post post = new Post()
                            {
                                UserId = new Random().Next(1000),
                                PostId = new Random().Next(1000),
                                Title = postTitle,
                                Body = postBody
                            };

                            string json = JsonSerializer.Serialize<Post>(post);

                            byte[] buffer = Encoding.UTF8.GetBytes(json);

                            postRequest.ContentType = "application/x-www-form-urlencoded";
                            postRequest.ContentLength = buffer.Length;

                            using (var stream = postRequest.GetRequestStream())
                            {
                                stream.Write(buffer, 0, buffer.Length);
                            }

                            //var postResponse = await postRequest.GetResponseAsync();
                            //using (var stream = postResponse.GetResponseStream())
                            //{
                            //    using (var reader = new StreamReader(stream))
                            //    {
                            //        Console.WriteLine(reader.ReadToEnd());
                            //    }
                            //}

                            //postResponse.Close();

                            break;
                        }
                    case 2:
                        {
                            var getRequest = WebRequest.Create(pathGET);
                            getRequest.Method = "GET";

                            var getResponse = getRequest.GetResponse() as HttpWebResponse;

                            using (var stream = new StreamReader(getResponse.GetResponseStream()))
                            {
                                Console.WriteLine(stream.ReadToEnd());
                            }
                            getResponse.Close();

                            break;
                        }
                    case 3:
                        {
                            Console.Write("Введите номер поста: ");
                            input = Console.ReadLine();
                            postNumber = int.Parse(input);

                            var getRequest = WebRequest.Create($"{pathGET}{postNumber}/");
                            getRequest.Method = "GET";

                            var getResponse = getRequest.GetResponse() as HttpWebResponse;

                            //Task.Run(() =>
                            //{
                            using (var stream = new StreamReader(getResponse.GetResponseStream()))
                            {
                                //Console.WriteLine(stream.ReadToEnd());
                                var json = stream.ReadToEnd();
                                //MessageBox.Show(json);
                                //client.DownloadFile(path, "file");
                                Post post = JsonSerializer.Deserialize<Post>(json);

                                Console.WriteLine($"{post.UserId}");
                                Console.WriteLine($"{post.PostId}");
                                Console.WriteLine($"{post.Title}");
                                Console.WriteLine($"{post.Body}");
                                posts.Add(post);
                                //context.Characters.Add(character);
                                //context.SaveChanges();
                            }
                            getResponse.Close();
                            //});

                            break;
                        }
                    case 4:
                        {
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                Console.WriteLine("Выберите опцию:");
                Console.WriteLine("1) Создать пост");
                Console.WriteLine("2) Получить список постов");
                Console.WriteLine("3) Получить пост по номеру");
                Console.WriteLine("4) Выход");
                input = Console.ReadLine();
                menu = int.Parse(input);
            }
        }
    }
}
