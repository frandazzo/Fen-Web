using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.WEBCONNECTOR.SharetopServices;

namespace WIN.WEBCONNECTOR.controls
{
    interface IPrintableAndSearchable
    {
        void PrintReport(OrganizzativeData data);
        void LoadData(OrganizzativeData data);
        
    }

    //public enum ViewType
    //{
    //    DatiStruttura,
    //    CongressoRegionale,
    //    CongressoTerritoriale,
    //    Rappresentanze,
    //    Rappresentanti

    //}
}
