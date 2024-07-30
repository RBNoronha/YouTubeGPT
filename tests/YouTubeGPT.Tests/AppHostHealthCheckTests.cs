using YouTubeGPT.Shared;

namespace YouTubeGPT.Tests;

public class AppHostHealthCheckTests : AspireTest
{
    [Test]
    public async Task ClientAppStartsAndRespondsWithOk()
    {
        var httpClient = _app.CreateHttpClient(ServiceNames.YouTubeGPTClient);
        await _resourceNotificationService.WaitForResourceAsync(ServiceNames.YouTubeGPTClient, KnownResourceStates.Running).WaitAsync(TimeSpan.FromSeconds(30));
        var response = await httpClient.GetAsync("/");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task IngestionAppStartsAndRespondsWithOk()
    {
        var httpClient = _app.CreateHttpClient(ServiceNames.YouTubeGPTIngestion);
        await _resourceNotificationService.WaitForResourceAsync(ServiceNames.YouTubeGPTIngestion, KnownResourceStates.Running).WaitAsync(TimeSpan.FromSeconds(30));
        var response = await httpClient.GetAsync("/");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}
