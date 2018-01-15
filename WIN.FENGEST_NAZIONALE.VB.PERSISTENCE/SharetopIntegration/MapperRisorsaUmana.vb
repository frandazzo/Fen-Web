Imports WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration

Public Class MapperRisorsaUmana
    Inherits AbstractRDBMapper





    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(True)
        m_IsAutoIncrementID = True
    End Sub




#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from sharetop_risorseumane"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from sharetop_risorseumane where Id = @Id"
    End Function

    Protected Overrides Function InsertStatement() As String
        Return "Insert into sharetop_risorseumane (Id_Regione, Id_Provincia, Anno,Id_Survey, Tipo, Nome, Cognome, DataNascita, CodiceFiscale, TipoRapporto, Ruolo, Indirizzo, Telefono, Mail, Notes) " _
                & "values (@idr, @idp, @ann, @ids, @tip, @nom, @cog,@dtn, @cod, @tir, @ruo, @ind, @tel, @mai, @not)"






    End Function

    Protected Overrides Function UpdateStatement() As String
        Return ""
    End Function

    Protected Overrides Function DeleteStatement() As String
        Return "Delete from sharetop_risorseumane where Id = @Id"
    End Function

    Protected Overrides Function FindNextKeyStatement() As String
        Return ""
    End Function

#End Region


#Region "Metodi per la ricerca dell'oggetto secondo l'id proposto"

    Public Overrides Function FindObjectById(ByVal Id As Integer) As AbstractPersistenceObject

        Return DirectCast(MyBase.FindByKey(New Key(Id)), RisorsaUmana)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try
            Dim Registro As PersistenceMapperRegistry = New PersistenceMapperRegistry
            Registro.SetPersistentService(m_PersistentService)

            Dim exp As New RisorsaUmana
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


            exp.Tipo = IIf(rs.Item("Tipo") IsNot Nothing, rs.Item("Tipo"), "")
            exp.Nome = IIf(rs.Item("Nome") IsNot Nothing, rs.Item("Nome"), "")
            exp.Cognome = IIf(rs.Item("Cognome") IsNot Nothing, rs.Item("Cognome"), "")
            exp.DataNascita = IIf(rs.Item("DataNascita") IsNot Nothing, rs.Item("DataNascita"), "")
            exp.CodiceFiscale = IIf(rs.Item("CodiceFiscale") IsNot Nothing, rs.Item("CodiceFiscale"), "")


            exp.TipoRapporto = IIf(rs.Item("TipoRapporto") IsNot Nothing, rs.Item("TipoRapporto"), "")
            exp.Ruolo = IIf(rs.Item("Ruolo") IsNot Nothing, rs.Item("Ruolo"), "")
            exp.Indirizzo = IIf(rs.Item("Indirizzo") IsNot Nothing, rs.Item("Indirizzo"), "")
            exp.Telefono = IIf(rs.Item("Telefono") IsNot Nothing, rs.Item("Telefono"), "")

            exp.Mail = IIf(rs.Item("Mail") IsNot Nothing, rs.Item("Mail"), "")
            exp.Note = IIf(rs.Item("Notes") IsNot Nothing, rs.Item("Notes"), "")
            

            Return exp
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto Risorsa umana con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
    End Function









    'Protected Overrides Function GetCommand(ByVal CommandText As String) As System.Data.IDbCommand
    '    Return DBTypeUtils.GetCommand(Me.m_PersistentService.ServiceName, CommandText, DBConnectionManager.Instance.GetCurrentConnection)
    'End Function

#End Region



    Protected Overrides Sub LoadInsertCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim lavoratore As RisorsaUmana = DirectCast(Item, RisorsaUmana)



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
            param.ParameterName = "@tip"
            param.Value = lavoratore.Tipo
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@nom"
            param.Value = lavoratore.Nome
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@cog"
            param.Value = lavoratore.Cognome
            Cmd.Parameters.Add(param)

       
            param = Cmd.CreateParameter
            param.ParameterName = "@dtn"
            param.Value = lavoratore.DataNascita
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@cod"
            param.Value = lavoratore.CodiceFiscale
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@tir"
            param.Value = lavoratore.TipoRapporto
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@ruo"
            param.Value = lavoratore.Ruolo
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@ind"
            param.Value = lavoratore.Indirizzo
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
            param.ParameterName = "@not"
            param.Value = lavoratore.Note
            Cmd.Parameters.Add(param)

          
        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di inserimento dell'oggetto Risorsa umana." & vbCrLf & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)

    End Sub
End Class
