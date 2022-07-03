using Newtonsoft.Json.Linq; // get objects from json
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
namespace LocationPlotter
{
    public partial class MapForm : Form
    {
        public string title = "Map Plotter";
        public List<InterestingPlace> places;
        public List<InterestingPlace> CustomPlaces;
        GMapOverlay markers;
        GMapOverlay polygons;
        public string json = string.Empty;
        PeriodicTimer timer;
        FilterForm FilterForm;
        // Filter Arguments for places list
        public InterestingPlaceOptions options = new();
        private bool doRepeat;
        public System.Data.DataTable table = new System.Data.DataTable();
        double currentLongitude;
        double currentLatitude;
        public MapForm()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            SetUpTable();

            await Task.Run(() => Repeater());
        }

        private void MyMap_MouseMove(object? sender, MouseEventArgs e)
        {
            currentLatitude = myMap.FromLocalToLatLng(e.X, e.Y).Lat;
            currentLongitude = myMap.FromLocalToLatLng(e.X, e.Y).Lng;
            Text = title + $" Lat: {currentLatitude:F15} Long: {currentLongitude:F15}";
        }


        private void SetUpTable()
        {
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("UserID", typeof(string));
            table.Columns.Add("Latitude", typeof(double));
            table.Columns.Add("Longitude", typeof(double));
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("Created_At", typeof(DateTime));
            table.Columns.Add("Updated_At", typeof(DateTime));
        }

        private async void myMap_Load(object sender, EventArgs e)
        {
            //Initial Setup
            GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            myMap.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
            myMap.ShowCenter = false;
            myMap.Zoom = 13;
            //Inconsistent across providers so chosen OpenStreetMapProvider
            myMap.SetPositionByKeywords("Invercargill, Southland, New Zealand");
            //Default is rightclick, changed for requirement
            myMap.DragButton = MouseButtons.Left;
            //myMap.Bearing = 180; //Flip the map upside down

            // Drawing Markers

            markers = new GMapOverlay("markers");
            myMap.Overlays.Add(markers);

            RefreshMarkers();
            myMap.ContextMenuStrip = contextMenuStrip;
        }
        private async Task<List<InterestingPlace>> GetPlacesOfInterest()
        {
           
            string newjson = string.Empty;
          
            using (HttpClient client = new HttpClient())
            {
                string url = @"http://developer.kensnz.com/getlocdata";
                newjson = await client.GetStringAsync(url);
                if (json.Length == newjson.Length) return places; // if no change, dont reprocess
                json = newjson;
            }
            JArray jerry = JArray.Parse(json);
            var interestingPlaces = jerry.Select(p => new InterestingPlace
            {
                ID = (int)p["id"],
                UserID = (int)p["userid"],
                Latitude = (double)p["latitude"],
                Longitude = (double)p["longitude"],
                Description = (string)p["description"] ?? string.Empty,
                Created_At = (DateTime)p["created_at"],
                Updated_At = (DateTime)p["updated_at"]
            }).ToList();

            return interestingPlaces;
        }
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

            MessageBox.Show(owner: this,
                caption: "Help for Map Plotter 1.0",
                text: "IT722 Project 2022\n" +
                "Author: Kara Heffernan, 2016012187\n" +
                "Purpose\n" +
                "To map coordinates from multiple people sourced from a web server, draw convex hulls.\n" +
                "Actions and filtering are done through the right-click context menu anywhere on the form.\n",
                icon: MessageBoxIcon.Question,
                buttons: MessageBoxButtons.OK
                );
        }

        private async void refreshMarkersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshMarkers();
        }

        private void filterMarkersOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FilterForm == null || FilterForm.IsDisposed) FilterForm = new FilterForm(this);
            FilterForm.Show();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(owner: this,
                caption: "About Map Plotter 1.0",
                text: "IT722 Project 2022\n" +
                "Author: Kara Heffernan, 2016012187\n" +
                "Purpose\n" +
                "To map coordinates from multiple people sourced from a web server, draw convex hulls.\n" +
                "Actions and filtering are done through the right-click context menu anywhere on the form.\n",
                icon: MessageBoxIcon.Question,
                buttons: MessageBoxButtons.OK
                );
        }
        public async void RefreshMarkers()
        {
            markers.Markers.Clear();
            places = await GetPlacesOfInterest();
            table.Rows.Clear();
            CustomPlaces = (from place in places
                            where place.ID >= options.IDMin && place.ID <= options.IDMax &&
                            place.Created_At >= options.CreatedMin && place.Created_At <= options.CreatedMax &&
                            place.Latitude >= options.LatMin && place.Latitude <= options.LatMax &&
                            place.Longitude >= options.LongMin && place.Longitude <= options.LongMax

                            select place).OrderByDescending(p => p.Created_At).ToList();
            if (options.LimitResults > 0) CustomPlaces = CustomPlaces.Take(options.LimitResults).ToList();
            if (options.UserFilter.Count > 0) CustomPlaces = CustomPlaces.Select(p => p).TakeWhile(p => options.UserFilter.Contains(p.UserID)).ToList();
            if (options.UniqueResults) CustomPlaces = CustomPlaces.Distinct().ToList();
            RefreshTable(CustomPlaces);

            foreach (var place in CustomPlaces)
            {
                var marker = new GMarkerGoogle(
                    new PointLatLng(place.Latitude, place.Longitude),
                    GMarkerGoogleType.blue_pushpin)
                {
                    ToolTipText = place.ToString(),
                    Tag = place
                };
                markers.Markers.Add(marker);
            }
        }
        private void RefreshTable(List<InterestingPlace> interestingPlaces)
        {
            foreach (var p in interestingPlaces)
            {
                table.Rows.Add(p.ID, p.UserID, p.Latitude, p.Longitude, p.Description, p.Created_At, p.Updated_At);
            }
        }
        private async void Repeater()
        {
            timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
            while (await timer.WaitForNextTickAsync())
            {
                if (!doRepeat) continue;
                var newplaces = await GetPlacesOfInterest();
                if (places == newplaces) continue;
                else
                {
                    places = newplaces;

                    RefreshMarkers();
                }
            }
        }



        private void copyLocationToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText($"{currentLatitude:F10}, {currentLongitude:F10}");
        }
        private void resetToStandardZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myMap.Zoom = 13;
        }
        private void ResetToDefaultViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myMap.SetPositionByKeywords("Invercargill, Southland, New Zealand");
            myMap.Zoom = 13;
        }
        private void resetSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            options = new InterestingPlaceOptions();
            if (FilterForm != null && !FilterForm.IsDisposed) { FilterForm.options = options; FilterForm.RefreshPropertyGrid(); }
            RefreshMarkers();
        }
    }
    public class InterestingPlace :IEquatable<InterestingPlace>
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
                    && Latitude.ToString("3F") == place.Latitude.ToString("3F")
                    && Longitude.ToString("3F") == place.Longitude.ToString("3F");
                //Tostrings to try address precision issues of floating point doublies
            }
            else return false;
        }

        public bool Equals(InterestingPlace? other)
        {
            if (other is not null)
            {
                return UserID == other.UserID
                    && Latitude.ToString("3F") == other.Latitude.ToString("3F")
                    && Longitude.ToString("3F") == other.Longitude.ToString("3F");
                //Tostrings to try address precision issues of floating point doublies
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return (UserID ^ (Latitude.ToString("3F").GetHashCode() * Longitude.ToString("3F").GetHashCode()));
        }
    }
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
    }
}