using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Criptografia;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FP;
using SC_SolutionsSystem.FP.Huellas;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;


namespace Almacen.Catalogos
{
    public partial class FrmHuellasPersonal : FrmBaseExt 
    {
        enum Cols
        {
            NumDedo = 1, 
            Mano = 2, Dedo = 3, Registrado = 4, FechaRegistro = 5, Status = 6 
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsListView lst;

        string sReferencia_Huella = "";
        string sIdPersonal = "";
        string sNombrePersonal = "";


        public FrmHuellasPersonal()
        {
            InitializeComponent();
            
            leer = new clsLeer(ref cnn);
            lst = new clsListView(lstvHuellas); 
        }

        private void FrmHuellasPersonal_Load(object sender, EventArgs e)
        {
            lblNombre.Text = string.Format("{0} - {1}", sIdPersonal, sNombrePersonal); 
            CargarHuellasRegistradas();
            FP_General.Conexion = General.DatosConexion;
        }

        public void RegistrarHuellas(string Referencia_Huella, string IdPersonal, string Nombre)
        {
            sReferencia_Huella = Referencia_Huella;
            sIdPersonal = IdPersonal;
            sNombrePersonal = Nombre; 

            this.ShowDialog(); 
        }

        private void CargarHuellasRegistradas()
        {
            string sSql = string.Format("Exec spp_Get_Huellas_Cedis_Personal '{0}' ", sReferencia_Huella);

            lst.LimpiarItems(); 
            if (!leer.Exec(sSql))
            {
            }
            else
            {
                lst.CargarDatos(leer.DataSetClase, false, false); 
            }
        }

        private void btnCapturarHuella_Click(object sender, EventArgs e)
        {
            FP_General.Referencia_Huella = sReferencia_Huella; 
            FP_General.Dedo = (Dedos)lst.GetValueInt(lst.RenglonActivo, (int)Cols.NumDedo);
            FP_General.StoreRegistroHuellas = "spp_Registrar_Huellas_Cedis";
            FP_General.TablaHuellas = "FP_Huellas_Cedis";

            clsLeerHuella f = new clsLeerHuella();
            f.Show();

            if (FP_General.HuellaRegistrada)
            {
                CargarHuellasRegistradas();
            }
        }

        private void verificarHuellaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FP_General.Referencia_Huella = sReferencia_Huella;
            FP_General.Dedo = (Dedos)lst.GetValueInt(lst.RenglonActivo, (int)Cols.NumDedo);

            clsVerificarHuella f = new clsVerificarHuella();
            f.Show(); 
        }

        private void btnCancelarHuella_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "";
            int iDedo = lst.GetValueInt(lst.RenglonActivo, (int)Cols.NumDedo);
            string sRegistrado = lst.GetValue(lst.RenglonActivo, (int)Cols.Registrado);
            string sStatusActual = lst.GetValue(lst.RenglonActivo, (int)Cols.Status);
            string message = "¿ Desea Cancelar la huella seleccionada ?";

            if (sRegistrado.Trim() == "SI")
            {
                if (sStatusActual == "ACTIVA")
                {
                    if (General.msjCancelar(message) == DialogResult.Yes)
                    {
                        if (cnn.Abrir())
                        {
                            cnn.IniciarTransaccion();

                            sSql = String.Format("Exec spp_Mtto_Activar_Desactivar_Huella_Cedis_Personal '{0}', '{1}', 'C' ", sReferencia_Huella, iDedo);

                            if (!leer.Exec(sSql))
                            {
                                cnn.DeshacerTransaccion();
                                Error.GrabarError(leer, "btnCancelarHuella_Click");
                                General.msjError("Error al guardar Información.");
                            }
                            else
                            {
                                if (leer.Leer())
                                {
                                    sMensaje = String.Format("{0}", leer.Campo("Mensaje"));
                                }

                                cnn.CompletarTransaccion();
                                General.msjUser(sMensaje); //Este mensaje lo genera el SP
                                CargarHuellasRegistradas();
                            }

                            cnn.Cerrar();
                        }
                        else
                        {
                            General.msjAviso(General.MsjErrorAbrirConexion);
                        }
                    }
                }
                else
                {
                    General.msjUser("Huella Cancelada. Favor de verificar.");
                }
            }
            else
            {
                General.msjUser("Seleccione una huella registrada"); 
            }            
        }

        private void activarHuellaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "";
            int iDedo = lst.GetValueInt(lst.RenglonActivo, (int)Cols.NumDedo);
            string sRegistrado = lst.GetValue(lst.RenglonActivo, (int)Cols.Registrado);
            string sStatusActual = lst.GetValue(lst.RenglonActivo, (int)Cols.Status);
            string message = "¿ Desea Activar la huella seleccionada ?";

            if (sRegistrado.Trim() == "SI")
            {
                if (sStatusActual == "CANCELADA")
                {
                    if (General.msjCancelar(message) == DialogResult.Yes)
                    {
                        if (cnn.Abrir())
                        {
                            cnn.IniciarTransaccion();

                            sSql = String.Format("Exec spp_Mtto_Activar_Desactivar_Huella_Cedis_Personal '{0}', '{1}', 'A' ", sReferencia_Huella, iDedo);

                            if (!leer.Exec(sSql))
                            {
                                cnn.DeshacerTransaccion();
                                Error.GrabarError(leer, "activarHuellaToolStripMenuItem_Click");
                                General.msjError("Error al guardar Información.");
                            }
                            else
                            {
                                if (leer.Leer())
                                {
                                    sMensaje = String.Format("{0}", leer.Campo("Mensaje"));
                                }

                                cnn.CompletarTransaccion();
                                General.msjUser(sMensaje); //Este mensaje lo genera el SP
                                CargarHuellasRegistradas();
                            }

                            cnn.Cerrar();
                        }
                        else
                        {
                            General.msjAviso(General.MsjErrorAbrirConexion);
                        }
                    }
                }
                else
                {
                    General.msjUser("Huella Activa. Favor de verificar.");
                }
            }
            else
            {
                General.msjUser("Seleccione una huella registrada");
            }

        }

        private void btnBorrarHuella_Click(object sender, EventArgs e)
        {
            string sSql = "";
            int iDedo = lst.GetValueInt(lst.RenglonActivo, (int)Cols.NumDedo);
            string sRegistrado = lst.GetValue(lst.RenglonActivo, (int)Cols.Registrado);
            string sStatusActual = lst.GetValue(lst.RenglonActivo, (int)Cols.Status);
            string message = "¿ Desea borrar la huella seleccionada ?";

            if (sRegistrado.Trim() == "SI")
            {
                if (General.msjCancelar(message) == DialogResult.Yes)
                {
                    if (cnn.Abrir())
                    {
                        cnn.IniciarTransaccion();

                        sSql = String.Format("Exec spp_Mtto_Borrar_Huella_Cedis_Personal '{0}', '{1}'", sReferencia_Huella, iDedo);

                        if (!leer.Exec(sSql))
                        {
                            cnn.DeshacerTransaccion();
                            Error.GrabarError(leer, "btnBorrarHuella_Click");
                            General.msjError("Error al aplicar cambio.");
                        }
                        else
                        {
                            if (leer.Leer())
                            {
                                //sMensaje = String.Format("{0}", leer.Campo("Mensaje"));
                                General.msjUser("la Huella ha sido borrada exitosamente.");
                            }

                            cnn.CompletarTransaccion();
                            CargarHuellasRegistradas();
                        }

                        cnn.Cerrar();
                    }
                    else
                    {
                        General.msjAviso(General.MsjErrorAbrirConexion);
                    }
                }
            }
            else
            {
                General.msjUser("Seleccione una huella registrada");
            }
        }
    }
}
