module data
open System


type TaskStatus =
    | Pending
    | Completed
    | Overdue

type Task = {
    TaskId: int
    Description: string
    DueDate: DateTime
    Priority: int
    Status: TaskStatus
}

let printTask task =  
    printfn "ID: %d, Description: %s, Due Date: %s, Priority: %d, Status: %A"  
        task.TaskId task.Description (task.DueDate.ToString("yyyy-MM-dd")) task.Priority task.Status  

let rec displayTasksRecursive tasks =  
    match tasks with  
    | [] -> () 
    | head :: tail ->   
        printTask head   
        displayTasksRecursive tail