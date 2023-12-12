using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem.FP.Huellas;

namespace SC_SolutionsSystem.FP
{
    public class clsVerificarHuella
    {
        bool bMostrarMensaje = true;
        string sTituloDefault = "Verificación de huella digital"; 
        string sTitulo = "Verificación de huella digital"; 

        public clsVerificarHuella()
        { 
        }

        public string Titulo
        {
            get { return sTitulo; }
            set { sTitulo = value; }
        }

        public bool MostrarMensaje
        {
            get { return bMostrarMensaje; }
            set { bMostrarMensaje = value; }
        }

        public void SetTituloDefault()
        {
            sTitulo = sTituloDefault;
        }

        public void Show()
        {
            Show(bMostrarMensaje); 
        }

        public void Show(bool MostrarMensaje)
        {
            FP_General.HuellaCapturada = false;
            FP_General.HuellaRegistrada = false;
            FP_General.ExisteHuella = false; 

            VerificarHuella f = new VerificarHuella();
            f.Titulo = sTitulo; 
            f.ShowDialog();

            if (FP_General.HuellaCapturada)
            {
                if (FP_General.ExisteHuella)
                {
                    if (MostrarMensaje)
                    {
                        General.msjAviso("Huella digital encontrada.");
                    }
                }
                else
                {
                    if (MostrarMensaje)
                    {
                        General.msjAviso("La huella digital no encuentra.");
                    }
                }
            }
        }
    }
}
