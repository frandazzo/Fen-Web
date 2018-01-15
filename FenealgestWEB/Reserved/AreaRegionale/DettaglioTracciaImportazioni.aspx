<%@ Page Language="C#"  MasterPageFile="~/FenealgestwebNew.Master" AutoEventWireup="true" CodeBehind="DettaglioTracciaImportazioni.aspx.cs" Inherits="FenealgestWEB.Reserved.AreaRegionale.DettaglioTracciaImportazioni" Title="Dettaglio importazione" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipale" runat="server">
   <div class="content">
    <div class="login">
		<h2>Dettaglio importazione</h2>
		<br/>
	    <br/> 
	    <div class="simple_text">
	        <%= this.IntestazioneImportazione %>
	    </div>
	    <br />
	    <div class="message_box">
	        <div class="lista">
	            <%= this.DatiImportazione %>
	        </div>
	    </div>
	     <br />
	            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="accedi" 
                     onclick="cmdAnnulla_Click">
                    Torna alla ricerca <asp:Image ID="Image3" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>


            <br />
            <br />
        
        
        <div class="message_box">
            <h4>Elimina importazione</h4>
            Procedura per la cancellazione di tutti i dati importati con l'importazione corrente
            <br />
            <br />   
            
        </div>
        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="accedi"  
                    OnClientClick="return confirm('Sicuro di voler procedere?');" onclick="cmdDeleteImportazione_Click">
                    Elimina importazione <asp:Image ID="Image1" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>

            <%--<asp:Button ID="cmdDeleteImportazione" runat="server" Text="Procedi"
            OnClientClick="return confirm('Sicuro di voler continuare?');" 
                onclick="cmdDeleteImportazione_Click" />
            &nbsp;--%>
       
       <%-- <asp:Button ID="cmdAnnulla" runat="server" Text="Torna alla ricerca"
            onclick="cmdAnnulla_Click" />--%>
    </div>
        <div class="column">
			    <h3>Dati di login</h3>
			        <%=this.HtmlLoginDataList("../../images/info.png") %>	    
			    <h3>Help on-line</h3>
			    <ul>
				    <li>
					    <h4>Presentazione contenuti pagina</h4>	
					    <font class="data">Pagina dettaglio importazione</font>	
					    La pagina corrente permette di visualizzare i dati di dettaglio dell'importazione.					
					    All’interno di questa pagina per la singola importazione, nell’ordine, sono mostrati: provincia, settore, periodo, data esportazione, responsabile, tipo esportazione e data ultima modifica. All’interno della pagina è possibile cancellare l’importazione visualizzata o tornare sulla pagina di ricerca.
				    </li> 
				    													
			    </ul>											
		  </div>   
 </div>
</asp:Content>
