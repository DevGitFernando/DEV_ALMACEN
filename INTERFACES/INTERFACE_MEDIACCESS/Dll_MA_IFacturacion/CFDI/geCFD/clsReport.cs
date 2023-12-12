using System;
using System.Collections;

using Dll_MA_IFacturacion;
using Dll_MA_IFacturacion.CFDI;

namespace Dll_MA_IFacturacion.CFDI.geCFD
{
    public class clsReport
    {
        public ArrayList InformacionAduanera = new ArrayList();
        private long manioAprobacion = 0L;
        private string mefectoComprobante = "";
        private int mestadoComprobante = -1;
        private DateTime mfechaHoraExpedicion;
        private long mfolio;
        private string mlastError = "";
        private double mmontoIVATrasladado;
        private double mmontoTotal;
        private long mnoAprobacion;
        private string mrfcReceptor;
        private string mserie;

        public string getData()
        {
            if (!this.isOK())
            {
                return "";
            }
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            if (this.anioAprobacion > 0L)
            {
                str5 = this.anioAprobacion.ToString();
            }
            str = ((((((((str + "|" + this.rfcReceptor) + "|" + this.serie) + "|" + this.folio.ToString()) + "|" + str5 + this.noAprobacion.ToString()) + "|" + this.fechaHoraExpedicion.ToString("dd/MM/yyyy HH:mm:ss")) + "|" + this.montoTotal.ToString("#.00")) + "|" + this.montoIVATrasladado.ToString("#.00")) + "|" + this.estadoComprobante.ToString()) + "|" + this.efectoComprobante.ToString();
            foreach (clsInformacionAduanera aduanera in this.InformacionAduanera)
            {
                if (str2 != "")
                {
                    str2 = str2 + ",";
                }
                str2 = str2 + aduanera.numero.Replace("|", "");
                if (str3 != "")
                {
                    str3 = str3 + ",";
                }
                str3 = str3 + aduanera.fecha.ToString("dd/MM/yyyy");
                if (str4 != "")
                {
                    str4 = str4 + ",";
                }
                str4 = str4 + aduanera.aduana.Replace("|", "");
            }
            return ((((str + "|" + str2) + "|" + str3) + "|" + str4) + "|");
        }

        public bool isOK()
        {
            if (this.rfcReceptor == null)
            {
                this.rfcReceptor = "";
            }
            if ((this.rfcReceptor.Length < 12) && (this.rfcReceptor.Length > 13))
            {
                this.lastError = "Valor incorrecto en campo rfc";
                return false;
            }
            if (this.serie == null)
            {
                this.serie = "";
            }
            if (this.serie.Length > 10)
            {
                this.lastError = "Valor incorrecto en campo serie. M\x00e1ximo 10 caract\x00e9res.";
                return false;
            }
            if (this.folio < 1L)
            {
                this.lastError = "Valor incorrecto en campo folio.";
                return false;
            }
            if (this.noAprobacion < 1L)
            {
                this.lastError = "Valor incorrecto en campo noAprobacion.";
                return false;
            }
            if (this.montoTotal < 0.0)
            {
                this.lastError = "Valor incorrecto en campo montoTotal.";
                return false;
            }
            if (this.montoIVATrasladado < 0.0)
            {
                this.lastError = "Valor incorrecto en campo montoIVATrasladado.";
                return false;
            }
            if ((this.estadoComprobante != 0) && (this.estadoComprobante != 1))
            {
                this.lastError = "Valor incorrecto en campo estadoComprobante. 0=Cancelado y 1=Vigente";
                return false;
            }
            if (((this.efectoComprobante != "I") && (this.efectoComprobante != "E")) && (this.efectoComprobante != "T"))
            {
                this.lastError = "Valor incorrecto en campo efectoComprobante. I=Ingreso, E=Egreso y T=Traslado";
                return false;
            }
            foreach (clsInformacionAduanera aduanera in this.InformacionAduanera)
            {
                if (!aduanera.isOK())
                {
                    this.lastError = aduanera.lastError;
                    return false;
                }
                if (aduanera.numero.Length > 15)
                {
                    this.lastError = "Valor incorrecto en n\x00famero de pedimento aduanal. Valor m\x00e1ximo 15 caracteres.";
                    return false;
                }
            }
            return true;
        }

        public long anioAprobacion
        {
            get
            {
                return this.manioAprobacion;
            }
            set
            {
                this.manioAprobacion = value;
            }
        }

        public string efectoComprobante
        {
            get
            {
                return this.mefectoComprobante;
            }
            set
            {
                if (((value != "I") && (value != "E")) && (value != "T"))
                {
                    value = "";
                }
                this.mefectoComprobante = value;
            }
        }

        public int estadoComprobante
        {
            get
            {
                return this.mestadoComprobante;
            }
            set
            {
                if ((value != 0) && (value != 1))
                {
                    value = -1;
                }
                this.mestadoComprobante = value;
            }
        }

        public DateTime fechaHoraExpedicion
        {
            get
            {
                return this.mfechaHoraExpedicion;
            }
            set
            {
                this.mfechaHoraExpedicion = value;
            }
        }

        public long folio
        {
            get
            {
                return this.mfolio;
            }
            set
            {
                this.mfolio = value;
            }
        }

        public string lastError
        {
            get
            {
                return this.mlastError;
            }
            set
            {
                this.mlastError = value.Trim();
            }
        }

        public double montoIVATrasladado
        {
            get
            {
                return this.mmontoIVATrasladado;
            }
            set
            {
                this.mmontoIVATrasladado = value;
                this.mmontoIVATrasladado = Math.Round(this.mmontoIVATrasladado, 2);
            }
        }

        public double montoTotal
        {
            get
            {
                return this.mmontoTotal;
            }
            set
            {
                this.mmontoTotal = value;
                this.mmontoTotal = Math.Round(this.mmontoTotal, 2);
            }
        }

        public long noAprobacion
        {
            get
            {
                return this.mnoAprobacion;
            }
            set
            {
                this.mnoAprobacion = value;
            }
        }

        public string rfcReceptor
        {
            get
            {
                return this.mrfcReceptor;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                this.mrfcReceptor = value;
                this.mrfcReceptor = this.mrfcReceptor.Replace("|", "");
            }
        }

        public string serie
        {
            get
            {
                return this.mserie;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                this.mserie = value.Trim();
                this.mserie = this.mserie.ToUpper();
                this.mserie = this.mserie.Replace("|", "");
            }
        }
    }
}

