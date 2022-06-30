using System.Net; //http client to get json string from kens website
using Newtonsoft.Json.Serialization; //get json out of string from http client
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
        InterestingPlaceOptions options = new();
        private bool doRepeat;
        System.Data.DataTable table = new System.Data.DataTable();
        public MapForm()
        {
            InitializeComponent();
        }



        private async void Form1_Load(object sender, EventArgs e)
        {
            options = new InterestingPlaceOptions();
            SetUpTable();


            await Task.Run(() => Repeater());
            myMap.MouseMove += MyMap_MouseMove;

        }

        private void MyMap_MouseMove(object? sender, MouseEventArgs e)
        {
            var lat = myMap.FromLocalToLatLng(e.X, e.Y).Lat;
            var lng = myMap.FromLocalToLatLng(e.X, e.Y).Lng;
            Text = title + $" Lat: {lat:F15} Long: {lng:F15}";
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
        private async Task<List<InterestingPlace>> GetPlacesOfInterest(int minid = 0, int maxid = 0, int limit = 0)
        {
            string url = @"http://developer.kensnz.com/getlocdata";
            string newjson = string.Empty;
            while (newjson == null || newjson == string.Empty)
            {
                using (HttpClient client = new HttpClient())
                {
                    newjson = await client.GetStringAsync(url);
                }
            }

            if (json.Length == newjson.Length) return places; // if no change, dont reprocess
            json = newjson;
            JArray jerry = JArray.Parse(json);
            var interestingPlaces = jerry.Select(p => new InterestingPlace
            {
                ID = (int)p["id"],
                UserID = (string)p["userid"] ?? string.Empty,
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
            if (FilterForm == null) FilterForm = new FilterForm(parent: this, options: options, table);
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
                            place.Created_At >= options.CreatedMin && place.Created_At <= options.CreatedMax
                            select place).ToList();
            RefreshTable(CustomPlaces);

            if (options.UserFilter.Count > 0) CustomPlaces = CustomPlaces.Select(p => p).TakeWhile(p => options.UserFilter.Contains(p.UserID)).ToList();
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


        private void centerOnInvercargillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myMap.SetPositionByKeywords("Invercargill, Southland, New Zealand");
            myMap.Zoom = 13;

        }
    }
    public class InterestingPlace
    {
        public int ID { get; set; }
        public string UserID { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public override string ToString()
        {
            return $"Mark {ID} from Student {UserID}\nCreated on {Created_At.ToString("g")}\n{Description}";
        }
    }
    public class InterestingPlaceOptions
    {
        public int LimitResults { get; set; }
        public int IDMin { get; set; }
        public int IDMax { get; set; } = int.MaxValue;
        public List<string> UserFilter { get; set; } = new List<string>();
        public double LatMin { get; set; } = -90;
        public double LatMax { get; set; } = 90;
        public double LongMin { get; set; } = -180;
        public double LongMax { get; set; } = 180;

        public DateTime CreatedMin { get; set; } = DateTime.Today.AddMonths(-14);
        public DateTime CreatedMax { get; set; } = DateTime.Today.AddDays(2);
    }
}