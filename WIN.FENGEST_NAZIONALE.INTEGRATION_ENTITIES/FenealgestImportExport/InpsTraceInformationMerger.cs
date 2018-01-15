using System;
using System.Collections.Generic;
using System.Text;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.InpsCompliantDetailsCheckSysstem;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport
{
    public class InpsTraceInformationMerger
    {
        public delegate void CountEventHandler(int totalCycles, int  cycles);
        public event CountEventHandler CountProgress;

        AbstractChain detailComplaintchecker;

        public InpsTraceInformationMerger()
        {
            detailComplaintchecker = InpsTraceDetailComplaintcheckerFactory.CreateChain();
        }

        public InpsTrace CreateTraceWithExceptions(InpsTrace targetOfInformation, InpsTrace supplementaryInformation)
        {
            InpsTrace result = new InpsTrace();


            result.Mailto = targetOfInformation.Mailto;
            result.Provincia = targetOfInformation.Provincia;
            result.Subject = targetOfInformation.Subject;
            result.InpsTraceDetails = new InpsTraceDetails[] { };

            CompleteResultDeatilsWithException(result, targetOfInformation, supplementaryInformation);



            return result;
        }

        private void CompleteResultDeatilsWithException(InpsTrace result, InpsTrace targetOfInformation, InpsTrace supplementaryInformation)
        {

            int numberOfCycles = supplementaryInformation.InpsTraceDetails.Length ;

            InpsTraceDetails[] det = new InpsTraceDetails[] { };
            int i = 0;
            //ciclo su ogni record con informazioni supplementari
            foreach (InpsTraceDetails suppl in supplementaryInformation.InpsTraceDetails)
            {
                i++;
                bool found = false;
                //ciclo su ogni elemento detail del target in modo da verificare se i dettagli sono compatibili
                //e se lo sono aggiungere al detail le informazioni aggiuntive del supplemnto
                foreach (InpsTraceDetails target in targetOfInformation.InpsTraceDetails)
                {
                    if (detailComplaintchecker.AreDatailsCompliant(target, suppl))
                    {
                        found = true;
                        break;
                    }

                }

                if (!found)
                {
                    //aggiungo il suppl all'array
                    Array.Resize<InpsTraceDetails>(ref det, det.Length  + 1);
                    det[det.Length - 1] = suppl;
                }
                if (CountProgress != null)
                    CountProgress(numberOfCycles, i);
                
            }
            result.InpsTraceDetails = det;
            
        }


        public InpsTrace CreateTrace(InpsTrace targetOfInformation, InpsTrace supplementaryInformation)
        {
            InpsTrace result = new InpsTrace();


            result.Mailto = targetOfInformation.Mailto;
            result.Provincia = targetOfInformation.Provincia;
            result.Subject = targetOfInformation.Subject;

            CompleteResultDeatils(result, targetOfInformation, supplementaryInformation);



            return result;
        }

        private void CompleteResultDeatils(InpsTrace result, InpsTrace targetOfInformation, InpsTrace supplementaryInformation)
        {

            //ciclo su ogni record con informazioni supplementari
            foreach (InpsTraceDetails  suppl in supplementaryInformation.InpsTraceDetails  )
            {
                
                //ciclo su ogni elemento detail del target in modo da verificare se i dettagli sono compatibili
                //e se lo sono aggiungere al detail le informazioni aggiuntive del supplemnto
                foreach (InpsTraceDetails  target in targetOfInformation.InpsTraceDetails )
                {
                    if (detailComplaintchecker.AreDatailsCompliant(target, suppl))
                    {
                        target.NOME_REFERENTE = suppl.NOME_REFERENTE;
                        target.COGNOME_REFERENTE = suppl.COGNOME_REFERENTE;
                        //target.TIPO_PRESTAZIONE = suppl.TIPO_PRESTAZIONE;
                    }

                }


            }

            //passo i dati mergiati alla traccia di ritorno
            result.InpsTraceDetails = targetOfInformation.InpsTraceDetails;

        }
    }
}
