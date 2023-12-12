using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.FuncionesGenerales;

namespace DllFarmaciaSoft.Lotes
{
    /// <summary>
    /// Clase para filtrar los caracteres válidos para el manejo de lotes.
    /// </summary>
    public class clsValidarLote
    {
        TextBox capturaLote; 
        char charAsterisco = '*';
        bool bValidarLetrasAlInicio = false;
        int iLetrasAlInicio = 3; 

        public clsValidarLote(TextBox Control) 
        {
            capturaLote = Control; 
            capturaLote.KeyPress += new KeyPressEventHandler(OnKeyPress);
            
            ContextMenuStrip blankContextMenu = new ContextMenuStrip();
            Control.ContextMenuStrip = blankContextMenu; 
        }

        public bool ValidarLetrasAlInicio
        {
            get { return bValidarLetrasAlInicio; }
            set { bValidarLetrasAlInicio = value; }
        }

        public int NoLetrasAlInicio
        {
            get { return iLetrasAlInicio; }
            set { iLetrasAlInicio = value; }
        }

        private bool validarTecla(KeyPressEventArgs e) 
        {
            bool bRegresa = true; 
            // e.KeyChar = '.'; 

            if (!(Char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == charAsterisco )) 
            {
                switch (e.KeyChar) 
                {
                    case (char)8: // Retroceso 
                        break; 

                    case (char)13: // Enter 
                        break; 

                    case (char)27: // ESC 
                        break; 

                    ////case (char)45: // - 
                    ////    break; 

                    ////case (char)46: // . 
                    ////    break; 

                    case (char)72: // Flecha arriba  
                        break; 

                    case (char)80: // Flecha abajo 
                        break; 

                    case (char)75: // Flecha izquierda  
                        break; 

                    case (char)77: // Flecha derecha 
                        break; 

                    default: 
                        bRegresa = false; 
                        break; 
                } 
            } 

            return bRegresa; 
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!validarTecla(e))
            {
                e.KeyChar = (char)0;
            }
            else
            {
                if (bValidarLetrasAlInicio)
                {
                    if (capturaLote.Text.Length <= iLetrasAlInicio - 1)
                    {
                        if (char.IsLetter(e.KeyChar))
                        {
                            e.KeyChar = (char)0;
                        }
                    }
                }
            }
        }
    }
}
