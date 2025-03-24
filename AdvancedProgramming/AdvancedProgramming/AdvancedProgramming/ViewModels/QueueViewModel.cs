using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvancedProgramming.ViewModels
{
    public class QueueViewModel
    {
        public int QueueId { get; set; }
        public int TaskId { get; set; }  // Relación con la tarea
        public Task Task { get; set; }  // Relación con Task
        public string Status { get; set; }
        public string Priority { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExecutionDate { get; set; }
    }
}