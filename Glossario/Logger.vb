Imports System.IO

Public Class Logger
    Private filePath As String = ""

    Public Sub New()
        'Read from command line arguments
        For Each line As String In Environment.GetCommandLineArgs()
            Dim cache(1) As String
            cache = Split(line, "=", 2)

            If cache(0) = "/LOG" Then
                filePath = cache(1)
            End If
        Next

        If filePath = "" Then
            filePath = "C:\Users\Francesco\Desktop\log.txt"
        End If

        'Create the log file if not exists at filePath
        If Not My.Computer.FileSystem.FileExists(filePath) Then
            Dim fs As FileStream = File.Create(filePath)
            fs.Close()
        End If
    End Sub

    Public Sub New(ByVal fp As String)
        filePath = fp

        'Create the log file if not exists at filePath
        If Not My.Computer.FileSystem.FileExists(filePath) Then
            Dim fs As FileStream = File.Create(filePath)
            fs.Close()
        End If
    End Sub

    Public Sub AddLog(ByVal operation As String, ByVal parameters As String)
        Dim lines As List(Of String) = File.ReadAllLines(filePath).ToList()
        Dim cache As String = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - Eseguita l'operazione <" + operation + ">"

        If Not parameters = "" Then
            cache = cache + " con parametri <" + parameters + ">"
        End If

        lines.Add("")
        lines.Add(cache)
        File.WriteAllLines(filePath, lines.ToArray())
    End Sub
End Class
