module TaskOperationsfs
open System

open data 
open json

//beter than .map
let addTask (description: string) (dueDate: DateTime) (priority: int) (status: TaskStatus) =
    let taskList = loadTasks ()
    let rec findMaxId tasks maxId =  
        match tasks with  
        | [] -> maxId  
        | head :: tail ->   
            let newMaxId = if head.TaskId > maxId then head.TaskId else maxId  
            findMaxId tail newMaxId  
            
    let nextTaskId = findMaxId taskList 0 + 1 
    let newTask = { TaskId = nextTaskId; Description = description; DueDate = dueDate; Priority = priority; Status = status }
    saveTasks (newTask :: taskList)

let updateTask (taskId: int) (newStatus: TaskStatus option) (newPriority: int option) =
    let taskList = loadTasks ()
    let rec updateTasks tasks =  
        match tasks with  
        | [] -> []  
        | head :: tail ->  
            if head.TaskId = taskId then  
                { head with  
                    Status = newStatus |> Option.defaultValue head.Status  
                    Priority = newPriority |> Option.defaultValue head.Priority } :: updateTasks tail        
            else  
                head :: updateTasks tail  

    let updatedTasks = updateTasks taskList 
    saveTasks updatedTasks
    printfn "Task updated successfully!\n\n"

//beter than List.filter
let deleteTask (taskId: int) =
    let taskList = loadTasks ()
    
    let rec removeTask tasks =  
        match tasks with  
        | [] -> []  
        | head :: tail ->  
            if head.TaskId = taskId then 
                printfn "Task deleted successfully!\n\n" 
                removeTask tail  
            else  
                head :: removeTask tail  

    let updatedTasks = removeTask taskList
    saveTasks updatedTasks
