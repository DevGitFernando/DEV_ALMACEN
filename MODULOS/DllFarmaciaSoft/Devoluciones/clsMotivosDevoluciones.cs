using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;

namespace DllFarmaciaSoft.Devoluciones
{
    public class clsMotivosDevoluciones
    {
        #region DeclaracionVariables
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosConexion datosCnn;
        clsLeer leer;

        DataSet dtsRetorno = new DataSet();
        clsGrabarError Error;
        string sIdTipoMovto = "";
        string sMovtoInv = "";
        bool bMarco = false;
        bool bBloquearCaptura = false; 

        string sEmpresa = "";
        string sEstado = "";
        string sFarmacia = "";

        TipoDevolucion tpDevolucion = TipoDevolucion.Ninguna;
        #endregion DeclaracionVariables

        #region Constructor
        public clsMotivosDevoluciones(clsDatosConexion DatosConexion, TipoDevolucion IdTipoDev, string Empresa, string Estado, string Farmacia)
        {
            cnn = new clsConexionSQL(DatosConexion);
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError();

            dtsRetorno = null;            

            this.Tipo = IdTipoDev;
            this.sEmpresa = Empresa;
            this.sEstado = Estado;
            this.sFarmacia = Farmacia;
            
        }
        #endregion Constructor 

        #region Propiedades
        public bool Marco
        {
            get { return bMarco; }
            set { bMarco = value; }
        }

        public bool BloquearCaptura
        {
            get { return bBloquearCaptura; }
            set { bBloquearCaptura = value; }
        }

        public DataSet Retorno
        {
            get { return dtsRetorno; }
            set { dtsRetorno = value; }
        }

        public TipoDevolucion Tipo
        {
            get { return tpDevolucion; }
            set
            {
                if (value == TipoDevolucion.Compras)
                {
                    sIdTipoMovto = "CC";                    
                }

                if (value == TipoDevolucion.Venta)
                {
                    sIdTipoMovto = "ED";                    
                }

                if (value == TipoDevolucion.PedidosVenta)
                {
                    sIdTipoMovto = "DEPD";                    
                }

                if (value == TipoDevolucion.PedidosConsignacion)
                {
                    sIdTipoMovto = "DPDC";                    
                }


                if (value == TipoDevolucion.EntradasDeConsignacion)
                {
                    sIdTipoMovto = "DEPC";                    
                }

                if (value == TipoDevolucion.OrdenCompra)
                {
                    sIdTipoMovto = "DOC";                    
                }

                if (value == TipoDevolucion.Dev_Proveedor)
                {
                    sIdTipoMovto = "CSDP";                    
                }

                if (value == TipoDevolucion.TransferenciaDeEntrada)
                {
                    sIdTipoMovto = "EDT";
                }

                if (value == TipoDevolucion.TransferenciaDeSalida)
                {
                    sIdTipoMovto = "SDT";
                }

                if (value == TipoDevolucion.Ninguna)
                {
                    sIdTipoMovto = sMovtoInv;
                }

                tpDevolucion = value;
            }
        }

        public string Movto_Inv
        {
            get { return sMovtoInv; }
            set { sMovtoInv = value; }
        }
        #endregion Propiedades

        #region Funciones Publicas
        public void MotivosDevolucion()
        {
            bool bMostrar = false;

            FrmListadoMotivosDev f = new FrmListadoMotivosDev();

            if (dtsRetorno == null) 
            {
                if (CargarMotivos())
                {
                    bMostrar = true;
                    f.MostrarPantalla(dtsRetorno, sIdTipoMovto, bBloquearCaptura);
                }
            }
            else
            {
                leer.DataSetClase = dtsRetorno;
                leer.Leer();

                if (leer.Campo("IdMovto") == sIdTipoMovto)
                {
                    bMostrar = true;
                    f.MostrarPantalla(dtsRetorno, sIdTipoMovto, bBloquearCaptura);
                }
                else
                {
                    if (CargarMotivos())
                    {
                        bMostrar = true;
                        f.MostrarPantalla(dtsRetorno, sIdTipoMovto, bBloquearCaptura);
                    }
                }
            }

            if (bMostrar)
            {
                f.ShowDialog();
            }

            dtsRetorno = f.Retorno;
            bMarco = f.MarcoMotivos;
            
        }
        #endregion Funciones Publicas

        #region Generar_Dataset
        public static DataSet PreparaDtsMotivos()
        {
             
            DataSet dts = new DataSet();
            DataTable dtMotivo = new DataTable("Motivos");

            dtMotivo.Columns.Add("IdMovto", GetType(TypeCode.String));
            dtMotivo.Columns.Add("IdMotivo", GetType(TypeCode.String));
            dtMotivo.Columns.Add("Motivo", GetType(TypeCode.String));
            dtMotivo.Columns.Add("Marca", GetType(TypeCode.Int32));
            dtMotivo.Columns.Add("Status", GetType(TypeCode.Boolean));


            dts.Tables.Add(dtMotivo);

            return dts.Clone();
        }

        private static Type GetType(TypeCode TipoDato)
        {
            return Type.GetType("System." + TipoDato.ToString());
        }
        #endregion Generar_Dataset

        #region Funciones_Privadas
        private bool CargarMotivos()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(
                " Select IdTipoMovto_Inv, IdMotivo, Descripcion, 0 as Marcar, (case when Status = 'A' then 1 else 0 end) as Status " + 
                " From MovtosInv_Motivos_Dev (NoLock) " + 
                " Where IdTipoMovto_Inv = '{0}' " + 
                " Order By IdMotivo ", sIdTipoMovto); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarMotivos");
                General.msjError("Ocurrio un error al obtener los datos de motivos.");
                bRegresa = false;
            }
            else
            {
                if (leer.Leer())
                {
                    dtsRetorno = leer.DataSetClase;
                }
                else
                {
                    General.msjUser("No se encontraron motivos de devolucion para este tipo de movimiento.");
                    bRegresa = false;
                }
            }

            return bRegresa;
        }
        #endregion Funciones_Privadas

        #region Asignar_Tipo_Movimiento
        public void AsignarTipoMovto()
        {
            sIdTipoMovto = sMovtoInv;
        }
        #endregion Asignar_Tipo_Movimiento
    }
}
