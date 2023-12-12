using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//System Extra
using System.Data;

//SC Solutions
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;


namespace Cliente_Regional.Code
{
    /// <summary>
    /// Descripción breve de ClsConsultas
    /// </summary>
    public class ClsConsultas
    {
        public ClsConsultas()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        #region Funciones publicas
        public DataSet GetBeneficiariosAlmacen()
        {
            string sTipoUnidadAlmacen = "006";
            string sSql = string.Format(@"Set DateFormat YMD
	                                    Select
		                                    B.IdBeneficiario, B.NombreCompleto		
	                                    From vw_Beneficiarios As B (NoLock)
	                                    Inner Join CatFarmacias As CF (NoLock) On ( B.IdEstado =  CF.IdEstado And B.IdFarmacia = CF.IdFarmacia )
	                                    Inner Join BI_RPT__CatFarmacias_A_Procesar As FP On ( B.IdEstado = FP.IdEstado  And B.IdFarmacia = FP.IdFarmacia )
	                                    Where B.IdEstado = '{0}' And B.IdCliente = '{1}' And B.IdSubCliente = '{2}' And CF.IdTipoUnidad = '{3}' And B.Status = 'A'",
                                            DtGeneral.IdEstado, DtGeneral.IdCliente, DtGeneral.IdSubCliente, sTipoUnidadAlmacen);

            DataSet dtsReturn = DtGeneral.ExecQuery(sSql, "Beneficiarios");
            return dtsReturn;
        }
        public string GetUnidadesAlmacen(string sOpcion, string sUnidad, string sJurisdiccion, string sMunicipio, string sFarmacia, bool bAddSelect, bool bUnique)
        {
            string sEstructura = string.Empty;
            string sOpcionAdministrador = string.Empty;
            string sQuery = null;
            string[] sColumnas = new string[] { };
            if (HttpContext.Current.Session["UnidadesAlmacen"] == null)
            {
                string sTipoUnidadAlmacen = "006";
                string sSql = string.Format(@"Set DateFormat YMD
                                        Select
                                            F.IdTipoUnidad, F.TipoDeUnidad, F.IdJurisdiccion, F.Jurisdiccion, F.IdMunicipio, F.Municipio, F.IdFarmacia, F.Farmacia
                                        From vw_Farmacias As F (Nolock)
                                        Inner Join BI_RPT__CatFarmacias_A_Procesar As FP On ( F.IdEstado = FP.IdEstado And F.IdFarmacia = FP.IdFarmacia And FP.MostrarEnListado  = '1')
                                        Where F.IdEstado = '{0}' And F.IdTipoUnidad = '{1}' And F.IdNivelDeAtencion = '3' And F.Status = 'A'",
                                            DtGeneral.IdEstado, sTipoUnidadAlmacen);
                HttpContext.Current.Session["UnidadesAlmacen"] = DtGeneral.ExecQuery(sSql, "UnidadesAlmacen");
            }
            DataTable dtData = ((DataSet)HttpContext.Current.Session["UnidadesAlmacen"]).Tables[0];


            switch (sOpcion)
            {
                case "Unidad":
                    sColumnas = new string[] { "IdTipoUnidad", "TipoDeUnidad" };
                    break;
                case "Jurisdiccion":
                    sColumnas = new string[] { "IdJurisdiccion", "Jurisdiccion" };
                    break;
                case "Municipio":
                    sColumnas = new string[] { "IdMunicipio", "Municipio" };
                    break;
                case "Farmacia":
                    sColumnas = new string[] { "IdFarmacia", "Farmacia" };
                    break;
                default:
                    break;
            }

            dtData = dtData.Select(sQuery, sColumnas[0]).CopyToDataTable();

            sEstructura += clsToolsHtml.OptionDropList(dtData.DefaultView.ToTable(true, sColumnas), sColumnas[0], sColumnas[1], bAddSelect);

            return sEstructura;
        }
        public DataSet Permisos(string sArbol, string sLoginUser)
        {
            DataSet dtsPermisos = new DataSet("dtsPermisos");

            string sSql = string.Format(" Exec sp_CTE_Permisos '{0}', '{1}', '{2}', '{3}' ", DtGeneral.IdEstado, DtGeneral.IdSucursal, sArbol, sLoginUser);
            dtsPermisos = DtGeneral.ExecQuery(sSql, "Permisos", "ClsConsultas - Permisos");

            return dtsPermisos;
        }

        public DataSet GetArbolNavegacion(string sArbol)
        {
            DataSet dtsReturn = new DataSet();
            string sSql = string.Format(" Exec sp_CTE_Permisos '{0}', '{1}', '{2}', '{3}' ", 
                DtGeneral.IdEstado, DtGeneral.ObtenerValorCookie("IdSucursal"), sArbol, DtGeneral.ObtenerValorCookie("LoginUser"));

            dtsReturn = DtGeneral.ExecQuery(sSql, "Arbol", "ClsConsultas - GetArbolNavegacion");
            return dtsReturn;
        }

        public static DataSet InformacionUsuario(string sLoginUser)
        {
            string sSql = string.Format(@"Set DateFormat YMD
                                        Select
                                            IdEstado, IdFarmacia as IdSucursal, IdUsuario as IdPersonal, Login as LoginUser, Nombre, Password, Status 
                                        From Net_Regional_Usuarios (NoLock) 
                                        Where IdEstado = '{0}' And Login = '{1}' ", DtGeneral.IdEstado, sLoginUser);

            return DtGeneral.ExecQuery(sSql, "Usuario", "ClsConsultas.cs - InformacionUsuario");

        }

        public static DataTable TiposDeUnidad()
        {
            clsLeer myLeer = new clsLeer()
            {
                DataSetClase = DtGeneral.GetInfoGeneral()
            };
            DataView view = new DataView(myLeer.DataTableClase);
            DataTable dtInfo = view.ToTable(true, "IdTipoUnidad", "TipoDeUnidad");

            if (DtGeneral.ObtenerValorCookie("IdSucursal") == "0001") {
                //dtInfo.Rows.Add(new Object[] {"*", "Todos los tipo de unidades" });
                DataRow drFirstOption = dtInfo.NewRow();
                drFirstOption["IdTipoUnidad"] = "*";
                drFirstOption["TipoDeUnidad"] = "Todos los tipo de unidades";
                dtInfo.Rows.InsertAt(drFirstOption, 0);
            }

            return dtInfo;
        }

        public static DataTable TiposDeUnidad2()
        {
            clsLeer myLeer = new clsLeer()
            {
                DataSetClase = DtGeneral.GetInfoAlmacen()
            };
            DataView view = new DataView(myLeer.DataTableClase);
            DataTable dtInfo = view.ToTable(true, "IdTipoUnidad", "TipoDeUnidad");

            if (DtGeneral.ObtenerValorCookie("IdSucursal") == "0001") {
                //dtInfo.Rows.Add(new Object[] {"*", "Todos los tipo de unidades" });
                DataRow drFirstOption = dtInfo.NewRow();
                drFirstOption["IdTipoUnidad"] = "*";
                drFirstOption["TipoDeUnidad"] = "Todos los tipo de unidades";
                dtInfo.Rows.InsertAt(drFirstOption, 0);
            }

            return dtInfo;
        }

        public static DataTable Jurisdicciones(string sIdTipoDeUnidad)
        {
            clsLeer myLeer = new clsLeer()
            {
                DataSetClase = DtGeneral.GetInfoGeneral()
            };

            if (sIdTipoDeUnidad != "*") {
                myLeer.DataRowsClase = myLeer.DataSetClase.Tables[0].Select(string.Format("IdTipoUnidad = '{0}'", sIdTipoDeUnidad));
            }

            DataView view = new DataView(myLeer.DataTableClase);
            DataTable dtInfo = view.ToTable(true, "IdJurisdiccion", "Jurisdiccion");

            if (DtGeneral.ObtenerValorCookie("IdSucursal") == "0001")
            {
                //dtInfo.Rows.Add(new Object[] { "*", "Todas las jurisdicciones" });
                DataRow drFirstOption = dtInfo.NewRow();
                drFirstOption["IdJurisdiccion"] = "*";
                drFirstOption["Jurisdiccion"] = "Todas las jurisdicciones";
                dtInfo.Rows.InsertAt(drFirstOption, 0);
            }

            return dtInfo;
        }

        public static DataTable Jurisdicciones2(string sIdTipoDeUnidad)
        {
            clsLeer myLeer = new clsLeer()
            {
                DataSetClase = DtGeneral.GetInfoAlmacen()
            };

            if (sIdTipoDeUnidad != "*")
            {
                myLeer.DataRowsClase = myLeer.DataSetClase.Tables[0].Select(string.Format("IdTipoUnidad = '{0}'", sIdTipoDeUnidad));
            }

            DataView view = new DataView(myLeer.DataTableClase);
            DataTable dtInfo = view.ToTable(true, "IdJurisdiccion", "Jurisdiccion");

            if (DtGeneral.ObtenerValorCookie("IdSucursal") == "0001")
            {
                //dtInfo.Rows.Add(new Object[] { "*", "Todas las jurisdicciones" });
                DataRow drFirstOption = dtInfo.NewRow();
                drFirstOption["IdJurisdiccion"] = "*";
                drFirstOption["Jurisdiccion"] = "Todas las jurisdicciones";
                dtInfo.Rows.InsertAt(drFirstOption, 0);
            }

            return dtInfo;
        }

        public static DataTable Localidades(string sIdTipoDeUnidad, string sIdJurisdiccion)
        {
            clsLeer myLeer = new clsLeer()
            {
                DataSetClase = DtGeneral.GetInfoGeneral()
            };

            if (sIdTipoDeUnidad != "*")
            {
                myLeer.DataRowsClase = myLeer.DataSetClase.Tables[0].Select(string.Format("IdTipoUnidad = '{0}'", sIdTipoDeUnidad));
            }

            if (sIdJurisdiccion != "*")
            {
                myLeer.DataRowsClase = myLeer.DataSetClase.Tables[0].Select(string.Format("IdJurisdiccion = '{0}'", sIdJurisdiccion));
            }

            DataView view = new DataView(myLeer.DataTableClase);
            DataTable dtInfo = view.ToTable(true, "IdMunicipio", "Municipio");

            if (DtGeneral.ObtenerValorCookie("IdSucursal") == "0001")
            {
                //dtInfo.Rows.Add(new Object[] { "*", "Todos los Municipios" });
                DataRow drFirstOption = dtInfo.NewRow();
                drFirstOption["IdMunicipio"] = "*";
                drFirstOption["Municipio"] = "Todos los Municipios";
                dtInfo.Rows.InsertAt(drFirstOption, 0);
            }

            return dtInfo;
        }

        public static DataTable Localidades2(string sIdTipoDeUnidad, string sIdJurisdiccion)
        {
            clsLeer myLeer = new clsLeer()
            {
                DataSetClase = DtGeneral.GetInfoAlmacen()
            };

            if (sIdTipoDeUnidad != "*")
            {
                myLeer.DataRowsClase = myLeer.DataSetClase.Tables[0].Select(string.Format("IdTipoUnidad = '{0}'", sIdTipoDeUnidad));
            }

            if (sIdJurisdiccion != "*")
            {
                myLeer.DataRowsClase = myLeer.DataSetClase.Tables[0].Select(string.Format("IdJurisdiccion = '{0}'", sIdJurisdiccion));
            }

            DataView view = new DataView(myLeer.DataTableClase);
            DataTable dtInfo = view.ToTable(true, "IdMunicipio", "Municipio");

            if (DtGeneral.ObtenerValorCookie("IdSucursal") == "0001")
            {
                //dtInfo.Rows.Add(new Object[] { "*", "Todos los Municipios" });
                DataRow drFirstOption = dtInfo.NewRow();
                drFirstOption["IdMunicipio"] = "*";
                drFirstOption["Municipio"] = "Todos los Municipios";
                dtInfo.Rows.InsertAt(drFirstOption, 0);
            }

            return dtInfo;
        }

        public static DataTable Almacen(string sIdTipoDeUnidad, string sIdJurisdiccion, string sIdMunicipio)
        {
            clsLeer myLeer = new clsLeer()
            {
                DataSetClase = DtGeneral.GetInfoAlmacen()
            };

            if (sIdTipoDeUnidad != "*")
            {
                myLeer.DataRowsClase = myLeer.DataSetClase.Tables[0].Select(string.Format("IdTipoUnidad = '{0}'", sIdTipoDeUnidad));
            }

            if (sIdJurisdiccion != "*")
            {
                myLeer.DataRowsClase = myLeer.DataSetClase.Tables[0].Select(string.Format("IdJurisdiccion = '{0}'", sIdJurisdiccion));
            }

            if (sIdMunicipio != "*")
            {
                myLeer.DataRowsClase = myLeer.DataSetClase.Tables[0].Select(string.Format("IdMunicipio = '{0}'", sIdMunicipio));
            }

            DataView view = new DataView(myLeer.DataTableClase);
            DataTable dtInfo = view.ToTable(true, "IdFarmacia", "Farmacia");

            if (DtGeneral.ObtenerValorCookie("IdSucursal") == "0001")
            {
                //dtInfo.Rows.Add(new Object[] { "*", "Todos los Almacenes" });
                DataRow drFirstOption = dtInfo.NewRow();
                drFirstOption["IdFarmacia"] = "*";
                drFirstOption["Farmacia"] = "Todos los Almacenes";
                dtInfo.Rows.InsertAt(drFirstOption, 0);
            }

            return dtInfo;
        }

        public static DataTable FarmaciasUnidosis()
        {
            DataTable dtInfo =  DtGeneral.GetFarmacias(DtGeneral.IdEstado).Tables[0];
            //dtInfo.Rows.Add(new Object[] { "*", "Todas las Farmacias" });
            DataRow drFirstOption = dtInfo.NewRow();
            drFirstOption["IdFarmacia"] = "*";
            drFirstOption["Farmacia"] = "Todos los Almacenes";
            dtInfo.Rows.InsertAt(drFirstOption, 0);
            return dtInfo;
        }


        public static DataTable Farmacias(string sIdTipoDeUnidad, string sIdJurisdiccion, string sIdMunicipio)
        {
            return Farmacias(sIdTipoDeUnidad, sIdJurisdiccion, sIdMunicipio, false); 
        }
        public static DataTable Farmacias(string sIdTipoDeUnidad, string sIdJurisdiccion, string sIdMunicipio, bool EsUnidosis)
        {
            clsLeer myLeer = new clsLeer()
            {
                DataSetClase = DtGeneral.GetInfoGeneral()
            };

            string sFiltro = EsUnidosis ? " and EsUnidosis = '1' " : "and EsUnidosis = '0' "; 

            if (sIdTipoDeUnidad != "*")
            {
                myLeer.DataRowsClase = myLeer.DataSetClase.Tables[0].Select(string.Format("IdTipoUnidad = '{0}' {1}", sIdTipoDeUnidad, sFiltro));
            }

            if (sIdJurisdiccion != "*")
            {
                myLeer.DataRowsClase = myLeer.DataSetClase.Tables[0].Select(string.Format("IdJurisdiccion = '{0}' {1} ", sIdJurisdiccion, sFiltro));
            }

            if (sIdMunicipio != "*")
            {
                myLeer.DataRowsClase = myLeer.DataSetClase.Tables[0].Select(string.Format("IdMunicipio = '{0}' {1} ", sIdMunicipio, sFiltro));
            }


            DataView view = new DataView(myLeer.DataTableClase);
            DataTable dtInfo = view.ToTable(true, "IdFarmacia", "Farmacia");

            if (DtGeneral.ObtenerValorCookie("IdSucursal") == "0001")
            {
                //dtInfo.Rows.Add(new Object[] { "*", "Todas las Farmacias" });
                DataRow drFirstOption = dtInfo.NewRow();
                drFirstOption["IdFarmacia"] = "*";
                drFirstOption["Farmacia"] = "Todos los Almacenes";
                dtInfo.Rows.InsertAt(drFirstOption, 0);
            }

            return dtInfo;
        }

        public static DataTable Beneficiarios(string sIdTipoDeUnidad, string sIdJurisdiccion, string sIdMunicipio)
        {
            clsLeer myLeer = new clsLeer()
            {
                DataSetClase = DtGeneral.GetInfoBeneficiarios()
            };

            //if (sIdTipoDeUnidad != "*")
            //{
            //    myLeer.DataRowsClase = myLeer.DataSetClase.Tables[0].Select(string.Format("IdTipoUnidad = '{0}'", sIdTipoDeUnidad));
            //}

            //if (sIdJurisdiccion != "*")
            //{
            //    myLeer.DataRowsClase = myLeer.DataSetClase.Tables[0].Select(string.Format("IdJurisdiccion = '{0}'", sIdJurisdiccion));
            //}

            //if (sIdMunicipio != "*")
            //{
            //    myLeer.DataRowsClase = myLeer.DataSetClase.Tables[0].Select(string.Format("IdMunicipio = '{0}'", sIdMunicipio));
            //}

            DataView view = new DataView(myLeer.DataTableClase);
            DataTable dtInfo = view.ToTable(true, "IdBeneficiario", "NombreCompleto");

            //if (DtGeneral.ObtenerValorCookie("IdSucursal") == "0001")
            //{
                //dtInfo.Rows.Add(new Object[] { "*", "Todas las Farmacias" });
                DataRow drFirstOption = dtInfo.NewRow();
                drFirstOption["IdBeneficiario"] = "*";
                drFirstOption["NombreCompleto"] = "Todas las Unidades";
                dtInfo.Rows.InsertAt(drFirstOption, 0);
            //}

            return dtInfo;
        }

        #endregion Funciones publicas
    }
}