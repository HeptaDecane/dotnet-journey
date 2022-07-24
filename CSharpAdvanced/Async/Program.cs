using System.Diagnostics;

namespace Async {
    class Program {
        static async Task Main() {
            
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            
            string urlBase = "https://raw.githubusercontent.com/HeptaDecane/dotnet-journey/main/";
            var spongebobTask = readFromUrl(urlBase + "spongebob.txt");
            var squidwardTask = readFromUrl(urlBase + "squidward.txt");

            // await Task.WhenAny(squidwardTask, spongebobTask);
            // await Task.WhenAll(squidwardTask, spongebobTask);
            
            string spongebob = await spongebobTask;
            string squidward = await squidwardTask;
            
            stopwatch.Stop();

            Console.WriteLine(squidward);
            Console.WriteLine(spongebob);
            Console.WriteLine($"time elapsed: {stopwatch.Elapsed.TotalSeconds}s");
        }

        static async Task<string> readFromUrl(string url) {
            Console.WriteLine($"start: {url}");
            var http = new HttpClient();
            string text = await http.GetStringAsync(url);
            Console.WriteLine($"end: {url}");
            return text;
        }
    }
    
    
}