IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<InquiryTaskWorker>();
        services.AddScoped<RealTimeDataUpSender>();
        services.AddScoped<IInquiryTaskService, InquiryTaskService>();
        services.AddScoped<IRealTimeDataService, RealTimeDataService>();
        services.AddScoped<ISignatureService, SignatureService>();
    })
    .Build();

await host.RunAsync();
