----------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'fg_FACT_TiposDeRemisiones' and xType = 'TF' )
   Drop Function fg_FACT_TiposDeRemisiones  
Go--#SQL     
      
Create Function dbo.fg_FACT_TiposDeRemisiones
(
	@Referencia int -- varchar(5) = '' 
--	@IdEmpresa varchar(3) = '', @IdEstado varchar(2) = ''  

--	@IdFuenteFinanciamiento varchar(4) = '', @IdFinanciamiento varchar(4) = '', @IdDocumento varchar(4) = ''  
)  
returns @Tabla Table 
( 
	IdEmpresa varchar(3), 
	IdEstado varchar(2), 
	TipoDeRemision int,  
	TipoDeRemisionDesc varchar(200), 
	TipoDeRemisionDesc_Base varchar(200) 
) 
With Encryption 
As 
Begin 
Declare 
	@sSql varchar(max),  
	@IdEmpresa varchar(3), 
	@IdEstado varchar(2) 

	Set @IdEmpresa = '' 
	Set @IdEstado = '' 


	Insert Into @Tabla  
	Select 
		R.IdEmpresa, R.IdEstado,  
		R.TipoDeRemision, IsNull(O.Descripcion, R.TipoDeRemisionDesc_Base) as TipoDeRemisionDesc, R.TipoDeRemisionDesc_Base 
	From 
	(	
		Select Distinct 
			C.IdEmpresa, C.IdEstado, E.TipoDeRemision, E.Descripcion as TipoDeRemisionDesc_Base
		From FACT_TiposDeRemisiones E (NoLock),  
		( 
			Select E.IdEmpresa, S.IdEstado 
			From CatEmpresas E (NoLock), CatEstados S (NoLock) 
		) C
	) R   
	Left Join FACT_TiposDeRemisiones_Operacion O (NoLock) On ( O.IdEmpresa = R.IdEmpresa and O.IdEstado = R.IdEstado and R.TipoDeRemision = O.TipoDeRemision ) 
	Order by R.TipoDeRemision 


	------------ Regresar el resultado 
    return 
          
End 
Go--#SQL 


