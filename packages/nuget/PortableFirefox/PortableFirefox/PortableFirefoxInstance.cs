namespace PortableFirefox;

public partial class PortableFirefoxInstance
{
    public string FirefoxPath => Path.Join(Path.Join(AppDomain.CurrentDomain.BaseDirectory,
        "portable-firefox-" + VERSION,
        "firefox-root"), "firefox");
}