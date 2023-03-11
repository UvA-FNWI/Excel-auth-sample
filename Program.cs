using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration.GetSection("AzureAd");
var appUri = config["Uri"];
var clientId = config["ClientId"];
var tenantId = config["TenantId"];

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(
        opt =>
        {
            opt.Audience = appUri;
            opt.Events = new JwtBearerEvents
            {
                OnChallenge = _ =>
                {
                    opt.Challenge =
                        $@"Bearer  realm="""", client_id=""{appUri}"", trusted_issuers=""00000001-0000-0000-c000-000000000000@*"", token_types=""app_asserted_user_v1 service_asserted_app_v1"",  authorization_uri=""https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/authorize""";
                    return Task.CompletedTask;
                }
            };
        },
        opt =>
        {
            opt.Instance = config["Instance"];
            opt.ClientId = appUri;
            opt.TenantId = tenantId;
        })
    .EnableTokenAcquisitionToCallDownstreamApi(opt =>
    {
        opt.Instance = config["Instance"];
        opt.TenantId = tenantId;
        opt.ClientId = clientId;
        opt.ClientSecret = config["Secret"];
    })
    .AddMicrosoftGraph()
    .AddInMemoryTokenCaches();

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();