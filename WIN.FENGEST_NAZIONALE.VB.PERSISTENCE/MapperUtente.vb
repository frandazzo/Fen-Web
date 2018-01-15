Imports WIN.TECHNICAL.PERSISTENCE
Imports WIN.BASEREUSE
Imports WIN.TECHNICAL.SECURITY_NEW.RoleManagement
Imports WIN.TECHNICAL.SECURITY_NEW
Imports WIN.FENGEST_NAZIONALE.DOMAIN.Security
Imports WIN.FENGEST_NAZIONALE.DOMAIN

Public Class MapperUtente
    Inherits AbstractRDBMapper


    Private MapperRuoli As IRoleProvider


    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(True)
        m_IsAutoIncrementID = True
    End Sub




#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from Utenti"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from Utenti where Id = @Id"
    End Function

    Protected Overrides Function InsertStatement() As String
        Return "Insert into Utenti (Username, Password, Nome, Cognome, Mail, Locked, PasswordData, PasswordDecay, RoleDescriptor,  Structure, Id_Regione, Id_Provincia) values (@usd, @pwd, @nam, @sur, @mai, @lok, @pdat, @pdec, @rol, @stru, @idreg, @idpro)"
    End Function

    Protected Overrides Function UpdateStatement() As String
        Return "UPDATE Utenti SET Username = @usd, Password = @pwd, Nome = @nam, Cognome = @sur, Mail = @mai, Locked = @lok, PasswordData = @pdat, PasswordDecay = @pdec, Roledescriptor = @rol,  Structure = @stru, Id_Regione = @idreg, Id_Provincia = @idpro WHERE (ID = @Id )"
    End Function

    Protected Overrides Function DeleteStatement() As String
        Return "Delete from Utenti where Id = @Id"
    End Function

    Protected Overrides Function FindNextKeyStatement() As String
        Return ""
    End Function

#End Region


#Region "Metodi per la ricerca dell'oggetto secondo l'id proposto"

    Public Overrides Function FindObjectById(ByVal Id As Integer) As AbstractPersistenceObject

        Return DirectCast(MyBase.FindByKey(New Key(Id)), Utente)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try
            'recupero il registro dei mapper
            Dim Registro As PersistenceMapperRegistry = New PersistenceMapperRegistry
            Registro.SetPersistentService(m_PersistentService)

            Dim User As New WIN.FENGEST_NAZIONALE.DOMAIN.Security.Utente
            User.Key = Key
            User.UserName = rs.Item("Username")
            User.Password = rs.Item("Password")
            User.Nome = rs.Item("Nome")
            User.Cognome = rs.Item("Cognome")
            User.Mail = rs.Item("Mail")
            User.Locked = IIf(rs.Item("Locked").ToString = "False", False, True)
            'User.NationalUser = IIf(rs.Item("NationalUser").ToString = "False", False, True)

            User.PasswordData = rs.Item("PasswordData")
            'User.PasswordDecay = rs.Item("PasswordDecay")
            CreaRuoli(User, rs.Item("RoleDescriptor"))


            User.Appartenenza.Struttura = [Enum].Parse(GetType(StrutturaApparteneza), rs.Item("Structure").ToString)


            Dim MapperRegione As MapperRegione = Registro.GetMapperByName("MapperRegione")

            Dim Id_Regione As Int32 = IIf(rs.Item("Id_Regione") IsNot Nothing, rs.Item("Id_Regione"), -1)
            Dim Regione As Regione = IIf(Id_Regione = -1, Nothing, MapperRegione.FindObjectById(Id_Regione))
            If TypeOf (Regione) Is RegioneNulla Then
                Regione = Nothing
            End If

            User.Appartenenza.Regione = Regione


            Dim MapperProvincia As MapperProvincia = Registro.GetMapperByName("MapperProvincia")

            Dim ID_PROVINCIA As Int32 = IIf(rs.Item("Id_Provincia") IsNot Nothing, rs.Item("Id_Provincia"), -1)
            Dim PROVINCIA As Provincia = IIf(ID_PROVINCIA = -1, Nothing, MapperProvincia.FindObjectById(ID_PROVINCIA))
            If TypeOf (PROVINCIA) Is ProvinciaNulla Then
                PROVINCIA = Nothing
            End If

            User.Appartenenza.Provicnia = PROVINCIA


            Return User
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto Utente con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
    End Function

    Private Sub CreaRuoli(ByVal User As WIN.FENGEST_NAZIONALE.DOMAIN.Security.Utente, ByVal descriptor As String)

        'Dim r As IList(Of TECHNICAL.SECURITY_NEW.RoleManagement.Role)


        If MapperRuoli Is Nothing Then

            MapperRuoli = New WIN.FENGEST_NAZIONALE.DOMAIN.Security.RoleProvider

        End If


        Dim result As IList(Of TECHNICAL.SECURITY_NEW.RoleManagement.Role) = New List(Of RoleManagement.Role)

        Dim ruoli As String() = Split(descriptor, ";")

        For Each elem As String In ruoli
            Dim role As RoleManagement.Role = GetRole(elem, MapperRuoli.GetRoles)
            If role IsNot Nothing Then
                User.AddRole(role)
            End If
        Next

    End Sub

    Private Function GetRole(ByVal name As String, ByVal list As IList(Of TECHNICAL.SECURITY_NEW.RoleManagement.Role)) As RoleManagement.Role
        For Each elem As RoleManagement.Role In list
            If elem.Name.ToLower.Equals(name.ToLower) Then
                Return elem
            End If
        Next
        Return Nothing
    End Function





    'Protected Overrides Function GetCommand(ByVal CommandText As String) As System.Data.IDbCommand
    '    Return DBTypeUtils.GetCommand(Me.m_PersistentService.ServiceName, CommandText, DBConnectionManager.Instance.GetCurrentConnection)
    'End Function

#End Region



    Protected Overrides Sub LoadInsertCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim User As WIN.FENGEST_NAZIONALE.DOMAIN.Security.Utente = DirectCast(Item, WIN.FENGEST_NAZIONALE.DOMAIN.Security.Utente)

            '= Cmd.CreateParameter
            'param.ParameterName = "@Idr"
            'param.Value = User.Role.ID
            'Cmd.Parameters.Add(param)


            Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@usd"
            param.Value = User.UserName
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@pwd"
            param.Value = User.Password
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@nam"
            param.Value = User.Nome
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@sur"
            param.Value = User.Cognome
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@mai"
            param.Value = User.Mail
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@lok"
            param.Value = User.Locked.ToString()
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@pdat"
            param.Value = User.PasswordData
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@pdec"
            param.Value = User.PasswordDecay
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@rol"
            param.Value = User.ToRoleDescriptor
            Cmd.Parameters.Add(param)



            'param = Cmd.CreateParameter
            'param.ParameterName = "@natu"
            'param.Value = User.NationalUser
            'Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@stru"
            param.Value = User.Appartenenza.Struttura.ToString
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@idreg"
            If User.Appartenenza.Regione Is Nothing Then
                param.Value = DBNull.Value
            Else
                param.Value = User.Appartenenza.Regione.Id
            End If
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@idpro"
            If User.Appartenenza.Provicnia Is Nothing Then
                param.Value = DBNull.Value
            Else
                param.Value = User.Appartenenza.Provicnia.Id
            End If
            Cmd.Parameters.Add(param)



        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di inserimento dell'oggetto Utente." & vbCrLf & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim User As WIN.FENGEST_NAZIONALE.DOMAIN.Security.Utente = DirectCast(Item, WIN.FENGEST_NAZIONALE.DOMAIN.Security.Utente)


            Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@usd"
            param.Value = User.UserName
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@pwd"
            param.Value = User.Password
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@nam"
            param.Value = User.Nome
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@sur"
            param.Value = User.Cognome
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@mai"
            param.Value = User.Mail
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@lok"
            param.Value = User.Locked.ToString()
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@pdat"
            param.Value = User.PasswordData
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@pdec"
            param.Value = User.PasswordDecay
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@rol"
            param.Value = User.ToRoleDescriptor
            Cmd.Parameters.Add(param)


            'param = Cmd.CreateParameter
            'param.ParameterName = "@natu"
            'param.Value = User.NationalUser
            'Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@stru"
            param.Value = User.Appartenenza.Struttura.ToString
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@idreg"
            If User.Appartenenza.Regione Is Nothing Then
                param.Value = DBNull.Value
            Else
                param.Value = User.Appartenenza.Regione.Id
            End If
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@idpro"
            If User.Appartenenza.Provicnia Is Nothing Then
                param.Value = DBNull.Value
            Else
                param.Value = User.Appartenenza.Provicnia.Id
            End If
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@Id"
            param.Value = User.Key.Value(0)
            Cmd.Parameters.Add(param)

        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di aggiornamento dell'oggetto Utente." & vbCrLf & ex.Message)
        End Try
    End Sub
End Class
