open System.IO
open System
open System.Globalization
open System.Text.RegularExpressions

let currentDirectory = Directory.GetCurrentDirectory()

let RenameFile (path:string, newName:string) =
    let dir () =
        Path.GetDirectoryName(path)

    let extension () =
        Path.GetExtension(path)

    let newPath = $"{dir()}\{newName}{extension()}"

    System.Diagnostics.Debug.WriteLine(path)
    System.Diagnostics.Debug.WriteLine(newPath)

    if (path = newPath) then
        ()
    else
        File.Move(path, newPath)
        System.Console.ForegroundColor <- System.ConsoleColor.DarkYellow
        System.Console.WriteLine(Path.GetFileNameWithoutExtension(newPath))

let GetNewName (path:string) =
    
    let removeZlib (name:string) =
        name.Replace("(z-lib.org)","").Replace("(Z Lib.Org)","")

    let removeAnna (name:string) =
        Regex.Replace(name, "--annas-archive--libgenrs-nf-\d{6}", "")

    let removeLibgen (name:string) =
        name.Replace("libgen.li","")

    let removeUnderscore (name:string) =
        name.Replace("_"," ")

    let removeDash (name:string) =
        name.Replace("-"," ")

    let TrimString (name:string) =
        name.Trim()

    let PrintState(name:string) = 
        System.Diagnostics.Debug.WriteLine(name)
        name

    Path.GetFileNameWithoutExtension(path)
    |> PrintState
    |> removeZlib
    |> removeAnna
    |> System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase
    |> PrintState
    |> removeUnderscore
    |> removeDash
    |> TrimString
    
let renameFiles (path:string) =
    Directory.GetFiles path 
    |> Array.filter (fun f -> Path.GetFileName(f) <> "pdf-renamer.exe")
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