using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data; 

namespace DllFarmaciaSoft.Facturacion
{
    public partial class FrmConfigImpresionVentaDetalle : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        clsConsultas Consultas;
        clsAyudas Ayuda;


        public FrmConfigImpresionVentaDetalle()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);
        }

        private void FrmConfigImpresionVentaDetalle_Load(object sender, EventArgs e)
        {
            CargarListaUnidades(); 
            btnNuevo_Click(null, null); 
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            cboUnidades.Focus(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                GuardarConfiguracion(); 
            }
        }
        #endregion Botones

        #region Cargar configuracion 
        private void cboUnidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboUnidades.SelectedIndex != 0)
            {
                CargarConfiguracion();
            }
        } 
        #endregion Cargar configuracion

        #region Funciones y Procedimientos Privados
        private void CargarListaUnidades()
        {
            string sSql = string.Format(
                "Select IdFarmacia, (IdFarmacia + ' - ' + Farmacia) as Farmacia " + 
                "From vw_Farmacias F (NoLock) " + 
                "Where F.IdEstado = '{1}' and F.Status = 'A' " +  
                "   and Exists " + 
                "   ( " + 
                "       Select * From FarmaciaProductos L (NoLock) " + 
                "       Where L.IdEmpresa = '{0}' and L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia " +  
                "   ) " + 
                "Order by IdFarmacia ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado);
            
            cboUnidades.Clear();
            cboUnidades.Add(); 

            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    cboUnidades.Add(leer.DataSetClase, true, "IdFarmacia", "Farmacia"); 
                }
            }

            cboUnidades.SelectedIndex = 0; 
        }

        private bool validarDatos()
        {
            bool bRegresa = true;

            if (cboUnidades.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una unidad válida, verifique.");
                cboUnidades.Focus(); 
            }

            return bRegresa; 
        }

        private void GuardarConfiguracion()
        {
            int i001_Cliente = chk001_Cliente.Checked ? 1 : 0;
            int i002_SoloEtiqueta_Cliente = chk002_SoloEtiqueta_Cliente.Checked ? 1 : 0; 
            int i003_SubCliente = chk003_SubCliente.Checked ? 1 : 0;
            int i004_SoloEtiqueta_SubCliente = chk004_SoloEtiqueta_SubCliente.Checked ? 1 : 0; 
            int i005_IntercambiarSubCliente_Cliente = chk005_IntercambiarSubCliente_Cliente.Checked ? 1 : 0;
            int i006_MostrarDescripcionPerfil = chk006_MostrarDescripcion_Perfil.Checked ? 1 : 0;             
            
            int i007_Programa = chk007_Programa.Checked ? 1 : 0;
            int i008_SoloEtiquetaProgama = chk008_SoloEtiquetaPrograma.Checked ? 1 : 0;
            int i009_SubPrograma = chk009_SubPrograma.Checked ? 1 : 0;
            int i010_SoloEtiquetaSubProgama = chk010_SoloEtiqueta_SubPrograma.Checked ? 1 : 0;

            int i011_Beneficiario = chk011_Beneficiario.Checked ? 1 : 0;
            int i012_SoloEtiqueta_Beneficiario = chk012_SoloEtiqueta_Beneficiario.Checked ? 1 : 0;
            int i013_FolioReferencia = chk013_FolioReferencia.Checked ? 1 : 0;
            int i014_SoloEtiqueta_FolioReferencia = chk014_SoloEtiqueta_FolioReferencia.Checked ? 1 : 0;
            int i015_FolioDocumento = chk015_FolioDocumento.Checked ? 1 : 0;
            int i016_SoloEtiqueta_FolioReferencia = chk016_SoloEtiqueta_FolioDocumento.Checked ? 1 : 0;

            int i017_Presentacion_ContenidoPaquete = chk017_Presentacion.Checked ? 1 : 0;

            string sSql = ""; 
            sSql = string.Format("Exec spp_Mtto_CFGC_Titulos_Reportes_Detallado_Venta " + 
                " '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', " +
                " '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, cboUnidades.Data,
                i001_Cliente, i002_SoloEtiqueta_Cliente, i003_SubCliente, i004_SoloEtiqueta_SubCliente,
                i005_IntercambiarSubCliente_Cliente, i006_MostrarDescripcionPerfil,
                i007_Programa, i008_SoloEtiquetaProgama, i009_SubPrograma, i010_SoloEtiquetaSubProgama,
                i011_Beneficiario, i012_SoloEtiqueta_Beneficiario, i013_FolioReferencia, i014_SoloEtiqueta_FolioReferencia,
                i015_FolioDocumento, i016_SoloEtiqueta_FolioReferencia, i017_Presentacion_ContenidoPaquete);


            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GuardarConfiguracion()");
                General.msjError("Ocurrió un error al guardar la configuración."); 
            }
            else
            {
                General.msjUser("Configuración guardada satisfactoriamente.");
                btnNuevo_Click(null, null);
            }
        }

        private void CargarConfiguracion()
        {
            string sSql =
                string.Format("If Not Exists ( Select * From CFGC_Titulos_Reportes_Detallado_Venta (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' ) \n " +
                " Insert Into CFGC_Titulos_Reportes_Detallado_Venta ( IdEmpresa, IdEstado, IdFarmacia ) Select '{0}', '{1}', '{2}' \n\n ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, cboUnidades.Data); 
                
                
            sSql += string.Format(
                "Select IdEmpresa, IdEstado, IdFarmacia, " + 
                    " Mostrar_Cliente, Mostrar_Solo_Etiqueta_Cliente, " + 
                    " Mostrar_SubCliente, Mostrar_Solo_Etiqueta_SubCliente, " + 
                    " Mostrar_SubCliente_Como_Cliente, " + 
                    " Mostrar_Descripcion_Perfil, " + 
                    " Mostrar_Programa, Mostrar_Solo_Etiqueta_Programa, " + 
                    " Mostrar_SubPrograma, Mostrar_Solo_Etiqueta_SubPrograma, " + 
                    " Mostrar_Beneficiario, Mostrar_Solo_Etiqueta_Beneficiario, " + 
                    " Mostrar_FolioReferencia, Mostrar_Solo_Etiqueta_FolioReferencia, " + 
                    " Mostrar_FolioDocumento, Mostrar_Solo_Etiqueta_FolioDocumento, " +
                    " Mostrar_Presentacion_ContenidoPaquete, " + 
                    " Actualizado " + 
                "From CFGC_Titulos_Reportes_Detallado_Venta " + 
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, cboUnidades.Data);

            Fg.IniciaControles(this, true, FramePrincipal); 

            if (!leer.Exec(sSql))
            {
            }
            else
            {
                cboUnidades.Enabled = false; 
                if (leer.Leer())
                {
                    // Cargar informacion 
                    chk001_Cliente.Checked = leer.CampoBool("Mostrar_Cliente");
                    chk002_SoloEtiqueta_Cliente.Checked = leer.CampoBool("Mostrar_Solo_Etiqueta_Cliente"); 
                    chk003_SubCliente.Checked = leer.CampoBool("Mostrar_SubCliente");
                    chk004_SoloEtiqueta_SubCliente.Checked = leer.CampoBool("Mostrar_Solo_Etiqueta_SubCliente");
                    chk005_IntercambiarSubCliente_Cliente.Checked = leer.CampoBool("Mostrar_SubCliente_Como_Cliente");
                    chk006_MostrarDescripcion_Perfil.Checked = leer.CampoBool("Mostrar_Descripcion_Perfil");  
                    chk007_Programa.Checked = leer.CampoBool("Mostrar_Programa");
                    chk008_SoloEtiquetaPrograma.Checked = leer.CampoBool("Mostrar_Solo_Etiqueta_Programa"); 
                    chk009_SubPrograma.Checked = leer.CampoBool("Mostrar_SubPrograma");
                    chk010_SoloEtiqueta_SubPrograma.Checked = leer.CampoBool("Mostrar_Solo_Etiqueta_SubPrograma"); 
                    chk011_Beneficiario.Checked = leer.CampoBool("Mostrar_Beneficiario");
                    chk012_SoloEtiqueta_Beneficiario.Checked = leer.CampoBool("Mostrar_Solo_Etiqueta_Beneficiario"); 
                    chk013_FolioReferencia.Checked = leer.CampoBool("Mostrar_FolioReferencia");
                    chk014_SoloEtiqueta_FolioReferencia.Checked = leer.CampoBool("Mostrar_Solo_Etiqueta_FolioReferencia");                    
                    chk015_FolioDocumento.Checked = leer.CampoBool("Mostrar_FolioDocumento");
                    chk016_SoloEtiqueta_FolioDocumento.Checked = leer.CampoBool("Mostrar_Solo_Etiqueta_FolioDocumento");
                    chk017_Presentacion.Checked = leer.CampoBool("Mostrar_Presentacion_ContenidoPaquete");   
                }
            }
        }
        #endregion Funciones y Procedimientos Privados
    }
}
