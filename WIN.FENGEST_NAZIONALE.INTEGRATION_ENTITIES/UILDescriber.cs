using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES
{
    public  class UILDescriber
    {

        public readonly  string Feneal = "FENEAL";
        private readonly string FenealIF = "IMPIANTI FISSI";
        private readonly string FenealEDILE = "EDILE";
        private readonly string FenealInps = "INPS";

        public readonly string Uila = "UILA";
        private readonly string UilaAgro = "AGROALIMENTARE";


        public readonly string Uimec = "UIMEC";
        private readonly string UmecCol = "COLTIVATORI DIRETTI";
        private readonly string UmecMez = "MEZZADRI";

        public readonly string Unsa = "UIL UNSA";
        private readonly string UnsaS = "SCRITTORI E ARTISTI";

        public readonly string Uilcem = "UILCEM";
        private readonly string Uilcem1 = "CHIMICO";
        private readonly string Uilcem2 = "ENERGIA";
        private readonly string Uilcem3 = "MANIFATTURIERO";

        public readonly string Uilm = "UILM";
        private readonly string Uilm1 = "METALMECCANICO";


        public readonly string Uilta = "UILTA";
        private readonly string Uilta1 = "TESSILE";
        private readonly string Uilta2 = "ABBIGLIAMENTO";
        private readonly string Uilta3 = "CALZATURIERO";

        public readonly string Uilcpo = "UIL CPO";
        private readonly string UilCpo1 = "LAVORATORI ATIPICI";



        public readonly string UilFpl = "UILFPL";
        private readonly string UilFpl1 = "POTERI LOCALI";

        public readonly string UilScuola = "UIL SCUOLA";
        private readonly string UilScuola1 = "SCUOLA";

        public readonly string Uilpa = "UILPA";
        private readonly string Uilpa1 = "PUBBLICA AMMINISTRAZIONE";

        public readonly string Uiloocc = "UIL OO CC";
        private readonly string Uiloocc1 = "ORGANI COSTITUZIONALI";

        public readonly string Uilpen = "UILPENSIONATI";
        private readonly string Uilpen1 = "PENSIONATI";

        public readonly string Uiltra = "UILTRASPORTI";
        private readonly string Uiltra1 = "TRASPORTI";




        public readonly string Uilca = "UILCA";
        private readonly string Uilca1 = "CREDITO";
        private readonly string Uilca2 = "ESATTORIE";
        private readonly string Uilca3 = "ASSICURAZIONI";

        public readonly string Uilposte = "UIL POSTE";
        private readonly string Uilposte1 = "LAVORATORI POSTELEGRAFICI";


        public readonly string Uilcom = "UIL COMUNICAZIONE";
        private readonly string Uilcom1 = "LAVORATORI COMUNICAZIONE";

        public readonly string Uiltuc = "UILTUCS";
        private readonly string Uiltuc1 = "TURISMO";
        private readonly string Uiltuc2 = "COMMERCIO";
        private readonly string Uiltuc3 = "SERVIZI";


        public readonly string Uilserv = "UIL SERVIZI";
        private readonly string Uilserv1 = "UNIAT";
        private readonly string Uilserv2 = "ADOC";
        private readonly string Uilserv3 = "ITAL";
        private readonly string Uilserv4 = "CAF";


        private IList<CategoryDescriber> _describers = new List<CategoryDescriber>();

        private static UILDescriber _instance;

        private UILDescriber()
        {
           
            _describers.Add(new CategoryDescriber(Uila,"AGRICOLTURA", new string[] { UilaAgro }));
            _describers.Add(new CategoryDescriber(Uimec, "AGRICOLTURA", new string[] { UmecCol, UmecMez }));
            _describers.Add(new CategoryDescriber(Unsa, "CULTURA", new string[] { UnsaS }));
            _describers.Add(new CategoryDescriber(Feneal, "INDUSTRIA", new string[] { FenealIF, FenealEDILE, FenealInps }));
            _describers.Add(new CategoryDescriber(Uilcem, "INDUSTRIA", new string[] { Uilcem1, Uilcem2, Uilcem3 }));
            _describers.Add(new CategoryDescriber(Uilm, "INDUSTRIA", new string[] { Uilm1 }));
            _describers.Add(new CategoryDescriber(Uilta, "INDUSTRIA", new string[] { Uilta1, Uilta2, Uilta3 }));
            _describers.Add(new CategoryDescriber(Uilcpo, "LAVORI ATIPICI", new string[] { UilCpo1 }));
            _describers.Add(new CategoryDescriber(UilFpl, "PUBBLICA AMMINISTRAZIONE", new string[] { UilFpl1 }));
            _describers.Add(new CategoryDescriber(UilScuola, "PUBBLICA AMMINISTRAZIONE", new string[] { UilScuola1 }));
            _describers.Add(new CategoryDescriber(Uilpa, "PUBBLICA AMMINISTRAZIONE", new string[] { Uilpa1 }));
            _describers.Add(new CategoryDescriber(Uiloocc, "PUBBLICA AMMINISTRAZIONE", new string[] { Uiloocc1 }));
            _describers.Add(new CategoryDescriber(Uilpen, "PENSIONATI", new string[] { Uilpen1 }));
            _describers.Add(new CategoryDescriber(Uiltra, "TRASPORTI", new string[] { Uiltra1 }));

            _describers.Add(new CategoryDescriber(Uilca, "SERVIZI", new string[] { Uilca1, Uilca2, Uilca3 }));
            _describers.Add(new CategoryDescriber(Uiltuc, "SERVIZI", new string[] { Uiltuc1, Uiltuc2, Uiltuc3 }));
            _describers.Add(new CategoryDescriber(Uilposte, "SERVIZI", new string[] { Uilposte1 }));
            _describers.Add(new CategoryDescriber(Uilcom, "SERVIZI", new string[] { Uilcom1 }));

            _describers.Add(new CategoryDescriber(Uilserv, "UIL SERVIZI", new string[] { Uilserv1, Uilserv2, Uilserv3, Uilserv4 }));
        }

        public static UILDescriber Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UILDescriber();
                return _instance;
            }
        }


        public IList<string> ListaStrutture
        {
            get
            {
                IList<string> s = new List<string>();

                foreach (CategoryDescriber  item in _describers )
                {
                    s.Add(item.Name);
                }


                return s;
            }
        }


        public IList<string> ListaStruttureByArea(string area)
        {
            IList<string> s = new List<string>();

            foreach (CategoryDescriber item in _describers)
            {
                if (area.ToUpper().Equals (item.Area))
                    s.Add(item.Name);
            }

            return s;
     
        }

        public IList<string> ListaSettoriByStruttura(string struttura)
        {

            CategoryDescriber d = GetStruttura(struttura);

            if (d == null)
                return new List<string>();


            return d.Sectors;


            

        }


        public bool  ExsistSettoreByStruttura(string struttura, string settore)
        {

            CategoryDescriber d = GetStruttura(struttura);

            if (d == null)
                return false;


            string s = SearchSettore(d, settore);

            if (string.IsNullOrEmpty(s))
                return false;

            return true;


        }


        public bool ExsistStruttura(string struttura)
        {

            CategoryDescriber d = GetStruttura(struttura);

            if (d == null)
                return false;
            return true;


        }

        private string SearchSettore(CategoryDescriber d, string settore)
        {
            foreach (string item in d.Sectors )
            {
                if (settore.ToUpper().Equals(item))
                    return item;
            }
            return "";
        }

        private CategoryDescriber GetStruttura(string struttura)
        {
            CategoryDescriber d = null;

            foreach (CategoryDescriber item in _describers)
            {
                if (struttura.ToUpper().Equals(item.Name))
                {
                    d = item;
                    break;
                }
            }
            return d;
        }


        private class CategoryDescriber
        {
            private string _name;
            private IList<string> _sectors = new List<string>();
            private string _area;


            public string Area
            {
                get
                {
                    return _area;
                }
            }

            internal CategoryDescriber(string name, string area, params string[] args)
            {
                _name = name;
                _area = area;
                foreach (string item in args )
                {
                    _sectors.Add(item);
                }
            }

            public IList<string> Sectors
            {
                get
                {
                    return _sectors;
                }
            }

            public string Name
            {
                get { return _name; }
            }


        }


        public string AreaByStruttura(string p)
        {
            CategoryDescriber d = GetStruttura(p);

            if (d == null)
                return "";

            return d.Area;
        }
    }
}
