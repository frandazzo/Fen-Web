<%@ Page Title="Oops! si è verificato un errore!" Language="C#" MasterPageFile="~/FenealgestwebNew.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="FenealgestWEB.ErrorHandling.ErrorPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipale" runat="server">
<div class="content">
          <div class="login">
				<h2>Oops! Si è verificato un errore!</h2>
				<br/>
				<br/>

         <asp:Label ID="lblErrorMessage" runat="server" Text="Eccezione"></asp:Label>
        <br />
        <asp:Label ID="lblErrorType" runat="server" Text="Tipo"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblInternal" runat="server" Text="Eccezione interna"></asp:Label>
        <br />
        <asp:Label ID="lblInternalType" runat="server" Text="Tipo eccezione interna"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblStack" runat="server" Text="Stack"></asp:Label>
    </div>
</div>
</asp:Content>
