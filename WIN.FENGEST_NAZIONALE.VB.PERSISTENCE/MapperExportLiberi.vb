Imports WIN.FENGEST_NAZIONALE.DOMAIN.Workers
Imports WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES

Public Class MapperExportLiberi
    Inherits AbstractRDBMapper





    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(True)
        m_IsAutoIncrementID = True
    End Sub




#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from importazioni_liberi"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from importazioni_liberi where Id = @Id"
    End Function

    Protected Overrides Function InsertStatement() As String
        Return "Insert into importazioni_liberi (Id_Provincia, " _
                        & "NomeProvincia, Id_Regione, NomeRegione, " _
                        & "DataEsportazione, TipoEsportazione, Responsabile, " _
                        & "TipoPeriodo, Anno, Periodo, DataInizio, DataFine,Settore, " _
                        & "UltimaModifica, Struttura, Area) values " _
                        & "(@idp, @nmp, @idr, @nmr, @dte, @tpe, " _
                        & "@res, @tpp, @ann, @per, @din, @dfi, " _
                        & "@set, @ulm, @stru, @area)"
    End Function

    Protected Overrides Function UpdateStatement() As String
        Return "UPDATE importazioni_liberi SET " _
                        & "Id_Provincia = @idp, " _
                        & "NomeProvincia = @nmp, " _
                        & "Id_Regione = @idr, " _
                        & "NomeRegione = @nmr, " _
                        & "DataEsportazione = @dte, " _
                        & "TipoEsportazione = @tpe, " _
                        & "Responsabile = @res, " _
                        & "TipoPeriodo = @tpp, " _
                        & "Anno = @ann, " _
                        & "Periodo = @per, " _
                        & "DataInizio = @din, " _
                        & "DataFine = @dfi, " _
                        & "Settore = @set, " _
                        & "UltimaModifica = @ulm, " _
                        & "Struttura = @stru, " _
                        & "Area = @area " _
                        & "WHERE (ID = @Id )"
    End Function

    Protected Overrides Function DeleteStatement() As String
        Return "Delete from importazioni_liberi where Id = @Id"
    End Function

    Protected Overrides Function FindNextKeyStatement() As String
        Return ""
    End Function

#End Region


#Region "Metodi per la ricerca dell'oggetto secondo l'id proposto"

    Public Overrides Function FindObjectById(ByVal Id As Integer) As AbstractPersistenceObject

        Return DirectCast(MyBase.FindByKey(New Key(Id)), Export)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try

            'recupero il registro dei mapper
            Dim Registro As PersistenceMapperRegistry = New PersistenceMapperRegistry
            Registro.SetPersistentService(m_PersistentService)

            Dim exp As New Export
            exp.Key = Key
            'User.UserName = rs.Item("Username")
            'User.Password = rs.Item("Password")
            'User.Nome = rs.Item("Nome")
            'User.Cognome = rs.Item("Cognome")
            'User.Mail = rs.Item("Mail")
            'User.Locked = IIf(rs.Item("Locked").ToString = "False", False, True)
            'User.NationalUser = IIf(rs.Item("NationalUser").ToString = "False", False, True)

            exp.Structure = IIf(rs.Item("Struttura") IsNot Nothing, rs.Item("Struttura"), "")
            exp.Area = IIf(rs.Item("Area") IsNot Nothing, rs.Item("Area"), "")


            exp.ExporterName = rs.Item("Responsabile")
            exp.Settore = rs.Item("Settore")
            exp.DataModifica = rs.Item("UltimaModifica")
            exp.ExportDate = rs.Item("DataEsportazione")
            exp.ExportType = [Enum].Parse(GetType(ExprtType), rs.Item("TipoEsportazione").ToString)

            'recupero il periodo
            Dim p As PeriodType = [Enum].Parse(GetType(PeriodType), rs.Item("TipoPeriodo").ToString)

            If p = PeriodType.Interval Then
                exp.Period = New SubscriptionPeriod(rs.Item("DataInizio"), rs.Item("DataFine"))
            Else
                exp.Period = New SubscriptionPeriod(rs.Item("Periodo"), rs.Item("Anno"), p)

            End If

            'recupero la regione
            Dim MapperRegione As MapperRegione = Registro.GetMapperByName("MapperRegione")

            Dim Id_Regione As Int32 = IIf(rs.Item("Id_Regione") IsNot Nothing, rs.Item("Id_Regione"), -1)
            Dim Regione As Regione = IIf(Id_Regione = -1, Nothing, MapperRegione.FindObjectById(Id_Regione))
            If TypeOf (Regione) Is RegioneNulla Then
                Regione = Nothing
            End If

            exp.Regione = Regione

            'recupero la provincia
            Dim MapperProvincia As MapperProvincia = Registro.GetMapperByName("MapperProvincia")

            Dim ID_PROVINCIA As Int32 = IIf(rs.Item("Id_Provincia") IsNot Nothing, rs.Item("Id_Provincia"), -1)
            Dim PROVINCIA As Provincia = IIf(ID_PROVINCIA = -1, Nothing, MapperProvincia.FindObjectById(ID_PROVINCIA))
            If TypeOf (PROVINCIA) Is ProvinciaNulla Then
                PROVINCIA = Nothing
            End If

            exp.Province = PROVINCIA


            Return exp
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto Export con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
    End Function









    'Protected Overrides Function GetCommand(ByVal CommandText As String) As System.Data.IDbCommand
    '    Return DBTypeUtils.GetCommand(Me.m_PersistentService.ServiceName, CommandText, DBConnectionManager.Instance.GetCurrentConnection)
    'End Function

#End Region



    Protected Overrides Sub LoadInsertCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim exp As Export = DirectCast(Item, Export)



            Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@idp"
            param.Value = exp.Province.Id
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@nmp"
            param.Value = exp.Province.Descrizione
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@idr"
            param.Value = exp.Regione.Id
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@nmr"
            param.Value = exp.Regione.Descrizione
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@dte"
            param.Value = exp.ExportDate.Date
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@tpe"
            param.Value = exp.ExportType.ToString
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@res"
            param.Value = exp.ExporterName
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@tpp"
            param.Value = exp.PeriodType.ToString
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@ann"
            param.Value = exp.Period.Year
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@per"
            param.Value = exp.Period.PeriodNumber
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@din"
            param.Value = exp.Period.InitialDate.Date
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@dfi"
            param.Value = exp.Period.EndDate.Date
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@set"
            param.Value = exp.Settore
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@ulm"
            param.Value = DateTime.Now
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@stru"
            param.Value = exp.Structure
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@area"
            param.Value = exp.Area
            Cmd.Parameters.Add(param)

        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di inserimento dell'oggetto Export Liberi." & vbCrLf & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim exp As Export = DirectCast(Item, Export)



            Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@idp"
            param.Value = exp.Province.Id
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@nmp"
            param.Value = exp.Province.Descrizione
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@idr"
            param.Value = exp.Regione.Id
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@nmr"
            param.Value = exp.Regione.Descrizione
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@dte"
            param.Value = exp.ExportDate.Date
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@tpe"
            param.Value = exp.ExportType.ToString
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@res"
            param.Value = exp.ExporterName
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@tpp"
            param.Value = exp.PeriodType.ToString
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@ann"
            param.Value = exp.Period.Year
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@per"
            param.Value = exp.Period.PeriodNumber
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@din"
            param.Value = exp.Period.InitialDate.Date
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@dfi"
            param.Value = exp.Period.EndDate.Date
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@set"
            param.Value = exp.Settore
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@ulm"
            param.Value = DateTime.Now
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@stru"
            param.Value = exp.Structure
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@area"
            param.Value = exp.Area
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@Id"
            param.Value = exp.Key.Value(0)
            Cmd.Parameters.Add(param)

        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di aggiornamento dell'oggetto Export Liberi." & vbCrLf & ex.Message)
        End Try
    End Sub
End Class
