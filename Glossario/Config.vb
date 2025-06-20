Imports System.Collections.Specialized.BitVector32
Imports System.IO

Public Class Config
    Private ReadOnly filePath As String = "C:\Users\Francesco\Desktop\config.ini"

    Public Sub New()
        If Not My.Computer.FileSystem.FileExists(filePath) Then
            Dim fs As FileStream = File.Create(filePath)
            fs.Close()
        End If
    End Sub

    Private Function getLineBySection(ByVal section As String) As List(Of String)
        Dim lines As String() = File.ReadAllLines(filePath)
        Dim res As New List(Of String)()

        Dim cache As Boolean = False

        For Each line As String In lines
            If Not String.IsNullOrWhiteSpace(line) Then
                If line = "[" + section + "]" Then
                    cache = True
                ElseIf line.Substring(0, 1) = "[" Then
                    cache = False
                End If

                If cache And Not line.Substring(0, 1) = "[" Then
                    res.Add(line)
                End If
            End If
        Next

        Return res
    End Function

    Private Function findLine(ByVal lines As String(), ByVal key As String) As String
        Dim res As String = ""

        For Each line As String In lines
            Dim parts(1) As String
            parts = Split(line, "=", 2)

            If key = parts(0) Then
                res = line
            End If
        Next

        Return res
    End Function

    Public Function Read(ByVal section As String, ByVal key As String) As String
        If Not section = "" Then
            Return findLine(File.ReadAllLines(filePath), key)
        Else
            Dim lines As String() = getLineBySection(section).ToArray()

            Return findLine(lines, key)
        End If
    End Function

    Public Function ReadAll(ByVal section As String) As Dictionary(Of String, String)
        Dim res As New Dictionary(Of String, String)
        Dim lines As String() = getLineBySection(section).ToArray()

        For Each line As String In lines
            Dim cache(1) As String
            cache = Split(line, "=", 2)

            If cache.Length = 0 Then
                res.Add("", "")
            ElseIf cache.Length = 1 Then
                res.Add(cache(0), "")
            ElseIf cache.Length = 2 Then
                res.Add(cache(0), cache(1))
            End If
        Next

        Return res
    End Function

    Public Function Write(ByVal Section As String, ByVal Key As String, ByVal Value As String) As String
        Return Read(Section, Key)
    End Function

    'Public Sub DeleteKey(ByVal Section As String, ByVal Key As String)
    '    WritePrivateProfileString(Section, Key, Nothing, filePath)
    'End Sub

    'Public Sub DeleteSection(ByVal Section As String)
    '    WritePrivateProfileString(Section, Nothing, Nothing, filePath)
    'End Sub
End Class