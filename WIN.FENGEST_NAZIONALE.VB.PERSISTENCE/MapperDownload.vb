Imports WIN.TECHNICAL.PERSISTENCE
Imports WIN.BASEREUSE
Imports WIN.FENGEST_NAZIONALE.DOMAIN.Public


Public Class MapperDownload
    Inherits AbstractRDBMapper





    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(True)
        m_IsAutoIncrementID = True
    End Sub




#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from download"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from download where Id = @Id"
    End Function

    Protected Overrides Function InsertStatement() As String
        Return "Insert into download (Data, Descrizione, Percorso, InseritoDa, Tipo, Id_Regione, NomeRegione) " _
                & "values (@dat, @desc, @per, @ins, @tipo, @idReg, @descReg)"
    End Function

    Protected Overrides Function UpdateStatement() As String
        Return "UPDATE download SET " _
                & "Data = @dat, " _
                & "Descrizione = @desc, " _
                & "Percorso = @per, " _
                & "InseritoDa = @ins, " _
                & "Tipo = @tipo, " _
                & "Id_Regione = @idReg, " _
                & "NomeRegione = @descReg WHERE (ID = @Id )"
    End Function

    Protected Overrides Function DeleteStatement() As String
        Return "Delete from download where Id = @Id"
    End Function

    Protected Overrides Function FindNextKeyStatement() As String
        Return ""
    End Function

#End Region


#Region "Metodi per la ricerca dell'oggetto secondo l'id proposto"

    Public Overrides Function FindObjectById(ByVal Id As Integer) As AbstractPersistenceObject

        Return DirectCast(MyBase.FindByKey(New Key(Id)), Download)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try

            'recupero il registro dei mapper
            Dim Registro As PersistenceMapperRegistry = New PersistenceMapperRegistry
            Registro.SetPersistentService(m_PersistentService)

            Dim d As DateTime = IIf(rs.Item("Data") IsNot Nothing, rs.Item("Data"), DateTime.Now.Date)
            Dim desc As String = IIf(rs.Item("Descrizione") IsNot Nothing, rs.Item("Descrizione"), "")
            Dim per As String = IIf(rs.Item("Percorso") IsNot Nothing, rs.Item("Percorso"), "")
            Dim ins As String = IIf(rs.Item("InseritoDa") IsNot Nothing, rs.Item("InseritoDa"), "")

            Dim MapperTipoDownload As MapperTipoDownload = Registro.GetMapperByName("MapperTipoDownload")
            Dim IdTipo As Int32 = IIf(rs.Item("Tipo") IsNot Nothing, rs.Item("Tipo"), -1)
            Dim tipo As TipoDownload = IIf(IdTipo = -1, Nothing, MapperTipoDownload.FindObjectById(IdTipo))

            Dim idReg As Int32 = IIf(rs.Item("Id_Regione") IsNot Nothing, rs.Item("Id_Regione"), -1)
            Dim descReg As String = IIf(rs.Item("NomeRegione") IsNot Nothing, rs.Item("NomeRegione"), "")

            Dim reg As Regione

            If (idReg = -1) Then
                reg = New RegioneNulla
            Else
                reg = New Regione
                reg.Key = New Key(idReg)
                reg.Descrizione = descReg
            End If

            Dim down As New Download()
            down.DataCreazione = d
            down.Descrizione = desc
            down.Percorso = per
            down.CreatoDa = ins
            down.Tipo = tipo
            down.Regione = reg
            'Dim down As New Download(d, desc, per, ins)

            down.Key = Key

            Return down
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto download con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
    End Function

    'Protected Overrides Function GetCommand(ByVal CommandText As String) As System.Data.IDbCommand
    '    Return DBTypeUtils.GetCommand(Me.m_PersistentService.ServiceName, CommandText, DBConnectionManager.Instance.GetCurrentConnection)
    'End Function

#End Region


    Protected Overrides Sub LoadInsertCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim doc As Download = DirectCast(Item, Download)

            Dim param As IDbDataParameter = Cmd.CreateParameter

            param.ParameterName = "@dat"
            param.Value = doc.DataCreazione
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@desc"
            If String.IsNullOrEmpty(doc.Descrizione) Then
                param.Value = ""
            Else
                param.Value = doc.Descrizione
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@per"
            If String.IsNullOrEmpty(doc.Percorso) Then
                param.Value = ""
            Else
                param.Value = doc.Percorso
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@ins"
            If String.IsNullOrEmpty(doc.CreatoDa) Then
                param.Value = ""
            Else
                param.Value = doc.CreatoDa
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@tipo"
            param.Value = doc.Tipo.Id.ToString()
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@idReg"
            If doc.Regione.Id = -1 Then
                param.Value = DBNull.Value
            Else
                param.Value = doc.Regione.Id
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@descReg"
            If doc.Regione.Descrizione = "" Then
                param.Value = DBNull.Value
            Else
                param.Value = doc.Regione.Descrizione
            End If
            Cmd.Parameters.Add(param)


        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di inserimento dell'oggetto download ." & vbCrLf & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim doc As Download = DirectCast(Item, Download)

            Dim param As IDbDataParameter = Cmd.CreateParameter

            param.ParameterName = "@dat"
            param.Value = doc.DataCreazione
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@desc"
            If String.IsNullOrEmpty(doc.Descrizione) Then
                param.Value = ""
            Else
                param.Value = doc.Descrizione
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@per"
            If String.IsNullOrEmpty(doc.Percorso) Then
                param.Value = ""
            Else
                param.Value = doc.Percorso
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@ins"
            If String.IsNullOrEmpty(doc.CreatoDa) Then
                param.Value = ""
            Else
                param.Value = doc.CreatoDa
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@tipo"
            param.Value = doc.Tipo.Id.ToString()
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@idReg"
            If doc.Regione.Id = -1 Then
                param.Value = DBNull.Value
            Else
                param.Value = doc.Regione.Id
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@descReg"
            If doc.Regione.Descrizione = "" Then
                param.Value = DBNull.Value
            Else
                param.Value = doc.Regione.Descrizione
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@Id"
            param.Value = doc.Key.Value(0)
            Cmd.Parameters.Add(param)

        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di aggiornamento dell'oggetto download ." & vbCrLf & ex.Message)
        End Try
    End Sub
End Class
