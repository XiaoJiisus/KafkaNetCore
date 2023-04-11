using Consumer.Hubs;
using Confluent.Kafka;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace Consumer.Services;

public class KafkaConsumer : IHostedService, IDisposable
{
    private IHubContext<CollegeHub> _collegeHub;
    private readonly IConsumer<Null, string> _consumer;
    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
    private Thread _pollLoopThread;

    public KafkaConsumer(IHubContext<CollegeHub> collegeHub, IConsumer<Null, string> consumer)
    {
        _collegeHub = collegeHub;
        _consumer = consumer;
        // _collegeHub.Clients.All.SendAsync("ShowKafkaMessage", );
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _pollLoopThread = new Thread(() =>
        {
            try
            {
                // using (var consumer = new ConsumerBuilder<Null, string>(_consumer).Build())
                // {
                    _consumer.Subscribe("college-topic");

                    try
                    {
                        while (!_cancellationTokenSource.IsCancellationRequested)//while (true)
                        {
                            var consr = _consumer.Consume(_cancellationTokenSource.Token);
                            _collegeHub.Clients.All.SendAsync("ShowKafkaMessage", $"received: {consr.Value}");
                            Console.WriteLine($"{consr.Value}");
                        }
                    }
                    catch (OperationCanceledException) {}

                    _consumer.Close();
                // }
            }
            catch
            {
                // something bad happened. logic should be improved to ensure consumer is always
                // operational over lifetime of background service.
            }
        });

        _pollLoopThread.Start();

        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.Run(() =>
        {
            _cancellationTokenSource.Cancel();
            _pollLoopThread.Join();
        });
    }

    public void Dispose() {}
}