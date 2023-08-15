using CliWrap;
using CliWrap.Buffered;

namespace PortableFirefox.Test;

public class PortableFirefoxTest
{
    [Fact]
    public async Task Should_Return_Non_Null_Version()
    {
        var res = await Cli.Wrap(PortableFirefoxInstance.FirefoxPath)
            .WithArguments("--version")
            .WithValidation(CommandResultValidation.None)
            .ExecuteBufferedAsync();
        
        Assert.Equal(0, res.ExitCode);
        Assert.NotEmpty(res.StandardOutput);
    }
}