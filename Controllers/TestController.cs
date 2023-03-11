using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;

namespace ExcelAuthTest.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;
    private readonly GraphServiceClient _graphServiceClient;

    public TestController(ILogger<TestController> logger, GraphServiceClient graphServiceClient)
    {
        _logger = logger;
        _graphServiceClient = graphServiceClient;
    }

    [HttpGet]
    [Route("{site}")]
    public async Task<Site> Get(string site)
    {
        var tenant = "amsuni"; // insert your tenant name here
        var test = await _graphServiceClient.Sites[$"{tenant}.sharepoint.com:/sites/{site}"].Request().GetAsync();

        return test;
    }
}