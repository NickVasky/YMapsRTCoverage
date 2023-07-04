using OpenCvSharp;

public class CoverageBitmap
{
    public static double GetCoveragePercentage(string? fileName)
    {
        if (String.IsNullOrEmpty(fileName))
        {
            Console.WriteLine($"No coverage map file. Coverage is 0%");
            return 0.0;
        }
        Console.WriteLine($"Calculating coverage percentage...");
        Mat src = new Mat(fileName, ImreadModes.Grayscale);
        int totalPixels = src.Rows * src.Cols;
        int numOfColoredPixels = src.CountNonZero();
        src.Dispose();
        System.IO.File.Delete(fileName);
        return (double)numOfColoredPixels/(double)totalPixels;
    }
}