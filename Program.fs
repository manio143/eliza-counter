open Suave
open Suave.Successful
open Suave.RequestErrors
open Suave.Operators
open Suave.Filters
open Suave.Redirection

open System.IO
open System.Net

let saveFile = "counter.dat"

[<EntryPoint>]
let main argv =

    let counter = ref 0
    do if File.Exists(saveFile) then 
        counter := int (File.ReadAllText(saveFile)) 
        else ()

    let index = 
        request <| 
            fun _ -> 
                OK (sprintf "<html><head><meta charset=\"utf-8\"><title>Licznik fauli na Elizie</title><style>* {text-align: center;}</style></head>
                    <body><h2>Licznik fauli na Elizie</h2><p>Eliza została sfaulowana %d razy.</p>
                    <!--<p style=\"margin-top: 50px\"><a href=\"/add/1\">Dodaj faul</a>&ensp;<a href=\"/remove/1\">Usuń faul</a></p>-->
                    </body></html>" !counter)

    let changeCounter c =
        counter := !counter + c
        File.WriteAllText(saveFile, string (!counter))    

    let app = 
        choose [
            GET >=> choose [
                path "/" >=> index
                pathScan "/add/%d" (fun c -> changeCounter c; redirect "/")
                pathScan "/remove/%d" (fun c -> 
                                            do if (!counter) > 0 then 
                                                changeCounter (-c)
                                                else ()
                                            redirect "/")
            ]
            NOT_FOUND "<h1>404 Not Found</h1><p>Requested page has not been found<p>"
        ]

    let serverConfig = 
        let envport = System.Environment.GetEnvironmentVariable "port"
        let port = (if not (isNull envport) then envport else "8083") |> int
        { defaultConfig with bindings = [ HttpBinding.createSimple HTTP "127.0.0.1" port ] }

    startWebServer serverConfig app
    0 // return an integer exit code

