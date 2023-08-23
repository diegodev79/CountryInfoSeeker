namespace MVCTest.Models
{
    public class CountryInfoModel
    {
        public CountryName Name { get; set; }
        public bool independent { get; set; }
        public Dictionary<string, CurrencyInfo> Currencies { get; set; }
        public string[] Capital { get; set; }
        public Dictionary<string, string> Languages { get; set; }
        public double[] Latlng { get; set; }
        public double Area { get; set; }
        public string Flag { get; set; }
        public int Population { get; set; }

    }
    public class CurrencyInfo
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
    }
    public class CountryName
    {
        public string Common { get; set; }
        public string Official { get; set; }
        public NativeName NativeName { get; set; }
    }

    public class NativeName
    {
        public Grn Grn { get; set; }
        public Spa Spa { get; set; }
    }

    public class Grn
    {
        public string Official { get; set; }
        public string Common { get; set; }
    }

    public class Spa
    {
        public string Official { get; set; }
        public string Common { get; set; }
    }
}
