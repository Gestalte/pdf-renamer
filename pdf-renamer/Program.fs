open System.IO

let currentDirectory = Directory.GetCurrentDirectory()

let RenameFile (path:string, newName:string) =
    let dir () =
        Path.GetDirectoryName(path)
    let extension () =
        Path.GetExtension(path)
    let newPath () = 
        $"{dir()}{newName}{extension()}"
    Directory.Move(path, newPath())

let removeZlib (name:string) =
    name.Replace("(z-lib.org)","")

let removeUnderscore (name:string) =
    name.Replace("_"," ")

let GetNewName (path:string) =
    let name () =
        Path.GetFileNameWithoutExtension(path)
    name()
    |> removeUnderscore
    |> removeZlib

let renameFiles (path:string) =
    Directory.GetFiles path 
    |> Array.map (fun a -> (a, GetNewName(a)))
    |> Array.iter RenameFile
   
[<EntryPoint>]
let main argv =
    renameFiles currentDirectory
    0 // return an integer exit code