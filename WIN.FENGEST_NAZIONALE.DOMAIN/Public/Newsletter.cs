using System;
using System.Collections.Generic;
using System.Text;
using WIN.TECHNICAL.SECURITY_NEW.PasswordManagement;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.Public
{
    public class Newsletter
    {

        private IMailer _mailer;
        private IContactProvider _contactProvider;
        private string _to = "";

        public Newsletter(IMailer mailer, IContactProvider contactProvider)
        {

            //aggiungere eccezione se gli oggetti sono nulli
            if (mailer != null)
                _mailer = mailer;
            else
                throw new Exception("Mailer non impostato");
            if (contactProvider != null)
                _contactProvider = contactProvider;
            else
                throw new Exception("Provider non impostato");

        }
        public Newsletter(IMailer mailer, string to)
        {

            //aggiungere eccezione se gli oggetti sono nulli
            if (mailer != null)
                _mailer = mailer;
            else
                throw new Exception("Mailer non impostato");

            _to = to;
        }


        public void Send(string subject, string body)
        {
            if (_contactProvider == null)
            {
                _mailer.SendMail(_to, subject, body);
            }
            else
            {
                string[] contacts = _contactProvider.GetContacts();
                _mailer.SendMail(contacts, subject, body);

            }
           
        }

    }
}
