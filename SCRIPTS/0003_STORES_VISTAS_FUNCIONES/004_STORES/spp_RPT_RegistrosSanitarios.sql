----------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_RPT_RegistrosSanitarios' and xType = 'P' ) 
   Drop Proc spp_RPT_RegistrosSanitarios 
Go--#SQL 

Create Proc spp_RPT_RegistrosSanitarios 
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

	Select 
		TipoDeProducto, ClaveSSA_Base, ClaveSSA, Laboratorio, DescripcionClave, Presentacion, CodigoEAN, 
		FolioRegistroSanitario, StatusRegistroAux AS Status, Vigente, 
		FechaVigencia As 'Fecha Vigencia', FechaRegistro as 'Fecha de alta en sistema', FechaUltimaActualizacion as 'Fecha de ultima actualizacion' 
	Into #tmpRegistrosSanitarios  
	From vw_RegistrosSanitarios_CodigoEAN (NoLock) 
	Where StatusRegistro <> '-1'  
	Order By ClaveSSA, Laboratorio, FolioRegistroSanitario 
	 

--------------------------- SALIDA 	 
	Select 
		TipoDeProducto, ClaveSSA_Base, ClaveSSA, Laboratorio, DescripcionClave, Presentacion, 
		FolioRegistroSanitario, Status, Vigente, [Fecha Vigencia], [Fecha de alta en sistema], [Fecha de ultima actualizacion]   
	From #tmpRegistrosSanitarios 
	Group by TipoDeProducto, ClaveSSA_Base, ClaveSSA, Laboratorio, DescripcionClave, Presentacion, FolioRegistroSanitario, Status, Vigente, 
		[Fecha Vigencia], [Fecha de alta en sistema], [Fecha de ultima actualizacion]     
	Order By ClaveSSA, Laboratorio, FolioRegistroSanitario  

	 
	Select 
		TipoDeProducto, ClaveSSA_Base, ClaveSSA, Laboratorio, DescripcionClave, Presentacion, CodigoEAN, 
		FolioRegistroSanitario, Status, Vigente, [Fecha Vigencia], [Fecha de alta en sistema], [Fecha de ultima actualizacion]    
	From #tmpRegistrosSanitarios 
	Order By ClaveSSA, Laboratorio, FolioRegistroSanitario 



End 
Go--#SQL  



