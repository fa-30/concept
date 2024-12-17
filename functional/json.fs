module json

open data 

open System.IO
open Newtonsoft.Json

let jsonFilePath = "tasks.json"

///beter than List.iter
let loadTasks () =
    if File.Exists(jsonFilePath) then
        let json = File.ReadAllText(jsonFilePath)
        JsonConvert.DeserializeObject<Task list>(json)
    else
        [] 
   

let saveTasks (all: Task list) = 
    let json = JsonConvert.SerializeObject(all, Formatting.Indented)
    File.WriteAllText(jsonFilePath, json)