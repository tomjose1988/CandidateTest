// See https://aka.ms/new-console-template for more information
using Business.Interfaces;
using Business.Managers;
using Business.Managers.Import;
using Business.Managers.Export;

Console.WriteLine("Start export!");

IOrderImportManager importManager = new OrderCSVFileImportManager();
IOrderExportManager exportManager=new OrderXMLFileExportManager();

IOrderManager manager = new OrderManager(importManager, exportManager);
manager.StartProcessing(@"D:\CandidateWork\Input", @"D:\CandidateWork\Output");
Console.WriteLine("Processing input files...\n\n Press enter to exit processing.");
Console.ReadLine();
manager.EndProcessing();
Console.ReadLine();
