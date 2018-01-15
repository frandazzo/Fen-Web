
Imports WIN.TECHNICAL.PERSISTENCE
Imports WIN.BASEREUSE
Imports WIN.FENGEST_NAZIONALE.DOMAIN.Workers
Imports WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES
Imports WIN.FENGEST_NAZIONALE.DOMAIN.Financial
Imports BilancioFenealgest.DomainLayer


Public Class MapperItemRendicontoWrapper
    Inherits AbstractRDBMapper





    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(True)
        m_IsAutoIncrementID = True
    End Sub




#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from rendicontoitem"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return ""
    End Function

    Protected Overrides Function InsertStatement() As String
        Return "Insert into rendicontoitem (Id_Rendiconto,IdNodo,DescrizioneNodo,ImportoBilancio,ImportoPreventivo,Livello, IdParent,IsLeaf) values " _
                        & "(@idr, @idn, @des,@imb,@imp,@liv, @idnp, @il)"
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

        Return DirectCast(MyBase.FindByKey(New Key(Id)), ItemRendicontoWrapper)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try
            Dim exp As New DTORendicontoItem
            Dim w = New ItemRendicontoWrapper(exp)
            w.Key = Key


            exp.IdNodo = IIf(rs.Item("IdNodo") IsNot Nothing, rs.Item("IdNodo").ToString, "")



            exp.DescrizioneNodo = IIf(rs.Item("DescrizioneNodo") IsNot Nothing, rs.Item("DescrizioneNodo").ToString, "")
            exp.ImportoBilancio = IIf(rs.Item("ImportoBilancio") IsNot Nothing, rs.Item("ImportoBilancio"), 0)
            exp.ImportoPreventivo = IIf(rs.Item("ImportoPreventivo") IsNot Nothing, rs.Item("ImportoPreventivo"), 0)
            exp.Livello = IIf(rs.Item("Livello") IsNot Nothing, rs.Item("Livello"), 0)
            exp.IdNodoPadre = IIf(rs.Item("IdParent") IsNot Nothing, rs.Item("IdParent").ToString, "")
            exp.IsLeaf = IIf(rs.Item("IsLeaf").ToString = "False", False, True)


            Return w
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto ItemRendicontoWrapper con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
    End Function

    'Protected Overrides Function GetCommand(ByVal CommandText As String) As System.Data.IDbCommand
    '    Return DBTypeUtils.GetCommand(Me.m_PersistentService.ServiceName, CommandText, DBConnectionManager.Instance.GetCurrentConnection)
    'End Function

#End Region



    Protected Overrides Sub LoadInsertCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim exp As ItemRendicontoWrapper = DirectCast(Item, ItemRendicontoWrapper)



            Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@idr"
            param.Value = exp.Parent.Id
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@idn"
            param.Value = exp.Item.IdNodo
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@des"
            param.Value = exp.Item.DescrizioneNodo
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@imb"
            param.Value = exp.Item.ImportoBilancio
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@imp"
            param.Value = exp.Item.ImportoPreventivo
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@liv"
            param.Value = exp.Item.Livello
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@idnp"
            param.Value = exp.Item.IdNodoPadre
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@il"
            param.Value = exp.Item.IsLeaf.ToString
            Cmd.Parameters.Add(param)


        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di inserimento dell'oggetto ItemRendicontoWrapper." & vbCrLf & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)

    End Sub
End Class

