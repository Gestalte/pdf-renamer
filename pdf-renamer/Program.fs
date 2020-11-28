open System.IO

let currentDirectory = Directory.GetCurrentDirectory()

let RenameFile (path:string, newName:string) =
    let dir () =
        Path.GetDirectoryName(path)
    let extension () =
        Path.GetExtension(path)
    let newPath = $"{dir()}\{newName}{extension()}"

    if path = newPath then
        ()
    else
        Directory.Move(path, newPath)
        System.Console.ForegroundColor <- System.ConsoleColor.DarkYellow
        System.Console.WriteLine(Path.GetFileNameWithoutExtension(newPath))

let GetNewName (path:string) =
    let removeZlib (name:string) =
        name.Replace("(z-lib.org)","")

    let removeUnderscore (name:string) =
        name.Replace("_"," ")

    let TrimString (name:string) =
        name.Trim()

    Path.GetFileNameWithoutExtension(path)
    |> removeUnderscore
    |> removeZlib
    |> TrimString

let renameFiles (path:string) =
    Directory.GetFiles path 
    |> Array.map (fun a -> (a, GetNewName(a)))
    |> Array.iter RenameFile

[<EntryPoint>]
let main argv =
    renameFiles currentDirectory    

    System.Console.ForegroundColor <- System.ConsoleColor.DarkYellow
    System.Console.WriteLine("")
    System.Console.WriteLine("All Files converted")
    let _ = System.Console.ReadKey()
    System.Console.ResetColor()

    0 // return an integer exit code