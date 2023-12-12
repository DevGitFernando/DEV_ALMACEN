using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Microsoft.VisualBasic;

namespace DtUtileriasBD
{
    public enum EstiloCaptura
    {
        Texto = 1, 
        Moneda = 2, 
        Porcentaje = 3, 
        Numerico = 4,
        FolioNumerico = 5
    }

    public class scTextBoxExt : TextBox
    {
        private string _SimboloMoneda = "$";
        private string _SimboloPorcentaje = "%";

        private EstiloCaptura tpTipoTexto = EstiloCaptura.Texto;
        private bool bPermitirApostrofo = false;
        // private bool bSoloDigitos = false;

        private string sSimboloMoneda = "$";
        private string sSimboloPorcentaje = "%";
        private string sSimbolo = "";
        private int iDecimales = 2;
        private bool bPermitirNegativos = false;
        private Color cForeColorNegativos = Color.Red;
        private bool bAutoSeleccionar = true;
        // private bool bFocused = false;

        // private double dValorNumerico = 0;
        private string sValorTexto = "";

        #region Constructor y Destructor de la Clase 
        public scTextBoxExt()
        {
            base.Text = sValorTexto;
        }
        #endregion Constructor y Destructor de la Clase

        #region Propiedades
        [Category("SC_Solutions"), DefaultValue(true), Browsable(true), Description("Indica si el control acepta el caracter apostrofo.")]
        public bool PermitirApostrofo
        {
            get { return bPermitirApostrofo; }
            set { bPermitirApostrofo = value; }
        }

        //public bool SoloDigitos
        //{
        //    get { return bSoloDigitos; }
        //    set { bSoloDigitos = value; }
        //}

        [Category("SC_Solutions"), DefaultValue(true), Browsable(true), Description("Determina el simbolo de Moneda para el control.")]
        public string SimboloMoneda
        {
            get { return sSimboloMoneda; }
            //set { sSimboloMoneda = value; }
        }

        [Category("SC_Solutions"), DefaultValue(true), Browsable(true), Description("Determina el simbolo de Porcentaje para el control.")]
        public string SimboloPorcentaje
        {
            get { return sSimboloPorcentaje; }
            //set { sSimboloPorcentaje = value; }
        }

        [Category("SC_Solutions"), DefaultValue(true), Browsable(true), Description("Especifica el número de lugares decimales.")]
        public int Decimales
        {
            get { return iDecimales; }
            set 
            { 
                iDecimales = value;
                this.Text = TextoAsignado("");
            }
        }

        [Category("SC_Solutions"), DefaultValue(true), Browsable(true), Description("Determina si el control acepta ó no números negativos.")]
        public bool PermitirNegativos
        {
            get { return bPermitirNegativos; }
            set { bPermitirNegativos = value; }
        }

        [Category("SC_Solutions"), DefaultValue(true), Browsable(true), Description("Determina el color que se usara para los valores negativos.")]
        public Color ColorFuenteNegativos
        {
            get { return cForeColorNegativos; }
            set { cForeColorNegativos = value; }
        }

        [Category("SC_Solutions"), DefaultValue(true), Browsable(true), Description("Determina el formato de texto válido para el control.")]
        public EstiloCaptura EstiloTexto
        {
            get { return tpTipoTexto; }
            set 
            {
                //bPermitirNegativos = false;
                //sSimboloMoneda = "$";
                //sSimboloPorcentaje = "%";

                base.TextAlign = HorizontalAlignment.Right;
                tpTipoTexto = value;
                sSimbolo = "";
                // sValorTexto = "0";

                if (tpTipoTexto == EstiloCaptura.Texto)
                {
                    bPermitirApostrofo = false;
                    sValorTexto = "";
                    base.TextAlign = HorizontalAlignment.Left;
                }
                else if (tpTipoTexto == EstiloCaptura.FolioNumerico)
                {
                    sValorTexto = "";
                    base.TextAlign = HorizontalAlignment.Center;
                }
                else if (tpTipoTexto == EstiloCaptura.Moneda)
                {
                    sSimboloMoneda = _SimboloMoneda;
                    sSimbolo = _SimboloMoneda;
                }
                else if (tpTipoTexto == EstiloCaptura.Porcentaje)
                {
                    sSimboloPorcentaje = _SimboloPorcentaje;
                    sSimbolo = _SimboloPorcentaje;
                }
                else if (tpTipoTexto == EstiloCaptura.Numerico)
                {
                    sSimboloMoneda = "";
                    sSimboloPorcentaje = "";
                }
                this.Text = sValorTexto;
            }
        }

        [Category("SC_Solutions"), DefaultValue(true), Browsable(true), Description("Determina si se selecciona automaticamente el contenido del control al recibir el foco.")]
        public bool AutoSeleccionar
        {
            get { return bAutoSeleccionar; }
            set { bAutoSeleccionar = value; }
        }
        
        #endregion Propiedades

        #region Override 
        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    //if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab)
        //    //{
        //    //    if (tpTipoTexto != EstiloCaptura.Texto)
        //    //        SendKeys.Send("{TAB}");
        //    //}
        //    //else if (e.KeyCode == Keys.Back)
        //    //    SendKeys.Send("+{TAB}"); 


        //    base.OnKeyDown(e);
        //}

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            //if (bSoloDigitos)
            //{
            //    if ( !validarTecla(e) )
            //        e.KeyChar = (char)0;
            //}
            //else
            //{
            //    if ( !bPermitirApostrofo )
            //    {
            //        if ( e.KeyChar.ToString() == "'" )
            //            e.KeyChar = (char)0;
            //    }
            //}

            switch (tpTipoTexto)
            {
                case EstiloCaptura.Texto:
                    if (!bPermitirApostrofo)
                    {
                        if (e.KeyChar.ToString() == "'")
                            e.KeyChar = (char)0;
                    }
                    break;

                case EstiloCaptura.FolioNumerico:
                    if (!validarTecla(e))
                        e.KeyChar = (char)0;
                    break;

                case EstiloCaptura.Moneda:
                case EstiloCaptura.Porcentaje:
                case EstiloCaptura.Numerico:
                    if (!validarTecla(e))
                        e.KeyChar = (char)0;
                    break;
            }

            // Asegurar que el control reciba el teclaso pulsado
            base.OnKeyPress(e);
            sValorTexto = base.Text;
            this.Text = sValorTexto;
            this.Refresh();
            ColorNegativos();
        }

        protected override void OnEnter(EventArgs e)
        {
            AutoSeleccionarContenido();
            base.OnEnter(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            sValorTexto = base.Text;
            if (sValorTexto.Length > 0)
            {
                if (sSimbolo != "")
                {
                    sValorTexto = sValorTexto.Replace(sSimbolo, "");
                }
            }

            sValorTexto = TextoAsignado(sValorTexto);
            ColorNegativos();
            base.Text = sValorTexto;
            // this.bFocused = false;
            base.OnLostFocus(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            // AutoSeleccionarContenido(); 
            base.OnMouseClick(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            AutoSeleccionarContenido();
            base.OnMouseDoubleClick(e);
        }

        //protected override Text Text
        //{
        //    get { return sValorTexto; }
        //    set 
        //    { 
        //        base.Text = TextoAsignado(value);
        //        ColorNegativos();
        //        this.Refresh();
        //    }
        //}

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = TextoAsignado(value);
                ColorNegativos();
                this.Refresh();
            }
        }

        public string Value
        {
            get { return sValorTexto.Replace(",", ""); }
        }

        #endregion Override 

        #region Funciones y Procedimientos Publicos
        public string TextoAsignado(string Valor)
        {
            string sRegresa = "";
            int ixDecimales = iDecimales;

            //if (Valor == "")
            //    Valor = sValorTexto;

            if (tpTipoTexto == EstiloCaptura.Texto || tpTipoTexto == EstiloCaptura.FolioNumerico)
            {
                sRegresa = Valor;
                sValorTexto = Valor;
            }
            else
            {
                //if (iDecimales > 0)
                //{
                //    //sPuntoDec = "." + Strings.;
                //}
                sRegresa = Valor;
                if (sRegresa == "")
                    sRegresa = "0";

                if ( tpTipoTexto == EstiloCaptura.FolioNumerico ) 
                    ixDecimales=0;

                try
                {
                    sRegresa = Strings.FormatNumber((object)sRegresa, ixDecimales, TriState.True, TriState.False, TriState.True);
                }
                catch
                {
                    sRegresa = Strings.FormatNumber((object)"0", ixDecimales, TriState.True, TriState.False, TriState.True); 
                }

                sValorTexto = sRegresa;
                if (tpTipoTexto == EstiloCaptura.Moneda)
                    sRegresa = sSimboloMoneda + "" + sRegresa;

                if (tpTipoTexto == EstiloCaptura.Porcentaje)
                    sRegresa = sSimboloPorcentaje + "" + sRegresa;

            }

            //base.Text = sRegresa;
            return sRegresa; 
        }

        private void AutoSeleccionarContenido()
        {
            if (base.Text.Length > 0)
            {
                if (sSimbolo != "")
                {
                    sValorTexto = base.Text.Replace(sSimbolo, "");
                }
            }
            
            base.Text = sValorTexto;
            ColorNegativos();
            if (bAutoSeleccionar)
            {
                //if (!this.bFocused)
                {
                    //this.bFocused = true;
                    base.SelectAll();
                }
            }
        }

        private void ColorNegativos()
        {
            base.ForeColor = Color.Black;
            try
            {
                string sDato = sValorTexto;

                if (sSimbolo != "")
                    sDato = sValorTexto.Replace(sSimbolo, "");

                if (Convert.ToDouble(sDato) < 0)
                {
                    if (bPermitirNegativos)
                        base.ForeColor = cForeColorNegativos;
                }
            }
            catch { }

            base.Refresh();
        }

        private bool validarTecla(KeyPressEventArgs e)
        {
            bool bRegresa = true;
            // e.KeyChar = '.';

            if (!Char.IsDigit(e.KeyChar))
            {
                switch (e.KeyChar)
                {
                    case '\b': // Retroceso
                        break;

                    case '\r': // Enter
                        break;

                    case (char)27: // ESC
                        break;

                    case (char)45: // -
                        if (!bPermitirNegativos)
                        {
                            bRegresa = false;
                        }
                        else
                        {
                            if (base.Text.Contains("-"))
                                bRegresa = false; 
                        }
                        break;

                    case (char)46: // .
                        if (iDecimales > 0)
                        {
                            if (base.Text.Contains("."))
                                bRegresa = false;
                        }
                        break;

                    default:
                        bRegresa = false;
                        break;
                }
            }

            return bRegresa;
        }
        #endregion Funciones y Procedimientos Publicos 
    }
}
