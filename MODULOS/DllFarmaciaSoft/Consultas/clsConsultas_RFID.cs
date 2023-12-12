using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

////using Dll_IMach4; 

namespace DllFarmaciaSoft
{
    public partial class clsConsultas
    {
        #region Registro de Movimientos de RFID
        public DataSet RFID_Movimientos(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Folio";
            string sMsjNoEncontrado = "Folio no encontrado, verifique.";

            IdEmpresa = Fg.PonCeros(IdEmpresa, 3);
            IdEstado = Fg.PonCeros(IdEstado, 2);
            IdFarmacia = Fg.PonCeros(IdFarmacia, 4);
            Folio = Fg.PonCeros(Folio, 10);

            sQuery = sInicio + string.Format("Select * \n" +
                " From vw_RFID__BitacoraMovimientos (NoLock) \n " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' \n",
                IdEmpresa, IdEstado, IdFarmacia, Folio);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet RFID_MovimientosDetalles(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos Detalles del Folio";
            string sMsjNoEncontrado = "Detalles del Folio no encontrados, verifique.";

            IdEmpresa = Fg.PonCeros(IdEmpresa, 3);
            IdEstado = Fg.PonCeros(IdEstado, 2);
            IdFarmacia = Fg.PonCeros(IdFarmacia, 4);
            Folio = Fg.PonCeros(Folio, 10);

            sQuery = sInicio + string.Format("Select TAG, UUID, IdEmpresa, IdEstado, IdFarmacia, Folio, Status  \n" +
                " From vw_RFID__BitacoraMovimientos_Detalles (NoLock) \n " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' \n",
                IdEmpresa, IdEstado, IdFarmacia, Folio);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet RFID_MovimientosDetalles_ListadoTAGS(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos Detalles del Folio";
            string sMsjNoEncontrado = "Detalles del Folio no encontrados, verifique.";

            IdEmpresa = Fg.PonCeros(IdEmpresa, 3);
            IdEstado = Fg.PonCeros(IdEstado, 2);
            IdFarmacia = Fg.PonCeros(IdFarmacia, 4);
            Folio = Fg.PonCeros(Folio, 10);

            sQuery = sInicio + string.Format("Select TAG, 1 as EsBase, 0 as Existe \n" +
                " From vw_RFID__BitacoraMovimientos_Detalles (NoLock) \n " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' \n",
                IdEmpresa, IdEstado, IdFarmacia, Folio);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        #endregion Registro de Movimientos de RFID 
    }
}
