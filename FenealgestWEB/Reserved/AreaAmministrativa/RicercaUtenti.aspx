<%@ Page Title="Ricerca utenti del prortale Fenealgest" Language="C#" MasterPageFile="~/FenealgestwebNew.Master" AutoEventWireup="true" CodeBehind="RicercaUtenti.aspx.cs" Inherits="FenealgestWEB.Reserved.AreaAmministrativa.RicercaUtenti" %>
 

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipale" runat="server">
<div class="content">
         <div class="login">
				<h2>Ricerca utenti del portale Fenealgest</h2>
				<br/>
				<br/>	


            <font class="largelabel">Username</font>
            <asp:TextBox ID="txtCognome" runat="server" CssClass="largeinput"></asp:TextBox>
            <br />
            <font class="largelabel">Struttura</font>
            <asp:DropDownList ID="cboTipo" runat="server" Font-Size="12px" Width="200px">
            </asp:DropDownList> 
            <br />
            <font class="largelabel">Num. ris.</font>
            <asp:TextBox ID="txtResultPerPage" runat="server" CssClass="largeinput"></asp:TextBox>
            <br />
            <br />
            <br />
            
            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="accedi" 
                onclick="cmdCreaUtente_Click">
                Nuovo utente <asp:Image ID="Image3" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>
            &nbsp;&nbsp;
	        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="accedi" 
                onclick="cmdSearch_Click" >
                Ricerca utente <asp:Image ID="Image1" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>
            <br />

            <asp:GridView ID="GridView1" runat="server" CssClass="generic_table">
            <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="Id" 
                DataNavigateUrlFormatString="GestioneUtenti.aspx?ID={0}" HeaderText="VAI" 
                Text="vai">
            <ItemStyle HorizontalAlign="Center" />
            </asp:HyperLinkField>
            </Columns>
            </asp:GridView>

            <asp:LinkButton ID="lnkPrec" runat="server" onclick="lnkPrec_Click">&lt; Risultati precedenti</asp:LinkButton>
            &nbsp;
            <asp:LinkButton ID="lnkSucc"  runat="server" onclick="lnkSucc_Click">Risultati successivi &gt;</asp:LinkButton>
            <div class="message_box">
                <asp:Label ID="lblVis" runat="server" Text="Visualizzazione record ...."></asp:Label>
                <br />
                <asp:Label ID="lblTotalResults" runat="server" Text="Elementi trovati..."></asp:Label>
            </div>	
		</div>	
		<div class="column">
			    <h3>Dati di login</h3>
			        <%=this.HtmlLoginDataList("../../images/info.png")%>	    
			    <h3>Help on-line</h3>
			    <ul>
				    <li>
					    <h4>Presentazione contenuti pagina</h4>	
					    <font class="data">Pagina per la ricerca degli utenti del portale</font>						
					    La pagina corrente permette di ricercare gli utenti presenti nel sistema o crearne uno nuovo.<br />
              La pagina mostra tre campi in cui inserire: cognome, struttura e risultati per pagina (e' consigliabile inserire un numero compreso tra 10 e  50; per valori piu' bassi verra' automaticamente settato il valore 10, mentre per valori piu' alti verra' automaticamente settato il valore 50).<br />
              Premendo su 'Ricerca utente' verra' avviata la ricerca degli utenti presenti all'interno del sistema.<br />
              I risultati della ricerca sono automaticamente inseriti in una griglia dove sono mostrati cognome e nome dell'utente, la sua mail e la struttura. A fondo pagina sono visualizzati due link per scorrere tra le pagine dei risultati, l'intervallo di utenti visualizzati e il numero totale di utenti trovati.<br />
              Premendo la lente di ingrandimento si viene reindirizzati ad una pagina di dettaglio dove leggere tutte le informazioni relative all'utente.<br />
              Premendo su 'Nuovo utente' si potra' creare un nuovo utente del sistema.
				    </li> 
				    													
			    </ul>											
		  </div>     
</div>
</asp:Content>
