using CliWrap;
using CliWrap.Buffered;

namespace PortableFirefox.Test;

public class PortableFirefoxTest
{
    [Fact]
    public async Task Should_Return_Non_Null_Version()
    {
        var res = await Cli.Wrap(Path.Join((ReadOnlySpan<char>) Path.Join((ReadOnlySpan<char>) AppDomain.CurrentDomain.BaseDirectory, (ReadOnlySpan<char>) "portable-firefox-116.0.2", (ReadOnlySpan<char>) "firefox-root"), (ReadOnlySpan<char>) "firefox-app", "firefox.run"))
            .WithArguments("--version")
            .WithValidation(CommandResultValidation.None)
            .ExecuteBufferedAsync();
        
        Assert.Equal(0, res.ExitCode);
        Assert.NotEmpty(res.StandardOutput);
    }
}