using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DllFarmaciaSoft
{
    public class clsSKU
    {
        private string sIdEmpresa = "";
        private string sIdEstado = "";
        private string sIdFarmacia = "";
        private string sTipoDeMovto = "";

        private string sFolioMovimiento = "";
        private string sFoliador = "";
        private string sSKU = "";        

        public clsSKU():this("") 
        { 
        }

        public clsSKU( string Movimiento )
        {
            sIdEmpresa = DtGeneral.EmpresaConectada;
            sIdEstado = DtGeneral.EstadoConectado;
            sIdFarmacia = DtGeneral.FarmaciaConectada;
            sTipoDeMovto = Movimiento;
        }

        public void Reset()
        {
            sFolioMovimiento = ""; 
            sFoliador = "";
            sSKU = "";            
        }
        #region Propiedades 
        public string IdEmpresa
        {
            get { return sIdEmpresa; }
            set { sIdEmpresa = value; }
        }
        public string IdEstado
        {
            get { return sIdEstado; }
            set { sIdEstado = value; }
        }
        public string IdFarmacia
        {
            get { return sIdFarmacia; }
            set { sIdFarmacia = value; }
        }
        public string TipoDeMovimiento
        {
            get { return sTipoDeMovto; }
            set { sTipoDeMovto = value; }
        }
        public string FolioMovimiento
        {
            get { return sFolioMovimiento; } 
            set { sFolioMovimiento = value; }
        }
        public string Foliador
        {
            get 
            {
                if(sTipoDeMovto != "")
                {
                    return sFoliador.Replace(sTipoDeMovto, "");
                }
                else
                {
                    return sFoliador.Replace(sTipoDeMovto, "");
                }
            } 
            set 
            {
                sFoliador = value;
                if(sTipoDeMovto != "")
                {
                    sFoliador = value.Replace(sTipoDeMovto, "");
                }
            }
        }
        public string SKU
        {
            get { return sSKU; }
            set { sSKU = value; }
        }
                
        #endregion Propiedades 
    }
}
