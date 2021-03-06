using Newtonsoft.Json.Linq; // get objects from json
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Text;

namespace LocationPlotter
{
    public partial class MapForm : Form
    {
        public string title = "Map Plotter";
        string url = @"http://developer.kensnz.com/getlocdata";
        HttpClient client = new HttpClient();
        public List<InterestingPlace> places;
        public List<InterestingPlace> CustomPlaces;
        GMapOverlay overlay;

        public string json = string.Empty;
        PeriodicTimer timer;
        FilterForm FilterForm;
        SubmitMarkerForm secret;
        // Filter Arguments for places list
        public InterestingPlaceOptions options = new();
        private bool doRepeat = false;
        public System.Data.DataTable table = new System.Data.DataTable();
        double currentLongitude;
        double currentLatitude;
        Random random = new Random();
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

            overlay = new GMapOverlay("markers");
            myMap.Overlays.Add(overlay);

            RefreshMarkers();
            myMap.ContextMenuStrip = contextMenuStrip;
        }
        private async Task<List<InterestingPlace>> GetPlacesOfInterest()
        {
            string newjson = string.Empty;
            newjson = await client.GetStringAsync(url);
            if (json.Length == newjson.Length) return places; // if no change, dont reprocess
            else json = newjson;
            JArray jerry = JArray.Parse(newjson);
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
                "Controls\n" +
                "Left-click drag on the map will pan the map, scrolling zooms the map in and out.\n" +
                "Actions and filtering are done through the right-click context menu anywhere on the form.\n" +
                "Middle Click submits a point ;)",
                icon: MessageBoxIcon.Question,
                buttons: MessageBoxButtons.OK
                );
        }

        private void refreshMarkersToolStripMenuItem_Click(object sender, EventArgs e)
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
                "Technologies used:\n" +
                ".NET 6, Windows Forms, Visual Studio 2022\n" +
                "Nuget Packages used:\n" +
                "Newtonsoft.JSON\n" +
                "GMap.NET.WinForms",
                icon: MessageBoxIcon.Question,
                buttons: MessageBoxButtons.OK
                );
        }
        public async void RefreshMarkers()
        {
            overlay.Markers.Clear();
            places = await GetPlacesOfInterest();
            table.Rows.Clear();
            CustomPlaces = (from place in places
                            where place.ID >= options.IDMin && place.ID <= options.IDMax &&
                            place.Created_At >= options.CreatedMin && place.Created_At <= options.CreatedMax &&
                            place.Latitude >= options.LatMin && place.Latitude <= options.LatMax &&
                            place.Longitude >= options.LongMin && place.Longitude <= options.LongMax
                            select place).OrderByDescending(p => p.Created_At).ToList();
            // Conditional Filtering
            if (options.LimitResults > 0) CustomPlaces = CustomPlaces.Take(options.LimitResults).ToList();
            if (options.UserFilter.Count > 0) CustomPlaces = CustomPlaces.Select(p => p).Where(p=>options.UserFilter.Contains( p.UserID)).ToList();
            if (options.UniqueResults) CustomPlaces = CustomPlaces.Distinct().ToList();
            RefreshTable(CustomPlaces);


            int tempUserId = int.MinValue+3;
            GMarkerGoogleType gMarker = (GMarkerGoogleType)1;
            Dictionary<int, List<PointLatLng>> PlacesByPeople = new Dictionary<int, List<PointLatLng>>();
            foreach (var place in CustomPlaces.OrderBy(p=>p.UserID).ToList())
            {
                // If new person encountered
                if (tempUserId != place.UserID)
                {
                    // Keep track of them
                    tempUserId = place.UserID;
                    // Give them a new marker
                    gMarker++;
                    // Create a dictionary entry for them to keep track of their points so we can make hulls out of them later.
                    PlacesByPeople.Add(tempUserId, new List<PointLatLng>());
                }

                var point = new PointLatLng(place.Latitude, place.Longitude);
                if (gMarker == GMarkerGoogleType.white_small) gMarker = GMarkerGoogleType.arrow;
                var marker = new GMarkerGoogle(point, gMarker);
                marker.ToolTipText = place.ToString();
                // Put the marker on the map
                overlay.Markers.Add(marker);
                // Put the point of the marker in the dictionary
                PlacesByPeople[tempUserId].Add(point);
            }
            DrawHulls(PlacesByPeople);
        }

        private void DrawHulls(Dictionary<int, List<PointLatLng>> placesByPeople)
        {
            overlay.Polygons.Clear();
            foreach (var keyvaluepair in placesByPeople)
            {
                if (keyvaluepair.Value.Count > 1)
                {
                    var hull = HullHelper.CalculateHull(keyvaluepair.Value);
                    // Debug hulls by generating a similar point beside them named with what order in the list it is
                    //for (int i = 0; i < hull.Count; i++)
                    //{
                    //    PointLatLng item = hull[i];
                    //    markers.Markers.Add(new GMarkerGoogle(new PointLatLng(item.Lat - .00005, item.Lng - .00005), GMarkerGoogleType.purple_dot) { ToolTipText=$"I am point {i}"});
                    //}

                    GMapPolygon polygon = new GMapPolygon(hull, keyvaluepair.Key.ToString());
                    polygon.Stroke = new Pen(Color.FromArgb(random.Next(0,255),random.Next(0, 255),random.Next(0, 255)),options.PenWidth);
                    polygon.Tag = GetAreaFromHull(hull);
                    overlay.Polygons.Add(polygon);
                }
            }
        }

        private double GetAreaFromHull(List<PointLatLng> hull)
        {
            static double ConvertToRadian(double input)
            {
                return input * Math.PI / 180;
            }
            double area = 0;

            if (hull.Count > 2)
            {
                for (var i = 0; i < hull.Count - 1; i++)
                {
                    PointLatLng p1 = hull[i];
                    PointLatLng p2 = hull[i + 1];
                    area += ConvertToRadian(p2.Lng - p1.Lng) * (2 + Math.Sin(ConvertToRadian(p1.Lat)) + Math.Sin(ConvertToRadian(p2.Lat)));
                }

                area = area * 6378137 * 6378137 / 2;
            }

            return Math.Abs(area);
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
                List<InterestingPlace> newplaces;
                if (!doRepeat) continue;
                try
                {
                    newplaces = await GetPlacesOfInterest();
                    if (places == newplaces) continue;
                    else
                    {
                        places = newplaces;
                        RefreshMarkers();
                    }
                }
                catch (Exception)
                {
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

        private void myMap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {

                if(secret ==null) secret =new SubmitMarkerForm(client: client);
                secret.SetLatLog(lat: currentLatitude, log: currentLongitude);
                secret.ShowDialog();
            }
        }

        private void MapForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Dispose();
            client.CancelPendingRequests();
            client.Dispose();
        }
        
        // Well, I tried to get info of a polygon working on click, and on leave, but no event seems to get triggered even though ive configured them to on the map control.
        // Do I get marks for trying?
        private void myMap_OnPolygonClick(GMapPolygon item, MouseEventArgs e)
        {
            if ((double)item.Tag == 0) 
                MessageBox.Show($"Polygon has no area, it is a straight line. Its distance is {item.Distance}.");
            else
                MessageBox.Show($"Polygon area size is {item.Tag}, distance is {item.Distance}");
        }

        private void myMap_OnPolygonLeave(GMapPolygon item)
        {
            if ((double)item.Tag == 0)
                MessageBox.Show($"Polygon has no area, it is a straight line. Its distance is {item.Distance}.");
            else
                MessageBox.Show($"Polygon area size is {item.Tag}, distance is {item.Distance}");
        }
    }
}