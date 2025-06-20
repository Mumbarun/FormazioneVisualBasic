Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text

Public Class Config
    Private ReadOnly filePath As String = "C:\Users\Francesco\Desktop\config.ini"

    Public Sub New()
        If Not My.Computer.FileSystem.FileExists(filePath) Then
            Dim fs As FileStream = File.Create(filePath)
            fs.Close()
        End If
    End Sub

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