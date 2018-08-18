<%@ Page Language="C#"  MasterPageFile="~/FenealgestwebNew.Master" AutoEventWireup="true" CodeBehind="GestioneNewsletter.aspx.cs" Inherits="FenealgestWEB.Reserved.AreaAmministrativa.GestioneNewsletter" Title="Invia newsletter" %>

<%@ Register assembly="DevExpress.Web.ASPxHtmlEditor.v18.1, Version=18.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHtmlEditor" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v18.1, Version=18.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxSpellChecker.v18.1, Version=18.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSpellChecker" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipale" runat="server">
 <div class="content">
    <div class="login">
		<h2>Invia newsletter</h2>
		<br/>
	
        <font class="largelabel">Oggetto</font>

        <asp:TextBox ID="txtOggetto" runat="server" CssClass="largeinput"></asp:TextBox>
        <br/>
        <br/>
        

        <dx:ASPxHtmlEditor ID="txtTesto1"  runat="server" ActiveView="Preview" 
             Width="100%">
            <SettingsImageSelector>
                <CommonSettings AllowedFileExtensions=".jpe, .jpeg, .jpg, .gif, .png"></CommonSettings>
            </SettingsImageSelector>
            <Settings AllowHtmlView="False" AllowPreview="False" />
            <SettingsImageUpload>
            <ValidationSettings AllowedFileExtensions=".jpe, .jpeg, .jpg, .gif, .png"></ValidationSettings>
            </SettingsImageUpload>
            <SettingsResize AllowResize="True" />
        </dx:ASPxHtmlEditor>

        <br/>
        <br/>
        <br/>
        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="accedi" 
                    onclick="Button1_Click">
                  Invia <asp:Image ID="Image3" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>
        &nbsp;&nbsp;
		<asp:LinkButton ID="LinkButton2" runat="server" CssClass="accedi" 
            onclick="LinkButton2_Click" >
                  Invia mail di test all'utente loggato <asp:Image ID="Image1" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>
		
        
        <div class="message_box">
			<asp:Literal ID="lblMessaggio1" runat="server"></asp:Literal>	
	    </div>		
        
    </div>
    <div class="column">
			    <h3>Dati di login</h3>
			        <%=this.HtmlLoginDataList("../../images/info.png")%>	    
			    <h3>Help on-line</h3>
			    <ul>
				    <li>
					    <h4>Presentazione contenuti pagina</h4>	
					    <font class="data">Pagina creazione e invio newsletter</font>						
					    La pagina corrente permette di inviare una newsletter a coloro che si sono registrati al servizio.<br />
              La pagina mostra un campo in cui inserire l’oggetto e una zona sottostante in cui inserire il testo della newsletter. Tale zona permette di formattare il testo attraverso la sua barra superiore.<br />
              Premendo su “Invia” la newsletter verrà inviata a coloro che si sono registrati al servizio.<br />
              Premendo su “Invia mail di test all’utente loggato” la newsletter verrà inviata unicamente all’utente loggato al fine di effettuarne un test.
				    </li> 
				    													
			    </ul>											
		  </div>     
</div>
</asp:Content>
