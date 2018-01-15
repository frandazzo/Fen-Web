<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ReservedArea.Master" AutoEventWireup="true" CodeBehind="VisualizzaDownload.aspx.cs" Inherits="FenealgestWEB.Reserved.AreaAmministrativa.VisualizzaDownload" Title="Pagina senza titolo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ChildContent1" runat="server">
   <%-- <asp:Label ID="lblTitolo" runat="server" CssClass="lblTitoloChild"></asp:Label>
    <br /><br />
    <div align="center">
    <div class="label_login">
    <div id="contenutoPrincipaleCentratoDownl">
        <table id="tabellaRicerca">
            <tr>
                <td class="TdRicerca">
                    <div class="label_login">
                        <asp:CheckBox ID="chbData" runat="server" 
                            oncheckedchanged="chbData_CheckedChanged" AutoPostBack="True" />
                        <asp:Label ID="Label9" runat="server" Text="Data di caricamento" Font-Bold="True"></asp:Label>
                    </div>
                </td>
                <td>
                    <BDP:BDPLite ID="bdpData" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="TdRicerca">
                    <div class="label_login">
                        <asp:Label ID="Label2" runat="server" Text="Tipo download" Font-Bold="True"></asp:Label>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="cboTipo" runat="server">
                    </asp:DropDownList> 
                </td>
            </tr>
            <tr>
                <td class="TdRicerca">
                    <div class="label_login">
                        <asp:Label ID="Label4" runat="server" Text="Regione" Font-Bold="True"></asp:Label>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="cboRegione" runat="server">
                    </asp:DropDownList> 
                </td>
            </tr>
            <tr>
                <td class="TdRicerca">
                    <div class="label_login">
                        <asp:Label ID="Label3" runat="server" Text="Risultati per pagina" Font-Bold="True"></asp:Label>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="txtResultPerPage" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Button ID="cmdCreaDownload" runat="server" 
                        Text="Nuovo download" onclick="cmdCreaDownload_Click" />
                </td>
                <td>
                    <asp:Button ID="cmdSearch" runat="server" Text="Ricerca" 
                        onclick="cmdSearch_Click" />
                </td>
            </tr>
        </table>
        </div>
        <br />
    
        <asp:Panel Visible="false" id="pnlGrid" runat="server" CssClass="divDocumIscriz">
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
        </asp:Panel>
        <br />
        <asp:Label ID="lblTotalResults" runat="server" Text=""></asp:Label>
         <br />
        <asp:LinkButton ID="lnkPrec" runat="server" onclick="lnkPrec_Click">&lt; Risultati precedenti</asp:LinkButton>
        
        &nbsp;
        <asp:LinkButton ID="lnkSucc"  runat="server" onclick="lnkSucc_Click">Risultati successivi &gt;</asp:LinkButton>
        
           <br />
        <asp:Label ID="lblVis" runat="server" Text="Visualizzazione record ...."></asp:Label>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Elementi trovati..."></asp:Label>
    </div>
    </div>--%>
</asp:Content>
