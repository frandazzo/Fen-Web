using System;
using System.Collections.Generic;
using System.Text;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.ValidationSubsystem
{
    public class DocumentFactory
    {
        public static Document CreateDocument(DocumentDTO prototype, Worker worker, Export parentExport, GeoLocationFacade geoFacade)
        {
            Document d = new Document();

            d.Struttura = parentExport.Structure.ToUpper();
            d.Area = UILDescriber.Instance.AreaByStruttura(parentExport.Structure);
            //prendo la provincia
            d.Province = parentExport.Province;
            //prendo la regione
            d.Regione = geoFacade.GetGeoHandler().GetRegioneById(d.Province.IdRegione.ToString());

            //imposto il comune
            SetCity(prototype, geoFacade, d);


            d.Worker = worker;
            d.ParentExport = parentExport;
            d.DocumentDate = prototype.DocumentDate;


            if (prototype.DocumentType == null)
                prototype.DocumentType = "";

            d.DocumentType = prototype.DocumentType;

            if (prototype.Notes == null)
                prototype.Notes = "";

            d.Notes = prototype.Notes;

            if (prototype.State == null)
                prototype.State = "";

            d.State = prototype.State;

            return d;
        }


        private static void SetCity(DocumentDTO prototype, GeoLocationFacade geoFacade, Document d)
        {
            //evito a priori qualunque problema di riferimento nullo anche se non dovrebbe mai accadere
            if (prototype.City == null)
                prototype.City = "";
            //calcolo il comune di invio documento
            Comune c = geoFacade.GetGeoHandler().GetComuneByName(prototype.City);

            //se il comune non è nullo o di tipo nullo allora imposto il comune del documento
            if (c != null)
                if (c.Id != -1)
                {
                    d.Comune  = c;
                }
            //se il comune non è stato specificato allora rimarrà un comune nullo
            


            


        }
    }
}
