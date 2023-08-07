namespace InfoTrackTechTest.Services
{
    public interface IScraperService
    {
        Task<string> GetHtmlContentAsync(string url);
    }
}
