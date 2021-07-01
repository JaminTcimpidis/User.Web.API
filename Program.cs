using System;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Users.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
          try{
            //Thread.Sleep(1000000);
            CreateHostBuilder(args).Build().Run();
          }
          catch(Exception ex){
            Console.WriteLine(ex);
          }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
