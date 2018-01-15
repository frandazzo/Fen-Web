<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeaderReserved.ascx.cs" Inherits="FenealgestWEB.assets.control.HeaderReserved" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>


         <script type="text/javascript" >

             function UnregisterMail() {
                 if (mail.GetText() == '')
                     return;
                 callback.PerformCallback(mail.GetText() + '#');
             }

             function RegisterMail() {
                 callback.PerformCallback(mail.GetText());
             }

             function EndCallback(s, e) {
                 if (e.result == '')
                     return;
                 document.getElementById('newslettermessage').innerHTML = e.result;
                 popup.Show();
             }
    
         </script> 


<div class="border_bottom">
    <div id="header">
	    <div class="left">			
		    <a href="#">
			    <%--<img alt="" src="~/images/logo.png" class="logo" />--%>
			    <asp:Image ID="Image1" runat="server" CssClass="logo" ImageUrl="~/images/logo.png" />
		    </a>
	    </div>	
	    <div class="right">
		    <div class="bg">
			    <div class="menu">	  
                    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" ClientInstanceName="mail" Width="170px" CssClass="newsletter" NullText="Iscriviti alla newsletter!" > <NullTextStyle ForeColor="#999999"> </NullTextStyle> </dx:ASPxTextBox>
                    <asp:LinkButton ID="LinkButton2" runat="server" CssClass="invia" OnClientClick="RegisterMail();return false;">Registrati <asp:Image ID="Image11" runat="server" ImageUrl="~/images/right_arrow.png" /> &nbsp;&nbsp; </asp:LinkButton> <asp:LinkButton ID="LinkButton3" runat="server" CssClass="invia" OnClientClick="UnregisterMail();return false;">Deregistrati <asp:Image ID="Image3" runat="server" ImageUrl="~/images/down_arrow.png" /></asp:LinkButton>
				    <ul>
					    <%--<li>
						    <a href="#">
							    Recupera password
						    </a>
					    </li>--%>
					    <li>
						    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="invia" 
                                onclick="LinkButton1_Click">
						        Esci <asp:Image ID="Image2" runat="server" ImageUrl="~/images/down_arrow.png" /> 
						    </asp:LinkButton>
					    </li>
				    </ul>
			    </div>	
		    </div>
	    </div> 					
    </div>
</div>

<dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" 
    ClientInstanceName="popup" AllowDragging="True" 
    HeaderText="Iscrizione newsletter" Modal="True" PopupAction="None" 
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            <div id="newslettermessage">
            </div>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <HeaderStyle HorizontalAlign="Left" />
</dx:ASPxPopupControl>

<dx:ASPxCallback ID="ASPxCallback1" runat="server" 
    ClientInstanceName="callback" oncallback="ASPxCallback1_Callback" >
    <ClientSideEvents CallbackComplete="EndCallback" />
</dx:ASPxCallback>
