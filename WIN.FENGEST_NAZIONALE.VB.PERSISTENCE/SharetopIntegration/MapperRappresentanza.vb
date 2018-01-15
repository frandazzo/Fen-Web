Imports WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration

Public Class MapperRappresentanza
    Inherits AbstractRDBMapper





    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(True)
        m_IsAutoIncrementID = True
    End Sub




#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from sharetop_rappresentanza"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from sharetop_rappresentanza where Id = @Id"
    End Function

    Protected Overrides Function InsertStatement() As String
        Return "Insert into sharetop_rappresentanza (Id_Regione, Id_Provincia, Anno, Id_Survey, Azienda, CodInpsAzienda, ProvinciaAzienda, ComuneAzienda, Contratto, NumDipendenti, NumIscrittiFeneal, VotiListaFeneal, NumRappresentantiFeneal, TipoNomina, UrlVerbale, DataElezione) " _
                & "values (@idr, @idp, @ann, @ids, @azi,@cod, @pro, @com, @con, @dip, @isc, @vot, @rap, @tip, @url, @dat)"






    End Function

    Protected Overrides Function UpdateStatement() As String
        Return ""
    End Function

    Protected Overrides Function DeleteStatement() As String
        Return "Delete from sharetop_rappresentanza where Id = @Id"
    End Function

    Protected Overrides Function FindNextKeyStatement() As String
        Return ""
    End Function

#End Region


#Region "Metodi per la ricerca dell'oggetto secondo l'id proposto"

    Public Overrides Function FindObjectById(ByVal Id As Integer) As AbstractPersistenceObject

        Return DirectCast(MyBase.FindByKey(New Key(Id)), Rappresentanza)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try
            Dim Registro As PersistenceMapperRegistry = New PersistenceMapperRegistry
            Registro.SetPersistentService(m_PersistentService)

            Dim exp As New Rappresentanza
            exp.Key = Key

            exp.BaseData = New SharetopBaseData
            exp.BaseData.Anno = IIf(rs.Item("Anno") IsNot Nothing, rs.Item("Anno"), DateTime.Now.Year)
            exp.BaseData.SurveyId = IIf(rs.Item("Id_Survey") IsNot Nothing, rs.Item("Id_Survey"), -1)

            Dim idReg As Int32 = IIf(rs.Item("Id_Regione") IsNot Nothing, rs.Item("Id_Regione"), -1)
            Dim id_Pro = IIf(rs.Item("Id_Provincia") IsNot Nothing, rs.Item("Id_Provincia"), -1)

            Dim MapperProvincia As MapperProvincia = Registro.GetMapperByName("MapperProvincia")


            Dim PROVINCIA As Provincia = IIf(id_Pro = -1, Nothing, MapperProvincia.FindObjectById(id_Pro))
            If TypeOf (PROVINCIA) Is ProvinciaNulla Then
                PROVINCIA = Nothing
            End If

            exp.BaseData.Provincia = PROVINCIA


            Dim MapperRegione As MapperRegione = Registro.GetMapperByName("MapperRegione")


            Dim Regione As Regione = IIf(idReg = -1, Nothing, MapperRegione.FindObjectById(idReg))
            If TypeOf (Regione) Is RegioneNulla Then
                Regione = Nothing
            End If

            exp.BaseData.Regione = Regione


            exp.Azienda = IIf(rs.Item("Azienda") IsNot Nothing, rs.Item("Azienda"), "")
            exp.CodInpsAzienda = IIf(rs.Item("CodInpsAzienda") IsNot Nothing, rs.Item("CodInpsAzienda"), "")
            exp.ProvinciaAzienda = IIf(rs.Item("ProvinciaAzienda") IsNot Nothing, rs.Item("ProvinciaAzienda"), "")
            exp.ComuneAzienda = IIf(rs.Item("ComuneAzienda") IsNot Nothing, rs.Item("ComuneAzienda"), "")
            exp.Contratto = IIf(rs.Item("Contratto") IsNot Nothing, rs.Item("Contratto"), "")


            exp.NumDipendenti = IIf(rs.Item("NumDipendenti") IsNot Nothing, rs.Item("NumDipendenti"), 0)
            exp.NumIscrittiFeneal = IIf(rs.Item("NumIscrittiFeneal") IsNot Nothing, rs.Item("NumIscrittiFeneal"), 0)
            exp.VotiListaFeneal = IIf(rs.Item("VotiListaFeneal") IsNot Nothing, rs.Item("VotiListaFeneal"), 0)
            exp.NumRappresentantiFeneal = IIf(rs.Item("NumRappresentantiFeneal") IsNot Nothing, rs.Item("NumRappresentantiFeneal"), 0)

            exp.TipoNomina = IIf(rs.Item("TipoNomina") IsNot Nothing, rs.Item("TipoNomina"), "")
            exp.UrlVerbale = IIf(rs.Item("UrlVerbale") IsNot Nothing, rs.Item("UrlVerbale"), "")
            exp.DataElezione = IIf(rs.Item("DataElezione") IsNot Nothing, rs.Item("DataElezione"), DateTime.MinValue)


            Return exp
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto Rappresentanza con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
    End Function









    'Protected Overrides Function GetCommand(ByVal CommandText As String) As System.Data.IDbCommand
    '    Return DBTypeUtils.GetCommand(Me.m_PersistentService.ServiceName, CommandText, DBConnectionManager.Instance.GetCurrentConnection)
    'End Function

#End Region



    Protected Overrides Sub LoadInsertCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim lavoratore As Rappresentanza = DirectCast(Item, Rappresentanza)



            Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@idr"
            param.Value = lavoratore.BaseData.Regione.Id
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@idp"
            If (lavoratore.BaseData.Provincia Is Nothing) Then
                param.Value = DBNull.Value
            Else
                param.Value = lavoratore.BaseData.Provincia.Id
            End If

            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@ann"
            param.Value = lavoratore.BaseData.Anno
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@ids"
            param.Value = lavoratore.BaseData.SurveyId
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@azi"
            param.Value = lavoratore.Azienda
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@cod"
            param.Value = lavoratore.CodInpsAzienda
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@pro"
            param.Value = lavoratore.ProvinciaAzienda
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@com"
            param.Value = lavoratore.ComuneAzienda
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@con"
            param.Value = lavoratore.Contratto
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@dip"
            param.Value = lavoratore.NumDipendenti
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@isc"
            param.Value = lavoratore.NumIscrittiFeneal
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@vot"
            param.Value = lavoratore.VotiListaFeneal
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@rap"
            param.Value = lavoratore.NumRappresentantiFeneal
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@tip"
            param.Value = lavoratore.TipoNomina
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@url"
            param.Value = lavoratore.UrlVerbale
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@dat"
            If lavoratore.DataElezione = DateTime.MinValue Then
                param.Value = DBNull.Value
            Else
                param.Value = lavoratore.DataElezione
            End If

            Cmd.Parameters.Add(param)


        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di inserimento dell'oggetto Rappresentanza." & vbCrLf & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)

    End Sub
End Class
