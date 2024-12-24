let lines = System.IO.File.ReadAllLines("input.txt");
let connections = lines |> Array.map(fun l -> l.Split("-"));
let comps = connections |> Array.collect(fun a -> a) |> Array.distinct
let connected =
    comps
    |> Array.map(fun comp ->
        let connected =
            connections
            |> Array.filter(fun cc -> cc[0] = comp || cc[1] = comp)
            |> Array.map(fun cc -> if (cc[0] = comp) then cc[1] else cc[0])
        (comp, connected)
    )
    |> dict
let rec dfs' (start: string) (next: string) (visited: seq<string>) : array<array<string>> =
    if (visited |> Seq.length > 3) then
        [||]
    else
        let attempt = connected[next] |> Array.except visited
        attempt
            |> Array.map(fun n ->
                if (n = start) then
                    [|visited |> Seq.append([|start|]) |> Seq.toArray|]
                else
                    let v = visited |> Seq.append([|n|])
                    dfs'(start)(n)(v)
                )
            |> Array.collect(fun a -> a)
let rec dfs (start: string) = dfs'(start)(start)(Seq.empty)
let result =
    comps
    |> Array.filter(fun c -> c.StartsWith("t"))
    |> Array.collect(fun c -> dfs(c))
    |> Array.map(fun cycle -> cycle |> Array.sort)
    |> Array.distinct
    |> Array.filter(fun cycle -> cycle.Length = 3)
    |> Array.length
printfn $"{result}";
    