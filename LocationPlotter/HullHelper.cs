using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationPlotter
{
    static class HullHelper
    {
        
        public static List<PointLatLng> CalculateHull(List<PointLatLng> points)
        {
            var pivot = points.Aggregate((a, b) => a.Lng < b.Lng ? a : a.Lng == b.Lng && a.Lat < b.Lat ? a : b);
            points.Remove(pivot);
            points.Sort(new RadialSorter(pivot));
            //points.Reverse();
            var hull = new List<PointLatLng>() { pivot, points[0] };
            points.RemoveAt(0);

            while (points.Count > 0)
            {
                hull.Add(points[0]);
                points.RemoveAt(0);
                while (!Validate(hull))
                {
                    hull.RemoveAt(hull.Count - 2);
                }
            }
            return hull;
        }

        private static bool Validate(List<PointLatLng> hull)
        {
            return hull.Count < 3 ||
                GetSignedArea(hull[hull.Count - 3], hull[hull.Count - 2], hull[hull.Count - 1]) > 0;
        }

        private static double GetSignedArea(PointLatLng a, PointLatLng b, PointLatLng c)
        {
            return 
                a.Lng * b.Lat - b.Lng * a.Lat + 
                b.Lng * c.Lat - c.Lng * b.Lat +
                c.Lng * a.Lat - a.Lng * c.Lat;
        }
        private class RadialSorter : IComparer<PointLatLng>
        {
            public PointLatLng Pivot { get; set; }
            public RadialSorter(PointLatLng pivot)
            {
                Pivot = pivot;
            }
            public int Compare(PointLatLng a, PointLatLng b)
            {
                double cmp = -GetSignedArea(Pivot,a,b);
                return cmp > 0 ? 1 : cmp < 0 ? -1 : 0;
            }
        }
    }
}
