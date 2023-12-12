using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem.FP.Huellas;

namespace SC_SolutionsSystem.FP
{
    public class clsLeerHuella
    {
        bool bMostrarMensaje = true;
        string sTituloDefault = "Lectura de huella digital"; 
        string sTitulo = "Lectura de huella digital"; 

        public clsLeerHuella()
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

            LeerHuella f = new LeerHuella();
            f.Titulo = sTitulo; 
            f.ShowDialog();

            if (FP_General.HuellaCapturada)
            {
                if (!FP_General.ExisteHuella)
                {
                    if (FP_General.HuellaRegistrada)
                    {
                        if (MostrarMensaje)
                        {
                            General.msjUser("Huella registrada satisfactoriamente.");
                        }
                    }
                    else
                    {
                        if (MostrarMensaje)
                        {
                            General.msjError("No fue posible registrar la huela digital.");
                        }
                    }
                }
                else
                {
                    if (MostrarMensaje)
                    {
                        General.msjAviso("La huella digital ya se encuentra registrada.");
                    }
                }
            }
        }
    }
}
