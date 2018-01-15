<%@ Page Title="" Language="C#" MasterPageFile="~/FenealgestwebNew.Master" AutoEventWireup="true" CodeBehind="ReportFlussi.aspx.cs" Inherits="FenealgestWEB.Reserved.AreaNazionale.ReportFlussi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipale" runat="server">

   <div class="content">

   
    <div class="login">
				<h2>Report sui flussi dei lavoratori</h2>
				<br/>
				<br/>
				
			<font class="largelabel">Anno</font>
            <asp:DropDownList ID="cboAnno" runat="server" Font-Size="12px" Width="200px">
            </asp:DropDownList>
            <br / />
            <div class="spacer">
            </div>

            <font class="largelabel">Regione</font>
            <asp:DropDownList ID="cboRegioni" runat="server" AutoPostBack="True"  Font-Size="12px" Width="200px"
                onselectedindexchanged="cboRegioni_SelectedIndexChanged">
            </asp:DropDownList>
            <div class="spacer">
            </div>

            <font class="largelabel">Provincia</font>
            <asp:DropDownList ID="cboProvince" runat="server"  Font-Size="12px" Width="200px"
                AutoPostBack="False">
            </asp:DropDownList>

             <div class="spacer">
            </div>
            
          <font class="largelabel">Anni di valutazione flussi</font>
            <asp:DropDownList ID="cboAnniValutazione" runat="server" Font-Size="12px" Width="200px">
                <asp:ListItem Selected="True" Value="-1">Tutti gli anni successivi</asp:ListItem>
                <asp:ListItem Value="0">Anno corrente</asp:ListItem>
                <asp:ListItem Value="1">Fino ad un anno dopo</asp:ListItem>
                <asp:ListItem Value="2">Fino a due anni dopo</asp:ListItem>
                <asp:ListItem Value="3">Fino a tre anni dopo</asp:ListItem>
            </asp:DropDownList>
            <br / />
            <div class="spacer">
            </div>
           
            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="accedi" OnClick="LinkButton1_Click" 
                   >
                  Esegui verifica <asp:Image ID="Image3" runat="server" ImageUrl="~/images/right_arrow.png" /> </asp:LinkButton>
			<br />
			
			<hr />

            <asp:Label ID="Label1" runat="server" Text="Label">Nessun elemento trovato!!!</asp:Label>

            <asp:GridView ID="GridView1" runat="server" CssClass="generic_table">
            </asp:GridView>

           

            <a href="javascript:;" id="print">Stampa</a>
            <script type="text/javascript" >
                 $('#print').click(function () {

                     $('.login').jqprint();
                 });
             </script>

    </div>


    <div class="column">
			    <h3>Dati di login</h3>
			        <%=this.HtmlLoginDataList("../../images/info.png")%>	    
			    <h3>Help on-line</h3>
			    <ul>
				    <li>
					    <h4>Presentazione contenuti pagina</h4>	
					    <font class="data">Pagina analisi iscritti Feneal</font>						
					    
				    </li> 
				    													
			    </ul>											
	</div>  
  </div>

</asp:Content>
