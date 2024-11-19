using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    // Import necessary Windows API functions
    [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("kernel32.dll", EntryPoint = "GetConsoleWindow")]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
    private static extern bool SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);

    // Constants
    private const int GWL_EXSTYLE = -20;
    private const int WS_EX_LAYERED = 0x00080000;
    private const int LWA_COLORKEY = 0x00000001;
    private const int LWA_ALPHA = 0x00000002;

   public static string[] woahstrings = { "I JUST SENT A THREAT TO THE WHITE HOUSE!!", "99 PERCENT OF GAMBELERS QUIT RIGHT BEFORE THEY HIT IT BIG?", "Im afread your a pinned paste..", "See my Botnet back there?", "Home Address here chat", "This tool was made by lucian <3", "I LIKE APPLES AND BANNANAS!!" };

    public static int[] numbersArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    static async Task Main(string[] args)
    {
        IntPtr hWnd = GetConsoleWindow();
        int currentStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
        SetWindowLong(hWnd, GWL_EXSTYLE, currentStyle | WS_EX_LAYERED);

        // (bAlpha: 0 = fully transparent, 255 = fully opaque)
        byte transparency = 0;  
        SetLayeredWindowAttributes(hWnd, 0, transparency, LWA_ALPHA);
        Console.WriteLine("Welcome To..");
        Thread.Sleep(1000);
         
        Console.WriteLine("██╗░░░░░██╗░░░██╗░█████╗░██╗░█████╗░███╗░░██╗██╗░░░░░░█████╗░███╗░░██╗██████╗░");
        Console.WriteLine("██║░░░░░██║░░░██║██╔══██╗██║██╔══██╗████╗░██║██║░░░░░██╔══██╗████╗░██║██╔══██╗");
        Console.WriteLine("██║░░░░░██║░░░██║██║░░╚═╝██║███████║██╔██╗██║██║░░░░░███████║██╔██╗██║██║░░██║");
        Console.WriteLine("██║░░░░░██║░░░██║██║░░██╗██║██╔══██║██║╚████║██║░░░░░██╔══██║██║╚████║██║░░██║");
        Console.WriteLine("███████╗╚██████╔╝╚█████╔╝██║██║░░██║██║░╚███║███████╗██║░░██║██║░╚███║██████╔╝");
        Console.WriteLine("╚══════╝░╚═════╝░░╚════╝░╚═╝╚═╝░░╚═╝╚═╝░░╚══╝╚══════╝╚═╝░░╚═╝╚═╝░░╚══╝╚═════╝░");
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

    static void LoadSteam()
    {
     // you dont get ts fella
    }

    static async Task SteamAppIdTool()
    {
        Random random = new Random();
        int randomIndex = random.Next(woahstrings.Length);
        string randomString = woahstrings[randomIndex];
        Console.WriteLine($"{randomString}");

        Thread.Sleep(1000);

        Console.Clear();

        Console.WriteLine("Welcome to the Steam App ID Tool!");
        Console.WriteLine("Enter a Steam App ID or URL (or type 'exit' to quit):");

        while (true)
        {
            Console.Write("Input: ");
            string input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("[Error] Input cannot be empty.");
                continue;
            }

            if (input.ToLower() == "exit")
            {
                Console.WriteLine("Exiting the application. Goodbye!");
                break;
            }

            string appId = ExtractAppId(input);

            if (string.IsNullOrEmpty(appId))
            {
                Console.WriteLine("[Error] Invalid URL or App ID.");
                continue;
            }

            Console.WriteLine($"[Information] Finding App ID: {appId}");
            await FetchAppDetails(appId);
        }
    }

    static string ExtractAppId(string input)
    {
        if (Regex.IsMatch(input, @"^\d+$"))
        {
            return input;
        }

        var match = Regex.Match(input, @"(?:app\/|agecheck\/app\/)(\d+)");
        return match.Success ? match.Groups[1].Value : null;
    }

    static async Task FetchAppDetails(string appId)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                string apiUrl = $"https://vinn-web-tools-dandys-projects-bb4af0ab.vercel.app/get-appid?appid={appId}";
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[Error] Failed to fetch data. HTTP Status: {response.StatusCode}");
                    return;
                }

                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[Result] {result}");

                if (result.StartsWith("Successfully generated APP ID: "))
                {
                    string branchName = result.Replace("Successfully generated APP ID: ", "").Trim();

                    Console.WriteLine($"[Information] Preparing download for branch: {branchName}");
                    string downloadUrl = await FetchDownloadUrl(branchName);

                    if (!string.IsNullOrEmpty(downloadUrl))
                    {
                        Console.WriteLine($"[Download] URL: {downloadUrl}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] {ex.Message}");
            }
        }
    }

    static async Task<string> FetchDownloadUrl(string branchName)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                string downloadApiUrl = $"https://vinn-web-tools-dandys-projects-bb4af0ab.vercel.app/download?appid={branchName}";
                HttpResponseMessage response = await client.GetAsync(downloadApiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[Error] Failed to fetch download URL. HTTP Status: {response.StatusCode}");
                    return null;
                }

                string jsonResult = await response.Content.ReadAsStringAsync();
                dynamic resultData = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResult);

                return resultData?.url;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] {ex.Message}");
                return null;
            }
        }
    }
}
