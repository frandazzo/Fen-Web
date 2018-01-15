

Imports WIN.TECHNICAL.PERSISTENCE
Imports WIN.BASEREUSE
Imports WIN.FENGEST_NAZIONALE.DOMAIN.ValidationSubsystem
Imports WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES
Imports WIN.BASEREUSE.AbstractPersona
Imports WIN.FENGEST_NAZIONALE.DOMAIN.Workers

Public Class MapperDocument
    Inherits AbstractRDBMapper





    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(True)
        m_IsAutoIncrementID = True
    End Sub




#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from documenti"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from documenti where Id = @Id"
    End Function

    Protected Overrides Function InsertStatement() As String
        Return "Insert into documenti (Id_Lavoratore, Id_Importazione, Id_Provincia, NomeProvincia, " _
                & "DataDocumento, TipoDocumento, Stato, Notes, Id_Regione, NomeRegione,Struttura, Area, Id_Comune, NomeComune) " _
                & "values (@idl, @idi, @idp, @nmp, @dat, @tpd, @sta, @not, @idr, @nmr, @stru, @area, @idc, @nmc)"
    End Function

    Protected Overrides Function UpdateStatement() As String
        Return "UPDATE documenti SET " _
                & "Id_Lavoratore = @idl, " _
                & "Id_Importazione = @idi, " _
                & "Id_Provincia = @idp, " _
                & "NomeProvincia = @nmp, " _
                & "DataDocumento = @dat, " _
                & "TipoDocumento = @tpd, " _
                & "Stato = @sta, " _
                & "Notes = @not, " _
                & "Id_Regione = @idr, " _
                & "NomeRegione = @nmr, " _
                & "Struttura = @stru, " _
                & "Area = @area," _
                & "Id_Comune = @idc, " _
                & "NomeComune = @nmc " _
                & "WHERE (ID = @Id )"
    End Function

    Protected Overrides Function DeleteStatement() As String
        Return "Delete from documenti where Id = @Id"
    End Function

    Protected Overrides Function FindNextKeyStatement() As String
        Return ""
    End Function

#End Region


#Region "Metodi per la ricerca dell'oggetto secondo l'id proposto"

    Public Overrides Function FindObjectById(ByVal Id As Integer) As AbstractPersistenceObject

        Return DirectCast(MyBase.FindByKey(New Key(Id)), Document)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try
            Dim iscrizione As New Document
            iscrizione.Key = Key


            iscrizione.Struttura = IIf(rs.Item("Struttura") IsNot Nothing, rs.Item("Struttura"), "")
            iscrizione.Area = IIf(rs.Item("Area") IsNot Nothing, rs.Item("Area"), "")

            iscrizione.DocumentDate = IIf(rs.Item("DataDocumento") IsNot Nothing, rs.Item("DataDocumento"), DateTime.Now.Date)
            iscrizione.DocumentType = IIf(rs.Item("TipoDocumento") IsNot Nothing, rs.Item("TipoDocumento"), "")
            iscrizione.State = IIf(rs.Item("Stato") IsNot Nothing, rs.Item("Stato"), "")
            iscrizione.Notes = IIf(rs.Item("Notes") IsNot Nothing, rs.Item("Notes"), "")


            Dim ID_COMUNE As Int32 = IIf(rs.Item("Id_Comune") IsNot Nothing, rs.Item("Id_Comune"), -1)
            Dim Nome_Comune As String = IIf(rs.Item("NomeComune") IsNot Nothing, rs.Item("NomeComune"), "")

            Dim COMUNE As Comune

            If (ID_COMUNE = -1) Then
                COMUNE = New ComuneNullo
            Else
                COMUNE = New Comune
                COMUNE.Key = New Key(ID_COMUNE)
                COMUNE.Descrizione = Nome_Comune
            End If
            iscrizione.Comune = COMUNE

            'recupero il registro dei mapper
            Dim Registro As PersistenceMapperRegistry = New PersistenceMapperRegistry
            Registro.SetPersistentService(m_PersistentService)

            'recupero l'utente
            Dim MapperWorker As MapperWorker = Registro.GetMapperByName("MapperWorker")

            Dim ID_WORKER As Int32 = IIf(rs.Item("Id_Lavoratore") IsNot Nothing, rs.Item("Id_Lavoratore"), -1)
            Dim WORKER As Worker = IIf(ID_WORKER = -1, Nothing, MapperWorker.FindObjectById(ID_WORKER))

            iscrizione.Worker = WORKER


            'recupero l'importazione di riferimento
            Dim MapperExport As MapperExport = Registro.GetMapperByName("MapperExport")

            Dim ID_Import As Int32 = IIf(rs.Item("Id_Importazione") IsNot Nothing, rs.Item("Id_Importazione"), -1)
            Dim Export As Export = IIf(ID_Import = -1, Nothing, MapperExport.FindObjectById(ID_Import))

            iscrizione.ParentExport = Export





            'materializzo la nazione in questo modo per 
            'evitare numerose query e velocizzare il processo di materializzazione




            Dim ID_PROVINCIA As Int32 = IIf(rs.Item("Id_Provincia") IsNot Nothing, rs.Item("Id_Provincia"), -1)
            Dim Nome_Provincia As String = IIf(rs.Item("NomeProvincia") IsNot Nothing, rs.Item("NomeProvincia"), "")

            Dim PROVINCIA As Provincia

            If (ID_PROVINCIA = -1) Then
                PROVINCIA = New ProvinciaNulla
            Else
                PROVINCIA = New Provincia
                PROVINCIA.Key = New Key(ID_PROVINCIA)
                PROVINCIA.Descrizione = Nome_Provincia
            End If


            Dim ID_REGIONE As Int32 = IIf(rs.Item("Id_Regione") IsNot Nothing, rs.Item("Id_Regione"), -1)
            Dim Nome_Regione As String = IIf(rs.Item("NomeRegione") IsNot Nothing, rs.Item("NomeRegione"), "")

            Dim Regione As Regione

            If (ID_REGIONE = -1) Then
                Regione = New RegioneNulla
            Else
                Regione = New Regione
                Regione.Key = New Key(ID_REGIONE)
                Regione.Descrizione = Nome_Regione
            End If









            iscrizione.Province = PROVINCIA
            iscrizione.Regione = Regione


            Return iscrizione
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto documento con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
    End Function









    'Protected Overrides Function GetCommand(ByVal CommandText As String) As System.Data.IDbCommand
    '    Return DBTypeUtils.GetCommand(Me.m_PersistentService.ServiceName, CommandText, DBConnectionManager.Instance.GetCurrentConnection)
    'End Function

#End Region



    Protected Overrides Sub LoadInsertCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim doc As Document = DirectCast(Item, Document)



            Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@idl"
            param.Value = doc.Worker.Id
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@idi"
            param.Value = doc.ParentExport.Id
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@idp"
            param.Value = doc.Province.Id
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@nmp"
            param.Value = doc.Province.Descrizione
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@dat"
            param.Value = doc.DocumentDate
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@tpd"
            If String.IsNullOrEmpty(doc.DocumentType) Then
                param.Value = ""
            Else
                param.Value = doc.DocumentType
            End If
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@sta"
            If String.IsNullOrEmpty(doc.State) Then
                param.Value = ""
            Else
                param.Value = doc.State
            End If
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@not"
            If String.IsNullOrEmpty(doc.Notes) Then
                param.Value = ""
            Else
                param.Value = doc.Notes
            End If
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@idr"
            param.Value = doc.Regione.Id
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@nmr"
            param.Value = doc.Regione.Descrizione
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@stru"
            param.Value = doc.Struttura
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@area"
            param.Value = doc.Area
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@idc"
            If doc.Comune.Id = -1 Then
                param.Value = DBNull.Value
            Else
                param.Value = doc.Comune.Id
            End If
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@nmc"
            If doc.Comune.Id = -1 Then
                param.Value = ""
            Else
                param.Value = doc.Comune.Descrizione
            End If
            Cmd.Parameters.Add(param)



        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di inserimento dell'oggetto documento." & vbCrLf & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim doc As Document = DirectCast(Item, Document)



            Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@idl"
            param.Value = doc.Worker.Id
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@idi"
            param.Value = doc.ParentExport.Id
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@idp"
            param.Value = doc.Province.Id
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@nmp"
            param.Value = doc.Province.Descrizione
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@dat"
            param.Value = doc.DocumentDate
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@tpd"
            If String.IsNullOrEmpty(doc.DocumentType) Then
                param.Value = ""
            Else
                param.Value = doc.DocumentType
            End If
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@sta"
            If String.IsNullOrEmpty(doc.State) Then
                param.Value = ""
            Else
                param.Value = doc.State
            End If
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@not"
            If String.IsNullOrEmpty(doc.Notes) Then
                param.Value = ""
            Else
                param.Value = doc.Notes
            End If
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@idr"
            param.Value = doc.Regione.Id
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@nmr"
            param.Value = doc.Regione.Descrizione
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@stru"
            param.Value = doc.Struttura
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@area"
            param.Value = doc.Area
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@idc"
            If doc.Comune.Id = -1 Then
                param.Value = DBNull.Value
            Else
                param.Value = doc.Comune.Id
            End If
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@nmc"
            If doc.Comune.Id = -1 Then
                param.Value = ""
            Else
                param.Value = doc.Comune.Descrizione
            End If
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@Id"
            param.Value = doc.Key.Value(0)
            Cmd.Parameters.Add(param)




        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di aggiornamento dell'oggetto iscrizione." & vbCrLf & ex.Message)
        End Try
    End Sub
End Class


