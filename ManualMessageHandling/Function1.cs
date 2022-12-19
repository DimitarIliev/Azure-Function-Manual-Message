using System;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ManualMessageHandling
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> log)
        {
            _logger = log;
        }

        [FunctionName("Function1")]
        public async Task Run([ServiceBusTrigger("manual-msg", "iliev-pc", Connection = "ConnectionString")]Message mySbMsg, MessageReceiver messageReceiver)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message");

            var success = false;

            if (success)
            {
                await messageReceiver.CompleteAsync(mySbMsg.SystemProperties.LockToken);
            }
            else
            {
                await messageReceiver.DeadLetterAsync(mySbMsg.SystemProperties.LockToken);
                //await messageReceiver.AbandonAsync(mySbMsg.SystemProperties.LockToken);
            }

        }
    }
}
