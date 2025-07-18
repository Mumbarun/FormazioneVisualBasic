Imports System.Reflection
Imports System.Text
Imports System.Data.Entity

Public Class LocalEmployee
    Public Property EmployeeID As Integer
    Public Property LastName As String
    Public Property FirstName As String
    Public Property Title As String
    Public Property TitleOfCourtesy As String
    Public Property BirthDate As DateTime
    Public Property HireDate As DateTime
    Public Property Address As String
    Public Property City As String
    Public Property Region As String
    Public Property PostalCode As String
    Public Property Country As String
    Public Property HomePhone As String
    Public Property Extension As String
    Public Property Photo As Byte()
    Public Property Notes As String
    Public Property ReportsTo As Integer
    Public Property PhotoPath As String

    Function contains(ByVal value As Object) As Boolean
        Dim type As Type = Me.GetType()
        For Each prop As PropertyInfo In type.GetProperties(BindingFlags.Public Or BindingFlags.Instance)
            If prop.GetValue(Me).ToString() = value.ToString() Then
                Return True
            End If
        Next

        Return False
    End Function
End Class

Module Module1
    Dim employees As List(Of LocalEmployee) = New List(Of LocalEmployee)

    Public Function randomInt(ByVal max As Integer) As Integer
        Return CInt(Math.Ceiling(Rnd() * max)) + 1
    End Function

    Public Function randomString(ByVal length As Integer, Optional ByVal allowedChars As String = "") As String
        If length > 0 Then
            If String.IsNullOrEmpty(allowedChars) Then
                allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
            End If

            Dim sb As New StringBuilder()
            Dim random As New Random() ' Inizializza una nuova istanza di Random

            For i As Integer = 1 To length
                Dim randomIndex As Integer = random.Next(0, allowedChars.Length)
                sb.Append(allowedChars(randomIndex))
            Next

            Return sb.ToString()
        Else
            Return ""
        End If
    End Function

    Sub populate(ByRef arr As List(Of LocalEmployee))
        arr.Clear()

        Using context As New NorthwindEntities()
            Dim es As List(Of Employee) = context.Employees.ToList()

            For Each e As Employee In es
                Dim localE As LocalEmployee = New LocalEmployee

                Dim type As Type = GetType(Employee)
                Dim localType As Type = localE.GetType()

                For Each prop As PropertyInfo In type.GetProperties(BindingFlags.Public Or BindingFlags.Instance)
                    Dim localProp As PropertyInfo = localType.GetProperty(prop.Name)

                    If localProp IsNot Nothing Then
                        localProp.SetValue(localE, prop.GetValue(e))
                    End If
                Next

                employees.Add(localE)
            Next
        End Using

        'For i As Integer = 1 To 100
        '    Dim cache As LocalEmployee = New LocalEmployee

        '    cache.EmployeeID = i
        '    cache.LastName = randomString(20)
        '    cache.FirstName = randomString(20)
        '    cache.Title = randomString(20)
        '    cache.TitleOfCourtesy = randomString(20)
        '    cache.BirthDate = randomString(20)
        '    cache.HireDate = randomString(20)
        '    cache.Address = randomString(20)
        '    cache.City = randomString(20)
        '    cache.Region = randomString(20)
        '    cache.PostalCode = randomString(20)
        '    cache.Country = randomString(20)
        '    cache.HomePhone = randomString(20)
        '    cache.Extension = randomString(20)
        '    cache.Photo = randomString(20)
        '    cache.Notes = randomString(20)
        '    cache.ReportsTo = randomInt(100)
        '    cache.PhotoPath = randomString(20)

        '    arr.Add(cache)
        'Next
    End Sub

    Sub reorder(ByRef arr As List(Of LocalEmployee), ByVal key As String, Optional ByVal ascending As Boolean = True)
        If String.IsNullOrWhiteSpace(key) Then
            If ascending Then
                arr = (From e In arr
                       Order By e.EmployeeID
                       Select e).ToList()
            Else
                arr = (From e In arr
                       Order By e.EmployeeID Descending
                       Select e).ToList()
            End If
        Else
            Dim prop As PropertyInfo = GetType(LocalEmployee).GetProperty(key)

            If prop IsNot Nothing Then
                If ascending Then
                    arr = (From e In arr
                           Order By prop.GetValue(e, Nothing)
                           Select e).ToList()
                Else
                    arr = (From e In arr
                           Order By prop.GetValue(e, Nothing) Descending
                           Select e).ToList()
                End If
            End If
        End If
    End Sub

    Function getElement(ByVal arr As List(Of LocalEmployee), ByVal key As String, ByVal value As Object) As LocalEmployee
        Dim res As LocalEmployee = New LocalEmployee

        Dim prop As PropertyInfo = res.GetType().GetProperty(key)
        If prop IsNot Nothing Then
            res = (From e In arr
                   Where prop.GetValue(e) = value
                   Select e).FirstOrDefault()
        End If

        Return res
    End Function

    Function getElementByVal(ByVal arr As List(Of LocalEmployee), ByVal value As Object) As LocalEmployee
        Dim res As LocalEmployee = Nothing

        res = (From e In arr
               Where e.contains(value)
               Select e).FirstOrDefault()

        Return res
    End Function

    Function ifElementExists(ByVal arr As List(Of LocalEmployee), ByVal key As String, ByVal value As Object) As Boolean
        Dim prop As PropertyInfo = GetType(LocalEmployee).GetProperty(key)

        Dim n As Integer = (From e In arr
                            Where prop.GetValue(e) = value
                            Select e).Count

        Return n > 0
    End Function

    Sub printList(ByVal arr As List(Of LocalEmployee))
        For Each e As LocalEmployee In arr
            printElement(e)
        Next
    End Sub

    Sub printListByKey(ByVal arr As List(Of LocalEmployee), ByVal key As String)
        For Each e As LocalEmployee In arr
            printElementbyKey(e, key)
        Next
    End Sub

    Sub printElement(ByVal e As LocalEmployee)
        Dim type As Type = e.GetType()

        Console.WriteLine("---")

        For Each prop As PropertyInfo In type.GetProperties(BindingFlags.Public Or BindingFlags.Instance)
            Console.WriteLine($"{prop.Name} => {prop.GetValue(e)}")
        Next

        Console.WriteLine("")
    End Sub

    Sub printElementbyKey(ByVal e As LocalEmployee, ByVal key As String)
        Dim type As Type = e.GetType()
        Dim prop As PropertyInfo = type.GetProperty(key)

        Console.WriteLine("---")

        If prop IsNot Nothing Then
            Console.WriteLine($"{prop.Name} => {prop.GetValue(e)}")
        End If

        Console.WriteLine("")
    End Sub

    Sub Main()
        populate(employees)

        reorder(employees, "EmployeeID", False)

        printList(employees)

        Console.ReadKey()

        printElement(getElement(employees, "EmployeeID", 3))

        Console.ReadKey()

        'printElement(getElementByVal(employees, 18))

        'Console.ReadKey()

        printListByKey(employees, "FirstName")

        Console.ReadKey()

        Console.WriteLine($"Numero dipendenti: {employees.Count}")

        Console.ReadKey()

        Console.WriteLine($"Esiste il dipendente con nome 'Gino'? {ifElementExists(employees, "FirstName", "Gino")}")
        Console.WriteLine($"Esiste il dipendente con come 'Aieie'? {ifElementExists(employees, "FirstName", "Aieie")}")

        Console.ReadKey()
    End Sub
End Module
