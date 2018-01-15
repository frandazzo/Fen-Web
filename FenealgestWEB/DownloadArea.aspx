<%@ Page Title="Pagina di download applicativi" Language="C#" MasterPageFile="~/FenealgestwebNew.Master" AutoEventWireup="true" CodeBehind="DownloadArea.aspx.cs" Inherits="FenealgestWEB.DownloadArea" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipale" runat="server">
<div class="content">
    <div class="login">
		<h2>Download area</h2>
		<hr />
        <br />		
        <h4 style="color:#FF9900">Area di download programma rendiconto economico</h4>
        <table cellpadding="0" class="generic_table">
            <tr>
                <td class="col_1_h">Software</td>
                <td class="col_2_h">Descrizione</td>
                <td class="col_3_h">Dim.</td>
                <td class="col_4_h">Scarica</td>
            </tr>
            <tr>
                <td class="col_1">Rendiconto Feneal</td>
                <td class="col_2">Software per la gestione amministrativa della Feneal provinciale e regionale</td>
                <td class="col_3">31 Mb</td>
                <td class="col_4"><a href="assets/setups/RendicontoFeneal.exe"><img src="images/download.png" /></a></td>
            </tr>            
            <tr>
                <td class="col_1">Rendiconto RLST</td>
                <td class="col_2">Software per la gestione amministrativa degli RLST</td>
                <td class="col_3">31 Mb</td>
                <td class="col_4"><a href="assets/setups/RendicontoRlst.exe"><img src="images/download.png" /></a></td>
            </tr> 
            <tr>
                <td class="col_1">Guida d'installazione e manuale utente</td>
                <td class="col_2">Guida per l'installazione e l'utilizzo dei programmi di gestione amministrativa</td>
                <td class="col_3">3 Mb</td>
                <td class="col_4"><a href="assets/docs/guida_installazione_rendiconto.pdf"><img src="images/download.png" /></a></td>
            </tr> 
            <tr>
                <td class="col_1">Linee guida di politica amministrativa</td>
                <td class="col_2">Il rendiconto di esercizio: linee guida di politica amministrativa stilate dalla Feneal Nazionale</td>
                <td class="col_3">3 Mb</td>
                <td class="col_4"><a href="assets/docs/linee guida.pdf"><img src="images/download.png" /></a></td>
            </tr> 
        </table>
        
        <hr />
        <br />        
        <h4 style="color:#FF9900">Area di download programma Fenealgest</h4>
        <table cellpadding="0" class="generic_table">
            <tr>
                <td class="col_1_h">Software</td>
                <td class="col_2_h">Descrizione</td>
                <td class="col_3_h">Dim.</td>
                <td class="col_4_h">Scarica</td>
            </tr>
            <tr>
                <td class="col_1">Gestionale Fenealgest (versione 5.0)</td>
                <td class="col_2">Software per la gestione della Feneal provinciale </td>
                <td class="col_3">150 Mb</td>
                <td class="col_4"><a href="assets/setups/FenealGest.exe"><img src="images/download.png" /></a></td>
            </tr>  
            <tr>
                <td class="col_1">Connettore Fenealgest Nazionale (versione 3.0)</td>
                <td class="col_2">Software per l'inoltro e la consulatazione con il database nazionale dei dati dei lavoratori iscritti alla Feneal </td>
                <td class="col_3">18 Mb</td>
                <td class="col_4"><a href="assets/setups/ConnettoreFenealgestNazionale.exe"><img src="images/download.png" /></a></td>
            </tr>  
            <tr>
                <td class="col_1">Utilità installazione Fenealgest </td>
                <td class="col_2">Programmi vari per l'installazione Fenenalgest  </td>
                <td class="col_3">174 Mb</td>
                <td class="col_4"><a href="assets/setups/SqlServer2005 Express.zip"><img src="images/download.png" /></a></td>
            </tr> 
            <tr>
                <td class="col_1">Utilità per calcolo RSU </td>
                <td class="col_2">Programma per il calcolo delle RSU nelle aziende  </td>
                <td class="col_3">8 Mb</td>
                <td class="col_4"><a href="assets/setups/ElezioneRSUSetup.exe"><img src="images/download.png" /></a></td>
            </tr> 
            <tr>
                <td class="col_1">Servizio di import export automatico Fenealgest </td>
                <td class="col_2">Programma per l'import export automatico delle quote associative e dei file di lavoratori liberi </td>
                <td class="col_3">1 Mb</td>
                <td class="col_4"><a href="assets/setups/FenealgestImportExportServiceSetup.msi"><img src="images/download.png" /></a></td>
            </tr>
             <tr>
                <td class="col_1">Utilità di corredo al servizio di import export </td>
                <td class="col_2">File di utilità per l'installazione dei servizi di import export </td>
                <td class="col_3">80 Mb</td>
                <td class="col_4"><a href="assets/setups/ImportExportUtilities.zip"><img src="images/download.png" /></a></td>
            </tr>                            
        </table>
        
        <hr />
        <br />
        <h4 style="color:#FF9900">Area di download assistenza ed help on line</h4>
        <table cellpadding="0" class="generic_table">
            <tr>
                <td class="col_1_h">Software</td>
                <td class="col_2_h">Descrizione</td>
                <td class="col_3_h">Dim.</td>
                <td class="col_4_h">Scarica</td>
            </tr>
            <tr>
                <td class="col_1">Manuale operativo Fenealgest WEB</td>
                <td class="col_2">Manuale d'utilizzo per il prtale Fenealgest WEB. </td>
                <td class="col_3">3 Mb</td>
                <td class="col_4"><a href="assets/docs/ManualeFenealgestWEB.doc"><img src="images/download.png" /></a></td>
            </tr> 
            <tr>
                <td class="col_1">Programma teleassistenza</td>
                <td class="col_2">Programma per ricevere supporto con la teleassistenza. </td>
                <td class="col_3">3 Mb</td>
                <td class="col_4"><a href="assets/setups/Teleassistenza.exe"><img src="images/download.png" /></a></td>
            </tr>            
        </table>

        <%--Per scaricare il programma <strong></strong> clicca <a href="assets/setups/RendicontoFeneal.zip" target="_blank"><strong>qui</strong></a>.
    
        Per scaricare il programma <strong>Rendiconto RLST</strong> clicca <a href="assets/setups/RendicontoRlst.zip" target="_blank"><strong>qui</strong></a>.
 
        Per scaricare la <strong>guida di installazione</strong> dei programmi per il rendiconto clicca <a href="assets/docs/guida_installazione_rendiconto.doc" target="_blank"><strong>qui</strong></a>.        
--%>    </div>
    	<div class="column">
			    <h3>Dati di login</h3>
			        <%=this.HtmlLoginDataList("images/info.png") %>	    
			    <h3>Help on-line</h3>
			    <ul>
				      <li>
					      <h4>Presentazione contenuti pagina</h4>	
					      <font class="data">Area di download</font>						
					      La pagina corrente permette di scaricare file dal portale. <br />
                I file suddivisi per aree e indicanti il nome, la descrizione e la dimensione sono scaricabili premendo sulla relativa icona posta a desta.
				      </li> 
				    													
			    </ul>											
		  </div>	
    
    
</div>


</asp:Content>
