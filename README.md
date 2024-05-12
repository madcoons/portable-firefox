# Portable Firefox
This is portable firefox browser binary distributed as nuget package that doesn't require any installation on system.

It works on:
- [x] MacOS apple silicone
- [x] MacOS intel
- [x] Linux
- [ ] Windows

Example:
```cs
using CliWrap;
using CliWrap.Buffered;
using PortableFirefox;

var res = await Cli.Wrap(PortableFirefoxInstance.FirefoxPath)
    .WithArguments("--version")
    .WithValidation(CommandResultValidation.None)
    .ExecuteBufferedAsync();

Console.WriteLine($"Exit code: {res.ExitCode}");
Console.WriteLine($"Output: {res.StandardOutput}");
```
