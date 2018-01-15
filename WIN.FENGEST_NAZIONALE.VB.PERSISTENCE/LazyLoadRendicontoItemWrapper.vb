
Imports WIN.TECHNICAL.PERSISTENCE
Public Class LazyLoadRendicontoItemWrapper
    Inherits VirtualLazyList

    Private ListLoader As MapperItemRendicontoWrapper
    Private Id_Rendiconto As Int32
    Private m_PersistenceMapperRegistry As PersistenceMapperRegistry

    Public Sub New(ByVal IdRendiconto As Int32, ByVal registry As PersistenceMapperRegistry)
        Id_Rendiconto = IdRendiconto
        m_PersistenceMapperRegistry = registry
        ListLoader = m_PersistenceMapperRegistry.GetMapperByName("MapperItemRendicontoWrapper")
    End Sub



    Protected Overrides Function GetList() As System.Collections.ArrayList
        If Source Is Nothing Then
            Source = GetElementList() ' ListLoader.FindByQuery("Select * from TB_PROVINCIE where ID_TB_REGIONI = " & m_Id_Regione & "  order by Descrizione")
        End If
        Return Source
    End Function

    Private Function GetElementList() As IList
        Dim ps As IPersistenceFacade = DataAccessServices.Instance.PersistenceFacade
        Dim query As Query = ps.CreateNewQuery("MapperItemRendicontoWrapper")



        Dim column As String = "Id_Rendiconto"


        Dim crit As AbstractBoolCriteria = Criteria.Equal(column, Id_Rendiconto.ToString)

        


        query.AddWhereClause(crit)



        Return query.Execute(ps)


        'If m_mode = Type.Provincie Then
        '   Return ListLoader.FindByQuery("Select * from TB_COMUNI where ID_TB_PROVINCIE = " & m_Id & "  order by Descrizione")
        'Else
        '   Return ListLoader.FindByQuery("Select * from TB_COMUNI where ID_TB_REGIONI = " & m_Id & "  order by Descrizione")
        'End If

    End Function


End Class
