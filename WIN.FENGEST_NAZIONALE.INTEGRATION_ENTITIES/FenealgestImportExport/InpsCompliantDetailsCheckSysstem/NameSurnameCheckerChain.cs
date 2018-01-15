using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.InpsCompliantDetailsCheckSysstem
{
    public class NameSurnameCheckerChain: AbstractChain 
    {
        public NameSurnameCheckerChain()
            : base(null)
        { }

        public override bool AreDatailsCompliant(InpsTraceDetails first, InpsTraceDetails second)
        {
            if (first == null || second == null)
                return false;

           

            //qui le date sono corrette
            //faccio un controllo sui campi minimi di nome  ecognome che non dovrebbero essere assenti
            //se anche uno è assente ritorno direttamente falso
            if (!(first.HasNomeCognomeNotNullOrEmpty() && second.HasNomeCognomeNotNullOrEmpty()))
            {
                return false;
            }


            //qui può partire l'uguaglianza sul nome, cognome 
            if (first.NOME_UTENTE.ToUpper().Trim().Equals(second.NOME_UTENTE.ToUpper().Trim()) &&
                first.COGNOME_UTENTE.ToUpper().Trim().Equals(second.COGNOME_UTENTE.ToUpper().Trim()))
            {
                return true;
            }


            return false;
        }
    }
}
