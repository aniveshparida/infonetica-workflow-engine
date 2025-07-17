# 🧠 Infonetica – Configurable Workflow Engine

### 💼 Software Engineer Intern Take-Home Assignment

---

## 📌 Objective

This backend project implements a minimal configurable workflow engine (state-machine API) that allows:

1. Defining one or more **workflows** as configurable **state machines** (with states + actions).
2. Starting **workflow instances** from a given definition.
3. Executing **actions** that transition an instance between states (with full validation).
4. Inspecting and listing **states, actions, definitions, and running instances**.

---

## 🚀 Quick Start

### 🧰 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)

---

### 🛠 Build and Run

```bash
dotnet build
dotnet run
Server will run at:

arduino
Copy
Edit
http://localhost:5116
You can use Postman or any REST client to test the endpoints.

📬 API Usage (Postman Instructions)
1. Create a Workflow Definition
POST /api/workflows/definitions

Example JSON body:

json
Copy
Edit
{
  "states": [
    { "id": "draft", "name": "Draft", "isInitial": true, "isFinal": false, "enabled": true },
    { "id": "review", "name": "Review", "isInitial": false, "isFinal": false, "enabled": true },
    { "id": "approved", "name": "Approved", "isInitial": false, "isFinal": true, "enabled": true }
  ],
  "actions": [
    {
      "id": "submit",
      "name": "Submit for Review",
      "fromStates": ["draft"],
      "toState": "review",
      "enabled": true
    },
    {
      "id": "approve",
      "name": "Approve Document",
      "fromStates": ["review"],
      "toState": "approved",
      "enabled": true
    }
  ]
}
2. Create a Workflow Instance
POST /api/workflows/instances

json
Copy
Edit
{
  "definitionId": "<YourDefinitionId>"
}
3. Execute an Action
POST /api/workflows/instances/execute

json
Copy
Edit
{
  "instanceId": "<YourInstanceId>",
  "actionId": "submit"
}
4. Get Instance State & History
GET /api/workflows/instances/{instanceId}

🧩 Features Implemented
✅ Create workflow definitions with states and actions.

✅ Start new workflow instances.

✅ Execute actions with validation:

Action must belong to the workflow definition.

Action must be enabled.

Current state must be in action’s fromStates.

Cannot execute from final states.

✅ Retrieve instance’s current state and transition history.

✅ In-memory persistence (no database).

📁 Project Structure
pgsql
Copy
Edit
.
├── Controllers/
│   └── WorkflowController.cs
├── Models/
│   ├── ActionDefinition.cs
│   ├── StateDefinition.cs
│   ├── WorkflowDefinition.cs
│   ├── WorkflowInstance.cs
│   └── WorkflowActionHistory.cs
├── Storage/
│   └── InMemoryStorage.cs
├── Program.cs
└── README.md
⚠️ Assumptions, Shortcuts & Known Limitations
✅ Assumptions
A workflow definition must have exactly one initial state (isInitial == true).

Transitions must be enabled to be executed.

Final states (isFinal == true) cannot be transitioned from.

Actions can have multiple source states (fromStates) but exactly one destination (toState).

⚡ Shortcuts Taken (due to 2-hour time limit)
Used in-memory storage instead of file/database.

Did not include Swagger/OpenAPI documentation.

No authentication, concurrency, or persistence layer.

No unit tests added.

🧠 Tech Stack
Language: C#

Framework: ASP.NET Core (.NET 8)

Architecture: Minimal API with MVC-style structure

Persistence: In-memory collections

📎 How to Extend
Add file-based storage using JSON/YAML for persistence.

Add Swagger support via Swashbuckle.AspNetCore.

Add authentication or role-based access.

Support partial updates (PATCH) to definitions.

👋 Author Notes
This was built as part of the Infonetica Software Engineer Intern selection process. The goal was to demonstrate correctness, validation, and clean structure .

Thank you for the opportunity to showcase this!

