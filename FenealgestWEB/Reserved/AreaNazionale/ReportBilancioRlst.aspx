<%@ Page Title="Reportistica rendiconti RLST provinciali" Language="C#" MasterPageFile="~/FenealgestwebNew.Master" AutoEventWireup="true" CodeBehind="ReportBilancioRlst.aspx.cs" Inherits="FenealgestWEB.Reserved.AreaNazionale.ReportBilancioRlst" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v18.1, Version=18.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipale" runat="server">
<div class="content">
    <div class="login">
				<h2>Reportistica rendiconti RLST provinciali</h2>
				<br/>
				<br/>
        <font class="largelabel">Tipologia conti</font>

       <asp:DropDownList ID="cboConto" runat="server" AutoPostBack="True" Font-Size="12px" Width="200px">
            
            <asp:ListItem Value="A">ATTIVITA'</asp:ListItem>
            <asp:ListItem Value="A.D.%">- Disponibilita'</asp:ListItem>

            <asp:ListItem Value="P">PASSIVITA'</asp:ListItem>
            <asp:ListItem Value="P.P.%">- PATRIMONIO SOCIALE</asp:ListItem>


            <asp:ListItem Value="S">SPESE</asp:ListItem>
            <asp:ListItem Value="S.1.%">- Spese per attività</asp:ListItem>
            <asp:ListItem Value="S.2.%">- Spese del personale</asp:ListItem>
            <asp:ListItem Value="S.3.%">- spese generali</asp:ListItem>

            <asp:ListItem Value="E">ENTRATE</asp:ListItem>
            <asp:ListItem Value="E.1">- Contributro attività RLST</asp:ListItem>
            <asp:ListItem Value="E.2">- Altre entrate</asp:ListItem>

        </asp:DropDownList>
        <br />
        <font class="largelabel">Anno</font>

        <asp:DropDownList ID="cboAnno"  runat="server" AutoPostBack="True" Font-Size="12px" Width="200px">
        </asp:DropDownList>
        <br />
	    <hr />      
	    <br />
        <font class="largelabel">Salva come:</font>
        <asp:DropDownList ID="DropDownList4" runat="server" Font-Size="12px" Width="200px" >
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
                
            </asp:Items></asp:DropDownList>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="accedi" 
                onclick="buttonSaveAs_Click">
                Scarica: <asp:Image ID="Image3" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton><br />
        <hr />
        
        <div style="width:100%; overflow:auto">
            <dx:ASPxPivotGrid ID="ASPxPivotGrid1" runat="server"
                DataSourceID="SqlDataSource1">
             <Fields>
                    <dx:PivotGridField ID="fieldNomeRegione" Area="RowArea" AreaIndex="0" 
                        FieldName="NomeRegione">
                    </dx:PivotGridField>
                    <dx:PivotGridField ID="fieldNomeProvincia" Area="RowArea" AreaIndex="1" 
                        FieldName="NomeProvincia">
                    </dx:PivotGridField>
                    <dx:PivotGridField ID="fieldAnno" AreaIndex="0" FieldName="Anno">
                    </dx:PivotGridField>
                    <dx:PivotGridField ID="fieldDescrizioneNodo" Area="ColumnArea" AreaIndex="0" 
                        FieldName="DescrizioneNodo">
                    </dx:PivotGridField>
                    <dx:PivotGridField ID="fieldImportoBilancio" Area="DataArea" AreaIndex="0" 
                        FieldName="ImportoBilancio">
                    </dx:PivotGridField>
                </Fields>
                <OptionsView RowTotalsLocation="Tree" ShowTotalsForSingleValues="True" />
            </dx:ASPxPivotGrid>
 </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:dbFeneal %>" 
            ProviderName="<%$ ConnectionStrings:dbFeneal.ProviderName %>" 
            
            SelectCommand="SELECT NomeRegione, NomeProvincia, Anno, DescrizioneNodo, ImportoBilancio FROM viewrendiconto WHERE (IdNodo LIKE ?) AND (Anno = ?) AND (NomeProvincia not like '') AND (ContoRLST = 'RLST')">
            <SelectParameters>
                <asp:ControlParameter ControlID="cboConto" DefaultValue="SelectedIndex" 
                    Name="?" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="cboAnno" DefaultValue="SelectedValue" Name="?" 
                    PropertyName="SelectedValue" />
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
					      <font class="data">Pagina report rendiconti provinciali</font>	
					    					  La pagina corrente permette di visualizzare un report per i rendiconti RLST a livello provinciale.
					                La pagina a cui si viene reindirizzati mostra due campi in cui inserire: la tipologia del conto e l’anno.<br />
                          Ad ogni variazione dei due campi, verrà aggiornata la tabella sottostante. Tale tabella è così costituita:<br /> 
                          sulle righe riporta i nomi delle regioni, e al loro interno i nomi delle province che hanno esportato il bilancio per l’anno in questione<br />
                          sulle colonne riporta la voce o le sottovoci del conto prescelto<br />
                          negli incroci riporta l’importo in bilancio per la provincia e la voce corrispondente, oppure la sommatoria degli importi per province afferenti ad una stessa regione.<br />
                          l’ultima colonna riporta il totale delle colonne precedenti per la regione o la provincia corrispondente.<br />
                          l’ultima riga riporta il totale delle righe precedenti per la voce corrispondente.<br />
                          Mediante i segni “+” e “-“ posti a sinistra delle regioni è possibile espandere o implodere le stesse nelle province che le compongono.  

				      </li> 
				    													
			    </ul>											
		  </div>    
      </div>
</asp:Content>

