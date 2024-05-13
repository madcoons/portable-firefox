string patchSuffixVariable = Environment.GetEnvironmentVariable("PATCH_VERSION_SUFFIX") ?? "1";

if (patchSuffixVariable.Contains('0'))
{
    throw new("PATCH_VERSION_INCREMENT can not container zeros.");
}

int patchSuffix = int.Parse(patchSuffixVariable);

using HttpClient client = new();

using HttpRequestMessage getFirefoxLatestVersionMessage = new(HttpMethod.Get,
    "https://download.mozilla.org/?product=firefox-latest-ssl&os=linux64&lang=en-US");

using HttpResponseMessage firefoxRes =
    await client.SendAsync(getFirefoxLatestVersionMessage, HttpCompletionOption.ResponseHeadersRead);

if (firefoxRes.RequestMessage is null || firefoxRes.RequestMessage.RequestUri is null)
{
    throw new("Response -> Request message or request uri is null.");
}

string archive = firefoxRes.RequestMessage.RequestUri.AbsolutePath.Split("/").Last();
string archiveFilename = archive.Split(".tar").First();
Version firefoxVersion = Version.Parse(archiveFilename.Split("-").Last());

Console.WriteLine($"Setting Firefox version {firefoxVersion}...");
await SetOutputAsync("FIREFOX_VERSION", firefoxVersion.ToString());

Version nugetPackageVersion = new(firefoxVersion.Major, firefoxVersion.Minor,
    firefoxVersion.Build == -1
        ? patchSuffix
        : firefoxVersion.Build * Convert.ToInt32(Math.Pow(10, Convert.ToInt32(Math.Log10(patchSuffix)) + 2)) +
          patchSuffix);

Console.WriteLine($"Setting nuget package version {nugetPackageVersion}...");
await SetOutputAsync("NUGET_PACKAGE_VERSION", nugetPackageVersion.ToString());

async Task SetOutputAsync(string key, string value)
{
    if (Environment.GetEnvironmentVariable("GITHUB_OUTPUT") is not { } githubOutputFile)
    {
        return;
    }

    await File.AppendAllLinesAsync(githubOutputFile, [$"{key}={value}"]);
}