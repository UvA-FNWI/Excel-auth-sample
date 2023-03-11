# Excel and ASP.NET Core Web API

This is an example project based on `Microsoft.Identity.Web` that demonstrates a Web API that
1. can be used in the "From Web" PowerQuery option in Excel with Azure Active Directory authentication, and
2. uses the provided credentials to call the Graph API.

## Setup
This needs an app registration in Azure with
- API permissions for the Graph calls that you want to do (with appropriate consent for your tenant)
- A configured Application ID URI (under "Expose an API" in app registrations) that matches the `Uri` setting in the config
- An authorized client application (same tab) of `d3590ed6-52b3-4102-aeff-aad2292ab01c` (Microsoft Office) with `user_impersonation` scope
- A client ID, secret and tenant ID set up in the `AzureAd` configuration section

## Usage
The sample can be used as follows:
1. Ensure that the configured application uri resolves to localhost (e.g. by adding it in your `hosts` file)
2. In Excel choose "Data" -> "From Web"
3. Enter the chosen uri with an appropriate endpoint (in the sample, `/Test/{siteName}` should get info for a SharePoint site)
4. You should be asked to log in using your AAD credentials