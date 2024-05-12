using System.Runtime.InteropServices;

namespace PortableFirefox;

public partial class PortableFirefoxInstance
{
    public static string FirefoxPath => Path.Join(Path.Join(AppDomain.CurrentDomain.BaseDirectory,
        "portable-firefox-" + VERSION + VERSION_SUFFIX,
        "firefox-root"), BinSubDir, "firefox.run");

    public static string GeckoDriverDir => Path.Join(Path.Join(AppDomain.CurrentDomain.BaseDirectory,
        "portable-firefox-" + VERSION + VERSION_SUFFIX,
        "firefox-root"), BinSubDir);

    private static string BinSubDir =>
        RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
            ? "firefox-app"
            : RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                ? Path.Join("Firefox.app", "Contents", "MacOS")
                : throw new("Unsupported platform.");
}