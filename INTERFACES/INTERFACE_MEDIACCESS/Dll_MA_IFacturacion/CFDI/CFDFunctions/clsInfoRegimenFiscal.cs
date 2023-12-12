namespace Dll_MA_IFacturacion.CFDI.CFDFunctions
{
    using System;

    public class clsInfoRegimenFiscal
    {
        private string mRegimen;
        private long mSys_PK;

        public clsInfoRegimenFiscal()
        {
            this.mRegimen = "";
        }

        public clsInfoRegimenFiscal(string sRegimen)
        {
            this.mRegimen = "";
            this.Regimen = sRegimen;
        }

        public clsInfoRegimenFiscal(string sRegimen, long lSys_PK)
        {
            this.mRegimen = "";
            this.Regimen = sRegimen;
            this.Sys_PK = lSys_PK;
        }

        public string Regimen
        {
            get
            {
                return this.mRegimen;
            }
            set
            {
                this.mRegimen = value;
            }
        }

        public long Sys_PK
        {
            get
            {
                return this.mSys_PK;
            }
            set
            {
                this.mSys_PK = value;
            }
        }
    }
}

