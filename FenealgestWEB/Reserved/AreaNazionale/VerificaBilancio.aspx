<%@ Page Language="C#"  MasterPageFile="~/FenealgestwebNew.Master" AutoEventWireup="true" CodeBehind="VerificaBilancio.aspx.cs" Inherits="FenealgestWEB.Reserved.AreaNazionale.VerificaBilancio" Title="Verifica invio rendiconti Feneal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipale" runat="server">
<div class="content">
    <div class="login">
				<h2>Verifica invio rendiconti</h2>
				<br/>
				<br/>
				
			<font class="largelabel">Anno</font>
            <asp:DropDownList ID="cboAnno" runat="server" Font-Size="12px" Width="200px">
            </asp:DropDownList>
            <br / />
            <div class="spacer">
            </div>
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" onselectedindexchanged="RadioButtonList1_SelectedIndexChanged" 
                    >
                <asp:ListItem Selected="True">Provinciale</asp:ListItem>
                <asp:ListItem>Regionale</asp:ListItem>
            </asp:RadioButtonList>

             <div class="spacer">
            </div>
            
            <font class="largelabel">Tipo rendiconto</font>
            <asp:DropDownList ID="cboTipo" runat="server" Font-Size="12px" Width="200px">
                <asp:ListItem>FENEAL</asp:ListItem>
                <asp:ListItem>RLST</asp:ListItem>
            </asp:DropDownList>
            <br / >
            <div class="spacer">
            </div>
            
           
            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="accedi" 
                    onclick="cmdRicerca_Click">
                  Esegui verifica <asp:Image ID="Image3" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>
			<br />
			
			<hr />

            <asp:GridView ID="GridView1" runat="server" CssClass="generic_table">
            </asp:GridView>

    </div>
    <div class="column">
			    <h3>Dati di login</h3>
			        <%=this.HtmlLoginDataList("../../images/info.png")%>	    
			    <h3>Help on-line</h3>
			    <ul>
				    <li>
					    <h4>Presentazione contenuti pagina</h4>	
					    <font class="data">Pagina verifica invio rendiconti</font>						
					    La pagina corrente permette di verificare l’invio dei rendiconti.<br />
              La pagina mostra un campo dove impostare l’anno e richiede di effettuare una scelta tra i rendiconti regionali o provinciali.<br />
              Premendo su “Esegui verifica” verrà avviata la ricerca dei rendiconti presenti all’interno del sistema.<br />
              I risultati della ricerca sono automaticamente inseriti in una griglia dove sono mostrati la descrizione della regione o provincia e l’indicazione dell’effettuato invio.
				    </li> 
				    													
			    </ul>											
		  </div>    
</div>
</asp:Content>
