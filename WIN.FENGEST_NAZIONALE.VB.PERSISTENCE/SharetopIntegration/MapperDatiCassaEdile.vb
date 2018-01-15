Imports WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration

Public Class MapperDatiCassaEdile
    Inherits AbstractRDBMapper





    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(True)
        m_IsAutoIncrementID = True
    End Sub




#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from sharetop_daticasseedili"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from sharetop_daticasseedili where Id = @Id"
    End Function

    Protected Overrides Function InsertStatement() As String
        Return "Insert into sharetop_daticasseedili (Id_Regione, Id_Provincia, Anno,Id_Survey, Addetti, Imprese, MSDenunciato, MSVersato, IscrittiOOSS, PercSindacalizzati, DelegheFeneal, PercFeneal, DelegheFilca, PercFilca, DelegheFillea, PercFillea, ImportoQACP, ImportoQACR, ImportoQACN , ImportoDelegheFeneal) " _
                & "values (@idr, @idp, @ann, @ids, @add, @imp, @msd,@msv, @isc, @psin, @dfe, @pfe, @dfi, @pfi, @dfl, @pfl, @qacp, @qacr, @qacn, @impp)"






    End Function

    Protected Overrides Function UpdateStatement() As String
        Return ""
    End Function

    Protected Overrides Function DeleteStatement() As String
        Return "Delete from sharetop_daticasseedili where Id = @Id"
    End Function

    Protected Overrides Function FindNextKeyStatement() As String
        Return ""
    End Function

#End Region


#Region "Metodi per la ricerca dell'oggetto secondo l'id proposto"

    Public Overrides Function FindObjectById(ByVal Id As Integer) As AbstractPersistenceObject

        Return DirectCast(MyBase.FindByKey(New Key(Id)), DatiCassaEdile)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try
            Dim Registro As PersistenceMapperRegistry = New PersistenceMapperRegistry
            Registro.SetPersistentService(m_PersistentService)

            Dim exp As New DatiCassaEdile
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


            exp.Addetti = IIf(rs.Item("Addetti") IsNot Nothing, rs.Item("Addetti"), 0)
            exp.Imprese = IIf(rs.Item("Imprese") IsNot Nothing, rs.Item("Imprese"), 0)
            exp.MSDenunciato = IIf(rs.Item("MSDenunciato") IsNot Nothing, rs.Item("MSDenunciato"), 0)
            exp.MSVersato = IIf(rs.Item("MSVersato") IsNot Nothing, rs.Item("MSVersato"), 0)
            exp.IscrittiOOSS = IIf(rs.Item("IscrittiOOSS") IsNot Nothing, rs.Item("IscrittiOOSS"), 0)


            exp.PercSindacalizzati = IIf(rs.Item("PercSindacalizzati") IsNot Nothing, rs.Item("PercSindacalizzati"), 0)
            exp.DelegheFeneal = IIf(rs.Item("DelegheFeneal") IsNot Nothing, rs.Item("DelegheFeneal"), 0)
            exp.PercFeneal = IIf(rs.Item("PercFeneal") IsNot Nothing, rs.Item("PercFeneal"), 0)
            exp.DelegheFilca = IIf(rs.Item("DelegheFilca") IsNot Nothing, rs.Item("DelegheFilca"), 0)

            exp.PercFilca = IIf(rs.Item("PercFilca") IsNot Nothing, rs.Item("PercFilca"), 0)
            exp.DelegheFillea = IIf(rs.Item("DelegheFillea") IsNot Nothing, rs.Item("DelegheFillea"), 0)
            exp.PercFillea = IIf(rs.Item("PercFillea") IsNot Nothing, rs.Item("PercFillea"), 0)


            exp.ImportoQACP = IIf(rs.Item("ImportoQACP") IsNot Nothing, rs.Item("ImportoQACP"), 0)
            exp.ImportoQACR = IIf(rs.Item("ImportoQACR") IsNot Nothing, rs.Item("ImportoQACR"), 0)
            exp.ImportoQACN = IIf(rs.Item("ImportoQACN") IsNot Nothing, rs.Item("ImportoQACN"), 0)
            exp.ImportoDelegheFeneal = IIf(rs.Item("ImportoDelegheFeneal") IsNot Nothing, rs.Item("ImportoDelegheFeneal"), 0)


            Return exp
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto DatiCasseEdili con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
    End Function









    'Protected Overrides Function GetCommand(ByVal CommandText As String) As System.Data.IDbCommand
    '    Return DBTypeUtils.GetCommand(Me.m_PersistentService.ServiceName, CommandText, DBConnectionManager.Instance.GetCurrentConnection)
    'End Function

#End Region



    Protected Overrides Sub LoadInsertCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim lavoratore As DatiCassaEdile = DirectCast(Item, DatiCassaEdile)



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
            param.ParameterName = "@add"
            param.Value = lavoratore.Addetti
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@imp"
            param.Value = lavoratore.Imprese
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@msd"
            param.Value = lavoratore.MSDenunciato
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@msv"
            param.Value = lavoratore.MSVersato
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@isc"
            param.Value = lavoratore.IscrittiOOSS
            Cmd.Parameters.Add(param)

         

            param = Cmd.CreateParameter
            param.ParameterName = "@psin"
            param.Value = lavoratore.PercSindacalizzati
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@dfe"
            param.Value = lavoratore.DelegheFeneal
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@pfe"
            param.Value = lavoratore.PercFeneal
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@dfi"
            param.Value = lavoratore.DelegheFilca
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@pfi"
            param.Value = lavoratore.PercFilca
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@dfl"
            param.Value = lavoratore.DelegheFillea
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@pfl"
            param.Value = lavoratore.PercFillea
            Cmd.Parameters.Add(param)

        
            param = Cmd.CreateParameter
            param.ParameterName = "@qacp"
            param.Value = lavoratore.ImportoQACP
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@qacr"
            param.Value = lavoratore.ImportoQACR
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@qacn"
            param.Value = lavoratore.ImportoQACN
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@impp"
            param.Value = lavoratore.ImportoDelegheFeneal
            Cmd.Parameters.Add(param)

        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di inserimento dell'oggetto Rappresentanza." & vbCrLf & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)

    End Sub
End Class
