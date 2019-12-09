
namespace DatingApp.API.Models.Data
{
    public class Sentiment
    {
        public string Value {get; set; } 
        public double SentimentScore_mixed { get; set; }
        public double SentimentScore_negative { get; set; }
        public double SentimentScore_neutral { get; set; }
        public double SentimentScore_positive { get; set; }
    }
}