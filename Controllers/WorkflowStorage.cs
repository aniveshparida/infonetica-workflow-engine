using System.Collections.Generic;
using InfoneticaWorkflow.Models;

namespace WorkflowEngine.Models
{
    public static class WorkflowStorage
    {
        public static Dictionary<string, WorkflowDefinition> WorkflowDefinitions { get; set; } = new();
        public static Dictionary<string, WorkflowInstance> WorkflowInstances { get; set; } = new();
    }
}
