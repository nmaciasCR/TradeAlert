using NotificationManager;
using NotificationManager.Business;
using NotificationManager.Business.Interfaces;
using System.Net.NetworkInformation;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
