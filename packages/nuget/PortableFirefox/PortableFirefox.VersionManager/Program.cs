using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

string patchSuffixVariable = Environment.GetEnvironmentVariable("PATCH_VERSION_SUFFIX") ?? "1";

if (patchSuffixVariable.Contains('0'))
{
    throw new("PATCH_VERSION_INCREMENT can not container zeros.");
}

int patchSuffix = int.Parse(patchSuffixVariable);

using HttpClient client = new();
client.DefaultRequestHeaders.Add("User-Agent", "DotNet-HttpClient");

using HttpRequestMessage getFirefoxLatestVersionMessage = new(HttpMethod.Get,
    "https://download.mozilla.org/?product=firefox-latest-ssl&os=linux64&lang=en-US");

using HttpResponseMessage firefoxRes =
    await client.SendAsync(getFirefoxLatestVersionMessage, HttpCompletionOption.ResponseHeadersRead);

if (firefoxRes.RequestMessage?.RequestUri is null)
{
    throw new("Response -> Request message or request uri is null.");
}

string archive = firefoxRes.RequestMessage.RequestUri.AbsolutePath.Split("/").Last();
string archiveFilename = archive.Split(".tar").First();
Version firefoxVersion = Version.Parse(archiveFilename.Split("-").Last());

await SetOutputAsync("FIREFOX_VERSION", firefoxVersion.ToString());

Version nugetPackageVersion = new(firefoxVersion.Major, firefoxVersion.Minor,
    firefoxVersion.Build == -1
        ? patchSuffix
        : firefoxVersion.Build * Convert.ToInt32(Math.Pow(10, Convert.ToInt32(Math.Log10(patchSuffix)) + 2)) +
          patchSuffix);

await SetOutputAsync("NUGET_PACKAGE_VERSION", nugetPackageVersion.ToString());

JsonObject geckodriverRelease =
    await client.GetFromJsonAsync<JsonObject>("https://api.github.com/repos/mozilla/geckodriver/releases/latest")
    ?? throw new("Failed to get geckodriver latest release.");

string tagName = geckodriverRelease["tag_name"]?.ToString()
                 ?? throw new("Failed to get release tag_name");

Version geckodriverVersion = Version.Parse(tagName[1..]);

await SetOutputAsync("GECKODRIVER_VERSION", geckodriverVersion.ToString());

async Task SetOutputAsync(string key, string value)
{
    Console.WriteLine($"Setting output {key}={value}");
    if (Environment.GetEnvironmentVariable("GITHUB_OUTPUT") is not { } githubOutputFile)
    {
        return;
    }

    await File.AppendAllLinesAsync(githubOutputFile, [$"{key}={value}"]);
}