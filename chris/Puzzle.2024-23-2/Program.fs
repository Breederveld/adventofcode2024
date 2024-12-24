let lines = System.IO.File.ReadAllLines("input.txt");
let connections = lines |> Array.map(fun l -> l.Split("-"));
let comps = connections |> Array.collect(fun a -> a) |> Array.distinct |> Array.sort
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
let rec find (start: string) : seq<array<string>> =
    connected[start]
        |> Seq.map(fun exclude ->
            let friends = connected[start] |> Seq.append(seq { start }) |> Seq.except(seq { exclude }) |> Seq.toArray
            if (friends |> Seq.forall(fun comp -> friends |> Seq.forall(fun friend -> comp = friend || connected[comp] |> Seq.contains(friend)))) then
                seq { friends }
            else
                Seq.empty)
        |> Seq.collect(fun a -> a)
let best =
    comps
    |> Seq.collect(fun c -> find(c))
    |> Seq.head
let result =
    best
    |> Array.sort
    |> String.concat ","
printfn $"{result}";