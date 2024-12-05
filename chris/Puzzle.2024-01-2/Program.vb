Imports System

Module Program
    Sub Main(args As String())
        Dim line As String
        Dim numList1 = New List(Of Integer)
        Dim numList2 = New List(Of Integer)
        line = Console.ReadLine()
        Do While Len(line) > 0
            Dim split = line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
            numList1.Add(Integer.Parse(split(0)))
            numList2.Add(Integer.Parse(split(1)))
            line = Console.ReadLine()
        Loop

        numList1.Sort()
        numList2.Sort()
        Dim sum = 0L
        For i = 0 To numList1.Count - 1
            sum = sum + numList1(i) * numList2.FindAll(Function(x) x = numList1(i)).Count
        Next

        Console.WriteLine(sum)
    End Sub
End Module
