using SentimentAnalytics.Models;
using Azure.AI.TextAnalytics;
using System.Collections.Generic;

namespace SentimentAnalytics.Operations
{
    public abstract class AnalyticOperation
    {
        public List<Document> Documents { get; set; } = new List<Document>();

        internal abstract void Analyse(TextAnalyticsClient client);
    }
}
