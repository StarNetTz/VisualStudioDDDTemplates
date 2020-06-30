﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using ServiceStack;

namespace DomainName.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NLog.LogManager.LoadConfiguration("nlog.config");
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseModularStartup<Startup>()
                .Build();
    }
}
