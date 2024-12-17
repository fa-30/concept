module notify
open data
open json
open System


let notifyNearingDeadlines thresholdDays =
    let tasks = loadTasks ()
    let cutoffDate = DateTime.Today.AddDays(float thresholdDays)
    let mutable nearingTasks = []

    for task in tasks do
        if task.DueDate <= cutoffDate &&  task.DueDate >= DateTime.Today && task.Status <> Completed  then
            nearingTasks <- task :: nearingTasks
 
    nearingTasks

let notifyOverdueTasks () =
    let tasks = loadTasks ()
    let mutable overdueTasks = []

    for task in tasks do
        if task.DueDate < DateTime.Today && task.Status <> Completed  then
            overdueTasks <- task :: overdueTasks

    overdueTasks

let displayNotifications () =
    let nearingDeadlines = notifyNearingDeadlines 3 // Notify for tasks due in 3 days
    let overdueTasks = notifyOverdueTasks ()
    
    let isEmpty lst =  
        match lst with  
        | [] -> true  
        | _ -> false 

    if not (isEmpty nearingDeadlines) then
        printfn "Tasks nearing deadlines(=<3days):"
        for task in nearingDeadlines do
            printTask task
    if (isEmpty nearingDeadlines) then
        printfn("no Tasks nearing deadlines") 

    if not (isEmpty overdueTasks ) then
        printfn "Overdue Tasks:"
        for task in overdueTasks do
            printTask task
    if (isEmpty overdueTasks) then
        printfn("no Overdue Tasks")  