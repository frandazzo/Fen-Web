Imports WIN.TECHNICAL.PERSISTENCE
Imports WIN.BASEREUSE
Imports WIN.FENGEST_NAZIONALE.DOMAIN.Public

Public Class MapperContatti
    Inherits AbstractRDBMapper


    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(True)
        m_IsAutoIncrementID = True
    End Sub




#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from contatti"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from contatti where Id = @Id"
    End Function

    Protected Overrides Function InsertStatement() As String
        Return "Insert into contatti (Mail) " _
                & "values (@mail)"
    End Function

    Protected Overrides Function UpdateStatement() As String
        Return "UPDATE contatti SET " _
                & "Mail = @mail WHERE (ID = @Id )"
    End Function

    Protected Overrides Function DeleteStatement() As String
        Return "Delete from contatti where Id = @Id"
    End Function

    Protected Overrides Function FindNextKeyStatement() As String
        Return ""
    End Function

#End Region


#Region "Metodi per la ricerca dell'oggetto secondo l'id proposto"

    Public Overrides Function FindObjectById(ByVal Id As Integer) As AbstractPersistenceObject

        Return DirectCast(MyBase.FindByKey(New Key(Id)), Contatti)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try

            Dim mail As String = IIf(rs.Item("Mail") IsNot Nothing, rs.Item("Mail"), "")

            Dim con As New Contatti()
            con.Mail = mail

            'Dim down As New Download(d, desc, per, ins)

            con.Key = Key

            Return con
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto contatti con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
    End Function

    'Protected Overrides Function GetCommand(ByVal CommandText As String) As System.Data.IDbCommand
    '    Return DBTypeUtils.GetCommand(Me.m_PersistentService.ServiceName, CommandText, DBConnectionManager.Instance.GetCurrentConnection)
    'End Function

#End Region


    Protected Overrides Sub LoadInsertCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim con As Contatti = DirectCast(Item, Contatti)

            Dim param As IDbDataParameter = Cmd.CreateParameter

            param.ParameterName = "@mail"
            If String.IsNullOrEmpty(con.Mail) Then
                param.Value = ""
            Else
                param.Value = con.Mail
            End If
            Cmd.Parameters.Add(param)

        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di inserimento dell'oggetto contatti ." & vbCrLf & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim con As Contatti = DirectCast(Item, Contatti)

            Dim param As IDbDataParameter = Cmd.CreateParameter

            param.ParameterName = "@mail"
            If String.IsNullOrEmpty(con.Mail) Then
                param.Value = ""
            Else
                param.Value = con.Mail
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@Id"
            param.Value = con.Key.Value(0)
            Cmd.Parameters.Add(param)

        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di aggiornamento dell'oggetto contatti ." & vbCrLf & ex.Message)
        End Try
    End Sub
End Class
