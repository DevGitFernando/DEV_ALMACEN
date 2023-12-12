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
    public class clsATP2_GenerarOrdenDeAcondicionamiento
    {
        #region Declaracion de Variables 
        clsLeer datos = new clsLeer();

        string sRutaArchivos_Salida = Application.StartupPath + @"\ATP2\OrdenesAcondicionamiento\Salida\";
        string sRutaArchivos_Entrada = Application.StartupPath + @"\ATP2\OrdenesAcondicionamiento\Entrada\";

        string sFile_Name = "";
        string sFile_Salida = "";
        DateTime dtMarcaDeTiempo = DateTime.Now; 

        #endregion Declaracion de Variables

        #region Constructores y Destructor
        public clsATP2_GenerarOrdenDeAcondicionamiento()
        {
            DtGeneral.CrearDirectorio(sRutaArchivos_Salida);
            DtGeneral.CrearDirectorio(sRutaArchivos_Entrada);
        }
        #endregion Constructores y Destructor

        #region Propiedades 
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos 
        public void AbrirDirectorio()
        {
            General.AbrirDirectorio(sRutaArchivos_Salida); 
        }

        public DataSet LeerArchivo(string FileEntrada)
        {
            DataSet dtsRetorno = clsIATP2_Parametros.PrepararDtsAcondicionamiento(); 
            string sLineaDeEntrada = "";
            string sLinea = "";
            bool bExiste_Archivo = File.Exists(FileEntrada); 
            clsIATP2_Parametros item = new clsIATP2_Parametros();

            if (bExiste_Archivo)
            {
                using (StreamReader reader = new StreamReader(FileEntrada))
                {
                    while (reader.Peek() > 0)
                    {
                        sLineaDeEntrada = reader.ReadLine();
                        sLinea = sLineaDeEntrada;

                        item.P_01_RecordType = item.Cortar(ref sLinea, Espacio.P_01_RecordType);
                        item.P_02_CustomerBatchID = item.Cortar(ref sLinea, Espacio.P_02_CustomerBatchID);
                        item.P_03_PatientFirstName = item.Cortar(ref sLinea, Espacio.P_03_PatientFirstName);
                        item.P_04_PatientLastName = item.Cortar(ref sLinea, Espacio.P_04_PatientLastName);
                        item.P_05_PatientIdentifier = item.Cortar(ref sLinea, Espacio.P_05_PatientIdentifier);
                        item.P_06_PatientDateOfBirth = item.Cortar(ref sLinea, Espacio.P_06_PatientDateOfBirth);
                        item.P_07_PatientAddress01 = item.Cortar(ref sLinea, Espacio.P_07_PatientAddress01);
                        item.P_08_PatientAddress02 = item.Cortar(ref sLinea, Espacio.P_08_PatientAddress02);
                        item.P_09_PatientCity = item.Cortar(ref sLinea, Espacio.P_09_PatientCity);
                        item.P_10_PatientState = item.Cortar(ref sLinea, Espacio.P_10_PatientState);


                        item.P_11_PatientZipCode = item.Cortar(ref sLinea, Espacio.P_11_PatientZipCode);
                        item.P_12_PatientPhoneNumber = item.Cortar(ref sLinea, Espacio.P_12_PatientPhoneNumber);
                        item.P_13_Insurence = item.Cortar(ref sLinea, Espacio.P_13_Insurence);
                        item.P_14_InsurenceGroup = item.Cortar(ref sLinea, Espacio.P_14_InsurenceGroup);
                        item.P_15_FacilityName = item.Cortar(ref sLinea, Espacio.P_15_FacilityName);
                        item.P_16_FacilityIdentifier = item.Cortar(ref sLinea, Espacio.P_16_FacilityIdentifier);
                        item.P_17_UnitOrWing = item.Cortar(ref sLinea, Espacio.P_17_UnitOrWing);
                        item.P_18_RoomNumber = item.Cortar(ref sLinea, Espacio.P_18_RoomNumber);
                        item.P_19_BedNumber = item.Cortar(ref sLinea, Espacio.P_19_BedNumber);
                        item.P_20_DoctorFirtsName = item.Cortar(ref sLinea, Espacio.P_20_DoctorFirtsName);


                        item.P_21_DoctorLastName = item.Cortar(ref sLinea, Espacio.P_21_DoctorLastName);
                        item.P_22_DoctorPhoneNumber = item.Cortar(ref sLinea, Espacio.P_22_DoctorPhoneNumber);
                        item.P_23_DoctorIdentifier = item.Cortar(ref sLinea, Espacio.P_23_DoctorIdentifier);
                        item.P_24_RPhInitials = item.Cortar(ref sLinea, Espacio.P_24_RPhInitials);
                        item.P_25_RxNumber = item.Cortar(ref sLinea, Espacio.P_25_RxNumber);
                        item.P_26_RxQuantity = item.Cortar(ref sLinea, Espacio.P_26_RxQuantity);
                        item.P_27_RxFillDestination = item.Cortar(ref sLinea, Espacio.P_27_RxFillDestination);
                        item.P_28_ReFillRemaining = item.Cortar(ref sLinea, Espacio.P_28_ReFillRemaining);
                        item.P_29_DaysSupply = item.Cortar(ref sLinea, Espacio.P_29_DaysSupply);
                        item.P_30_RefillMessage = item.Cortar(ref sLinea, Espacio.P_30_RefillMessage);


                        item.P_31_OriginalFillDate = item.Cortar(ref sLinea, Espacio.P_31_OriginalFillDate);
                        item.P_32_LastRefillDate = item.Cortar(ref sLinea, Espacio.P_32_LastRefillDate);
                        item.P_33_NextRefillDate = item.Cortar(ref sLinea, Espacio.P_33_NextRefillDate);
                        item.P_34_AdministrationDate = item.Cortar(ref sLinea, Espacio.P_34_AdministrationDate);
                        item.P_35_AdministrationTime = item.Cortar(ref sLinea, Espacio.P_35_AdministrationTime);
                        item.P_36_MedicationIdentifier = item.Cortar(ref sLinea, Espacio.P_36_MedicationIdentifier);
                        item.P_37_InventoryNumber = item.Cortar(ref sLinea, Espacio.P_37_InventoryNumber);
                        item.P_38_RxInstrucion = item.Cortar(ref sLinea, Espacio.P_38_RxInstrucion);
                        item.P_39_RxWarning = item.Cortar(ref sLinea, Espacio.P_39_RxWarning);
                        item.P_40_PrePack = item.Cortar(ref sLinea, Espacio.P_40_PrePack);


                        item.P_41_QuantityPerDose = item.Cortar(ref sLinea, Espacio.P_41_QuantityPerDose);
                        item.P_42_CustomFiel_01 = item.Cortar(ref sLinea, Espacio.P_42_CustomFiel_01);
                        item.P_43_CustomFiel_02 = item.Cortar(ref sLinea, Espacio.P_43_CustomFiel_02);
                        item.P_44_CustomFiel_03 = item.Cortar(ref sLinea, Espacio.P_44_CustomFiel_03);
                        item.P_45_CustomFiel_04 = item.Cortar(ref sLinea, Espacio.P_45_CustomFiel_04);
                        item.P_46_CustomFiel_05 = item.Cortar(ref sLinea, Espacio.P_46_CustomFiel_05);

                        dtsRetorno.Tables[0].Rows.Add(item.GetRenglon());
                    }
                }
            }


            return dtsRetorno; 
        }

        public bool GenerarArchivo(DataSet Detalles)
        {
            bool bRegresa = false;
            basGenerales Fg = new basGenerales(); 
            //string sMarcaDeTiempo = "";
            string sLinea = ""; 
            clsIATP2_Parametros item = new clsIATP2_Parametros(); 

            datos = new clsLeer(); 
            datos.DataSetClase = Detalles;
            datos.DataTableClase = datos.Tabla("Datos_04_OrdenDeAcondicionamiento"); 
            dtMarcaDeTiempo = DateTime.Now;

            sFile_Name = 
                string.Format
                (
                    "{0}{1}{2}_{3}{4}{5}_{6}.DAT",
                    Fg.PonCeros(dtMarcaDeTiempo.Year, 4), Fg.PonCeros(dtMarcaDeTiempo.Month, 2), Fg.PonCeros(dtMarcaDeTiempo.Day, 2),
                    Fg.PonCeros(dtMarcaDeTiempo.Hour, 2), Fg.PonCeros(dtMarcaDeTiempo.Minute, 2), Fg.PonCeros(dtMarcaDeTiempo.Second, 2),
                    Fg.PonCeros(dtMarcaDeTiempo.Millisecond, 2) 
                );

            sFile_Salida = Path.Combine(sRutaArchivos_Salida, sFile_Name);

            if (datos.Registros > 0)
            {
                using (StreamWriter writer = new StreamWriter(sFile_Salida))
                {
                    while (datos.Leer())
                    {
                        item = new clsIATP2_Parametros(); 

                        item.P_01_RecordType = "A";
                        item.P_02_CustomerBatchID = datos.Campo("FolioAcondicionamiento");
                        item.P_03_PatientFirstName = datos.Campo("Nombre_Paciente");
                        item.P_04_PatientLastName = datos.Campo("ApMaterno_Paciente");
                        item.P_05_PatientIdentifier = datos.Campo("IdBeneficiario");
                        item.P_06_PatientDateOfBirth_Date = datos.CampoFecha("FechaNacimiento_Paciente");

                        item.P_07_PatientAddress01 = "";
                        item.P_08_PatientAddress02 = "";
                        item.P_09_PatientCity = "";
                        item.P_10_PatientState = "";
                        item.P_11_PatientZipCode = ""; 
                        
                        item.P_12_PatientPhoneNumber = datos.Campo("Telefono_Paciente");

                        item.P_13_Insurence = "";
                        item.P_14_InsurenceGroup = "";
                        item.P_15_FacilityName = "";
                        item.P_16_FacilityIdentifier = "";
                        item.P_17_UnitOrWing = "";

                        item.P_18_RoomNumber = datos.Campo("NumeroDeHabitacion");

                        item.P_19_BedNumber = datos.Campo("NumeroDeCama");
                        item.P_20_DoctorFirtsName = datos.Campo("Nombre_Medico"); 
                        item.P_21_DoctorLastName = datos.Campo("ApPaterno_Medico"); 
                        item.P_22_DoctorPhoneNumber = "";
                        item.P_23_DoctorIdentifier = datos.Campo("IdMedico");
                        item.P_24_RPhInitials = ""; 

                        item.P_25_RxNumber = datos.CampoInt("Partida").ToString();
                        item.P_26_RxQuantity = datos.Campo("CantidadSolicitada");

                        item.P_27_RxFillDestination = "";
                        item.P_28_ReFillRemaining = "";
                        item.P_29_DaysSupply = "";
                        item.P_30_RefillMessage = "";
                        //item.P_31_OriginalFillDate = "";
                        //item.P_32_LastRefillDate = ""; 
                        //item.P_33_NextRefillDate = "";
                        item.P_34_AdministrationDate_Date = datos.CampoFecha("FechaHora_De_Administracion");
                        item.P_35_AdministrationTime_Date = datos.CampoFecha("FechaHora_De_Administracion");

                        item.P_36_MedicationIdentifier = datos.Campo("CodigoEAN");

                        item.P_37_InventoryNumber = "";
                        item.P_38_RxInstrucion = "";
                        item.P_39_RxWarning = "";
                        item.P_40_PrePack = "";
                        item.P_41_QuantityPerDose = "";
                        item.P_42_CustomFiel_01 = "";
                        item.P_43_CustomFiel_02 = "";
                        item.P_44_CustomFiel_03 = "";
                        item.P_45_CustomFiel_04 = "";
                        item.P_46_CustomFiel_05 = ""; 


                        sLinea = item.GetRegistro();
                        writer.WriteLine(sLinea); 
                    }

                    writer.Close();
                    bRegresa = true; 
                }
            }

            bRegresa = enviarOrdenDeAcondicionamiento();

            return bRegresa; 
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private bool enviarOrdenDeAcondicionamiento()
        {
            bool bRegresa = false;
            string sFileEnvio = Path.Combine(IATP2.RutaRepositorio_OrdenesAcondicionamiento, sFile_Name);  
           
            try
            {
                File.Copy(sFile_Salida, sFileEnvio); 
                bRegresa = true; 
            }
            catch (Exception ex)
            { 

            }

            return bRegresa; 
        }
        #endregion Funciones y Procedimientos Privados
    }
}
