internal class Program
{
    private static void Main(string[] args)
    {
        double lat = 55.7805983; //y
        double lon = 49.09003; //x
        int zoom = 11;
        string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        string? savedFile = Yandex.Helper.GetCoverageMap(lat, lon, zoom, "2g", dir);
        double result = CoverageBitmap.GetCoveragePercentage(savedFile);
        Console.WriteLine($"Coverage is {result*100:F3}%");
    }

}