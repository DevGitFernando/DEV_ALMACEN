using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Threading;
using System.ComponentModel;

using CryptoSysPKI;

using Dll_MA_IFacturacion;
using Dll_MA_IFacturacion.CFDI;

namespace Dll_MA_IFacturacion.CFDI.geCFD
{

    public class clsProccess
    {
        private string lastPFXFunctionErrorDesc = "";
        private string mlastError = "";

        public string createOriginalString(string xmlfilename, string xsltfilename)
        {
            this.lastError = "";
            XslCompiledTransform transform = new XslCompiledTransform();
            string resultsFile = Path.GetTempFileName() + ".xml";
            try
            {
                transform.Load(xsltfilename);
                transform.Transform(xmlfilename, resultsFile);
                string str2 = System.IO.File.ReadAllText(resultsFile);
                System.IO.File.Delete(resultsFile);
                return str2;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return null;
            }
        }

        public string createOriginalStringByXMLString(string xmlString, string xsltfilename)
        {
            string tempFileName = Path.GetTempFileName();
            System.IO.File.WriteAllText(tempFileName, xmlString);
            string str2 = "";
            str2 = this.createOriginalString(tempFileName, xsltfilename);
            try
            {
                System.IO.File.Delete(tempFileName);
            }
            catch (Exception)
            {
            }
            return str2;
        }

        private string decryptString(string data)
        {
            this.lastError = "";
            return data;
        }

        public string doBase64(byte[] data)
        {
            this.lastError = "";
            try
            {
                return Convert.ToBase64String(data);
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return null;
            }
        }

        public string doBase64FromFile(string filename)
        {
            this.lastError = "";
            try
            {
                byte[] data = System.IO.File.ReadAllBytes(filename);
                if (data == null)
                {
                    this.lastError = "Archivo vac\x00edo";
                    return null;
                }
                if (data.Length < 1)
                {
                    this.lastError = "Archivo vac\x00edo";
                    return null;
                }
                return this.doBase64(data);
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return null;
            }
        }

        public string doBase64FromString(string data)
        {
            this.lastError = "";
            try
            {
                byte[] bytes = Encoding.Default.GetBytes(data);
                return this.doBase64(bytes);
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return null;
            }
        }

        public byte[] doMD5(byte[] data)
        {
            this.lastError = "";
            return MD5.Create().ComputeHash(data);
        }

        public byte[] doRSAWith(byte[] data, string privateKeyFileName, string pwd, CryptoSysPKI.HashAlgorithm hashAlgorithm)
        {
            this.lastError = "";
            try
            {
                string privateKeyFile = privateKeyFileName;
                StringBuilder builder = new StringBuilder(pwd);
                privateKeyFileName = null;
                pwd = null;
                StringBuilder builder2 = Rsa.ReadEncPrivateKey(privateKeyFile, builder.ToString());
                if (builder2.Length < 1)
                {
                    return null;
                }
                int keyBytes = Rsa.KeyBytes(builder2.ToString());
                if (keyBytes < 1)
                {
                    return null;
                }
                byte[] buffer = Rsa.EncodeMsgForSignature(keyBytes, data, hashAlgorithm);
                if (buffer.Length < 1)
                {
                    return null;
                }
                buffer = Rsa.RawPrivate(buffer, builder2.ToString());
                if (buffer.Length < 1)
                {
                    return null;
                }
                builder = null;
                builder2 = null;
                pwd = null;
                privateKeyFileName = null;
                return buffer;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message + " " + exception.Source;
                return null;
            }
        }

        public byte[] doRSAWithMd5(byte[] data, string privateKeyFileName, string pwd)
        {
            this.lastError = "";
            try
            {
                return this.doRSAWith(data, privateKeyFileName, pwd, CryptoSysPKI.HashAlgorithm.Md5);
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message + " " + exception.Source;
                return null;
            }
        }

        public byte[] doUTF8(string data)
        {
            byte[] bytes;
            this.lastError = "";
            try
            {
                bytes = new UTF8Encoding().GetBytes(data);
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                throw;
            }
            return bytes;
        }

        public double getCFDDocumentVersion(string xmlfile)
        {
            try
            {
                if (System.IO.File.Exists(xmlfile))
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(xmlfile);
                    if (document.DocumentElement.Prefix == this.getDefault_cfdi_Prefix())
                    {
                        document = null;
                        return 3.0;
                    }
                    document = null;
                    return 2.0;
                }
                this.lastError = "El archivo no existe.";
                return 0.0;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return 0.0;
            }
        }

        public string getDefault_cfdi_Prefix()
        {
            return "cfdi";
        }

        public string getDefault_cfdi_URI()
        {
            return "http://www.sat.gob.mx/cfd/3";
        }

        public string getDigitalSeal(string xmlfilename, string xsltfilename, string privateKeyFileName, string password)
        {
            return getDigitalSeal(xmlfilename, xsltfilename, privateKeyFileName, password, "3.2");
        }

        public string getDigitalSeal(string xmlfilename, string xsltfilename, string privateKeyFileName, string password, string version)
        {
            byte[] buffer;
            this.lastError = "";
            string pwd = ""; 
            string data = this.createOriginalString(xmlfilename, xsltfilename);

            switch (data)
            {
                case null:
                case "":
                    this.lastError = "Error al crear cadena original. " + this.lastError;
                    return null;
            }

            byte[] buffer2 = this.doUTF8(data);
            if (buffer2 == null)
            {
                this.lastError = "Error la generar utf-8. " + this.lastError;
                return null;
            }

            if (buffer2.Length < 1)
            {
                this.lastError = "Error al generar utf-8. " + this.lastError;
                return null;
            }

            pwd = password; //AQUI this.decryptString(password);
            if (DateTime.Now.Year < 0x7db)
            {
                buffer = this.doRSAWithMd5(buffer2, privateKeyFileName, pwd);
            }
            else
            {
                buffer = this.doRSAWith(buffer2, privateKeyFileName, pwd, CryptoSysPKI.HashAlgorithm.Sha1);
            }

            //////////// Cambios version 3.3 
            if (version == "3.2")
            {
                buffer = this.doRSAWith(buffer2, privateKeyFileName, pwd, CryptoSysPKI.HashAlgorithm.Sha1);
            }

            if (version == "3.3")
            {
                buffer = this.doRSAWith(buffer2, privateKeyFileName, pwd, CryptoSysPKI.HashAlgorithm.Sha256);
            }
            //////////// Cambios version 3.3 


            pwd = "00000111111";
            if (buffer == null)
            {
                this.lastError = "Error al aplicar RSA. " + this.lastError;
                return null;
            }

            if (buffer.Length < 1)
            {
                this.lastError = "Error al aplicar RSA. " + this.lastError;
                return null;
            }

            string str2 = this.doBase64(buffer);
            switch (str2)
            {
                case null:
                case "":
                    this.lastError = "Error al aplicar Base64. " + this.lastError;
                    return null;
            }

            password = null;
            privateKeyFileName = null;
            return str2;
        }

        public string getLastPFXFunctionErrorDesc()
        {
            return this.lastPFXFunctionErrorDesc;
        }

        public string getMD5(string data)
        {
            string str2;
            try
            {
                MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
                string str = "";
                byte[] buffer = provider.ComputeHash(Encoding.Default.GetBytes(data));
                for (int i = 0; i < buffer.Length; i++)
                {
                    str = str + buffer[i].ToString("x2");
                }
                provider = null;
                str2 = str;
            }
            catch (Exception)
            {
                throw;
            }
            return str2;
        }

        public string getTempFileName()
        {
            return Path.GetTempFileName();
        }

        public bool makePFX(string outputFileName, string cerFile, string privateKeyFile, string password, string friendlyName, bool excludePrivateKey)
        {
            Exception exception;
            this.lastPFXFunctionErrorDesc = "";
            long num = -1L;
            try
            {
                num = Pfx.MakeFile(outputFileName, cerFile, privateKeyFile, password, friendlyName, excludePrivateKey);
                if (num == 0L)
                {
                    return true;
                }
                try
                {
                    this.lastPFXFunctionErrorDesc = CryptoSysPKI.General.LastError();
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                }
                return false;
            }
            catch (Exception exception2)
            {
                exception = exception2;
                this.lastPFXFunctionErrorDesc = exception.Message;
                return false;
            }
        }

        public bool saveStrBase64toFile(string dataB64, string filename)
        {
            this.lastError = "";
            try
            {
                byte[] bytes = Convert.FromBase64String(dataB64);
                System.IO.File.WriteAllBytes(filename, bytes);
                return true;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return false;
            }
        }

        public bool sendeMail(string smtpServer, int port, int timeout, bool useSSL, string user, string pwd, string fromMail, string displayName, string toMail, string ccMail, string subject, string body, string extbody, string attachFiles)
        {
            return this.sendeMail2(smtpServer, port, timeout, useSSL, user, pwd, fromMail, displayName, toMail, ccMail, subject, body, extbody, attachFiles, "");
        }

        public bool sendeMail2(string smtpServer, int port, int timeout, bool useSSL, string user, string pwd, string fromMail, string displayName, string toMail, string ccMail, string subject, string body, string extbody, string attachFiles, string replyTo)
        {
            string[] strArray2;
            SmtpClient client = new SmtpClient();
            MailMessage message = new MailMessage();

            try
            {



                client.Host = smtpServer;
                client.Port = port;
                client.Credentials = new NetworkCredential(user, pwd);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = useSSL;
                client.Timeout = timeout * 0x3e8;

                if (toMail == null)
                {
                    toMail = "";
                }

                if (toMail.Contains(";"))
                {
                    strArray2 = null;
                    strArray2 = toMail.Split(new char[] { ';' });
                    foreach (string str in strArray2)
                    {
                        if (str.Trim() != "" && str.Trim() != ";")
                        {
                            message.To.Add(new MailAddress(str));
                        }
                    }
                }
                else
                {
                    message.To.Add(new MailAddress(toMail));
                }

                message.From = new MailAddress(fromMail, displayName, Encoding.UTF8);
                if (ccMail == null)
                {
                    ccMail = "";
                }

                if (ccMail != "")
                {
                    if (ccMail.Contains(";"))
                    {
                        strArray2 = null;
                        strArray2 = ccMail.Split(new char[] { ';' });
                        foreach (string str in strArray2)
                        {
                            if (str.Trim() != "" && str.Trim() != ";")
                            {
                                message.CC.Add(new MailAddress(str));
                            }
                        }
                    }
                    else
                    {
                        message.CC.Add(new MailAddress(ccMail));
                    }
                }

                if (replyTo == null)
                {
                    replyTo = "";
                }

                if (replyTo != "")
                {
                    message.ReplyTo = new MailAddress(replyTo);
                }

                if (subject == null)
                {
                    subject = "";
                }

                if (subject == "")
                {
                    subject = "(Ninguno)";
                }

                message.Subject = subject;
                message.Body = body + "\n\r" + extbody;
                if (attachFiles == null)
                {
                    attachFiles = "";
                }

                if (attachFiles != "")
                {
                    if (attachFiles.Contains(";"))
                    {
                        string[] strArray = null;
                        strArray = attachFiles.Split(new char[] { ';' });
                        foreach (string str2 in strArray)
                        {
                            message.Attachments.Add(new Attachment(str2));
                        }
                    }
                    else
                    {
                        message.Attachments.Add(new Attachment(attachFiles));
                    }
                }
                else
                {
                    this.lastError = "Ningún archivo adjunto.";
                }

                ////client.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);
                client.Send(message);
                ////client.SendAsync(message, null); 
                return true;
            }
            catch (Exception exception)
            {
                ////client.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);

                this.lastError = exception.Message;
                return false;
            }
        }

        public bool setAddendaInXMLCFDI(string xmlCfdiFile, clsMyElement MyAddenda)
        {
            try
            {
                XmlDocument cfd = new XmlDocument();
                cfd.Load(xmlCfdiFile);
                XmlElement newChild = MyAddenda.getElement(cfd);
                XmlNode node = null;
                foreach (XmlNode node2 in cfd.DocumentElement.ChildNodes)
                {
                    if (node2.LocalName.ToLower() == "addenda")
                    {
                        node = node2;
                        break;
                    }
                }
                if (node == null)
                {
                    cfd.DocumentElement.AppendChild(newChild);
                }
                else
                {
                    foreach (XmlNode node3 in newChild.ChildNodes)
                    {
                        node.AppendChild(node3);
                    }
                }
                cfd.Save(xmlCfdiFile);
                return true;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return false;
            }
        }

        public string toUTF8String(string stext)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(stext);
            return encoding.GetString(bytes);
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
    }
}

