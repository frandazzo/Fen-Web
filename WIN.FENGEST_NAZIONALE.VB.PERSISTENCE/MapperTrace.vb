Public Class MapperTrace
    Inherits AbstractRDBMapper





    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(True)
        m_IsAutoIncrementID = True
    End Sub




#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from trace"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from trace where Id = @Id"
    End Function

    Protected Overrides Function InsertStatement() As String
        Return "Insert into trace (Counter, Province, Region, App, Month, Year) " _
                & "values (@cou, @pro, @reg, @app, @mon,@yea)"
    End Function

    Protected Overrides Function UpdateStatement() As String
        Return "UPDATE trace SET " _
                & "Counter = @cou, " _
                & "Province = @pro, " _
                & "Region = @reg, " _
                & "App = @app, " _
                & "Month = @mon, " _
                & "Year = @yea " _
                & "WHERE (ID = @Id )"
    End Function

    Protected Overrides Function DeleteStatement() As String
        Return "Delete from trace where Id = @Id"
    End Function

    Protected Overrides Function FindNextKeyStatement() As String
        Return ""
    End Function

#End Region


#Region "Metodi per la ricerca dell'oggetto secondo l'id proposto"

    Public Overrides Function FindObjectById(ByVal Id As Integer) As AbstractPersistenceObject

        Return DirectCast(MyBase.FindByKey(New Key(Id)), WIN.FENGEST_NAZIONALE.DOMAIN.Public.Trace)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try
            Dim utente As New WIN.FENGEST_NAZIONALE.DOMAIN.Public.Trace
            utente.Key = Key

            utente.Counter = rs.Item("Counter")
            utente.Province = rs.Item("Province")
            utente.Region = rs.Item("Region")
            utente.App = rs.Item("App")

            utente.Month = rs.Item("Month")
            utente.Year = rs.Item("Year")


           


            Return utente
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto Trace con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
    End Function









    'Protected Overrides Function GetCommand(ByVal CommandText As String) As System.Data.IDbCommand
    '    Return DBTypeUtils.GetCommand(Me.m_PersistentService.ServiceName, CommandText, DBConnectionManager.Instance.GetCurrentConnection)
    'End Function

#End Region



    Protected Overrides Sub LoadInsertCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim lavoratore As WIN.FENGEST_NAZIONALE.DOMAIN.Public.Trace = DirectCast(Item, WIN.FENGEST_NAZIONALE.DOMAIN.Public.Trace)



            Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@cou"
            param.Value = lavoratore.Counter
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@pro"
            param.Value = lavoratore.Province
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@reg"
            param.Value = lavoratore.Region
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@app"
            param.Value = lavoratore.App
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@mon"
            param.Value = lavoratore.Month
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@yea"
            param.Value = lavoratore.Year
            Cmd.Parameters.Add(param)


            

        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di inserimento dell'oggetto Trace." & vbCrLf & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim lavoratore As WIN.FENGEST_NAZIONALE.DOMAIN.Public.Trace = DirectCast(Item, WIN.FENGEST_NAZIONALE.DOMAIN.Public.Trace)



            Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@cou"
            param.Value = lavoratore.Counter
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@pro"
            param.Value = lavoratore.Province
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@reg"
            param.Value = lavoratore.Region
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@app"
            param.Value = lavoratore.App
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@mon"
            param.Value = lavoratore.Month
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@yea"
            param.Value = lavoratore.Year
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@Id"
            param.Value = lavoratore.Key.Value(0)
            Cmd.Parameters.Add(param)

        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di aggiornamento dell'oggetto Trace." & vbCrLf & ex.Message)
        End Try
    End Sub
End Class
