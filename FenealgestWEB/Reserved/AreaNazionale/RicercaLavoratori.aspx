<%@ Page Language="C#"  MasterPageFile="~/FenealgestwebNew.Master" AutoEventWireup="true" CodeBehind="RicercaLavoratori.aspx.cs" Inherits="FenealgestWEB.Reserved.AreaNazionale.RicercaLavoratori" Title="Ricerca lavoratori" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.v13.2, Version=13.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipale" runat="server">
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"></dx:ASPxGridViewExporter>
    <script type="text/javascript">
        var colName;
        function OnItemClick(s, e) {
            if (e.item.name == 'HideColumn') {
                grid.PerformCallback(colName);
                colName = null;
            }
            else {
                if (grid.IsCustomizationWindowVisible())
                    grid.HideCustomizationWindow();
                else
                    grid.ShowCustomizationWindow();
            }
        }

        function OnContextMenu(s, e) {
            if (e.objectType == 'header') {
                colName = s.GetColumn(e.index).fieldName;
                headerMenu.GetItemByName('HideColumn').SetEnabled((colName == null ? false : true));
                headerMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
            }
        }
    </script>



    <div class="content"style="width:100%">
    <div class="login"style="width:100%" >
        <dx:ASPxHiddenField ID="ASPxHiddenField1" runat="server">
        </dx:ASPxHiddenField>
			<h2>Ricerca lavoratori</h2>
			<br/>
             <%= this.TipoRicerca %>            
            <br/>
            <asp:LinkButton ID="LinkButton4" runat="server" CssClass="accedi" 
                onclick="LinkButton4_Click" >
                    Ricerca per codice fiscale
                    <asp:Image ID="Image4" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>
            &nbsp  
            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="accedi" 
                    onclick="cmdAvanzata_Click">
                    Ricerca per dati anagrafici
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>  
            &nbsp   
            <asp:LinkButton ID="LinkButton3" runat="server" CssClass="accedi" 
                    onclick="Button1_Click">
                    Ricerca per azienda
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton> 

            <hr />
             <% if ( this.CodiceRicerca == 0 ) { %>
                <font class="largelabel">Codice fiscale</font>
                <asp:TextBox ID="txtCodFisc" runat="server" CssClass="largeinput"></asp:TextBox>
			    <br/> 
            <% } else if  ( this.CodiceRicerca == 1 ){ %> 
                <font class="largelabel">Anno</font> 
                <asp:DropDownList ID="cboAnno" runat="server"></asp:DropDownList> 
                </br>  
                <font class="largelabel">Settore</font> 
                <asp:DropDownList ID="cboSector" runat="server">
                    <asp:ListItem Selected="True" Value="ALL">(tutto)</asp:ListItem>
                    <asp:ListItem >EDILE</asp:ListItem>
                    <asp:ListItem>IMPIANTI FISSI</asp:ListItem>
                    <asp:ListItem>INPS</asp:ListItem>
                </asp:DropDownList>   
                </br>  
                <font class="largelabel">Territorio</font>
                <asp:DropDownList ID="cboTerr" runat="server" Font-Size="12px" Width="200px">
                </asp:DropDownList>
                </br>
                <font class="largelabel">Sesso</font> 
                <asp:DropDownList ID="cboSex" runat="server">
                    <asp:ListItem Selected="True" Value="ALL">(tutto)</asp:ListItem>
                    <asp:ListItem >UOMO</asp:ListItem>
                    <asp:ListItem>DONNA</asp:ListItem>
                </asp:DropDownList> 
                </br>     
                <font class="largelabel">Cognome</font>
                <asp:TextBox ID="txtCognome" runat="server" CssClass="largeinput"></asp:TextBox>
			    <br/> 
                <font class="largelabel">Nome</font>
                <asp:TextBox ID="txtNome" runat="server" CssClass="largeinput"></asp:TextBox>
			    <br/> 
                <div class="devcontrolcontainer">
                    <div class="labelcontroldiv" style="margin-right: 15px;">
                        <asp:CheckBox ID="chbNascita" runat="server" Text="Data di nascita"
                            oncheckedchanged="chbNascita_CheckedChanged" />&nbsp
                    </div>
                    
			        <div class="devcontroldiv">
			            <dx:ASPxDateEdit ID="bdpNascita1" runat="server" AllowNull="False">
                            <CalendarProperties TodayButtonText="Oggi">
                            </CalendarProperties>
                            </dx:ASPxDateEdit>
                        <dx:ASPxDateEdit ID="bdpNascita2" runat="server" AllowNull="False">
                            <CalendarProperties TodayButtonText="Oggi">
                            </CalendarProperties>
                            </dx:ASPxDateEdit>
			        </div>
                </div>
                <font class="largelabel">Nazionalità</font>
                <asp:DropDownList ID="cboNazionalita" Font-Size="12px" Width="200px" runat="server">
                </asp:DropDownList>
			    <br/> 
                <font class="largelabel">Prov. residenza</font>
                <asp:DropDownList ID="cboProvince" runat="server" Font-Size="12px" Width="200px"
                    onselectedindexchanged="cboProvince_SelectedIndexChanged" 
                    AutoPostBack="True">
                </asp:DropDownList>
			    <br/> 
                <font class="largelabel">Com. residenza</font>
                <asp:DropDownList ID="cboComune" runat="server" Font-Size="12px" Width="200px">
                </asp:DropDownList>
            <% } else { %>
                <font class="largelabel">Partita iva</font>
                <asp:TextBox ID="txtPartitaIva" runat="server" CssClass="largeinput"></asp:TextBox>
			    <br/> 
                <font class="largelabel">Azienda</font>
                <asp:TextBox ID="txtAzienda" runat="server" CssClass="largeinput"></asp:TextBox>
            <% } %>


            <br />
            <br />
       
            <br />
            <br />
            <br />
            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="accedi" 
                    onclick="cmdRicerca_Click">
                    Esegui ricerca 
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>
            &nbsp  
             <asp:LinkButton ID="LinkButton5" runat="server" CssClass="accedi" OnClick="LinkButton5_Click" 
                    >Scarica in excel </asp:LinkButton>


         <dx:ASPxPopupMenu ID="ASPxPopupMenu1" runat="server" ClientInstanceName="headerMenu">
               <Items>
                <dx:MenuItem Text="Hide column" Name="HideColumn">
                </dx:MenuItem>
                <dx:MenuItem Text="Show/hide hidden field list" Name="ShowHideList">
                </dx:MenuItem>
            </Items>
              <ClientSideEvents ItemClick="OnItemClick"/>
         </dx:ASPxPopupMenu>

            <dx:ASPxGridView style="width:99%" oncustomcallback="gvData_CustomCallback" ClientInstanceName="grid" ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" EnableTheming="True" Theme="Aqua" SettingsBehavior-ColumnResizeMode="Control" Settings-ShowFilterBar="Auto" Settings-ShowFooter="True" SettingsPager-PageSize="15" Settings-HorizontalScrollBarMode="Auto">
                <TotalSummary>
                    <dx:ASPxSummaryItem FieldName="Id" SummaryType="Count" ShowInColumn="Id"/>
                </TotalSummary>
                <Columns>
                    <dx:GridViewDataTextColumn Caption="Cognome" FieldName="Surname" VisibleIndex="1" MinWidth="300">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Nome" FieldName="Name" VisibleIndex="2">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Codice fiscale" FieldName="Fiscalcode" VisibleIndex="3">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataHyperLinkColumn FieldName="Id" Caption="Id" VisibleIndex="0">
                        <PropertiesHyperLinkEdit NavigateUrlFormatString="~/Reserved/AreaNazionale/VisualizzaLavoratore.aspx?id={0}&isnat=true" />
                    </dx:GridViewDataHyperLinkColumn>
                    <dx:GridViewDataTextColumn FieldName="BirthDate" Caption="Data nascita" VisibleIndex="4">
                        <PropertiesTextEdit DisplayFormatString="d"></PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Nationality" Caption="Nazionalit&#224;" VisibleIndex="5" MinWidth="200">
                        <Settings AllowAutoFilter="True" HeaderFilterMode="CheckedList"></Settings>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="BirthPlace" Caption="Luogo di nascita" VisibleIndex="6" MinWidth="200">
                        <Settings HeaderFilterMode="CheckedList"></Settings>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="LivingPlace" Caption="Residenza" VisibleIndex="7" MinWidth="200">
                        <Settings HeaderFilterMode="CheckedList"></Settings>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Address" Caption="Indirizzo" VisibleIndex="8" MinWidth="300"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Cap" Caption="Cap" VisibleIndex="9"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Phone" Caption="Telefono" VisibleIndex="10"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="LastModifier" Caption="Ultima sede" VisibleIndex="11">
                        <Settings HeaderFilterMode="CheckedList"></Settings>
                    </dx:GridViewDataTextColumn>
                </Columns>
                <SettingsBehavior EnableCustomizationWindow="True" />
                <ClientSideEvents ContextMenu="OnContextMenu" />
                <Settings ShowFilterRow="True" ShowGroupPanel="True"  ShowFilterBar="Auto" ShowFilterRowMenu="True" ShowFooter="True" ShowGroupFooter="VisibleIfExpanded" ShowHeaderFilterButton="True"  />
                <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />

            </dx:ASPxGridView>


            <br />


           

            <div class="message_box">
                <asp:Label ID="lblTotalResults" runat="server" Text="Elementi trovati..."></asp:Label>
            </div>

    </div>

</div>

</asp:Content>

