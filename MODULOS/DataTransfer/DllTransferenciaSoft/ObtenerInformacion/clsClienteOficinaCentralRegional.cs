using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Windows.Forms;

using System.Threading; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
// using SC_SolutionsSystem.Criptografia;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

using DllTransferenciaSoft;
using DllTransferenciaSoft.EnviarInformacion;
using DllTransferenciaSoft.Zip;

namespace DllTransferenciaSoft.ObtenerInformacion
{
    public class clsClienteOficinaCentralRegional : clsObtenerInformacion
    {
        public clsClienteOficinaCentralRegional(string ArchivoCfg, clsDatosConexion DatosConexion, string ClaveRenapo, string Centro)
            : base(ArchivoCfg, DatosConexion, ClaveRenapo, Centro, TipoServicio.ClienteOficinaCentralRegional)
        {
        }

        public clsClienteOficinaCentralRegional(string ArchivoCfg, clsDatosConexion DatosConexion, string ClaveRenapo, string Centro, 
            string IdEstado, string IdFarmacia )
            :base   (ArchivoCfg, DatosConexion, ClaveRenapo, Centro, TipoServicio.ClienteOficinaCentralRegional, IdEstado, IdFarmacia) 
        {
        }
    }
} 