﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoggedUtilityMenu.ascx.cs" Inherits="FenealgestWEB.assets.control.LoggedUtilityMenu" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


			    
<div class="menu">
        <dx:ASPxMenu ID="ASPxMenu1" runat="server" AllowSelectItem="True" 
            BackColor="White" Orientation="Vertical" ShowSubMenuShadow="False" >
            <Paddings PaddingLeft="0px" />
            <ItemStyle ForeColor="#666666">
                <SelectedStyle BackColor="White">
                    <Border BorderStyle="None" />
                </SelectedStyle>
                <HoverStyle BackColor="#F4F4F4"  ForeColor="#FF9900">
                    <Border BorderStyle="None" />
                </HoverStyle>
                <Paddings PaddingBottom="3px" PaddingLeft="2px" PaddingTop="3px" />
            </ItemStyle>
            <SubMenuItemStyle>
                <SelectedStyle BackColor="White">
                    <Border BorderStyle="None" />
                </SelectedStyle>
                <HoverStyle BackColor="White" >
                    <Border BorderStyle="None" />
                </HoverStyle>
                <Paddings PaddingBottom="3px" PaddingLeft="2px" PaddingRight="2px" 
                    PaddingTop="3px" />
            </SubMenuItemStyle>
            <Border BorderStyle="None"/>
            <Items>
                <dx:MenuItem Text="Home" Image-Url="~/images/utenti.png" NavigateUrl="~/ReservedAreaHomePage.aspx">
                    <Image Url="~/images/utenti.png"></Image>
                </dx:MenuItem>
                <dx:MenuItem Text="Vai al sito Feneal" Image-Url="~/images/utenti.png" NavigateUrl="http://www.fenealuil.it" Target="_blank">
                    <Image Url="~/images/utenti.png"></Image>
                </dx:MenuItem>
                <dx:MenuItem Text="Area download" Image-Url="~/images/download.png"  NavigateUrl="~/DownloadArea.aspx">
                    <Image Url="~/images/download.png"></Image>
                </dx:MenuItem>
                <dx:MenuItem Text="La guida FenealGest" Image-Url="~/images/guida.png" NavigateUrl="http://www.fenealgest.it/manual/Esercitazioni/Index.html"  Target="_blank">
                    <Image Url="~/images/guida.png"></Image>
                </dx:MenuItem>
                <dx:MenuItem Text="Scarica la teleassistenza" Image-Url="~/images/teleassistenza.png" NavigateUrl="~/assets/setups/Teleassistenza.exe">
                    <Image Url="~/images/teleassistenza.png"></Image>
                </dx:MenuItem>
            </Items>
            <ItemImage Height="15px" Width="15px">
            </ItemImage>
            <SubMenuStyle BackColor="White" GutterColor="White">
               <Border BorderStyle="None" />
            </SubMenuStyle>
       </dx:ASPxMenu>
</div>
    			    

