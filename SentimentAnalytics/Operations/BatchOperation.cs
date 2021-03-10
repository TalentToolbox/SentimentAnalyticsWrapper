using SentimentAnalytics.Models;
using Azure.AI.TextAnalytics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SentimentAnalytics.Operations
{
    public class BatchOperation : AnalyticOperation
    {
        public BatchOperation() { }
        public BatchOperation(IEnumerable<Document> documents)
        {
            AddDocuments(documents);
        }

        public void AddDocument(Document document)
        {
            Documents.Add(document);
        }

        public void AddDocuments(IEnumerable<Document> documents)
        {
            Documents.AddRange(documents);
        }

        internal override void Analyse(TextAnalyticsClient client)
        {
            if (!Documents.Any())
                throw new InvalidOperationException("No documents have been added to the Batch Operation");

            foreach (AnalyzeSentimentResult sentimentInDocument in client.AnalyzeSentimentBatch(Documents.Select(d => d.Text)).Value)
            {
                int index = int.Parse(sentimentInDocument.Id);

                if (sentimentInDocument.HasError)
                {
                    Documents[index].SetErrorMessage(sentimentInDocument.Error.Message);
                }
                else
                {
                    Documents[index].SetResults(sentimentInDocument.DocumentSentiment);
                }
            }
        }
    }

}
