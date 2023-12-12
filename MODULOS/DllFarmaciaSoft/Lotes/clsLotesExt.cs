using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DllFarmaciaSoft;


using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;
namespace DllFarmaciaSoft.Lotes
{
    public class clsLotesExt: clsLotes
    {
        #region Declaracion de variables
        private bool bEsLoteCaducado = false;
        private bool bLoteEnCero = false;
        #endregion Declaracion de variables

        #region Propiedades
        public bool EsLoteCaducado
        {
            get { return bEsLoteCaducado; }
            set { bEsLoteCaducado = value; }
        }

        public bool LoteEnCero
        {
            get { return bLoteEnCero; }
            set { bLoteEnCero = value; }
        }
        #endregion Propiedades

        #region Constructores y Destructor
        public clsLotesExt(string Estado, string Farmacia, int MesesCaducidad)
            : base(Estado, Farmacia, MesesCaducidad)
        {
        }

        public clsLotesExt(string Estado, string Farmacia, int MesesCaducidad, bool BuscarUbicaciones)
            : base(Estado, Farmacia, MesesCaducidad, BuscarUbicaciones)
        {
        }

        public clsLotesExt(string Estado, string Farmacia, int MesesCaducidad, TiposDeInventario TipoDeInventario)
            : base(Estado, Farmacia, MesesCaducidad, TipoDeInventario)
        {
        }

        public clsLotesExt(string Estado, string Farmacia, int MesesCaducidad, TiposDeInventario TipoDeInventario, bool BuscarUbicaciones)
            : base(Estado, Farmacia, MesesCaducidad, TipoDeInventario, BuscarUbicaciones)
        {
        }
        #endregion Constructores y Destructor

        #region Show 
        public void Show(string IdProducto, string CodigoEAN, string SubFarmacia, string ClaveLote)
        {
            base.Show(); 
            //////FrmCapturaLotes f = new FrmCapturaLotes(base.Rows(IdProducto, CodigoEAN, SubFarmacia, ClaveLote), base.DtsSubFarmacias);
        }
        #endregion Show

        #region Funciones y Procedimientos Publicos
        //////public override void Incrementar_Cantidad(string IdProducto, string CodigoEAN, string SubFarmacia, string ClaveLote)
        //////{
        //////    clsLeer leerIncremento = new clsLeer();
        //////    int iCantidad = 0, iExistencia = 0, iMesesCad = 0; 

        //////    leerIncremento.DataRowsClase = base.Rows(IdProducto, CodigoEAN, SubFarmacia, ClaveLote);

        //////    if (leerIncremento.Leer())
        //////    {
        //////        iCantidad = leerIncremento.CampoInt("Cantidad");
        //////        iExistencia = leerIncremento.CampoInt("Existencia");
        //////        iMesesCad = leerIncremento.CampoInt("MesesCad");
        //////        iCantidad++;

        //////        if (iMesesCad > 0)
        //////        {
        //////            if (iCantidad <= iExistencia)
        //////            {
        //////                leerIncremento.GuardarDatos(1, "Cantidad", iCantidad);

        //////                base.IntegrarInformacion(leerIncremento.DataSetClase, IdProducto, CodigoEAN, SubFarmacia, ClaveLote);
        //////                base.Cantidad = leerIncremento.CampoInt("Cantidad");
        //////            }
        //////            else
        //////            {
        //////                bLoteEnCero = true;
        //////            }
        //////        }
        //////        else
        //////        {
        //////            bEsLoteCaducado = true;
        //////        }
        //////    }

        //////}
        #endregion Funciones y Procedimientos Publicos 

        #region Funciones y Procedimientos Privados 
        ////private DataRow[] Rows(string IdProducto, string CodigoEAN)
        ////{
        ////    return 
        ////}

        ////private override DataRow[] Rows(string IdProducto, string CodigoEAN, string IdSubFarmacia, string ClaveLote)
        ////{
        ////    string sSelect = string.Format("1=1");
        ////    string sFiltroConsignacion = "";

        ////    sSelect = string.Format("IdProducto = '{0}' and CodigoEAN = '{1}' {2} ", IdProducto, CodigoEAN, sFiltroConsignacion);
        ////    sSelect += string.Format(" and IdSubFarmacia = '{0}' and ClaveLote = '{1}' ", IdSubFarmacia, ClaveLote);

        ////    return base.DataSetLotes.Tables[0].Select(sSelect);
        ////}

        ////private override void IntegrarInformacion(DataSet Lista, string IdProducto, string CodigoEAN, string IdSubFarmacia, string ClaveLote)
        ////{
        ////    foreach (DataRow dtRow in this.Rows(IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote))
        ////    {
        ////        base.DataSetLotes.Tables[0].Rows.Remove(dtRow);
        ////    }

        ////    // pDtsLotes = dtsPuente;
        ////    base.DataSetLotes.Tables[0].Merge(Lista.Tables[0]);
        ////}
        #endregion Funciones y Procedimientos Privados
    }
}
