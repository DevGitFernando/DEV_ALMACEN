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
    public class clsLotesUbicacionesItem
    {
        string sIdEstado = "";
        string sIdFarmacia = "";
        string sIdSubFarmacia = "";

        string sIdProducto = "";
        string sCodigoEAN = "";
        string sSKU = "";
        //string sFLO = "";
        string sClaveLote = "";

        int iPasillo = 0;
        int iEstante = 0;
        int iEntrepano = 0;

        int iPasillo_Aux = 0;
        int iEstante_Aux = 0;
        int iEntrepano_Aux = 0;

        int iExistenciaActual = 0;
        int iCantidad = 0;

        bool bItemCerrado = false;

        public clsLotesUbicacionesItem()
        {
        }

        #region Propiedades
        public void Cerrar()
        {
            bItemCerrado = true;
        }

        #region Informacion de Producto
        public string IdEstado
        {
            get { return sIdEstado; }
            set
            {
                if (!bItemCerrado)
                {
                    sIdEstado = value;
                }
            }
        }

        public string IdFarmacia
        {
            get { return sIdFarmacia; }
            set
            {
                if (!bItemCerrado)
                {
                    sIdFarmacia = value;
                }
            }
        }

        public string IdSubFarmacia
        {
            get { return sIdSubFarmacia; }
            set
            {
                if (!bItemCerrado)
                {
                    sIdSubFarmacia = value;
                }
            }
        }

        public string IdProducto
        {
            get { return sIdProducto; }
            set
            {
                if (!bItemCerrado)
                {
                    sIdProducto = value;
                }
            }
        }

        public string CodigoEAN
        {
            get { return sCodigoEAN; }
            set
            {
                if (!bItemCerrado)
                {
                    sCodigoEAN = value;
                }
            }
        }

        public string SKU
        {
            get { return sSKU; }
            set
            {
                if(!bItemCerrado)
                {
                    sSKU = value;
                }
            }
        }

        //public string FLO
        //{
        //    get { return sFLO; }
        //    set
        //    {
        //        if (!bItemCerrado)
        //        {
        //            sFLO = value;
        //        }
        //    }
        //}

        public string ClaveLote
        {
            get { return sClaveLote; }
            set
            {
                if (!bItemCerrado)
                {
                    sClaveLote = value;
                }
            }
        }
        #endregion Informacion de Producto

        #region Informacion de Ubicaciones
        public int Pasillo
        {
            get { return iPasillo; }
            set
            {
                if (!bItemCerrado)
                {
                    iPasillo = value;
                }
            }
        }

        public int Estante
        {
            get { return iEstante; }
            set
            {
                if (!bItemCerrado)
                {
                    iEstante = value;
                }
            }
        }

        public int Entrepano
        {
            get { return iEntrepano; }
            set
            {
                if (!bItemCerrado)
                {
                    iEntrepano = value;
                }
            }
        }

        #region Posicion de Reubicacion
        public int Pasillo_Aux
        {
            get { return iPasillo_Aux; }
            set
            {
                if (!bItemCerrado)
                {
                    iPasillo_Aux = value;
                }
            }
        }

        public int Estante_Aux
        {
            get { return iEstante_Aux; }
            set
            {
                if (!bItemCerrado)
                {
                    iEstante_Aux = value;
                }
            }
        }

        public int Entrepano_Aux
        {
            get { return iEntrepano_Aux; }
            set
            {
                if (!bItemCerrado)
                {
                    iEntrepano_Aux = value;
                }
            }
        }
        #endregion Posicion de Reubicacion

        /// <summary>
        /// Existencia actual en la Posición [ Ubicación actual | Reubicación ]
        /// </summary>
        public int ExistenciaActual
        {
            get { return iExistenciaActual; }
            set
            {
                if (!bItemCerrado)
                {
                    iExistenciaActual = value;
                }
            }
        }

        /// <summary>
        /// Cantidad actual en la Posición [ Ubicación actual | Reubicación ]
        /// </summary>
        public int Cantidad
        {
            get { return iCantidad; }
            set
            {
                if (!bItemCerrado)
                {
                    iCantidad = value;
                }
            }
        }
        #endregion Informacion de Ubicaciones
        #endregion Propiedades
    }

    internal class clsLotesUbicaciones
    {
        #region Declaracion de variables
        private string sIdEstado = DtGeneral.EstadoConectado;
        private string sIdFarmacia = DtGeneral.FarmaciaConectada;

        private string sIdSubFarmacia = "";
        // private string sSubFarmacia = "";

        private string sSKU = "";
        //private string sFLO = "";
        private string sIdProducto = "";
        private string sCodigoEAN = "";
        private string sClaveLote = "";
        private string sDescripcion = "";
        private string sDispensarPor = "";
        private string sContenidoPaquete = "";
        private string sClaveSSA = "";
        private string sClaveSSA_Descripcion = "";
        public int iMesesCaducidad = 0;

        private DateTime dFechaEntrada = DateTime.Now;
        private DateTime dFechaCaducidad = DateTime.Now;
        // private string sStatus = "";
        // private int iTipoCaptura = 0;
        private DateTime dFechaSistema = DateTime.Now;
        private bool bMostrarLotesExistencia_0 = true;

        int iTotalCantidad = 0;
        public bool bPermitirSacarCaducados = false;
        public bool bPermitirCapturaLotesNuevos = false;
        public bool bPermitirCapturaUbicacionesNuevas = false;
        public bool bModificarCaptura = false;
        public bool bEsEntrada = false; // **  
        public bool bEsTransferenciaDeEntrada = false;
        public bool bEsCancelacionCompras = false;
        public bool bEsConsignacion = false;
        public bool bPermitirLotesNuevosConsignacion = true;
        public bool bEsInventarioActivo = false;

        public bool bBloqueaPorInventario = false;
        public bool bEsCaducadudo = false;
        public bool bEsDevolucion = false;
        public int iExistenciaDisponible = 0;

        // EncabezadosManejoLotes tpEncabezados = EncabezadosManejoLotes.Default;
        OrigenManejoLotes tpOrigenManLotes = OrigenManejoLotes.Default;
        // TiposDeInventario tipoDeInventario = TiposDeInventario.Todos;

        // int iExistencia = 0;
        int iCantidad = 0;

        DataSet pDtsLotesUbicacionesRegistradas;
        DataSet pDtsLotes;
        // DataSet pDtsSubFarmacias;
        // DataSet pDtsSubFarmacias_Aux;
        basGenerales Fg = new basGenerales();
        clsConexionSQL pCnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer = new clsLeer();

        private int iPasillo = 0;
        private int iEstante = 0;
        private int iEntrepano = 0;

        public string sPosicionEstandar = "";
        #endregion Declaracion de variables


        #region Constructores y Destructor
        public clsLotesUbicaciones(string Estado, string Farmacia)
        {
            this.sIdEstado = Estado;
            this.sIdFarmacia = Farmacia;
            this.pDtsLotes = clsLotesUbicaciones.PreparaDtsLotesUbicaciones();
        }

        public clsLotesUbicaciones(string Estado, string Farmacia, string SubFarmacia, string SKU, 
            string IdProducto, string CodigoEAN, string ClaveLote, DataSet Ubicaciones)
        {
            this.sIdEstado = Estado;
            this.sIdFarmacia = Farmacia;

            this.sIdSubFarmacia = SubFarmacia;
            this.sIdProducto = IdProducto;
            this.sCodigoEAN = CodigoEAN;
            this.sSKU = SKU;
            this.sClaveLote = ClaveLote;
            this.pDtsLotes = Ubicaciones;
            //this.sFLO = FLO;
        }
        #endregion Constructor y Destructor

        #region Propidades Publicas
        public DataSet DataSetLotesUbicaciones
        {
            get { return pDtsLotes; }
            set { pDtsLotes = value; }
        }

        public DataSet UbicacionesRegistradas
        {
            set { pDtsLotesUbicacionesRegistradas = value; }
        }

        public string SKU
        {
            get { return sSKU; }
            set { sSKU = value; }
        }

        //public string FLO
        //{
        //    get { return sFLO; }
        //    set { sFLO = value; }
        //}

        public string Codigo
        {
            get { return Fg.PonCeros(sIdProducto, 8); }
            set { sIdProducto = value; }
        }

        public string CodigoEAN
        {
            get { return sCodigoEAN; }
            set { sCodigoEAN = value; }
        }

        public string ClaveLote
        {
            get { return sClaveLote; }
            set { sClaveLote = value; }
        }

        public string DescripcionProducto
        {
            get { return sDescripcion; }
            set { sDescripcion = value; }
        }

        public string DispensarPor
        {
            get { return sDispensarPor; }
            set { sDispensarPor = value; }
        }

        public string ContenidoPaquete
        {
            get { return sContenidoPaquete; }
            set { sContenidoPaquete = value; }
        }

        public string ClaveSSA
        {
            get { return sClaveSSA; }
            set { sClaveSSA = value; }
        }

        public string ClaveSSA_Descripcion
        {
            get { return sClaveSSA_Descripcion; }
            set { sClaveSSA_Descripcion = value; }
        }

        public int Cantidad
        {
            get { return iCantidad; }
            set { iCantidad = value; }
        }

        public int CantidadTotal
        {
            get
            {
                // Lotes(); // Oblicar a totalizar los Lotes 
                return iTotalCantidad;
            }
        }

        public bool ModificarCantidades
        {
            get { return bModificarCaptura; }
            set { bModificarCaptura = value; }
        }

        public bool EsEntrada
        {
            get { return bEsEntrada; }
            set { bEsEntrada = value; }
        }
        #endregion Propidades Publicas

        #region Propidades Publicas Ubicaciones
        public bool CapturarUbicaciones
        {
            get { return bPermitirCapturaUbicacionesNuevas; }
            set { bPermitirCapturaUbicacionesNuevas = value; }
        }

        public int Pasillo
        {
            get { return iPasillo; }
            set { iPasillo = value; }
        }

        public int Estante
        {
            get { return iEstante; }
            set { iEstante = value; }
        }

        public int Entrepano
        {
            get { return iEntrepano; }
            set { iEntrepano = value; }
        }        
        #endregion Propidades Publicas Ubicaciones

        #region Funciones y Procedimientos Static
        private static Type GetType(TypeCode TipoDato)
        {
            return Type.GetType("System." + TipoDato.ToString());
        }

        public static DataSet PreparaDtsLotesUbicaciones()
        {
            // Nombres de columnas no mayores de 10 caracteres 
            DataSet dts = new DataSet();
            DataTable dtLote = new DataTable("LotesUbicaciones");

            dtLote.Columns.Add("IdSubFarmacia", GetType(TypeCode.String));

            dtLote.Columns.Add("IdProducto", GetType(TypeCode.String));
            dtLote.Columns.Add("CodigoEAN", GetType(TypeCode.String));
            dtLote.Columns.Add("SKU", GetType(TypeCode.String));    //20200601.1748            
            dtLote.Columns.Add("ClaveLote", GetType(TypeCode.String));

            dtLote.Columns.Add("IdPasillo", GetType(TypeCode.Int32));
            dtLote.Columns.Add("IdEstante", GetType(TypeCode.Int32));
            dtLote.Columns.Add("IdEntrepano", GetType(TypeCode.Int32));

            dtLote.Columns.Add("Status", GetType(TypeCode.String));
            dtLote.Columns.Add("Existencia", GetType(TypeCode.Int32));
            dtLote.Columns.Add("ExistenciaDisponible", GetType(TypeCode.Int32));
            dtLote.Columns.Add("Cantidad", GetType(TypeCode.Int32));

            //dtLote.Columns.Add("FLO", GetType(TypeCode.String));    //20231122.1119FAV

            dts.Tables.Add(dtLote);

            return dts.Clone();
        }

        #endregion Funciones y Procedimientos Static

        #region Manejo de Lotes
        public void Show()
        {
            DataRow[] mysRows = this.Rows(Registros.Iguales);
            FrmCaptura_LotesUbicaciones f = new FrmCaptura_LotesUbicaciones(mysRows);

            f.dtsLotesUbicacionesRegistradas = pDtsLotesUbicacionesRegistradas;
            f.sIdEstado = this.sIdEstado;
            f.sIdFarmacia = this.sIdFarmacia;
            f.sIdSubFarmacia = this.sIdSubFarmacia;

            f.sSKU = sSKU;
            f.sIdProducto = sIdProducto;
            f.sDescripcion = sDescripcion;
            f.sCodigoEAN = sCodigoEAN;
            f.sClaveLote = sClaveLote;
            f.sDispensarPor = sDispensarPor;
            f.sContenidoPaquete = sContenidoPaquete;
            f.sClaveSSA = sClaveSSA;
            f.sClaveSSA_Descripcion = sClaveSSA_Descripcion;
            f.bPermitirCapturaUbicacionesNuevas = bPermitirCapturaUbicacionesNuevas;
            f.dtsLotesUbicaciones = this.pDtsLotes;

            f.bPermitirCapturaUbicacionesNuevas = bPermitirCapturaUbicacionesNuevas;
            f.bModificarCaptura = bModificarCaptura;
            f.bEsEntrada = bEsEntrada;
            f.bEsTransferenciaDeEntrada = bEsTransferenciaDeEntrada;
            f.bEsCancelacionCompras = bEsCancelacionCompras;
            f.bEsConsignacion = bEsConsignacion;
            f.bPermitirLotesNuevosConsignacion = bPermitirLotesNuevosConsignacion;
            f.bEsInventarioActivo = bEsInventarioActivo;
            f.bBloqueaPorInventario = bBloqueaPorInventario;
            f.bEsCaducadudo = bEsCaducadudo;
            f.bEsDevolucion = bEsDevolucion;
            f.iExistenciaDisponible = iExistenciaDisponible;

            //f.sFLO = sFLO;

            f.sPosicionEstandar = ""; 
            if (GnFarmacia.ManejaUbicacionesEstandar)
            {
                f.sPosicionEstandar = sPosicionEstandar;
            }


            f.ShowDialog();

            this.iCantidad = f.iTotalCantidad;
            this.iTotalCantidad = this.iCantidad;
            this.IntegrarInformacion(f.dtsLotesUbicaciones);
        }

        public void AddUbicaciones(DataSet Lista)
        {
            if (pDtsLotes == null)
            {
                pDtsLotes = PreparaDtsLotesUbicaciones();
            }

            try
            {
                // Agrega los nuevos lotes a la lista de Lotes 
                pDtsLotes.Tables[0].Merge(Lista.Tables[0]);
            }
            catch (Exception ex1)
            {
                ex1.Source = ex1.Source; 
            }
        }

        /// <summary>
        /// Vacia la lista de Lotes 
        /// </summary>
        public void RemoveUbicaciones()
        {
            pDtsLotes = PreparaDtsLotesUbicaciones();
        }

        /// <summary>
        /// Quita de la lista los lotes que cumplan con el criterio 
        /// </summary>
        /// <param name="IdProducto">Producto del cual se quitaran los lotes-ubicaciones</param>
        /// <param name="CodigoEAN">CodigoEAN del cual se quitaran los lotes-ubicaciones</param>
        /// <param name="ClaveLote">Clave del Lote del cual se quitaran los lotes-ubicaciones</param>
        public void RemoveUbicaciones(string IdProducto, string CodigoEAN, string ClaveLote)
        {
            string sFiltro = string.Format("IdProducto = '{0}' and CodigoEAN = '{1}' and ClaveLote = '{2}' ", IdProducto, CodigoEAN, ClaveLote);
            foreach (DataRow dtRow in pDtsLotes.Tables[0].Select(sFiltro))
            {
                pDtsLotes.Tables[0].Rows.Remove(dtRow);
            }
        }

        private DataRow[] Rows(Registros Tipo)
        {
            string sSelect = string.Format("1=1");
            string sFiltroConsignacion = "";

            if (bEsConsignacion)
            {
                sFiltroConsignacion = string.Format(" and ClaveLote like '%*%' ");
            }

            // Carga inicial de Lotes 
            if (Tipo == Registros.Iguales)
            {
                sSelect = string.Format(" SKU = '{0}' and IdSubFarmacia = '{1}' and IdProducto = '{2}' and CodigoEAN = '{3}' and ClaveLote = '{4}' {5} ",
                    sSKU, sIdSubFarmacia, sIdProducto, sCodigoEAN, sClaveLote, sFiltroConsignacion );

                //// Mostrar solo Lotes con Existencia en Sistema 
                if (!bMostrarLotesExistencia_0)
                {
                    switch (tpOrigenManLotes)
                    {
                        case OrigenManejoLotes.Ventas_Dispensacion:
                        case OrigenManejoLotes.Ventas_PublicoGeneral:
                            sSelect += " and Existencia > 0 ";
                            break;
                        default:
                            break;
                    }
                }
            }

            // Carga Retorno Proceso 
            if (Tipo == Registros.Diferentes)
            {
                sSelect = string.Format(" SKU = '{0}' and IdSubFarmacia = '{1}' and IdProducto <> '{2}' and CodigoEAN <> '{3}' and ClaveLote <> '{4}' ",
                    sSKU, sIdSubFarmacia, sIdProducto, sCodigoEAN, sClaveLote);
            }

            return pDtsLotes.Tables[0].Select(sSelect);
        }

        private void IntegrarInformacion(DataSet Lista)
        {
            foreach (DataRow dtRow in this.Rows(Registros.Iguales))
            {
                pDtsLotes.Tables[0].Rows.Remove(dtRow);
            }

            // pDtsLotes = dtsPuente;
            pDtsLotes.Tables[0].Merge(Lista.Tables[0]);
        }

        public clsLotesUbicacionesItem[] Ubicaciones()
        {
            return Ubicaciones("1=1", false, 0);
        }

        public clsLotesUbicacionesItem[] Ubicaciones(string CodigoEAN, string ClaveLote)
        {
            return Ubicaciones(string.Format("CodigoEAN = '{0}' and ClaveLote = '{1}' ",
                CodigoEAN, ClaveLote), true, 0);
        }

        public clsLotesUbicacionesItem[] Ubicaciones(string CodigoEAN, string ClaveLote, bool Cantidad_Mayor_0)
        {
            return Ubicaciones(string.Format("CodigoEAN = '{0}' and ClaveLote = '{1}' ", CodigoEAN, ClaveLote), Cantidad_Mayor_0, 0);
        }

        public clsLotesUbicacionesItem[] Ubicaciones( string IdProducto, string CodigoEAN, string ClaveLote, string IdSubFarmacia )
        {
            return Ubicaciones("", IdProducto, CodigoEAN, ClaveLote, IdSubFarmacia);
        }

        public clsLotesUbicacionesItem[] Ubicaciones( string SKU, string IdProducto, string CodigoEAN, string ClaveLote, string IdSubFarmacia)
        {
            string sFiltro_SKU = SKU != "" ? string.Format(" and SKU = '{0}' ", SKU) : " ";

            return Ubicaciones(string.Format("IdProducto = '{0}' and CodigoEAN = '{1}' and ClaveLote = '{2}' And IdSubFarmacia = '{3}' {4}",
                IdProducto, CodigoEAN, ClaveLote, IdSubFarmacia, sFiltro_SKU), true, 0);
        }

        public clsLotesUbicacionesItem[] Ubicaciones( string IdProducto, string CodigoEAN, string ClaveLote, string IdSubFarmacia, bool Cantidad_Mayor_0 )
        {
            return Ubicaciones("", IdProducto, CodigoEAN, ClaveLote, IdSubFarmacia, Cantidad_Mayor_0); 
        }

        public clsLotesUbicacionesItem[] Ubicaciones(string SKU, string IdProducto, string CodigoEAN, string ClaveLote, string IdSubFarmacia, bool Cantidad_Mayor_0)
        {
            string sFiltro_SKU = SKU != "" ? string.Format(" and SKU = '{0}' ", SKU) : " ";

            return Ubicaciones(string.Format("IdProducto = '{0}' and CodigoEAN = '{1}' and ClaveLote = '{2}' And IdSubFarmacia = '{3}' {4}",
                IdProducto, CodigoEAN, ClaveLote, IdSubFarmacia, sFiltro_SKU), Cantidad_Mayor_0, 0);
        }

        private clsLotesUbicacionesItem[] Ubicaciones(string Filtro, bool Cantidad_Mayor_0, int Valor)
        {
            List<clsLotesUbicacionesItem> pListaLote = new List<clsLotesUbicacionesItem>();
            DataSet dtsEx = PreparaDtsLotesUbicaciones();
            iTotalCantidad = 0;

            Filtro += Cantidad_Mayor_0 ? " and Cantidad > 0 " : "";
            foreach (DataRow dtRow in pDtsLotes.Tables[0].Select(Filtro))
            {
                dtsEx.Tables[0].Rows.Add(dtRow.ItemArray);
            }

            leer.DataSetClase = dtsEx;
            while (leer.Leer())
            {
                clsLotesUbicacionesItem myUbicacion = new clsLotesUbicacionesItem();

                myUbicacion.IdEstado = leer.Campo("IdEstado");
                myUbicacion.IdFarmacia = leer.Campo("IdFarmacia");
                myUbicacion.IdSubFarmacia = leer.Campo("IdSubFarmacia");

                myUbicacion.IdProducto = leer.Campo("IdProducto");
                myUbicacion.CodigoEAN = leer.Campo("CodigoEAN");
                myUbicacion.SKU = leer.Campo("SKU");                
                myUbicacion.ClaveLote = leer.Campo("ClaveLote");

                myUbicacion.Pasillo = leer.CampoInt("IdPasillo");
                myUbicacion.Estante = leer.CampoInt("IdEstante");
                myUbicacion.Entrepano = leer.CampoInt("IdEntrepano");

                myUbicacion.ExistenciaActual = leer.CampoInt("Existencia");
                myUbicacion.Cantidad = leer.CampoInt("Cantidad");

                //myUbicacion.FLO = leer.Campo("FLO");    //20231122.1119FAV

                myUbicacion.Cerrar();

                iTotalCantidad += myUbicacion.Cantidad;
                pListaLote.Add(myUbicacion);
            }

            return pListaLote.ToArray();
        }

        #endregion Manejo de Lotes
    }
}
