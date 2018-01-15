
Imports WIN.TECHNICAL.PERSISTENCE
Imports WIN.BASEREUSE
Imports WIN.FENGEST_NAZIONALE.DOMAIN.Workers
Imports WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES
Imports WIN.FENGEST_NAZIONALE.DOMAIN.Financial
Imports BilancioFenealgest.DomainLayer


Public Class MapperRendicontoWrapper
    Inherits AbstractRDBMapper





    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(True)
        m_IsAutoIncrementID = True
    End Sub




#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from rendiconto"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from rendiconto where Id = @Id"
    End Function

    Protected Overrides Function InsertStatement() As String
        Return "Insert into rendiconto (DataImportazione,NomeRegione,NomeProvincia,Anno,IsRegionale,IsBilancioQuadrato,IsPreventivoQuadrato,StatoPatrimoniale, ContoRLST) values " _
                        & "(@dti, @nmr, @nmp,@ann,@isr,@isb,@isp,@stp, @crls)"
    End Function

    Protected Overrides Function UpdateStatement() As String
        Return ""
    End Function

    Protected Overrides Function DeleteStatement() As String
        Return "Delete from rendiconto where Id = @Id"
    End Function

    Protected Overrides Function FindNextKeyStatement() As String
        Return ""
    End Function

#End Region


#Region "Metodi per la ricerca dell'oggetto secondo l'id proposto"

    Public Overrides Function FindObjectById(ByVal Id As Integer) As AbstractPersistenceObject

        Return DirectCast(MyBase.FindByKey(New Key(Id)), RendicontoWrapper)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try
            Dim rlst As String = IIf(rs.Item("ContoRLST") IsNot Nothing, rs.Item("ContoRLST").ToString, "")
            'recupero il registro dei mapper
            Dim Registro As PersistenceMapperRegistry = New PersistenceMapperRegistry
            Registro.SetPersistentService(m_PersistentService)

            Dim exp As New DTORendiconto
            Dim w As RendicontoWrapper

            If rlst = "FENEAL" Then
                w = New RendicontoWrapper(exp, False)
            Else
                w = New RendicontoWrapper(exp, True)
            End If

            w.Key = Key

            Dim data As DateTime = IIf(rs.Item("DataImportazione") IsNot Nothing, rs.Item("DataImportazione"), DateTime.Now)
            w.DataCreazione = data

            exp.Regione = IIf(rs.Item("NomeRegione") IsNot Nothing, rs.Item("NomeRegione").ToString, "")
            exp.Provincia = IIf(rs.Item("NomeProvincia") IsNot Nothing, rs.Item("NomeProvincia").ToString, "")
            exp.Anno = IIf(rs.Item("Anno") IsNot Nothing, rs.Item("Anno").ToString, -1)
            exp.IsRegionale = IIf(rs.Item("IsRegionale").ToString = "False", False, True)
            exp.IsBilancioQuadrato = IIf(rs.Item("IsBilancioQuadrato").ToString = "False", False, True)

            exp.StatoPatrimoniale = IIf(rs.Item("StatoPatrimoniale") IsNot Nothing, rs.Item("StatoPatrimoniale"), "")



            'exp.ContoRLST = IIf(rs.Item("ContoRLST") IsNot Nothing, rs.Item("ContoRLST"), 0)

            exp.IsPreventivoQuadratoQuadrato = IIf(rs.Item("IsPreventivoQuadrato").ToString = "False", False, True)

            w.Items = New LazyLoadRendicontoItemWrapper(Key.LongValue(), Registro)

            'Dim a As Int32 = w.Items.Count
            Dim a As DTORendiconto = w.NormalizedDTORendicontoAfterRetrieve()


            Return w
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto RendicontoWrapper con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
    End Function

    'Protected Overrides Function GetCommand(ByVal CommandText As String) As System.Data.IDbCommand
    '    Return DBTypeUtils.GetCommand(Me.m_PersistentService.ServiceName, CommandText, DBConnectionManager.Instance.GetCurrentConnection)
    'End Function

#End Region



    Protected Overrides Sub LoadInsertCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim exp As RendicontoWrapper = DirectCast(Item, RendicontoWrapper)



            Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@dti"
            param.Value = DateTime.Now
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@nmr"
            param.Value = exp.DTORendiconto.Regione
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@nmp"
            param.Value = exp.DTORendiconto.Provincia
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@ann"
            param.Value = exp.DTORendiconto.Anno
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@isr"
            param.Value = exp.DTORendiconto.IsRegionale.ToString
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@isb"
            param.Value = exp.DTORendiconto.IsBilancioQuadrato.ToString
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@isp"
            param.Value = exp.DTORendiconto.IsPreventivoQuadratoQuadrato.ToString
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@stp"
            param.Value = exp.DTORendiconto.StatoPatrimoniale
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@crls"
            If exp.ForRlst Then
                param.Value = "RLST"
            Else
                param.Value = "FENEAL"
            End If
            'param.Value = exp.DTORendiconto.ContoRLST
            Cmd.Parameters.Add(param)




        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di inserimento dell'oggetto RendicontoWrapper." & vbCrLf & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)

    End Sub
End Class

