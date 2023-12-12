using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_IGPI.Protocolos
{
    /// <summary>
    /// Almacena la Lista de Productos que se encuentran en Mach4.
    /// </summary>
    public static class clsI_K_Informacion
    {
        static DataTable dtRegistros;

        static clsI_K_Informacion()
        {
            PrepararRecepcion(); 
        }

        /// <summary>
        /// Inicializa la Lista de Productos Inventariados. 
        /// </summary>
        public static void PrepararRecepcion()
        {
            dtRegistros = new DataTable("InventarioRobot");
            dtRegistros.Columns.Add("CodigoEAN", System.Type.GetType("System.String"));
            dtRegistros.Columns.Add("Descripcion", System.Type.GetType("System.String"));
            dtRegistros.Columns.Add("Cantidad", System.Type.GetType("System.String"));  
        }

        /// <summary>
        /// Agrega la información recibida a la Lista de Productos. 
        /// </summary>
        /// <param name="Registro">Información a ser almacenada.</param>
        public static void Add(object []Registro)
        {
            dtRegistros.Rows.Add(Registro); 
        }

        public static DataSet Registros
        {
            get
            {
                DataSet dts = new DataSet("Inventario");
                dts.Tables.Add(dtRegistros.Copy());
                return dts;
            }
        }
    }

    public class clsI_K_Response_Registros
    {
        basGenerales Fg = new basGenerales(); 
        public string CodigoEAN = "";
        public string Cantidad = "";

        public clsI_K_Response_Registros(string Registro)
        {
            Registro = Registro.Replace("\0", " "); 
            CodigoEAN = Fg.Mid(Registro, 1, 20).Trim();
            Cantidad = Fg.Mid(Registro, 21, 5).Trim(); 
        }

        public object []Registro
        {
            get
            {
                object[] obj = { CodigoEAN, "", Cantidad };
                return obj;
            }
        }
    }

    public class clsI_K_Response
    {
        #region Declaracion de Varibles 
        // clsI_P_Response Response; 
        IGPIParametros pMach;
        basGenerales Fg = new basGenerales(); 
        string sSolicitudRecibida = "";
        string sRespuestaA_Solicitud = "";
        bool bResponder = false; 

        #endregion Declaracion de Varibles

        #region Constructores y Destructor de Clase 
        public clsI_K_Response(string Mensaje)
        {
            pMach = new IGPIParametros();
            pMach.Dialogo = "k";

            this.sSolicitudRecibida = Mensaje;
            DecodificarMensaje(sSolicitudRecibida);

            ////////// Preparar la respuesta 
            ////Response = new clsI_P_Response(pMach);
            ////sRespuestaA_Solicitud = Response.Respuesta;
        }

        ~clsI_K_Response()
        { 
        }
        #endregion Constructores y Destructor de Clase

        #region Propiedades Publicas 
        public string Dialogo
        {
            get { return pMach.Dialogo; }
            set { pMach.Dialogo = value; }
        }

        public string RequestLocationNumber
        {
            get { return pMach.RequestLocationNumber; }
            set { pMach.RequestLocationNumber = value; }
        }

        public string Respuesta
        {
            get { return sRespuestaA_Solicitud; }
        }

        public bool GenerarRespuesta
        {
            get { return bResponder; }
        }

        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private void DecodificarMensaje(string Mensaje)
        {
            bResponder = false;
            string sCodigoEAN_Response = "";
            basGenerales Fg = new basGenerales();
            string sMsj = Mensaje.Replace("\0", " ");
            int iDatos = 31; 
            int iVueltas = ((sMsj.Length - iDatos) / 30);
            clsI_K_Response_Registros[] k = new clsI_K_Response_Registros[iVueltas];

            StreamWriter fileInventario = new StreamWriter(IGPI.ArchivoSalida_K, true);
            fileInventario.WriteLine(Mensaje);
            fileInventario.WriteLine(""); 

            // Dll_IGPI.Registros_K = 0; 
            pMach.Dialogo = pMach.Cortar(ref sMsj, 1).ToUpper();
            pMach.RequestLocationNumber = pMach.Cortar(ref sMsj, 3);

            pMach.CountryCode = pMach.Cortar(ref sMsj, 3);
            pMach.TypeCode = pMach.Cortar(ref sMsj, 2);
            pMach.ProductCode = pMach.Cortar(ref sMsj, 20);
            //pMach.Cortar(ref sMsj, 25);
            pMach.LineNumber = pMach.Cortar(ref sMsj, 2);

            ////pMach.CountryCode = pMach.Cortar(ref sMsj, 3);
            ////pMach.TypeCode = pMach.Cortar(ref sMsj, 2);
            ////pMach.ProductCode = pMach.Cortar(ref sMsj, 20);
            ////pMach.CountryCode = pMach.Cortar(ref sMsj, 3);
            ////pMach.TypeCode = pMach.Cortar(ref sMsj, 2);
            ////pMach.ProductCode = pMach.Cortar(ref sMsj, 20);
            ////pMach.Quantity = pMach.Cortar(ref sMsj, 5);

            for (int i = 0; i <= iVueltas - 1; i++)
            {
                bResponder = true;
                
                pMach.CountryCode = pMach.Cortar(ref sMsj, 3);
                pMach.TypeCode = pMach.Cortar(ref sMsj, 2);
                k[i] = new clsI_K_Response_Registros(pMach.Cortar(ref sMsj, 25));

                pMach.ProductCode = k[i].CodigoEAN;
                fileInventario.WriteLine(k[i].CodigoEAN); 

                // Agregar el Registro de Inventario 
                clsI_K_Informacion.Add(k[i].Registro); 
                //Dll_IGPI.Registros_K++; 
            }

            ////pMach.Dialogo = pMach.Cortar(ref sMsj, 1);
            ////pMach.RequestLocationNumber = pMach.Cortar(ref sMsj, 3); 
            ////pMach.ProductCode = pMach.Cortar(ref sMsj, 20);
            ////pMach.Barcode = pMach.Cortar(ref sMsj, 20); 

            if (iVueltas < IGPI.Registros_K)
            {
                bResponder = false;
            }


            fileInventario.Close();



            sCodigoEAN_Response = pMach.ProductCode.Trim().Replace("\0", "");
            sCodigoEAN_Response = Fg.Right(Fg.PonFormato("", " ", 20) + sCodigoEAN_Response, 20);

            sCodigoEAN_Response = pMach.ProductCode.Trim().Replace("\0", "");
            sCodigoEAN_Response = Fg.Left(sCodigoEAN_Response + Fg.PonFormato("", " ", 20), 20); 
            ////pMach.CountryCode = IGPI.CountryCode;
            ////pMach.TypeCode = IGPI.TypeCode; 
            sRespuestaA_Solicitud = pMach.Dialogo + pMach.RequestLocationNumber + pMach.CountryCode + pMach.TypeCode + sCodigoEAN_Response + Fg.PonCeros(IGPI.Registros_K, 2); 
        }
        #endregion Funciones y Procedimientos Privados
    }
}
