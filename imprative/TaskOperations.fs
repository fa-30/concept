module TaskOperations

open data
open json

let addTask description dueDate priority status =
    let mutable taskList = loadTasks ()
    let mutable maxId = 0

    for task in taskList do
        if task.TaskId > maxId then
            maxId <- task.TaskId

    let nextTaskId = maxId + 1
    let newTask = { TaskId = nextTaskId; Description = description; DueDate = dueDate; Priority = priority; Status = status }
    taskList <- newTask :: taskList
    saveTasks taskList

let viewTasks() =
    let tasks = loadTasks ()
    let isEmpty lst =  
        match lst with  
        | [] -> true  
        | _ -> false  

    if isEmpty tasks then  
        printfn "No tasks available."  
    else  
        printfn "Current Tasks:"  
        for task in tasks do  
            printTask task  
let updateTask taskId newStatus newPriority =
    let taskList = loadTasks ()
    let mutable updatedTasks = []

    for task in taskList do
        if task.TaskId = taskId then
            let updatedTask = 
                { task with
                    Status = newStatus |> Option.defaultValue task.Status
                    Priority = newPriority |> Option.defaultValue task.Priority }
            updatedTasks <- updatedTasks @ [updatedTask ] //@ operator is used to concatenate lists, which allows us to maintain the order without needing to reverse the list later.
        else
            updatedTasks <- updatedTasks @ [task] 

   (* let mutable finalTasks = []
    for i in 0.. (List.length taskList - 1) do
        finalTasks <- updatedTasks.[i] :: finalTasks*)

    saveTasks  updatedTasks //finalTask
    printfn "Task updated successfully!\n"
   

let deleteTask taskId =
    let taskList = loadTasks ()
    let mutable updatedTasks = []

    for task in taskList do
        if task.TaskId <> taskId then
            updatedTasks <- updatedTasks @ [task]  
        else 
            printfn "Task deleted successfully!\n"

    saveTasks updatedTasks
    