module FAndS
 
open System
open data


let rec filterByStatus (status: TaskStatus) (tasks: Task list) =
    match tasks with
    | [] -> []
    | task :: rest ->
        if task.Status = status then
            task :: filterByStatus status rest
        else
            filterByStatus status rest

let rec filterByPriority (priority: int) (tasks: Task list) =
    match tasks with
    | [] -> []
    | task :: rest ->
        if task.Priority = priority then
            task :: filterByPriority priority rest
        else
            filterByPriority priority rest

let rec filterByDueDate (dueDate: DateTime) (tasks: Task list) =
    match tasks with
    | [] -> []
    | task :: rest ->
        if task.DueDate.Date = dueDate.Date then
            task :: filterByDueDate dueDate rest
        else
            filterByDueDate dueDate rest

//small..big
let rec insertByDueDate (task: Task) (sortedTasks: Task list) =
    match sortedTasks with
    | [] -> [task]
    | head :: tail ->
        if task.DueDate < head.DueDate then
            task :: sortedTasks 
        else
            head :: insertByDueDate task tail

let rec sortByDueDate (tasks: Task list) =
    match tasks with
    | [] -> []
    | head :: tail ->
        insertByDueDate head (sortByDueDate tail)

//1.2.3...
let rec insertByPriority (task: Task) (sortedTasks: Task list) =
    match sortedTasks with
    | [] -> [task]
    | head :: tail ->
        if task.Priority < head.Priority then
            task :: sortedTasks
        else
            head :: insertByPriority task tail

let rec sortByPriority (tasks: Task list) =
    match tasks with
    | [] -> []
    | head :: tail ->
        insertByPriority head (sortByPriority tail)
//1.2.3
let rec insertById (task: Task) (sortedTasks: Task list) =
    match sortedTasks with
    | [] -> [task]
    | head :: tail ->
        if task.TaskId < head.TaskId then
            task :: sortedTasks
        else
            head :: insertById task tail

let rec sortById (tasks: Task list) =
    match tasks with
    | [] -> []
    | head :: tail ->
        insertById head (sortById tail)

//donot load
let displayTasksafterSF (tasks: Task list) =
    let isEmpty lst =  
        match lst with  
        | [] -> true  
        | _ -> false  

    let rec printTasks lst =
        match lst with
        | [] -> ()  
        | task :: tail -> 
            printTask task  
            printTasks tail  

    if isEmpty tasks then  
        printfn "No tasks available."  
    else  
        printfn "Current Tasks:"  
        printTasks tasks
