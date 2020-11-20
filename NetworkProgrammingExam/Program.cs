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
        static void Main(string[] args)
        {
            string input = String.Empty;
            int menu = 4;
            int postNumber = 1;
            string pathGET = "https://jsonplaceholder.typicode.com/posts/";
            string pathPOST = "https://jsonplaceholder.typicode.com/posts/";
            string postTitle;
            string postBody;
            bool result;
            Console.WriteLine("Выберите опцию:");
            Console.WriteLine("1) Создать пост");
            Console.WriteLine("2) Получить список постов");
            Console.WriteLine("3) Получить пост по номеру");
            Console.WriteLine("4) Выход");
            input = Console.ReadLine();
            result = int.TryParse(input, out menu);
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

                            break;
                        }
                    case 2:
                        {
                            posts.Clear();
                            var getRequest = WebRequest.Create(pathGET);
                            getRequest.Method = "GET";

                            var getResponse = getRequest.GetResponse() as HttpWebResponse;

                            using (var stream = new StreamReader(getResponse.GetResponseStream()))
                            {
                                var json = stream.ReadToEnd();
                                posts = JsonSerializer.Deserialize<List<Post>>(json);
                            }
                            getResponse.Close();

                            break;
                        }
                    case 3:
                        {
                            Console.Write("Введите номер поста: ");
                            input = Console.ReadLine();
                            postNumber = int.Parse(input);

                            try
                            {
                                var getRequest = WebRequest.Create($"{pathGET}{postNumber}/");
                                getRequest.Method = "GET";

                                var getResponse = getRequest.GetResponse() as HttpWebResponse;

                                using (var stream = new StreamReader(getResponse.GetResponseStream()))
                                {
                                    var json = stream.ReadToEnd();

                                    Post post = JsonSerializer.Deserialize<Post>(json);

                                    Console.WriteLine($"{post.UserId}");
                                    Console.WriteLine($"{post.PostId}");
                                    Console.WriteLine($"{post.Title}");
                                    Console.WriteLine($"{post.Body}");
                                    posts.Add(post);
                                }
                                getResponse.Close();
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine(exception.Message);
                                Console.WriteLine("Вы ввели недопустимое число");
                                Console.WriteLine("Попробуйте еще раз");
                            }

                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Good Bye");
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Вы ввели неверную опцию");
                            Console.WriteLine("Попробуйте выбрать еще раз");
                            break;
                        }
                }

                Console.WriteLine("Выберите опцию:");
                Console.WriteLine("1) Создать пост");
                Console.WriteLine("2) Получить список постов");
                Console.WriteLine("3) Получить пост по номеру");
                Console.WriteLine("4) Выход");
                input = Console.ReadLine();
                result = int.TryParse(input, out menu);
            }

        }
    }
}
