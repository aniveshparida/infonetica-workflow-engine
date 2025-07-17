namespace InfoneticaWorkflow.Models
{
    public class StateDefinition
    {
        public string? Name { get; set; }
        public bool IsInitial { get; set; } = false;
        public bool IsFinal { get; set; } = false;
        public bool Enabled { get; set; } = true;
    }
}
