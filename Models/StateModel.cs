namespace ApiDemo.Models
{
    public class StateModel
    {
        public int? StateID { get; set; }

        public int CountryID { get; set; }

        public string StateName { get; set; }

        public string StateCode { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public int CityCount { get; set; } // New field
    }

    public class StateDropDownModel
    {
        public int? StateID { get; set; }

        public string StateName { get; set; }
    }
}
