<%@ Page Title="Visualizza rendiconto" Language="C#"  MasterPageFile="~/FenealgestwebNew.Master" AutoEventWireup="true" CodeBehind="Bilancio.aspx.cs" Inherits="FenealgestWEB.Reserved.AreaNazionale.Bilancio" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v18.1.Export, Version=18.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v18.1, Version=18.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
 

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipale" runat="server">
   
    <dx:ASPxTreeListExporter ID="treeListExporter1" runat="server">
    </dx:ASPxTreeListExporter>  
   
<div class="content">
    <div class="login">
				<h2>Visualizza rendiconto</h2>
				<br/>
				<br/>
                <font class="largelabel">Regione</font> 
                <asp:DropDownList ID="cboRegioni" runat="server" Font-Size="12px" Width="200px"
                            onselectedindexchanged="cboRegioni_SelectedIndexChanged" 
                            AutoPostBack="True">
                </asp:DropDownList>
				<br/>
                <font class="largelabel">Provincia</font> 
                <asp:DropDownList ID="cboProvince" runat="server" AutoPostBack="True" Font-Size="12px" Width="200px"
                     onselectedindexchanged="cboProvince_SelectedIndexChanged">
                </asp:DropDownList>
				<br/>       
                <font class="largelabel">Anno</font> 
                <asp:DropDownList ID="cboAnno" runat="server" AutoPostBack="True" Font-Size="12px" Width="200px"
                    onselectedindexchanged="cboAnno_SelectedIndexChanged">
                </asp:DropDownList>
				<br/> 
				<font class="largelabel">Tipo rendinconto</font> 
                <asp:DropDownList ID="cboTipo" runat="server" Font-Size="12px" Width="200px" onselectedindexchanged="cboTipo_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem>FENEAL</asp:ListItem>
                    <asp:ListItem>RLST</asp:ListItem>
                    
                </asp:DropDownList>  
                <br/> 
				<hr />      
				<br />
				<font class="largelabel">Salva come:</font> 
                <asp:DropDownList ID="DropDownList4" runat="server" Font-Size="12px" Width="200px">
                    <Items>
                        <asp:ListItem Value="0">Pdf</asp:ListItem>
                        <asp:ListItem Value="1">Excel '03</asp:ListItem>
                        <asp:ListItem Value="2">Excel '07</asp:ListItem>
                        <asp:ListItem Value="3">Rtf</asp:ListItem> 
                    </Items>
                   <asp:Items>
					<asp:ListItem Text="Pdf" Value="0"/>
                    <asp:ListItem Text="Excel '03" Value="1"/>
                    <asp:ListItem Text="Excel '07" Value="2"/>
                    <asp:ListItem Text="Rtf" Value="3"/>
                  </asp:Items></asp:DropDownList>&nbsp;&nbsp; <asp:LinkButton ID="LinkButton1" runat="server" CssClass="accedi" 
                    onclick="buttonSaveAs_Click">
                    Scarica <asp:Image ID="Image3" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton><br />


                <div class="message_box">
                         <%=this.IntestazioneBilancio %> <br/>
                    <br/>                    
                </div>
            
            <hr />
            <br/>   

        
            <% if (this.Found) { %> <asp:LinkButton ID="LinkButton2" runat="server" CssClass="accedi" 
                    onclick="btnExpand_Click">
                    Espandi <asp:Image ID="Image1" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>&nbsp; <asp:LinkButton ID="LinkButton3" runat="server" CssClass="accedi" 
                    onclick="btnCollapse_Click">
                    Comprimi <asp:Image ID="Image2" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton><br />
     
            <div style="width:100%; overflow:auto">
                <dx:ASPxTreeList ID="ASPxTreeList2" runat="server" 
                        AutoGenerateColumns="False" DataSourceID="SqlDataSource1" 
                        KeyboardSupport="True" KeyFieldName="IdNodo" 
                        onhtmlrowprepared="ASPxTreeList2_HtmlRowPrepared" 
                        ParentFieldName="IdParent" Width="100%"><Styles><Node Wrap="True"></Node></Styles><Border 
                            BorderStyle="Solid" /><Columns><dx:TreeListTextColumn 
                            FieldName="IdNodo" Visible="False" VisibleIndex="3"></dx:TreeListTextColumn><dx:TreeListTextColumn 
                            FieldName="IdParent" Visible="False" VisibleIndex="4"></dx:TreeListTextColumn><dx:TreeListTextColumn 
                            FieldName="DescrizioneNodo" VisibleIndex="0" Width="200px"><CellStyle 
                                Wrap="True"></CellStyle></dx:TreeListTextColumn><dx:TreeListTextColumn 
                            FieldName="ImportoBilancio" VisibleIndex="1"></dx:TreeListTextColumn><dx:TreeListTextColumn 
                            FieldName="ImportoPreventivo" VisibleIndex="2"></dx:TreeListTextColumn><dx:TreeListTextColumn 
                            FieldName="IsLeaf" Visible="False" VisibleIndex="7"></dx:TreeListTextColumn></Columns>
                 </dx:ASPxTreeList>
            </div>
            <hr />
            
            
            <div class="message_box" style="overflow:hidden">
            
                <div id="conto" style="width:45%;display:inline-table; float:left;">
                    <strong>Conto economico</strong> <br />
                    <%=this.Conto %> </div><div id="quadratura" style="width:45%;display:inline-table; float:left">
                    <strong>Stato patrimoniale</strong> <br />
                    <%=this.quadratura %> </div></div><br />
            
         <div class="message_box" style="position:relative;">
                    <table  class="generic_table">
                        <tr>
                            <td>
                                <h4>
                                    INVENTARIO PATRIMONIALE </h4>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <strong>Beni immobili</strong> 
                            </td>
                         </tr>
                         <tr>
                            <td>
                                <asp:Literal ID="litImmobili" runat="server"></asp:Literal>
                            </td>
                         </tr>
                         <tr>
                            <td>
                                <strong>Depositi</strong> 
                            </td>
                         </tr>
                         <tr>
                            <td>
                                 <asp:Literal ID="litDepositi" runat="server"></asp:Literal>
                            </td>
                         </tr>
                         <tr>
                            <td>
                                <strong>Polizze</strong>
                             </td>
                         </tr>
                         <tr>
                            <td>
                                 <asp:Literal ID="litPolizze" runat="server"></asp:Literal>
                            </td>
                          </tr>
                           <tr>
                               <td>
                                <strong>Auto</strong> 
                               </td>
                           </tr>
                           <tr>
                                <td>
                                     <asp:Literal ID="litAuto" runat="server"></asp:Literal>
                                </td>
                           </tr>
                           <tr>
                               <td>
                                <strong>Beni mobili</strong> 
                               </td>
                           </tr>
                           <tr>
                                <td>
                                     <asp:Literal ID="litMobili" runat="server"></asp:Literal>
                                </td>
                           </tr>
                           <tr>
                               <td>
                                    <strong>Accantonamenti TFR</strong> 
                               </td>
                           </tr>
                           <tr>
                                <td>
                                     <asp:Literal ID="litAccantonamenti" runat="server"></asp:Literal>
                                </td>
                           </tr>
                           <tr>
                               <td>
                                    <strong>Avanzo/Disavanzo</strong> 
                               </td>
                           </tr>
                           <tr>
                                <td>
                                     <asp:Literal ID="litChiusure" runat="server"></asp:Literal>
                                </td>
                           </tr>
                    </table>
         </div><%} %> 
    </div><div class="column">
			    <h3>Dati di login</h3><%=this.HtmlLoginDataList("../../images/info.png")%> <h3>Help on-line</h3><ul>
				    <li>
					    <h4>Presentazione contenuti pagina</h4><font class="data">  Pagina visualizzazione rendiconto</font> La pagina corrente permette di ricercare e visualizzare un rendiconto.  La pagina mostra tre campi in cui inserire; regione, provincia, anno e il tipo di formato del file da scaricare. E' possibile settare solo regione e anno qualora si voglia un bilancio regionale, oppure regione, provincia e anno qualora si voglia un bilancio provinciale.
                                                                                                                 Premendo su 'Scarica' verra' avviata la ricerca del rendiconto presente all'interno del sistema e quindi il rendiconto verra' salvato sul proprio pc.<br />
                                                                                                                 Il risultato della ricerca e' mostrato attraverso una prima zona riepilogativa di anno, regione ed eventualmente provincia, piu' una seconda zona dove mediante una vista ad albero sono mostrate le descrizioni delle voci di rendiconto con i relativi importi. Mediante i segni '+' e '-' posti a sinistra delle voci e' possibile espandere o implodere le stesse nelle sottovoci che le compongono.<br />
                                                                                                                 Premendo su 'Espandi' tutte le voci di rendiconto saranno espanse nelle loro sottovoci.<br />
                                                                                                                 Premendo su  'Comprimi' tutte le voci di rendiconto saranno implose nelle quattro voci iniziali: Situazione finanziaria iniziale, Situazione finanziaria finale, Spese, Entrate.<br />
                                                                                                                 E' inoltre possibile effettuare un download della vista ad albero visualizzata. Scegliere un formato tra: Pdf, Excel '03, Excel '07, Rtf e premendo su 'Scarica' verra' automaticamente avviato il download.<br />
                                                                                                                 A fondo pagina e' visualizzato uno specchietto riepilogativo del rendiconto stesso.
 </li></ul></div></div><asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:dbFeneal %>" 
        ProviderName="<%$ ConnectionStrings:dbFeneal.ProviderName %>" 
    
        SelectCommand="SELECT IdNodo, IdParent, DescrizioneNodo, ImportoBilancio, ImportoPreventivo, IsLeaf FROM viewrendiconto WHERE (IdNodo not like 'Bilancio') AND (viewrendiconto.Anno = ?) AND (viewrendiconto.NomeRegione = ?) AND (viewrendiconto.NomeProvincia = ?) AND (viewrendiconto.ContoRLST = ?)">
        <SelectParameters>
        <asp:ControlParameter ControlID="cboAnno" DefaultValue="Text" Name="?" 
            PropertyName="Text" />
        <asp:ControlParameter ControlID="cboRegioni" DefaultValue="Text" 
            Name="?" PropertyName="Text" />
        <asp:ControlParameter ControlID="cboProvince" DefaultValue="Text" 
            Name="?" PropertyName="Text" />
        <asp:ControlParameter ControlID="cboTipo" DefaultValue="Text" 
            Name="?" PropertyName="Text" />
        </SelectParameters>
</asp:SqlDataSource>
</asp:Content>