Politics and War API Wrapper

Using Directives:
```csharp
using PnW.Client;
using PnW.Models;
```
Example query:
```csharp
var pnw = new PnWClient(apiKey);
var city = await pnw.Query.GetQueryAsync<City>(targetCityId, new() { "name", "barracks", "factory", "hangar", "drydock" });
```

Example mutation:
```csharp
BankDepositInput newDeposit = new BankDepositInput();
newDeposit.Money = 100000;
newDeposit.Note = "safekeeping";

var pnw = new PnWClient(apiKey, botKey, botKeyApiKey);
var deposit = await pnw.BankDeposit.DepositAsync(newDeposit);
```
