using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace CV19.Models
{
    internal struct ConfirmedCount
    {
        public DateTime Date { get; set; }
        public int Coubt { get; set; }
    }

    internal class PlaceInfo
    {
        public string Name { get; set; }
        public Point Location { get; set; }

        IEnumerable<ConfirmedCount> Counts { get; set; }
    }

    internal class CountryInfo : PlaceInfo
    {
        public IEnumerable<ProvinceInfo> Provinces { get; set; }
    }

    internal class ProvinceInfo:PlaceInfo
    {

    }
  
}
