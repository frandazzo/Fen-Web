using System;
using System.Collections.Generic;
using System.Text;
using BilancioFenealgest.DomainLayer;
using WIN.TECHNICAL.PERSISTENCE;
using System.Collections;
using WIN.FENGEST_NAZIONALE.DOMAIN.Financial;
using WIN.BASEREUSE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.Financial
{
    public class BilancioHandler
    {
        private IPersistenceFacade _persistence;
        private bool _found;
        private RendicontoWrapper _rendiconto;
        private GeoLocationFacade _geo;
        public BilancioHandler(IPersistenceFacade f, GeoLocationFacade geo)
        {
            _persistence = f;
            _geo = geo;
            
        }



        public bool Found
        {
            get {return  _found; }
        }

        public Rendiconto Rendiconto
        {
            get 
            { 
                return _rendiconto.Rendiconto  ; 
            }
        }



        public Rendiconto RendicontoFromTemplate(string template)
        {
            return _rendiconto.RendicontoFromTemplate(template);
        }


        public double ContoRLST
        {
            get
            {
                if (_rendiconto == null)
                    return 0;
                return _rendiconto.DTORendiconto.ContoRLST;
            }
        }


        public void LoadRendiconto(string regione, string provincia, int anno, bool forRlst)
        {
            Query q = _persistence.CreateNewQuery("MapperRendicontoWrapper");

            AbstractBoolCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);

            c.AddCriteria (Criteria.Equal("Anno", anno.ToString ()));
            c.AddCriteria(Criteria.MatchesEqual("NomeRegione", regione , _persistence.DBType));
            c.AddCriteria (Criteria.MatchesEqual("NomeProvincia", provincia, _persistence.DBType));
            if (forRlst)
                c.AddCriteria(Criteria.MatchesEqual("ContoRLST", "RLST", _persistence.DBType));
            else
                c.AddCriteria(Criteria.MatchesEqual("ContoRLST", "FENEAL", _persistence.DBType));

            q.AddWhereClause(c);

            ExecuteQuery(q);
        }


        public IList<SendCheck> GetSendedRendiconto(int anno, bool isTipoRegionale, bool forRlst)
        {
            Query q = _persistence.CreateNewQuery("MapperRendicontoWrapper");

            AbstractBoolCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);

            c.AddCriteria(Criteria.Equal("Anno", anno.ToString()));

           
            c.AddCriteria(Criteria.MatchesEqual("IsRegionale", isTipoRegionale.ToString (), _persistence.DBType));

            if (forRlst)
                c.AddCriteria(Criteria.MatchesEqual("ContoRLST", "RLST", _persistence.DBType));
            else
                c.AddCriteria(Criteria.MatchesEqual("ContoRLST", "FENEAL", _persistence.DBType));


            q.AddWhereClause(c);


            IList rendiconti = q.Execute(_persistence);


            return CreateSendCheckList(rendiconti, _geo, isTipoRegionale);



        }

        private IList<SendCheck> CreateSendCheckList(IList rendiconti, GeoLocationFacade geo,bool isTipoRegionale)
        {
            IList elements;
            IList<SendCheck> _checks = new List<SendCheck>();

            if (isTipoRegionale)
                elements = geo.GetListaRegioni();
            else
                elements = geo.GetListaProvincie();


            foreach (string item in elements)
            {
                SendCheck c = new SendCheck();

                c.Description = item;

                CompileCheck(isTipoRegionale, c, rendiconti);

                _checks.Add(c);
            }


            return _checks;


        }

        private void CompileCheck(bool isTipoRegionale, SendCheck item, IList rendiconti)
        {
            if (isTipoRegionale)
                CompileForRegioni(item, rendiconti);
            else
                CompileForProvince(item, rendiconti);

        }

        private void CompileForRegioni(SendCheck item, IList rendiconti)
        {
            foreach (RendicontoWrapper elem in rendiconti)
            {
                if (elem.Rendiconto.Region.Equals(item.Description))
                {
                    item.Sended = "Inviato";
                    break;
                }
            }
        }

        private void CompileForProvince(SendCheck item, IList rendiconti)
        {
            foreach (RendicontoWrapper elem in rendiconti)
            {
                if (elem.Rendiconto.Province.Equals(item.Description))
                {
                    item.Sended = "Inviato";
                    break;
                }
            }
        }


        private void ExecuteQuery(Query q)
        {
            IList w = q.Execute(_persistence);

            if (w.Count > 0)
            {
                RendicontoWrapper r = w[0] as RendicontoWrapper;
                //non deve mai capitare
                if (r == null)
                    throw new ArgumentException("RendicontoWrapper nullo: impossibile trasformarlo in oggetto di tipo rendiconto");
                _rendiconto = r;
                
                _found = true;
            }
            else
            {
                _rendiconto = null;
                _found = false;
            }
        }



        public void InsertOrUpdateRendiconto(DTORendiconto rendiconto, bool forRlst)
        {
          
            LoadRendiconto(rendiconto.Regione,rendiconto .Provincia , rendiconto.Anno, forRlst);
           

            //e lo reinserisco aggiornato
            InsertRendiconto(rendiconto, forRlst);

        }

        private void InsertRendiconto(DTORendiconto dto, bool forRlst)
        {
            RendicontoWrapper rendiconto = TransformToPwersistibleObject(dto, forRlst);

            try
            {
                //action questo punto apro un transazione
                _persistence.BeginTransaction();
                _persistence.MarkNew(rendiconto);
                foreach (ItemRendicontoWrapper  item in rendiconto.Items)
                {
                    _persistence.MarkNew(item);
                }
                //Se esiste lo cancello. Da db la cancellazione del parent cancella anche i figli
                if (_found)
                {
                    _persistence.MarkRemoved(_rendiconto);
                }
                _persistence.CommitTransaction();

            }
            catch (Exception)
            {
                _persistence.RollBackTRansaction();
                throw;
            }

        }

        private static RendicontoWrapper TransformToPwersistibleObject(DTORendiconto dto, bool forRlst)
        {
            RendicontoWrapper rendiconto = new RendicontoWrapper(dto, forRlst);
            //lo normalizzo per portalro ad uno stato persistibile con i figli
            rendiconto.NormalizeDTOForInsertOrUpdate();
            // dato che si tratta di un master detail 
            //per prima cosa riporto un riferimento del padre sui figli
            rendiconto.SetParentReferenceOnChildren();

            return rendiconto;
        }


      


    }
}
