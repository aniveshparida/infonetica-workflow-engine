namespace InfoneticaWorkflow.Models
{
    public class WorkflowInstance
    {
        public string? Id { get; set; }
        public string? DefinitionId { get; set; }
        public string? CurrentState { get; set; }
        public List<WorkflowActionHistory> History { get; set; } = new();
    }
}
