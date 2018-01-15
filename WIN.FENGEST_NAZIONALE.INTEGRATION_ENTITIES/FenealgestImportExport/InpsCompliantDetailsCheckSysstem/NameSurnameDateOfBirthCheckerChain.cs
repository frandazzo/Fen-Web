using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.InpsCompliantDetailsCheckSysstem
{
    public class NameSurnameDateOfBirthCheckerChain: AbstractChain 
    {
        public NameSurnameDateOfBirthCheckerChain(AbstractChain chain)
            : base(chain)
        { }

        public override bool AreDatailsCompliant(InpsTraceDetails first, InpsTraceDetails second)
        {
            if (first == null || second == null)
                return false;

            //per prima cosa verifico il dato più stringente ossia la data di nascita
            //se per uno dei due è nulla non posso eseguire l'uguaglianza...
            if (string.IsNullOrEmpty(first.DATA_NASCITA_UTENTE) || string.IsNullOrEmpty(second.DATA_NASCITA_UTENTE))
            {
                if (_chain != null)
                    return _chain.AreDatailsCompliant(first, second);
                else
                    return false;
            }

            //a questo punto punto sono sicuro che le date di nascita ci sono (sono delle stringhe di cui non sono sicuro del formato)
            //devo assicurarmi che il loro sia un formato data
            if (!(first.IsDataNascitaFormatCorrect () && second.IsDataNascitaFormatCorrect()))
            {
                //le date non sono in un formato corretto 
                if (_chain != null)
                    return _chain.AreDatailsCompliant(first, second);
                else
                    return false;
            }

            //qui le date sono corrette
            //faccio un controllo sui campi minimi di nome  ecognome che non dovrebbero essere assenti
            //se anche uno è assente ritorno direttamente falso
            if (!(first.HasNomeCognomeNotNullOrEmpty() && second.HasNomeCognomeNotNullOrEmpty()))
            {
                return false;
            }


            //qui può partire l'uguaglianza sul nome, cognome e sulla data di nascita
            if (first.NOME_UTENTE.ToUpper().Trim().Equals(second.NOME_UTENTE.ToUpper().Trim()) &&
                first.COGNOME_UTENTE.ToUpper().Trim().Equals(second.COGNOME_UTENTE.ToUpper().Trim()) &&
                first.DATA_NASCITA_UTENTE.Equals(second.DATA_NASCITA_UTENTE))
            {
                return true;
            }
            else
                return false;


            //return _chain.AreDatailsCompliant(first, second);
        }
    }
}
