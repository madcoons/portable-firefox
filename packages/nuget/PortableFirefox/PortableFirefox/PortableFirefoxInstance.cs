using System.Runtime.InteropServices;

namespace PortableFirefox;

public partial class PortableFirefoxInstance
{
    public static string FirefoxPath => Path.Join(AbsoluteRootPath, BinSubDir, FirefoxBinName);

    public static string GeckoDriverDir => Path.Join(AbsoluteRootPath, BinSubDir);

    private static string AbsoluteRootPath
        => Path.Join(AppDomain.CurrentDomain.BaseDirectory, "portable-firefox-" + VERSION + VERSION_SUFFIX);

    private static string BinSubDir
        => RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
            ? Path.Join("firefox-root", "firefox-app")
            : RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                ? Path.Join("Firefox.app", "Contents", "MacOS")
                : throw new("Unsupported platform.");

    private static string FirefoxBinName
        => RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
            ? "firefox.run"
            : RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                ? "firefox"
                : throw new("Unsupported platform.");
}