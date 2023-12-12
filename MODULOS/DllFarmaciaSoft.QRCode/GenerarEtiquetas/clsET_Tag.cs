using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem.QRCode.Codec;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.QRCode;

using SC_SolutionsSystem.QRCode;
using SC_SolutionsSystem.SistemaOperativo; 

namespace DllFarmaciaSoft.QRCode.GenerarEtiquetas
{
    public class clsET_Tag
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerInformacion;
        clsAyudas ayuda;

        QR_Reporte Impresion; // = new QR_Reporte(); 
        DataSet dtsPosicion = new DataSet();
        DataSet dtsInformacion = new DataSet();
        DataSet dtsReeimpresion = new DataSet();
        string sFolioGenerado = "";

        string sIdEmpresa = "";
        string sIdEstado = "";
        string sIdFarmacia = "";
        string sFolio = "";
        string sTAG = "";

        bool bInformacion = false; 

        public clsET_Tag()
        {
        }

        #region Funciones y Procedimientos Publicos 
        public bool GenerarEtiqueta(string Empresa, string Estado, string Farmacia, string Folio, bool VistaPrevia )
        {
            bool bRegresa = false; 
            sIdEmpresa = Empresa;
            sIdEstado = Estado;
            sIdFarmacia = Farmacia;
            sFolio = Folio;

            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);
            leerInformacion = new clsLeer(ref cnn);

            if (GetInformacion())
            {
                GenerarEtiqueta(VistaPrevia); 
            }

            return bRegresa; 
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        private void GenerarEtiqueta(bool VistaPrevia)
        {
            Impresion = new QR_Reporte();
            Impresion.Reporte = "ET_Tag";
            Impresion.EnviarAImpresora = !VistaPrevia;

            GenerarDetalles();

            Impresion.GenerarXml();
            Impresion.Imprimir(true, false);

        }

        private void GenerarDetalles()
        {

            if (bInformacion)
            {
                leerInformacion.Leer();
                leerInformacion.RegistroActual = 1;

                Impresion.AgregarDetalles(leerInformacion.DataSetClase);

                QR_General.Encoder.Encode(sTAG);
                Impresion.AgregarEtiqueta(QR_General.Encoder.Imagen);
            }
        }

        private bool GetInformacion()
        {
            bool bRegresa = false;
            string sSql = string.Format(
                "Select Distinct E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.Folio, E.Tag, P.ClaveSSA, P.DescripcionClave, P.DescripcionCortaClave, " +
                " T.CodigoEAN, T.IdProducto, P.Descripcion as NombreComercial, T.ClaveLote, convert(varchar(7), F.FechaCaducidad, 120) as Caducidad, T.Cantidad, T.Renglon   " +
                "From RFID_Tags_Enc E (NoLock) " + 
                "Inner Join RFID_Tags_Det T (NoLock) On ( E.IdEmpresa = T.IdEmpresa and E.IdEstado = T.IdEstado and E.IdFarmacia = T.IdFarmacia and E.Folio = T.Folio ) " +
                "Inner Join vw_Productos_CodigoEAN P (NoLock) On ( T.IdProducto = P.IdProducto and T.CodigoEAN = P.CodigoEAN ) " +
                "Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) " +
                "   On ( T.IdEmpresa = F.IdEmpresa and T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia and T.IdProducto = F.IdProducto " + 
                "       and T.CodigoEAN = F.CodigoEAN and T.ClaveLote = F.ClaveLote ) " +
                "Where T.IdEmpresa = '{0}' and T.IdEstado = '{1}' and T.IdFarmacia = '{2}' and T.Folio = '{3}' " +
                "Order by T.Renglon ",
                sIdEmpresa, sIdEstado, sIdFarmacia, sFolio);


            bInformacion = false;
            if (!leerInformacion.Exec("Informacion", sSql))
            {
                ////Error.GrabarError(leerInformacion, "GetCodigoEAN()");
            }
            else
            {
                if (leerInformacion.Leer())
                {
                    bInformacion = true;
                    dtsInformacion = leerInformacion.DataSetClase;
                    sTAG = leerInformacion.Campo("TAG");
                    bRegresa = true; 
                }
            }

            return bRegresa; 
        }
        #endregion Funciones y Procedimientos Privados 
    }
}
