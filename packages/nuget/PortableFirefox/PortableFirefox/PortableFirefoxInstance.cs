namespace PortableFirefox;

public partial class PortableFirefoxInstance
{
    public static string FirefoxPath => Path.Join(Path.Join(AppDomain.CurrentDomain.BaseDirectory,
        "portable-firefox-" + VERSION,
        "firefox-root"), "firefox-app", "firefox.run");
    
    public static string GeckoDriverPath => Path.Join(Path.Join(AppDomain.CurrentDomain.BaseDirectory,
        "portable-firefox-" + VERSION,
        "firefox-root"), "firefox-app", "geckodriver");
}