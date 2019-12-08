using Amazon.Comprehend;
using Amazon.Comprehend.Model;
using System;
namespace DatingApp.API.Models.Data
{
    public class Sentiment
    {
        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            String text = "It's raining today in Seattle!";

            AmazonComprehendClient comprehendClient = new AmazonComprehendClient(Amazon.RegionEndpoint.USEast1);

            // Call DetectKeyPhrases API
            Console.WriteLine("Calling DetectSentiment");
            DetectSentimentRequest detectSentimentRequest = new DetectSentimentRequest()
            {
                Text = text,
                LanguageCode = "en"
            };
            DetectSentimentResponse detectSentimentResponse = await comprehendClient.DetectSentimentAsync(detectSentimentRequest);
            //DetectSentimentResponse detectSentimentResponse = comprehendClient.DetectSentimentAsync(detectSentimentRequest);
            Console.WriteLine(detectSentimentResponse.Sentiment);
            Console.WriteLine("Done");
        }
    }
}