using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CoreTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            IPHostEntry fromHE = Dns.GetHostEntry(Dns.GetHostName());
            string[] urls = new string[fromHE.AddressList.Length];
            for(int i=0;i<fromHE.AddressList.Length;i++)
            {
                urls[i] = string.Format("http://{0}:{1}", fromHE.AddressList[i], 5000);
            }
            return WebHost.CreateDefaultBuilder(args)
                     .UseKestrel()
                     .UseContentRoot(Directory.GetCurrentDirectory())
                     .UseIISIntegration()
                     .UseUrls(urls)
                     .UseStartup<Startup>()
                     .Build();
        }
    }
}
