﻿<%@ Page Language="C#" MasterPageFile="~/FenealgestwebNew.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="FenealgestWEB.Index" Title="Home Fenealgest" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
  Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipale" runat="server">
<div class="content">
          <div class="login">
				<h2>Home page</h2>
				<br/>
				<br/>
				<font class="label">Username</font>
				<asp:TextBox runat="server" ID="txtUsername1" CssClass="user"></asp:TextBox>
				<font class="label">Password</font>
				<asp:TextBox runat="server" ID="txtPassword1" CssClass="password" 
                    TextMode="Password"></asp:TextBox>
                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="accedi" 
                    onclick="LinkButton1_Click">
                  Accedi <img  alt="Accedi" src="images/right_arrow.png" style="border:0px" />
                </asp:LinkButton>
				<div class="message_box">
					<img alt="" src="images/info.png" />
					Benvenuto nell'area login
					<br />
					<br />
                    <asp:Literal ID="errorMessage" runat="server"></asp:Literal>                   
				</div>
		  </div>

					
		  <div class="column">
			    <h3>Novità dal mondo FenealGest</h3>
		    
			   <%-- <%this.GetNews();%>

			    <h3>News dalla Feneal-Uil</h3>--%>
			    <ul id="news_2">
				    <li>
					    <h4>Visita l'area di download per scaricare le ultime novità</h4>	
					    <font class="data">13 luglio 2013</font>						
					    Nell'area di download del portale è possibile trovare le novità dei software Fenealgest.
                        Contattare l'amministratore del portale per l'installazione di importanti novità nell'automazione delle procedure 
                        di approviggionamento dei dati dalle casse edili
				    </li> 
				    														
			    </ul>											
		  </div>
</div>

</asp:Content>
