Imports WIN.FENGEST_NAZIONALE.DOMAIN.GestioneOrganizzativa
Imports WIN.FENGEST_NAZIONALE.DOMAIN.ValidationSubsystem

Public Class MapperAdministrativeRecord
    Inherits AbstractRDBMapper

    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(True)
        m_IsAutoIncrementID = True
    End Sub




#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from administrativedata"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from administrativedata where Id = @Id"
    End Function

    Protected Overrides Function InsertStatement() As String
        Return "Insert into administrativedata (Anno, Ente, EnteSecondario,Regione,  Provincia, " _
                & "Addetti, Aziende, Denunciato, Versato, QACP, QACN, QACR, Deleghe, Arretratri ) " _
                & "values (@anno, @bilateral, @bilateral1, @regione, @provincia, @addessti, @aziende, @denunciato, @versato, @qacp, @qacn, @qacr, @deleghe, @pending)"
    End Function

    Protected Overrides Function UpdateStatement() As String
        Return "UPDATE administrativedata SET " _
                & "Anno = @anno, " _
                & "Ente = @bilateral, " _
                & "EnteSecondario = @bilateral1, " _
                & "Regione = @regione, " _
                & "Provincia = @provincia, " _
                & "Addetti = @addessti, " _
                & "Aziende = @aziende, " _
                & "Denunciato = @denunciato, " _
                & "Versato = @versato, " _
                & "QACP = @qacp, " _
                & "QACN = @qacr, " _
                & "QACR = @qacn, " _
                & "Deleghe = @deleghe, " _
                & "Arretratri = @pending, " _
                & "WHERE (ID = @Id )"
    End Function

    Protected Overrides Function DeleteStatement() As String
        Return "Delete from administrativedata where Id = @Id"
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
            Dim iscrizione As New AdministrativeRecord()
            iscrizione.Key = Key


            iscrizione.Year = IIf(rs.Item("Anno") IsNot Nothing, rs.Item("Anno"), -1)
            iscrizione.Bilateral = IIf(rs.Item("Ente") IsNot Nothing, rs.Item("Ente"), "")
            iscrizione.SpecificBilateral = IIf(rs.Item("EnteSecondario") IsNot Nothing, rs.Item("EnteSecondario"), "")

            Dim Nome_Regione As String = IIf(rs.Item("Regione") IsNot Nothing, rs.Item("Regione"), "")
            iscrizione.Region = Nome_Regione

            Dim Nome_Provincia As String = IIf(rs.Item("Provincia") IsNot Nothing, rs.Item("Provincia"), "")
            iscrizione.Province = Nome_Provincia

            iscrizione.Workers = IIf(rs.Item("Addetti") IsNot Nothing, rs.Item("Addetti"), 0)
            iscrizione.Companies = IIf(rs.Item("Aziende") IsNot Nothing, rs.Item("Aziende"), 0)



            iscrizione.DeclaredSalary = IIf(rs.Item("Denunciato") IsNot Nothing, rs.Item("Denunciato"), 0)
            iscrizione.GivenSalary = IIf(rs.Item("Versato") IsNot Nothing, rs.Item("Versato"), 0)
            iscrizione.QACP = IIf(rs.Item("QACP") IsNot Nothing, rs.Item("QACP"), 0)
            iscrizione.QACN = IIf(rs.Item("QACN") IsNot Nothing, rs.Item("QACN"), 0)
            iscrizione.QACR = IIf(rs.Item("QACR") IsNot Nothing, rs.Item("QACR"), 0)
            iscrizione.DelegheAmount = IIf(rs.Item("Deleghe") IsNot Nothing, rs.Item("Deleghe"), 0)
            iscrizione.Pending = IIf(rs.Item("Arretratri") IsNot Nothing, rs.Item("Arretratri"), 0)



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
            Dim doc As AdministrativeRecord = DirectCast(Item, AdministrativeRecord)


           
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
            param.Value = doc.Workers
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@aziende"
            param.Value = doc.Companies
            Cmd.Parameters.Add(param)
           ' @denunciato,
            '    @versato, @qacp, @qacn, @qacr, @deleghe, @pending)"

            param = Cmd.CreateParameter
            param.ParameterName = "@denunciato"
            param.Value = doc.DeclaredSalary
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@versato"
            param.Value = doc.GivenSalary
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@qacp"
            param.Value = doc.QACP
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@qacn"
            param.Value = doc.QACN
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@qacr"
            param.Value = doc.QACR
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@deleghe"
            param.Value = doc.DelegheAmount
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@pending"
            param.Value = doc.Pending
            Cmd.Parameters.Add(param)




        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di inserimento dell'oggetto Organizzative." & vbCrLf & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim doc As AdministrativeRecord = DirectCast(Item, AdministrativeRecord)



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
            param.Value = doc.Workers
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@aziende"
            param.Value = doc.Companies
            Cmd.Parameters.Add(param)
            ' @denunciato,
            '    @versato, @qacp, @qacn, @qacr, @deleghe, @pending)"

            param = Cmd.CreateParameter
            param.ParameterName = "@denunciato"
            param.Value = doc.DeclaredSalary
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@versato"
            param.Value = doc.GivenSalary
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@qacp"
            param.Value = doc.QACP
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@qacn"
            param.Value = doc.QACN
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@qacr"
            param.Value = doc.QACR
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@deleghe"
            param.Value = doc.DelegheAmount
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@pending"
            param.Value = doc.Pending
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



