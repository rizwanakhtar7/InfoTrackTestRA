namespace InfoTrackTechTest.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}


//html content 
//var results = new List<string>();

//string pattern = url;

//MatchCollection matches = Regex.Matches(htmlContent, pattern, RegexOptions.IgnoreCase);

//foreach (Match match in matches )
//{
//    results.Add(match.Value);
//}

//return results;


//MatchCollection matches = Regex.Matches(htmlContent, position, RegexOptions.IgnoreCase);

//foreach (Match match in mc)
//{
//    string urlDiv = match.Value;
//    Console.WriteLine($"URL <div>: {urlDiv}");
//}

//foreach (Match match in matches)
//{
//    positions.Add(match.Index);
//}