<%@ Page Language="C#"  MasterPageFile="~/FenealgestwebNew.Master" AutoEventWireup="true" CodeBehind="VisualizzaLavoratore.aspx.cs" Inherits="FenealgestWEB.Reserved.AreaNazionale.VisualizzaLavoratore" Title="Visualizza dettaglio lavoratore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipale" runat="server">
  <div class="content">
    <div class="login">
				<h2>Dettagli lavoratore</h2>
				<br/>
				<br/>
				
			<font class="largelabel">Codice fiscale</font> 
            <asp:Label ID="lblCodFisc" runat="server"></asp:Label>
            <br />
            <div class="spacer">
            </div>         
            <font class="largelabel">Cognome</font> 
            <asp:Label ID="lblCognome" runat="server"></asp:Label>
            <br />
             <div class="spacer">
            </div> 
            <font class="largelabel">Nome</font> 
            <asp:Label ID="lblNome" runat="server"></asp:Label>
            <br />
             <div class="spacer">
            </div> 
            <font class="largelabel">Data nascita</font> 
            <asp:Label ID="lblDataNasc" runat="server"></asp:Label>
            <br />
             <div class="spacer">
            </div> 
            <font class="largelabel">Comune nascita</font> 
            <asp:Label ID="lblComuneNasc" runat="server"></asp:Label>
            <br />
             <div class="spacer">
            </div> 
            <font class="largelabel">Provincia nascita</font> 
            <asp:Label ID="lblProvinciaNasc" runat="server"></asp:Label>          
            <br />
             <div class="spacer">
            </div> 
            <font class="largelabel">Nazionalità</font> 
            <asp:Label ID="lblNazione" runat="server"></asp:Label>
            <br />
             <div class="spacer">
            </div> 
            <font class="largelabel">Sesso</font> 
            <asp:Label ID="lblSesso" runat="server"></asp:Label>
            <br />
             <div class="spacer">
            </div> 
            <font class="largelabel">Indirizzo</font> 
            <asp:Label ID="lblIndirizzo" runat="server"></asp:Label>
            <br />
             <div class="spacer">
            </div> 
            <font class="largelabel">Cap</font> 
            <asp:Label ID="lblCap" runat="server"></asp:Label>
            <br />
             <div class="spacer">
            </div> 
            <font class="largelabel">Com. residenza</font> 
            <asp:Label ID="lblComuneRes" runat="server"></asp:Label>
            <br />
             <div class="spacer">
            </div> 
            <font class="largelabel">Prov. residenza</font> 
            <asp:Label ID="lblProvinciaRes" runat="server"></asp:Label>
            <br />
             <div class="spacer">
            </div> 
            <font class="largelabel">Telefono</font> 
            <asp:Label ID="lblTelefono" runat="server"></asp:Label>
            <br />
             <div class="spacer">
            </div> 
            <br />
            <font class="largelabel">Ultima modifica</font> 
            <asp:Label ID="lblDataMod" runat="server"></asp:Label>
            <br />
             <div class="spacer">
            </div> 
            <font class="largelabel">Ultima modica Feneal</font> 
            <asp:Label ID="lblProvMod" runat="server"></asp:Label>
            <br />
            <br />
        
        
        
            <font class="largelabel">Iscrizioni</font> 

               <div style="width:470px; height:100px; overflow:auto;">
               

                    <asp:GridView ID="grdIscrizioni" runat="server" CssClass="generic_table" >
                        <AlternatingRowStyle BackColor="#FF9900" />
                    </asp:GridView>
                    
               </div>
          
            <br />
            <br />
            
            
            <font class="largelabel">Documenti Fenealgest</font> 
    
           <div style="width:470px; height:100px; overflow: scroll;">
                <asp:GridView ID="grdDocumenti" runat="server" CssClass="generic_table">
                     <AlternatingRowStyle BackColor="#FF9900" />
                </asp:GridView>
           </div>

    </div>
    <div class="column">
			    <h3>Dati di login</h3>
			        <%=this.HtmlLoginDataList("../../images/info.png")%>	    
			    <h3>Help on-line</h3>
			    <ul>
				    <li>
					    <h4>Presentazione contenuti pagina</h4>	
					    <font class="data">Pagina dettaglio lavoratore</font>	
					    La pagina corrente permette di visualizzare i dati di dettaglio del lavoratore.					
					    All’interno di questa pagina per il singolo lavoratore, nell’ordine, sono mostrati: codice fiscale, cognome, nome, data e luogo di nascita, nazionalità, sesso, indirizzo, cap, comune di residenza, provincia di residenza, telefono, data di ultima modifica, ultima provincia a modificare i dati, tutte le iscrizioni (inserite in una griglia) e tutti i documenti (inseriti anch’essi in una griglia).
				    </li> 
				    													
			    </ul>											
		  </div>    
    
 </div>
</asp:Content>
