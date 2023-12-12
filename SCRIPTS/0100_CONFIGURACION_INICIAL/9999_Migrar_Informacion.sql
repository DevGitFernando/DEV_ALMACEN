/*
	Insert Into CatFarmacias (IdEstado, IdFarmacia, NombreFarmacia, EsDeConsignacion, ManejaVtaPubGral, ManejaControlados, IdJurisdiccion, IdRegion, IdSubRegion, EsAlmacen, IdAlmacen,
		EsFrontera, IdMunicipio, IdColonia, Domicilio, CodigoPostal, Telefonos, Email, Actualizado, IdTipoUnidad, EsUnidosis, Status)
	Select
		IdEstado, IdFarmacia + 1000 As IdFarmacia, NombreFarmacia, EsDeConsignacion, ManejaVtaPubGral, ManejaControlados, IdJurisdiccion, IdRegion, IdSubRegion, EsAlmacen, IdAlmacen,
		EsFrontera, IdMunicipio, IdColonia, Domicilio, CodigoPostal, Telefonos, Email, Actualizado, IdTipoUnidad, EsUnidosis, Status
	From CatFarmacias 
	Where IdEstado = '21' And Idfarmacia > '2000'
*/


  Declare
		@IdEmpresa Varchar(3),
		@IdEmpresaNueva Varchar(3),
		@IdEstado Varchar(2),
		@IdFarmacia varchar(4),
		@IdFarmaciaNueva varchar(4),
		@Sql varchar(3000),
		@Ejecutar int

	Set @Ejecutar = 1


    Declare tmpCursor1 Cursor For
	Select IdEmpresa, IdEmpresaNueva, IdEstado, IdFarmacia, IdFarmaciaNueva
	From CatFarmacias_Migracion
	Where Status = 'A'
    Open tmpCursor1
    FETCH NEXT FROM tmpCursor1 Into @IdEmpresa, @IdEmpresaNueva, @IdEstado,  @IdFarmacia, @IdFarmaciaNueva
        WHILE @@FETCH_STATUS = 0
        BEGIN

		Set @Sql = ' Exec spp_CFG_OP__01__SubFarmacias ' +
			Char(39)  + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmacia + CHAR(39) + ', ' + Char(39) + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmaciaNueva + CHAR(39)

		Set @Sql += ' Exec spp_CFG_OP__02__Movimientos ' +
			Char(39)  + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmacia + CHAR(39) + ', ' + Char(39) + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmaciaNueva + CHAR(39)

		Set @Sql += ' Exec spp_CFG_OP__03__Clientes_SubClientes ' +
			Char(39)  + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmacia + CHAR(39) + ', ' + Char(39) + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmaciaNueva + CHAR(39)

		Set @Sql += ' Exec spp_CFG_OP__04__Programas_SubProgramas ' +
			Char(39)  + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmacia + CHAR(39) + ', ' + Char(39) + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmaciaNueva + CHAR(39)

		Set @Sql += ' Exec spp_CFG_OP__05__Servicios_Areas ' +
			Char(39)  + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmacia + CHAR(39) + ', ' + Char(39) + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmaciaNueva + CHAR(39)

		Set @Sql += ' Exec spp_CFG_OP__06__Personal ' +
			Char(39)  + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmacia + CHAR(39) + ', ' + Char(39) + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmaciaNueva + CHAR(39)

		Set @Sql += ' Exec spp_CFG_OP__07__Usuarios ' +
			Char(39)  + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmacia + CHAR(39) + ', ' + Char(39) + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmaciaNueva + CHAR(39)

		Set @Sql += ' Exec spp_CFG_OP__08__Permisos ' +
			Char(39)  + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmacia + CHAR(39) + ', ' + Char(39) + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmaciaNueva + CHAR(39)

		Set @Sql += ' Exec spp_CFG_OP__09__ProductosEstado ' +
			Char(39)  + @IdEstado + CHAR(39)

		Set @Sql += ' Exec spp_CFG_OP__10__NivelesAtencion_Miembros ' +
			Char(39)  + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmacia + CHAR(39) + ', ' + Char(39) + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmaciaNueva + CHAR(39)

		Set @Sql += ' Exec spp_CFG_OP__11__ConfigurarConexiones ' +
			Char(39)  + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmacia + CHAR(39) + ', ' + Char(39) + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmaciaNueva + CHAR(39)
			
		Set @Sql += ' Exec spp_CFG_OP__12__CFG_EmpresasFarmacias ' +
			Char(39)  + @IdEmpresa + CHAR(39) + ', ' + Char(39)  + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmacia + CHAR(39) + ', ' +
			Char(39)  + @IdEmpresaNueva + CHAR(39) + ', ' + Char(39) + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmaciaNueva + CHAR(39)		

		Set @Sql += ' Exec spp_CFG_OP__13__CFG_Farmacias_ConvenioVales ' +
			Char(39)  + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmacia + CHAR(39) + ', ' + Char(39) + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmaciaNueva + CHAR(39)

		Set @Sql += ' Exec spp_CFG_OP__14__CatFarmacias_ProveedoresVales ' +
			Char(39)  + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmacia + CHAR(39) + ', ' + Char(39) + @IdEstado + CHAR(39) + ', ' + Char(39) + @IdFarmaciaNueva + CHAR(39)

		if (@Ejecutar = 0)
			Begin
				Print(@Sql)
			End
		Else
			Begin
				Exec(@Sql)
			End


		FETCH NEXT FROM tmpCursor1 Into  @IdEmpresa, @IdEmpresaNueva, @IdEstado,  @IdFarmacia, @IdFarmaciaNueva
        END
    Close tmpCursor1
    Deallocate tmpCursor1