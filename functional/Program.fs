module program.fs
open System

open data
open json
open TaskOperationsfs
open FAndS
open notify

let promptForTask () =
    printfn "Enter Task Description:"
    let description = Console.ReadLine()

    printfn "Enter Due Date (yyyy-MM-dd):"
    let dueDateInput = Console.ReadLine()
    let dueDate =
        match DateTime.TryParse(dueDateInput) with
        | true, date -> date
        | false, _ ->
            printfn "Invalid date format.Defaulting to today "
            DateTime.Today
            

    printfn "Enter Priority (1-5):"
    let priorityInput = Console.ReadLine()
    let priority =
        match Int32.TryParse(priorityInput) with
        | true, p when p >= 1 && p <= 5 -> p
        | _ ->
            printfn "Invalid priority. Defaulting to 1."
            1

    printfn "Select Status (1: Pending, 2: Completed):"
    let statusInput = Console.ReadLine()
    let status =
        match statusInput with
        | "1" -> Pending
        | "2" -> Completed
        | _ ->
            printfn "Invalid status. Defaulting to Pending."
            Pending

    if (dueDate < DateTime.Today && status <> Completed) then
        printfn "This task is overdue."
        addTask description dueDate priority Overdue
    else 
        addTask description dueDate priority status

    printfn "Task added successfully!\n\n"

/////
let viewTasks () =
        let tasks = loadTasks ()
        let isEmpty lst =  
         match lst with  
            | [] -> true  
            | _ -> false  

        if isEmpty tasks then
            printfn "No tasks available.\n"
        else
            printfn "Current Tasks:"
            displayTasksRecursive tasks 
////
let  promptForUpdate () =
    printfn "Enter Task ID to update:"
    let taskIdInput = Console.ReadLine()

    match Int32.TryParse(taskIdInput) with
    | true, id ->
       
        printfn "Enter new Priority (or press Enter to skip):"
        let newPriorityInput = Console.ReadLine()
        let newPriority =
            if String.IsNullOrWhiteSpace(newPriorityInput) then
                None 
            else
                match Int32.TryParse(newPriorityInput) with
                | true, p when p >= 1 && p <= 5 -> Some p
                | _ ->
                    printfn "Invalid priority. Defaulting to None."
                    None

       
        printfn "Select new Status (1: Pending, 2: Completed, 3: Overdue or press Enter to skip):"
        let statusInput = Console.ReadLine()
        let newStatus =
            match statusInput with
            | "1" -> Some Pending
            | "2" -> Some Completed
            | "3" -> Some Overdue
            | _ -> None 

        
        updateTask id newStatus newPriority

    | _ ->
        printfn "Invalid Task ID. Please try again."

////
let promptForDelete () =
    printfn "Enter Task ID to delete:"
    let taskIdInput = Console.ReadLine()

    match Int32.TryParse(taskIdInput) with
    | true, id ->
                     deleteTask id
    | _ ->
        printfn "Invalid Task ID. Please try again."

/////////////////////
let rec mainLoop () : unit =
    printfn "Task Scheduler"
    printfn "1. Add Task"
    printfn "2. Show Tasks"
    printfn "3. Update Task"
    printfn "4. Delete Task"
    printfn "5. Filter Tasks"
    printfn "6. Sort Tasks"
    printfn "7. Show Notifications"
    printfn "8. Exit"
    printf "Choose an option: "

    match Console.ReadLine() with
    | "1" -> 
        promptForTask ()
        printfn(" ")
        mainLoop ()
    | "2" -> 
        viewTasks () 
        printfn(" ")
        mainLoop ()
    | "3" -> 
        promptForUpdate () 
        printfn(" ")
        mainLoop ()
    | "4" -> 
        promptForDelete ()
        printfn(" ")
        mainLoop ()

    | "5" -> 
        printfn "Filter by:\n1. Status\n2. Priority\n3. Due Date"
        match Console.ReadLine() with
        | "1" -> 
            printfn "Enter Status (1: Pending, 2: Completed, 3: Overdue):"
            let statusInput = Console.ReadLine()
            let status = match statusInput with
                         | "1" -> Pending
                         | "2" -> Completed
                         | "3" -> Overdue
                         | _ -> Pending
            let filteredTasks = filterByStatus status (loadTasks ())
            displayTasksafterSF filteredTasks
        | "2" -> 
            printfn "Enter Priority (1-5):"
            let priority = int (Console.ReadLine())
            let filteredTasks = filterByPriority priority (loadTasks ())
            displayTasksafterSF filteredTasks
        | "3" -> 
            printfn "Enter Due Date (yyyy-MM-dd):"
            let dueDate = DateTime.Parse(Console.ReadLine())
            let filteredTasks = filterByDueDate dueDate (loadTasks ())
            displayTasksafterSF filteredTasks
        | _ -> printfn "Invalid option."
        printfn(" ")
        mainLoop ()

    | "6" -> 
        printfn "Sort by:\n1. Due Date\n2. Priority\n3.creathion"
        match Console.ReadLine() with
        | "1" -> 
            let sortedTasks = sortByDueDate (loadTasks ())
            displayTasksafterSF sortedTasks
        | "2" -> 
            let sortedTasks = sortByPriority (loadTasks ())
            displayTasksafterSF sortedTasks
        | "3" -> 
            let sortedTasks = sortById (loadTasks ())
            displayTasksafterSF sortedTasks
        | _ -> printfn "Invalid option."
        printfn(" ")
        mainLoop ()

    | "7" -> 
        displayNotifications ()
        printfn(" ")
        mainLoop ()
    | "8" -> 
        printfn "Exiting the program. Goodbye!"
    | _ -> 
        printfn "Invalid option. Please try again."
        mainLoop ()


mainLoop ()
