namespace Yandex
{
    public static class Helper
    {
        public static int lat2tileY_Yandex(double lat, int z)//lat=широта в градусах, z=масштаб
        {
            lat = lat * Math.PI / 180.0; //радианы = градусы * ПИ / 180
            double a = 6378137;
            double k = 0.0818191908426;
            double z1 = Math.Tan(Math.PI / 4 + lat / 2) / Math.Pow(Math.Tan(Math.PI / 4 + Math.Asin(k * Math.Sin(lat)) / 2) , k);
            int pix_Y = (int)Math.Floor((20037508.342789 - a * Math.Log(z1)) * 53.5865938 / Math.Pow(2, 23 - z));
            return (pix_Y / 256);
        }
        public static int long2tileX_Yandex(double lon, int z)//lon=долгота в градусах, z=масштаб
        {
            lon = lon * Math.PI / 180.0; //радианы = градусы * ПИ / 180
            double a = 6378137;
            //double k = 0.0818191908426;
            int pix_X = (int)Math.Floor((20037508.342789 + a * lon) * 53.5865938 / Math.Pow(2.0 , 23 - z));
            return (pix_X / 256);
        }
        public static string? GetCoverageMap(double lat, double lon, int mapZoomLevel, string networkType, string saveDirectory)
        {
            Console.WriteLine($"Latitude:{lat}");
            Console.WriteLine($"Longitude:{lon}");

            int y = Yandex.Helper.lat2tileY_Yandex(lat, mapZoomLevel);
            int x = Yandex.Helper.long2tileX_Yandex(lon, mapZoomLevel);
            networkType = networkType.ToLower();
            
            string fileName = Path.Combine(saveDirectory, $@"{networkType}-{x}-{y}-{mapZoomLevel}.png");
            string webResource = $"https://rt-static-cdn.rt.ru/maps/{networkType}/{mapZoomLevel}/{x}/{y}.png";
            
            bool result;

            using (System.Net.WebClient webClient = new System.Net.WebClient())
            {
                // Download the Web resource and save it into the current filesystem folder.
                try
                {
                    Console.WriteLine($"Downloading coverage map...");
                    webClient.DownloadFile(webResource, fileName);
                    result = true; 
                    Console.WriteLine($"Coverage map downloaded!");

                }
                catch(Exception e)
                {
                    if ( System.IO.File.Exists(fileName) )
                        System.IO.File.Delete(fileName);
                    result = false;
                    Console.WriteLine($"No Coverage map for that area!");

                }
            }
            if (result)
                return fileName;
            else
                return null;
        }

    }

}