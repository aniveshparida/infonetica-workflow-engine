namespace InfoneticaWorkflow.Models
{
    public class WorkflowDefinition
    {
        public string? Id { get; set; }
        public List<StateDefinition> States { get; set; } = new();
        public List<ActionDefinition> Actions { get; set; } = new();
    }
}
