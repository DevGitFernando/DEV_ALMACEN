using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_ControlsCS;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using DllFarmaciaSoft.Lotes;

namespace DllFarmaciaSoft.LimitesConsumoClaves
{
    public partial class clsVerificarCantSubPerfil  
    {
        public DataSet dtsLotes = clsLotes.PreparaDtsLotes();

        bool bRevision_a_Nivel_Farmacia = false; 
        string sIdCliente = "";
        string sIdSubCliente = ""; 
        string sIdPrograma = "";
        string sIdSubPrograma = "";
        string sTipoDispensacion = "";

        #region Constructores y Destructor de Clase 
        public clsVerificarCantSubPerfil(bool Revision_a_Nivel_Farmacia) //:this(Revision_a_Nivel_Farmacia, "") 
        {
            bRevision_a_Nivel_Farmacia = Revision_a_Nivel_Farmacia;
            sTipoDispensacion = ""; 
        }

        public clsVerificarCantSubPerfil(bool Revision_a_Nivel_Farmacia, string TipoDispensacion)
        {
            bRevision_a_Nivel_Farmacia = Revision_a_Nivel_Farmacia;
            sTipoDispensacion = TipoDispensacion; 
        }

        public bool VerificarCantidadesConExceso(clsLotes Lotes, string IdCliente, string IdSubCliente, string IdPrograma, string IdSubPrograma)
        {
            bool bRegresa = false;

            sIdCliente = IdCliente;
            sIdSubCliente = IdSubCliente;
            sIdPrograma = IdPrograma;
            sIdSubPrograma = IdSubPrograma;
            bRegresa = VerificarCantidadesConExceso(Lotes.DataSetLotes, false);            

            return bRegresa;
        }
        #endregion Constructores y Destructor de Clase

        #region Propiedades 
        public bool Revision_a_Nivel_Farmacia
        {
            get { return bRevision_a_Nivel_Farmacia; }
            set { bRevision_a_Nivel_Farmacia = value; }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Privados
        #endregion Funciones y Procedimientos Privados

        #region Funciones y Procedimientos Publicos
        public bool VerificarCantidadesConExceso(clsLotes Lotes, bool MostrarMsj)
        {
            bool bRegresa = false;

            bRegresa = VerificarCantidadesConExceso(Lotes.DataSetLotes, MostrarMsj);
            
            return bRegresa;  
        }

        private bool VerificarCantidadesConExceso(DataSet Lotes)
        {
            return VerificarCantidadesConExceso(Lotes, false); 
        }

        private bool VerificarCantidadesConExceso(DataSet Lotes, bool MostrarMsj) 
        {
            bool bRegresa = false;

            FrmClavesCantidadesExcedidas VerificarCantidadesExceso = new FrmClavesCantidadesExcedidas(bRevision_a_Nivel_Farmacia, sTipoDispensacion);
            bRegresa = VerificarCantidadesExceso.VerificarCantidadesConExceso(Lotes, sIdCliente, sIdSubCliente, sIdPrograma, sIdSubPrograma); 
            
            return bRegresa;
        }
        #endregion Funciones y Procedimientos Publicos
    }
}
