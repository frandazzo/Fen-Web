<%@ Page Language="C#" Theme="Default" MasterPageFile="~/FenealgestwebNew.Master" AutoEventWireup="true" CodeBehind="VisualizzaCode.aspx.cs" Inherits="FenealgestWEB.Reserved.AreaRegionale.VisualizzaCode" Title="Visualizza code di importazione in attesa" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipale" runat="server">
  <div class="content">
         <div class="login">
				<h2>Visualizzazione code di importazione in attesa</h2>
				<br/>
				<br/>  
                <font class="label">Regione</font>
                <asp:DropDownList ID="cboRegione" runat="server">
                </asp:DropDownList>
                <br/>
				<br/> 
                <asp:LinkButton ID="cmdRicerca1" runat="server" CssClass="accedi" 
                    onclick="cmdRicerca_Click">
                    Ricerca importazioni in attesa<asp:Image ID="Image3" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>

                <br />
                <br />

                <asp:GridView ID="GridView1" runat="server" CssClass="generic_table">
                </asp:GridView>

                <br />
                
                <asp:LinkButton ID="cmdAggiorna1" runat="server" CssClass="accedi" 
                    onclick="cmdAggiorna_Click">
                    Aggiorna<asp:Image ID="Image1" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>

    </div>
    
        <div class="column">
			    <h3>Dati di login</h3>
			        <%=this.HtmlLoginDataList("../../images/info.png") %>	    
			    <h3>Help on-line</h3>
			    <ul>
				    <li>
					    <h4>Presentazione contenuti pagina</h4>	
					    <font class="data">Pagina visualizzazione code in attesa</font>						
					    La pagina corrente permette di ricercare eventuali code in attesa di essere processate.<br />
              La pagina mostra un campo preimpostato con l’indicazione della regione se trattasi di utente regionale, o non preimpostato se trattasi di un utente nazionale.<br />
              Premendo su “Ricerca importazioni in attesa” verrà avviata la ricerca delle importazioni in attesa presenti all’interno del sistema.<br />
              I risultati della ricerca sono automaticamente inseriti in una griglia sottostante dove sono mostrati: etichetta e data di arrivo.<br />
				    </li> 
				    													
			    </ul>											
		  </div>   
</div>
</asp:Content>

