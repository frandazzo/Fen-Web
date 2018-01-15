<%@ Page Language="C#" MasterPageFile="~/FenealgestwebNew.Master" AutoEventWireup="true" CodeBehind="GestioneUtenti.aspx.cs"  Inherits="FenealgestWEB.Reserved.AreaAmministrativa.GestioneUtenti" Title="Gestione utenti portale Fenealgest" %>

<%@ Register assembly="DevExpress.Web.v13.2, Version=13.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
 

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipale" runat="server">
 <div class="content">
         <div class="login">
				<h2>Gestione utenti del portale Fenealgest</h2>
				<br/>
				<br/>	


            <font class="largelabel">Cognome*</font>
            <asp:TextBox ID="txtCognome" runat="server"  CssClass="largeinput"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCognome" runat="server" CssClass="validators" ErrorMessage="Obbligatorio" ControlToValidate="txtCognome" ValidationGroup="aggUtente"></asp:RequiredFieldValidator>
            <br />
            <font class="largelabel">Nome*</font>
            <asp:TextBox ID="txtNome" runat="server"  CssClass="largeinput"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNome" runat="server" CssClass="validators" ErrorMessage="Obbligatorio" ControlToValidate="txtNome" ValidationGroup="aggUtente"></asp:RequiredFieldValidator>
            <br />
            <font class="largelabel">Mail*</font>
            <asp:TextBox ID="txtMail" runat="server"   CssClass="largeinput"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvMail" runat="server" CssClass="validators" ErrorMessage="Obbligatorio" ControlToValidate="txtMail" ValidationGroup="aggUtente"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="revMail" runat="server" ErrorMessage="Mail non valida" ControlToValidate="txtMail" ValidationExpression='\w+([-+.&#039;]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*' ValidationGroup="aggUtente"></asp:RegularExpressionValidator>
            <br />
            <font class="largelabel">Struttura</font>
            <asp:DropDownList ID="cboTipo" runat="server" AutoPostBack="True" Font-Size="12px" Width="200px"
                OnSelectedIndexChanged="cboTipo_SelectedIndexChanged">
            </asp:DropDownList>
            <br />           
            <font class="largelabel">Regione</font>
            <asp:DropDownList ID="cboRegione" runat="server" AutoPostBack="True" Font-Size="12px" Width="200px"
                onselectedindexchanged="cboRegione_SelectedIndexChanged">
            </asp:DropDownList>
            <br /> 
            <font class="largelabel">Provincia</font>
            <asp:DropDownList ID="cboProvincia" runat="server" Font-Size="12px" Width="200px">
            </asp:DropDownList>
            <br />
            <hr />
            <font class="label">Ruoli</font>
            <asp:RequiredFieldValidator ID="rfvListBox1" runat="server" CssClass="validators" ErrorMessage="Obbligatorio" ControlToValidate="ListBox1" ValidationGroup="aggUtente"></asp:RequiredFieldValidator>
            <br />
            <asp:ListBox ID="ListBox1" runat="server" Font-Size="12px" Width="200px"> </asp:ListBox>

            <asp:Button ID="btnAddRole" runat="server" onclick="btnAddRole_Click" Text="&lt;" />
            
            <asp:Button ID="btnDeleteRole" runat="server" onclick="btnDeleteRole_Click" Text="X" />

            <asp:ListBox ID="ListBox2" runat="server" Font-Size="12px" Width="200px"></asp:ListBox>
            <br />

          
            <br />
            <hr />

        <font class="largelabel">Username*</font>
        <asp:TextBox ID="txtUser" runat="server" MaxLength="30"  CssClass="largeinput"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvUser" runat="server"  CssClass="validators" ErrorMessage="Obbligatorio" ControlToValidate="txtUser" ValidationGroup="aggUtente"></asp:RequiredFieldValidator>
        <br />
        <font class="largelabel">Password*</font>
        <asp:TextBox ID="txtPass" runat="server" MaxLength="30"  CssClass="largeinput"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvPass" runat="server"  CssClass="validators" ErrorMessage="Obbligatorio" ControlToValidate="txtPass" ValidationGroup="aggUtente"></asp:RequiredFieldValidator>
        <br />
        
        <div class="devcontrolcontainer">
            <div class="labelcontroldiv" >
                <font class="largelabel" style="margin-top: 6px;margin-right: 5px;">Data password</font>
            </div>
	        <div class="devcontroldiv">
	            <dx:ASPxDateEdit ID="bdpPass1" runat="server" Width="150px" 
                    TodayButtonText="Oggi" AllowNull="False">
                    <CalendarProperties TodayButtonText="Oggi">
                    </CalendarProperties>
                </dx:ASPxDateEdit>
	        </div>
        </div>
        
        <br />
        <font class="largelabel">Scadenza pass</font>
        
        <asp:Label ID="lblDataDeclay" runat="server"  class="largelabel"></asp:Label>
        <br />
        <br />
         <asp:CheckBox ID="chkLocked" runat="server" Text="Utente bloccato" />
        <br />
        <br />
        <asp:LinkButton ID="cmdCreaUtente1" runat="server" CssClass="accedi" 
                    onclick="cmdCreaUtente_Click">
                  Crea nuovo utente <asp:Image ID="Image3" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>
        &nbsp;&nbsp;

        <asp:LinkButton ID="cmdSave1" runat="server" CssClass="accedi" 
                    onclick="Button2_Click">
                  Aggiorna utente <asp:Image ID="Image18" runat="server"  ImageUrl="~/images/right_arrow.png" ValidationGroup="aggUtente" /> </asp:LinkButton>
        
        &nbsp;&nbsp;
        
       <asp:LinkButton ID="cmdDeleteUtente1" runat="server" CssClass="accedi" 
                    onclick="cmdDeleteUtente_Click">
                  Elimina utente <asp:Image ID="Image2" runat="server"  ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>
        
        
        <br />
        <br />

        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="accedi" 
                    onclick="cmdAnnulla_Click">
                  Torna alla ricerca utente <asp:Image ID="Image4" runat="server"  ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>

      

    </div>
    <div class="column">
			    <h3>Dati di login</h3>
			        <%=this.HtmlLoginDataList("../../images/info.png")%>	    
			    <h3>Help on-line</h3>
			    <ul>
				    <li>
					    <h4>Presentazione contenuti pagina</h4>	
					    <font class="data">Pagina per la gestione utenti del portale</font>						
					    La pagina corrente permette di gestire gli utenti del sistema.<br />
              La pagina mostra i campi seguenti: cognome, nome, mail, struttura, (a seconda della struttura) regione e provincia, ruoli, user, password, data di importazione password, e  indicazione di utente bloccato.<br />
              Premendo su “Inserisci nuovo utente” si inseriranno i dati nel sistema.<br />
              Premendo su “Aggiorna utente” sarà possibile aggiornare i dati nel sistema.<br />
              Premendo su “Elimina utente”  questo verrà eliminato dal sistema.<br />
              Le voci “Crea nuovo utente” e “Torna alla ricerca utente” permettono rispettivamente di azzerare i campi per l’inserimento di un nuovo utente e di tornare sulla pagina di ricerca.
				    </li> 
				    													
			    </ul>											
		  </div>     
</div>
</asp:Content>
