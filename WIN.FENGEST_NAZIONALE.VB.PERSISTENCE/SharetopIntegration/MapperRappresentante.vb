Imports WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration

Public Class MapperRappresentante
    Inherits AbstractRDBMapper





    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(True)
        m_IsAutoIncrementID = True
    End Sub




#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from sharetop_rappresentante"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from sharetop_rappresentante where Id = @Id"
    End Function

    Protected Overrides Function InsertStatement() As String
        Return "Insert into sharetop_rappresentante (Id_Regione, Id_Provincia, Anno, Id_Survey, Azienda, CodInpsAzienda, ProvinciaAzienda, ComuneAzienda, Contratto, Cognome, Nome, CodiceFiscale, Telefono, Mail, Tipo) " _
                & "values (@idr, @idp, @ann, @ids, @azi,@cod, @pro, @com, @con, @cog, @nom, @codf, @tel, @mai, @tip)"
    End Function

    Protected Overrides Function UpdateStatement() As String
        Return ""
    End Function

    Protected Overrides Function DeleteStatement() As String
        Return "Delete from sharetop_rappresentante where Id = @Id"
    End Function

    Protected Overrides Function FindNextKeyStatement() As String
        Return ""
    End Function

#End Region


#Region "Metodi per la ricerca dell'oggetto secondo l'id proposto"

    Public Overrides Function FindObjectById(ByVal Id As Integer) As AbstractPersistenceObject

        Return DirectCast(MyBase.FindByKey(New Key(Id)), Rappresentante)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try
            Dim Registro As PersistenceMapperRegistry = New PersistenceMapperRegistry
            Registro.SetPersistentService(m_PersistentService)

            Dim exp As New Rappresentante
            exp.Key = Key

            exp.BaseData = New SharetopBaseData
            exp.BaseData.Anno = IIf(rs.Item("Anno") IsNot Nothing, rs.Item("Anno"), DateTime.Now.Year)
            exp.BaseData.SurveyId = IIf(rs.Item("Id_Survey") IsNot Nothing, rs.Item("Id_Survey"), -1)

            Dim idReg As Int32 = IIf(rs.Item("Id_Regione") IsNot Nothing, rs.Item("Id_Regione"), -1)
            Dim id_Pro = IIf(rs.Item("Id_Provincia") IsNot Nothing, rs.Item("Id_Provincia"), -1)

            Dim MapperProvincia As MapperProvincia = registro.GetMapperByName("MapperProvincia")


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

            exp.Tipo = IIf(rs.Item("Tipo") IsNot Nothing, rs.Item("Tipo"), "")
            exp.Azienda = IIf(rs.Item("Azienda") IsNot Nothing, rs.Item("Azienda"), "")
            exp.CodInpsAzienda = IIf(rs.Item("CodInpsAzienda") IsNot Nothing, rs.Item("CodInpsAzienda"), "")
            exp.ProvinciaAzienda = IIf(rs.Item("ProvinciaAzienda") IsNot Nothing, rs.Item("ProvinciaAzienda"), "")
            exp.ComuneAzienda = IIf(rs.Item("ComuneAzienda") IsNot Nothing, rs.Item("ComuneAzienda"), "")
            exp.Contratto = IIf(rs.Item("Contratto") IsNot Nothing, rs.Item("Contratto"), "")
            exp.Cognome = IIf(rs.Item("Cognome") IsNot Nothing, rs.Item("Cognome"), "")
            exp.Nome = IIf(rs.Item("Nome") IsNot Nothing, rs.Item("Nome").ToString, "")
            exp.CodiceFiscale = IIf(rs.Item("CodiceFiscale") IsNot Nothing, rs.Item("CodiceFiscale"), "")
            exp.Mail = IIf(rs.Item("Mail") IsNot Nothing, rs.Item("Mail"), "")
            exp.Telefono = IIf(rs.Item("Telefono") IsNot Nothing, rs.Item("Telefono"), "")




            Return exp
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto Rappresentante con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
    End Function









    'Protected Overrides Function GetCommand(ByVal CommandText As String) As System.Data.IDbCommand
    '    Return DBTypeUtils.GetCommand(Me.m_PersistentService.ServiceName, CommandText, DBConnectionManager.Instance.GetCurrentConnection)
    'End Function

#End Region



    Protected Overrides Sub LoadInsertCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim lavoratore As Rappresentante = DirectCast(Item, Rappresentante)



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
            param.ParameterName = "@cog"
            param.Value = lavoratore.Cognome
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@nom"
            param.Value = lavoratore.Nome
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@codf"
            param.Value = lavoratore.CodiceFiscale
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@tel"
            param.Value = lavoratore.Telefono
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@mai"
            param.Value = lavoratore.Mail
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@tip"
            param.Value = lavoratore.Tipo
            Cmd.Parameters.Add(param)


        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di inserimento dell'oggetto Rappresentante." & vbCrLf & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)

    End Sub
End Class
