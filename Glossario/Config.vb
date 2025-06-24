Imports System.Collections.Specialized.BitVector32
Imports System.IO

Public Class Config
    Private filePath As String

    Public Sub New(ByVal fp As String)
        If String.IsNullOrWhiteSpace(fp) Then
            'Read from command line arguments
            For Each line As String In Environment.GetCommandLineArgs()
                Dim cache(1) As String
                cache = Split(line, "=", 2)

                If cache(0) = "/PATH" Then
                    filePath = cache(1)
                End If
            Next
        Else
            'Read from class constructor
            filePath = fp
        End If

        'Create the config file if not exists at filePath
        If Not My.Computer.FileSystem.FileExists(filePath) Then
            Dim fs As FileStream = File.Create(filePath)
            fs.Close()
        End If
    End Sub

    Public Function getSections() As List(Of String)
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
                If cache Then
                    If line.Substring(0, 1) = "[" Then
                        cache = False
                    Else
                        res.Add(line)
                    End If
                End If

                If line = "[" + section + "]" Then
                    cache = True
                End If
            End If
        Next

        Return res
    End Function

    Private Function getLine(ByVal lines As String(), ByVal key As String) As String
        Dim res As String = ""

        For Each line As String In lines
            If Not String.IsNullOrWhiteSpace(line) Then
                If line.Contains("=") Then
                    Dim parts(1) As String
                    parts = Split(line, "=", 2)

                    If key = parts(0) Then
                        res = line
                    End If
                End If
            End If
        Next

        Return res
    End Function

    Public Function Read(ByVal section As String, ByVal key As String) As String
        If Not section = "" Then
            Return getLine(File.ReadAllLines(filePath), key)
        Else
            Dim lines As String() = getLinesBySection(section).ToArray()

            Return getLine(lines, key)
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

        Dim sections As List(Of String) = getSections()

        'Check if it must create a "section" or not
        If sections.Contains(section) Then
            'The "section" already exists, now is checking if the key is already in the config file

            If Not String.IsNullOrWhiteSpace(getLine(getLinesBySection(section).ToArray(), key)) Then
                For i As Integer = 0 To (lines.Count - 1)
                    If lines(i).Contains("=") Then
                        Dim cache(1) As String
                        cache = Split(lines(i), "=", 2)

                        If key = cache(0) And Not value = cache(1) Then
                            lines(i) = key + "=" + value
                        End If
                    End If
                Next
            Else
                'Create a "section" with the new value
                Dim index As Integer = lines.IndexOf("[" + section + "]")

                If index > -1 Then
                    lines.Insert(index + 1, key + "=" + value)
                End If
            End If
        Else
            lines.Add("[" + section + "]")
            lines.Add(key + "=" + value)
        End If

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