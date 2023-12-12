using System;
using System.Collections; 
using System.Collections.Generic;
using System.Text;

using UpdaterOficinaCentralRegional.FuncionesGenerales; 

namespace UpdaterOficinaCentralRegional 
{
    public class clsScritpSQL
    {
        string sScriptBase = "";
        ArrayList pScripts;
        basGenerales Fg = new basGenerales();
        string sSeparador = "--#SQL";
        string sSeparadorDiseño = "Go--#SQL"; 
        bool bWithOutEncryption = false;

        public clsScritpSQL(string ScriptBase, bool NoUseEncryption)
        {
            sScriptBase = ScriptBase;
            bWithOutEncryption = NoUseEncryption;
            DescomponerScript(sScriptBase);
        }

        public ArrayList ListaScripts
        {
            get { return pScripts; }
        }

        private void DescomponerScript(string Script)
        {
            string sFragmento = "";
            pScripts = new ArrayList();

            // string sScriptCadena = Script;
            string sScriptCadena = Script.Replace(sSeparadorDiseño, sSeparador); 

            int iPosInicial = 1;
            int iPosFinal = 0;

            while (iPosFinal >= 0)
            {
                try
                {
                    iPosFinal = sScriptCadena.ToLower().IndexOf(sSeparador.ToLower());
                    sFragmento = Fg.Mid(sScriptCadena, iPosInicial, iPosFinal);
                    sScriptCadena = Fg.Mid(sScriptCadena, iPosFinal + sSeparador.Length + 1).Trim();
                    //iPosInicial = iPosFinal + sSeparador.Length + 1 ;

                    // Procesar script sin encrypcion 
                    // Dar tratamiento al script 
                    if (bWithOutEncryption)
                    {
                        clsWithEncryption x = new clsWithEncryption(sFragmento);
                        sFragmento = x.Script;
                    }

                    // Segmento de Script 
                    pScripts.Add(sFragmento);
                    if (sScriptCadena.Length <= 0)
                        break;
                }
                catch
                {
                    break;
                }
            }
        }
    }

    public class clsWithEncryption
    {
        string sScriptOriginal = "";
        string sScript = "";

        public clsWithEncryption(string Script)
        {
            sScriptOriginal = Script;
            Procesar();
        }

        public string Script
        {
            get { return sScript; }
        }

        private void Procesar()
        {
            string[] sToken = sScriptOriginal.Split('\r');
            string sT = "";

            foreach (string Token in sToken)
            {
                sT = Token;
                if (sT.ToUpper().Contains("With".ToUpper()))
                {
                    sT = sT.ToUpper();
                    sT = sT.Replace("WITH", "--WITH");
                }

                sScript += sT;
            }
        }
    }
}
