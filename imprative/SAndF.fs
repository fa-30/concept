module SAndF
open System
open data
open json

let filterByStatus (status: TaskStatus) =
    let tasks = loadTasks ()
    let mutable filteredTasks = []

    for task in tasks do
        if task.Status = status then
            filteredTasks <-filteredTasks @  [task]

    filteredTasks
let filterByPriority priority =
    let tasks = loadTasks ()
    let mutable filteredTasks = []

    for task in tasks do
        if task.Priority = priority then
            filteredTasks <-filteredTasks @  [task]

    filteredTasks

let filterByDueDate (dueDate: DateTime) =
    let tasks = loadTasks ()
    let mutable filteredTasks = []

    for task in tasks do
        if task.DueDate.Date =dueDate.Date then
            filteredTasks <-filteredTasks @  [task]

    filteredTasks

//small..big
let sortByDueDate()  =
    let tasks=loadTasks()
    let mutable sortedTasks = []
    for task in tasks do
        let mutable inserted = false
        let mutable currentLength = 0
        
        // .length
        for _ in sortedTasks do
            currentLength <- currentLength + 1
        
        for i in 0 .. currentLength - 1 do
            if task.DueDate < sortedTasks.[i].DueDate && not inserted then
                sortedTasks <- sortedTasks.[0 .. i - 1] @ [task] @ sortedTasks.[i ..]
                inserted <- true
        if not inserted then
            sortedTasks <- sortedTasks @ [task]
    sortedTasks

let sortByPriority()  =
    let tasks=loadTasks()
    let mutable sortedTasks = []
    for task in tasks do
        let mutable inserted = false
        let mutable currentLength = 0
        
        // .legnth
        for _ in sortedTasks do
            currentLength <- currentLength + 1

        for i in 0 .. currentLength - 1 do
            if task.Priority < sortedTasks.[i].Priority && not inserted then
                sortedTasks <- sortedTasks.[0 .. i - 1] @ [task] @ sortedTasks.[i ..]
                inserted <- true
        if not inserted then
            sortedTasks <- sortedTasks @ [task]
    sortedTasks

let sortByid()  =
    let tasks=loadTasks()
    let mutable sortedTasks = []
    for task in tasks do
        let mutable inserted = false
        let mutable currentLength = 0
        
        // .legnth
        for _ in sortedTasks do
            currentLength <- currentLength + 1

        for i in 0 .. currentLength - 1 do
            if task.TaskId < sortedTasks.[i].TaskId && not inserted then
                sortedTasks <- sortedTasks.[0 .. i - 1] @ [task] @ sortedTasks.[i ..]
                inserted <- true
        if not inserted then
            sortedTasks <- sortedTasks @ [task]
    sortedTasks

let displayTasksSorF tasksorf=
    let isEmpty lst =  
        match lst with  
        | [] -> true  
        | _ -> false  

    if isEmpty tasksorf then  
        printfn "No tasks available."  
    else  
        printfn "Current Tasks:"  
        for task in tasksorf do  
            printTask task  