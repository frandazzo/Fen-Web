<%@ Page Language="C#"  MasterPageFile="~/FenealgestwebNew.Master" AutoEventWireup="true" CodeBehind="PassDimenticata.aspx.cs" Inherits="FenealgestWEB.Reserved.AreaPubblica.PassDimenticata" Title="Password dimenticata" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipale" runat="server">
 
 <div class="content">
    <div class="login">
				<h2>Password dimenticata</h2>
				<br/>
				<br/>
				
			<font class="label">Username</font>
			<asp:TextBox ID="txtUser" runat="server" CssClass="user"></asp:TextBox>
			<asp:LinkButton ID="LinkButton1" runat="server" CssClass="accedi" 
                    onclick="btnInvio_Click">
                  Invia <asp:Image ID="Image3" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>
			<br />
			<br />
			<div class="message_box">
			    <asp:Literal ID="lblMessaggio1" runat="server"></asp:Literal>	
	        </div>		
			
    </div>
     <div class="column">
			      
			    <h3>Help on-line</h3>
			    <ul>
				    <li>
					    <h4>Presentazione contenuti pagina</h4>	
					    <font class="data">Pagina per l'invio della password dimenticata</font>						
					    La pagina corrente permette di recuperare la propria password.<br />
              La pagina mostra un campo in cui inserire il proprio username.<br />
              Premendo il tasto “Invia” nel caso di user valido una mail contenete la password verrà inviata all’indirizzo di posta elettronica collegato allo user in questione.
				    </li> 
				    													
			    </ul>											
		  </div>  
 </div>

</asp:Content>
