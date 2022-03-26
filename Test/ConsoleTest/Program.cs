using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace ConsoleTest
{
    class Program
    {
        private const string url = @"https://github.com/CSSEGISandData/COVID-19/raw/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";
        private const string filePath = @"d:\Programming\VS2019Projects\CS\Wpf\CV19Lab\time_series_covid19_confirmed_global.csv";

        #region Internet
        private enum EField : Int16
        {
            Province = 0,
            Country = 1,
            Lat = 2,
            Lon = 3
        }

        private static async Task<Stream> GetDataStreamNet()
        {
            var client = new HttpClient();
            var resp = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            return await resp.Content.ReadAsStreamAsync();
        }


        private static IEnumerable<string> GetDataLinesNet()
        {
            using var data_stream = GetDataStreamNet().Result;
            using var sr = new StreamReader(data_stream);

            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                if (string.IsNullOrEmpty(line)) continue;
                yield return line.Replace("Bonaire,", "Bonaire -")
                                        .Replace("Helena,", "Helena -")
                                        .Replace("Korea,", "Korea -");
            }
        }


        private static DateTime[] GetDatesNet() => GetDataLinesNet().First()
                                                                 .Split(',')
                                                                 .Skip(4)
                                                                 .Select(d => DateTime.Parse(d, System.Globalization.CultureInfo.InvariantCulture))
                                                                 .ToArray();

        private static IEnumerable<(string Country, string Province, double Lat, double Lon, int[] Counts)> GetDataNet()
        {
            var lines = GetDataLinesNet().Skip(1)
                                         .Select(l => l.Split(','));

            foreach (var line in lines)
            {
                var province = line[(int)EField.Province].Trim(' ', '"') ?? string.Empty;
                var country = line[(int)EField.Country].Trim(' ', '"');
                var lat = string.IsNullOrEmpty(line[(int)EField.Lat]) ? 0d : double.Parse(line[(int)EField.Lat].Replace('.', ','));
                var lon = string.IsNullOrEmpty(line[(int)EField.Lon]) ? 0d : double.Parse(line[(int)EField.Lon].Replace('.', ','));
                var counts = line.Skip(4)
                                 .Select(i => int.Parse(i))
                                 .ToArray();

                yield return (country, province, lat, lon, counts);
            }
        }

        #endregion Internet


        #region File

        private static FileStream GetDataStream()
        {
            return File.OpenRead(filePath);
        }

        private static IEnumerable<string> GetDataLines()
        {
            using var sr = new StreamReader(GetDataStream());

            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                if (string.IsNullOrEmpty(line)) continue;

                yield return line.Replace("Bonaire,", "Bonaire -")
                                        .Replace("Helena,", "Helena -")
                                        .Replace("Korea,", "Korea -");
            }
        }


        private static DateTime[] GetDates() => GetDataLines().First()
                                                              .Split(',')
                                                              .Skip(4)
                                                              .Select(d => DateTime.Parse(d, System.Globalization.CultureInfo.InvariantCulture))
                                                              .ToArray();

        private static IEnumerable<(string Country, string Province, double Lat, double Lon, int[] Counts)> GetData()
        {
            var rows = GetDataLines().Skip(1)
                                      .Select(line => line.Split(','));

            foreach (var row in rows)
            {
                var country = row[(int)EField.Country].Trim(' ', '"');
                var province = row[(int)EField.Province].Trim(' ', '"') ?? string.Empty;
                var lat = string.IsNullOrEmpty(row[(int)EField.Lat]) ? 0d : double.Parse(row[(int)EField.Lat].Replace('.', ','));
                var lon = string.IsNullOrEmpty(row[(int)EField.Lon]) ? 0d : double.Parse(row[(int)EField.Lon].Replace('.', ','));
                var counts = row.Skip(4)
                                .Select(i => int.Parse(i))
                                .ToArray();

                yield return (country, province, lat, lon, counts);
            }
        }

        #endregion File


        static void Main(string[] args)
        {

            #region  Internet    
            //Console.WriteLine(string.Join("\r\n", GetDataLinesNet()));
            //Console.WriteLine(string.Join("\r\n", GetDatesNet()));

            //var canada_data = GetDataNet().First(x => x.Country.Equals("Canada", StringComparison.OrdinalIgnoreCase));

            //Console.WriteLine(string.Join("\r\n", GetDatesNet().Zip(canada_data.Counts, (data, count) => $"{data:dd.MM.yyyy}: {count}")));
            #endregion Internet    


            #region File

            //Console.WriteLine(string.Join("\r\n", GetDates()));

            var russia_data = GetData().First(c => c.Country.Equals("Russia", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine(string.Join("\r\n", GetDates().Zip(russia_data.Counts, (date, count)=>$"{date:dd.MM.yyyy}: {count}"  )));

            #endregion File

            Console.ReadKey();
        }
    }
}
