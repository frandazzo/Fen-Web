<%@ Page Language="C#" MasterPageFile="~/Fenealgestweb.Master" AutoEventWireup="true" Theme="Default" CodeBehind="Home.aspx.cs" Inherits="FenealgestWEB.Home" Title="" %>

<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <div style="background-color: #938E8B; padding-top:10px; color: #000066;">
    </div>
<div style="background-color: #938E8B;" >
</div>
    

    <div class="menuContentBottom">
    
    &nbsp;&nbsp;&nbsp;
        <br />
        <a href="javascript:WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions(">
        </a><br />
    </div>
    <asp:Image ID="Image1" ImageUrl="~/immagini/menu_separator.png"  runat="server" />
    

</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderPrincipale" runat="server">
    <table class="Tabella_News" >
    
                    <tr>
                        <td colspan="2" >
                        <div class="label_news">
                            <asp:Label ID="Label2" runat="server" Text="News" ></asp:Label>
                            <hr />
                        </div> 
                        </td>
                    </tr>
                    
                    
                    <tr>
                        <td>
                        <div class="titolo_news">
                            <asp:Label ID="lblTitolo1" runat="server" ></asp:Label>
                        </div> 
 
                        </td>
                        <td>
                        <div class="titolo_news" align="right">
                            <asp:Label ID="lblData1" runat="server"></asp:Label>
                        </div> 
 
                        </td>
                    </tr>
                     <tr>
                        <td colspan="2">
 
                        <div class="contenuto_news">
                            <asp:Label ID="lblTesto1" runat="server" ></asp:Label>
                        </div> 
                        </td>
                    </tr> 
                    <tr>
                        <td>
                            <div class="titolo_news">
                                <asp:Label ID="lblTitolo2" runat="server" ></asp:Label>
                            </div> 
                         </td>
                        <td>
                        <div class="titolo_news" align="right">
                            <asp:Label ID="lblData2" runat="server"></asp:Label>
                        </div> 
 
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="contenuto_news">
                                <asp:Label ID="lblTesto2" runat="server" ></asp:Label>
                            </div> 
                         </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="titolo_news">
                                <asp:Label ID="lblTitolo3" runat="server" ></asp:Label>
                            </div> 
                        </td>
                        <td>
                        <div class="titolo_news" align="right">
                            <asp:Label ID="lblData3" runat="server"></asp:Label>
                        </div> 
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="contenuto_news">
                                <asp:Label ID="lblTesto3" runat="server" ></asp:Label>
                            </div> 
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="titolo_news">
                                <asp:Label ID="lblTitolo4" runat="server" ></asp:Label>
                            </div> 
                        </td>
                        <td>
                        <div class="titolo_news" align="right">
                            <asp:Label ID="lblData4" runat="server"></asp:Label>
                        </div> 
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="contenuto_news">
                                <asp:Label ID="lblTesto4" runat="server" ></asp:Label>
                            </div> 
                        </td>
                    </tr>
                    <tr>

                         <td  class="piede_news">
                            <asp:LinkButton ID="lnkPrec" runat="server" onclick="lnkPrec_Click" >&lt; News precedenti</asp:LinkButton>
                         <br/>
                        </td>
                        <td class="piede_news" align="right">
                        <asp:LinkButton ID="lnkSucc"  runat="server" onclick="lnkSucc_Click" >News successive &gt;</asp:LinkButton>
                         <br/>
                        </td>
                    </tr>
    </table>
</asp:Content>
