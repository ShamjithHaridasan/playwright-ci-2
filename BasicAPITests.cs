using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

public class BasicApiTest : PlaywrightTest
{
    private IAPIRequestContext requestContext = null!;

    [SetUp]
    public async Task Setup()
    {
        await CreateAPIRequestContext();
    }

    [Test]
    public async Task GetWeatherDataTest()
    {
        var response = await requestContext.GetAsync("weatherforecast");
        await Expect(response).ToBeOKAsync();
        var responseJSON = await response.JsonAsync();

        Assert.That(responseJSON, Is.Not.Null);
    }

    public async Task CreateAPIRequestContext()
    {
        requestContext = await Playwright.APIRequest.NewContextAsync(new()
        {
            BaseURL = "http://localhost:5201/"
        });
    }

    [TearDown]
    public async Task TearDown()
    {
        await requestContext.DisposeAsync();
    } 
}