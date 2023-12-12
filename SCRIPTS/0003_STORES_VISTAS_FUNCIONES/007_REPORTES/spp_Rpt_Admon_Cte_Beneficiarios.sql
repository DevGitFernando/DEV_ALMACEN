If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_Admon_Cte_Beneficiarios' and xType = 'P' ) 
   Drop Proc spp_Rpt_Admon_Cte_Beneficiarios 
Go--#SQL 

Create Proc spp_Rpt_Admon_Cte_Beneficiarios 
(
	@Tabla varchar(500) = '', @EncPrincipal varchar(500) = '', @EncSecundario varchar(500) = '' 
)
With Encryption 
As 
Begin 
Set NoCount On
	Declare 
	@sQuery varchar(7500) 
----	@EncSecundario varchar(500) 

	Set @sQuery = ''

--- Borrar las tablas de Datos 
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_Cte_Beneficiarios' and xType = 'U' )
	   Drop Table Rpt_Admon_Cte_Beneficiarios 

	
------------------------------------------------------------------------------------------- 
------------------------------------------------------------------------------------------- 
--- Detallado de Beneficiarios 
	
	Set @sQuery = ' Select ' + char(39) + @EncPrincipal + char(39) + ' as EncabezadoPrincipal, ' + char(39) + @EncSecundario + char(39) + 
	' as EncabezadoSecundario, IdFarmacia, Farmacia,  
	FolioReferencia, IdBeneficiario, Beneficiario, Folio, NumReceta, FechaReceta, 
	ClaveSSA, DescripcionCorta, Cantidad, PrecioLicitacion, ImporteEAN, FechaInicial, FechaFinal 
	Into Rpt_Admon_Cte_Beneficiarios 
	From ' + @Tabla + ' (Nolock)  
	Where IdBeneficiario In ( Select IdBeneficiario From Cte_BeneficiariosProcesar (Nolock) ) 
	Group By IdFarmacia, Farmacia, FolioReferencia, IdBeneficiario, Beneficiario, Folio, NumReceta, FechaReceta, 
	ClaveSSA, DescripcionCorta, Cantidad, PrecioLicitacion, ImporteEAN, FechaInicial, FechaFinal  
	Order By IdFarmacia, Beneficiario '

	Exec (@sQuery) 

End 
Go--#SQL