# TextAnalyticsWrapper
 
## Setup
Set up the `Analytics` object with the `endpoint` and `apiKey` for your Azure Cognitive Service, then inject the `Analytics` object into your service as a singleton.
It's recommended that you store your ApiKey and Endpoint in the AzureKeyVault or another secure store.

```
public static void Configure(IServiceCollection services, IConfiguration configuration)
{
    services.AddSingleton<IAnalytics>(GetConfiguredAnalytics(configuration));
}

private static Analytics GetConfiguredAnalytics(IConfiguration configuration)
{
    var apikey = configuration["apiKey"];
    var endpoint = configuration["endpoint"];
    return new Analytics(endpoint, apikey);
}
```

## Usage
Specifiy your strategy by creating either a `BatchOperation()` or a `SingleOperation()` and populating the operation with the document/s to analyise.

**Single Document**

```csharp
var documentA = new Document(documentAText);
var singleOperation = new SingleOperation(documentA);
var results = _analytics.GetResults(singleOperation);
```

**Multiple Documents**

```csharp                 
var documentA = new Document(documentAText);
var documentB = new Document(documentBText);
var documentC = new Document(documentCText);

var documentList = new List<Document> { documentA, documentB };
var batchOperation = new BatchOperation(documentList);
batchOperation.AddDocument(documentC);
var batchResults = _analytics.GetResults(batchOperation);
```
