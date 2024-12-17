module notify

open System
open data
open json


let rec notifyNearingDeadlines (tasks: Task list) (thresholdDays: int) =
    let cutoffDate = DateTime.Today.AddDays(float thresholdDays)
    match tasks with
    | [] -> []
    | task :: rest ->
        if task.DueDate <= cutoffDate && task.DueDate >= DateTime.Today && task.Status <> Completed  then
            task :: notifyNearingDeadlines rest thresholdDays
        else
            notifyNearingDeadlines rest thresholdDays

let rec notifyOverdueTasks (tasks: Task list) =
    match tasks with
    | [] -> []
    | task :: rest -> //task.Status= Overdue
        if task.DueDate < DateTime.Today   && task.Status <> Completed then
            task :: notifyOverdueTasks rest
        else
            notifyOverdueTasks rest

let displayNotifications () =
    let tasks = loadTasks ()
    let nearingDeadlines = notifyNearingDeadlines tasks 3 // Notify for tasks due in 3 days
    let overdueTasks = notifyOverdueTasks tasks

    let isEmpty lst =  
        match lst with  
        | [] -> true  
        | _ -> false  

    if not (isEmpty nearingDeadlines) then
        printfn "Tasks nearing deadlines(=<3days)::"
        displayTasksRecursive nearingDeadlines 
    if (isEmpty nearingDeadlines) then
        printfn("no Tasks nearing deadlines")   

    if not (isEmpty overdueTasks) then
        printfn "Overdue Tasks:"
        displayTasksRecursive overdueTasks
    if (isEmpty overdueTasks) then
        printfn("no Overdue Tasks")   