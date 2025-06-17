Imports System.IO
Imports System.Runtime.InteropServices

'Public Class Config
'    Private ReadOnly filePath As String = "config.ini"

'    Public Function Read(ByVal key As String) As String
'        Dim line As String
'        'Dim reader As StreamReader = My.Computer.FileSystem.OpenTextFileReader(filePath)

'        'Using reader As StreamReader = New StreamReader(filePath)
'        '    line = reader.ReadLine
'        'End Using
'        line = key

'        Return line
'    End Function
'End Class

'Public Class Config
'    Private ReadOnly filePath As String = "config.ini"

'    <DllImport("kernel32", CharSet:=CharSet.Unicode, SetLastError:=True)>
'    Private Shared Function WritePrivateProfileString(
'        section As String,
'        key As String,
'        value As String,
'        filePath As String) As Boolean
'    End Function

'    <DllImport("kernel32", CharSet:=CharSet.Unicode)>
'    Private Shared Function GetPrivateProfileString(
'        section As String,
'        key As String,
'        defaultValue As String,
'        returnValue As StringBuilder,
'        size As Integer,
'        filePath As String) As Integer
'    End Function

'    Public Sub Write(section As String, key As String, value As String)
'        WritePrivateProfileString(section, key, value, filePath)
'    End Sub

'    Public Function Read(section As String, key As String, Optional defaultValue As String = "") As String
'        Dim ret As New StringBuilder(255)
'        GetPrivateProfileString(section, key, defaultValue, ret, ret.Capacity, filePath)
'        Return ret.ToString()
'    End Function
'End Class

Public Class Config
    <DllImport("kernel32.dll", EntryPoint:="GetPrivateProfileStringA")>
    Private Shared Function GetPrivateProfileString(
        ByVal lpApplicationName As String,
        ByVal lpKeyName As String,
        ByVal lpDefault As String,
        ByVal lpReturnedString As System.Text.StringBuilder,
        ByVal nSize As Integer,
        ByVal lpFileName As String
    ) As Integer
    End Function

    <DllImport("kernel32.dll", EntryPoint:="WritePrivateProfileStringA")>
    Private Shared Function WritePrivateProfileString(
        ByVal lpApplicationName As String,
        ByVal lpKeyName As String,
        ByVal lpString As String,
        ByVal lpFileName As String
    ) As Integer
    End Function

    Private ReadOnly filePath As String = "C:\Users\Francesco\Desktop\config.ini"

    Public Sub New()
        If Not My.Computer.FileSystem.FileExists(filePath) Then
            Dim fs As FileStream = File.Create(filePath)
            fs.Close()
        End If
    End Sub

    Public Function Read(ByVal Section As String, ByVal Key As String, ByVal DefaultValue As String) As String
        Dim temp As New System.Text.StringBuilder(255)
        GetPrivateProfileString(Section, Key, DefaultValue, temp, 255, filePath)
        Return temp.ToString()
    End Function

    Public Function Write(ByVal Section As String, ByVal Key As String, ByVal Value As String) As String
        WritePrivateProfileString(Section, Key, Value, filePath)

        Return Read(Section, Key, Value)
    End Function

    Public Function DeleteKey(ByVal Section As String, ByVal Key As String)
        WritePrivateProfileString(Section, Key, Nothing, filePath)
    End Function

    Public Function DeleteSection(ByVal Section As String)
        WritePrivateProfileString(Section, Nothing, Nothing, filePath)
    End Function
End Class