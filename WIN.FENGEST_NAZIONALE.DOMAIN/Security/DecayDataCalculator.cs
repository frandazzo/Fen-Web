using System;
using System.Collections.Generic;
using System.Text;
using WIN.TECHNICAL.SECURITY_NEW.PasswordManagement;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.Security
{
    public class DecayDataCalculator: IPasswordDecayHandler 
    {
        public DateTime CalculateDecayDate(DateTime date)
        {
            return date.AddMonths(300).Date;
        }
    }
}
