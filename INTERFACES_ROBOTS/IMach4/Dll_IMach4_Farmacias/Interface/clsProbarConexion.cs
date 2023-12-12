using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

using Dll_IMach4;
using Dll_IMach4.Protocolos;


namespace Dll_IMach4.Interface
{
    public static class clsProbarConexion
    {
        const int TmMinuto = 60;
        const int TmSegundo = 1000;

        static int iTiempo = TmSegundo * (45);
        static Timer temporizador;

        #region Constructor de Clase
        static clsProbarConexion()
        {
        }
        #endregion Constructor de Clase

        #region Propiedades
        public static int TiempoEnSegundos
        {
            get { return 0; }
            set { iTiempo = 0; }
        }

        public static int TiempoEnMinutos
        {
            get { return 0; }
            set { iTiempo = 0; }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos
        public static void Iniciar()
        {
        }

        public static void Detener()
        {
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        private static void Probar_R(object sender, EventArgs e)
        {
        }
        #endregion Funciones y Procedimientos Privados
    }
}
