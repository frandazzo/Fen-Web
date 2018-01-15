<%@ Page Language="C#" MasterPageFile="~/FenealgestwebNew.Master" AutoEventWireup="true" CodeBehind="GestioneNews.aspx.cs" Inherits="FenealgestWEB.Reserved.AreaAmministrativa.GestioneNews" Title="Gestione news" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipale" runat="server">
 <div class="content">
    <div class="login">
		<h2>Gestione news</h2>
		<br/>
	    <br/>
        <font class="label">Titolo</font>
        <asp:TextBox ID="txtTitolo" runat="server" CssClass="user"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validators" ValidationGroup="Agg" ControlToValidate="txtTitolo" runat="server" ErrorMessage="Titolo obbligatorio"></asp:RequiredFieldValidator>

        <br />
        <font class="label">Creato da</font>
        <asp:TextBox ID="txtCreatoDa" runat="server" CssClass="user"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validators" ValidationGroup="Agg" ControlToValidate="txtCreatoDa" runat="server" ErrorMessage="Autore obbligatorio"></asp:RequiredFieldValidator>

        <br />
        <font class="label">Testo</font>
        <asp:TextBox ID="txtTesto"  runat="server" Height="100px" Width="400px" TextMode="MultiLine"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validators" ValidationGroup="Agg" ControlToValidate="txtTesto" runat="server" ErrorMessage="Testo obbligatorio"></asp:RequiredFieldValidator>

        <br />
        
        <div class="message_box">
            
        </div>
        
        <asp:LinkButton ID="cmdCreaNews1" runat="server" CssClass="accedi" 
                onclick="cmdCreaNews_Click">
              Nuova news <asp:Image ID="Image3" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>
        &nbsp;&nbsp;
        <asp:LinkButton ID="cmdSave1" runat="server" CssClass="accedi" ValidationGroup="Agg"
                onclick="cmdSave_Click">
              Aggiorna news <asp:Image ID="Image1" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>
        &nbsp;&nbsp;
        <asp:LinkButton ID="cmdDeleteNews1" runat="server" CssClass="accedi"
                onclick="cmdDeleteNews_Click">
              Elimina news <asp:Image ID="Image2" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>
        &nbsp;&nbsp;
         <asp:LinkButton ID="cmdAnnulla1" runat="server" CssClass="accedi"
                onclick="cmdAnnulla_Click">
              Torna alla ricerca <asp:Image ID="Image4" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>

    </div>
    <div class="column">
			    <h3>Dati di login</h3>
			        <%=this.HtmlLoginDataList("../../images/info.png")%>	    
			    <h3>Help on-line</h3>
			    <ul>
				    <li>
					    <h4>Presentazione contenuti pagina</h4>	
					    <font class="data">Pagina gestione news</font>						
					    La pagina corrente permette di gestire le news del sistema.<br />
              la pagina mostra i campi seguenti: titolo, creato da  e testo.<br />
              Premendo su “Inserisci news” si inseriranno i dati nel sistema.<br />
              Premendo su “Aggiorna news” sarà possibile aggiornare i dati nel sistema.<br />
              Premendo su “Elimina news”  questa verrà eliminato dal sistema.<br />
              Le voci “Nuova news” e “Torna alla ricerca” permettono rispettivamente di azzerare i campi per l’inserimento di una nuova news e di tornare sulla pagina di ricerca.
				    </li> 
				    													
			    </ul>											
		  </div>     
</div>

</asp:Content>
