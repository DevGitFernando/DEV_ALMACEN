----------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (noLock) where Name = 'sp_Net_CheckUpdateVersion' and xType = 'P' ) 
   Drop Proc sp_Net_CheckUpdateVersion 
Go--#SQL      

---		sp_Net_CheckUpdateVersion 'Ortopedic inventarios.exe', '1.0.11.0', 1  

Create Proc sp_Net_CheckUpdateVersion 
(
	@Modulo varchar(100) = 'Proveedores.exe', @Version varchar(20) = '0.0.0.0', @ForzarUpdate int = 0 
) 
With Encryption 
As 
Begin 
Set NoCount On 

Declare 
	@bExistenDatos bit,  
	@bEsUpdater bit 

	Select @bEsUpdater = case when upper(@Modulo) = upper('Updater Ortopedic.exe') then 1 else 0 end 
	Set @bExistenDatos = 0 

--- Generar la Tabla Intermedia de Version 
	Select IdModulo, Nombre, Extension, Version, MD5, 
		EmpacadoModulo, Tamaño, FechaRegistro, FechaActualizacion, 0 as Keyx, Status, Actualizado, 0 as Iteracion  
	into #tmpVersion 	
    From Net_Modulos (NoLock) 
    Where 1 = 0 
    

--- Agregar el Modulo Solicitado 
	Insert Into #tmpVersion ( IdModulo, Nombre, Extension, Version, MD5, 
		EmpacadoModulo, Tamaño, FechaRegistro, FechaActualizacion, Keyx, Status, Actualizado, Iteracion ) 
	Select IdModulo, Nombre, Extension, Version, MD5, 
		EmpacadoModulo, Tamaño, FechaRegistro, FechaActualizacion, Keyx, Status, Actualizado, 1 as Iteracion 
	-- into #tmpVersion 	
    From Net_Modulos (NoLock) 
	Where Nombre = @Modulo and (Version > @Version or VersionArchivo > @Version )  

	
	Select @bExistenDatos = 1 from #tmpVersion 
	If ( @bExistenDatos = 1 or @ForzarUpdate = 1  ) and @bEsUpdater =  0 
	--If @ForzarUpdate = 1 and @bEsUpdater =  0 
	   Begin 
	        Delete From #tmpVersion 
	        
	        ------ Modulo Solicitado 
			Insert Into #tmpVersion ( IdModulo, Nombre, Extension, Version, MD5, 
				EmpacadoModulo, Tamaño, FechaRegistro, FechaActualizacion, Keyx, Status, Actualizado, Iteracion ) 
			Select -- top 2 
				IdModulo, Nombre, Extension, Version, MD5, 
				EmpacadoModulo, Tamaño, FechaRegistro, FechaActualizacion, Keyx, Status, Actualizado, 2 as Iteracion 
			-- into #tmpVersion 	
			From Net_Modulos (NoLock) 
			Where Extension In ( 'exe', 'dll', 'xslt' ) 
		    
		    ------ Referencias  
	        Insert Into #tmpVersion ( IdModulo, Nombre, Extension, Version, MD5, 
				EmpacadoModulo, Tamaño, FechaRegistro, FechaActualizacion, Keyx, Status, Actualizado, Iteracion ) 
			Select M.IdModulo, M.Nombre, M.Extension, M.Version, M.MD5, 
				M.EmpacadoModulo, M.Tamaño, M.FechaRegistro, M.FechaActualizacion, M.Keyx, M.Status, M.Actualizado, 3 as Iteracion 
			From Net_Modulos M (NoLock) 
			Where Exists 
				( 
					Select * 
					From vw_Net_Modulos_Relacionados R (noLock) 
					Where R.Modulo =  @Modulo and M.Nombre = R.ModuloRelacionado and R.Status = 'A'
				) 
				and not Exists 
				( 
					Select * 
					From #tmpVersion L (noLock) 
					Where L.Nombre =  M.Nombre and L.Status = 'A'
				) 
				--and 1 = 0 


			Delete From #tmpVersion Where Nombre like '%Servicio%cliente%'
       End 



--- Devolver la version solicitada    
    Select IdModulo, Nombre, Extension, Version, MD5, 
		Tamaño, FechaRegistro, FechaActualizacion, Keyx, Status, Actualizado, Iteracion, EmpacadoModulo  
    From #tmpVersion (nolock) 
    Order by FechaActualizacion, Iteracion  
    
    
    
End 
Go--#SQL       