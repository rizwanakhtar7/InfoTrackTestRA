namespace InfoTrackTechTest.Models
{
    public class SearchTermResult
    {
       public List<string> TodaysResult { get; set; }

       public int TodaysCountIncludingLinks { get; set;}

        public int TodaysCountExcludingLinks { get; set; }


        public DateTime DateToday { get; set; }  
    }
}
