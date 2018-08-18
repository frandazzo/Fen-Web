<%@ Page Title="Calcola codice fiscale" Language="C#" MasterPageFile="~/FenealgestwebNew.Master" AutoEventWireup="true" CodeBehind="CodiceFiscale.aspx.cs" Inherits="FenealgestWEB.Reserved.AreaPubblica.CodiceFiscale" %>

<%@ Register assembly="DevExpress.Web.v18.1, Version=18.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipale" runat="server">

 <div class="content">
    <div class="login">
		<h2>Calcola codice fiscale</h2>
		<br/>
		<br/>
        <font class="largelabel">Cognome</font>
        <asp:TextBox ID="txtCognome" runat="server" CssClass="largeinput"></asp:TextBox>
        <br/>  
        <font class="largelabel">Nome</font>
        <asp:TextBox ID="txtNome" runat="server" CssClass="largeinput"></asp:TextBox>
        <br/> 

        <div class="devcontrolcontainer">
            <div class="labelcontroldiv" >
                <font class="largelabel" style="margin-top: 6px;margin-right: 5px;">Data nascita</font>
            </div>
	        <div class="devcontroldiv">
	            <dx:ASPxDateEdit ID="bdpData1" runat="server" Width="150px" 
                    TodayButtonText="Oggi" AllowNull="False">
                    <CalendarProperties TodayButtonText="Oggi">
                    </CalendarProperties>
                </dx:ASPxDateEdit>
	        </div>
        </div>
        
        <br/> 
        <font class="largelabel">Sesso</font>             
        <asp:DropDownList ID="cboSesso" runat="server" Font-Size="12px" Width="200px">
            <asp:ListItem>MASCHIO</asp:ListItem>
            <asp:ListItem>FEMMINA</asp:ListItem>
        </asp:DropDownList>
        <br/>             
        <font class="largelabel" >Nazione</font>          
        <asp:DropDownList ID="cboNazione" runat="server" AutoPostBack ="true" Font-Size="12px" Width="200px"
            onselectedindexchanged="cboNazione_SelectedIndexChanged" >
        </asp:DropDownList>
        <br/>                     
        <font class="largelabel">Provincia</font>
        <asp:DropDownList ID="cboProvincia" runat="server" AutoPostBack ="true" Font-Size="12px" Width="200px"
            onselectedindexchanged="cboProvincia_SelectedIndexChanged" > 
        </asp:DropDownList>
        <br/>
        <font class="largelabel">Comune</font>
        <asp:DropDownList ID="cboComune" runat="server"  Font-Size="12px" Width="200px">   
        </asp:DropDownList>
        <br/>                                  
        <br />
        
            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="accedi" onclick="cmdCalcolaCodiceFiscale_Click">Calcola codice fiscale <asp:Image ID="Image3" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>
                        
              
         <br />            
         <hr />        
                               
            <font class="largelabel">Codice fiscale</font>
            <asp:TextBox ID="txtCodiceFiscale" runat="server" CssClass="largeinput"></asp:TextBox>
             <br />
             <br />
             <asp:LinkButton ID="LinkButton2" runat="server" CssClass="accedi" onclick="cmdDatiFisc_Click">Calcola dati fiscali <asp:Image ID="Image1" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>
         
         <div class="message_box">
             <font class="largelabel">Dati fiscali</font>
			    <asp:Literal ID="lblDatiFiscali1" runat="server"></asp:Literal>	
         </div>
         
    </div>
    <div class="column">
			    <h3>Dati di login</h3>
			        <%=this.HtmlLoginDataList("../../images/info.png")%>	    
			    <h3>Help on-line</h3>
			    <ul>
				    <li>
					    <h4>Presentazione contenuti pagina</h4>	
					    <font class="data">Pagina per il calcolo del codice fiscale</font>						
					    La pagina corrente permette di calcolare il codice fiscale dai dati anagrafici e viceversa.<br />
              La pagina mostra una serie di campi in cui inserire: cognome, nome, data di nascita, sesso, nazione e solo nel caso in cui si selezioni come nazione l’Italia si avrà la possibilità di settare la provincia e quindi il comune di nascita.<br />
              Premendo su “Calcola codice fiscale” il risultato è automaticamente mostrato nel campo “Codice fiscale”.<br />
              Nel caso in cui si voglia risalire dal codice fiscale ai dati anagrafici, inserire unicamente il codice fiscale e premere su “Calcola dati fiscali”. Nel campo dati fiscali compariranno nell’ordine: data di nascita, sesso, nazione, provincia e comune di nascita.
				    </li> 
				    													
			    </ul>											
		  </div>    
 </div>

</asp:Content>
