using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

using Dll_IFacturacion.CFDI.EDOInvoice;
using Dll_IFacturacion.CFDI.geCFD;

namespace Dll_IFacturacion.CFDI.CFDFunctions
{
    public class clsGenCFD
    {             
        private SqlCommand command;
        public SqlConnection connection;
        public clsInfoEmisor infoEmisor;
        public cMain main = new cMain();
        public double pISH = 0.0;
        private long pkRangoFolioAprobado = 0L;
        private clsProccess proccess;

        private string certificadoBase64(string filename)
        {
            string str = this.proccess.doBase64FromFile(filename);
            if (str != null)
            {
                return str;
            }
            this.Log("(opcional) No se pudo incluir el certificado de sello digital. " + this.proccess.lastError);
            return null;
        }

        public bool DeleteFile(string fileName)
        {
            try
            {
                File.Delete(fileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //////private bool DocumentoAprobadoParaCFD(long pkVenta)
        //////{
        //////    string msg = "El folio usado no pertenece a un block de documentos para generaci\x00f3n de CFD.";
        //////    try
        //////    {
        //////        this.command.CommandText = "SELECT BlockDocumentos.uf_CFDFolio FROM BlockDocumentos INNER JOIN Comprobante ON BlockDocumentos.Sys_PK=Comprobante.Block WHERE Comprobante.Sys_PK=" + pkVenta;
        //////        object obj2 = this.command.ExecuteScalar();
        //////        if (obj2 == null)
        //////        {
        //////            this.Log(msg);
        //////            return false;
        //////        }
        //////        if (Convert.ToInt64(obj2) > 0L)
        //////        {
        //////            return true;
        //////        }
        //////        this.Log(msg);
        //////        return false;
        //////    }
        //////    catch (Exception)
        //////    {
        //////        this.Log(msg);
        //////        return false;
        //////    }
        //////}

        public bool esInvalido(object v)
        {
            return this.esInvalido(v, "", false, false);
        }

        public bool esInvalido(object v, string serror)
        {
            return this.esInvalido(v, serror, false, false);
        }

        public bool esInvalido(object v, string serror, bool mayor_que_cero)
        {
            return this.esInvalido(v, serror, mayor_que_cero, false);
        }

        public bool esInvalido(object v, string serror, bool mayor_que_cero, bool diferente_vacio)
        {
            try
            {
                if (v == null)
                {
                    if (serror != "")
                    {
                        this.Log(serror);
                    }
                    return true;
                }
                if (mayor_que_cero)
                {
                    if (Convert.ToDouble(v) > 0.0)
                    {
                        return false;
                    }
                    if (serror != "")
                    {
                        this.Log(serror);
                    }
                    return true;
                }
                if (diferente_vacio)
                {
                    if (v.ToString() != "")
                    {
                        return false;
                    }
                    if (serror != "")
                    {
                        this.Log(serror);
                    }
                    return true;
                }
                return false;
            }
            catch (Exception exception)
            {
                this.Log(exception.Message);
                return true;
            }
        }

        private void Log(string msg)
        {
            //this.main.showMsg("Error al genear CFD.\n" + msg, MessageBoxIcon.Exclamation);
        }

        public int NDecsMonto()
        {
            try
            {
                return 2; // Convert.ToInt32(this.main.getVarStrValue(clsVars.varAdicDecimalesPrecisionXML()));
            }
            catch (Exception)
            {
                return 2;
            }
        }

        public string obtenerUbicacionCorrecta(string v)
        {
            try
            {
                if ((v.ToLower() == "(desconocido)") || (v.ToLower() == "desconocido"))
                {
                    return "";
                }
                return v;
            }
            catch (Exception)
            {
                return v;
            }
        }

        public double roundValue(double v)
        {
            return Math.Round(v, this.NDecsMonto());
        }

        public string sCondicionesDePago(int FormaPago)
        {
            switch (FormaPago)
            {
                case 0:
                    return "CREDITO";

                case 1:
                    return "INMEDIATO";
            }
            return "";
        }

        ////public string sCondicionesDePago2(clsInfoVenta infoVenta)
        ////{
        ////    return this.main.stringFieldValue(infoVenta.Comprobante, "XMLCondicionesPago");
        ////}

        ////public string sFormaDePago(clsInfoVenta infoVenta)
        ////{
        ////    try
        ////    {
        ////        string str = this.main.stringFieldValue(infoVenta.Comprobante, "XMLFormaPago");
        ////        if (str == "")
        ////        {
        ////            str = "PAGO EN UNA SOLA EXHIBICIÓN";
        ////        }
        ////        return str;
        ////    }
        ////    catch (Exception)
        ////    {
        ////        return "";
        ////    }
        ////}

        public string sTipoComprobante(long documento)
        {
            long num = documento;
            if ((num <= 3L) && (num >= 1L))
            {
                switch (((int) (num - 1L)))
                {
                    case 0:
                        return "ingreso";

                    case 1:
                        return "egreso";

                    case 2:
                        return "ingreso";
                }
            }
            return "";
        }

    }
}

