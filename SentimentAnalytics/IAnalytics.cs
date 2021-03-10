using SentimentAnalytics.Models;
using SentimentAnalytics.Operations;
using System.Collections.Generic;

namespace SentimentAnalytics
{
    /// <summary>
    /// It is recommended to inject this class as a singleton
    /// </summary>
    public interface IAnalytics
    {
        IEnumerable<Document> GetResults(AnalyticOperation operation);
    }
}
