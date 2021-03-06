﻿<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ReservedArea.Master" AutoEventWireup="true" CodeBehind="GestioneDownload.aspx.cs" Inherits="FenealgestWEB.Reserved.AreaAmministrativa.GestioneDownload" Title="Pagina senza titolo" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<%--<%@ Register Assembly="FUA" Namespace="Subgurim.Controles" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ChildContent1" runat="server">
    <asp:Label ID="lblTitolo" runat="server" CssClass="lblTitoloChild"></asp:Label>
    <br /><br />
    <div align="center" id="contenutoPrincipaleGesDow"> 
    <div class="label_login">
    <asp:Panel align="left" ID="Panel1" runat="server" >
         <table>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Data" Font-Bold="True"></asp:Label>
                        &nbsp;  
                    </td>
                    <td>
                        
                        <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server">
                        </dx:ASPxDateEdit>
                    </td>
                    
                    <td>
                    
                    </td>
                </tr>
                <tr>
                    <td >
                        <asp:Label ID="Label1" runat="server" Text="Descrizione" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescrizione" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Agg" ControlToValidate="txtDescrizione" runat="server" ErrorMessage="Descrizione obbligatoria"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="TdRicerca">
                        <asp:Label ID="Label5" runat="server" Text="Creato da" Font-Bold="True"></asp:Label>
            
                    </td>
                    <td>
                        <asp:TextBox ID="txtCreatoDa" runat="server"></asp:TextBox>
                    </td>
                     <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Agg" ControlToValidate="txtCreatoDa" runat="server" ErrorMessage="Descrizione obbligatoria"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="TdRicerca">
                        <asp:Label ID="Label4" runat="server" Text="Tipo" Font-Bold="True"></asp:Label>
            
                    </td>
                    <td>
                        <asp:DropDownList ID="cboTipo" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>

                    </td>
                </tr>
                <tr>
                    <td class="TdRicerca">
                        <asp:Label ID="Label6" runat="server" Text="Regione" Font-Bold="True"></asp:Label>
            
                    </td>
                    <td>
                        <asp:DropDownList ID="cboRegione" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>

                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="lblFileDim" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TdRicerca">
                        <asp:Label ID="Label3" runat="server" Text="File" Font-Bold="True"></asp:Label>
            
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server"   />
                    </td>
                    <td>
                    
                    </td>
                </tr>
                <tr>
                    <td class="TdRicerca">
                        <asp:Label ID="lblNomeFile" runat="server" Text="Salvato in" Visible="false" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPercorsoFile" runat="server" Text="" Visible="false"></asp:Label>
                    </td>
                    <td>
                    
                    </td>
                </tr>
            </table>
            <br />
        </asp:Panel>
        
        <div id="buttonGesDown">
            <asp:Button ID="cmdCreaDownload" runat="server" Text="Nuovo" 
                onclick="cmdCreaDownload_Click"/>
            &nbsp;

          
            <asp:Button ID="cmdSave" runat="server" Text="Aggiorna" ValidationGroup="Agg" 
                onclick="cmdSave_Click" />
            &nbsp;
             <asp:Button runat="server" Text="Button" ID="button3" style="display: none;" />
            <asp:Button ID="cmdDeleteDownload" runat="server" Text="Cancella" 
                onclick="cmdDeleteDownload_Click"/>
            &nbsp;
            <asp:Button ID="cmdAnnulla" runat="server" onclick="cmdAnnulla_Click" 
                Text="Annulla"/>
        </div>
    </div>
    </div>
</asp:Content>
