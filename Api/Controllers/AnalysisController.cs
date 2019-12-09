using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Amazon.Comprehend;
using Amazon.Comprehend.Model;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AnalysisController : ControllerBase
    {
        private readonly DataContext _context;

        public AnalysisController(DataContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet("{phrase}")]
        public async Task<IActionResult> GetAnalysis(string phrase)
        {
            try
            {
                //"BLL" in the controller 
                AmazonComprehendClient comprehendClient = new AmazonComprehendClient(Amazon.RegionEndpoint.USEast1);
                DetectSentimentRequest detectSentimentRequest = new DetectSentimentRequest()
                {
                    Text = phrase,
                    LanguageCode = "en"
                };
                DetectSentimentResponse detectSentimentResponse = await comprehendClient.DetectSentimentAsync(detectSentimentRequest);

                var response = new Sentiment();
                response.Value = detectSentimentResponse.Sentiment.Value.ToString();
                response.SentimentScore_mixed = detectSentimentResponse.SentimentScore.Mixed;
                response.SentimentScore_negative = detectSentimentResponse.SentimentScore.Negative;
                response.SentimentScore_neutral = detectSentimentResponse.SentimentScore.Neutral;
                response.SentimentScore_positive = detectSentimentResponse.SentimentScore.Positive;

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}