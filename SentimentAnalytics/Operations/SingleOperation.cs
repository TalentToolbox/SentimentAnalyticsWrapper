using SentimentAnalytics.Models;
using Azure.AI.TextAnalytics;
using System;
using System.Linq;

namespace SentimentAnalytics.Operations
{
    public class SingleOperation : AnalyticOperation
    {
        private Document Document => Documents.Single();

        public SingleOperation(Document document)
        {
            Documents.Add(document);
        }

        internal override void Analyse(TextAnalyticsClient client)
        {
            if (!Documents.Any() || Documents.Any(d => d == null))
                throw new InvalidOperationException("A document to analyse has not been provided");

            if (string.IsNullOrEmpty(Document.Text))
                throw new InvalidOperationException("Text has not been set for the Document");

            Document.SetResults(client.AnalyzeSentiment(Document.Text));
        }
    }
}
