using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
private static readonly HttpClient client = new HttpClient();
    private const string SiteKey = "6LdOQoUqAAAAAKRCt5axnH1bSAgxLCGZ-RwAnBcz";
    private const string ReCaptchaUrl = "https://www.google.com/recaptcha/api/siteverify";
    private const string ApiBaseUrl = "https://vinn-web-tools-dandys-projects-bb4af0ab.vercel.app";

   public static string[] woahstrings = { "I JUST SENT A THREAT TO THE WHITE HOUSE!!", "99 PERCENT OF GAMBELERS QUIT RIGHT BEFORE THEY HIT IT BIG?", "Im afread your a pinned paste..", "See my Botnet back there?", "Home Address here chat", "This tool was made by lucian <3", "I LIKE APPLES AND BANNANAS!!" };

    public static int[] numbersArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    static async Task Main(string[] args)
    {
        Console.WriteLine("Welcome To..");
        Thread.Sleep(1000);
         
        Console.WriteLine("██╗░░░░░██╗░░░██╗░█████╗░██╗░█████╗░███╗░░██╗██╗░░░░░░█████╗░███╗░░██╗██████╗░");
        Console.WriteLine("██║░░░░░██║░░░██║██╔══██╗██║██╔══██╗████╗░██║██║░░░░░██╔══██╗████╗░██║██╔══██╗");
        Console.WriteLine("██║░░░░░██║░░░██║██║░░╚═╝██║███████║██╔██╗██║██║░░░░░███████║██╔██╗██║██║░░██║");
        Console.WriteLine("██║░░░░░██║░░░██║██║░░██╗██║██╔══██║██║╚████║██║░░░░░██╔══██║██║╚████║██║░░██║");
        Console.WriteLine("███████╗╚██████╔╝╚█████╔╝██║██║░░██║██║░╚███║███████╗██║░░██║██║░╚███║██████╔╝");
        Console.WriteLine("╚══════╝░╚═════╝░░╚════╝░╚═╝╚═╝░░╚═╝╚═╝░░╚══╝╚══════╝╚═╝░░╚═╝╚═╝░░╚══╝╚═════╝░");
        Console.WriteLine("                        Made with love by lucian <3                           ");

        Thread.Sleep(1000);

        Console.Clear();

        Console.WriteLine("Select an option:");
        Console.WriteLine("1. Load Steam Games From Lua");
        Console.WriteLine("2. Use Steam App ID Tool");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                LoadSteam();
                break;
            case "2":
                await SteamAppIdTool();
                break;
            default:
                Console.WriteLine("[Error] Invalid option selected.");
                break;
        }
    }

    static async Task LoadSteam()
    {
        // who tf think you are for me leaking this 
    }


    static async Task SteamAppIdTool()
    {
        Random random = new Random();
        int randomIndex = random.Next(woahstrings.Length);
        string randomString = woahstrings[randomIndex];
        Console.WriteLine($"{randomString}");

        Thread.Sleep(1000);

        Console.Clear();

      Console.WriteLine("Manifest & Lua Generator (ALPHA MAY NOT WORK)");
        Console.WriteLine("------------------------------------");
        Console.WriteLine("Enter your APP ID or URL:");

        string input = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("[Error] Input cannot be empty.");
            return;
        }

        string appId = ExtractAppId(input);
        if (string.IsNullOrEmpty(appId))
        {
            Console.WriteLine("[Error] Invalid APP ID or URL.");
            return;
        }

        Console.WriteLine($"[Information] Validating APP ID: {appId}");

        Console.WriteLine("[Information] Validating CAPTCHA...");
        string captchaToken = await SimulateCaptchaAsync(); 

        if (string.IsNullOrEmpty(captchaToken))
        {
            Console.WriteLine("[Error] CAPTCHA validation failed.");
            return;
        }

        Console.WriteLine("[Information] Sending request to generate manifest...");
        string result = await FetchAppDataAsync(appId, captchaToken);

        if (string.IsNullOrEmpty(result))
        {
            Console.WriteLine("[Error] Failed to fetch data.");
        }
        else
        {
            Console.WriteLine($"[Result] {result}");
        }

    }

    private static string ExtractAppId(string input)
    {
        if (long.TryParse(input, out _))
        {
            return input;
        }

        var appIdMatch = System.Text.RegularExpressions.Regex.Match(input, @"(?:app\/|agecheck\/app\/)(\d+)");
        return appIdMatch.Success ? appIdMatch.Groups[1].Value : null;
    }
    private static async Task<string> SimulateCaptchaAsync()
    {
        await Task.Delay(1000); 
        return "6LdOQoUqAAAAALl8JR66xmOfCfeFdZpK-qZk9nD4"; 
    }

    private static async Task<string> FetchAppDataAsync(string appId, string captchaToken)
    {
        try
        {
            var requestUrl = $"{ApiBaseUrl}/get-appid?appId={appId}&token={captchaToken}";
            var response = await client.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                Console.WriteLine($"[Error] API returned {response.StatusCode}: {response.ReasonPhrase}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error] {ex.Message}");
        }

        return null;
    }
}
