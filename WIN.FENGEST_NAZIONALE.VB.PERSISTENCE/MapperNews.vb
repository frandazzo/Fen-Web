Imports WIN.TECHNICAL.PERSISTENCE
Imports WIN.BASEREUSE
Imports WIN.FENGEST_NAZIONALE.DOMAIN.Public

Public Class MapperNews
    Inherits AbstractRDBMapper

    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(True)
        m_IsAutoIncrementID = True
    End Sub




#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from news"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from news where Id = @Id"
    End Function

    Protected Overrides Function InsertStatement() As String
        Return "Insert into news (Data, Titolo, Testo, InseritoDa) " _
                & "values (@dat, @tit, @tes, @ins)"
    End Function

    Protected Overrides Function UpdateStatement() As String
        Return "UPDATE news SET " _
                & "Data = @dat, " _
                & "Titolo = @tit, " _
                & "Testo = @tes, " _
                & "InseritoDa = @ins WHERE (ID = @Id )"
    End Function

    Protected Overrides Function DeleteStatement() As String
        Return "Delete from news where Id = @Id"
    End Function

    Protected Overrides Function FindNextKeyStatement() As String
        Return ""
    End Function

#End Region


#Region "Metodi per la ricerca dell'oggetto secondo l'id proposto"

    Public Overrides Function FindObjectById(ByVal Id As Integer) As AbstractPersistenceObject

        Return DirectCast(MyBase.FindByKey(New Key(Id)), News)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try

            Dim d As DateTime = IIf(rs.Item("Data") IsNot Nothing, rs.Item("Data"), DateTime.Now.Date)
            Dim tit As String = IIf(rs.Item("Titolo") IsNot Nothing, rs.Item("Titolo"), "")
            Dim tes As String = IIf(rs.Item("Testo") IsNot Nothing, rs.Item("Testo"), "")
            Dim ins As String = IIf(rs.Item("InseritoDa") IsNot Nothing, rs.Item("InseritoDa"), "")

            Dim news1 As New News()
            news1.DataCreazione = d
            news1.Titolo = tit
            news1.Testo = tes
            news1.CreatoDa = ins

            news1.Key = Key

            Return news1
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto news con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
    End Function

    'Protected Overrides Function GetCommand(ByVal CommandText As String) As System.Data.IDbCommand
    '    Return DBTypeUtils.GetCommand(Me.m_PersistentService.ServiceName, CommandText, DBConnectionManager.Instance.GetCurrentConnection)
    'End Function

#End Region


    Protected Overrides Sub LoadInsertCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim news1 As News = DirectCast(Item, News)

            Dim param As IDbDataParameter = Cmd.CreateParameter

            param.ParameterName = "@dat"
            param.Value = news1.DataCreazione
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@tit"
            If String.IsNullOrEmpty(news1.Titolo) Then
                param.Value = ""
            Else
                param.Value = news1.Titolo
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@tes"
            If String.IsNullOrEmpty(news1.Testo) Then
                param.Value = ""
            Else
                param.Value = news1.Testo
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@ins"
            If String.IsNullOrEmpty(news1.CreatoDa) Then
                param.Value = ""
            Else
                param.Value = news1.CreatoDa
            End If
            Cmd.Parameters.Add(param)

        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di inserimento dell'oggetto news ." & vbCrLf & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim news1 As News = DirectCast(Item, News)

            Dim param As IDbDataParameter = Cmd.CreateParameter

            param.ParameterName = "@dat"
            param.Value = news1.DataCreazione
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@tit"
            If String.IsNullOrEmpty(news1.Titolo) Then
                param.Value = ""
            Else
                param.Value = news1.Titolo
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@tes"
            If String.IsNullOrEmpty(news1.Testo) Then
                param.Value = ""
            Else
                param.Value = news1.Testo
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@ins"
            If String.IsNullOrEmpty(news1.CreatoDa) Then
                param.Value = ""
            Else
                param.Value = news1.CreatoDa
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@Id"
            param.Value = news1.Key.Value(0)
            Cmd.Parameters.Add(param)

        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di aggiornamento dell'oggetto news ." & vbCrLf & ex.Message)
        End Try
    End Sub
End Class
