using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using DllFarmaciaSoft;

using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem;

namespace DllCompras.OrdenesDeCompra
{
    public enum Registros
    {
        Todos = 0, Iguales = 1, Diferentes = 2
    }

    class clsMotivosSobreCompra
    {

        private string sIdEmpresa = DtGeneral.EmpresaConectada;
        private string sIdEstado = DtGeneral.EstadoConectado;
        private string sIdFarmacia = DtGeneral.FarmaciaConectada;

        public DataSet pdtsMotivosDet;
        public DataSet pdtsMotivosEnc;
        private string sIdProducto = "", sCodigoEAN = "", sDescripcionProducto = "", sIdMotivoSobrePrecio = "", sStatus = "", sObservaciones = "";
        private double dPrecioCaja, dPrecioReferencia;
        private bool bContinuar = false;
        basGenerales Fg = new basGenerales();
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        

        public void AddMovtos(DataSet Lista)
        {
            if (pdtsMotivosDet == null)
            {
                pdtsMotivosDet = PreparaDtsMotivosDet();
            }

            if (pdtsMotivosEnc == null)
            {
                pdtsMotivosEnc = PreparaDtsMotivosEnc();
            }

            try
            {
                clsLeer leer = new clsLeer();

                leer.DataTableClase = Lista.Tables[1];

                leer.Leer();
                object[] objRowEnc = {
                                    leer.Campo("IdEmpresa"),  
                                    leer.Campo("IdEstado"),
                                    leer.Campo("IdFarmacia"),
                                    leer.Campo("FolioOrden"),
                                    leer.Campo("IdProducto"),
                                    leer.Campo("CodigoEAN"),
                                    dPrecioCaja,
                                    dPrecioReferencia,
                                    leer.Campo("Observaciones")
                                  };

                pdtsMotivosEnc.Tables[0].Rows.Add(objRowEnc);
                // Agrega los nuevos motivos a la lista de motivos
                pdtsMotivosDet.Tables[0].Merge(Lista.Tables[0]);
                //dtsMotivosEnc.Tables[0].Merge(leer.DataTableClase);
            }
            catch (Exception ex1)
            {
                ex1.Source = ex1.Source;
            }
        }

        public void RemoveMovtos(string IdProducto, string CodigoEAN)
        {
            string sFiltro = string.Format("IdProducto = '{0}' and CodigoEAN = '{1}' ", IdProducto, CodigoEAN);

            foreach (DataRow dtRow in pdtsMotivosDet.Tables[0].Select(sFiltro))
            {
                pdtsMotivosDet.Tables[0].Rows.Remove(dtRow);
            }
        }

        public static DataSet PreparaDtsMotivosDet()
        {
            // Nombres de columnas no mayores de 10 caracteres 
            DataSet dts = new DataSet();
            DataTable dtsMotivosDet = new DataTable("Motivos");
            dtsMotivosDet.Columns.Add("IdEmpresa", GetType(TypeCode.String));
            dtsMotivosDet.Columns.Add("IdEstado", GetType(TypeCode.String));
            dtsMotivosDet.Columns.Add("IdFarmacia", GetType(TypeCode.String));
            dtsMotivosDet.Columns.Add("FolioOrden", GetType(TypeCode.String));
            dtsMotivosDet.Columns.Add("IdProducto", GetType(TypeCode.String));
            dtsMotivosDet.Columns.Add("CodigoEAN", GetType(TypeCode.String));
            dtsMotivosDet.Columns.Add("IdMotivoSobrePrecio", GetType(TypeCode.String));
            dtsMotivosDet.Columns.Add("Descripcion", GetType(TypeCode.String));
            dtsMotivosDet.Columns.Add("Status", GetType(TypeCode.String));
            dts.Tables.Add(dtsMotivosDet);

            return dts.Clone();
        }

        public static DataSet PreparaDtsMotivosEnc()
        {
            DataSet dts = new DataSet();
            DataTable dtsMotivosEnc = new DataTable("Motivos");
            dtsMotivosEnc.Columns.Add("IdEmpresa", GetType(TypeCode.String));
            dtsMotivosEnc.Columns.Add("IdEstado", GetType(TypeCode.String));
            dtsMotivosEnc.Columns.Add("IdFarmacia", GetType(TypeCode.String));
            dtsMotivosEnc.Columns.Add("FolioOrden", GetType(TypeCode.String));
            dtsMotivosEnc.Columns.Add("IdProducto", GetType(TypeCode.String));
            dtsMotivosEnc.Columns.Add("CodigoEAN", GetType(TypeCode.String));
            dtsMotivosEnc.Columns.Add("PrecioCaja", GetType(TypeCode.Decimal));
            dtsMotivosEnc.Columns.Add("PrecioReferencia", GetType(TypeCode.Decimal));
            dtsMotivosEnc.Columns.Add("Observaciones", GetType(TypeCode.String));
            dts.Tables.Add(dtsMotivosEnc);

            return dts.Clone();
        }

        private static Type GetType(TypeCode TipoDato)
        {
            return Type.GetType("System." + TipoDato.ToString());
        }

        public void Show()
        {
            FrmObservacionesDeSobrePrecio f = new FrmObservacionesDeSobrePrecio(this.RowsDet(Registros.Iguales), this.RowsEnc(Registros.Iguales));
            f.ShowInTaskbar = false;
            f.Descripcion = Descripcion;
            f.ShowDialog();
            this.bContinuar = f.Continuar;
            if (bContinuar)
            {
                this.IntegrarInformacion(f.dtsMotivosDet,f.dtsMotivosEnc );
            }
        }

        private DataRow[] RowsDet(Registros Tipo)
        {
            string sSelect = string.Format("1=1");

            if (Tipo == Registros.Iguales)
            {
                sSelect = string.Format("IdProducto = '{0}' and CodigoEAN = '{1}'", sIdProducto, sCodigoEAN);
            }

            // Carga Retorno Proceso 
            if (Tipo == Registros.Diferentes)
            {
                sSelect = string.Format("IdProducto <> '{0}' and CodigoEAN <> '{1}' ", sIdProducto, sCodigoEAN);
            }

            return pdtsMotivosDet.Tables[0].Select(sSelect);
        }

        private DataRow[] RowsEnc(Registros Tipo)
        {
            string sSelect = string.Format("1=1");

            if (Tipo == Registros.Iguales)
            {
                sSelect = string.Format("IdProducto = '{0}' and CodigoEAN = '{1}'", sIdProducto, sCodigoEAN);
            }

            // Carga Retorno Proceso 
            if (Tipo == Registros.Diferentes)
            {
                sSelect = string.Format("IdProducto <> '{0}' and CodigoEAN <> '{1}' ", sIdProducto, sCodigoEAN);
            }

            return pdtsMotivosEnc.Tables[0].Select(sSelect);
        }

        private void IntegrarInformacion(DataSet ListaDet, DataSet ListaEnc)
        {
            foreach (DataRow dtRow in this.RowsDet(Registros.Iguales))
            {
                pdtsMotivosDet.Tables[0].Rows.Remove(dtRow);
            }
            pdtsMotivosDet.Tables[0].Merge(ListaDet.Tables[0]);


            foreach (DataRow dtRow in this.RowsEnc(Registros.Iguales))
            {
                pdtsMotivosEnc.Tables[0].Rows.Remove(dtRow);
            }
            pdtsMotivosEnc.Tables[0].Merge(ListaEnc.Tables[0]);
        }

        public clsMotivosSobreCompra[] MotivosDet(string IdProducto, string CodigoEAN)
        {
            string Filtro = string.Format("IdProducto = '{0}' and CodigoEAN = '{1}' ", IdProducto, CodigoEAN);
            List<clsMotivosSobreCompra> pListaMovtos = new List<clsMotivosSobreCompra>();
            DataSet dtsEx = PreparaDtsMotivosDet();
            clsLeer myLeer = new clsLeer(ref cnn);

            foreach (DataRow dtRow in pdtsMotivosDet.Tables[0].Select(Filtro))
            {
                dtsEx.Tables[0].Rows.Add(dtRow.ItemArray);
            }

            myLeer.DataSetClase = dtsEx;
            while (myLeer.Leer())
            {
                clsMotivosSobreCompra myMotivos = new clsMotivosSobreCompra();

                myMotivos.IdProducto = myLeer.Campo("IdProducto");
                myMotivos.CodigoEAN = myLeer.Campo("CodigoEAN");
                myMotivos.sIdMotivoSobrePrecio = myLeer.Campo("IdMotivoSobrePrecio");
                myMotivos.sStatus = myLeer.Campo("Status");

                pListaMovtos.Add(myMotivos);
            }

            return pListaMovtos.ToArray();
        }

        public clsMotivosSobreCompra[] MotivosEnc(string IdProducto, string CodigoEAN)
        {
            string Filtro = string.Format("IdProducto = '{0}' and CodigoEAN = '{1}' ", IdProducto, CodigoEAN);
            List<clsMotivosSobreCompra> pListaMovtos = new List<clsMotivosSobreCompra>();
            DataSet dtsEx = PreparaDtsMotivosEnc();
            clsLeer myLeer = new clsLeer(ref cnn);

            foreach (DataRow dtRow in pdtsMotivosEnc.Tables[0].Select(Filtro))
            {
                dtsEx.Tables[0].Rows.Add(dtRow.ItemArray);
            }

            myLeer.DataSetClase = dtsEx;
            while (myLeer.Leer())
            {
                clsMotivosSobreCompra myMotivos = new clsMotivosSobreCompra();

                myMotivos.IdProducto = myLeer.Campo("IdProducto");
                myMotivos.CodigoEAN = myLeer.Campo("CodigoEAN");
                myMotivos.dPrecioCaja = myLeer.CampoDouble("PrecioCaja");
                myMotivos.dPrecioReferencia = myLeer.CampoDouble("PrecioReferencia");
                myMotivos.sObservaciones = myLeer.Campo("Observaciones");

                pListaMovtos.Add(myMotivos);
            }

            return pListaMovtos.ToArray();
        }

        #region PropiedadesPublicas

        public string IdProducto
        {
            get { return Fg.PonCeros(sIdProducto, 8); }
            set { sIdProducto = value; }
        }

        public string CodigoEAN
        {
            get { return sCodigoEAN; }
            set { sCodigoEAN = value; }
        }

        public bool Continuar
        {
            get { return bContinuar; }
            set { bContinuar = value; }
        }

        public double PrecioCaja
        {
            get { return dPrecioCaja; }
            set { dPrecioCaja = value; }
        }

        public double PrecioReferencia
        {
            get { return dPrecioReferencia; }
            set { dPrecioReferencia = value; }
        }

        public string Descripcion
        {
            get { return sDescripcionProducto; }
            set { sDescripcionProducto = value; }
        }

        public string Observaciones
        {
            get { return sObservaciones; }
            set { sObservaciones = value; }
        }

        public string IdMotivoSobrePrecio
        {
            get { return sIdMotivoSobrePrecio; }
            set { sIdMotivoSobrePrecio = value; }
        }

        public string Status
        {
            get { return sStatus; }
            set { sStatus = value; }
        }

        #endregion PropiedadesPublicas
    }
}
