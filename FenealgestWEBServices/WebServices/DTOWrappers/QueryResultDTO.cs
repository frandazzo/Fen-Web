using System;
using System.Collections.Generic;
using System.Web;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;

namespace FenealgestWEB.WebServices.DTOWrappers
{
    [Serializable]
    public class QueryResultDTO
    {

        private string _error = "";
        public string Error
        {
            get { return _error; }
            set { _error = value; }
        }


        private string _message = "";
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private WorkerDTO _worker;
        public WorkerDTO WorkerDTO
        {
            get { return _worker; }
            set { _worker = value; }
        }

        private WorkerDTO[] _workers;
        public WorkerDTO[] Workers
        {
            get { return _workers; }
            set { _workers = value; }
        }


        public bool IsResultValid { get; set; }
    }
}
