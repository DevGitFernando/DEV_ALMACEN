using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Data;

using DllFarmaciaSoft;

namespace Almacen.Pedidos.Validacion
{
    class ClsValidarLotes
    {
        basGenerales Fg = new basGenerales();

        private string sIdProducto = "";
        private string sCodigoEAN = "";
        private string sDescripcion = "";
        private string sClaveLote = "";
        private int iCantidad = 0;

        private string sIdEmpresa = DtGeneral.EmpresaConectada;
        private string sIdEstado = DtGeneral.EstadoConectado;
        private string sIdFarmacia = DtGeneral.FarmaciaConectada;

        DataSet pDtsLotes;
        clsLeer leer = new clsLeer();

        public ClsValidarLotes()
        {
            if (pDtsLotes == null)
            {
                pDtsLotes = PreparaDtsLotes();
            }
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

        public string Descripcion
        {
            get { return sDescripcion; }
            set { sDescripcion = value; }
        }

        public int Cantidad
        {
            get { return iCantidad; }
            set { iCantidad = value; }
        }

        public string ClaveLote
        {
            get { return sClaveLote; }
            set { sClaveLote = value; }
        }

        public ClsValidarLotes[] Lotes(string IdProducto, string CodigoEAN)
        {
            return Lotes(IdProducto, CodigoEAN, true);
        }

        public ClsValidarLotes[] Lotes(string IdProducto, string CodigoEAN, bool Cantidad_Mayor_0)
        {
            return Lotes_ProcesoInterno(string.Format("IdProducto = '{0}' and CodigoEAN = '{1}' ", IdProducto, CodigoEAN), Cantidad_Mayor_0, 0);
        }

        private ClsValidarLotes[] Lotes_ProcesoInterno(string Filtro, bool Cantidad_Mayor_0, int Valor)
        {
            List<ClsValidarLotes> pListaLote = new List<ClsValidarLotes>();
            DataSet dtsEx = PreparaDtsLotes();
            int iTotalCantidad = 0;

            Filtro += Cantidad_Mayor_0 ? " and Cantidad > 0 " : "";
            foreach (DataRow dtRow in pDtsLotes.Tables[0].Select(Filtro))
            {
                dtsEx.Tables[0].Rows.Add(dtRow.ItemArray);
            }

            leer.DataSetClase = dtsEx;
            while (leer.Leer())
            {
                ClsValidarLotes myLote = new ClsValidarLotes();

                myLote.sIdEmpresa = sIdEmpresa;
                myLote.sIdEstado = sIdEstado;
                myLote.sIdFarmacia = sIdFarmacia;


                myLote.Codigo = leer.Campo("IdProducto");
                myLote.CodigoEAN = leer.Campo("CodigoEAN");
                myLote.ClaveLote = leer.Campo("ClaveLote");

                myLote.Cantidad = leer.CampoInt("Cantidad");
                iTotalCantidad += myLote.Cantidad;
                pListaLote.Add(myLote);
            }

            return pListaLote.ToArray();
        }

        public void Show()
        {
            FrmPedidosValidacionCiegaLotes f = new FrmPedidosValidacionCiegaLotes(this.Rows());
            f.ShowInTaskbar = false;

            f.sIdEmpresa = this.sIdEmpresa;
            f.sIdEstado = this.sIdEstado;
            f.sIdFarmacia = this.sIdFarmacia;
            f.sIdArticulo = this.sIdProducto;
            f.sCodigoEAN = this.sCodigoEAN;
            f.sDescripcion = this.sDescripcion;


            f.ShowDialog();
            this.iCantidad = f.iTotalCantidad;
            this.IntegrarInformacion(f.dtsLotes);
        }

        private DataRow[] Rows()
        {
            string sSelect = string.Format("IdProducto = '{0}' and CodigoEAN = '{1}' ", sIdProducto, sCodigoEAN);

            return pDtsLotes.Tables[0].Select(sSelect);
        }

        private void IntegrarInformacion(DataSet Lista)
        {
            foreach (DataRow dtRow in this.Rows())
            {
                pDtsLotes.Tables[0].Rows.Remove(dtRow);
            }

            // pDtsLotes = dtsPuente;
            pDtsLotes.Tables[0].Merge(Lista.Tables[0]);
        }

        public static DataSet PreparaDtsLotes()
        {
            // Nombres de columnas no mayores de 10 caracteres 
            DataSet dts = new DataSet();
            DataTable dtLote = new DataTable("Lotes");

            dtLote.Columns.Add("IdProducto", GetType(TypeCode.String));
            dtLote.Columns.Add("CodigoEAN", GetType(TypeCode.String));
            dtLote.Columns.Add("ClaveLote", GetType(TypeCode.String));
            dtLote.Columns.Add("Cantidad", GetType(TypeCode.Int32));
            dts.Tables.Add(dtLote);

            return dts.Clone();
        }

        private static Type GetType(TypeCode TipoDato)
        {
            return Type.GetType("System." + TipoDato.ToString());
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

        public void RemoveLotes(string IdProducto, string CodigoEAN)
        {
            string sFiltro = string.Format("IdProducto = '{0}' and CodigoEAN = '{1}' ", IdProducto, CodigoEAN);

            foreach (DataRow dtRow in pDtsLotes.Tables[0].Select(sFiltro))
            {
                pDtsLotes.Tables[0].Rows.Remove(dtRow);
            }
        }

    }
}
