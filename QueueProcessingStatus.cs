using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiPath
{
    public class QueueProcessingStatus
    {
        public int ItemsToProcess { get; set; }
        public int ItemsInProgress { get; set; }
        public int QueueDefinitionId { get; set; }
        public string QueueDefinitionName { get; set; }
        public string QueueDefinitionDescription { get; set; }
        public bool QueueDefinitionAcceptAutomaticallyRetry { get; set; }
        public int QueueDefinitionMaxNumberOfRetries { get; set; }
        public bool QueueDefinitionEnforceUniqueReference { get; set; }
        public double ProcessingMeanTime { get; set; }
        public int SuccessfulTransactionsNo { get; set; }
        public int ApplicationExceptionsNo { get; set; }
        public int BusinessExceptionsNo { get; set; }
        public double SuccessfulTransactionsProcessingTime { get; set; }
        public double ApplicationExceptionsProcessingTime { get; set; }
        public double BusinessExceptionsProcessingTime { get; set; }
        public int TotalNumberOfTransactions { get; set; }
        public DateTime LastProcessed { get; set; }
    }
}
