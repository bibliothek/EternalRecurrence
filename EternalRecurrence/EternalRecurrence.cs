using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs;

namespace EternalRecurrence
{
    public static class EternalRecurrence
    {
        private static string slackWebhookUri = Environment.GetEnvironmentVariable("SLACK_WEBHOOK_URI");

        private static HttpClient httpClient = new HttpClient();

        [FunctionName("TribeStandup")]
        public static async Task TribeStandup([TimerTrigger("0 40 9 * * 1,2,3,4,5")]TimerInfo myTimer, ILogger log)
        {
            var victim = Victims.GetVictim();
            var message = new Message
            {
                text = $"<@{victim}>, it is your turn to go to Tribe Standup"
            };
            var response = await httpClient.PostAsJsonAsync<Message>(slackWebhookUri, message);
            var responseContent = await response.Content.ReadAsStringAsync();
            log.Log(LogLevel.Information, responseContent);
        }

        [FunctionName("Retry")]
        public static async Task<IActionResult> Retry(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var victim = Victims.GetVictim();
            var message = new Message
            {
                text = $"Repetition is a form of change. <@{victim}>, you're up!"
            };
            var response = await httpClient.PostAsJsonAsync<Message>(slackWebhookUri, message);
            var responseContent = await response.Content.ReadAsStringAsync();
            log.Log(LogLevel.Information, responseContent);

            return new OkResult();
        }
    }
}
