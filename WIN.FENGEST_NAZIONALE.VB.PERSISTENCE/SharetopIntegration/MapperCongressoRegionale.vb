Imports WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration

Public Class MapperCongressoRegionale
    Inherits AbstractRDBMapper





    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(True)
        m_IsAutoIncrementID = True
    End Sub




#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from sharetop_congressoregionale"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from sharetop_congressoregionale where Id = @Id"
    End Function

    Protected Overrides Function InsertStatement() As String
        Return "Insert into sharetop_congressoregionale (Id_Regione, Id_Provincia, Anno, Id_Survey, SegretarioGenerale," _
                & "SegretarioOrganizzativo, " _
                & "MembriSegretaria,Tesoriere,ConsiglioTerritoriale,ConsiglioTerritorialeSupplente," _
                & "AssembleaTerritoriale,AssembleaTerritorialeSupplente,RevisoriDeiConti,Probiviri) " _
                & "values (@idr, @idp, @ann, @ids, @gen,@org, @seg, @tes, @conter, @conters, @asster, @assters, @rev, @pro)"
    End Function

    Protected Overrides Function UpdateStatement() As String
        Return ""
    End Function

    Protected Overrides Function DeleteStatement() As String
        Return "Delete from sharetop_congressoregionale where Id = @Id"
    End Function

    Protected Overrides Function FindNextKeyStatement() As String
        Return ""
    End Function

#End Region


#Region "Metodi per la ricerca dell'oggetto secondo l'id proposto"

    Public Overrides Function FindObjectById(ByVal Id As Integer) As AbstractPersistenceObject

        Return DirectCast(MyBase.FindByKey(New Key(Id)), CongressoRegionale)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try
            Dim Registro As PersistenceMapperRegistry = New PersistenceMapperRegistry
            Registro.SetPersistentService(m_PersistentService)

            Dim exp As New CongressoRegionale
            exp.Key = Key

            exp.BaseData = New SharetopBaseData
            exp.BaseData.Anno = IIf(rs.Item("Anno") IsNot Nothing, rs.Item("Anno"), DateTime.Now.Year)
            exp.BaseData.SurveyId = IIf(rs.Item("Id_Survey") IsNot Nothing, rs.Item("Id_Survey"), -1)

            Dim idReg As Int32 = IIf(rs.Item("Id_Regione") IsNot Nothing, rs.Item("Id_Regione"), -1)
            Dim id_Pro As Int32 = IIf(rs.Item("Id_Provincia") IsNot Nothing, rs.Item("Id_Provincia"), -1)

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


            exp.SegretarioGenerale = IIf(rs.Item("SegretarioGenerale") IsNot Nothing, rs.Item("SegretarioGenerale"), "")
            exp.SegretarioOrganizzativo = IIf(rs.Item("SegretarioOrganizzativo") IsNot Nothing, rs.Item("SegretarioOrganizzativo"), "")
            exp.MembriSegretaria = IIf(rs.Item("MembriSegretaria") IsNot Nothing, rs.Item("MembriSegretaria"), "")
            exp.Tesoriere = IIf(rs.Item("Tesoriere") IsNot Nothing, rs.Item("Tesoriere"), "")
            exp.ConsiglioTerritoriale = IIf(rs.Item("ConsiglioTerritoriale") IsNot Nothing, rs.Item("ConsiglioTerritoriale"), "")
            exp.ConsiglioTerritorialeSupplente = IIf(rs.Item("ConsiglioTerritorialeSupplente") IsNot Nothing, rs.Item("ConsiglioTerritorialeSupplente"), "")
            exp.AssembleaTerritoriale = IIf(rs.Item("AssembleaTerritoriale") IsNot Nothing, rs.Item("AssembleaTerritoriale"), "")
            exp.AssembleaTerritorialeSupplente = IIf(rs.Item("AssembleaTerritorialeSupplente") IsNot Nothing, rs.Item("AssembleaTerritorialeSupplente"), "")
            exp.RevisoriDeiConti = IIf(rs.Item("RevisoriDeiConti") IsNot Nothing, rs.Item("RevisoriDeiConti"), "")
            exp.Probiviri = IIf(rs.Item("Probiviri") IsNot Nothing, rs.Item("Probiviri"), "")

           


            Return exp
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto CongressoRegionale con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
    End Function









    'Protected Overrides Function GetCommand(ByVal CommandText As String) As System.Data.IDbCommand
    '    Return DBTypeUtils.GetCommand(Me.m_PersistentService.ServiceName, CommandText, DBConnectionManager.Instance.GetCurrentConnection)
    'End Function

#End Region



    Protected Overrides Sub LoadInsertCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim lavoratore As CongressoRegionale = DirectCast(Item, CongressoRegionale)



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

            'SegretarioGenerale,SegretarioOrganizzativo, 
            'MembriSegretaria, Tesoriere, ConsiglioTerritoriale, 
            'ConsiglioTerritorialeSupplente, AssembleaTerritoriale, 
            'AssembleaTerritorialeSupplente, RevisoriDeiConti, Probiviri


            param = Cmd.CreateParameter
            param.ParameterName = "@gen"
            param.Value = lavoratore.SegretarioGenerale
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@org"
            param.Value = lavoratore.SegretarioOrganizzativo
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@seg"
            param.Value = lavoratore.MembriSegretaria
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@tes"
            param.Value = lavoratore.Tesoriere
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@conter"
            param.Value = lavoratore.ConsiglioTerritoriale
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@conters"
            param.Value = lavoratore.ConsiglioTerritorialeSupplente
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@asster"
            param.Value = lavoratore.AssembleaTerritoriale
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@assters"
            param.Value = lavoratore.AssembleaTerritorialeSupplente
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@rev"
            param.Value = lavoratore.RevisoriDeiConti
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@pro"
            param.Value = lavoratore.Probiviri
            Cmd.Parameters.Add(param)


        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di inserimento dell'oggetto CongressoRegionale." & vbCrLf & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)

    End Sub
End Class
