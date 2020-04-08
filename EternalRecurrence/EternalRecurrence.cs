using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace EternalRecurrence
{
    public static class EternalRecurrence
    {
        private static Dictionary<string, string> victims = new Dictionary<string, string> {
            {"steml", "U59F531H7"},
            {"maber", "U1D7UQT6V"},
            {"ivapo", "UR3C5Q1ST"},
            {"flole", "UD3H5EV40"},
            {"matha", "U0C3HCB7C"},
        };

        private static string slackWebhookUri = Environment.GetEnvironmentVariable("SLACK_WEBHOOK_URI");

        private static HttpClient httpClient = new HttpClient();

        [FunctionName("TribeStandup")]
        public static void TribeStandup([TimerTrigger("0 40 9 * * 1,2,3,4,5")]TimerInfo myTimer, ILogger log)
        {
            var victim = GetVictim();
            var message = new Message
            {
                text = $"<@{victim}>, it's your turn to go to Tribe Standup"
            };
            var response = httpClient.PostAsJsonAsync<Message>(slackWebhookUri, message).Result;
            log.Log(LogLevel.Information, response.Content.ReadAsStringAsync().Result);
        }

        public class Message
        {
            public string text { get; set; }
        }

        private static string GetVictim()
        {
            var i = Math.Abs(GetRandom()) % victims.Count;
            return victims.Values.ToArray()[i];
        }

        private static int GetRandom()
        {
            using (RNGCryptoServiceProvider rg = new RNGCryptoServiceProvider())
            {
                byte[] rno = new byte[5];
                rg.GetBytes(rno);
                return BitConverter.ToInt32(rno, 0);
            }
        }
    }
}
