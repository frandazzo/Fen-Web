using System;
using System.Collections.Generic;
using System.Text;
using WIN.FENGEST_NAZIONALE.DOMAIN;
using WIN.TECHNICAL.PERSISTENCE;
using System.Collections;
using WIN.FENGEST_NAZIONALE.DOMAIN.Public;

namespace WIN.FENGEST_NAZIONALE.HANDLERS
{
    public class ContactProvider: IContactProvider
    {
       protected IPersistenceFacade _ps;


        public ContactProvider(IPersistenceFacade ps)
        {
            _ps = ps;
        }

        #region IContactProvider Membri di

        public string[] GetContacts()
        {
            Query q = _ps.CreateNewQuery("MapperContatti");
        
            IList contatti = q.Execute(_ps);

            string[] cn = new string[contatti.Count];
            int i =0;
            foreach (Contatti c in contatti)
            {
                cn[i] = c.Mail;
                i++;
            }
            return cn;

        }

        #endregion
    }
}
