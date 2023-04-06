﻿using Confluent.Kafka;
using System;
using System.Threading;

var config = new ConsumerConfig
{
    GroupId = "kafka-dotnet-getting-started",
    BootstrapServers = "localhost:9092",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

const string topic = "purchases";

CancellationTokenSource cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) => {
    e.Cancel = true; // prevent the process from terminating.
    cts.Cancel();
};

using (var consumer = new ConsumerBuilder<string, string>(config).Build())
{
    consumer.Subscribe(topic);
    
    try
    {
        while (true)
        {
            var cr = consumer.Consume(cts.Token);
            Console.WriteLine($"Consumed event from topic {topic} with key {cr.Message.Key,-10} and value {cr.Message.Value}");
        }
    }
    catch (OperationCanceledException)
    {
        // Ctrl-C was pressed.
    }
    finally
    {
        consumer.Close();
    }
}
