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
    public enum Registros
    {
        Todos = 0, Iguales = 1, Diferentes = 2
    }

    public enum enumLotes
    {
        Producto = 1, Producto_EAN = 2, Todos = 3
    }

    /// <summary>
    /// Semaforos e Indicadores 
    /// </summary>
    internal static class IndicadoresLotes
    {
        // Semaroforos e indicadores 
        public static Color colorDefault = Color.White;
        public static Color colorCaducados = Color.Red;
        public static Color colorPrecaucion = Color.Yellow;
        public static Color colorStatusOk = Color.Green;
        public static Color colorBloqueaCaducados = Color.BurlyWood;
        public static Color colorSalidaCaducados = Color.LightGray;
        public static Color colorConsignacion = Color.LightSteelBlue;
        public static Color colorBloqueaVenta_ExisteConsignacion = Color.Khaki;
        public static Color colorBloqueaVenta_NoExisteCuadroBasico = Color.Khaki; 
    }

    public class clsLotes_ItemUUID
    {
        private string sUUID = "";
        private string sIdProducto = "";
        private string sCodigoEAN = "";
        private string sClaveLote = "";
        private string sIdSubFarmacia = "";

        basGenerales Fg = new basGenerales(); 

        #region Constructores y Destructor
        public clsLotes_ItemUUID()
        { 
        }
        #endregion Constructores y Destructor

        public string UUID
        {
            get { return sUUID; }
            set { sUUID = value; }
        }

        public string IdSubFarmacia
        {
            get { return Fg.PonCeros(sIdSubFarmacia, 3); }
            set { sIdSubFarmacia = value; }
        }

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
    }

    public class clsLotes
    {
        ////private enum Registros
        ////{
        ////    Todos = 0, Iguales = 1, Diferentes = 2
        ////}

        ////private enum enumLotes
        ////{
        ////    Producto = 1, Producto_EAN = 2, Todos = 3
        ////}

        #region Declaracion de variables 
        clsLotesUbicaciones LotesConUbicaciones;
        clsLotes_Reubicaciones LotesReubicacion;

        private string sIdEmpresa = DtGeneral.EmpresaConectada; 
        private string sIdEstado = DtGeneral.EstadoConectado;
        private string sIdFarmacia = DtGeneral.FarmaciaConectada;

        private string sIdSubFarmacia = "";
        private string sSubFarmacia = "";

        private string sIdCliente = "";
        private string sIdSubCliente = "";
        private string sIdPrograma = "";
        private string sIdSubPrograma = "";

        private bool bProgramaSubPrograma_ForzarCapturaEnMultiplosDeCajas = false;

        private string sIdProducto = "";
        private string sCodigoEAN = "";
        private string sSKU = "";
        //private string sFLO = "";
        private string sClaveLote = "";
        private string sDescripcion = "";
        private DateTime dFechaEntrada = DateTime.Now;
        private DateTime dFechaCaducidad = DateTime.Now;
        private string sStatus = "";
        private int iMesesCaducidad = 12;
        private int iTipoCaptura = 0;
        private bool bEsTransferenciaDeEntrada = false;
        private DateTime dFechaSistema = DateTime.Now;
        private bool bPermitirSacarCaducados = false;
        private bool bMostrarLotesExistencia_0 = true; 

        int iTotalCantidad = 0;
        bool bPermitirCapturaLotesNuevos = false;
        bool bModificarCaptura = false;
        //bool bCapturandoLotes = false;
        bool bEsEntrada = false;
        bool bEsCancelacionCompras = false;
        bool bEsConsignacion = false;
        bool bPermitirLotesNuevosConsignacion = true;
        bool bEsInventarioActivo = false;
        bool bModificaCaducidades = false;

        bool bEsReubicacion = false;
        bool bBuscarUbicaciones = true; 

        EncabezadosManejoLotes tpEncabezados = EncabezadosManejoLotes.Default;
        OrigenManejoLotes tpOrigenManLotes = OrigenManejoLotes.Default;
        TiposDeInventario tipoDeInventario = TiposDeInventario.Todos;
        TiposDeSubFarmacia tipoDeSubFarmacia = TiposDeSubFarmacia.Todos; 

        int iExistencia = 0;
        int iCantidad = 0;

        DataSet pDtsLotes;
        // DataSet pDtsLotesUbicaciones;
        DataSet pDtsLotesUbicacionesRegistradas; 
        DataSet pDtsSubFarmacias;
        DataSet pDtsSubFarmacias_Aux; 
        basGenerales Fg = new basGenerales();
        clsConexionSQL pCnn = new clsConexionSQL(General.DatosConexion); 
        clsLeer leer = new clsLeer();

        //  Variable para posiciones estandar
        public string sPosicionEstandar = "";



        ////Codificacion 
        DataSet dtsUUIDS;
        Dictionary<string, clsLotes_ItemUUID> lst_UUID = new Dictionary<string, clsLotes_ItemUUID>(); 
        #endregion Declaracion de variables

        #region Constructores y Destructor 
        public clsLotes(string Estado, string Farmacia, int MesesCaducidad)
        : this(Estado, Farmacia, MesesCaducidad, TiposDeInventario.Todos, true)
        {
            ////this.sIdEstado = Estado;
            ////this.sIdFarmacia = Farmacia;
            ////this.iMesesCaducidad = MesesCaducidad;
            ////this.bBuscarUbicaciones = true; 
            ////this.ObtenerSubFarmacias(TiposDeInventario.Todos);
            ////this.InicializarUbicaciones();
        }

        public clsLotes(string Estado, string Farmacia, int MesesCaducidad, bool BuscarUbicaciones)
        : this(Estado, Farmacia, MesesCaducidad, TiposDeInventario.Todos, BuscarUbicaciones)
        {
            ////this.sIdEstado = Estado;
            ////this.sIdFarmacia = Farmacia;
            ////this.iMesesCaducidad = MesesCaducidad;
            ////this.bBuscarUbicaciones = false;
            ////this.ObtenerSubFarmacias(TiposDeInventario.Todos);
            ////this.InicializarUbicaciones();
        }

        public clsLotes(string Estado, string Farmacia, int MesesCaducidad, TiposDeInventario TipoDeInventario)
            : this(Estado, Farmacia, MesesCaducidad, TipoDeInventario, true)
        {
            ////this.sIdEstado = Estado;
            ////this.sIdFarmacia = Farmacia;
            ////this.iMesesCaducidad = MesesCaducidad;
            ////this.bBuscarUbicaciones = true; 
            ////this.ObtenerSubFarmacias(TipoDeInventario);
            ////this.InicializarUbicaciones(); 
        }

        public clsLotes(string Estado, string Farmacia, int MesesCaducidad, TiposDeInventario TipoDeInventario, TiposDeSubFarmacia TipoDeSubFarmacia)
            : this(Estado, Farmacia, MesesCaducidad, TipoDeInventario, TipoDeSubFarmacia, true)
        {
        }

        public clsLotes(string Estado, string Farmacia, int MesesCaducidad, TiposDeInventario TipoDeInventario, TiposDeSubFarmacia TipoDeSubFarmacia, string IdSubFarmacia)
            : this(Estado, Farmacia, MesesCaducidad, TipoDeInventario, TipoDeSubFarmacia, IdSubFarmacia, true)
        {
        }

        public clsLotes(string Estado, string Farmacia, int MesesCaducidad, TiposDeInventario TipoDeInventario, bool BuscarUbicaciones)
            :this(Estado, Farmacia, MesesCaducidad, TipoDeInventario, TiposDeSubFarmacia.Todos, BuscarUbicaciones) 
        {

        }

        public clsLotes(string Estado, string Farmacia, int MesesCaducidad, TiposDeInventario TipoDeInventario, TiposDeSubFarmacia TipoDeSubFarmacia, bool BuscarUbicaciones)
            : this(Estado, Farmacia, MesesCaducidad, TipoDeInventario, TipoDeSubFarmacia, "", BuscarUbicaciones) 
        {
        }

        public clsLotes(string Estado, string Farmacia, int MesesCaducidad, 
            TiposDeInventario TipoDeInventario, TiposDeSubFarmacia TipoDeSubFarmacia, string IdSubFarmacia, bool BuscarUbicaciones)
        {
            this.sIdEstado = Estado;
            this.sIdFarmacia = Farmacia;
            this.iMesesCaducidad = MesesCaducidad;
            this.bBuscarUbicaciones = BuscarUbicaciones;
            this.ObtenerSubFarmacias(TipoDeInventario, TipoDeSubFarmacia, IdSubFarmacia);
            this.InicializarUbicaciones();

            SubFarmacias.Iniciar();
        }

        private void InicializarUbicaciones()
        {
            //// Evitar errores en el modulo de Compras 
            if (pDtsLotes == null) 
            {
                pDtsLotes = PreparaDtsLotes();
            }

            if (GnFarmacia.ManejaUbicaciones) 
            {
                LotesConUbicaciones = new clsLotesUbicaciones(sIdEstado, sIdFarmacia); 
                LotesReubicacion = new clsLotes_Reubicaciones(sIdEstado, sIdFarmacia); 
                CargarUbicacionesRegistradas(); 
            }
            else
            {
                LotesConUbicaciones = new clsLotesUbicaciones("", ""); 
                LotesReubicacion = new clsLotes_Reubicaciones("", ""); 
                pDtsLotesUbicacionesRegistradas = new DataSet(); 
            }

            // pDtsLotesUbicaciones = clsLotesUbicaciones.PreparaDtsLotesUbicaciones(); 

        }
        #endregion Constructor y Destructor

        #region Propiedades publicas 
        public string IdCliente
        {
            get { return sIdCliente; }
            set { sIdCliente = Fg.PonCeros(value, 4); }
        }

        public string IdSubCliente
        {
            get { return sIdSubCliente; }
            set { sIdSubCliente = Fg.PonCeros(value, 4); }
        }

        public string IdPrograma
        {
            get { return sIdPrograma; }
            set { sIdPrograma = Fg.PonCeros(value, 4); }
        }

        public string IdSubPrograma
        {
            get { return sIdSubPrograma; }
            set { sIdSubPrograma = Fg.PonCeros(value, 4); }
        }

        public bool ProgramaSubPrograma_ForzarCapturaEnMultiplosDeCajas
        {
            get { return bProgramaSubPrograma_ForzarCapturaEnMultiplosDeCajas; }
            set { bProgramaSubPrograma_ForzarCapturaEnMultiplosDeCajas = value; } 
        }

        public DataSet DataSetLotes
        {
            get { return pDtsLotes; } 
        }

        public DataSet DtsSubFarmacias
        {
            get { return pDtsSubFarmacias;  }
        }

        public DataSet DataSetUbicaciones
        {
            get { return LotesConUbicaciones.DataSetLotesUbicaciones; }
            set { LotesConUbicaciones.DataSetLotesUbicaciones = value; }
        }

        public DataSet DataSetLotes_Destinos
        {
            get { return LotesReubicacion.DataSetLotes_Destinos; }
            set { LotesReubicacion.DataSetLotes_Destinos = value; }
        }

        public string Descripcion
        {
            get { return sDescripcion; } 
            set { sDescripcion = value; } 
        }

        public string IdSubFarmacia
        {
            get { return Fg.PonCeros(sIdSubFarmacia, 3); }
            set { sIdSubFarmacia = value; }
        }

        public string SubFarmacia
        {
            get { return sSubFarmacia; }
            set { sSubFarmacia = value; }
        }

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

        public string ClaveLote
        {
            get { return sClaveLote; }
            set { sClaveLote = value; }
        }

        public DateTime FechaEntrada
        {
            get { return dFechaEntrada; }
            set { dFechaEntrada = value; } 
        }

        public DateTime FechaCaducidad
        {
            get { return dFechaCaducidad; }
            set { dFechaCaducidad = value; }
        }

        public string Status
        {
            get { return sStatus; }
            set { sStatus = value; }
        }

        public int Existencia
        {
            get { return iExistencia; }
            set { iExistencia = value; } 
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
                Lotes(); // Obligar a totalizar los Lotes 
                return iTotalCantidad; 
            }
        }

        public bool CapturarLotes
        {
            get { return bPermitirCapturaLotesNuevos; }
            set { bPermitirCapturaLotesNuevos = value; }
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

        public bool EsConsignacion
        {
            get { return bEsConsignacion; }
            set { bEsConsignacion = value; }
        }

        public bool PermitirLotesNuevosConsignacion
        {
            get { return bPermitirLotesNuevosConsignacion; }
            set { bPermitirLotesNuevosConsignacion = value; } 
        }

        public bool EsInventarioActivo
        {
            get { return bEsInventarioActivo; }
            set { bEsInventarioActivo = value; }
        }

        public bool ModificaCaducidades
        {
            get { return bModificaCaducidades; }
            set { bModificaCaducidades = value; }
        }

        public int MesesDeCaducidad
        {
            get { return iMesesCaducidad; }
            //set { iMesesCaducidad = value; }
        }

        public int TipoCaptura
        {
            get { return iTipoCaptura; }
            set { iTipoCaptura = value; }
        }

        public bool EsTransferenciaDeEntrada
        {
            get { return bEsTransferenciaDeEntrada; }
            set { bEsTransferenciaDeEntrada = value; }
        }

        public bool EsCancelacionCompras
        {
            get { return bEsCancelacionCompras; }
            set { bEsCancelacionCompras = value; }
        }

        public EncabezadosManejoLotes Encabezados
        {
            get { return tpEncabezados; }
            set { tpEncabezados = value; }
        }

        public DateTime FechaDeSistema
        {
            get { return dFechaSistema; }
            set { dFechaSistema = value; }
        }

        public OrigenManejoLotes ManejoLotes
        {
            get { return tpOrigenManLotes; }
            set { tpOrigenManLotes = value; }
        }

        public TiposDeInventario TipoDeInventario
        {
            get { return tipoDeInventario; }
            set 
            { 
                tipoDeInventario = value;
                PrepararSubFarmacias(); 
            }
        }

        public bool PermitirSalidaCaducados
        {
            get { return bPermitirSacarCaducados; }
            set { bPermitirSacarCaducados = value; }
        }

        public bool MostrarLotesExistencia_0
        {
            get { return bMostrarLotesExistencia_0; }
            set { bMostrarLotesExistencia_0 = value; }
        }

        public bool EsReubicacion 
        {
            get { return bEsReubicacion; }
            set { bEsReubicacion = value; }
        }
        #endregion Propiedades publicas

        #region Funciones y Procedimientos Privados 
        private void PrepararSubFarmacias()
        {
            string sFiltroConsignacion = ""; 

            switch (tipoDeInventario)
            {
                case TiposDeInventario.Todos:
                    sFiltroConsignacion = " ";
                    break;

                case TiposDeInventario.Venta:
                    sFiltroConsignacion = " EsConsignacion = 0 ";
                    break;

                case TiposDeInventario.Consignacion:
                    sFiltroConsignacion = " EsConsignacion = 1 ";
                    break;
            }

            leer.DataSetClase = pDtsSubFarmacias_Aux; 
            DataRow []dtSubFarmacias = leer.DataTableClase.Select(sFiltroConsignacion);  
            leer.DataRowsClase = dtSubFarmacias; 
            pDtsSubFarmacias = leer.DataSetClase; 
        }

        private void ObtenerSubFarmacias(TiposDeInventario TipoDeInventario, TiposDeSubFarmacia TipoDeSubFarmacia, string IdSubFarmacia)
        {
            leer = new clsLeer(ref pCnn);
            string sSql = ""; 
            string sFiltroConsignacion = "";
            string sFiltroSubFarmacia = ""; 

            tipoDeInventario = TipoDeInventario;
            tipoDeSubFarmacia = TipoDeSubFarmacia;

            switch (tipoDeInventario)
            {
                case TiposDeInventario.Todos: 
                    sFiltroConsignacion = " ";
                    break;

                case TiposDeInventario.Venta:
                    sFiltroConsignacion = " and EsConsignacion = 0 ";
                    break;

                case TiposDeInventario.Consignacion:
                    sFiltroConsignacion = " and EsConsignacion = 1 ";

                    if (tipoDeSubFarmacia == TiposDeSubFarmacia.Consignacion) 
                    {
                        sFiltroConsignacion += " and EmulaVenta = 0 ";
                    }

                    if (tipoDeSubFarmacia == TiposDeSubFarmacia.ConsignacionEmulaVenta) 
                    {
                        sFiltroConsignacion += " and EmulaVenta = 1 ";
                    }

                    if ( IdSubFarmacia != "" )
                    {
                        sFiltroSubFarmacia = string.Format(" and IdSubFarmacia = '{0}' ", IdSubFarmacia); 
                    }

                    break;
            }

            sSql = string.Format("Select IdEstado, IdFarmacia, IdSubFarmacia, SubFarmacia, EsConsignacion, EmulaVenta " +
                    " From vw_Farmacias_SubFarmacias (NoLock) " + 
                    " Where IdEstado = '{0}' and IdFarmacia = '{1}' {2} {3} ", 
                    sIdEstado, sIdFarmacia, sFiltroConsignacion, sFiltroSubFarmacia); 


            if (leer.Exec("Farmacias_SubFarmacias", sSql))
            {
                pDtsSubFarmacias = leer.DataSetClase;
                pDtsSubFarmacias_Aux = leer.DataSetClase; 
            }
        }
        #endregion Funciones y Procedimientos Privados

        #region Funciones y Procedimientos Static 
        private static Type GetType(TypeCode TipoDato)
        {
            return Type.GetType("System." + TipoDato.ToString());
        }

        public static DataSet PreparaDtsClaves()
        {
            // Nombres de columnas no mayores de 10 caracteres 
            DataSet dts = new DataSet();
            DataTable dtClave = new DataTable("Claves");

            dtClave.Columns.Add("ClaveSSA", GetType(TypeCode.String)); 
            dtClave.Columns.Add("IdClaveSSA", GetType(TypeCode.String)); 
            dtClave.Columns.Add("Descripcion", GetType(TypeCode.String)); 
            dtClave.Columns.Add("Cantidad", GetType(TypeCode.Int32)); 
            dts.Tables.Add(dtClave); 

            return dts.Clone();
        }

        public static DataSet PreparaDtsClavesCajas()
        {
            // Nombres de columnas no mayores de 10 caracteres 
            DataSet dts = new DataSet();
            DataTable dtClave = new DataTable("Claves");

            dtClave.Columns.Add("ClaveSSA", GetType(TypeCode.String));
            dtClave.Columns.Add("IdClaveSSA", GetType(TypeCode.String));
            dtClave.Columns.Add("Descripcion", GetType(TypeCode.String));
            dtClave.Columns.Add("ContenidoPaquete", GetType(TypeCode.Int32));
            dtClave.Columns.Add("Cajas", GetType(TypeCode.Int32));
            dtClave.Columns.Add("Cantidad", GetType(TypeCode.Int32));
            dts.Tables.Add(dtClave);

            return dts.Clone();
        }

        public static DataSet PreparaDtsLotes()
        {
            // Nombres de columnas no mayores de 10 caracteres 
            DataSet dts = new DataSet();
            DataTable dtLote = new DataTable("Lotes");

            dtLote.Columns.Add("IdSubFarmacia", GetType(TypeCode.String));
            dtLote.Columns.Add("SubFarmacia", GetType(TypeCode.String));

            dtLote.Columns.Add("IdProducto", GetType(TypeCode.String));
            dtLote.Columns.Add("CodigoEAN", GetType(TypeCode.String)); 
            dtLote.Columns.Add("SKU", GetType(TypeCode.String));    //20200601.1748            
            dtLote.Columns.Add("ClaveLote", GetType(TypeCode.String));

            dtLote.Columns.Add("MesesCad", GetType(TypeCode.Int32));

            dtLote.Columns.Add("FechaReg", GetType(TypeCode.DateTime));
            dtLote.Columns.Add("FechaCad", GetType(TypeCode.DateTime));
            ////dtLote.Columns.Add("FechaReg", GetType(TypeCode.String));
            ////dtLote.Columns.Add("FechaCad", GetType(TypeCode.String)); 


            dtLote.Columns.Add("Status", GetType(TypeCode.String));
            dtLote.Columns.Add("Existencia", GetType(TypeCode.Int32));
            dtLote.Columns.Add("ExistenciaDisponible", GetType(TypeCode.Int32));
            dtLote.Columns.Add("Cantidad", GetType(TypeCode.Int32));

            //dtLote.Columns.Add("FLO", GetType(TypeCode.String));    //20231122.1119FAV

            dts.Tables.Add(dtLote);

            return dts.Clone();
        }

        public static DataSet PreparaDtsRevision()
        {
            // Nombres de columnas no mayores de 10 caracteres 
            DataSet dts = new DataSet();
            DataTable dtLote = new DataTable("ProductosLotes");

            dtLote.Columns.Add("IdProducto", GetType(TypeCode.String));
            dtLote.Columns.Add("CodigoEAN", GetType(TypeCode.String));
            dtLote.Columns.Add("Descripcion", GetType(TypeCode.String));
            dtLote.Columns.Add("Cantidad", GetType(TypeCode.Int32));
            dtLote.Columns.Add("CantidadLotes", GetType(TypeCode.Int32));
            dts.Tables.Add(dtLote);

            return dts.Clone(); 
        }

        public static DataSet PreparaDtsUUIDS()
        {
            // Nombres de columnas no mayores de 10 caracteres 
            DataSet dts = new DataSet();
            DataTable dtUUID = new DataTable("UUIDS");

            dtUUID.Columns.Add("UUID", GetType(TypeCode.String));
            dtUUID.Columns.Add("IdSubFarmacia", GetType(TypeCode.String));
            dtUUID.Columns.Add("IdProducto", GetType(TypeCode.String));
            dtUUID.Columns.Add("CodigoEAN", GetType(TypeCode.String));
            dtUUID.Columns.Add("ClaveLote", GetType(TypeCode.String));

            dts.Tables.Add(dtUUID);

            return dts.Clone();
        }
        #endregion Funciones y Procedimientos Static 

        #region Manejo de Lotes 
        /// <summary>
        /// Muestra la lista de lotes disponibles para el Producto-CodigoEAN
        /// </summary>
        public void Show()
        {
            FrmCapturaLotes f = new FrmCapturaLotes(this.Rows(Registros.Iguales), pDtsSubFarmacias);
            f.ShowInTaskbar = false;


            f.sIdEmpresa = this.sIdEmpresa;
            f.sIdEstado = this.sIdEstado;
            f.sIdFarmacia = this.sIdFarmacia;
            f.sIdCliente = this.sIdCliente;
            f.sIdSubCliente = this.sIdSubCliente;
            f.sIdPrograma = this.sIdPrograma;
            f.sIdSubPrograma = this.sIdSubPrograma;
            f.bCapturaEnMultiplosDeCajas_ProgramaSubPrograma = this.bProgramaSubPrograma_ForzarCapturaEnMultiplosDeCajas; 

            f.tpOrigenManLotes = tpOrigenManLotes;  
            f.dtsLotesUbicacionesRegistradas = pDtsLotesUbicacionesRegistradas; 
            f.bModificarCaptura = this.bModificarCaptura;
            f.bPermitirCapturaLotesNuevos = this.bPermitirCapturaLotesNuevos;
            f.sIdArticulo = this.sIdProducto;
            f.sCodigoEAN = this.sCodigoEAN;
            f.sDescripcion = this.sDescripcion;
            f.bEsEntrada = this.bEsEntrada;
            f.bEsConsignacion = this.bEsConsignacion;
            f.bPermitirLotesNuevosConsignacion = this.bPermitirLotesNuevosConsignacion; // Consignacion 
            f.bModificarCaptura = this.bModificarCaptura;
            f.iTipoCaptura = this.iTipoCaptura;
            f.bEsTransferenciaDeEntrada = this.bEsTransferenciaDeEntrada;
            f.bEsCancelacionCompras = this.bEsCancelacionCompras;
            f.Encabezados = this.tpEncabezados;
            f.dFechaDeSistema = this.dFechaSistema;
            f.tpOrigenManLotes = this.tpOrigenManLotes;
            f.bPermitirSacarCaducados = this.bPermitirSacarCaducados;
            f.bEsInventarioActivo = this.bEsInventarioActivo;
            f.bEsReubicacion = this.bEsReubicacion;
            f.bModificaCaducidades = this.bModificaCaducidades; 

            f.dtsLotesUbicaciones = this.LotesConUbicaciones.DataSetLotesUbicaciones;
            f.dtsLotes_Destinos = this.LotesReubicacion.DataSetLotes_Destinos;

            f.sPosicionEstandar = sPosicionEstandar;

            f.sIdFuenteFin = this.IdSubFarmacia;

            f.ShowDialog(); 
            this.iCantidad = f.iTotalCantidad;
            this.IntegrarInformacion(f.dtsLotes);
            this.iTipoCaptura = f.iTipoCaptura;
                        
            this.LotesConUbicaciones.DataSetLotesUbicaciones = f.dtsLotesUbicaciones;
            this.LotesReubicacion.DataSetLotes_Destinos = f.dtsLotes_Destinos;
        } 

        public void AddLotes(DataSet Lista)
        {
            if (pDtsLotes == null)
            {
                pDtsLotes = PreparaDtsLotes();
            }

            //leer.DataSetClase = Lista;
            //while (leer.Leer())
            //{
            //}

            try
            {
                // Agrega los nuevos lotes a la lista de Lotes 
                pDtsLotes.Tables[0].Merge(Lista.Tables[0]);

                ////leer.DataSetClase = pDtsLotes;
                ////while (leer.Leer())
                ////{
                ////} 

            }
            catch (Exception ex1)
            {
                ex1.Source = ex1.Source; 
            }
        }

        public void AddLotesUbicaciones(DataSet Lista)
        {
            if (GnFarmacia.ManejaUbicaciones)
            {
                LotesConUbicaciones.AddUbicaciones(Lista); 
            }
        } 

        ////public void AddLotesReUbicaciones(DataSet Lista)
        ////{
        ////    if (GnFarmacia.ManejaUbicaciones)
        ////    {
        ////        LotesReubicacion.AddUbicaciones(Lista);
        ////    }
        ////}  

        /// <summary>
        /// Vacia la lista de Lotes 
        /// </summary>
        public void RemoveLotes()
        {
            pDtsLotes = PreparaDtsLotes();
            this.dtsUUIDS = PreparaDtsUUIDS();
            lst_UUID = new Dictionary<string, clsLotes_ItemUUID>(); 
        }

        /// <summary>
        /// Quita de la lista los lotes que cumplan con el criterio 
        /// </summary>
        /// <param name="IdProducto">Producto del cual se quitaran los lotes</param>
        /// <param name="CodigoEAN">CodigoEAN del cual se quitaran los lotes</param>
        public void RemoveLotes(string IdProducto, string CodigoEAN)
        {
            clsLeer leerRemove = new clsLeer();
            clsLeer leerRemoveUUID = new clsLeer();
            clsLeer UUIDs_Remove = new clsLeer();
            string sFiltro = string.Format("IdProducto = '{0}' and CodigoEAN = '{1}' ", IdProducto, CodigoEAN);
            string sClaveLote = ""; 

            
            foreach (DataRow dtRow in pDtsLotes.Tables[0].Select(sFiltro))
            {
                leerRemove.DataRowClase = dtRow;
                leerRemove.Leer();

                sClaveLote = leerRemove.Campo("ClaveLote"); 

                LotesConUbicaciones.RemoveUbicaciones(IdProducto, CodigoEAN, sClaveLote);
                LotesReubicacion.RemoveUbicaciones(IdProducto, CodigoEAN, sClaveLote); 
                LotesReubicacion.RemoveReubicaciones(IdProducto, CodigoEAN, sClaveLote);


                ////UUIDs_Remove.DataRowsClase = dtsUUIDS.Tables[0].Select(sFiltro);
                UUIDs_Remove = UUID_Listado();
                UUIDs_Remove.DataRowsClase = UUIDs_Remove.DataSetClase.Tables[0].Select(sFiltro);

                while (UUIDs_Remove.Leer())
                {
                    UUID_Remove(UUIDs_Remove.Campo("UUID"));
                }
                
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
                sSelect = string.Format("IdProducto = '{0}' and CodigoEAN = '{1}' {2} ", sIdProducto, sCodigoEAN, sFiltroConsignacion);

                //// Mostrar solo Lotes con Existencia en Sistema 
                if (!bMostrarLotesExistencia_0) 
                {
                    switch (tpOrigenManLotes)
                    {
                        case OrigenManejoLotes.Ventas_Dispensacion:
                        case OrigenManejoLotes.Ventas_PublicoGeneral:
                            if (bModificarCaptura)
                            {
                                sSelect += " and Existencia > 0 ";
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            // Carga Retorno Proceso 
            if (Tipo == Registros.Diferentes)
            {
                sSelect = string.Format("IdProducto <> '{0}' and CodigoEAN <> '{1}' ", sIdProducto, sCodigoEAN);
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

        public clsLotes[] Lotes()
        {
            return Lotes_ProcesoInterno("1=1", false, 0);
        }

        public clsLotes[] Lotes(string CodigoEAN)
        {
            return Lotes(CodigoEAN, true);
        }

        public clsLotes[] Lotes(string CodigoEAN, bool Cantidad_Mayor_0)
        {
            return Lotes_ProcesoInterno(string.Format(" CodigoEAN = '{0}' ", CodigoEAN), Cantidad_Mayor_0, 0);
        }

        public clsLotes[] Lotes(string IdProducto, string CodigoEAN)
        {
            return Lotes(IdProducto, CodigoEAN, true); 
        }

        public clsLotes[] Lotes(string IdProducto, string CodigoEAN, bool Cantidad_Mayor_0)
        {
            return Lotes_ProcesoInterno(string.Format("IdProducto = '{0}' and CodigoEAN = '{1}' ", IdProducto, CodigoEAN), Cantidad_Mayor_0, 0);
        }

        private clsLotes[] Lotes_ProcesoInterno(string Filtro, bool Cantidad_Mayor_0, int Valor)
        {
            List<clsLotes> pListaLote = new List<clsLotes>(); 
            DataSet dtsEx = PreparaDtsLotes();
            iTotalCantidad = 0;

            Filtro += Cantidad_Mayor_0 ? " and Cantidad > 0 " : "";
            foreach (DataRow dtRow in pDtsLotes.Tables[0].Select(Filtro))
            {
                dtsEx.Tables[0].Rows.Add(dtRow.ItemArray);
            }

            leer.DataSetClase = dtsEx;
            while( leer.Leer() )
            {
                clsLotes myLote = new clsLotes(sIdEstado, sIdFarmacia, iMesesCaducidad, false);

                myLote.LotesConUbicaciones.DataSetLotesUbicaciones = LotesConUbicaciones.DataSetLotesUbicaciones; 
                myLote.LotesReubicacion.DataSetLotesUbicaciones = LotesConUbicaciones.DataSetLotesUbicaciones; 
                myLote.LotesReubicacion.DataSetLotes_Destinos = LotesReubicacion.DataSetLotes_Destinos; 


                myLote.IdSubFarmacia = leer.Campo("IdSubFarmacia");
                myLote.SubFarmacia = leer.Campo("SubFarmacia");

                myLote.Codigo = leer.Campo("IdProducto");
                myLote.CodigoEAN = leer.Campo("CodigoEAN");
                myLote.SKU = leer.Campo("SKU");                
                myLote.ClaveLote = leer.Campo("ClaveLote");
                myLote.FechaEntrada = leer.CampoFecha("FechaReg");
                myLote.FechaCaducidad = leer.CampoFecha("FechaCad");

                myLote.Status = "A";
                if (leer.Campo("Status") != "")
                {
                    myLote.Status = leer.Campo("Status").Substring(0, 1); 
                } 

                myLote.Existencia = leer.CampoInt("Existencia");
                myLote.Cantidad = leer.CampoInt("Cantidad");

                //myLote.FLO = leer.Campo("FLO");     //20231122.1119FAV

                iTotalCantidad += myLote.Cantidad;
                pListaLote.Add(myLote);
            }

            return pListaLote.ToArray();             
        }
        #endregion Manejo de Lotes 
    
        #region Override
        private bool bEsLoteCaducado = false;
        private bool bLoteEnCero = false;

        public virtual bool Incrementar_CantidadTE(string IdSubFarmacia,  string SubFarmacia, string IdProducto, string CodigoEAN, string ClaveLote)
        {   
            bool bRegresa = false;
            clsLeer leerIncremento = new clsLeer();
            int iCantidad_Interna = 0, iExistencia_Interna = 0, iMesesCad = 0;


            leerIncremento.DataSetClase = RowsTE(IdProducto, CodigoEAN, IdSubFarmacia, SubFarmacia, ClaveLote);

            if (leerIncremento.Leer())
            {
                iCantidad_Interna = leerIncremento.CampoInt("Cantidad");
                iExistencia_Interna = leerIncremento.CampoInt("Existencia");
                iMesesCad = leerIncremento.CampoInt("MesesCad");
                iCantidad_Interna++;

                leerIncremento.GuardarDatos(1, "Cantidad", iCantidad_Interna);

                IntegrarInformacion(leerIncremento.DataSetClase, IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote);
                iCantidad = leerIncremento.CampoInt("Cantidad");
                bRegresa = true;
            }
            return bRegresa;
        }

        public virtual bool Incrementar_Cantidad(string IdProducto, string CodigoEAN, string SubFarmacia, string ClaveLote)
        {
            bool bRegresa = false;
            clsLeer leerIncremento = new clsLeer();
            int iCantidad_Interna = 0, iExistencia_Interna = 0, iMesesCad = 0;

            leerIncremento.DataRowsClase = Rows(IdProducto, CodigoEAN, SubFarmacia, ClaveLote);

            if (leerIncremento.Leer())
            {
                iCantidad_Interna = leerIncremento.CampoInt("Cantidad");
                iExistencia_Interna = leerIncremento.CampoInt("Existencia");
                iMesesCad = leerIncremento.CampoInt("MesesCad");
                iCantidad_Interna++;

                if (iMesesCad > 0)
                {
                    if (iCantidad_Interna <= iExistencia_Interna)
                    {
                        leerIncremento.GuardarDatos(1, "Cantidad", iCantidad_Interna);

                        IntegrarInformacion(leerIncremento.DataSetClase, IdProducto, CodigoEAN, SubFarmacia, ClaveLote);
                        iCantidad = leerIncremento.CampoInt("Cantidad");
                        bRegresa = true;
                    }
                    else
                    {
                        bLoteEnCero = true;
                    }
                }
                else
                {
                    bEsLoteCaducado = true;
                }
            }
            return bRegresa;
        }

        public virtual bool Incrementar_Cantidad_Ubicaion(string IdProducto, string CodigoEAN, string SubFarmacia, string ClaveLote, string IdPasillo, string IdEstante, string IdEntrepaño)
        {
            bool bRegresa = false;
            clsLeer leerIncremento = new clsLeer();
            int iCantidad_Interna = 0, iExistencia_Interna = 0, iMesesCad = 0;

            leerIncremento.DataRowsClase = Rows_Ubicacion(IdProducto, CodigoEAN, SubFarmacia, ClaveLote, IdPasillo, IdEstante, IdEntrepaño);

            if (leerIncremento.Leer())
            {
                iCantidad_Interna = leerIncremento.CampoInt("Cantidad");
                iExistencia_Interna = leerIncremento.CampoInt("Existencia");
                iMesesCad = leerIncremento.CampoInt("MesesCad");
                iCantidad_Interna++;

                if (iCantidad_Interna <= iExistencia_Interna)
                {
                    leerIncremento.GuardarDatos(1, "Cantidad", iCantidad_Interna);

                    IntegrarInformacion_Ubicacion(leerIncremento.DataSetClase, IdProducto, CodigoEAN, SubFarmacia, ClaveLote, IdPasillo, IdEstante, IdEntrepaño);
                    iCantidad = leerIncremento.CampoInt("Cantidad");
                    bRegresa = true;
                }
                else
                {
                    bLoteEnCero = true;
                }
            }
            return bRegresa;
        }

        private DataRow[] Rows(string IdProducto, string CodigoEAN, string IdSubFarmacia, string ClaveLote)
        {
            string sSelect = string.Format("1=1");
            string sFiltroConsignacion = "";

            sSelect = string.Format("IdProducto = '{0}' and CodigoEAN = '{1}' {2} ", IdProducto, CodigoEAN, sFiltroConsignacion);
            sSelect += string.Format(" and IdSubFarmacia = '{0}' and ClaveLote = '{1}' ", IdSubFarmacia, ClaveLote);

            return pDtsLotes.Tables[0].Select(sSelect);
        }

        private DataSet RowsTE(string IdProducto, string CodigoEAN, string IdSubFarmacia, string SubFarmacia, string ClaveLote)
        {
            if (this.Rows(IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote).Length == 0)
            {
                object[] obj = { IdSubFarmacia, SubFarmacia, IdProducto, CodigoEAN, ClaveLote, iMesesCaducidad, dFechaEntrada, dFechaCaducidad, "A", 0, 0, 0 };
                pDtsLotes.Tables[0].Rows.Add(obj);
            }

            return pDtsLotes;
        }

        private DataRow[] Rows_Ubicacion(string IdProducto, string CodigoEAN, string IdSubFarmacia, string ClaveLote, string IdPasillo, string IdEstante, string IdEntrepaño)
        {
            string sSelect = string.Format("1=1");
            string sFiltroConsignacion = "";

            sSelect = string.Format("IdProducto = '{0}' And CodigoEAN = '{1}' {2} ", IdProducto, CodigoEAN, sFiltroConsignacion);
            sSelect += string.Format(" And IdSubFarmacia = '{0}' And ClaveLote = '{1}' ", IdSubFarmacia, ClaveLote);
            sSelect += string.Format(" And IdPasillo = '{0}' And IdEstante = '{1}' And IdEntrepano = '{2}'", IdPasillo, IdEstante, IdEntrepaño);

            return LotesConUbicaciones.DataSetLotesUbicaciones.Tables[0].Select(sSelect);
        }

        private void IntegrarInformacion(DataSet Lista, string IdProducto, string CodigoEAN, string IdSubFarmacia, string ClaveLote)
        {
            foreach (DataRow dtRow in this.Rows(IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote))
            {
                pDtsLotes.Tables[0].Rows.Remove(dtRow);
            }

            // pDtsLotes = dtsPuente;
            pDtsLotes.Tables[0].Merge(Lista.Tables[0]);
        }

        private void IntegrarInformacion_Ubicacion(DataSet Lista, string IdProducto, string CodigoEAN, string IdSubFarmacia, string ClaveLote, string IdPasillo, string IdEstante, string IdEntrepaño)
        {
            foreach (DataRow dtRow in this.Rows_Ubicacion(IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote, IdPasillo, IdEstante, IdEntrepaño))
            {
                LotesConUbicaciones.DataSetLotesUbicaciones.Tables[0].Rows.Remove(dtRow);
            }

            // pDtsLotes = dtsPuente;
            LotesConUbicaciones.DataSetLotesUbicaciones.Tables[0].Merge(Lista.Tables[0]);
        }

        #endregion Override

        #region Firma GUID 
        public bool UUID_UpdateList(DataSet Lista)
        {
            bool bRegresa = false;
            string sUUID = "";
            clsLeer leerReg = new clsLeer();
            leerReg.DataSetClase = Lista;

            if (dtsUUIDS == null)
            {
                dtsUUIDS = PreparaDtsUUIDS();
                lst_UUID = new Dictionary<string, clsLotes_ItemUUID>();
            }

            while (leerReg.Leer())
            {
                sUUID = leerReg.Campo("UUID");
                if (sUUID != "")
                {
                    UUID_Add(sUUID, new clsLotes_ItemUUID()); 
                }
            }

            return bRegresa;
        }

        public bool UUID_Add(string UUID, clsLotes_ItemUUID ItemUUID)
        {
            bool bRegresa = false;

            try
            {
                if (dtsUUIDS == null)
                {
                    dtsUUIDS = PreparaDtsUUIDS();
                    lst_UUID = new Dictionary<string, clsLotes_ItemUUID>();
                }

                if (!lst_UUID.ContainsKey(UUID))
                {
                    ItemUUID.UUID = UUID;
                    lst_UUID.Add(UUID, ItemUUID);

                    ////dtUUID.Columns.Add("UUID", GetType(TypeCode.String));
                    ////dtUUID.Columns.Add("IdSubFarmacia", GetType(TypeCode.String));
                    ////dtUUID.Columns.Add("IdProducto", GetType(TypeCode.String));
                    ////dtUUID.Columns.Add("CodigoEAN", GetType(TypeCode.String));
                    ////dtUUID.Columns.Add("ClaveLote", GetType(TypeCode.String));

                    object[] obj = { ItemUUID.UUID, ItemUUID.IdSubFarmacia, ItemUUID.Codigo, ItemUUID.CodigoEAN, ItemUUID.ClaveLote };
                    dtsUUIDS.Tables[0].Rows.Add(obj); 

                    bRegresa = true;
                }
            }
            catch { }

            return bRegresa;
        }

        public bool UUID_Remove(string UUID)
        {
            bool bRegresa = false;

            try
            {
                if (dtsUUIDS == null)
                {
                    dtsUUIDS = PreparaDtsUUIDS();
                    lst_UUID = new Dictionary<string, clsLotes_ItemUUID>();
                }

                if (lst_UUID.ContainsKey(UUID))
                {
                    lst_UUID.Remove(UUID);
                    bRegresa = true;
                }
            }
            catch { }

            return bRegresa;
        }

        public bool UUID_Exists(string UUID)
        {
            bool bRegresa = false;

            try
            {
                if (dtsUUIDS == null)
                {
                    dtsUUIDS = PreparaDtsUUIDS();
                    lst_UUID = new Dictionary<string, clsLotes_ItemUUID>();
                }

                bRegresa = lst_UUID.ContainsKey(UUID);
            }
            catch { }

            return bRegresa;
        }

        public clsLeer UUID_List
        {
            get 
            {
                return UUID_Listado(); 
            }
        }

        private clsLeer UUID_Listado()
        {
            clsLeer leerReg = new clsLeer();
            DataSet dts = PreparaDtsUUIDS();
            clsLotes_ItemUUID item_UUID; 

            try
            {
                foreach (string key in lst_UUID.Keys)
                {
                    item_UUID = lst_UUID[key];

                    object[] objValues = { key, item_UUID.IdSubFarmacia, item_UUID.Codigo, item_UUID.CodigoEAN, item_UUID.ClaveLote };
                    ////dtsEx.Tables[0].Rows.Add(dtRow.ItemArray); 
                    dts.Tables[0].Rows.Add(objValues);
                }
            }
            catch { }

            leerReg.DataSetClase = dts; 
            return leerReg;
        }

        public List<string> ListadoDeCodigosEAN()
        {
            List<string> sLista = new List<string>();
            clsLeer leerReg = new clsLeer();

            leerReg.DataSetClase = pDtsLotes; 
            while(leerReg.Leer())
            {
                if ( !sLista.Contains(leerReg.Campo("CodigoEAN")) )
                {
                    sLista.Add(leerReg.Campo("CodigoEAN")); 
                }
            }

            return sLista; 
        }

        public int Totalizar(string IdProducto, string CodigoEAN)
        {
            clsLeer leerReg = new clsLeer(); 
            int iRegresa = 0;
            string sFiltro = string.Format("IdProducto = '{0}' and CodigoEAN = '{1}' ", IdProducto, CodigoEAN);

            try
            {
                leerReg.DataRowsClase = pDtsLotes.Tables[0].Select(sFiltro);
                while (leerReg.Leer())
                {
                    iRegresa += leerReg.CampoInt("Cantidad"); 
                }
            }
            catch { }

            return iRegresa; 
        }
        #endregion Firma GUID

        #region Manejo de Ubicaciones
        private void CargarUbicacionesRegistradas()
        {
            string sSql = string.Format("Select * " + 
                " From vw_Pasillos_Estantes_Entrepaños (NoLock) " + 
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " , 
                sIdEmpresa, sIdEstado, sIdFarmacia ) ;

            //// Jesus Diaz 2K111112.1220 
            if (bBuscarUbicaciones)
            {
                if (leer.Exec(sSql))
                {
                    pDtsLotesUbicacionesRegistradas = leer.DataSetClase;
                }
            }
        }
        public clsLotesUbicacionesItem[] Ubicaciones()
        {
            return LotesConUbicaciones.Ubicaciones(); 
        }

        //public clsLotesUbicacionesItem[] Ubicaciones(string CodigoEAN, string ClaveLote)
        //{
        //    return LotesConUbicaciones.Ubicaciones(CodigoEAN, ClaveLote);
        //}

        public clsLotesUbicacionesItem[] Ubicaciones( string IdProducto, string CodigoEAN, string ClaveLote, string IdSubFarmacia )
        {
            return Ubicaciones("", IdProducto, CodigoEAN, ClaveLote, IdSubFarmacia); 
        }
        public clsLotesUbicacionesItem[] Ubicaciones( string SKU, string IdProducto, string CodigoEAN, string ClaveLote, string IdSubFarmacia )
        {
            return LotesConUbicaciones.Ubicaciones(SKU, IdProducto, CodigoEAN, ClaveLote, IdSubFarmacia, true);
        }
        public clsLotesUbicacionesItem[] Ubicaciones( string IdProducto, string CodigoEAN, string ClaveLote, string IdSubFarmacia, bool Cantidad_Mayor_0 )
        {
            return Ubicaciones("", IdProducto, CodigoEAN, ClaveLote, IdSubFarmacia, Cantidad_Mayor_0);
        }
        public clsLotesUbicacionesItem[] Ubicaciones( string SKU, string IdProducto, string CodigoEAN, string ClaveLote, string IdSubFarmacia, bool Cantidad_Mayor_0 )
        {
            return LotesConUbicaciones.Ubicaciones(IdProducto, CodigoEAN, ClaveLote, IdSubFarmacia, Cantidad_Mayor_0);
        } 
        #endregion Manejo de Ubicaciones

        #region Manejo de Reubicaciones 
        //Reubicaciones
        public clsLotes_ReubicacionesItem[] Reubicaciones()
        {
            return LotesReubicacion.Reubicaciones();
        }

        public clsLotes_ReubicacionesItem[] Reubicaciones( string SKU, string IdSubFarmacia, string CodigoEAN, string ClaveLote) 
        {
            return LotesReubicacion.Reubicaciones(SKU, IdSubFarmacia, CodigoEAN, ClaveLote);
        }

        public clsLotes_ReubicacionesItem[] Reubicaciones( string SKU, string IdSubFarmacia, string IdProducto, string CodigoEAN, string ClaveLote)
        {
            return LotesReubicacion.Reubicaciones(SKU, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote);
        }

        //Esto es para las Entradas

        public clsLotes_ReubicacionesItem[] Reubicaciones_Salidas()
        {
            return LotesReubicacion.Ubicaciones();
        }

        public clsLotes_ReubicacionesItem[] Reubicaciones_Salidas( string SKU, string IdSubFarmacia, string CodigoEAN, string ClaveLote)
        {
            return LotesReubicacion.Ubicaciones(SKU, IdSubFarmacia, CodigoEAN, ClaveLote);
        }

        public clsLotes_ReubicacionesItem[] Reubicaciones_Salidas( string SKU, string IdSubFarmacia, string IdProducto, string CodigoEAN, string ClaveLote)
        {
            return LotesReubicacion.Ubicaciones(SKU, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote);
        }

        #endregion Manejo de Reubicaciones
    }
}