using Azure;
using Azure.AI.TextAnalytics;
using SentimentAnalytics.Models;
using SentimentAnalytics.Operations;
using System;
using System.Collections.Generic;

namespace SentimentAnalytics
{
    public class Analytics : IAnalytics
    {
        private readonly TextAnalyticsClient _textAnalyticsClient;

        // you can also authenticate with Azure Active Directory using the Azure Identity library.
        public Analytics(TextAnalyticsClient textAnalyticsClient)
        {
            _textAnalyticsClient = textAnalyticsClient;
        }

        public Analytics(string endpoint, string apiKey)
        {
            _textAnalyticsClient = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(apiKey));
        }

        public IEnumerable<Document> GetResults(AnalyticOperation operation)
        {
            operation.Analyse(_textAnalyticsClient);
            return operation.Documents;
        }
    }
}