using System;
using System.Collections.Generic;
using System.Text;

namespace Dll_IFacturacion.XSA.ObservacionesCancelacion
{
    public class clsObservaciones_CancelacionCFDI
    {
        string sEncabezado = "Cancelación de CFDI";
        string sObservaciones = "";
        bool bExito = false;
        int iLargoObservaciones = 200;
        int iMax = 7800;
        int iMin = 1;
        FrmObservaciones f;

        public string RFC_Receptor = "";
        public string ClaveMotivoCancelacion = "";
        public string TipoDocumento = "";
        public string Serie = "";
        public string Folio = "";
        public string UUID_Relacionado = "";

        #region Constructores y Destructor de la Clase 
        public clsObservaciones_CancelacionCFDI()
        {
        }

        public clsObservaciones_CancelacionCFDI(string Encabezado)
        {
            sEncabezado = Encabezado;
        }

        ~clsObservaciones_CancelacionCFDI()
        {
            f = null;
        }
        #endregion Constructores y Destructor de la Clase

        #region Propiedades
        public string Encabezado
        {
            get { return sEncabezado; }
            set { sEncabezado = value; }
        }

        public string Observaciones
        {
            get { return sObservaciones; }
            set { sObservaciones = value; }
        }

        public bool Exito
        {
            get { return bExito; }
            set { bExito = value; }
        }

        public int MaxLength
        {
            get { return iLargoObservaciones; }
            set
            {
                if (value > iMax)
                {
                    iLargoObservaciones = iMax;
                }
                else if ( value < iMin )
                {
                    iLargoObservaciones = iMin;
                }
                else
                {
                    iLargoObservaciones = value;
                }
            }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos 
        public void Show()
        {
            f = new FrmObservaciones();
            f.Encabezado = sEncabezado;
            f.LargoTexto = iLargoObservaciones;

            f.RFC_Receptor = RFC_Receptor;
            f.TipoDocumento = TipoDocumento; 
            f.ShowDialog();

            sObservaciones = f.Observaciones;

            ClaveMotivoCancelacion = f.ClaveMotivoCancelacion;
            Serie = f.Serie;
            Folio = f.Folio;
            UUID_Relacionado = f.UUID_Relacionado;

            bExito = f.Aceptar;
        }
        #endregion Funciones y Procedimientos Publicos

    }
}
