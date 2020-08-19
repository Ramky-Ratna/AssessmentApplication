using DataService;
using DataService.Models;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace AssessmentApplication
{
    /// <summary>
    /// Program class
    /// </summary>
    public class Program
    {
        private static TelemetryClient _telemetryClient;
        private static IServiceProvider _serviceProvider;

        /// Main method
        /// </summary>
        /// <param name="args">string arguments</param>
        static void Main(string[] args)
        {
            RegisterServices();
            var services = _serviceProvider.GetService<IDataRepository>();
            try
            {
                TelemetryConfiguration teleconfiguration = TelemetryConfiguration.CreateDefault();
                teleconfiguration.InstrumentationKey = "7f4c73d9-de75-44da-b04d-0b84605a37b4";
                _telemetryClient = new TelemetryClient(teleconfiguration);
                _telemetryClient.TrackTrace("Main method execution started");
                var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

                Console.WriteLine("enter text file name without extention");
                string filename = Console.ReadLine();
                Console.WriteLine("Eneter Minimum sales amount");
                decimal minimumSaleAmount = Convert.ToDecimal(Console.ReadLine());
                var configuration = builder.Build();

                var currentdir = Directory.GetCurrentDirectory() + "\\" + configuration["sourceDirectory"];
                var textFiles = Directory.EnumerateFiles(currentdir, filename + ".txt");
                if (textFiles != null)
                    Process_TextFiles(textFiles, minimumSaleAmount, configuration["apiUrl"]);
                else
                    Console.WriteLine("Text file not vailable in folder");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _telemetryClient.TrackException(ex);
                services.LogFile(ex.GetType().ToString(), "MainMethod", "Program.cs", ex.Message.ToString());
            }
            _telemetryClient.TrackTrace("Main method execution Ended");
            DisposeServices();
        }

        /// <summary>
        /// POST method to insert customer
        /// </summary>
        /// <param name="model">list of customers data</param>
        /// <param name="url">API Url string</param>
        public static void PostEvent_data(List<Customers> model,string url)  
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add("Content-Type:application/json");
                    client.Headers.Add("Accept:application/json");
                    var result = client.UploadString(url, JsonConvert.SerializeObject(model));
                    Console.WriteLine(result);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            
        }

        /// <summary>
        /// Processing text files
        /// </summary>
        /// <param name="textFiles">Text file string</param>
        /// <param name="url">API url string</param>
        public static void Process_TextFiles(IEnumerable<string> textFiles, decimal minimumSaleAmount, string url)
        {
            try
            {
                foreach (string file in textFiles)
                {
                    char[] specialchars = { '#', ';', '-', ':' };
                    List<string> Textlines = File.ReadAllLines(file).Where(line => !specialchars.Contains(line.First())).ToList();
                    List<Customers> sortedlines = new List<Customers>();
                    foreach (string line in Textlines)
                    {
                        string[] col = line.Split(new char[] { ',' });
                        var date =Convert.ToDateTime(col[4].Replace("[", "").Replace("]", "").Replace("(", "").Replace(")", "")).ToLocalTime();
                        if (date<=DateTime.Now.ToLocalTime() && minimumSaleAmount< Convert.ToDecimal(col[3]))
                        {
                            sortedlines.Add(new Customers
                            {
                                CustomerId = Convert.ToInt32(col[0]),
                                CustomerType = (col[1] == "1" ? "Private Person" : "Company"),
                                CustomerName = col[2].ToString(),
                                TotalSalesAmount = Convert.ToDecimal(col[3]),
                                TimeStamp = Convert.ToDateTime(date).ToLocalTime()
                            });
                        }                        
                    }

                    PostEvent_data(sortedlines, url);

                    foreach (Customers model in sortedlines)
                    {
                        Console.Out.WriteLine(model.CustomerId + "," + model.CustomerType + "," + model.CustomerName + "," + model.TotalSalesAmount + "," + model.TimeStamp);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }            
        }

        /// <summary>
        /// Register Services
        /// </summary>
        private static void RegisterServices()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<IDataRepository, DataRepository>();
            
            _serviceProvider = collection.BuildServiceProvider();
        }

        /// <summary>
        /// Dispose Service
        /// </summary>
        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
