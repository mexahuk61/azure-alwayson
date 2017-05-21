# Azure AlwaysOn Handler

### NuGet Package

```
Install-Package AzureAlwaysOnHandler
```


### Usage

In Startup.cs, in Configure, add:
```
app.UseAzureAlwaysOnHandler();
```
This middleware should be added before all other.