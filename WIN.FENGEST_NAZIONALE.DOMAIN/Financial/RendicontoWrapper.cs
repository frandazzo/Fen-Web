using System;
using System.Collections.Generic;
using System.Text;
using BilancioFenealgest.DomainLayer;
using WIN.BASEREUSE;
using System.Collections;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.Financial
{
    public class RendicontoWrapper : AbstractPersistenceObject
    {
        DTORendiconto _rendiconto;
        IList _items = new ArrayList();
        private bool normalized = false;
        private bool _forRlst = false;

        public RendicontoWrapper(DTORendiconto rendiconto, bool forRlst)
        {
            _rendiconto = rendiconto;
            _forRlst = forRlst;

        }


        public bool ForRlst
        {
            get
            {
                return _forRlst;
            }
        }


        public void NormalizeDTOForInsertOrUpdate()
        {
            if (_rendiconto == null)
                return;

            foreach (DTORendicontoItem  item in _rendiconto .Items)
            {
                _items.Add(new ItemRendicontoWrapper(item));
            }


        }


        public DTORendiconto NormalizedDTORendicontoAfterRetrieve
        {
            get 
            {
                return CompleteRendiconto(); 
            }
        }

        public DTORendiconto DTORendiconto
        {
            get
            {
                return _rendiconto ;
            }
        }

        private DTORendiconto CompleteRendiconto()
        {
            if (_rendiconto == null)
                return null;

            if (normalized == false)
            {
                Nomalize();
            }

            return _rendiconto;
        }

        private void Nomalize()
        {
            IList l = new ArrayList();

            foreach (ItemRendicontoWrapper  item in _items )
            {
                l.Add(item.Item);
            }

            _rendiconto.Items = l;

            normalized = true;
        }


        public IList Items
        {
            get { return _items; }
            set { _items = value; }
        }


        public Rendiconto Rendiconto
        {
            get
            {
                Rendiconto r =RendicontoFactory.CreateRendiconto(DTORendiconto);
                r.DeserializeStatoPatrimoniale(DTORendiconto.StatoPatrimoniale);
                return r;
            }
        }



        public Rendiconto RendicontoFromTemplate(string template)
        {
            Rendiconto r = RendicontoFactory.CreateRendiconto(DTORendiconto,template);
            r.DeserializeStatoPatrimoniale(DTORendiconto.StatoPatrimoniale);
            return r;
        }

        public void SetParentReferenceOnChildren()
        {
            foreach (ItemRendicontoWrapper  item in _items )
            {
                item.Parent = this;
            }
        }
    }
}
