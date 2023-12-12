
-----	 sp_listacolumnas catFarmacias   

-----	 sp_listacolumnas CFGS_ConfigurarConexiones   

-----	 sp_listacolumnas CatPersonal    


---			Drop table #tmp_Farmacias_Migracion  

---			Drop table #tmp_Farmacias  


Begin tran 


----		rollback tran  

----		commit tran  

	
	delete from CatFarmacias_Migracion where IdEstado in ( 11, 22 ) 

	------------------------------------------------------------------------------------------------------------------------------------  
	Select 
		'001' as IdEmpresa, IdEstado, IdFarmacia, 
		'001' as IdEmpresaNueva, 
		right('00000000' + cast((cast(Idfarmacia as int) + 1000 ) as varchar(4)), 4) as IdFarmaciaNueva, 
		Status, Actualizado
	Into #tmp_Farmacias_Migracion  
	from catFarmacias (NoLock) 
	where IdEstado = 11 
		and ( IdFarmacia between 3000 and 4000 ) 
		-- and Status = 'C' 
	order by IdFarmacia 

	insert Into #tmp_Farmacias_Migracion  
	Select 
		'001' as IdEmpresa, IdEstado, IdFarmacia, 
		'001' as IdEmpresaNueva, 
		right('00000000' + cast((cast(Idfarmacia as int) + 100 ) as varchar(4)), 4) as IdFarmaciaNueva, 
		Status, Actualizado 
	from catFarmacias (NoLock) 
	where IdEstado = 22 
		and ( IdFarmacia between 3 and 100 ) 
		-- and Status = 'C' 
	order by IdFarmacia 


	select 
		IdEstado, 	
		right('00000000' + cast((cast(Idfarmacia as int) + 1000 ) as varchar(4)), 4) as IdFarmacia, 		
		NombreFarmacia, EsDeConsignacion, ManejaVtaPubGral, ManejaControlados, IdJurisdiccion, IdRegion, IdSubRegion, EsAlmacen, IdAlmacen, EsFrontera, IdMunicipio, IdColonia, Domicilio, CodigoPostal, Telefonos, eMail, Status, Actualizado, IdTipoUnidad, EsUnidosis, CLUES, NombrePropio_UMedica 
	Into #tmp_Farmacias 
	from catFarmacias (NoLock) 
	where IdEstado = 11 
		and ( IdFarmacia between 3000 and 4000 ) 
		-- and Status = 'C' 
	order by IdFarmacia 


	Insert Into #tmp_Farmacias 
	select 
		IdEstado, 	
		right('00000000' + cast((cast(Idfarmacia as int) + 100 ) as varchar(4)), 4) as IdFarmacia, 		
		NombreFarmacia, EsDeConsignacion, ManejaVtaPubGral, ManejaControlados, IdJurisdiccion, IdRegion, IdSubRegion, EsAlmacen, IdAlmacen, EsFrontera, IdMunicipio, IdColonia, Domicilio, CodigoPostal, Telefonos, eMail, Status, Actualizado, IdTipoUnidad, EsUnidosis, CLUES, NombrePropio_UMedica  
	from catFarmacias (NoLock) 
	where IdEstado = 22 
		and ( IdFarmacia between 3 and 100 ) 
		-- and Status = 'C' 
	order by IdFarmacia 





	Insert Into catFarmacias 
	( 
		IdEstado, IdFarmacia, NombreFarmacia, EsDeConsignacion, ManejaVtaPubGral, ManejaControlados, IdJurisdiccion, IdRegion, IdSubRegion, EsAlmacen, IdAlmacen, EsFrontera, IdMunicipio, IdColonia, Domicilio, CodigoPostal, Telefonos, eMail, 
		Status, Actualizado, IdTipoUnidad, EsUnidosis, CLUES, NombrePropio_UMedica
	) 
	select 
		IdEstado, IdFarmacia, NombreFarmacia, EsDeConsignacion, ManejaVtaPubGral, ManejaControlados, IdJurisdiccion, IdRegion, IdSubRegion, EsAlmacen, IdAlmacen, EsFrontera, IdMunicipio, IdColonia, Domicilio, CodigoPostal, Telefonos, eMail, 
		Status, Actualizado, IdTipoUnidad, EsUnidosis, CLUES, NombrePropio_UMedica
	From #tmp_Farmacias L 
	Where Not Exists 
	( 
		Select * 
		From CatFarmacias F (NoLock) 
		Where L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia 
	) 


	Insert Into CatFarmacias_Migracion ( IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado ) 
	select 
		 IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado
	From #tmp_Farmacias_Migracion L 
	Where Not Exists 
	( 
		Select * 
		From CatFarmacias_Migracion F (NoLock) 
		Where L.IdEmpresa = F.IdEmpresa and L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia		
			and L.IdEmpresaNueva = F.IdEmpresaNueva and L.IdFarmaciaNueva = F.IdFarmaciaNueva 
	) 


	select * from CatFarmacias_Migracion order by idestado, idfarmacia 


	select * from catfarmacias (nolock) where IdEstado = 22 order by IdFarmacia 



/* 
	------------------------------------------------------------------------------------------------------------------------------------  
	Select  
		IdEstado, 
		right('00000000' + cast((cast(Idfarmacia as int) + 1000 ) as varchar(4)), 4) as IdFarmacia, 		
		Servidor, WebService, PaginaWeb, Status, Actualizado, SSL, ModoActivoDeTransferenciaFTP 
	Into #tmp_Farmacias_Urls 
	From CFGS_ConfigurarConexiones (NoLock) 
	where IdEstado = 11 
		and IdFarmacia >= 11 



	Insert Into CFGS_ConfigurarConexiones (  IdEstado, IdFarmacia, Servidor, WebService, PaginaWeb, Status, Actualizado, SSL, ModoActivoDeTransferenciaFTP ) 
	Select  IdEstado, IdFarmacia, Servidor, WebService, PaginaWeb, Status, Actualizado, SSL, ModoActivoDeTransferenciaFTP   
	From #tmp_Farmacias_Urls L 
	Where Not Exists 
	( 
		Select * 
		From CFGS_ConfigurarConexiones F (NoLock) 
		Where L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia 
	) 
*/ 


/* 
	------------------------------------------------------------------------------------------------------------------------------------  
	-- Insert Into CatPersonal (  IdEstado, IdFarmacia, IdPersonal, Nombre, ApPaterno, ApMaterno, FechaRegistro, Actualizado, Status ) 
	Select 
		IdEstado, 
		right('00000000' + cast((cast(Idfarmacia as int) + 1000 ) as varchar(4)), 4) as IdFarmacia, 		
		IdPersonal, Nombre, ApPaterno, ApMaterno, FechaRegistro, Actualizado, Status  
	Into #tmp_Personal 
	From CatPersonal (NoLock) 
	where IdEstado = 11 
		and ( IdFarmacia >= 11 or IdFarmacia = 5 ) 


	Insert Into CatPersonal (  IdEstado, IdFarmacia, IdPersonal, Nombre, ApPaterno, ApMaterno, FechaRegistro, Actualizado, Status ) 
	Select IdEstado, IdFarmacia, IdPersonal, Nombre, ApPaterno, ApMaterno, FechaRegistro, Actualizado, Status 
	From #tmp_Personal L (NoLock) 
	Where Not Exists 
	( 
		Select * 
		From CatPersonal F (NoLock) 
		Where L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdPersonal = F.IdPersonal 
	) 

*/  


