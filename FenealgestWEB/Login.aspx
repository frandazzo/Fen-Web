<%@ Page Language="C#" MasterPageFile="~/Fenealgestweb.Master" AutoEventWireup="true" Theme="Default" CodeBehind="Login.aspx.cs" Inherits="FenealgestWEB.Login" Title="" %>

<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
</asp:Content>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderPrincipale" runat="server">
    
    <asp:Label ID="lblTitolo" runat="server" CssClass="lblTitoloChild"></asp:Label>   
   
   
        <table class="table_login">
                        <tr id="TrLoginDistanza">
                        <td colspan="4">
                            &nbsp;</td>
                        </tr>
                        <tr>
                         <td class="TdLoginDistanza">

                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                             &nbsp;</td>
                            <td>
                            <div class="label_login">
                                <asp:Label ID="Label1" runat="server" Text="USERNAME " Font-Bold="True"></asp:Label>
                            </div> 
                            </td>
                            <td>
                                <asp:TextBox ID="txtUserName" runat="server" TabIndex="1"></asp:TextBox>
                            </td>
                            <td rowspan="2">
                               <asp:ImageMap ID="ImageMap1" runat="server" HotSpotMode="PostBack" 
                                    ImageUrl="~/immagini/login_button.png"  onclick="ImageMap1_Click" 
                                    CssClass="Accedi">
                                    
                                    <asp:CircleHotSpot Radius="38" X="72" Y="72" TabIndex="3" />
                                </asp:ImageMap>
                            </td>
                        </tr>
                         <tr>
                          <td class="TdLoginDistanza">

                            </td>
                            <td>
                            <div class="label_login">
                                    <asp:Label ID="Label2" runat="server" Text="PASSWORD" Font-Bold="True"></asp:Label>
                             </div> 
                             </td>
                            <td>
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" TabIndex="2" 
                                    on AutoPostBack="True" ontextchanged="txtPassword_TextChanged" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>

                            </td>
                            <td colspan="3">
                            <div class="piedeLogin">
                                <asp:Label ID="lblTryMessage" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="lblText" runat="server" Text="Benvenuto nell'area di login!"></asp:Label>
                            </div> 
                            </td>
                        </tr>   
        </table>
   
</asp:Content>
