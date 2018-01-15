using System;
using System.Collections.Generic;
using System.Text;
using WIN.TECHNICAL.PERSISTENCE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Public;
using System.Collections;

namespace WIN.FENGEST_NAZIONALE.HANDLERS
{
    public class TraceProvider
    {
        protected IPersistenceFacade _ps;


         public TraceProvider(IPersistenceFacade ps)
        {
            _ps = ps;
        }

         public void InsertOrUpdateTrace(string app, string province, string region)
         {
             int year = DateTime.Now.Year;
             string month = GetMonth();

             //A questo punto devo cercare un oggetto trace per la provincia, l'anno, l'app, e mese
             //se la trovo ne incremento il contatore e salvo
             //altrimenti ne creo una nuova e la inserisco
             Trace t = RetrieveTrace(app, year, province, month);

             //se è nullo lo creo
             if (t == null)
             {
                 t = new Trace();
                 t.Month = month;
                 t.Year = year;
                 t.Region = region;
                 t.Province = province;
                 t.App = app;
                 t.Counter = 1;
                 _ps.InsertObject(t.GetType().Name, t);
                 return;
             }

             //incremento il contatore
             t.Counter = t.Counter + 1;
             _ps.UpdateObject(t.GetType().Name, t);
         }

         private Trace RetrieveTrace(string app, int year, string province, string month)
         {
             Query q = _ps.CreateNewQuery("MapperTrace");

             CompositeCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);

            
             c.AddCriteria(Criteria.MatchesEqual("App", app, _ps.DBType));
             c.AddCriteria(Criteria.Equal("Year", year.ToString()));
             c.AddCriteria(Criteria.MatchesEqual("Province", province, _ps.DBType));
             c.AddCriteria(Criteria.MatchesEqual("Month", month, _ps.DBType));

             q.AddWhereClause(c);

             IList w = q.Execute(_ps);

             if (w.Count > 0)
                 return w[0] as Trace;

             return null;

         }

         private string GetMonth()
         {
             int m = DateTime.Now.Month;


             switch (m)
             {
                 case 1:
                     return "GENNAIO";
                 case 2:
                     return "FEBBRAIO";
                 case 3:
                     return "MARZO";
                 case 4:
                     return "APRILE";
                 case 5:
                     return "MAGGIO";
                 case 6:
                     return "GIUGNO";
                 case 7:
                     return "LUGLIO";
                 case 8:
                     return "AGOSTO";
                 case 9:
                     return "SETTEMBRE";
                 case 10:
                     return "OTTOBRE";
                 case 11:
                     return "NOVEMBRE";
                 case 12:
                     return "DICEMBRE";

                 default:
                     return "";
                     
             }

         }

    }
}
