using LiteDb.VersionChecker;
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter path to LiteDB file:");
        var path = Console.ReadLine();

        try
        {
            string version = LiteDbVersionDetector.DetectLiteDbVersion(path);
            Console.WriteLine($"Detected version: {version}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
