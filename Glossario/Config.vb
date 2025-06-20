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

    Private Function getSection() As List(Of String)
        Dim lines As String() = File.ReadAllLines(filePath)
        Dim res As New List(Of String)()

        For Each line As String In lines
            If Not String.IsNullOrWhiteSpace(line) Then
                If line.Substring(0, 1) = "[" Then
                    res.Add(line.Trim(New Char() {"[", "]"}))
                End If
            End If
        Next

        Return res
    End Function

    Private Function getLinesBySection(ByVal section As String) As List(Of String)
        Dim lines As String() = File.ReadAllLines(filePath)
        Dim res As New List(Of String)()

        Dim cache As Boolean = False

        For Each line As String In lines
            If Not String.IsNullOrWhiteSpace(line) Then
                If line = "[" + section + "]" Then
                    cache = True
                End If

                If cache Then
                    If line.Substring(0, 1) = "[" Then
                        cache = False
                    Else
                        res.Add(line)
                    End If
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
            Dim lines As String() = getLinesBySection(section).ToArray()

            Return findLine(lines, key)
        End If
    End Function

    Public Function ReadAll(ByVal section As String) As Dictionary(Of String, String)
        Dim res As New Dictionary(Of String, String)
        Dim lines As String() = getLinesBySection(section).ToArray()

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

    Public Function Write(ByVal section As String, ByVal key As String, ByVal value As String) As String
        Dim lines As List(Of String) = File.ReadAllLines(filePath).ToList()

        For Each line As String In lines
            Console.WriteLine(line)
        Next

        Dim sections As List(Of String) = getSection()

        If sections.Contains(section) Then
            'Dim index As Integer = lines.IndexOf(key + "=" + value)

            Console.WriteLine(String.IsNullOrWhiteSpace(findLine(getLinesBySection(section).ToArray(), key)))

            If Not String.IsNullOrWhiteSpace(findLine(getLinesBySection(section).ToArray(), key)) Then
                Dim cache As Boolean = False
                For i As Integer = 0 To (lines.Count - 1)
                    Console.WriteLine("i => " + i.ToString())
                    Dim line As String = lines(i)

                    If Not String.IsNullOrWhiteSpace(line) Then
                        If line = "[" + section + "]" Then
                            cache = True
                        End If

                        If cache Then
                            If line.Substring(0, 1) = "[" Then
                                cache = False
                            ElseIf line = key + "=" + value Then
                                lines(i) = key + "=" + value
                            End If
                        End If
                    End If
                Next
            Else
                Dim index As Integer = lines.IndexOf("[" + section + "]")

                If index > -1 Then
                    lines.Insert(index + 1, key + "=" + value)
                End If
            End If
        Else
            lines.Add("[" + section + "]")
            lines.Add(key + "=" + value)
        End If

        For Each line In lines
            Console.WriteLine(line)
        Next

        File.WriteAllLines(filePath, lines.ToArray())
        Return Read(section, key)
    End Function

    'Public Sub DeleteKey(ByVal Section As String, ByVal Key As String)
    '    WritePrivateProfileString(Section, Key, Nothing, filePath)
    'End Sub

    'Public Sub DeleteSection(ByVal Section As String)
    '    WritePrivateProfileString(Section, Nothing, Nothing, filePath)
    'End Sub
End Class