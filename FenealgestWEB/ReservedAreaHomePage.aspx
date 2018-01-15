<%@ Page Language="C#" MasterPageFile="~/FenealgestwebNew.Master" AutoEventWireup="true" CodeBehind="ReservedAreaHomePage.aspx.cs" Inherits="FenealgestWEB.ReservedAreaHomePage" Title="Area riservata Fenealgest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipale" runat="server">

<div class="content">
         <div class="login">
				<h2>Benvenuto <%=FenealgestWEB.Utility.SessionServiceManager.SecurityService.CurrentUser.CompleteName.ToUpper()  %></h2>
				<br/>
				<br/>	
			    <div class="message_box">
		            Benvenuto nell&#39;area riservata del portale Fenealgest.
		            <br />
		            Seleziona le funzionalità che desideri nei menu&#39; a sinistra o dalle immagini in basso.
                </div>
                
                <div class="tasti">
                    <div class="box">
                        <a href="DownloadArea.aspx">
                            <img src="images/download.png" class="utility" />
                        </a>
                        <a href="DownloadArea.aspx">Accedi all'area di download del portale Fenealgest</a>
                    </div>
                    <div class="box">
                        <a href="../assets/setups/Teleassistenza.exe">
                            <img src="images/teleassistenza.png" class="utility" />
                        </a>
                        <a href="../assets/setups/Teleassistenza.exe">Scarica il programma per la teleasisstenza</a>
                    </div>                    
                    <div class="box">
                        <a href="http://www.fenealgest.it/manual/Esercitazioni/Index.html" target="_blank">
                            <img src="images/guida.png" class="utility" />
                        </a>     
                        <a href="http://www.fenealgest.it/manual/Esercitazioni/Index.html" target="_blank">Accedi alla guda del gestionale Fenealgest</a>
                    </div>
                    <div class="box">
                        <a href="http://www.fenealuil.it" target="_blank">
                            <img src="images/utenti.png" class="utility" />
                        </a>
                        <a href="http://www.fenealuil.it" target="_blank">Accedi al sito web della Feneal Nazionale</a>
                    </div>                
                </div>	
		</div>
		<div class="column">
			    <h3>Dati di login</h3>
			        <%=this.HtmlLoginDataList("images/info.png") %>	    
			    <h3>Help on-line</h3>
			    <ul>
				    <li>
					    <h4>Presentazione contenuti pagina</h4>	
					    <font class="data">Home area riservata</font>						
					    Questa sezione sarà dedicata alla spigazione dei contenuti presenti in ogni pagina del portale al fine di migliorarne la comprensione e dunque l'utilizzo. 
					    La pagina corrente è la home page dell'area riservata a cui un utente accede dopo avere inserito le sue credenziali. Nel centro della
					    pagina sono disponibili in evidenza il link per scaricare il programma di teleassistenza, il link per accedere all'area di download, il link per accedere alla guida on line del programma gestionale Fenealgest e un link diretto 
					    al sito della Feneal Nazionale.
				    </li> 
				    													
			    </ul>											
		  </div>	
</div>



</asp:Content>

