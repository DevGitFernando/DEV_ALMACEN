using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

namespace DllFarmaciaSoft
{
    public static class CURP
    {
        private static int iAño = 0;
        private static int iMes = 0;
        private static int iDia = 0;

        private static TextBox año = new TextBox();
        private static TextBox mes = new TextBox();
        private static TextBox dia = new TextBox();
        private static TextBox apellidoM = new TextBox();
        private static TextBox apellidoP = new TextBox();
        private static TextBox caracter1 = new TextBox();
        private static TextBox caracter10 = new TextBox();
        private static TextBox caracter11 = new TextBox();
        private static TextBox caracter12 = new TextBox();
        private static TextBox caracter13 = new TextBox();
        private static TextBox caracter14 = new TextBox();
        private static TextBox caracter15 = new TextBox();
        private static TextBox caracter16 = new TextBox();
        private static TextBox caracter17 = new TextBox();
        private static TextBox caracter18 = new TextBox();
        private static TextBox caracter2 = new TextBox();
        private static TextBox caracter3 = new TextBox();
        private static TextBox caracter4 = new TextBox();
        private static TextBox caracter5 = new TextBox();
        private static TextBox caracter6 = new TextBox();
        private static TextBox caracter7 = new TextBox();
        private static TextBox caracter8 = new TextBox();
        private static TextBox caracter9 = new TextBox();
        private static TextBox _curp = new TextBox();
        private static TextBox nombre = new TextBox();
        private static TextBox nombre2 = new TextBox();
        private static basGenerales Fg = new basGenerales();

        private static DataSet dtsEstados;
        static object[] estados; // = new object[,]
        ////    {
        ////        {"",""}, 
        ////        {"AGUASCALIENTES","AS"},
        ////        {"BAJA CALIFORNIA","BC"},
        ////        {"BAJA CALIFORNIA SUR","BS"},
        ////        {"CAMPECHE","CC"},
        ////        {"CHIAPAS","CS"},
        ////        {"CHIHUAHUA","CH"},
        ////        {"COAHUILA","CL"},
        ////        {"COLIMA","CM"},
        ////        {"DISTRITO FEDERAL","DF"},
        ////        {"DURANGO","DG"},
        ////        {"GUANAJUATO","GT"},
        ////        {"GUERRERO","GR"},
        ////        {"HIDALGO","HG"},
        ////        {"JALISCO","JC"},
        ////        {"MEXICO","MC"},
        ////        {"MICHOACAN","MN"},
        ////        {"MORELOS","MS"},
        ////        {"NAYARIT","NT"},
        ////        {"NUEVO LEON","NL"},
        ////        {"OAXACA","OC"},
        ////        {"PUEBLA","PL"},
        ////        {"QUERETARO","QT"},
        ////        {"QUINTANA ROO","QR"},
        ////        {"SAN LUIS POTOSI","SP"},
        ////        {"SINALOA","SL"},
        ////        {"SONORA","SR"},
        ////        {"TABASCO","TC"},
        ////        {"TAMAULIPAS","TS"},
        ////        {"TLAXCALA","TL"},
        ////        {"VERACRUZ","VZ"},
        ////        {"YUCATÁN","YN"},
        ////        {"ZACATECAS","ZS"},
        ////        {"NACIDO EXTRANJERO","NE"}
        ////    };

        #region Constructor de Clase 
        static CURP()
        {
            ListaEstados();
        }
        #endregion Constructor de Clase

        public static string Generar_CURP( string ApellidoPaterno, string ApellidoMaterno,
            string Nombre, string NombreDos, DateTime FechaNacimiento, string Sexo, int IdEstado )
        {
            string sRegresa = "";

            sRegresa = Generar_CURP(ApellidoPaterno, ApellidoMaterno, Nombre, NombreDos, General.FechaYMD(FechaNacimiento), Sexo, IdEstado);

            return sRegresa;
        }

        public static string Generar_CURP( string ApellidoPaterno, string ApellidoMaterno,
            string Nombre, string NombreDos, string FechaNacimiento, string Sexo, int IdEstado )
        {
            string sRegresa = "";
            DateTime dtFechaNac = new DateTime();
            string[] sNombres = Nombre.Split(' ');

            apellidoP.Text = ApellidoPaterno.ToUpper();
            apellidoM.Text = ApellidoMaterno.ToUpper();
            nombre.Text = Nombre.ToUpper();
            nombre2.Text = NombreDos.ToUpper();

            iAño = Convert.ToInt32("0" + Fg.Left(FechaNacimiento, 4));
            iMes = Convert.ToInt32("0" + Fg.Mid(FechaNacimiento, 6, 2));
            iDia = Convert.ToInt32("0" + Fg.Right(FechaNacimiento, 2));
            dtFechaNac = new DateTime(iAño, iMes, iDia);

            try
            {
                string str;
                string str2 = Convert.ToString(apellidoP.Text[0]);
                if(apellidoP.Text[0] != '\x00d1')
                {
                    caracter1.Text = Convert.ToString(str2);
                }
                else
                {
                    caracter1.Text = "X";
                }

                int num2 = 1;
                while(num2 < apellidoP.Text.Length)
                {
                    if((((apellidoP.Text[num2] == 'A') || (apellidoP.Text[num2] == 'E')) || ((apellidoP.Text[num2] == 'I') || (apellidoP.Text[num2] == 'O'))) || (apellidoP.Text[num2] == 'U'))
                    {
                        caracter2.Text = Convert.ToString(apellidoP.Text[num2]);
                        break;
                    }
                    if((apellidoP.Text.Length - 1) == num2)
                    {
                        caracter2.Text = "X";
                        break;
                    }
                    num2++;
                }

                if((apellidoM.Text != "") && (apellidoM.Text[0] != '\x00d1'))
                {
                    string str3 = Convert.ToString(apellidoM.Text[0]);
                    caracter3.Text = Convert.ToString(str3);
                }
                else
                {
                    caracter3.Text = "X";
                }

                if((((nombre.Text[0] == 'M') && (nombre.Text[1] == 'A')) && (((nombre.Text[2] == 'R') && (nombre.Text[3] == 'I')) && (nombre.Text[4] == 'A'))) || ((((nombre.Text[0] == 'J') && (nombre.Text[1] == 'O')) && ((nombre.Text[2] == 'S') && (nombre.Text[3] == 'E'))) && (nombre2.Text != "")))
                {
                    caracter4.Text = "X";
                    if(sNombres.Length > 1)
                    {
                        nombre2.Text = sNombres[1];
                        if(nombre2.Text[0] != '\x00d1')
                        {
                            str = Convert.ToString(nombre2.Text[0]);
                            caracter4.Text = Convert.ToString(str);
                        }
                        else
                        {
                            caracter4.Text = "X";
                        }
                    }
                }
                else if(nombre.Text[0] != '\x00d1')
                {
                    str = Convert.ToString(nombre.Text[0]);
                    caracter4.Text = Convert.ToString(str);
                }
                else
                {
                    caracter4.Text = "X";
                }

                año.Text = iAño.ToString();
                string str4 = Convert.ToString(año.Text[2]);
                caracter5.Text = Convert.ToString(str4);
                string str5 = Convert.ToString(año.Text[3]);
                caracter6.Text = Convert.ToString(str5);

                //// Determinar el año 
                if(iAño > 0x7cf)
                {
                    caracter17.Text = "A";
                }
                else
                {
                    caracter17.Text = "0";
                }

                //// Determinar el mes  
                string sMes = Fg.PonCeros(iMes, 2);
                caracter7.Text = Fg.Left(sMes, 1);
                caracter8.Text = Fg.Right(sMes, 1);
                ;

                ////if (iMes > 8)
                ////{
                ////    caracter7.Text = "1";
                ////    caracter8.Text = Convert.ToString((int)(iMes - 9));
                ////}
                ////else
                ////{
                ////    caracter7.Text = "0";
                ////    caracter8.Text = Convert.ToString((int)(iMes + 1));
                ////}

                //// Determinar el dia  
                string sDia = Fg.PonCeros(iDia, 2);
                caracter9.Text = Fg.Left(sDia, 1);
                caracter10.Text = Fg.Right(sDia, 1);
                ;

                ////if (iDia < 9)
                ////{
                ////    caracter9.Text = "0";
                ////    caracter10.Text = Convert.ToString((int)(iDia + 1));
                ////}
                ////if ((iDia > 8) && (iDia < 0x13))
                ////{
                ////    caracter9.Text = "1";
                ////    caracter10.Text = Convert.ToString((int)(iDia - 9));
                ////}
                ////if ((iDia > 0x12) && (iDia < 0x1d))
                ////{
                ////    caracter9.Text = "2";
                ////    caracter10.Text = Convert.ToString((int)(iDia - 0x13));
                ////}
                ////if (iDia > 0x1c)
                ////{
                ////    caracter9.Text = "3";
                ////    caracter10.Text = Convert.ToString((int)(iDia - 0x1d));
                ////}

                //// Determinar el Sexo 
                if(Sexo.ToUpper() == "Hombre".ToUpper())
                {
                    caracter11.Text = "H";
                }
                else
                {
                    caracter11.Text = "M";
                }

                //// Determinar el Estado 
                string sEstado = ClaveEstado(IdEstado);
                caracter12.Text = Fg.Left(sEstado, 1);
                caracter13.Text = Fg.Right(sEstado, 1);

                //// Determinar el apellido paterno 
                for(num2 = 1; num2 < apellidoP.Text.Length; num2++)
                {
                    if((((apellidoP.Text[num2] == 'A') || (apellidoP.Text[num2] == 'E')) || ((apellidoP.Text[num2] == 'I') || (apellidoP.Text[num2] == 'O'))) || (apellidoP.Text[num2] == 'U'))
                    {
                        if((apellidoP.Text.Length - 1) == num2)
                        {
                            caracter14.Text = "X";
                            break;
                        }
                    }
                    else
                    {
                        if(apellidoP.Text[num2] != '\x00d1')
                        {
                            caracter14.Text = Convert.ToString(apellidoP.Text[num2]);
                            break;
                        }
                        if(apellidoP.Text[num2] == '\x00d1')
                        {
                            caracter14.Text = "X";
                            break;
                        }
                    }
                }

                //// Determinar el apellido materno 
                if(apellidoM.Text != "")
                {
                    for(num2 = 1; num2 < apellidoM.Text.Length; num2++)
                    {
                        if((((apellidoM.Text[num2] == 'A') || (apellidoM.Text[num2] == 'E')) || ((apellidoM.Text[num2] == 'I') || (apellidoM.Text[num2] == 'O'))) || (apellidoM.Text[num2] == 'U'))
                        {
                            if((apellidoM.Text.Length - 1) == num2)
                            {
                                caracter15.Text = "X";
                                break;
                            }
                        }
                        else
                        {
                            if(apellidoM.Text[num2] != '\x00d1')
                            {
                                caracter15.Text = Convert.ToString(apellidoM.Text[num2]);
                                break;
                            }
                            if(apellidoM.Text[num2] == '\x00d1')
                            {
                                caracter15.Text = "X";
                                break;
                            }
                        }
                    }
                }
                else
                {
                    caracter15.Text = "X";
                }

                for(num2 = 1; num2 < nombre.Text.Length; num2++)
                {
                    if((((nombre.Text[num2] == 'A') || (nombre.Text[num2] == 'E')) || ((nombre.Text[num2] == 'I') || (nombre.Text[num2] == 'O'))) || (nombre.Text[num2] == 'U'))
                    {
                        if((nombre.Text.Length - 1) == num2)
                        {
                            caracter16.Text = "X";
                            break;
                        }
                    }
                    else
                    {
                        if(nombre.Text[num2] != '\x00d1')
                        {
                            caracter16.Text = Convert.ToString(nombre.Text[num2]);
                        }
                        else
                        {
                            caracter16.Text = "X";
                        }
                        break;
                    }
                }

                char[] chArray = new char[] {
                            Convert.ToChar(caracter1.Text), Convert.ToChar(caracter2.Text), Convert.ToChar(caracter3.Text), Convert.ToChar(caracter4.Text), Convert.ToChar(caracter5.Text), Convert.ToChar(caracter6.Text), Convert.ToChar(caracter7.Text), Convert.ToChar(caracter8.Text), Convert.ToChar(caracter9.Text), Convert.ToChar(caracter10.Text), Convert.ToChar(caracter11.Text), Convert.ToChar(caracter12.Text), Convert.ToChar(caracter13.Text), Convert.ToChar(caracter14.Text), Convert.ToChar(caracter15.Text), Convert.ToChar(caracter16.Text),
                            Convert.ToChar(caracter17.Text)
                         };
                int[] numArray = new int[0x11];
                for(int k = 0; k < 0x11; k++)
                {
                    if(chArray[k] == '0')
                    {
                        numArray[k] = 0;
                    }
                    if(chArray[k] == '1')
                    {
                        numArray[k] = 1;
                    }
                    if(chArray[k] == '2')
                    {
                        numArray[k] = 2;
                    }
                    if(chArray[k] == '3')
                    {
                        numArray[k] = 3;
                    }
                    if(chArray[k] == '4')
                    {
                        numArray[k] = 4;
                    }
                    if(chArray[k] == '5')
                    {
                        numArray[k] = 5;
                    }
                    if(chArray[k] == '6')
                    {
                        numArray[k] = 6;
                    }
                    if(chArray[k] == '7')
                    {
                        numArray[k] = 7;
                    }
                    if(chArray[k] == '8')
                    {
                        numArray[k] = 8;
                    }
                    if(chArray[k] == '9')
                    {
                        numArray[k] = 9;
                    }
                    if(chArray[k] == 'A')
                    {
                        numArray[k] = 10;
                    }
                    if(chArray[k] == 'B')
                    {
                        numArray[k] = 11;
                    }
                    if(chArray[k] == 'C')
                    {
                        numArray[k] = 12;
                    }
                    if(chArray[k] == 'D')
                    {
                        numArray[k] = 13;
                    }
                    if(chArray[k] == 'E')
                    {
                        numArray[k] = 14;
                    }
                    if(chArray[k] == 'F')
                    {
                        numArray[k] = 15;
                    }
                    if(chArray[k] == 'G')
                    {
                        numArray[k] = 0x10;
                    }
                    if(chArray[k] == 'H')
                    {
                        numArray[k] = 0x11;
                    }
                    if(chArray[k] == 'I')
                    {
                        numArray[k] = 0x12;
                    }
                    if(chArray[k] == 'J')
                    {
                        numArray[k] = 0x13;
                    }
                    if(chArray[k] == 'K')
                    {
                        numArray[k] = 20;
                    }
                    if(chArray[k] == 'L')
                    {
                        numArray[k] = 0x15;
                    }
                    if(chArray[k] == 'M')
                    {
                        numArray[k] = 0x16;
                    }
                    if(chArray[k] == 'N')
                    {
                        numArray[k] = 0x17;
                    }
                    if(chArray[k] == '\x00d1')
                    {
                        numArray[k] = 0x18;
                    }
                    if(chArray[k] == 'O')
                    {
                        numArray[k] = 0x19;
                    }
                    if(chArray[k] == 'P')
                    {
                        numArray[k] = 0x1a;
                    }
                    if(chArray[k] == 'Q')
                    {
                        numArray[k] = 0x1b;
                    }
                    if(chArray[k] == 'R')
                    {
                        numArray[k] = 0x1c;
                    }
                    if(chArray[k] == 'S')
                    {
                        numArray[k] = 0x1d;
                    }
                    if(chArray[k] == 'T')
                    {
                        numArray[k] = 30;
                    }
                    if(chArray[k] == 'U')
                    {
                        numArray[k] = 0x1f;
                    }
                    if(chArray[k] == 'V')
                    {
                        numArray[k] = 0x20;
                    }
                    if(chArray[k] == 'W')
                    {
                        numArray[k] = 0x21;
                    }
                    if(chArray[k] == 'X')
                    {
                        numArray[k] = 0x22;
                    }
                    if(chArray[k] == 'Y')
                    {
                        numArray[k] = 0x23;
                    }
                    if(chArray[k] == 'Z')
                    {
                        numArray[k] = 0x24;
                    }
                }
                numArray[0] *= 0x12;
                numArray[1] *= 0x11;
                numArray[2] *= 0x10;
                numArray[3] *= 15;
                numArray[4] *= 14;
                numArray[5] *= 13;
                numArray[6] *= 12;
                numArray[7] *= 11;
                numArray[8] *= 10;
                numArray[9] *= 9;
                numArray[10] *= 8;
                numArray[11] *= 7;
                numArray[12] *= 6;
                numArray[13] *= 5;
                numArray[14] *= 4;
                numArray[15] *= 3;
                numArray[0x10] *= 2;

                int num6 = (((((((((((((((numArray[0] + numArray[1]) + numArray[2]) + numArray[3]) + numArray[4]) + numArray[5]) + numArray[6]) + numArray[7]) + numArray[8]) + numArray[9]) + numArray[10]) + numArray[11]) + numArray[12]) + numArray[13]) + numArray[14]) + numArray[15]) + numArray[0x10];
                num6 = num6 % 10;
                if(num6 == 0)
                {
                    caracter18.Text = "0";
                }
                else
                {
                    num6 = 10 - num6;
                }


                caracter18.Text = Convert.ToString(num6);
                _curp.Text = caracter1.Text + caracter2.Text + caracter3.Text + caracter4.Text + caracter5.Text + caracter6.Text + caracter7.Text + caracter8.Text + caracter9.Text + caracter10.Text + caracter11.Text + caracter12.Text + caracter13.Text + caracter14.Text + caracter15.Text + caracter16.Text + caracter17.Text + caracter18.Text;

                if(((((((caracter1.Text != "") && (caracter2.Text != "")) && ((caracter3.Text != "") && (caracter4.Text != ""))) && (((caracter5.Text != "") && (caracter6.Text != "")) && ((caracter7.Text != "") && (caracter8.Text != "")))) && ((((caracter9.Text != "") && (caracter10.Text != "")) && ((caracter11.Text != "") && (caracter12.Text != ""))) && (((caracter13.Text != "") && (caracter14.Text != "")) && ((caracter15.Text != "") && (caracter16.Text != ""))))) && (caracter17.Text != "")) && (caracter18.Text != ""))
                {
                    sRegresa = _curp.Text;
                }
                else
                {
                    sRegresa = "";
                }

            }
            catch(Exception ex)
            {
                sRegresa = ex.Message;
                sRegresa = "";
            }

            return sRegresa;
        }

        private static string ClaveEstado( int IdEstado )
        {
            string sRegresa = "";
            clsLeer leerEdo = new clsLeer();

            try
            {
                leerEdo.DataRowsClase = dtsEstados.Tables[0].Select(string.Format("ClaveEstado = '{0}'", IdEstado));
                if(leerEdo.Leer())
                {
                    sRegresa = leerEdo.Campo("ClaveRENAPO");
                }
            }
            catch { }

            return sRegresa;
        }

        internal class ClsEdo
        {
            public int IdEstado = 0;
            public string ClaveRenapo = "";
            public string Estado = "";

            public ClsEdo( int ClaveEstado, string NombreEdo, string RENAPO )
            {
                IdEstado = ClaveEstado;
                ClaveRenapo = RENAPO;
                Estado = NombreEdo;
            }
        }

        private static void ListaEstados()
        {
            dtsEstados = new DataSet("DtsEstados");
            DataTable dtTable = new DataTable("Estados");
            Dictionary<int, ClsEdo> list = new Dictionary<int, ClsEdo>();

            dtTable.Columns.Add("ClaveEstado", System.Type.GetType("System.String"));
            dtTable.Columns.Add("Estado", System.Type.GetType("System.String"));
            dtTable.Columns.Add("ClaveRENAPO", System.Type.GetType("System.String"));

            object[] obj_00 = { 0, "", "" };
            object[] obj_01 = { 1, "AGUASCALIENTES", "AS" };
            object[] obj_02 = { 2, "BAJA CALIFORNIA", "BC" };
            object[] obj_03 = { 3, "BAJA CALIFORNIA", "BC" };
            object[] obj_04 = { 4, "CAMPECHE", "CC" };
            object[] obj_05 = { 5, "CHIAPAS", "CS" };
            object[] obj_06 = { 6, "CHIHUAHUA", "CH" };
            object[] obj_07 = { 7, "COAHUILA", "CL" };
            object[] obj_08 = { 8, "COLIMA", "CM" };
            object[] obj_09 = { 9, "DISTRITO FEDERAL", "DF" };
            object[] obj_10 = { 10, "DURANGO", "DG" };
            object[] obj_11 = { 11, "GUANAJUATO", "GT" };
            object[] obj_12 = { 12, "GUERRERO", "GR" };
            object[] obj_13 = { 13, "HIDALGO", "HG" };
            object[] obj_14 = { 14, "JALISCO", "JC" };
            object[] obj_15 = { 15, "MEXICO", "MC" };
            object[] obj_16 = { 16, "MICHOACAN", "MN" };
            object[] obj_17 = { 17, "MORELOS", "MS" };
            object[] obj_18 = { 18, "NAYARIT", "NT" };
            object[] obj_19 = { 19, "NUEVO LEON", "NL" };
            object[] obj_20 = { 20, "OAXACA", "OC" };
            object[] obj_21 = { 21, "PUEBLA", "PL" };
            object[] obj_22 = { 22, "QUERETARO", "QT" };
            object[] obj_23 = { 23, "QUINTANA ROO", "QR" };
            object[] obj_24 = { 24, "SAN LUIS POTOSI", "SP" };
            object[] obj_25 = { 25, "SINALOA", "SL" };
            object[] obj_26 = { 26, "SONORA", "SR" };
            object[] obj_27 = { 27, "TABASCO", "TC" };
            object[] obj_28 = { 28, "TAMAULIPAS", "TS" };
            object[] obj_29 = { 29, "TLAXCALA", "TL" };
            object[] obj_30 = { 30, "VERACRUZ", "VZ" };
            object[] obj_31 = { 31, "YUCATÁN", "YN" };
            object[] obj_32 = { 32, "ZACATECAS", "ZS" };
            object[] obj_33 = { 33, "NACIDO EXTRANJERO", "NE" };

            dtTable.Rows.Add(obj_00);
            dtTable.Rows.Add(obj_01);
            dtTable.Rows.Add(obj_02);
            dtTable.Rows.Add(obj_03);
            dtTable.Rows.Add(obj_04);
            dtTable.Rows.Add(obj_05);
            dtTable.Rows.Add(obj_06);
            dtTable.Rows.Add(obj_07);
            dtTable.Rows.Add(obj_08);
            dtTable.Rows.Add(obj_09);
            dtTable.Rows.Add(obj_10);
            dtTable.Rows.Add(obj_11);
            dtTable.Rows.Add(obj_12);
            dtTable.Rows.Add(obj_13);
            dtTable.Rows.Add(obj_14);
            dtTable.Rows.Add(obj_15);
            dtTable.Rows.Add(obj_16);
            dtTable.Rows.Add(obj_17);
            dtTable.Rows.Add(obj_18);
            dtTable.Rows.Add(obj_19);
            dtTable.Rows.Add(obj_20);
            dtTable.Rows.Add(obj_21);
            dtTable.Rows.Add(obj_22);
            dtTable.Rows.Add(obj_23);
            dtTable.Rows.Add(obj_24);
            dtTable.Rows.Add(obj_25);
            dtTable.Rows.Add(obj_26);
            dtTable.Rows.Add(obj_27);
            dtTable.Rows.Add(obj_28);
            dtTable.Rows.Add(obj_29);
            dtTable.Rows.Add(obj_30);
            dtTable.Rows.Add(obj_31);
            dtTable.Rows.Add(obj_32);
            dtTable.Rows.Add(obj_33);

            dtsEstados.Tables.Add(dtTable.Copy());
        }
    }
}
