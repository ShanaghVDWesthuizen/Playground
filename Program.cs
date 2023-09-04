using System;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using KingPriceUserManagementWebApp.Data;
using KingPriceUserManagementWebApp.Models;

class Program
{
    static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>();
}

