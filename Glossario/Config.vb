Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text

Public Class Config
    'Private Declare Auto Function GetPrivateProfileString Lib "kernel32" (
    '    ByVal lpApplicationName As String,
    '    ByVal lpKeyName As String,
    '    ByVal lpDefault As String,
    '    ByVal lpReturnedString As System.Text.StringBuilder,
    '    ByVal nSize As Integer,
    '    ByVal lpFileName As String
    ') As Integer

    'Private Declare Auto Function GetPrivateProfileSectionNames Lib "kernel32" (
    '    ByVal lpReturnedString As System.Text.StringBuilder,
    '    ByVal nSize As String,
    '    ByVal lpFileName As String
    ') As Integer

    'Private Declare Auto Function GetPrivateProfileSection Lib "kernel32" (
    '    ByVal lpAppName As String,
    '    ByVal lpReturnedString As System.Text.StringBuilder,
    '    ByVal nSize As String,
    '    ByVal lpFileName As String
    ') As Integer

    'Private Declare Auto Function WritePrivateProfileString Lib "kernel32" (
    '    ByVal lpApplicationName As String,
    '    ByVal lpKeyName As String,
    '    ByVal lpString As String,
    '    ByVal lpFileName As String
    ') As Integer

    Private ReadOnly filePath As String = "C:\Users\Francesco\Desktop\config.ini"

    Public Sub New()
        If Not My.Computer.FileSystem.FileExists(filePath) Then
            Dim fs As FileStream = File.Create(filePath)
            fs.Close()
        End If
    End Sub

    Private Function Test(ByVal myStringBuilder As StringBuilder) As String
        Dim fullString As String = ""

        If myStringBuilder.Length > 0 Then
            Dim chars(myStringBuilder.Length - 1) As Char
            myStringBuilder.CopyTo(0, chars, 0, myStringBuilder.Length)
            Console.WriteLine("chars => " & chars)
            fullString = New String(chars)
        End If

        Return fullString
    End Function
    Public Function GetSections() As List(Of String)
        Dim sections As New List(Of String)()
        Dim temp As New StringBuilder(255)
        Dim charsRead As Integer

        charsRead = GetPrivateProfileSectionNames(temp, 255, filePath)

        Console.WriteLine("temp.ToString() => " & Test(temp))
        'Console.WriteLine("charsRead => " & charsRead)
        'Console.WriteLine("temp[0] => " & temp(0))

        If charsRead > 0 Then
            Dim rawSections As String = Test(temp)
            sections.AddRange(rawSections.Split(New Char() {ControlChars.NullChar}, StringSplitOptions.RemoveEmptyEntries))
        End If

        Return sections
    End Function

    Public Function GetKeysAndValues(ByVal sectionName As String) As Dictionary(Of String, String)
        Dim result As New Dictionary(Of String, String)
        Dim temp As New System.Text.StringBuilder(255)
        Dim charsRead As Integer

        charsRead = GetPrivateProfileSection(sectionName, temp, 255, filePath)

        If charsRead > 0 Then
            Dim rawPairs As String = temp.ToString()
            Dim pairs As String() = rawPairs.Split(New Char() {ControlChars.NullChar}, StringSplitOptions.RemoveEmptyEntries)

            For Each pair As String In pairs
                Dim parts As String() = pair.Split(New Char() {"="c}, 2)

                If parts.Length = 2 Then
                    result.Add(parts(0), parts(1))
                ElseIf parts.Length = 1 Then
                    result.Add(parts(0), "")
                End If
            Next
        End If

        Return result
    End Function

    Public Function Read(ByVal Section As String, ByVal Key As String, ByVal DefaultValue As String) As String
        Dim temp As New System.Text.StringBuilder(255)
        GetPrivateProfileString(Section, Key, DefaultValue, temp, 255, filePath)
        Return temp.ToString()
    End Function

    Public Function ReadAll() As Dictionary(Of String, String)
        Dim sections As List(Of String) = GetSections()
        Console.WriteLine(sections(0))

        If sections.Count > 0 Then
            For Each section As String In sections
                Console.WriteLine($"[{section}]")
                Dim keysAndValues As Dictionary(Of String, String) = GetKeysAndValues(section)
                If keysAndValues.Count > 0 Then
                    For Each kvp As KeyValuePair(Of String, String) In keysAndValues
                        Console.WriteLine($"  {kvp.Key}={kvp.Value}")
                    Next
                Else
                    Console.WriteLine("  (Nessuna chiave trovata in questa sezione)")
                End If

                'Dim keysAndValues As Dictionary(Of String, String) = GetKeysAndValues(section)

                If keysAndValues.Count > 0 Then
                    Return keysAndValues
                Else
                    Return New Dictionary(Of String, String)
                End If
            Next
        Else
            Console.WriteLine("Nessuna sezione trovata nel file INI.")
        End If

        'Dim keysAndValues As Dictionary(Of String, String) = GetKeysAndValues(Section)

        'If keysAndValues.Count > 0 Then
        '    Return keysAndValues
        'Else
        '    Return New Dictionary(Of String, String)
        'End If
    End Function

    Public Function Write(ByVal Section As String, ByVal Key As String, ByVal Value As String) As String
        WritePrivateProfileString(Section, Key, Value, filePath)

        Return Read(Section, Key, Value)
    End Function

    Public Sub DeleteKey(ByVal Section As String, ByVal Key As String)
        WritePrivateProfileString(Section, Key, Nothing, filePath)
    End Sub

    Public Sub DeleteSection(ByVal Section As String)
        WritePrivateProfileString(Section, Nothing, Nothing, filePath)
    End Sub
End Class