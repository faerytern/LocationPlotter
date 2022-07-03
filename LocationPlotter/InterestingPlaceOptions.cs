namespace LocationPlotter
{
    /// <summary>
    /// Ideally done in a better way than this, lol
    /// Every property on InterestingPlace has a min and a max. UserFilter instead of UserID Max and UserID Min because I ran out of time
    /// Used in conditional LINQ filtering
    /// </summary>
    public class InterestingPlaceOptions
    {
        public int LimitResults { get; set; }
        public int IDMin { get; set; }
        public int IDMax { get; set; } = int.MaxValue;
        public List<int> UserFilter { get; set; } = new List<int>();
        public double LatMin { get; set; } = -90;
        public double LatMax { get; set; } = 90;
        public double LongMin { get; set; } = -180;
        public double LongMax { get; set; } = 180;
        public DateTime CreatedMin { get; set; } = DateTime.Parse("2022-01-01");
        public DateTime CreatedMax { get; set; } = DateTime.Today.AddDays(2);
        public bool UniqueResults { get; set; } = true;
        public float PenWidth { get; set; } = 4;
    }
}