using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;

namespace WIN.WEBCONNECTOR.Commands
{
    public class CommandFactory
    {
        public static ICommand CreateCommand()
        {
            switch (ProgramParameters.Instance.Command)
            {
                case "1":
                    return new ExportDataCommand();
                case "2":
                    return new QueryWorkerCommand();
                case "3":
                    return new QueryWorkerListCommand();
                case "4":
                    return new SearchWorkerCommand();
                case "5":
                    return new BackgroundExportDataFromFile();
                case "6":
                    return new BackgroundSearchWorkersCommand();
                case "7":
                    return new QueryWorkerByAziendaCommand();

            }
            return null;
        }
    }
}
