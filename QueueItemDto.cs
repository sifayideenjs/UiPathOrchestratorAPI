using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiPath
{
    //public class QueueItemDto
    //{
    //    public int QueueDefinitionId { get; set; }
    //    public string QueueDefinitionName { get; set; }
    //    public Int64? ProcessingExceptionId { get; set; }
    //    public string SpecificContent { get; set; }
    //    public string Output { get; set; }
    //    public string OutputData { get; set; }
    //    public string Status { get; set; }
    //    public string ReviewStatus { get; set; }
    //    public string Key { get; set; }
    //    public string Reference { get; set; }
    //    public string ExceptionType { get; set; }
    //    public DateTime? DueDate { get; set; }
    //    public string Priority { get; set; }
    //    public RobotDto Robot { get; set; }
    //    public DateTime? DeferDate { get; set; }
    //    public DateTime? StartProcessing { get; set; }
    //    public DateTime? EndProcessing { get; set; }
    //    public int SecondsInPreviousAttempts { get; set; }
    //    public Int64? AncestorId { get; set; }
    //    public Int32? RetryNumber { get; set; }
    //    public string SpecificData { get; set; }
    //    public DateTime? CreationTime { get; set; }
    //    public int Id { get; set; }
    //}

    public class QueueItemDto
    {
        public int QueueDefinitionId { get; set; }
        public object OutputData { get; set; }
        public string Status { get; set; }
        public string ReviewStatus { get; set; }
        public object ReviewerUserId { get; set; }
        public string Key { get; set; }
        public object Reference { get; set; }
        public string ProcessingExceptionType { get; set; }
        public object DueDate { get; set; }
        public string Priority { get; set; }
        public object DeferDate { get; set; }
        public DateTime StartProcessing { get; set; }
        public DateTime EndProcessing { get; set; }
        public int SecondsInPreviousAttempts { get; set; }
        public int? AncestorId { get; set; }
        public int RetryNumber { get; set; }
        public string SpecificData { get; set; }
        public DateTime CreationTime { get; set; }
        public object Progress { get; set; }
        public string RowVersion { get; set; }
        public int Id { get; set; }
        public ProcessingException ProcessingException { get; set; }
        public object SpecificContent { get; set; }
        public object Output { get; set; }
    }

    public class ProcessingException
    {
        public string Reason { get; set; }
        public string Details { get; set; }
        public string Type { get; set; }
        public object AssociatedImageFilePath { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
