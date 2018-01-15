<%@ Page Language="C#" MasterPageFile="~/FenealgestwebNew.Master" AutoEventWireup="true" CodeBehind="RinnovaPassLogged.aspx.cs" Inherits="FenealgestWEB.Reserved.AreaPubblica.RinnovaPassLogged" Title="Rinnova password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipale" runat="server">

<div class="content">
    <div class="login">
			<h2>Password dimenticata</h2>
			<br/>
			<br/>

			<font class="label">Password corrente</font>
            <asp:TextBox ID="TextBox1" runat="server" CssClass="user" TextMode="Password"></asp:TextBox>	
			<br />
			<br />
			<font class="label">Nuova password</font>
            <asp:TextBox ID="txtReNewPass" runat="server" CssClass="user" TextMode="Password"></asp:TextBox>	

			<font class="label">Ridigita password</font>	
			<asp:TextBox ID="TextBox2" CssClass="user" runat="server" TextMode="Password"></asp:TextBox>
			<br />
			<br/>
			<asp:LinkButton ID="LinkButton1" runat="server" CssClass="accedi" 
                    onclick="btnRinnova_Click">
                    Rinnova password 
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/images/right_arrow.png" /> 
            </asp:LinkButton>
			<br />
			<br />
			<div class="message_box">
			    <asp:Literal ID="lblPassErrata1" runat="server"></asp:Literal>	
	        </div>				
    </div>
        <div class="column">
			    <h3>Dati di login</h3>
			        <%=this.HtmlLoginDataList("../../images/info.png") %>	    
			    <h3>Help on-line</h3>
			    <ul>
				    <li>
					    <h4>Presentazione contenuti pagina</h4>	
					    <font class="data">Pagina per rinnovare la password</font>						
					    La pagina corrente permette di rinnovare la propria password.<br />
              La pagina mostra tre campi in cui inserire: la password corrente e per due volte la digitalizzazione della nuova password.<br />
              Premendo su “Rinnova password” in caso di esito positivo la password viene sostituita, mentre a video viene mostrato il messaggio: “Password modificata con successo”.
				    </li> 
				    													
			    </ul>											
		  </div>   
 </div>

   
</asp:Content>


