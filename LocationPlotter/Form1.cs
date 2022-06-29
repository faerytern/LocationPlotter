using System.Net; //http client to get json string from kens website
using Newtonsoft.Json.Serialization; //get json out of string from http client
using Newtonsoft.Json.Linq; // get objects from json
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;


namespace LocationPlotter
{
    public partial class Form1 : Form
    {
        List<InterestingPlace> places;
        GMapOverlay markers;
        public string json = string.Empty;
        PeriodicTimer timer;

       
        
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
            while(await timer.WaitForNextTickAsync())
            {
                var newplaces = await GetPlacesOfInterest();
                if (places == newplaces) continue;
                else
                {
                    places = newplaces;
                    RefreshMarkers();
                }
            }

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
            places = await GetPlacesOfInterest();
            //MessageBox.Show(placesOfInterest.Count.ToString());

            markers = new GMapOverlay("markers");
            myMap.Overlays.Add(markers);

            RefreshMarkers();
            myMap.ContextMenuStrip = contextMenuStrip;
        }
        private async Task<List<InterestingPlace>> GetPlacesOfInterest(int minid = 0, int maxid = 0, int limit = 0)
        {
            string url = @"http://developer.kensnz.com/getlocdata";
            string newjson = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                newjson = await client.GetStringAsync(url);
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
            
        }

        private async void refreshMarkersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshMarkers();
        }

        private void filterMarkersOnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private async void RefreshMarkers(int filter1 = 0, int filter2 = 0)
        {
            markers.Clear();
            foreach (var place in await GetPlacesOfInterest())
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
    }
    public class InterestingPlace
    {
        // Filter Arguments for places list
        public static int IDStart = 0, IDFinish = int.MaxValue, IDLimitResults = int.MaxValue;
        public static string[]? UserIDFilter;
        public static double LatStart = 0, LatFinish = double.MaxValue, LatLimitResults = double.MaxValue;
        public static double LongStart = 0, LongFinish = double.MaxValue, LongLimitResults = double.MaxValue;
        public static DateTime CreatedStart = DateTime.MinValue, CreatedFinish = DateTime.Now; 
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
}