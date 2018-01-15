using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using BilancioFenealgest.DomainLayer;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.Financial
{
    public class ItemRendicontoWrapper : AbstractPersistenceObject
    {
        DTORendicontoItem _item;
        RendicontoWrapper _parent;
        public ItemRendicontoWrapper(DTORendicontoItem item)
        {
            _item = item;
        }

        public DTORendicontoItem Item
        {
            get { return _item; }
        }

        public RendicontoWrapper Parent
        {
            get{return _parent;}
            set{_parent = value ;}
        }
    }
}
