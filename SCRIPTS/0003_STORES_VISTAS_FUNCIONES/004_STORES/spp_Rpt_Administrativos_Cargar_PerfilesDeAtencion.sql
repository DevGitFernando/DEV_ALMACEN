----------------------------------------------------------------------------------------------------------------------------------------------- 
----------------------------------------------------------------------------------------------------------------------------------------------- 
----------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( select * from sysobjects (nolock) Where Name = 'RptAdmonDispensacion_PerfilesDeAtencion' and xType = 'U' ) 
	Drop table RptAdmonDispensacion_PerfilesDeAtencion   
Go--#SQL 

----------------------------------------------------------------------------------------------------------------------------------------------- 
----------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Rpt_Administrativos_Cargar_PerfilesDeAtencion' and xType = 'P' ) 
   Drop Proc spp_Rpt_Administrativos_Cargar_PerfilesDeAtencion
Go--#SQL  
 
 ----	Exec spp_Rpt_Administrativos_Cargar_PerfilesDeAtencion 0
 
 
Create Proc spp_Rpt_Administrativos_Cargar_PerfilesDeAtencion 
(
	@IdPerfilAtencion int = 0, @IdSubPerfilAtencion int = 0 
)
With Encryption 
As 
Begin 
Set NoCount On 

/* 
	select * from RptAdmonDispensacion (nolock)	
*/

	---------------- Validar la fecha de creacion de la tabla 
	--If Exists ( select * from sysobjects (nolock) Where Name = 'RptAdmonDispensacion_PerfilesDeAtencion' and xType = 'U' ) 
	--Begin 
	--	If Exists ( select * from sysobjects (nolock) Where Name = 'RptAdmonDispensacion_PerfilesDeAtencion' and xType = 'U' and datediff(minute, crDate, getdate()) > 10 ) 
	--		Drop table RptAdmonDispensacion_PerfilesDeAtencion  
	--End 



	If @IdPerfilAtencion = 0 and @IdSubPerfilAtencion = 0 
		Begin
			If Exists ( select * from sysobjects (nolock) Where Name = 'RptAdmonDispensacion_PerfilesDeAtencion' and xType = 'U' ) 
				Drop table RptAdmonDispensacion_PerfilesDeAtencion  
			
			Select * Into RptAdmonDispensacion_PerfilesDeAtencion From RptAdmonDispensacion_Detallado (nolock)			
		End 
	


	If Not Exists ( select * from sysobjects (nolock) Where Name = 'RptAdmonDispensacion_PerfilesDeAtencion' and xType = 'U' ) 
		Begin			
			Select * Into RptAdmonDispensacion_PerfilesDeAtencion From RptAdmonDispensacion_Detallado (nolock) 
		End
	

	Delete From RptAdmonDispensacion_Detallado 
	
	Insert Into RptAdmonDispensacion_Detallado 
	Select * From RptAdmonDispensacion_PerfilesDeAtencion (nolock) 
	Where IdPerfilAtencion = @IdPerfilAtencion	and IdSubPerfilAtencion = @IdSubPerfilAtencion 
	
 			
End
Go--#SQL 