using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using WIN.TECHNICAL.PERSISTENCE;
using System.Collections;
using WIN.FENGEST_NAZIONALE.DOMAIN.Public;
using FenealgestWEB.Utility;
using WIN.BASEREUSE;

namespace FenealgestWEB.assets.control
{
    public partial class HeaderHome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ASPxCallback1_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            string elle = e.Parameter;


            //Verifico che non si tratti di una operazione di deregistrazione
            //me ne accorgo dal cancelletto finale messo dal codice javascript
            if (!string.IsNullOrEmpty(elle))
            {
                if (elle.EndsWith("#"))
                {
                    Deregister(elle.Substring(0,elle.Length-1), e);
                    return;
                }
            }

            if (string.IsNullOrEmpty(elle))
            {
                e.Result = "<br/> Specificare una mail valida. <br/>";
                return;
            }
            else
            {
                string MatchEmailPattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                if (!Regex.IsMatch(elle, MatchEmailPattern))
                {
                    e.Result = "<br/> Specificare una mail valida. <br/>";
                    return;
                }
            }

            // a questo punto posso procedere con l'inserimento nella base dati
            try
            {
                IPersistenceFacade p = SessionServiceManager.PersistentService;

                Query q = p.CreateNewQuery("MapperContatti");

                q.SetTable("Contatti");

                CompositeCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);

               
                c.AddCriteria(Criteria.MatchesEqual("Mail", elle, p.DBType));

                q.AddWhereClause(c);


                IList cont = q.Execute(p);

                //imposto l'interfaccia utente
                if (cont.Count > 0)
                {
                    e.Result = "<br/> Mail già presente. <br/>";
                    return;
                }
                else
                {
                    Contatti cn = new Contatti();
                    cn.Mail = elle;
                    p.InsertObject("Contatti", cn);
                    e.Result = "<br/> Registrazione avvenuta con successo. <br/>";
                    return;
                }
            }
            catch (Exception)
            {
                    e.Result = "<br/> Si è verificato un errore. Contattare l'amministratore del sistema per una verifica. <br/>";
                    return;
            }

            
            

            
        }

        private void Deregister(string elle, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            try
            {
                IPersistenceFacade p = SessionServiceManager.PersistentService;

                Query q = p.CreateNewQuery("MapperContatti");

                q.SetTable("Contatti");

                CompositeCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);


                c.AddCriteria(Criteria.MatchesEqual("Mail", elle, p.DBType));

                q.AddWhereClause(c);


                IList cont = q.Execute(p);

                //imposto l'interfaccia utente
                if (cont.Count > 0)
                {
                    Contatti cn = new Contatti();
                    cn.Mail = elle;
                    p.DeleteObject("Contatti", (AbstractPersistenceObject)cont[0]);
                    e.Result = "<br/> Deregistrazione avvenuta con successo. <br/>";
                    return;
                }
                else
                    e.Result = "";
            }
            catch (Exception)
            {
                e.Result = "<br/> Si è verificato un errore. Contattare l'amministratore del sistema per una verifica. <br/>";
                return;
            }

        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            SessionServiceManager.SecurityService.ResetLogin();
            Response.Redirect(LinkHandler.LinkHomepage);
        }
     
    }
}