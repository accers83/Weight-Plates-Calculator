# Weight-Plates-Calculator
Portfolio project. Weight-Plates-Calculator provides an offline available web solution for weight plate calculation, including local caching of personalised configuration.

## Key features
The Weight-Plates-Calculator demonstrates use of the following key features.
- .NET 8 [Blazor WebAssembly Progressive Web App](../main/WeightPlatesCalculator.Web).
- App hosted on [Azure Static Web Apps](https://kind-glacier-09aaf6e03.6.azurestaticapps.net/).
- Continuous deployment from GitHub repository to Azure Static Web Apps via [GitHub Actions](../../../Weight-Plates-Calculator/actions/workflows/azure-static-web-apps-kind-glacier-09aaf6e03.yml).
- Recursion and backtracking for [weight plates calculation](../main/WeightPlatesCalculatorLibrary/Processors/WeightPlatesProcessor.cs).
- Storage of user customised values on local device using [Blazored LocalStorage](../main/WeightPlatesCalculator.Web/Pages/Home.razor.cs).

## Minimal viable product
The current version of the app represents the planned MVP, meeting the following requirements.
- User inputs target weight to calculate.
- User selects lifting device type (single / double ended).
- User can customise count of weight plates available for a fixed set of metric weights.
- User can customise the maximum number of weight plates per end on each lifting device type.
- Custom values stored on user's device in local storage (requires user consent).
- App calculates lowest number of plates required for target weight and displays.
	
## Possible future versions
The following features were not included in the MVP, but are under consideration for later addition. 
- Option for user to add additional weight values.
- Option for user to take into account (and specify) weight of lifting device.
- Option for user to select imperial units.
- App to display nearest attainable weight if target can't be reached.
- App to retain history of previous calculations.

