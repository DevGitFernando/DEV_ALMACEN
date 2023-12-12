
Declare 
	@IdEstado int, 
	@IdFarmacia int, 
	@iEjecutar int  


	Set @iEjecutar = 0  
	Set @IdEstado = 21 
	Set @IdFarmacia = 5 

----------------- 
	If Exists ( Select * From Sysobjects (nolock) Where name = 'tmp_Permisos_Base' and xType = 'u' ) 
	   Drop Table tmp_Permisos_Base  

	Select IdEstado, IdSucursal, IdGrupo, Arbol, Rama, Ruta, TipoRama, RutaCompleta, Status, Actualizado, getdate() as FechaUpdate
	Into tmp_Permisos_Base 
	from Net_Privilegios_Grupo 
	Where IdEstado = @IdEstado and IdSucursal = @IdFarmacia 
	
	
----------------- 	
	If Exists ( Select * From Sysobjects (nolock) Where name = 'tmp_Clonar_Permisos' and xType = 'u' ) 
	   Drop Table tmp_Clonar_Permisos 
	   	
--	Insert Into Net_Privilegios_Grupo 
	Select C.IdEstado, F.IdFarmacia as IdSucursal, C.IdGrupo, C.Arbol, C.Rama, C.Ruta, C.TipoRama, C.RutaCompleta, C.Status, C.Actualizado, getdate() as FechaUpdate
	Into tmp_Clonar_Permisos 
	From tmp_Permisos_Base C, CatFarmacias F 
	Where C.IdEstado = F.IdEstado 
	      and IdFarmacia not in ( 1, 2, 3, @IdFarmacia ) 
	 
	

	if @iEjecutar = 0  
		Begin 	
			Select C.IdEstado, C.IdSucursal, C.IdGrupo, C.Arbol, C.Rama, C.Ruta, C.TipoRama, C.RutaCompleta, C.Status, C.Actualizado, getdate() as FechaUpdate 
			From tmp_Clonar_Permisos C  
			Where Not Exists 
				( 
					Select * 
					From Net_Privilegios_Grupo P 
					Where C.IdEstado = P.IdEstado and C.IdSucursal = P.IdSucursal and C.IdGrupo = P.IdGrupo 
						  and C.Arbol = P.Arbol and C.Rama = P.Rama 
				) 	
			End 
	Else
		Begin 	
			Insert Into Net_Privilegios_Grupo 
			Select C.IdEstado, C.IdSucursal, C.IdGrupo, C.Arbol, C.Rama, C.Ruta, C.TipoRama, C.RutaCompleta, C.Status, C.Actualizado, getdate() as FechaUpdate 
			From tmp_Clonar_Permisos C  
			Where Not Exists 
				( 
					Select * 
					From Net_Privilegios_Grupo P 
					Where C.IdEstado = P.IdEstado and C.IdSucursal = P.IdSucursal and C.IdGrupo = P.IdGrupo 
						  and C.Arbol = P.Arbol and C.Rama = P.Rama 
				) 	
	End 	
	
--	select * 	from tmp_Clonar_Permisos 
	
	
--	sp_listacolumnas Net_Privilegios_Grupo 
                   
                   
                   