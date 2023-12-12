﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;

namespace DllFarmaciaSoft.Usuarios_y_Permisos
{
    public partial class FrmGrupos : FrmBaseExt
    {
        public int iIdGrupo = 0;
        public string IdEstado = "";
        public string IdFarmacia = "";
        public string sNombreGrupo = "";
        public bool bAceptar = false;

        // clsGuardarSC Guardar = new clsGuardarSC();
        // clsGruposDeUsuarios GrupoUsuario;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        public FrmGrupos()
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn);

        }

        private void FrmGrupos_Load(object sender, EventArgs e)
        {
            txtNombreGrupo.Text = sNombreGrupo;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bAceptar = false;
            this.Hide();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            sNombreGrupo = txtNombreGrupo.Text;
            string sSql = string.Format("Exec spp_Net_Grupos_De_Usuarios_Mtto '{0}', '{1}', '{2}', '{3}' ", IdEstado, IdFarmacia, iIdGrupo, sNombreGrupo);

            if (!leer.Exec(sSql))
            {
                General.msjError("Ocurrió un errro al registrar el grupo.");
            }
            else
            {
                bAceptar = true;
                this.Hide();
            }


            //GrupoUsuario = new clsGruposDeUsuarios();

            //GrupoUsuario.IdSucursal = General.EntidadConectada;
            //GrupoUsuario.IdGrupo = iIdGrupo;
            //GrupoUsuario.NombreGrupo = txtNombreGrupo.Text;

            //if (Guardar.Exec(GrupoUsuario.PreparaSp()))
            //{
            //    bAceptar = true;
            //    this.Hide();
            //}
        }
    }
}
