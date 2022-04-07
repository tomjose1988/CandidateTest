// See https://aka.ms/new-console-template for more information
using Business.Interfaces;
using Business.Managers;
using Business.Managers.Import;
using Business.Managers.Export;

Console.WriteLine("Start export!");

IOrderCSVFileImportManager importManager = new OrderCSVFileImportManager();
IOrderXmlFileExportManager exportManager=new OrderXMLFileExportManager();

OrderManager manager = new OrderManager(importManager, exportManager);
manager.StartExport(@"D:\CandidateWork\Input", @"D:\CandidateWork\Output");
Console.ReadLine();
