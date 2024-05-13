namespace PortableFirefox;

public partial class PortableFirefoxInstance
{
    public static string FirefoxPath => Path.Join(AbsoluteRootPath, BinSubDir, FirefoxBinName);

    public static string GeckoDriverDir => Path.Join(AbsoluteRootPath, BinSubDir);

    private static string AbsoluteRootPath
        => Path.Join(AppDomain.CurrentDomain.BaseDirectory, "portable-firefox-" + VERSION + VERSION_SUFFIX);

    private static string BinSubDir
        => OperatingSystem.IsLinux()
            ? Path.Join("firefox-root", "firefox-app")
            : OperatingSystem.IsMacOS()
                ? Path.Join("Firefox.app", "Contents", "MacOS")
                : throw new("Unsupported platform.");

    private static string FirefoxBinName
        => OperatingSystem.IsLinux()
            ? "firefox.run"
            : OperatingSystem.IsMacOS()
                ? "firefox"
                : throw new("Unsupported platform.");
}