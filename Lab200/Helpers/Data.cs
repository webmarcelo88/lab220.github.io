namespace Lab200.Helpers;

public static class Data
{
    public static Dictionary<int, string> Orientations { get; set; } = new Dictionary<int, string>()
    {
        {1,"Horizontal (1366 x 768)" },
        {2," Vertical ( 768 x 1366)" }
    };
    public static string TOKEN = "lab220_token";
    public static string EXPIRES = "lab220_expires";
}
