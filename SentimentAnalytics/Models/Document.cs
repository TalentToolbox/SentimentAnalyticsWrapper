using Azure.AI.TextAnalytics;
using System;

namespace SentimentAnalytics.Models
{
    public class Document
    {
        public Guid Id { get; set; }
        public Document(string text)
        {
            ValidateText(text);
            Text = text;

            Id = Guid.NewGuid();
        }

        public string Text { get; private set; }
        public TextSentiment Sentiment { get; set; }
        public ConfidenceScores Scores { get; private set; }

        public string ErrorMessage { get; private set; }
        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        protected internal void SetErrorMessage(string message)
        {
            ErrorMessage = message;
        }

        protected internal void SetResults(DocumentSentiment value)
        {
            SetConfidenceScores(value.ConfidenceScores);
            Sentiment = value.Sentiment;
        }

        private void SetConfidenceScores(SentimentConfidenceScores scores)
        {
            Scores = new ConfidenceScores(scores.Positive, scores.Neutral, scores.Negative);
        }

        private void ValidateText(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new InvalidOperationException("Document text cannot be empty");

            if (text.Length > 5120)
                throw new InvalidOperationException("Text exceeds the 5,120 limit of the analytics API");
        }

        public sealed class ConfidenceScores
        {
            public ConfidenceScores(double positive, double neutral, double negative)
            {
                Positive = positive;
                Neutral = neutral;
                Negative = negative;
            }

            public double Positive { get; private set; }
            public double Neutral { get; private set; }
            public double Negative { get; private set; }
        }
    }
}
