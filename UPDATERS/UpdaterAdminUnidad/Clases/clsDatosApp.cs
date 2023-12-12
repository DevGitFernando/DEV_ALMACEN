using System;
using System.Collections.Generic;
using System.Text;

namespace UpdaterAdminUnidad
{
    public class clsDatosApp
    {
        public string sModulo = "SC-Solutions";
        public string sVersion = "12.15.2006.1300";

        public clsDatosApp(string Modulo, string Version)
        {
            this.sModulo = Modulo;
            this.sVersion = Version;
        }

        public string Modulo
        {
            get { return sModulo; }
            set { sModulo = value; }
        }

        public string Version
        {
            get { return sVersion; }
            set { sVersion = value; }
        }
    }
}
