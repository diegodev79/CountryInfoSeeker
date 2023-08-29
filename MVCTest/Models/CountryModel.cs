namespace MVCTest.Models
{
    public class CountryModel
    {
        public NameModel name { get; set; }
        public string cca2 { get; set; }
        public string cca3 { get; set; }
    }

    public class NameModel
    {
        public string common { get; set; }
        public string official { get; set; }
    }

}
