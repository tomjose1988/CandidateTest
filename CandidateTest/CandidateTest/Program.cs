// See https://aka.ms/new-console-template for more information
using Business.Interfaces;
using Business.Managers;
using Business.Managers.Import;
using Business.Managers.Export;
using Microsoft.Extensions.DependencyInjection;
using CandidateTest;
using Microsoft.Extensions.Configuration;

IConfiguration configuration = null;
var path = Path.Combine(Environment.CurrentDirectory, "AppSettings.json");
var configurationBuilder = new ConfigurationBuilder().AddJsonFile(path);
configuration=configurationBuilder.Build();
                                
var services = new ServiceCollection();
services.AddSingleton<IConfiguration>(configuration)
    .AddSingleton<IManager, OrderManager>()
    .AddSingleton<IOrderImportManager, OrderCSVFileImportManager>()
    .AddSingleton<IOrderExportManager, OrderXMLFileExportManager>()
    .AddSingleton<Service,Service>()
    .BuildServiceProvider().GetService<Service>().StartService();






