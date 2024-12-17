module json
open System.IO
open Newtonsoft.Json
open data


let jsonFilePath = "tasks.json"

let loadTasks () =
    if File.Exists(jsonFilePath) then
        let json = File.ReadAllText(jsonFilePath)
        JsonConvert.DeserializeObject<Task list>(json)
    else
        []
let saveTasks (all: Task list) =
    let json = JsonConvert.SerializeObject(all, Formatting.Indented)
    File.WriteAllText(jsonFilePath, json)