using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DllProveedores;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

using DllProveedores.OrdenesCompra;

namespace DllProveedores.Lotes
{
    public class clsEAN_Lotes
    {
        private enum Registros
        {
            Todos = 0, Iguales = 1, Diferentes = 2
        }

        public enum enumLotes
        {
            Producto = 1, Producto_EAN = 2, Todos = 3
        }

        #region Declaracion de variables

        private string sIdClaveSSA = "";
        private string sCodigoEAN = "";
        private string sClaveLote = "";
        private string sDescripcion = "";
        private int iCantEAN = 0;
        private DateTime dFechaEntrada = DateTime.Now;
        private DateTime dFechaCaducidad = DateTime.Now;
       
        private int iMesesCaducidad = 12;

        private bool bPrimerLote = false;
        
        //EncabezadosManejoLotes tpEncabezados = EncabezadosManejoLotes.Default;
        //OrigenManejoLotes tpOrigenManLotes = OrigenManejoLotes.Default;
        
        int iCantidad = 0;

        DataSet pDtsLotes;
        basGenerales Fg = new basGenerales();
        clsLeer leer = new clsLeer();
        #endregion Declaracion de variables

        #region Constructores y Destructor 
        public clsEAN_Lotes()
        {            
            
        }
        #endregion Constructor y Destructor

        #region Propiedades publicas

        public DataSet ListaLotes
        {
            get { return pDtsLotes; }
        }
        public string IdClaveSSA
        {
            get { return sIdClaveSSA; }
            set { sIdClaveSSA = value; }
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

        public string Descripcion
        {
            get { return sDescripcion; }
            set { sDescripcion = value; }
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

        public int Cantidad
        {
            get { return iCantidad; }
            set { iCantidad = value; }
        }                

        public int MesesDeCaducidad
        {
            get { return iMesesCaducidad; }
            set { iMesesCaducidad = value; }
        }

        public bool PrimerLote
        {
            get { return bPrimerLote; }
            set { bPrimerLote = value; }
        }

        public int CantEAN
        {
            get { return iCantEAN; }
            set { iCantEAN = value; }
        }

        #endregion Propiedades publicas

        #region Funciones y Procedimientos Static
        private static Type GetType(TypeCode TipoDato)
        {
            return Type.GetType("System." + TipoDato.ToString());
        }

        public static DataSet PreparaDtsLotes()
        {
            // Nombres de columnas no mayores de 10 caracteres 
            DataSet dts = new DataSet();
            DataTable dtLote = new DataTable("Lotes");

            dtLote.Columns.Add("IdClaveSSA", GetType(TypeCode.String));
            dtLote.Columns.Add("CodigoEAN", GetType(TypeCode.String));
            dtLote.Columns.Add("ClaveLote", GetType(TypeCode.String));
            dtLote.Columns.Add("MesesCad", GetType(TypeCode.Int32)); 
            dtLote.Columns.Add("FechaReg", GetType(TypeCode.DateTime));
            dtLote.Columns.Add("FechaCad", GetType(TypeCode.DateTime));          
            dtLote.Columns.Add("Cantidad", GetType(TypeCode.Int32));
            dts.Tables.Add(dtLote);

            return dts.Clone();
        }
        #endregion Funciones y Procedimientos Static

        #region Manejo de Lotes 
        /// <summary>
        /// Muestra la lista de lotes disponibles para el Producto-CodigoEAN
        /// </summary>
        public void Show()
        {
            FrmOrdComCodigosEAN_Lotes f = new FrmOrdComCodigosEAN_Lotes(this.Rows(Registros.Iguales));

            f.sIdClaveSSA = this.sIdClaveSSA;
            f.sCodigoEAN = this.sCodigoEAN;
            f.sDescripcion = this.sDescripcion;
            f.iCantidadReq = this.iCantidad;
            f.bPrimerLote = this.PrimerLote;

            f.ShowDialog();
            this.iCantEAN = f.iCant_EAN;
            this.IntegrarInformacion(f.dtsLotes);
            
        }

        public void AddLotes(DataSet Lista)
        {
            if (pDtsLotes == null)
                pDtsLotes = PreparaDtsLotes();

            //leer.DataSetClase = Lista;
            //while (leer.Leer())
            //{
            //}

            try
            {
                // Agrega los nuevos lotes a la lista de Lotes 
                pDtsLotes.Tables[0].Merge(Lista.Tables[0]);

                leer.DataSetClase = pDtsLotes;
                while (leer.Leer())
                {
                }
            }
            catch { }
        }

        /// <summary>
        /// Vacia la lista de Lotes 
        /// </summary>
        public void RemoveLotes()
        {
            pDtsLotes = PreparaDtsLotes();
        }

        /// <summary>
        /// Quita de la lista los lotes que cumplan con el criterio 
        /// </summary>
        /// <param name="IdProducto">Producto del cual se quitaran los lotes</param>
        /// <param name="CodigoEAN">CodigoEAN del cual se quitaran los lotes</param>
        public void RemoveLotes(string CodigoEAN)
        {
            string sFiltro = string.Format("CodigoEAN = '{0}' ", CodigoEAN);
            foreach (DataRow dtRow in pDtsLotes.Tables[0].Select(sFiltro))
            {
                pDtsLotes.Tables[0].Rows.Remove(dtRow);
            }
        }

        private DataRow[] Rows(Registros Tipo)
        {
            string sSelect = string.Format("1=1");

            if (pDtsLotes == null)
            {
                pDtsLotes = PreparaDtsLotes();
            }

            // else
            {
                if (Tipo == Registros.Iguales)
                    sSelect = string.Format("CodigoEAN = '{0}' ", sCodigoEAN);

                if (Tipo == Registros.Diferentes)
                    sSelect = string.Format("CodigoEAN <> '{0}' ", sCodigoEAN);
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

        public clsEAN_Lotes[] Lotes()
        {
            return Lotes("1=1",0);
        }

        public clsEAN_Lotes[] Lotes(string CodigoEAN)
        {
            return Lotes(string.Format(" CodigoEAN = '{0}' ", CodigoEAN),0);
        }

        private clsEAN_Lotes[] Lotes(string Filtro, int Valor)
        {
            List<clsEAN_Lotes> pListaLote = new List<clsEAN_Lotes>();
            DataSet dtsEx = PreparaDtsLotes();
            //iTotalCantidad = 0;

            foreach (DataRow dtRow in pDtsLotes.Tables[0].Select(Filtro))
            {
                dtsEx.Tables[0].Rows.Add(dtRow.ItemArray);
            }

            leer.DataSetClase = dtsEx;
            while( leer.Leer() )
            {
                clsEAN_Lotes myLote = new clsEAN_Lotes();

                myLote.IdClaveSSA = leer.Campo("IdClaveSSA");
                myLote.CodigoEAN = leer.Campo("CodigoEAN");
                myLote.ClaveLote = leer.Campo("ClaveLote");
                myLote.MesesDeCaducidad = leer.CampoInt("MesesCad");
                myLote.FechaEntrada = leer.CampoFecha("FechaReg");      
                myLote.FechaCaducidad = leer.CampoFecha("FechaCad");                               
                myLote.Cantidad = leer.CampoInt("Cantidad");
                //iTotalCantidad += myLote.Cantidad;
                pListaLote.Add(myLote);
            }

            return pListaLote.ToArray();             
        }
        #endregion Manejo de Lotes 
    
    }
}