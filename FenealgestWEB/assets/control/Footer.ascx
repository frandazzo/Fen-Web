<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Footer.ascx.cs" Inherits="FenealgestWEB.assets.control.Footer" %>

<div class="footer">
    
	<div class="row">
	     <asp:Image ID="Image332" runat="server" ImageUrl="~/images/utenti.png" class="users"></asp:Image>
	     <font class="numero">Numero visitatori online:<strong> <%=this.GetVisitorNumber()%></strong></font>
		<a href="http://www.tecnoesis.it" target="_blank" class="noesis"> <asp:Image ID="Image333" runat="server" ImageUrl="~/images/logo_noesis.png"  /></a>
	</div>				
</div>