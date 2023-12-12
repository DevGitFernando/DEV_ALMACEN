using System;
using System.IO;
using System.Xml;

using SC_SolutionsSystem;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Errores;

namespace Dll_IFacturacion.CFDI.geCFD
{
    public class clsTimbreFDInfo
    {
        public DateTime FechaTimbrado;
        public string lastError = "";
        public string rfcProvCertif = "";
        public string noCertificadoSAT = "";
        public string selloCFD = "";
        public string selloSAT = "";
        public string UUID = "";
        public string version = "";

        public bool loadInfo(string xmlString)
        {
            return loadInfo(xmlString, eVersionCFDI.Version__3_2); 
        }

        public bool loadInfo(string xmlString, eVersionCFDI VersionCFDI)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(xmlString);
                foreach (XmlNode node in document.DocumentElement.ChildNodes)
                {
                    if (node.Name.ToLower() == "cfdi:complemento")
                    {
                        foreach (XmlNode node2 in node.ChildNodes)
                        {
                            if (node2.Name.ToLower() == "tfd:timbrefiscaldigital")
                            {
                                if (VersionCFDI == eVersionCFDI.Version__3_2)
                                {
                                    this.version = node2.Attributes.GetNamedItem("version").Value;
                                    this.UUID = node2.Attributes.GetNamedItem("UUID").Value;
                                    this.FechaTimbrado = DateTime.Parse(node2.Attributes.GetNamedItem("FechaTimbrado").Value);
                                    this.selloCFD = node2.Attributes.GetNamedItem("selloCFD").Value;
                                    this.noCertificadoSAT = node2.Attributes.GetNamedItem("noCertificadoSAT").Value;
                                    this.selloSAT = node2.Attributes.GetNamedItem("selloSAT").Value;
                                    return true;
                                }


                                if (VersionCFDI == eVersionCFDI.Version__3_3 || VersionCFDI == eVersionCFDI.Version__4_0)
                                {
                                    this.version = node2.Attributes.GetNamedItem("Version").Value;
                                    this.UUID = node2.Attributes.GetNamedItem("UUID").Value;
                                    this.FechaTimbrado = DateTime.Parse(node2.Attributes.GetNamedItem("FechaTimbrado").Value);
                                    this.selloCFD = node2.Attributes.GetNamedItem("SelloCFD").Value;
                                    this.noCertificadoSAT = node2.Attributes.GetNamedItem("NoCertificadoSAT").Value;
                                    this.selloSAT = node2.Attributes.GetNamedItem("SelloSAT").Value;
                                    this.rfcProvCertif = node2.Attributes.GetNamedItem("RfcProvCertif").Value;
                                    return true;
                                }
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return false;
            }
        }

        public bool loadInfoFromFile(string xmlFile)
        {
            try
            {
                string xmlString = File.ReadAllText(xmlFile);
                return this.loadInfo(xmlString);
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                clsGrabarError.LogFileError("Lectura de XML"); 
                clsGrabarError.LogFileError(lastError); 
                return false;
            }
        }
    }
}

