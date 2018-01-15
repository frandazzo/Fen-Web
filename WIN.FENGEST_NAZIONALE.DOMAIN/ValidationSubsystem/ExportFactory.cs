using System;
using System.Collections.Generic;
using System.Text;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.ValidationSubsystem
{
    public class ExportFactory
    {

        private static Export w;

        public static Export CreateExportTrace(ExportTrace trace, GeoLocationFacade geoFacade)
        {
            if (trace == null)
                throw new ArgumentNullException("Impossibile creare una traccia di importazione: traccia nulla!");



            w = new Export();

            //Imposto le proprità per la persona
            w.ExportDate  = trace.ExportDate;
            w.ExportType  = trace.ExportType;
            w.ExporterName = trace.ExporterName;
            w.Transacted = trace.Transacted;
            w.Settore = trace.Sector;
            w.ExportNumber = trace.ExportNumber;
            w.ExporterMail = trace.ExporterMail;
            w.Structure = trace.Struttura.ToUpper();
            w.Area = UILDescriber.Instance.AreaByStruttura(trace.Struttura);
            //prendo la provincia
            w.Province = geoFacade.GetGeoHandler().GetProvinciaByName(trace.Province);

            //verifico l'esistenza della della provincia anche se tale controllo è ridondante
            if (w.Province == null)
                throw new Exception("Impossibile creare una traccia di esportazione: provincia nulla");


            if (w.Province != null)
                if (w.Province.Id == -1)
                    throw new Exception("Impossibile creare una traccia di esportazione: provincia nulla");


            //a questo punto posso anche recuperare la regione relativa alla provincia
            w.Regione = geoFacade.GetGeoHandler().GetRegioneById(w.Province.IdRegione.ToString());


           

            //Imposto adesso il periodo
            if (trace.PeriodType == PeriodType.Interval)
            {
                w.Period = new SubscriptionPeriod(trace.InitialDate, trace.EndDate);
            }
            else
            {
                w.Period = new SubscriptionPeriod(trace.Period, trace.Year, trace.PeriodType);
            }

            return w;
        }

        public static Export CreateExportTrace(INTEGRATION_ENTITIES.FenealgestImportExport.LiberiTrace trace, GeoLocationFacade geoFacade)
        {
            if (trace == null)
                throw new ArgumentNullException("Impossibile creare una traccia di importazione liberi: traccia nulla!");



            w = new Export();

            //Imposto le proprità per la persona
            w.ExportDate = DateTime.Now;
            w.ExportType = ExprtType.LiberiExport;
            w.ExporterName = "ConnectorAdmin";
            w.Transacted = true;
            w.Settore = "EDILE";
            w.ExportNumber = trace.ExportNumber;
            w.ExporterMail = trace.Mailto;
            w.Structure = "FENEAL";
            w.Area = UILDescriber.Instance.AreaByStruttura("FENEAL");
            //prendo la provincia
            w.Province = geoFacade.GetGeoHandler().GetProvinciaByName(trace.Provincia);

            //verifico l'esistenza della della provincia anche se tale controllo è ridondante
            if (w.Province == null)
                throw new Exception("Impossibile creare una traccia di esportazione: provincia nulla");


            if (w.Province != null)
                if (w.Province.Id == -1)
                    throw new Exception("Impossibile creare una traccia di esportazione: provincia nulla");


            //a questo punto posso anche recuperare la regione relativa alla provincia
            w.Regione = geoFacade.GetGeoHandler().GetRegioneById(w.Province.IdRegione.ToString());

            w.Period = new SubscriptionPeriod(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year,DateTime.Now.Month)));
            

            return w;
        }
    }
}
