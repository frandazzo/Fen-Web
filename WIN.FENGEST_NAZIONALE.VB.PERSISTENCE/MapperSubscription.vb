
Imports WIN.TECHNICAL.PERSISTENCE
Imports WIN.BASEREUSE
Imports WIN.FENGEST_NAZIONALE.DOMAIN.Workers
Imports WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES
Imports WIN.BASEREUSE.AbstractPersona

Public Class MapperSubscription
    Inherits AbstractRDBMapper





    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(True)
        m_IsAutoIncrementID = True
    End Sub




#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from iscrizioni"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from iscrizioni where Id = @Id"
    End Function

    Protected Overrides Function InsertStatement() As String
        Return "Insert into iscrizioni (Id_Lavoratore, " _
                & "Id_Importazione, Id_Provincia, NomeProvincia, " _
                & "Id_Regione, NomeRegione, Settore, Ente, " _
                & "Azienda, Livello, Quota , TipoPeriodo, " _
                & "Anno, Periodo, DataInizio, DataFine, " _
                & "Contratto, Piva, NomeCompleto, AnnoNascita, NomeNazioneNascita, " _
                & "NomeProvinciaNascita, NomeComuneNascita, Sesso, Struttura, Area) " _
                & "values " _
                & "(@idl, @idi, @idp, @nmp, @idr, " _
                & "@nmr, @set, @ent, @azi, @lev, @quo, " _
                & "@tpp, @ann, @per, @din, @dfi, " _
                & "@con, @piva, @nomcomp, @annnas, @nomnaz, @nomprov, @nomcom, @sex, @stru, @area )"
    End Function

    Protected Overrides Function UpdateStatement() As String
        Return "UPDATE iscrizioni SET " _
                & "Id_Lavoratore = @idl, " _
                & "Id_Importazione = @idi, " _
                & "Id_Provincia = @idp, " _
                & "NomeProvincia = @nmp, " _
                & "Id_Regione = @idr, " _
                & "NomeRegione = @nmr, " _
                & "Settore = @set, " _
                & "Ente = @ent, " _
                & "Azienda = @azi, " _
                & "Livello = @lev, " _
                & "Quota = @quo, " _
                & "TipoPeriodo = @tpp, " _
                & "Anno = @ann, " _
                & "Periodo = @per, " _
                & "DataInizio = @din, " _
                & "DataFine = @dfi, " _
                & "Contratto = @con, " _
                & "Piva = @piva, " _
                & "NomeCompleto = @nomcomp, " _
                & "AnnoNascita = @annnas, " _
                & "NomeNazioneNascita = @nomnaz, " _
                & "NomeProvinciaNascita = @nomprov, " _
                & "NomeComuneNascita = @nomcom, " _
                & "Sesso = @sex, " _
                & "Struttura = @stru, " _
                & "Area = @area " _
                & "WHERE (ID = @Id )"
    End Function

    Protected Overrides Function DeleteStatement() As String
        Return "Delete from iscrizioni where Id = @Id"
    End Function

    Protected Overrides Function FindNextKeyStatement() As String
        Return ""
    End Function

#End Region


#Region "Metodi per la ricerca dell'oggetto secondo l'id proposto"

    Public Overrides Function FindObjectById(ByVal Id As Integer) As AbstractPersistenceObject

        Return DirectCast(MyBase.FindByKey(New Key(Id)), Subscription)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try
            'recupero il registro dei mapper
            Dim Registro As PersistenceMapperRegistry = New PersistenceMapperRegistry
            Registro.SetPersistentService(m_PersistentService)

            Dim iscrizione As New Subscription
            iscrizione.Key = Key



            iscrizione.Level = IIf(rs.Item("Livello") IsNot Nothing, rs.Item("Livello"), "")
            iscrizione.EmployCompany = IIf(rs.Item("Azienda") IsNot Nothing, rs.Item("Azienda"), "")
            iscrizione.Sector = IIf(rs.Item("Settore") IsNot Nothing, rs.Item("Settore"), "")
            iscrizione.Entity = IIf(rs.Item("Ente") IsNot Nothing, rs.Item("Ente"), "")
            iscrizione.Quota = IIf(rs.Item("Quota") IsNot Nothing, rs.Item("Quota"), 0)
            iscrizione.Contract = IIf(rs.Item("Contratto") IsNot Nothing, rs.Item("Contratto"), "")


            iscrizione.Struttura = IIf(rs.Item("Struttura") IsNot Nothing, rs.Item("Struttura"), "")
            iscrizione.Area = IIf(rs.Item("Area") IsNot Nothing, rs.Item("Area"), "")



            iscrizione.FiscalCode = IIf(rs.Item("Piva") IsNot Nothing, rs.Item("Piva"), "")
            iscrizione.DenormalizedData.NomeCompleto = IIf(rs.Item("NomeCompleto") IsNot Nothing, rs.Item("NomeCompleto"), "")
            iscrizione.DenormalizedData.NomeComuneNascita = IIf(rs.Item("NomeComuneNascita") IsNot Nothing, rs.Item("NomeComuneNascita"), "")
            iscrizione.DenormalizedData.NomeProvicnciaNascita = IIf(rs.Item("NomeProvinciaNascita") IsNot Nothing, rs.Item("NomeProvinciaNascita"), "")
            iscrizione.DenormalizedData.NomeNazioneNascita = IIf(rs.Item("NomeNazioneNascita") IsNot Nothing, rs.Item("NomeNazioneNascita"), "")
            iscrizione.DenormalizedData.Sesso = IIf(rs.Item("Sesso") IsNot Nothing, rs.Item("Sesso"), "")

            iscrizione.DenormalizedData.annoNascita = IIf(rs.Item("AnnoNascita") IsNot Nothing, rs.Item("AnnoNascita"), 0)



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




            'recupero il periodo
            Dim p As PeriodType = [Enum].Parse(GetType(PeriodType), rs.Item("TipoPeriodo").ToString)

            If p = PeriodType.Interval Then
                iscrizione.Period = New SubscriptionPeriod(rs.Item("DataInizio"), rs.Item("DataFine"))
            Else
                iscrizione.Period = New SubscriptionPeriod(rs.Item("Periodo"), rs.Item("Anno"), p)
            End If


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







            iscrizione.Regione = Regione
            iscrizione.Province = PROVINCIA



            Return iscrizione
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto Iscrizione con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
    End Function


    'Protected Overrides Function LoadHashTableDataFromDatareader(ByVal rs As IDataReader) As Hashtable

    '    Dim DataHash As New Hashtable
    '    Dim int As Integer = 0
    '    For I As Int32 = 0 To rs.FieldCount - 1
    '        Dim name As String = rs.GetName(I)
    '        Dim Value As Object = Nothing


    '        'a causa di un bug nella libreria mysqldrivercs per i campi decimal e double sono
    '        'costretto a questo artifizio poichè non vengono lette le cifre dopo la virgola

    '        If name = "Quota" Then
    '            Value = New Decimal(rs.GetDecimal(int)) / 100
    '        Else
    '            Value = IIf(IsDBNull(rs(name)), Nothing, rs(name))
    '        End If

    '        DataHash.Add(name, Value)
    '        int = int + 1
    '    Next
    '    Return DataHash

    'End Function






    'Protected Overrides Function GetCommand(ByVal CommandText As String) As System.Data.IDbCommand
    '    Return DBTypeUtils.GetCommand(Me.m_PersistentService.ServiceName, CommandText, DBConnectionManager.Instance.GetCurrentConnection)
    'End Function

#End Region



    Protected Overrides Sub LoadInsertCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim iscrizione As Subscription = DirectCast(Item, Subscription)



            Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@idl"
            param.Value = iscrizione.Worker.Id
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@idi"
            param.Value = iscrizione.ParentExport.Id
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@idp"
            param.Value = iscrizione.Province.Id
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@nmp"
            param.Value = iscrizione.Province.Descrizione
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@idr"
            param.Value = iscrizione.Regione.Id
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@nmr"
            param.Value = iscrizione.Regione.Descrizione
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@set"
            If String.IsNullOrEmpty(iscrizione.Sector) Then
                param.Value = ""
            Else
                param.Value = iscrizione.Sector
            End If
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@ent"
            If String.IsNullOrEmpty(iscrizione.Entity) Then
                param.Value = ""
            Else
                param.Value = iscrizione.Entity
            End If
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@azi"
            If String.IsNullOrEmpty(iscrizione.EmployCompany) Then
                param.Value = ""
            Else
                param.Value = iscrizione.EmployCompany
            End If
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@lev"
            If String.IsNullOrEmpty(iscrizione.Level) Then
                param.Value = ""
            Else
                param.Value = iscrizione.Level
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@quo"


            param.Value = iscrizione.Quota
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@tpp"
            param.Value = iscrizione.PeriodType.ToString
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@ann"
            param.Value = iscrizione.Period.Year
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@per"
            param.Value = iscrizione.Period.PeriodNumber
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@din"
            param.Value = iscrizione.Period.InitialDate.Date
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@dfi"
            param.Value = iscrizione.Period.EndDate.Date
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@con"
            If String.IsNullOrEmpty(iscrizione.Contract) Then
                param.Value = ""
            Else
                param.Value = iscrizione.Contract
            End If
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@piva"
            If String.IsNullOrEmpty(iscrizione.FiscalCode) Then
                param.Value = ""
            Else
                param.Value = iscrizione.FiscalCode
            End If
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@nomcomp"
            param.Value = iscrizione.DenormalizedData.NomeCompleto
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@annnas"
            param.Value = iscrizione.DenormalizedData.annoNascita
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@nomnaz"
            param.Value = iscrizione.DenormalizedData.NomeNazioneNascita
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@nomprov"
            If String.IsNullOrEmpty(iscrizione.DenormalizedData.NomeProvicnciaNascita) Then
                param.Value = ""
            Else
                param.Value = iscrizione.DenormalizedData.NomeProvicnciaNascita
            End If
            Cmd.Parameters.Add(param)
        
            param = Cmd.CreateParameter
            param.ParameterName = "@nomcom"
            If String.IsNullOrEmpty(iscrizione.DenormalizedData.NomeComuneNascita) Then
                param.Value = ""
            Else
                param.Value = iscrizione.DenormalizedData.NomeComuneNascita
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@sex"
            param.Value = iscrizione.DenormalizedData.Sesso
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@stru"
            param.Value = iscrizione.Struttura
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@area"
            param.Value = iscrizione.Area
            Cmd.Parameters.Add(param)
            

        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di inserimento dell'oggetto iscrizione." & vbCrLf & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim iscrizione As Subscription = DirectCast(Item, Subscription)



            Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@idl"
            param.Value = iscrizione.Worker.Id
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@idi"
            param.Value = iscrizione.ParentExport.Id
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@idp"
            param.Value = iscrizione.Province.Id
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@nmp"
            param.Value = iscrizione.Province.Descrizione
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@idr"
            param.Value = iscrizione.Regione.Id
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@nmr"
            param.Value = iscrizione.Regione.Descrizione
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@set"
            If String.IsNullOrEmpty(iscrizione.Sector) Then
                param.Value = ""
            Else
                param.Value = iscrizione.Sector
            End If
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@ent"
            If String.IsNullOrEmpty(iscrizione.Entity) Then
                param.Value = ""
            Else
                param.Value = iscrizione.Entity
            End If
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@azi"
            If String.IsNullOrEmpty(iscrizione.EmployCompany) Then
                param.Value = ""
            Else
                param.Value = iscrizione.EmployCompany
            End If
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@lev"
            If String.IsNullOrEmpty(iscrizione.Level) Then
                param.Value = ""
            Else
                param.Value = iscrizione.Level
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@quo"
            param.Value = iscrizione.Quota
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@tpp"
            param.Value = iscrizione.PeriodType.ToString
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@ann"
            param.Value = iscrizione.Period.Year
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@per"
            param.Value = iscrizione.Period.PeriodNumber
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@din"
            param.Value = iscrizione.Period.InitialDate.Date
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@dfi"
            param.Value = iscrizione.Period.EndDate.Date
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@con"
            If String.IsNullOrEmpty(iscrizione.Contract) Then
                param.Value = ""
            Else
                param.Value = iscrizione.Contract
            End If
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@piva"
            If String.IsNullOrEmpty(iscrizione.FiscalCode) Then
                param.Value = ""
            Else
                param.Value = iscrizione.FiscalCode
            End If
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@nomcomp"
            param.Value = iscrizione.DenormalizedData.NomeCompleto
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@annnas"
            param.Value = iscrizione.DenormalizedData.annoNascita
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@nomnaz"
            param.Value = iscrizione.DenormalizedData.NomeNazioneNascita
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@nomprov"
            If String.IsNullOrEmpty(iscrizione.DenormalizedData.NomeProvicnciaNascita) Then
                param.Value = ""
            Else
                param.Value = iscrizione.DenormalizedData.NomeProvicnciaNascita
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@nomcom"
            If String.IsNullOrEmpty(iscrizione.DenormalizedData.NomeComuneNascita) Then
                param.Value = ""
            Else
                param.Value = iscrizione.DenormalizedData.NomeComuneNascita
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@sex"
            param.Value = iscrizione.DenormalizedData.Sesso
            Cmd.Parameters.Add(param)




            param = Cmd.CreateParameter
            param.ParameterName = "@stru"
            param.Value = iscrizione.Struttura
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@area"
            param.Value = iscrizione.Area
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@Id"
            param.Value = iscrizione.Key.Value(0)
            Cmd.Parameters.Add(param)

        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di aggiornamento dell'oggetto iscrizione." & vbCrLf & ex.Message)
        End Try
    End Sub
End Class


