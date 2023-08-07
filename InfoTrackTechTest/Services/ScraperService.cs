using System.Net;
using System.Text;

namespace InfoTrackTechTest.Services
{
    public class ScraperService : IScraperService
    {
        public async Task<string> GetHtmlContentAsync(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);

                using (WebResponse response = await request.GetResponseAsync())
                using (Stream dataStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(dataStream))
                {
                    string htmlContent = await reader.ReadToEndAsync();
                    return htmlContent;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
