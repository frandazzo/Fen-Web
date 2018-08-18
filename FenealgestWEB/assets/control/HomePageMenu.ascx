<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomePageMenu.ascx.cs" Inherits="FenealgestWEB.assets.control.HomePageMenu" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
    
    <div class="menu">
    			    
			            <dx:ASPxMenu ID="mainMenu" runat="server" AllowSelectItem="True" 
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
                            <SubMenuItemStyle ForeColor="#666666">
                                <SelectedStyle BackColor="White">
                                    <Border BorderStyle="None" />
                                </SelectedStyle>
                                <HoverStyle BackColor="#F4F4F4"   ForeColor="#FF9900">
                                    <Border BorderStyle="None" />
                                </HoverStyle>
                                <Paddings PaddingBottom="3px" PaddingLeft="2px" PaddingRight="2px" 
                                    PaddingTop="3px" />
                            </SubMenuItemStyle>
                            <Border BorderStyle="None"/>
                            <SubMenuItemImage Height="15px" Width="15px">
                            </SubMenuItemImage>
                            <Items>
                                <dx:MenuItem Text="Home" Image-Url="~/images/lucchetto.png" NavigateUrl="~/Index.aspx">
                                    <Image Url="~/images/lucchetto.png"></Image>
                                </dx:MenuItem>
                                <dx:MenuItem Text="Rinnova password" Image-Url="~/images/rinnova.png" NavigateUrl="~/Reserved/AreaPubblica/RinnovaPassNotLogged.aspx">
                                    <Image Url="~/images/rinnova.png"></Image>
                                </dx:MenuItem>
                                <dx:MenuItem Text="Recupera password" Image-Url="~/images/recupera.png" NavigateUrl="~/Reserved/AreaPubblica/PassDimenticata.aspx">
                                    <Image Url="~/images/recupera.png"></Image>
                                </dx:MenuItem>
                            </Items>
                            <ItemImage Height="15px" Width="15px">
                            </ItemImage>
                            <SubMenuStyle BackColor="White" GutterColor="White">
                               <Paddings PaddingBottom="3px" PaddingLeft="2px" PaddingTop="3px" />
                               <Border BorderStyle="Solid" BorderWidth="1px" />
                            </SubMenuStyle>
                       </dx:ASPxMenu>
    			    
			        </div>