using InfoTrackTechTest.Models;
using InfoTrackTechTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace InfoTrackTechTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScrapedDataController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScraperService _scraperService;

        public ScrapedDataController(ILogger<HomeController> logger, IScraperService scraperService)
        {
            _logger = logger;
            _scraperService = scraperService;
        }


        [HttpGet]
        [Route("/api/getData")]
        public async Task<SearchTermResult> GetData()
        {

            var googleSearch = "https://www.google.co.uk/search?num=100&q=land+registry+search";


          

            //call actual web scraper

            
            var result = await ScrapeWebPage(googleSearch);

            var searchTerm = "www.hometrack.com";

            var escapedSearchTerm = Regex.Escape(searchTerm);

            var patternMatch = @$"<div[^>]*>\s*{escapedSearchTerm}[^<]*<\/div>";
         
            var matches = Regex.Matches(result, patternMatch, RegexOptions.IgnoreCase);
           
            List<string> results = new List<string>();  


            if (result != null)
            {
                foreach (Match match in matches)
                {
                    results.Add(match.ToString());
                }

            }


            //FOR including links and additional places

            var searchTermTwo = "www.infotrack.co.uk";

            var escapedSearchTermTwo = Regex.Escape(searchTermTwo); // Escape the search term


            var patternMatchTwo = @$"\b{escapedSearchTermTwo}\b";




            var matchesTwo = Regex.Matches(result, patternMatchTwo, RegexOptions.IgnoreCase);


            List<string> resultsTwo = new List<string>();


            if (result != null)
            {
                foreach (Match match in matchesTwo)
                {
                    resultsTwo.Add(match.ToString());
                }

            }

            var searchTermResultsToday = new SearchTermResult
            {
                TodaysResult = results,
                TodaysCountExcludingLinks = results.Count(),
                TodaysCountIncludingLinks = resultsTwo.Count(),
                DateToday = DateTime.Now,
                
            };

            return searchTermResultsToday;
        }

        [HttpPost]
        [Route("/api/scrapeddata")]
        public async Task<ActionResult<SearchTermResult>> ProcessData([FromBody] SearchData searchData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var formattedSearchTerm = searchData.SearchTerm.Replace(" ", "+");

            var googleSearch = $"https://www.google.co.uk/search?num=100&q={formattedSearchTerm}";

            // call service to make the request to google
            var htmlContent = await ScrapeWebPage(googleSearch);

            var matchesExcludingLinks = GetMatchesExcludingLinks(htmlContent, searchData.SearchUrl);

            List<string> results = new List<string>();

            var matchesIncludingLinks = GetMatchesIncludingLinks(htmlContent, searchData.SearchUrl);

            List<string> resultsTwo = new List<string>();

            if (htmlContent != null)
            {
                foreach (Match match in matchesIncludingLinks)
                {
                    resultsTwo.Add(match.ToString());
                }

            }

            if (htmlContent != null)
            {
                foreach (Match match in matchesExcludingLinks)
                {
                    results.Add(match.ToString());
                }

            }

            var searchTermResultsToday = new SearchTermResult
            {
                TodaysResult = results,
                TodaysCountExcludingLinks = results.Count(),
                TodaysCountIncludingLinks = resultsTwo.Count(),
                DateToday = DateTime.Now,
            };

            return Ok(searchTermResultsToday);
        }

        private MatchCollection GetMatchesExcludingLinks(string htmlContent, string searchUrl)
        {
            var escapedSearchTerm = Regex.Escape(searchUrl);

            var patternMatch = @$"<div[^>]*>\s*{escapedSearchTerm}[^<]*<\/div>";

            var matches = Regex.Matches(htmlContent, patternMatch, RegexOptions.IgnoreCase);

            return matches;
        }

        private MatchCollection GetMatchesIncludingLinks(string htmlContent, string searchUrl)
        {
            var escapedSearchTerm = Regex.Escape(searchUrl);

            var patternMatch = @$"\b{escapedSearchTerm}\b";

            var matches = Regex.Matches(htmlContent, patternMatch, RegexOptions.IgnoreCase);

            return matches;
        }

        public async Task<string> ScrapeWebPage(string url)
        {
            try
            {
                var htmlContent = await _scraperService.GetHtmlContentAsync(url);
                return htmlContent;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }

        }

    }
}
