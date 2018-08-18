<%@ Page Title="Analisi iscritti" Language="C#" MasterPageFile="~/FenealgestwebNew.Master" AutoEventWireup="true" CodeBehind="ReportIscritti.aspx.cs" Inherits="FenealgestWEB.Reserved.AreaNazionale.ReportIscritti" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v18.1, Version=18.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipale" runat="server">
    <div class="content">
    <div class="login">
				<h2>Analisi iscritti</h2>
				<br/>
				<br/>
				
			<font class="largelabel">Anno</font>
            <asp:DropDownList ID="cboAnno"  runat="server" AutoPostBack="True" Font-Size="12px" Width="200px">
            </asp:DropDownList>
            <br/>
            <font class="largelabel">Regione</font>
            <asp:DropDownList ID="cboRegioni" runat="server" AutoPostBack="True"  Font-Size="12px" Width="200px"
                onselectedindexchanged="cboRegioni_SelectedIndexChanged">
            </asp:DropDownList>
            <br/>
            <font class="largelabel">Provincia</font>
            <asp:DropDownList ID="cboProvince" runat="server"  Font-Size="12px" Width="200px"
                AutoPostBack="True">
            </asp:DropDownList>
            <br/>
            
            <asp:CheckBox ID="chkRedundance" runat="server" AutoPostBack="true" Text="Iscritti presi una sola volta" OnCheckedChanged="chkRedundance_CheckedChanged" />



            <hr />      
			<br />
            
            <font class="largelabel">Salva come: </font>

            <asp:DropDownList ID="DropDownList4" runat="server"  Font-Size="12px" Width="200px" >
                <Items>
                    <asp:ListItem Value="0">Pdf</asp:ListItem>
                    <asp:ListItem Value="1">Excel</asp:ListItem>
                    <asp:ListItem Value="2">Mht</asp:ListItem>
                    <asp:ListItem Value="3">Rtf</asp:ListItem>
                    <asp:ListItem Value="4">Text</asp:ListItem>
                    <asp:ListItem Value="5">Html</asp:ListItem>
                </Items>
                <asp:Items>
                    <asp:ListItem Text="Pdf" Value="0"/>
                    <asp:ListItem Text="Excel" Value="1"/>
                    <asp:ListItem Text="Mht" Value="2"/>
                    <asp:ListItem Text="Rtf" Value="3"/>
                    <asp:ListItem Text="Text" Value="4"/>
                    <asp:ListItem Text="Html" Value="5"/>
                </asp:Items></asp:DropDownList>
                
                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="accedi" 
                    onclick="buttonSaveAs_Click">
                  Scarica <asp:Image ID="Image3" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>
			    <br />
                
               <hr />
               


<div style="width:100%; overflow:auto">

            <asp:LinkButton ID="LinkButton2" runat="server" Font-Bold="True" OnClick="LinkButton2_Click">Scarica report invii al database nazionale</asp:LinkButton>

            <dx:ASPxPivotGrid ID="ASPxPivotGrid1" runat="server" 
                    DataSourceID="SqlDataSource1" Caption="Report">
                <Fields>
                    <dx:PivotGridField ID="fieldIdLavoratore" Area="DataArea" AreaIndex="0" 
                        FieldName="Id_Lavoratore" SummaryType="Count" Caption="Num Lavoratori">
                    </dx:PivotGridField>
                    <dx:PivotGridField ID="fieldNomeProvinciaNascita" AreaIndex="0" 
                        FieldName="NomeProvinciaNascita" Visible="False">
                    </dx:PivotGridField>
                    <dx:PivotGridField ID="fieldNomeProvincia" AreaIndex="1" 
                        FieldName="NomeProvincia" Area="RowArea">
                    </dx:PivotGridField>
                    <dx:PivotGridField ID="fieldSettore" AreaIndex="0" 
                        FieldName="Settore">
                    </dx:PivotGridField>
                    <dx:PivotGridField ID="fieldNomeRegione" AreaIndex="0" 
                        FieldName="NomeRegione" Area="RowArea">
                    </dx:PivotGridField>
                    <dx:PivotGridField ID="fieldNomeNazioneNascita" AreaIndex="1" 
                        FieldName="NomeNazioneNascita">
                    </dx:PivotGridField>
                    <dx:PivotGridField ID="fieldEnte" AreaIndex="2" 
                        FieldName="Ente">
                    </dx:PivotGridField>
                    <dx:PivotGridField ID="fieldFasciaet" Area="ColumnArea" AreaIndex="0" 
                        FieldName="Fascia_età">
                    </dx:PivotGridField>
                </Fields>
                <OptionsView RowTotalsLocation="Tree" ShowTotalsForSingleValues="True" />
            </dx:ASPxPivotGrid>
 </div>
 
      
   <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:dbFeneal %>" 
                ProviderName="<%$ ConnectionStrings:dbFeneal.ProviderName %>" 

                SelectCommand="SELECT Id_Lavoratore, NomeProvinciaNascita, NomeProvincia, Settore, NomeRegione, NomeNazioneNascita, Ente, funDescEta(AnnoNascita, ?) AS Fascia_età FROM iscrizioni WHERE (Anno = ?) AND (NomeRegione = ? OR ? = '#' ) AND (NomeProvincia = ? OR ? = '#' ) GROUP BY Id_Lavoratore, Settore, NomeProvincia">
                <SelectParameters>
                    <asp:ControlParameter ControlID="cboAnno" DefaultValue="SelectedValue" Name="?" 
                        PropertyName="SelectedValue" />
                    <asp:ControlParameter ControlID="cboAnno" DefaultValue="SelectedValue" Name="?" 
                        PropertyName="SelectedValue" />
                    <asp:ControlParameter ControlID="cboRegioni" DefaultValue="#" Name="?" 
                        PropertyName="Text" />
                    <asp:ControlParameter ControlID="cboRegioni" DefaultValue="#" Name="?" 
                        PropertyName="Text" />
                    <asp:ControlParameter ControlID="cboProvince" DefaultValue="#" Name="?" 
                        PropertyName="Text" />
                    <asp:ControlParameter ControlID="cboProvince" DefaultValue="#" Name="?" 
                        PropertyName="Text" />
                </SelectParameters>
            </asp:SqlDataSource>





            
            <dx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" 
                ASPxPivotGridID="ASPxPivotGrid1">
            </dx:ASPxPivotGridExporter>

    </div>
    <div class="column">
			    <h3>Dati di login</h3>
			        <%=this.HtmlLoginDataList("../../images/info.png")%>	    
			    <h3>Help on-line</h3>
			    <ul>
				    <li>
					    <h4>Presentazione contenuti pagina</h4>	
					    <font class="data">Pagina analisi iscritti Feneal</font>						
					    La pagina corrente permette di visualizzare un report per gli iscritti. <br />
              La pagina mostra tre campi in cui inserire: anno, regione e provincia. <br />
              A seconda se l’utente è un amministratore nazionale, un segretario regionale o un segretario provinciale potrà scegliere tutte le regioni e province, oppure solo la sua regione con le province appartenenti negli ultimi due casi. <br />
              Ad ogni variazione dei due campi, verrà aggiornata la tabella sottostante. Tale tabella è così costituita:<br /> 
              sulle righe riporta i nomi delle regioni, e al loro interno i nomi delle province<br />
              sulle colonne riporta le fasce di età dei lavoratori<br />
              negli incroci riporta il numero di lavoratori per la provincia e la fascia corrispondente, oppure la sommatoria del numero di lavoratori per province afferenti ad una stessa regione<br />
              l’ultima colonna riporta il totale delle colonne precedenti per la regione o la provincia corrispondente<br />
              l’ultima riga riporta il totale delle righe precedenti per la fascia corrispondente<br />
              Mediante i segni “+” e “-“ posti a sinistra delle regioni è possibile espandere o implodere le stesse nelle province che le compongono. <br /> 
              Grazie alle tre voci in alto, “Settore”, “NomeNazioneNascita” ed “Ente”, è possibile aggiungere ulteriori righe o colonne all’interno della tabella. Per fare questo trascinare la voce desiderata di fianco ad una voce già presente nelle riga o colonna.
				    </li> 
				    													
			    </ul>											
		  </div>    
</div>

    </asp:Content>
