--	sp_listacolumnas INV__Inventario_CargaMasiva 

----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'sp_INV_Procesar_Archivo_De_Entrada' and xType = 'P' ) 
   Drop Proc sp_INV_Procesar_Archivo_De_Entrada
Go--#SQL 
   
Create Proc sp_INV_Procesar_Archivo_De_Entrada 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '1182' 
) 
As 
Begin 
Set NoCount On 
Declare 
	@iPosicion int 

	Create Table #input 
	( 
		Ubicacion_Base_Aux varchar(50), 	
		Ubicacion_Base varchar(50), 
		IdPasillo varchar(10) default '', 
		IdEstante varchar(10) default '', 
		IdEntrepaño varchar(10) default '', 
		CodigoEAN varchar(30) default '', 
		ClaveLote varchar(30) default '', 		
		Caducidad_Base varchar(30) default '', 
		Caducidad varchar(30) default '', 
		Piezas int 
	) 

	Insert Into #input ( Ubicacion_Base_Aux, Ubicacion_Base, CodigoEAN, ClaveLote, Caducidad_Base, Piezas ) 
	Select Ubi, Ubi, EAN, Lote, right('00000' + Caducidad, 7), Piezas 
	from Output 

--- Establecer la caducidad 
	Update I Set Caducidad = right(Caducidad_Base, 4) + '-' + left(Caducidad_Base, 2) + '-01' 
	From #input I 

---------------------- Establecer las ubicaciones 
	Update I Set IdPasillo = Left(Ubicacion_Base, charindex('-', Ubicacion_Base) -1)
	From #input I 
	
	Update I Set Ubicacion_Base = right(Ubicacion_Base, len(Ubicacion_Base) - (charindex('-', Ubicacion_Base)))
	From #input I 
		
		
	Update I Set IdEstante = Left(Ubicacion_Base, charindex('-', Ubicacion_Base) -1)
	From #input I 
	
	Update I Set Ubicacion_Base = right(Ubicacion_Base, len(Ubicacion_Base) - (charindex('-', Ubicacion_Base)))
	From #input I 	
	
	Update I Set IdEntrepaño = Ubicacion_Base 
	From #input I 			
---------------------- Establecer las ubicaciones 

	
-- @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '1182'

--	Insert Into INV__Inventario_CargaMasiva 		( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño, CodigoEAN, ClaveLote, Caducidad, Piezas )
	Select 
		dbo.fg_Apostrofos(@IdEmpresa) as IdEmpresa, dbo.fg_Apostrofos(@IdEstado) as IdEstado, dbo.fg_Apostrofos(@IdFarmacia) as IdFarmacia, 
		dbo.fg_Apostrofos(IdPasillo) as IdPasillo, 
		dbo.fg_Apostrofos(IdEstante) as IdEstante, 
		dbo.fg_Apostrofos(IdEntrepaño) as IdEntrepaño, 
		dbo.fg_Apostrofos(CodigoEAN) as CodigoEAN, 
		dbo.fg_Apostrofos(ClaveLote) as ClaveLote, 
		dbo.fg_Apostrofos(Caducidad) as Caducidad, 
		cast(Piezas as int) as Piezas 
	From #input 

--sp_listacolumnas INV__Inventario_CargaMasiva 


	
--	sp_INV_Procesar_Archivo_De_Entrada 


End 
Go--#SQL 


-------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'fg_Apostrofos' and xType = 'FN' ) 
   Drop Function fg_Apostrofos 
Go--#SQL 
    
Create Function dbo.fg_Apostrofos(@Cadena varchar(max) = '' )
Returns varchar(max) 
As
Begin 
	
	return char(39) + IsNull(@Cadena, '') + char(39) 
	
End 
Go--#SQL 
	
	