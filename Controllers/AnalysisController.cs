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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnalysis(string phrase)
        {
            //"BLL" in the controller 
            AmazonComprehendClient comprehendClient = new AmazonComprehendClient(Amazon.RegionEndpoint.USEast1);
            DetectSentimentRequest detectSentimentRequest = new DetectSentimentRequest()
            {
                Text = phrase,
                LanguageCode = "en"
            };
            DetectSentimentResponse detectSentimentResponse = await comprehendClient.DetectSentimentAsync(detectSentimentRequest);
            
            var response = detectSentimentResponse.Sentiment;


            return Ok(response);
        }
    }
}