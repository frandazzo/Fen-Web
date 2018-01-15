Imports WIN.FENGEST_NAZIONALE.DOMAIN.GestioneOrganizzativa
Imports WIN.FENGEST_NAZIONALE.DOMAIN.ValidationSubsystem

Public Class MapperOrganizativeRecord
    Inherits AbstractRDBMapper

    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(True)
        m_IsAutoIncrementID = True
    End Sub




#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from organizzativedata"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from organizzativedata where Id = @Id"
    End Function

    Protected Overrides Function InsertStatement() As String
        Return "Insert into organizzativedata (Anno, Ente, EnteSecondario,Regione,  Provincia, " _
                & "Addetti, Aziende, Sindacalizzati, Feneal, Filca, Fillea) " _
                & "values (@anno, @bilateral, @bilateral1, @regione, @provincia, @addessti, @aziende, @sind, @feneal, @filca, @fillea)"
    End Function

    Protected Overrides Function UpdateStatement() As String
        Return "UPDATE organizzativedata SET " _
                 & "Anno = @anno, " _
                & "Ente = @bilateral, " _
                & "EnteSecondario = @bilateral1, " _
                & "Regione = @regione, " _
                & "Provincia = @provincia, " _
                & "Addetti = @tpd, " _
                & "Aziende = @sta, " _
                & "Sindacalizzati = @sind, " _
                & "Feneal = @feneal, " _
                & "Filca = @filca, " _
                & "Fillea = @fillea " _
                & "WHERE (ID = @Id )"
    End Function

    Protected Overrides Function DeleteStatement() As String
        Return "Delete from organizzativedata where Id = @Id"
    End Function

    Protected Overrides Function FindNextKeyStatement() As String
        Return ""
    End Function

#End Region


#Region "Metodi per la ricerca dell'oggetto secondo l'id proposto"

    Public Overrides Function FindObjectById(ByVal Id As Integer) As AbstractPersistenceObject

        Return DirectCast(MyBase.FindByKey(New Key(Id)), OrganizativeRecord)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try
            Dim iscrizione As New OrganizativeRecord()
            iscrizione.Key = Key


            iscrizione.Year = IIf(rs.Item("Anno") IsNot Nothing, rs.Item("Anno"), -1)
            iscrizione.Bilateral = IIf(rs.Item("Ente") IsNot Nothing, rs.Item("Ente"), "")
            iscrizione.SpecificBilateral = IIf(rs.Item("EnteSecondario") IsNot Nothing, rs.Item("EnteSecondario"), "")

            Dim Nome_Regione As String = IIf(rs.Item("Regione") IsNot Nothing, rs.Item("Regione"), "")
            iscrizione.Region = Nome_Regione

            Dim Nome_Provincia As String = IIf(rs.Item("Provincia") IsNot Nothing, rs.Item("Provincia"), "")
            iscrizione.Province = Nome_Provincia

            iscrizione.TotalWorkers = IIf(rs.Item("Addetti") IsNot Nothing, rs.Item("Addetti"), 0)
            iscrizione.TotalCompanies = IIf(rs.Item("Aziende") IsNot Nothing, rs.Item("Aziende"), 0)




            iscrizione.TotalSindacalizedWorkers = IIf(rs.Item("Sindacalizzati") IsNot Nothing, rs.Item("Sindacalizzati"), 0)
            iscrizione.FenealWorkers = IIf(rs.Item("Feneal") IsNot Nothing, rs.Item("Feneal"), 0)
            iscrizione.FilcaWorkers = IIf(rs.Item("Filca") IsNot Nothing, rs.Item("Filca"), 0)
            iscrizione.FilleaWorkers = IIf(rs.Item("Fillea") IsNot Nothing, rs.Item("Fillea"), 0)


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
            Dim doc As OrganizativeRecord = DirectCast(Item, OrganizativeRecord)



            Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@anno"
            param.Value = doc.Year
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@bilateral"
            param.Value = doc.Bilateral
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@bilateral1"
            param.Value = doc.SpecificBilateral
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@regione"
            param.Value = doc.Region
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@provincia"
            param.Value = doc.Province
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@addessti"
            param.Value = doc.TotalWorkers
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@aziende"
            param.Value = doc.TotalCompanies
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@sind"
            param.Value = doc.TotalSindacalizedWorkers
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@feneal"
            param.Value = doc.FenealWorkers
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@filca"
            param.Value = doc.FilcaWorkers
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@fillea"
            param.Value = doc.FilleaWorkers
            Cmd.Parameters.Add(param)



           



        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di inserimento dell'oggetto Organizzative." & vbCrLf & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim doc As OrganizativeRecord = DirectCast(Item, OrganizativeRecord)



           Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@anno"
            param.Value = doc.Year
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@bilateral"
            param.Value = doc.Bilateral
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@bilateral1"
            param.Value = doc.SpecificBilateral
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@regione"
            param.Value = doc.Region
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@provincia"
            param.Value = doc.Province
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@addessti"
            param.Value = doc.TotalWorkers
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@aziende"
            param.Value = doc.TotalCompanies
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@sind"
            param.Value = doc.TotalSindacalizedWorkers
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@feneal"
            param.Value = doc.FenealWorkers
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@filca"
            param.Value = doc.FilcaWorkers
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@fillea"
            param.Value = doc.FilleaWorkers
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@Id"
            param.Value = doc.Key.Value(0)
            Cmd.Parameters.Add(param)




        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di aggiornamento dell'oggetto Organizzative record." & vbCrLf & ex.Message)
        End Try
    End Sub
End Class


