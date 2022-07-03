namespace LocationPlotter
{
    /// <summary>
    /// Chose InterestingPlace as name instead of PlaceOfInterest so collections will follow the convention of pluralising the final word
    /// Contains all fields for JSON Objects retrieved from http://developer.kensnz.com/getlocdata
    /// Collections can be filtered by InterestingPlaceOptions object
    /// IEquatable is implemented so I can call .Distinct() on a collection with LINQ.
    /// Multiple of the same points ended up actually just causing clutter and a really heavy shadow so this is fiiiiine instead
    /// </summary>
    public class InterestingPlace : IEquatable<InterestingPlace>
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public override string ToString()
        {
            return $"\nMark {ID} from Student {UserID}\nCreated on {Created_At.ToString("g")}\n{Description}";
        }
        public override bool Equals(object? obj)
        {
            if (obj is not null && obj is InterestingPlace place)
            {
                return UserID == place.UserID
                    && Latitude.ToString("5F") == place.Latitude.ToString("5F")
                    && Longitude.ToString("5F") == place.Longitude.ToString("5F");
                //Tostrings to try address precision issues of floating point doublies
            }
            else return false;
        }
        public bool Equals(InterestingPlace? other)
        {
            if (other is not null)
            {
                return UserID == other.UserID
                    && Latitude.ToString("5F") == other.Latitude.ToString("5F")
                    && Longitude.ToString("5F") == other.Longitude.ToString("5F");
                // ToStrings to try address precision issues of floating point doublies
            }
            else return false;
        }
        public override int GetHashCode()
        {
            return (UserID + Latitude + Longitude).ToString("5F").GetHashCode();
        }
    }
}