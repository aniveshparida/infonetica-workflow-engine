using InfoneticaWorkflow.Models;

namespace InfoneticaWorkflow.Storage
{
    public class InMemoryStorage
    {
        public Dictionary<string, WorkflowDefinition> WorkflowDefinitions { get; } = new();
        public Dictionary<string, WorkflowInstance> WorkflowInstances { get; } = new();
    }
}
