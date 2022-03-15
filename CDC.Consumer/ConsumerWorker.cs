using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CDC.Consumer
{
    public class ConsumerWorker : BackgroundService
    {
        private readonly ILogger<ConsumerWorker> _logger;
        private readonly IConsumer<string, string> _consumer;

        public ConsumerWorker(ILogger<ConsumerWorker> logger)
        {
            _logger = logger;

            var conf = new ConsumerConfig
            {
                GroupId = "employee-consumer-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false,
                AutoCommitIntervalMs = 0
            };

            _consumer = new ConsumerBuilder<string, string>(conf)
                .Build();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Subscribing");
            _consumer.Subscribe("Employee");

            var tcs = new TaskCompletionSource<bool>();

            // polling for messages is a blocking operation,
            // so spawning a new thread to keep doing it in the background
            var thread = new Thread(() =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        //_logger.LogInformation("Waiting for message...");

                        ConsumeResult<string, string> result = _consumer.Consume(stoppingToken);

                        string receivedMessage = Convert.ToString(result.Message.Value);
                        dynamic dynamicReceivedMessage = (dynamic)JObject.Parse(receivedMessage);
                        string eventName = Convert.ToString(dynamicReceivedMessage.eventName);
                        _logger.LogInformation(eventName);
                        _logger.LogInformation(result.Message.Key);

                        _consumer.Commit(); // note: committing every time can have a negative impact on performance
                    }
                    catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                    {
                        _logger.LogInformation("Shutting down gracefully.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error occurred when consuming message!");
                    }
                }

                tcs.SetResult(true);
            })
            {
                IsBackground = true
            };

            thread.Start();

            return tcs.Task;
        }

        public override void Dispose()
        {
            try
            {
                _consumer?.Close();
            }
            catch (Exception)
            {
                // no exceptions in Dispose :)
            }

            _consumer?.Dispose();
            base.Dispose();
        }
    }
}
