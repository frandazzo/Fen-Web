Public Class LazyLoadCoordinateBancarie
    Inherits VirtualLazyList

    Private ListLoader As MapperCoordinataBancaria
    Private m_Id_Struttura As Int32
    Private m_PersistenceMapperRegistry As PersistenceMapperRegistry

    Public Sub New(ByVal Id_Struttura As Int32, ByVal registry As PersistenceMapperRegistry)
        m_Id_Struttura = Id_Struttura
        m_PersistenceMapperRegistry = registry
        ListLoader = m_PersistenceMapperRegistry.GetMapperByName("MapperCoordinataBancaria")
    End Sub



    Protected Overrides Function GetList() As System.Collections.ArrayList
        If Source Is Nothing Then
            Source = GetElementList() ' ListLoader.FindByQuery("Select * from TB_PROVINCIE where ID_TB_REGIONI = " & m_Id_Regione & "  order by Descrizione")
        End If
        Return Source
    End Function

    Private Function GetElementList() As IList
        Dim ps As IPersistenceFacade = DataAccessServices.Instance.PersistenceFacade
        Dim query As Query = ps.CreateNewQuery("MapperCoordinataBancaria")



        Dim column As String = "Id_Struttura"


        Dim crit As AbstractBoolCriteria = Criteria.Equal(column, m_Id_Struttura.ToString)




        query.AddWhereClause(crit)



        Return query.Execute(ps)


        'If m_mode = Type.Provincie Then
        '   Return ListLoader.FindByQuery("Select * from TB_COMUNI where ID_TB_PROVINCIE = " & m_Id & "  order by Descrizione")
        'Else
        '   Return ListLoader.FindByQuery("Select * from TB_COMUNI where ID_TB_REGIONI = " & m_Id & "  order by Descrizione")
        'End If

    End Function
End Class
