using System;
using NLog.Web;
using System.IO;
using System.Linq;

namespace BlogsConsole
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {

            int selection = 0;
            logger.Info("Program started");

            do
            {
                Console.WriteLine("What would you like to do?\n1) View all Blogs\n 2) Add a blog\n 3) Add a post\n 4) View all posts\n 5).Exit");
                selection = Convert.ToInt32(Console.ReadLine());
                if(selection == 1){
                    // Display all Blogs from the database
                    var db = new BloggingContext();
                    var query = db.Blogs.OrderBy(b => b.Name);
                    
                    

                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }
                } else if (selection == 2){
                // Create and save a new Blog
                Console.Write("Enter a name for a new Blog: ");
                var name = Console.ReadLine();

                var blog = new Blog { Name = name };

                var db = new BloggingContext();
                db.AddBlog(blog);
                logger.Info("Blog added - {name}", name);

                } else if (selection == 3){
                    
                    Console.WriteLine("Please select a Blog to post to");
                    var db = new BloggingContext();
                    var query = db.Blogs.OrderBy(b => b.BlogId);
                     foreach (var item in query)
                {
                    Console.WriteLine(item.BlogId +". " + item.Name);
                }
                   var input = Console.ReadLine(); //get the input
                    int choice;
                    if (!int.TryParse(input, out choice))
                            {
                                Console.WriteLine("Not an integer");
                            }
                            
                            else {
                                    choice = Convert.ToInt32(input);
                                    int max = query.Count();
                                    
                                    if (choice > max) {
                                        Console.WriteLine("Not a valid Blog Id");
                                    }
                                    else {
                                        Console.WriteLine("Enter the Post title");
                                        var title = Console.ReadLine();
                                        if (title.Length == 0){
                                            Console.WriteLine("Must enter a Title");
                                        } else{
                                            Console.WriteLine("Enter Post content");
                                            var content = Console.ReadLine();
                                            
                                            var post = new Post {Title = title, Content = content, BlogId = choice };
                                            db.AddPost(post);
                                            logger.Info("Post added - {title}", title);
                                        }
                                        
                                    }
                            }
  
                } else if (selection == 4){
                     var db = new BloggingContext();
                     var count = db.Posts.Count();

                     Console.WriteLine("What Blog would you like to view the posts of?\n0). All Blogs\n");
                     var query = db.Blogs.OrderBy(b => b.BlogId);
                     foreach (var item in query)
                {
                    Console.WriteLine(item.BlogId +". " + item.Name);
                }
                   var input = Console.ReadLine(); //get the input
                    int choice;
                    if (!int.TryParse(input, out choice))
                            {
                                Console.WriteLine("Not an integer");
                            }
                            
                            else{
                            
                             choice = Convert.ToInt32(input);
                             int max = query.Count();
                                if (choice == 0){
                                     Console.WriteLine("Number of posts: " + count);
                                     var query2 = db.Posts.OrderBy(b => b.PostId);
                                 foreach (var item in query2)
                                    {
                                        Console.WriteLine("Blog: " + item.Blog.Name);
                                        Console.WriteLine("Title: " + item.Title);
                                        Console.WriteLine("Content: " + item.Content);
                                    }
                                    }else if(choice > 0 & 0 < max){
                                        
                                        var query3 = db.Posts.Where(b => b.BlogId == choice);
                                        var count2 = query3.Count();
                                        Console.WriteLine("Number of posts: " + count2);
                                        foreach (var item in query3)
                                    {
                                    Console.WriteLine("Blog: " + item.Blog.Name);
                                        Console.WriteLine("Title: " + item.Title);
                                        Console.WriteLine("Content: " + item.Content);
                                    }

                     }
                     else if (choice > max ) {
                      Console.WriteLine("Not a valid Blog Id");
                     }
      
                }
                }

            }while(selection != 5);  
            
            logger.Info("Program ended");
        }
    }
}
