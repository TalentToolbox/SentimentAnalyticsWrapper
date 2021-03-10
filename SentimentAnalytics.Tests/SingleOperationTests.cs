using Azure;
using Azure.AI.TextAnalytics;
using Moq;
using NUnit.Framework;
using SentimentAnalytics.Models;
using SentimentAnalytics.Operations;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SentimentAnalytics.Tests
{
    [TestFixture]
    class SingleOperationTests
    {
        const string documentAText = @"The food and service were unacceptable, but the concierge were nice.
                    After talking to them about the quality of the food and the process
                    to get room service they refunded the money we spent at the restaurant and
                    gave us a voucher for nearby restaurants.";

        private Analytics _analytics;

        [SetUp]
        public void SetUp()
        {
            Mock<TextAnalyticsClient> _client = new Mock<TextAnalyticsClient>();

            var sentimentResponse = new Mock<Response<DocumentSentiment>>();
            sentimentResponse
                .Setup(s => s.Value)
                .Returns(GetMockSentimentResult());

            _client
                .Setup(s => s.AnalyzeSentiment(It.IsAny<string>(), null, new System.Threading.CancellationToken()))
                .Returns(sentimentResponse.Object);

            _analytics = new Analytics(_client.Object);
        }

        [Test]
        public void GetResults_ShouldThrowArguementException_IfDocumentsNotProvided()
        {
            Should.Throw<InvalidOperationException>(() =>
            {
                var singleOperation = new SingleOperation(default);
                var batchResults = _analytics.GetResults(singleOperation);
            }, "A document to analyse has not been provided");
        }

        [Test]
        public void GetResults_ShouldNotThrowException_IfDocumentsAndClientProvided()
        {
            Should.NotThrow(() =>
            {
                var documentA = new Document(documentAText);
                var singleOperation = new SingleOperation(documentA);
                var batchResults = _analytics.GetResults(singleOperation);
            });
        }

        [Test]
        public void GetResults_ShouldReturnSingleItem_IfDocumentsAndClientProvided()
        {
            var documentA = new Document(documentAText);
            var singleOperation = new SingleOperation(documentA);

            var batchResults = _analytics.GetResults(singleOperation);

            batchResults.ShouldHaveSingleItem();
        }

        [Test]
        public void GetResults_ShouldReturnDocumentThatMatchesDocumentSet_IfDocumentsAndClientProvided()
        {
            var documentA = new Document(documentAText);
            var singleOperation = new SingleOperation(documentA);

            var batchResults = _analytics.GetResults(singleOperation);

            var documentResult = batchResults.Single();
            documentResult.Text.ShouldBe(documentA.Text);
        }

        [Test]
        public void GetResults_ShouldReturnDocumentSetWithScoreResults_IfDocumentsAndClientProvided()
        {
            var documentA = new Document(documentAText);
            var singleOperation = new SingleOperation(documentA);

            var batchResults = _analytics.GetResults(singleOperation);

            var documentResult = batchResults.Single();
            documentResult.ShouldSatisfyAllConditions(
                (r) => { r.Sentiment.ShouldBe(TextSentiment.Mixed); },
                (r) => { r.Scores.ShouldNotBeNull(); },
                (r) => { r.Scores.Positive.ShouldBe(PositiveScore); },
                (r) => { r.Scores.Neutral.ShouldBe(NeutralScore); },
                (r) => { r.Scores.Negative.ShouldBe(NegativeScore); }
            );
        }

        const double PositiveScore = 0.9;
        const double NeutralScore = 0.8;
        const double NegativeScore = 0.7;

        private DocumentSentiment GetMockSentimentResult()
        {
            return TextAnalyticsModelFactory.DocumentSentiment(TextSentiment.Mixed, PositiveScore, NeutralScore, NegativeScore, new List<SentenceSentiment>(), null);
        }
    }
}
