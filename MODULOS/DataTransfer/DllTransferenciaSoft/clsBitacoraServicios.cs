using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;

namespace DllTransferenciaSoft
{
    internal class clsBitacoraServicios
    {
        clsConexionSQL cnn;
        clsDatosConexion datosCnn;
        clsLeer leer;

        private string sIdMovimiento = "";
        private DateTime dFechaMovto = DateTime.Now;
        private string sIdTipoArchivo = "";
        private string sIdArchivo = "";
        private string sMotivo = "";
        private string sDescripcion = "";
        private TipoServicio tpBitacora = TipoServicio.Ninguno;


        public clsBitacoraServicios(clsDatosConexion DatosCnn, TipoServicio Bitacora)
        {
            this.datosCnn = DatosCnn;
            this.cnn = new clsConexionSQL(datosCnn);
            this.leer = new clsLeer(ref cnn);
            this.tpBitacora = Bitacora;
        }

        #region Propiedades 
        public string IdMovimiento
        {
            get { return sIdMovimiento; }
            set { sIdMovimiento = value; }
        }

        public DateTime FechaMovto
        {
            get { return dFechaMovto; }
            set { dFechaMovto = value; }
        }

        public string IdTipoArchivo
        {
            get { return sIdTipoArchivo; }
            set { sIdTipoArchivo = value; }
        }

        public string IdArchivo
        {
            get { return sIdArchivo; }
            set { sIdArchivo = value; }
        }

        public string Motivo
        {
            get { return sMotivo; }
            set { sMotivo = value; }
        }

        public string Descripcion
        {
            get { return sDescripcion; }
            set { sDescripcion = value; }
        }

        #endregion Propiedades

        public bool GrabarMovto()
        {
            bool bRegresa = true;
            string sSql = ""; // "Exec spp_CFG_Bitacora '{0}', '{1}', '{2}'  ";
            string sTipo = "";

            if (tpBitacora == TipoServicio.Cliente)
                sTipo = " spp_CFGC_Bitacora_Movimientos ";
            else if (tpBitacora == TipoServicio.OficinaCentral)
                sTipo = " spp_CFGS_Bitacora_Movimientos ";


            sSql = string.Format("Exec {0} '{1}', '{2}', '{3}' ", sTipo, sIdTipoArchivo, sIdArchivo, sMotivo, sDescripcion );
            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                leer.Leer();
                sIdMovimiento = leer.Campo("Folio");
                dFechaMovto = leer.CampoFecha("Fecha");

            }

            return bRegresa; 
        }

    }
}
