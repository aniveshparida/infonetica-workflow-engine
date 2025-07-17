using Microsoft.AspNetCore.Mvc;
using InfoneticaWorkflow.Models;
using InfoneticaWorkflow.Storage;

namespace InfoneticaWorkflow.Controllers
{
    [ApiController]
    [Route("api/workflows")]
    public class WorkflowController : ControllerBase
    {
        private readonly InMemoryStorage _storage;

        public WorkflowController(InMemoryStorage storage)
        {
            _storage = storage;
        }

        [HttpPost("definitions")]
        public IActionResult CreateDefinition([FromBody] WorkflowDefinition definition)
        {
            if (_storage.WorkflowDefinitions.ContainsKey(definition.Id!))
                return BadRequest("Workflow with this ID already exists.");

            _storage.WorkflowDefinitions[definition.Id!] = definition;
            return Ok("Workflow definition created.");
        }

        [HttpPost("instances")]
        public IActionResult CreateInstance([FromBody] CreateWorkflowInstanceRequest request)
        {
            if (!_storage.WorkflowDefinitions.ContainsKey(request.WorkflowDefinitionId!))
                return NotFound("Workflow definition not found.");

            var definition = _storage.WorkflowDefinitions[request.WorkflowDefinitionId!];
            var initialState = definition.States.FirstOrDefault(s => s.IsInitial);

            if (initialState == null)
                return BadRequest("No initial state defined in the workflow.");

            var instance = new WorkflowInstance
            {
                Id = Guid.NewGuid().ToString(),
                DefinitionId = request.WorkflowDefinitionId,
                CurrentState = initialState.Name
            };

            _storage.WorkflowInstances[instance.Id] = instance;
            return Ok(instance);
        }

        [HttpPost("instances/execute")]
        public IActionResult ExecuteAction([FromBody] ExecuteActionRequest request)
        {
            if (!_storage.WorkflowInstances.ContainsKey(request.InstanceId!))
                return NotFound("Workflow instance not found.");

            var instance = _storage.WorkflowInstances[request.InstanceId!];

            if (!_storage.WorkflowDefinitions.TryGetValue(instance.DefinitionId!, out var definition))
                return NotFound("Workflow definition not found.");

            var action = definition.Actions.FirstOrDefault(a =>
                a.Id == request.ActionId &&
                a.FromStates.Contains(instance.CurrentState!) &&
                a.Enabled);

            if (action == null)
                return BadRequest("Action not valid or not enabled from current state.");

            // Log history
            instance.History.Add(new WorkflowActionHistory
            {
                ActionId = action.Id,
                Timestamp = DateTime.UtcNow
            });

            instance.CurrentState = action.ToState;

            return Ok(instance);
        }

        [HttpGet("instances/{id}")]
        public IActionResult GetInstance(string id)
        {
            if (!_storage.WorkflowInstances.TryGetValue(id, out var instance))
                return NotFound("Workflow instance not found.");

            return Ok(instance);
        }
    }
}
