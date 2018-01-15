Imports WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration

Public Class MapperStruttura
    Inherits AbstractRDBMapper





    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(True)
        m_IsAutoIncrementID = True
    End Sub




#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from sharetop_struttura"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from sharetop_struttura where Id = @Id"
    End Function

    Protected Overrides Function InsertStatement() As String
        Return "Insert into sharetop_struttura (Id_Regione, Id_Provincia, Anno, Id_Survey, SitoInternet, Indirizzo, Mail, RecapitiTelefonici, Denominazione, Responsabile, Fiscale, Altri) " _
                & "values (@idr, @idp, @ann, @ids, @sit,@ind, @mai, @rec, @den, @res, @fis, @alt)"




    End Function

    Protected Overrides Function UpdateStatement() As String
        Return ""
    End Function

    Protected Overrides Function DeleteStatement() As String
        Return "Delete from sharetop_struttura where Id = @Id"
    End Function

    Protected Overrides Function FindNextKeyStatement() As String
        Return ""
    End Function

#End Region


#Region "Metodi per la ricerca dell'oggetto secondo l'id proposto"

    Public Overrides Function FindObjectById(ByVal Id As Integer) As AbstractPersistenceObject

        Return DirectCast(MyBase.FindByKey(New Key(Id)), Struttura)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try
            Dim Registro As PersistenceMapperRegistry = New PersistenceMapperRegistry
            Registro.SetPersistentService(m_PersistentService)

            Dim exp As New Struttura
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


           
            exp.SitoInternet = IIf(rs.Item("SitoInternet") IsNot Nothing, rs.Item("SitoInternet"), "")
            exp.Indirizzo = IIf(rs.Item("Indirizzo") IsNot Nothing, rs.Item("Indirizzo"), "")
            exp.Mail = IIf(rs.Item("Mail") IsNot Nothing, rs.Item("Mail"), "")
            exp.RecapitiTelefonici = IIf(rs.Item("RecapitiTelefonici") IsNot Nothing, rs.Item("RecapitiTelefonici"), "")

            exp.Denominazione = IIf(rs.Item("Denominazione") IsNot Nothing, rs.Item("Denominazione"), "")
            exp.Responsabile = IIf(rs.Item("Responsabile") IsNot Nothing, rs.Item("Responsabile"), "")
            exp.Fiscale = IIf(rs.Item("Fiscale") IsNot Nothing, rs.Item("Fiscale"), "")
            exp.Altri = IIf(rs.Item("Altri") IsNot Nothing, rs.Item("Altri"), "")




            If PROVINCIA Is Nothing Then
                exp.Descrizione = Regione.Descrizione
            Else
                exp.Descrizione = PROVINCIA.Descrizione
            End If



            Dim lazyList As New LazyLoadCoordinateBancarie(exp.Id, Registro)
            'innesco il proxy
            Dim num As Int32 = lazyList.Count
            Dim List As New List(Of CoordinataBancaria)



            For Each CoordinataBancaria In lazyList
                List.Add(CoordinataBancaria)
            Next


            exp.CoordinateBancarie = List.ToArray


            Return exp
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto struttura con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
    End Function



    Public Overrides Sub PostInsertAction(ByVal item As AbstractPersistenceObject)
        InsertAssociatedCoordinate(item)
    End Sub





    'Protected Overrides Function GetCommand(ByVal CommandText As String) As System.Data.IDbCommand
    '    Return DBTypeUtils.GetCommand(Me.m_PersistentService.ServiceName, CommandText, DBConnectionManager.Instance.GetCurrentConnection)
    'End Function

#End Region



    Protected Overrides Sub LoadInsertCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim lavoratore As Struttura = DirectCast(Item, Struttura)



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
            param.ParameterName = "@sit"
            param.Value = lavoratore.SitoInternet
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@ind"
            param.Value = lavoratore.Indirizzo
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@mai"
            param.Value = lavoratore.Mail
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@rec"
            param.Value = lavoratore.RecapitiTelefonici
            Cmd.Parameters.Add(param)






            param = Cmd.CreateParameter
            param.ParameterName = "@den"
            param.Value = lavoratore.Denominazione
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@res"
            param.Value = lavoratore.Responsabile
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@fis"
            param.Value = lavoratore.Fiscale
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@alt"
            param.Value = lavoratore.Altri
            Cmd.Parameters.Add(param)

           


        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di inserimento dell'oggetto struttura." & vbCrLf & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)

    End Sub

    Private Sub InsertAssociatedCoordinate(item As AbstractPersistenceObject)
        Dim struttura As Struttura = DirectCast(item, Struttura)

        Dim Registro As PersistenceMapperRegistry = New PersistenceMapperRegistry
        Registro.SetPersistentService(m_PersistentService)

        Dim mapperCoordinateBancarie As MapperCoordinataBancaria = Registro.GetMapperByName("MapperCoordinataBancaria")

        If struttura.CoordinateBancarie IsNot Nothing Then
            For Each elem As CoordinataBancaria In struttura.CoordinateBancarie
                elem.Id_Struttura = struttura.Id
                mapperCoordinateBancarie.Insert(elem)
            Next
        End If
        


    End Sub

End Class
