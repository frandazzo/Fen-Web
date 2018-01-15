Imports WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration

Public Class MapperCoordinataBancaria
    Inherits AbstractRDBMapper





    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(True)
        m_IsAutoIncrementID = True
    End Sub




#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from Sharetop_coordinatabancaria"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return ""
    End Function

    Protected Overrides Function InsertStatement() As String
        Return "Insert into Sharetop_coordinatabancaria (Iban,Banca,Indirizzo,NumAgenzia,Id_Struttura) values " _
                        & "(@iba, @ban, @ind,@num,@ids)"
    End Function

    Protected Overrides Function UpdateStatement() As String
        Return ""
    End Function

    Protected Overrides Function DeleteStatement() As String
        Return ""
    End Function

    Protected Overrides Function FindNextKeyStatement() As String
        Return ""
    End Function

#End Region


#Region "Metodi per la ricerca dell'oggetto secondo l'id proposto"

    Public Overrides Function FindObjectById(ByVal Id As Integer) As AbstractPersistenceObject

        Return DirectCast(MyBase.FindByKey(New Key(Id)), CoordinataBancaria)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try
            Dim exp As New CoordinataBancaria

            exp.Key = Key

            exp.Iban = IIf(rs.Item("Iban") IsNot Nothing, rs.Item("Iban"), "")
            exp.Banca = IIf(rs.Item("Banca") IsNot Nothing, rs.Item("Banca"), "")
            exp.Indirizzo = IIf(rs.Item("Indirizzo") IsNot Nothing, rs.Item("Indirizzo"), "")
            exp.NumAgenzia = IIf(rs.Item("NumAgenzia") IsNot Nothing, rs.Item("NumAgenzia"), "")


          

            Return exp
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto Coordinata bancaria con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
    End Function

    'Protected Overrides Function GetCommand(ByVal CommandText As String) As System.Data.IDbCommand
    '    Return DBTypeUtils.GetCommand(Me.m_PersistentService.ServiceName, CommandText, DBConnectionManager.Instance.GetCurrentConnection)
    'End Function

#End Region



    Protected Overrides Sub LoadInsertCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim exp As CoordinataBancaria = DirectCast(Item, CoordinataBancaria)



            Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@iba"
            param.Value = exp.Iban
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@ban"
            param.Value = exp.Banca
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@ind"
            param.Value = exp.Indirizzo
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@num"
            param.Value = exp.NumAgenzia
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@ids"
            param.Value = exp.Id_Struttura
            Cmd.Parameters.Add(param)


         


        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di inserimento dell'oggetto Coordinatabancaria." & vbCrLf & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)

    End Sub
End Class
