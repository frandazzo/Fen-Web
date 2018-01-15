<%@ Page Language="C#" MasterPageFile="~/FenealgestwebNew.Master" AutoEventWireup="true" CodeBehind="RicercaNews.aspx.cs" Inherits="FenealgestWEB.Reserved.AreaAmministrativa.RicercaNews" Title="Ricerca news" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipale" runat="server">
 <div class="content">
    <div class="login">
		<h2>Ricerca news</h2>
		<br/>
	    <br/>
        <font class="label">Titolo</font>
        <asp:TextBox ID="txtTitolo" runat="server" CssClass="user"></asp:TextBox>
        <br/>
	    <br/>
        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="accedi" 
                onclick="cmdSearch_Click">
              Ricerca news <asp:Image ID="Image3" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>
        &nbsp;&nbsp;
	    <asp:LinkButton ID="LinkButton2" runat="server" CssClass="accedi" 
            onclick="cmdCreaNews_Click" >
              Crea una nuova news <asp:Image ID="Image1" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>

       <asp:GridView ID="GridView1" runat="server" CssClass="generic_table">
       </asp:GridView>
       <br/>
       <div class="message_box">
		 <asp:Literal ID="lblTotalResults1" runat="server"></asp:Literal>	
       </div>	
    </div>
    <div class="column">
			    <h3>Dati di login</h3>
			        <%=this.HtmlLoginDataList("../../images/info.png")%>	    
			    <h3>Help on-line</h3>
			    <ul>
				    <li>
					    <h4>Presentazione contenuti pagina</h4>	
					    <font class="data">Pagina per la ricerca delle news</font>						
					    La pagina corrente permette di ricercare o creare una news.<br />
              La pagina mostra un campo in cui inserire il titolo della news da cercare.<br />
              Premendo su “Ricerca news” verrà avviata la ricerca delle news presenti all’interno del sistema.<br />
              I risultati della ricerca sono automaticamente inseriti in una griglia sottostante dove sono mostrati data, titolo e autore. A fondo pagina sono visualizzati due link per scorrere tra le pagine dei risultati, l’intervallo di news visualizzate e il numero totale di news trovate.<br />
              Premendo la lente di ingrandimento si viene reindirizzati ad una pagina di dettaglio dove leggere tutte le informazioni relative alla news scelta.<br />
              Premendo su “Crea news” si potrà creare una nuova news.  
				    </li> 
				    													
			    </ul>											
		  </div>     
</div>
</asp:Content>
