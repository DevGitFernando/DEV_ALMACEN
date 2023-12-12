using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Data.Odbc;

using Microsoft.VisualBasic;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;

using Dll_SII_IMediaccess;

namespace Dll_SII_IMediaccess.ValesRecepcion
{
    public class ValesRecepcionRegistrarInformacion
    {
        List<ItemInsumo> pInsumos = new List<ItemInsumo>();
        //ArrayList pInsumos = new ArrayList(); 


        string sFolio_Vale = "";
        DateTime dtFechaEmision_Vale = DateTime.Now;
        string sIdEmpresa = "";
        string sIdEstado = "";
        string sIdFarmacia = "";
        string sIdPersonal = "";
        ////string sBeneficiario_Nombre = "";
        ////string sBeneficiario_ApPaterno = "";
        ////string sBeneficiario_ApMaterno = "";
        ////string sBeneficiario_Sexo = "";
        ////string sBeneficiario_FechaNacimiento = "";
        ////string sBeneficiario_FolioReferencia = "";
        ////string sBeneficiario_FechaFinVigencia = "";
        string sIdTipoDeDispensacion = "";
        string sNumReceta = "";
        DateTime dtFechaReceta = DateTime.Now;
        string sIdBeneficio = "";
        string sIdDiagnostico = "";
        string sRefObservaciones = "";
        string sIdSocioComercial = "";
        string sIdSucursal = "";

        ItemBeneficiario personaBeneficiario = new ItemBeneficiario();
        ItemMedico personaMedico = new ItemMedico();
        ItemInsumo[] ItemInsumos;

        public ValesRecepcionRegistrarInformacion()
        {
            personaBeneficiario = new ItemBeneficiario();
            personaMedico = new ItemMedico();

            //pInsumos = new ArrayList();
            pInsumos = new List<ItemInsumo>();
        }

        public string IdSocioComercial
        {
            get { return sIdSocioComercial; }
            set { sIdSocioComercial = value; }
        }

        public string IdSucursal
        {
            get { return sIdSucursal; }
            set { sIdSucursal = value; }
        }

        public string Folio_Vale 
        {
            get { return sFolio_Vale; }
            set { sFolio_Vale = value; }
        }

        public DateTime FechaEmision_Vale
        {
            get { return dtFechaEmision_Vale; }
            set { dtFechaEmision_Vale = value; }
        }

        public string IdEmpresa
        {
            get { return sIdEmpresa; }
            set { sIdEmpresa = value; }
        }

        public string IdEstado 
        {
            get { return sIdEstado; }
            set { sIdEstado = value; }
        }

        public string IdFarmacia 
        {
            get { return sIdFarmacia; }
            set { sIdFarmacia = value; }
        }

        public string IdPersonal 
        {
            get { return sIdPersonal; }
            set { sIdPersonal = value; }
        }

        public string IdTipoDeDispensacion 
        {
            get { return sIdTipoDeDispensacion; }
            set { sIdTipoDeDispensacion = value; }
        }

        public string NumReceta 
        {
            get { return sNumReceta; }
            set { sNumReceta = value; }
        }

        public DateTime FechaReceta 
        {
            get { return dtFechaReceta; }
            set { dtFechaReceta = value; }
        }

        public string IdBeneficio 
        {
            get { return sIdBeneficio; }
            set { sIdBeneficio = value; }
        }


        public string IdDiagnostico 
        {
            get { return sIdDiagnostico; }
            set { sIdDiagnostico = value; }
        }

        public string RefObservaciones 
        {
            get { return sRefObservaciones; }
            set { sRefObservaciones = value; }
        }

        public ItemBeneficiario Beneficiario
        {
            get { return personaBeneficiario; }
            set { personaBeneficiario = value; }
        }

        public ItemMedico Medico
        {
            get { return personaMedico;  }
            set { personaMedico = value; }
        }

        public ItemInsumo InsumoTest
        {
            get { return new ItemInsumo(); }
        }

        public ItemInsumo[] Insumos
        {
            get
            {
                return ItemInsumos;
            }
            set
            {
                ItemInsumos = value;
            }
        }
    }
}
