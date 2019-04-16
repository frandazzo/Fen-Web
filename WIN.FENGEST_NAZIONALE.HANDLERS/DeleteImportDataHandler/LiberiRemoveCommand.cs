using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.BASEREUSE;
using WIN.TECHNICAL.PERSISTENCE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.DeleteImportDataHandler
{
    public class LiberiRemoveCommand: ICommand
    {
        private IPersistenceFacade _persistence;

        private int _provinceId;
        private string _ente;

        public LiberiRemoveCommand(IPersistenceFacade f, int provinceId, string ente)
        {
            _persistence = f;
            _provinceId = provinceId;
            _ente = ente;

        }

        #region ICommand Membri di

        public void Execute()
        {
            try
            {
                _persistence.BeginTransaction();


                _persistence.ExecuteNonQuery(string.Format("insert into lavoratori_liberi_copy select * from lavoratori_liberi where Id_ProvinciaFeneal = {0} and Ente = '{1}'", _provinceId, _ente));

                _persistence.ExecuteNonQuery(string.Format("Delete from lavoratori_liberi where Id_ProvinciaFeneal = {0} and Ente = '{1}'", _provinceId, _ente));
              

                _persistence.CommitTransaction();
            }
            catch (Exception ex)
            {
                _persistence.RollBackTRansaction();
                throw ex;
            }
           
        }

        #endregion
    }
}
