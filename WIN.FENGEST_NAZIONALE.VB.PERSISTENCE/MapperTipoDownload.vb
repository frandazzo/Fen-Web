Imports WIN.TECHNICAL.PERSISTENCE
Imports WIN.FENGEST_NAZIONALE.DOMAIN.Public
Imports WIN.BASEREUSE

Public Class MapperTipoDownload
    Inherits AbstractRDBMapper


    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(True)
        m_IsAutoIncrementID = True
    End Sub




#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from tipo_download"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from tipo_download where Id = @Id"
    End Function

    Protected Overrides Function InsertStatement() As String
        Return "Insert into tipo_download (Descrizione) " _
                & "values (@desc)"
    End Function

    Protected Overrides Function UpdateStatement() As String
        Return "UPDATE tipo_download SET " _
                & "Descrizione = @desc WHERE (ID = @Id )"
    End Function

    Protected Overrides Function DeleteStatement() As String
        Return "Delete from tipo_download where Id = @Id"
    End Function

    Protected Overrides Function FindNextKeyStatement() As String
        Return ""
    End Function

#End Region


#Region "Metodi per la ricerca dell'oggetto secondo l'id proposto"

    Public Overrides Function FindObjectById(ByVal Id As Integer) As AbstractPersistenceObject

        Return DirectCast(MyBase.FindByKey(New Key(Id)), TipoDownload)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try

            Dim tipo As New TipoDownload()
            tipo.Descrizione = rs.Item("Descrizione").ToString()

            tipo.Key = Key

            Return tipo
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto tipo_download con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
    End Function

    'Protected Overrides Function GetCommand(ByVal CommandText As String) As System.Data.IDbCommand
    '    Return DBTypeUtils.GetCommand(Me.m_PersistentService.ServiceName, CommandText, DBConnectionManager.Instance.GetCurrentConnection)
    'End Function

#End Region


    Protected Overrides Sub LoadInsertCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim tipo As TipoDownload = DirectCast(Item, TipoDownload)

            Dim param As IDbDataParameter = Cmd.CreateParameter

            param.ParameterName = "@desc"
            param.Value = tipo.Descrizione
            Cmd.Parameters.Add(param)

        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di inserimento dell'oggetto tipo_download ." & vbCrLf & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim tipo As TipoDownload = DirectCast(Item, TipoDownload)

            Dim param As IDbDataParameter = Cmd.CreateParameter

            param.ParameterName = "@desc"
            param.Value = tipo.Descrizione
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@Id"
            param.Value = tipo.Key.Value(0)
            Cmd.Parameters.Add(param)

        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di aggiornamento dell'oggetto tipo_download ." & vbCrLf & ex.Message)
        End Try
    End Sub
End Class
