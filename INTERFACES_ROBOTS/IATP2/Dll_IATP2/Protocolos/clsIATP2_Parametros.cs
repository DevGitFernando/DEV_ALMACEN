using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft; 

namespace Dll_IATP2.Protocolos
{
    public enum Espacio
    {
        P_01_RecordType = 1,
        P_02_CustomerBatchID = 50,
        P_03_PatientFirstName = 30,
        P_04_PatientLastName = 30,
        P_05_PatientIdentifier = 20,
        P_06_PatientDateOfBirth = 10,
        P_07_PatientAddress01 = 50,
        P_08_PatientAddress02 = 50,
        P_09_PatientCity = 30,
        P_10_PatientState = 20,
        P_11_PatientZipCode = 20,

        P_12_PatientPhoneNumber = 20,
        P_13_Insurence = 20,
        P_14_InsurenceGroup = 20,
        P_15_FacilityName = 30,
        P_16_FacilityIdentifier = 20,
        P_17_UnitOrWing = 20,
        P_18_RoomNumber = 20,
        P_19_BedNumber = 20,

        P_20_DoctorFirtsName = 30,
        P_21_DoctorLastName = 30,
        P_22_DoctorPhoneNumber = 20,
        P_23_DoctorIdentifier = 20,
        P_24_RPhInitials = 15,
        P_25_RxNumber = 30,

        P_26_RxQuantity = 10,
        P_27_RxFillDestination = 5,
        P_28_ReFillRemaining = 2,
        P_29_DaysSupply = 3,

        P_30_RefillMessage = 120,
        P_31_OriginalFillDate = 10,
        P_32_LastRefillDate = 10,
        P_33_NextRefillDate = 10,
        P_34_AdministrationDate = 10,
        P_35_AdministrationTime = 6,
        P_36_MedicationIdentifier = 20,
        P_37_InventoryNumber = 30,
        P_38_RxInstrucion = 1024,
        P_39_RxWarning = 1024,
        P_40_PrePack = 1,
        P_41_QuantityPerDose = 10,
        P_42_CustomFiel_01 = 20,
        P_43_CustomFiel_02 = 20,
        P_44_CustomFiel_03 = 20,
        P_45_CustomFiel_04 = 20,
        P_46_CustomFiel_05 = 20
    }

    public class clsIATP2_Parametros
    {

        #region Declaracion de Varibles
        basGenerales Fg = new basGenerales();


        DateTime dtP_06_PatientDateOfBirth = DateTime.Now;
        DateTime dtP_31_OriginalFillDate = DateTime.Now;
        DateTime dtP_32_LastRefillDate = DateTime.Now;
        DateTime dtP_33_NextRefillDate = DateTime.Now;
        DateTime dtP_34_AdministrationDate = DateTime.Now;
        DateTime dtP_35_AdministrationTime = DateTime.Now;



        string sP_01_RecordType = "";
        string sP_02_CustomerBatchID = "";
        string sP_03_PatientFirstName = "";
        string sP_04_PatientLastName = "";
        string sP_05_PatientIdentifier = "";
        string sP_06_PatientDateOfBirth = "";
        string sP_07_PatientAddress01 = "";
        string sP_08_PatientAddress02 = "";
        string sP_09_PatientCity = "";
        string sP_10_PatientState = "";
        string sP_11_PatientZipCode = "";

        string sP_12_PatientPhoneNumber = "";
        string sP_13_Insurence = "";
        string sP_14_InsurenceGroup = "";
        string sP_15_FacilityName = "";
        string sP_16_FacilityIdentifier = "";
        string sP_17_UnitOrWing = "";
        string sP_18_RoomNumber = "";
        string sP_19_BedNumber = "";

        string sP_20_DoctorFirtsName = "";
        string sP_21_DoctorLastName = "";
        string sP_22_DoctorPhoneNumber = "";
        string sP_23_DoctorIdentifier = "";
        string sP_24_RPhInitials = "";        
        string sP_25_RxNumber = "";

        string sP_26_RxQuantity = "";
        string sP_27_RxFillDestination = "";
        string sP_28_ReFillRemaining = "";
        string sP_29_DaysSupply = "";

        string sP_30_RefillMessage = "";
        string sP_31_OriginalFillDate = "";
        string sP_32_LastRefillDate = "";
        string sP_33_NextRefillDate = "";
        string sP_34_AdministrationDate = "";
        string sP_35_AdministrationTime = "";
        string sP_36_MedicationIdentifier = "";
        string sP_37_InventoryNumber = "";
        string sP_38_RxInstrucion = "";
        string sP_39_RxWarning = "";
        string sP_40_PrePack = "";
        string sP_41_QuantityPerDose = "";
        string sP_42_CustomFiel_01 = "";
        string sP_43_CustomFiel_02 = "";
        string sP_44_CustomFiel_03 = "";
        string sP_45_CustomFiel_04 = "";
        string sP_46_CustomFiel_05 = "";

        string sP_4 = "";
        // string sFormatoHumano = "028\0"; 

        string sFormato_01_Texto = "0"; 

        #endregion Declaracion de Varibles

        #region Constructores y Destructor de Clase
        public clsIATP2_Parametros()
        {
            Fg = new basGenerales(); 
        }

        ~clsIATP2_Parametros()
        {
        }
        #endregion Constructores y Destructor de Clase

        #region Funciones y Procedimientos Publicos
        public string Cortar(ref string Mensaje, Espacio Caracteres)
        {
            return Cortar(ref Mensaje, (int)Caracteres); 
        }

        public string Cortar(ref string Mensaje, int Caracteres)
        {
            return Cortar(ref Mensaje, 1, Caracteres);
        }

        public string Cortar(ref string Mensaje, int Inicio, int Caracteres)
        {
            string sRegresa = "";
            int iLargo = Mensaje.Length;
            try
            {
                sRegresa = Fg.Mid(Mensaje, Inicio, Caracteres);
                Mensaje = Fg.Right(Mensaje, iLargo - Caracteres);
            }
            catch { }

            return sRegresa;
        }

        public static DataSet PrepararDtsAcondicionamiento()
        {
            DataSet dts = new DataSet("OrdenDeAcondicionamiento");
            DataTable dtTable = new DataTable("Ordenes");


            dtTable.Columns.Add("P_01_RecordType", GetType(TypeCode.String));
            dtTable.Columns.Add("P_02_CustomerBatchID", GetType(TypeCode.String));
            dtTable.Columns.Add("P_03_PatientFirstName", GetType(TypeCode.String));
            dtTable.Columns.Add("P_04_PatientLastName", GetType(TypeCode.String));
            dtTable.Columns.Add("P_05_PatientIdentifier", GetType(TypeCode.String));
            dtTable.Columns.Add("P_06_PatientDateOfBirth", GetType(TypeCode.String));
            dtTable.Columns.Add("P_07_PatientAddress01", GetType(TypeCode.String));
            dtTable.Columns.Add("P_08_PatientAddress02", GetType(TypeCode.String));
            dtTable.Columns.Add("P_09_PatientCity", GetType(TypeCode.String));
            dtTable.Columns.Add("P_10_PatientState", GetType(TypeCode.String));

            dtTable.Columns.Add("P_11_PatientZipCode", GetType(TypeCode.String));
            dtTable.Columns.Add("P_12_PatientPhoneNumber", GetType(TypeCode.String));
            dtTable.Columns.Add("P_13_Insurence", GetType(TypeCode.String));
            dtTable.Columns.Add("P_14_InsurenceGroup", GetType(TypeCode.String));
            dtTable.Columns.Add("P_15_FacilityName", GetType(TypeCode.String));
            dtTable.Columns.Add("P_16_FacilityIdentifier", GetType(TypeCode.String));
            dtTable.Columns.Add("P_17_UnitOrWing", GetType(TypeCode.String));
            dtTable.Columns.Add("P_18_RoomNumber", GetType(TypeCode.String));
            dtTable.Columns.Add("P_19_BedNumber", GetType(TypeCode.String));
            dtTable.Columns.Add("P_20_DoctorFirtsName", GetType(TypeCode.String));

            dtTable.Columns.Add("P_21_DoctorLastName", GetType(TypeCode.String));
            dtTable.Columns.Add("P_22_DoctorPhoneNumber", GetType(TypeCode.String));
            dtTable.Columns.Add("P_23_DoctorIdentifier", GetType(TypeCode.String));
            dtTable.Columns.Add("P_24_RPhInitials", GetType(TypeCode.String));
            dtTable.Columns.Add("P_25_RxNumber", GetType(TypeCode.String));
            dtTable.Columns.Add("P_26_RxQuantity", GetType(TypeCode.String));
            dtTable.Columns.Add("P_27_RxFillDestination", GetType(TypeCode.String));
            dtTable.Columns.Add("P_28_ReFillRemaining", GetType(TypeCode.String));
            dtTable.Columns.Add("P_29_DaysSupply", GetType(TypeCode.String));
            dtTable.Columns.Add("P_30_RefillMessage", GetType(TypeCode.String));

            dtTable.Columns.Add("P_31_OriginalFillDate", GetType(TypeCode.String));
            dtTable.Columns.Add("P_32_LastRefillDate", GetType(TypeCode.String));
            dtTable.Columns.Add("P_33_NextRefillDate", GetType(TypeCode.String));
            dtTable.Columns.Add("P_34_AdministrationDate", GetType(TypeCode.String));
            dtTable.Columns.Add("P_35_AdministrationTime", GetType(TypeCode.String));
            dtTable.Columns.Add("P_36_MedicationIdentifier", GetType(TypeCode.String));
            dtTable.Columns.Add("P_37_InventoryNumber", GetType(TypeCode.String));
            dtTable.Columns.Add("P_38_RxInstrucion", GetType(TypeCode.String));
            dtTable.Columns.Add("P_39_RxWarning", GetType(TypeCode.String));
            dtTable.Columns.Add("P_40_PrePack", GetType(TypeCode.String));

            dtTable.Columns.Add("P_41_QuantityPerDose", GetType(TypeCode.String));
            dtTable.Columns.Add("P_42_CustomFiel_01", GetType(TypeCode.String));
            dtTable.Columns.Add("P_43_CustomFiel_02", GetType(TypeCode.String));
            dtTable.Columns.Add("P_44_CustomFiel_03", GetType(TypeCode.String));
            dtTable.Columns.Add("P_45_CustomFiel_04", GetType(TypeCode.String));
            dtTable.Columns.Add("P_46_CustomFiel_05", GetType(TypeCode.String));

            dts.Tables.Add(dtTable);

            return dts.Clone();
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        private static Type GetType(TypeCode TipoDato)
        {
            return Type.GetType("System." + TipoDato.ToString());
        }

        private string Formatear(string Valor, Espacio Largo)
        {
            string sRegresa = "";
            int iLargo = (int)Largo;

            sFormato_01_Texto = " ";

            sRegresa = Fg.Left(Valor + Fg.PonFormato("", sFormato_01_Texto, iLargo), iLargo);

            return sRegresa;
        }

        private string Formatear(string Cadena, int Caracteres)
        {
            string sCadena = Cadena.Replace("\0", "");
            int iCaracteres = Caracteres - sCadena.Length;

            for (int i = 1; i <= iCaracteres; i++)
            {
                sCadena += "\0";
            }

            return Fg.Mid(sCadena, 1, Caracteres);
        }

        private string Normalizar(string Valor)
        {
            string sRegresa = Valor;
            string consignos = "áàäéèëíìïóòöúùuñÁÀÄÉÈËÍÌÏÓÒÖÚÙÜÑçÇ";
            string sinsignos = "aaaeeeiiiooouuunAAAEEEIIIOOOUUUNcC";

            for (int i = 0; i <= consignos.Length - 1; i++)
            {
                sRegresa = sRegresa.Replace(consignos[i], sinsignos[i]);
            }

            return sRegresa; 
        }
        #endregion Funciones y Procedimientos Privados

        #region Propiedades Publicas 
        public string P_01_RecordType 
        {
            get { return Fg.PonCeros(sP_01_RecordType, (int)Espacio.P_01_RecordType); }
            set { sP_01_RecordType = Fg.PonCeros(value, (int)Espacio.P_01_RecordType); }
        }

        public string P_02_CustomerBatchID
        {
            get { return Formatear(sP_02_CustomerBatchID, Espacio.P_02_CustomerBatchID); }
            set { sP_02_CustomerBatchID = Formatear(value, Espacio.P_02_CustomerBatchID); }
        }

        public string P_03_PatientFirstName
        {
            get { return Formatear(sP_03_PatientFirstName, Espacio.P_03_PatientFirstName); }
            set { sP_03_PatientFirstName = Formatear(value, Espacio.P_03_PatientFirstName); }
        }

        public string P_04_PatientLastName
        {
            get { return Formatear(sP_04_PatientLastName, Espacio.P_04_PatientLastName); }
            set { sP_04_PatientLastName = Formatear(value, Espacio.P_04_PatientLastName); }
        }

        public string P_05_PatientIdentifier
        {
            get { return Formatear(sP_05_PatientIdentifier, Espacio.P_05_PatientIdentifier); }
            set { sP_05_PatientIdentifier = Formatear(value, Espacio.P_05_PatientIdentifier); }
        }

        public string P_06_PatientDateOfBirth
        {
            //get { return Fg.PonFormato(sP_06_PatientDateOfBirth, sFormato_01_Texto, 10); }
            get { return Formatear(sP_06_PatientDateOfBirth, Espacio.P_06_PatientDateOfBirth); }
            set
            {
                sP_06_PatientDateOfBirth = value; 
                //sP_06_PatientDateOfBirth = General.FechaYMD(value, "-");
            }
        }

        public DateTime P_06_PatientDateOfBirth_Date
        {
            get 
            {
                return dtP_06_PatientDateOfBirth; 
            }

            set 
            { 
                dtP_06_PatientDateOfBirth = value;
                sP_06_PatientDateOfBirth = General.FechaYMD(value, "-"); 
            }
        }

        public string P_07_PatientAddress01
        {
            get { return Formatear(sP_07_PatientAddress01, Espacio.P_07_PatientAddress01); }
            set { sP_07_PatientAddress01 = Formatear(value, Espacio.P_07_PatientAddress01); }
        }

        public string P_08_PatientAddress02
        {
            get { return Formatear(sP_08_PatientAddress02, Espacio.P_08_PatientAddress02); }
            set { sP_08_PatientAddress02 = Formatear(value, Espacio.P_08_PatientAddress02); }
        }

        public string P_09_PatientCity
        {
            get { return Formatear(sP_09_PatientCity, Espacio.P_09_PatientCity); }
            set { sP_09_PatientCity = Formatear(value, Espacio.P_09_PatientCity); }
        }

        public string P_10_PatientState
        {
            get { return Formatear(sP_10_PatientState, Espacio.P_10_PatientState); }
            set { sP_10_PatientState = Formatear(value, Espacio.P_10_PatientState); }
        }

        public string P_11_PatientZipCode
        {
            get { return Formatear(sP_11_PatientZipCode, Espacio.P_11_PatientZipCode); }
            set { sP_11_PatientZipCode = Formatear(value, Espacio.P_11_PatientZipCode); }
        }

        public string P_12_PatientPhoneNumber
        {
            get { return Formatear(sP_12_PatientPhoneNumber, Espacio.P_12_PatientPhoneNumber); }
            set { sP_12_PatientPhoneNumber = Formatear(value, Espacio.P_12_PatientPhoneNumber); }
        }

        public string P_13_Insurence
        {
            get { return Formatear(sP_13_Insurence, Espacio.P_13_Insurence); }
            set { sP_13_Insurence = Formatear(value, Espacio.P_13_Insurence); }
        }

        public string P_14_InsurenceGroup
        {
            get { return Formatear(sP_14_InsurenceGroup, Espacio.P_14_InsurenceGroup); }
            set { sP_14_InsurenceGroup = Formatear(value, Espacio.P_14_InsurenceGroup); }
        }

        public string P_15_FacilityName
        {
            get { return Formatear(sP_15_FacilityName, Espacio.P_15_FacilityName); }
            set { sP_15_FacilityName = Formatear(value, Espacio.P_15_FacilityName); }
        }

        public string P_16_FacilityIdentifier
        {
            get { return Formatear(sP_16_FacilityIdentifier, Espacio.P_16_FacilityIdentifier); }
            set { sP_16_FacilityIdentifier = Formatear(value, Espacio.P_16_FacilityIdentifier); }
        }

        public string P_17_UnitOrWing
        {
            get { return Formatear(sP_17_UnitOrWing, Espacio.P_17_UnitOrWing); }
            set { sP_17_UnitOrWing = Formatear(value, Espacio.P_17_UnitOrWing); }
        }

        public string P_18_RoomNumber
        {
            get { return Formatear(sP_18_RoomNumber, Espacio.P_18_RoomNumber); }
            set { sP_18_RoomNumber = Formatear(value, Espacio.P_18_RoomNumber); }
        }

        public string P_19_BedNumber
        {
            get { return Formatear(sP_19_BedNumber, Espacio.P_19_BedNumber); }
            set { sP_19_BedNumber = Formatear(value, Espacio.P_19_BedNumber); }
        }

        public string P_20_DoctorFirtsName
        {
            get { return Formatear(sP_20_DoctorFirtsName, Espacio.P_20_DoctorFirtsName); }
            set { sP_20_DoctorFirtsName = Formatear(value, Espacio.P_20_DoctorFirtsName); }
        }

        public string P_21_DoctorLastName
        {
            get { return Formatear(sP_21_DoctorLastName, Espacio.P_21_DoctorLastName); }
            set { sP_21_DoctorLastName = Formatear(value, Espacio.P_21_DoctorLastName); }
        }

        public string P_22_DoctorPhoneNumber
        {
            get { return Formatear(sP_22_DoctorPhoneNumber, Espacio.P_22_DoctorPhoneNumber); }
            set { sP_22_DoctorPhoneNumber = Formatear(value, Espacio.P_22_DoctorPhoneNumber); }
        }

        public string P_23_DoctorIdentifier
        {
            get { return Formatear(sP_23_DoctorIdentifier, Espacio.P_23_DoctorIdentifier); }
            set { sP_23_DoctorIdentifier = Formatear(value, Espacio.P_23_DoctorIdentifier); }
        }

        public string P_24_RPhInitials
        {
            get { return Formatear(sP_24_RPhInitials, Espacio.P_24_RPhInitials); }
            set { sP_24_RPhInitials = Formatear(value, Espacio.P_24_RPhInitials); }
        }

        public string P_25_RxNumber
        {
            get { return Formatear(sP_25_RxNumber, Espacio.P_25_RxNumber); }
            set { sP_25_RxNumber = Formatear(value, Espacio.P_25_RxNumber); }
        }

        public string P_26_RxQuantity
        {
            get { return Formatear(sP_26_RxQuantity, Espacio.P_26_RxQuantity); }
            set { sP_26_RxQuantity = Formatear(value, Espacio.P_26_RxQuantity); }
        }

        public string P_27_RxFillDestination
        {
            get { return Formatear(sP_27_RxFillDestination, Espacio.P_27_RxFillDestination); }
            set { sP_27_RxFillDestination = Formatear(value, Espacio.P_27_RxFillDestination); }
        }

        public string P_28_ReFillRemaining
        {
            get { return Formatear(sP_28_ReFillRemaining, Espacio.P_28_ReFillRemaining); }
            set { sP_28_ReFillRemaining = Formatear(value, Espacio.P_28_ReFillRemaining); }
        }

        public string P_29_DaysSupply
        {
            get { return Formatear(sP_29_DaysSupply, Espacio.P_29_DaysSupply); }
            set { sP_29_DaysSupply = Formatear(value, Espacio.P_29_DaysSupply); }
        }

        public string P_30_RefillMessage
        {
            get { return Formatear(sP_30_RefillMessage, Espacio.P_30_RefillMessage); }
            set { sP_30_RefillMessage = Formatear(value, Espacio.P_30_RefillMessage); }
        }

        public string P_31_OriginalFillDate
        {
            //get { return Fg.PonFormato(sP_31_OriginalFillDate, sFormato_01_Texto, 10); }
            get { return Formatear(sP_31_OriginalFillDate, Espacio.P_31_OriginalFillDate); }

            set
            {
                sP_31_OriginalFillDate = value;
                //sP_31_OriginalFillDate = General.FechaYMD(value, "-");
            }
        }

        public DateTime P_31_OriginalFillDate_Date
        {
            get
            {
                return dtP_31_OriginalFillDate;
            }

            set
            {
                dtP_31_OriginalFillDate = value;
                sP_31_OriginalFillDate = General.FechaYMD(value, "-");
            }
        }

        public string P_32_LastRefillDate
        {
            //get { return Fg.PonFormato(sP_32_LastRefillDate, sFormato_01_Texto, 10); }
            get { return Formatear(sP_32_LastRefillDate, Espacio.P_32_LastRefillDate); }

            set
            {
                sP_32_LastRefillDate = value;
                //sP_32_LastRefillDate = General.FechaYMD(value, "-");
            }

        }

        public DateTime P_32_LastRefillDate_Date
        {
            get
            {
                return dtP_32_LastRefillDate;
            }

            set
            {
                dtP_32_LastRefillDate = value;
                sP_32_LastRefillDate = General.FechaYMD(value, "-");
            }
        }

        public string P_33_NextRefillDate
        {
            //get { return Fg.PonFormato(sP_33_NextRefillDate, sFormato_01_Texto, 10); }
            get { return Formatear(sP_33_NextRefillDate, Espacio.P_33_NextRefillDate); }

            set
            {
                sP_33_NextRefillDate = value;
                //sP_33_NextRefillDate = General.FechaYMD(value, "-");
            }
        }

        public DateTime P_33_NextRefillDate_Date
        {
            get
            {
                return dtP_33_NextRefillDate;
            }

            set
            {
                dtP_33_NextRefillDate = value;
                sP_33_NextRefillDate = General.FechaYMD(value, "-");
            }
        }

        public string P_34_AdministrationDate
        {
            //get { return Fg.Left(sP_34_AdministrationDate + Fg.PonFormato("", sFormato_01_Texto, 10), 10); }
            get { return Formatear(sP_34_AdministrationDate, Espacio.P_34_AdministrationDate); }

            set
            {
                sP_34_AdministrationDate = value;
                //sP_34_AdministrationDate = General.FechaYMD(value, "-");
            }
        }

        public DateTime P_34_AdministrationDate_Date
        {
            get
            {
                return dtP_34_AdministrationDate;
            }

            set
            {
                dtP_34_AdministrationDate = value;
                sP_34_AdministrationDate = General.FechaYMD(value, "-");
            }
        }

        public string P_35_AdministrationTime
        {
            // Error, se enviaban 10 caracteres 

            //get { return Fg.Left(sP_35_AdministrationTime + Fg.PonFormato("", sFormato_01_Texto, 10), 10); }
            get { return Formatear(sP_35_AdministrationTime, Espacio.P_35_AdministrationTime); }


            set
            {
                sP_35_AdministrationTime = value;
                //sP_35_AdministrationTime = General.FechaYMD(value, "-");
            }
        }

        public DateTime P_35_AdministrationTime_Date
        {
            get
            {
                return dtP_35_AdministrationTime;
            }

            set
            {
                dtP_35_AdministrationTime = value;
                sP_35_AdministrationTime = General.Hora(value, "");
            }
        }

        public string P_36_MedicationIdentifier
        {
            get { return Formatear(sP_36_MedicationIdentifier, Espacio.P_36_MedicationIdentifier); }
            set { sP_36_MedicationIdentifier = Formatear(value, Espacio.P_36_MedicationIdentifier); }
        }

        public string P_37_InventoryNumber
        {
            get { return Formatear(sP_37_InventoryNumber, Espacio.P_37_InventoryNumber); }
            set { sP_37_InventoryNumber = Formatear(value, Espacio.P_37_InventoryNumber); }
        }

        public string P_38_RxInstrucion
        {
            get { return Formatear(sP_38_RxInstrucion, Espacio.P_38_RxInstrucion); }
            set { sP_38_RxInstrucion = Formatear(value, Espacio.P_38_RxInstrucion); }
        }

        public string P_39_RxWarning
        {
            get { return Formatear(sP_39_RxWarning, Espacio.P_39_RxWarning); }
            set { sP_39_RxWarning = Formatear(value, Espacio.P_39_RxWarning); }
        }

        public string P_40_PrePack
        {
            get { return Formatear(sP_40_PrePack, Espacio.P_40_PrePack); }
            set { sP_40_PrePack = Formatear(value, Espacio.P_40_PrePack); }
        }

        public string P_41_QuantityPerDose
        {
            get { return Formatear(sP_41_QuantityPerDose, Espacio.P_41_QuantityPerDose); }
            set { sP_41_QuantityPerDose = Formatear(value, Espacio.P_41_QuantityPerDose); }
        }

        public string P_42_CustomFiel_01
        {
            get { return Formatear(sP_42_CustomFiel_01, Espacio.P_42_CustomFiel_01); }
            set { sP_42_CustomFiel_01 = Formatear(value, Espacio.P_42_CustomFiel_01); }
        }

        public string P_43_CustomFiel_02
        {
            get { return Formatear(sP_43_CustomFiel_02, Espacio.P_43_CustomFiel_02); }
            set { sP_43_CustomFiel_02 = Formatear(value, Espacio.P_43_CustomFiel_02); }
        }

        public string P_44_CustomFiel_03
        {
            get { return Formatear(sP_44_CustomFiel_03, Espacio.P_44_CustomFiel_03); }
            set { sP_44_CustomFiel_03 = Formatear(value, Espacio.P_44_CustomFiel_03); }
        }

        public string P_45_CustomFiel_04
        {
            get { return Formatear(sP_45_CustomFiel_04, Espacio.P_45_CustomFiel_04); }
            set { sP_45_CustomFiel_04 = Formatear(value, Espacio.P_45_CustomFiel_04); }
        }

        public string P_46_CustomFiel_05
        {
            get { return Formatear(sP_46_CustomFiel_05, Espacio.P_46_CustomFiel_05); }
            set { sP_46_CustomFiel_05 = Formatear(value, Espacio.P_46_CustomFiel_05); }
        }
        #endregion Propiedades Publicas

        #region Propiedades Publicas Ext
        public string GetRegistro()
        {
            string sRegresa = "";

            sRegresa += P_01_RecordType + P_02_CustomerBatchID + P_03_PatientFirstName + P_04_PatientLastName + P_05_PatientIdentifier;
            sRegresa += P_06_PatientDateOfBirth + P_07_PatientAddress01 + P_08_PatientAddress02 + P_09_PatientCity + P_10_PatientState;
            sRegresa += P_11_PatientZipCode + P_12_PatientPhoneNumber + P_13_Insurence + P_14_InsurenceGroup + P_15_FacilityName;            
            sRegresa += P_16_FacilityIdentifier + P_17_UnitOrWing + P_18_RoomNumber + P_19_BedNumber + P_20_DoctorFirtsName;
            sRegresa += P_21_DoctorLastName + P_22_DoctorPhoneNumber + P_23_DoctorIdentifier + P_24_RPhInitials + P_25_RxNumber;
            sRegresa += P_26_RxQuantity + P_27_RxFillDestination + P_28_ReFillRemaining + P_29_DaysSupply + P_30_RefillMessage;
            sRegresa += P_31_OriginalFillDate + P_32_LastRefillDate + P_33_NextRefillDate + P_34_AdministrationDate + P_35_AdministrationTime;
            sRegresa += P_36_MedicationIdentifier + P_37_InventoryNumber + P_38_RxInstrucion + P_39_RxWarning + P_40_PrePack;
            sRegresa += P_41_QuantityPerDose + P_42_CustomFiel_01 + P_43_CustomFiel_02 + P_44_CustomFiel_03 + P_45_CustomFiel_04;
            sRegresa += P_46_CustomFiel_05; 

            sRegresa += "";

            sRegresa = Normalizar(sRegresa);

            return sRegresa; 
        }

        public object[] GetRenglon()
        {
            object[] items =
            {
                P_01_RecordType.Trim(), P_02_CustomerBatchID.Trim(), P_03_PatientFirstName.Trim(), P_04_PatientLastName.Trim(), P_05_PatientIdentifier.Trim(), 
                P_06_PatientDateOfBirth.Trim(), P_07_PatientAddress01.Trim(), P_08_PatientAddress02.Trim(), P_09_PatientCity.Trim(), P_10_PatientState.Trim(), 
                P_11_PatientZipCode.Trim(), P_12_PatientPhoneNumber.Trim(), P_13_Insurence.Trim(), P_14_InsurenceGroup.Trim(), P_15_FacilityName.Trim(), 
                P_16_FacilityIdentifier.Trim(), P_17_UnitOrWing.Trim(), P_18_RoomNumber.Trim(), P_19_BedNumber.Trim(), P_20_DoctorFirtsName.Trim(), 
                P_21_DoctorLastName.Trim(), P_22_DoctorPhoneNumber.Trim(), P_23_DoctorIdentifier.Trim(), P_24_RPhInitials.Trim(), P_25_RxNumber.Trim(), 
                P_26_RxQuantity.Trim(), P_27_RxFillDestination.Trim(), P_28_ReFillRemaining.Trim(), P_29_DaysSupply.Trim(), P_30_RefillMessage.Trim(), 
                P_31_OriginalFillDate.Trim(), P_32_LastRefillDate.Trim(), P_33_NextRefillDate.Trim(), P_34_AdministrationDate.Trim(), P_35_AdministrationTime.Trim(), 
                P_36_MedicationIdentifier.Trim(), P_37_InventoryNumber.Trim(), P_38_RxInstrucion.Trim(), P_39_RxWarning.Trim(), P_40_PrePack.Trim(), 
                P_41_QuantityPerDose.Trim(), P_42_CustomFiel_01.Trim(), P_43_CustomFiel_02.Trim(), P_44_CustomFiel_03.Trim(), P_45_CustomFiel_04.Trim(), 
                P_46_CustomFiel_05.Trim() 
            };

            ////object[] item = 
            ////{
            ////    P_01_RecordType 
            ////};

            return items; 
        }
        #endregion Propiedades Publicas Ext

        #region Funciones y Procedimientos Publicos 
        ////private void DecodificarMensaje(string Mensaje)
        ////{
        ////    string sMsj = Mensaje;

        ////    pMach.Dialogo = pMach.Cortar(ref sMsj, 1);
        ////    pMach.OrderNumber = pMach.Cortar(ref sMsj, 8);
        ////    pMach.RequestLocationNumber = pMach.Cortar(ref sMsj, 3);


        ////    pMach.DeliveryNote = pMach.Cortar(ref sMsj, 20);
        ////    pMach.ProductCode = pMach.Cortar(ref sMsj, 20);
        ////    pMach.Barcode = pMach.Cortar(ref sMsj, 20);
        ////    pMach.BatchNumber = pMach.Cortar(ref sMsj, 20);
        ////    pMach.Quantity = pMach.Cortar(ref sMsj, 5);
        ////    pMach.UseByDate = pMach.Cortar(ref sMsj, 8);
        ////    pMach.StorageDate = pMach.Cortar(ref sMsj, 8);
        ////    pMach.State = pMach.Cortar(ref sMsj, 2);
        ////}
        #endregion Funciones y Procedimientos Publicos
    }
}
