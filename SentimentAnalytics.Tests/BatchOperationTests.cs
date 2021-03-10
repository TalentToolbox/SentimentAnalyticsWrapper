using Azure;
using Azure.AI.TextAnalytics;
using Moq;
using NUnit.Framework;
using SentimentAnalytics.Models;
using SentimentAnalytics.Operations;
using Shouldly;
using System;
using System.Collections.Generic;

namespace SentimentAnalytics.Tests
{
    [TestFixture]
    class BatchOperationTests
    {
        const string documentAText = @"The food and service were unacceptable, but the concierge were nice.
                    After talking to them about the quality of the food and the process
                    to get room service they refunded the money we spent at the restaurant and
                    gave us a voucher for nearby restaurants.";

        const string documentBText = @"Nice rooms! I had a great unobstructed view of the Microsoft campus but bathrooms
                    were old and the toilet was dirty when we arrived. It was close to bus stops and
                    groceries stores.
                    If you want to be close to campus I will recommend it, otherwise, might be
                    better to stay in a cleaner one";

        private Analytics _analytics;

        [SetUp]
        public void SetUp()
        {
            Mock<TextAnalyticsClient> _client = new Mock<TextAnalyticsClient>();

            var sentimentResponse = new Mock<Response<AnalyzeSentimentResultCollection>>();
            sentimentResponse
                .Setup(s => s.Value)
                .Returns(GetMockAnalyzeSentimentResultCollection());

            _client
                .Setup(s => s.AnalyzeSentimentBatch(It.IsAny<IEnumerable<string>>(), null, null, new System.Threading.CancellationToken()))
                .Returns(sentimentResponse.Object);

            _analytics = new Analytics(_client.Object);
        }

        [Test]
        public void GetResults_ShouldThrowArguementException_IfDocumentsNotProvided()
        {
            Should.Throw<InvalidOperationException>(() =>
            {
                var batch = new BatchOperation();
                var batchResults = _analytics.GetResults(batch);
            }, "No documents have been added to the Batch Operation");
        }

        [Test]
        public void GetResults_ShouldNotThrowException_IfDocumentsAndClientProvided()
        {
            Should.NotThrow(() =>
            {
                var documentA = new Document(documentAText);
                var documentB = new Document(documentBText);

                var batch = new BatchOperation();
                batch.AddDocument(documentA);
                batch.AddDocument(documentB);

                var batchResults = _analytics.GetResults(batch);
            });
        }

        private List<AnalyzeSentimentResult> GetMockSentimentResults()
        {
            return new List<AnalyzeSentimentResult>
                {
                    TextAnalyticsModelFactory.AnalyzeSentimentResult("0", new TextDocumentStatistics(), TextAnalyticsModelFactory.DocumentSentiment(TextSentiment.Positive, 0.9, 0.8, 0.7, new List<SentenceSentiment>(), null)),
                    TextAnalyticsModelFactory.AnalyzeSentimentResult("1", new TextDocumentStatistics(), TextAnalyticsModelFactory.DocumentSentiment(TextSentiment.Positive, 0.9, 0.8, 0.7, new List<SentenceSentiment>(), null))
                };
        }

        private TextDocumentBatchStatistics GetMockStatistics()
        {
            int documentCount = 2, validDocumentCount = 2, invalidDocumentCount = 0;
            long transactionCount = 5;

            return TextAnalyticsModelFactory.TextDocumentBatchStatistics(documentCount, validDocumentCount, invalidDocumentCount, transactionCount);
        }

        private AnalyzeSentimentResultCollection GetMockAnalyzeSentimentResultCollection()
        {
            return TextAnalyticsModelFactory.AnalyzeSentimentResultCollection(GetMockSentimentResults(), GetMockStatistics(), "1");
        }
    }
}
