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

                } else if (selection == 4){
                     var db = new BloggingContext();
                     var query = db.Posts.OrderBy(b => b.Title);
                }
                

            }while(selection != 5);  
            
            

            logger.Info("Program ended");
        }
    }
}
