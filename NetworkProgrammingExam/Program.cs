using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace NetworkProgrammingExam
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = String.Empty;
            int menu;
            int postNumber = 1;
            string pathToGetPosts = "https://jsonplaceholder.typicode.com/posts/";
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


                            break;
                        }
                    case 2:
                        {
                            var getRequest = WebRequest.Create(pathToGetPosts);
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

                            var getRequest = WebRequest.Create($"{pathToGetPosts}{postNumber}/");
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
