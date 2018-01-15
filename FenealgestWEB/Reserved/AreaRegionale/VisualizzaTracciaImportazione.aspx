<%@ Page Language="C#" MasterPageFile="~/FenealgestwebNew.Master" AutoEventWireup="true" CodeBehind="VisualizzaTracciaImportazione.aspx.cs" Inherits="FenealgestWEB.Reserved.AreaRegionale.VisualizzaTracciaImportazione" Title="Visualizzazione importazioni per regione" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipale" runat="server">
<div class="content">
         <div class="login">
				<h2>Visualizzazione importazioni per regione</h2>
				<br/>
				<br/>	

            <font class="label">Regione</font>
            <asp:DropDownList ID="cboRegione" runat="server" Font-Size="12px" Width="200px">
            </asp:DropDownList>
            <br />
            <font class="label">Settore</font>
            <asp:DropDownList ID="cboSettore" runat="server" AutoPostBack="True" Font-Size="12px" Width="200px"
                onselectedindexchanged="cboSettore_SelectedIndexChanged">
            </asp:DropDownList> 
            <br />
            <font class="label">Anno</font>
            <asp:DropDownList ID="cboAnno" runat="server" Font-Size="12px" Width="200px">
            </asp:DropDownList>
            <br />
            <font class="label">Semestre</font>
            <asp:DropDownList ID="cboSemestre" runat="server" Font-Size="12px" Width="200px">
            </asp:DropDownList>
            <br />
            <br />
            
            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="accedi" 
                onclick="cmdSearch_Click">
                Ricerca importazioni <asp:Image ID="Image3" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>

            
            <br />
            <br />

            <asp:GridView ID="GridView1" runat="server" CssClass="generic_table">
            </asp:GridView>

            <div class="message_box">            
                <asp:Label ID="lblTotalResults" runat="server" Text="Label"></asp:Label>
                <br />
                <br />
                <asp:Label ID="lblProvMancanti" runat="server" Text="Label"></asp:Label>
            </div>
    </div> 
        <div class="column">
			    <h3>Dati di login</h3>
			        <%=this.HtmlLoginDataList("../../images/info.png") %>	    
			    <h3>Help on-line</h3>
			    <ul>
				    <li>
					    <h4>Presentazione contenuti pagina</h4>	
					    <font class="data">Pagina ricerca importazioni</font>						
					    La pagina corrente permette di ricercare le importazioni di una regione.<br />
              La pagina mostra un campo preimpostato con l’indicazione della regione, più altri due dove impostare settore e anno. Nell’unico caso in cui venga scelto come settore quello “EDILE” sarà reso necessario impostare anche il semestre.<br />
              Premendo su “Ricerca importazioni” verrà avviata la ricerca delle importazioni presenti all’interno del sistema.<br />
              I risultati della ricerca sono automaticamente inseriti in una griglia sottostante dove sono mostrati data, provincia, data inizio, data fine e settore. A fondo pagina è visualizzato un messaggio riepilogativo di tutte le province appartenenti alla regione che non hanno ancora provveduto ad effettuare l’esportazione.<br />
              Premendo la lente di ingrandimento si viene reindirizzati ad una pagina di dettaglio dove leggere tutte le informazioni relative alla lista prescelta.<br />
				    </li> 
				    													
			    </ul>											
		  </div>   
</div>
</asp:Content>
