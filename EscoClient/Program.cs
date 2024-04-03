// See https://aka.ms/new-console-template for more information
using Simple.OData.Client;
using System;
using System.IO;
using EscoService;

//Sleep in order to wait for the web service to start
// Thread.Sleep(10000);

var client = new ODataClient("https://localhost:7004/odata/");

var escos = await client
    .For<Esco>()
    .Top(2)
    .Skip(1)
    .FindEntriesAsync();

foreach (var esco in escos)
{
    Console.Out.Write(esco.Name);
}