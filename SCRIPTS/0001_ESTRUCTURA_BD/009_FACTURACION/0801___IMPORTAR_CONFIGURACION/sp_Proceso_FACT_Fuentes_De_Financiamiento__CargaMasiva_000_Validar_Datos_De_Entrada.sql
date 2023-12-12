------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Proceso_FACT_Fuentes_De_Financiamiento__CargaMasiva_000_Validar_Datos_De_Entrada' and xType = 'P' ) 
   Drop Proc sp_Proceso_FACT_Fuentes_De_Financiamiento__CargaMasiva_000_Validar_Datos_De_Entrada 
Go--#SQL 

Create Proc sp_Proceso_FACT_Fuentes_De_Financiamiento__CargaMasiva_000_Validar_Datos_De_Entrada
(
	@Tipo int = 2
) 
With Encryption 
As 
Begin 
Set NoCount On

	Select top 0 Cast('' As Varchar(200)) As Descripcion
	Into #Lista 



	select 
		IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, 
		PorcParticipacion, 
		sum(CantidadPresupuestadaPiezas) as CantidadPresupuestadaPiezas, sum(CantidadPresupuestada) as CantidadPresupuestada, sum(CantidadAsignada) as CantidadAsignada, sum(CantidadRestante) as CantidadRestante, 
		Status, Actualizado, SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida, 
		CostoBase, Porcentaje_01, Porcentaje_02, Costo, Agrupacion, CostoUnitario, TasaIva, Iva, ImporteNeto, 
	
		IdEstado, IdFarmacia, Referencia_01, Referencia_02, Referencia_03, Referencia_04, Referencia_05, 
		IdBeneficiario, NombreBeneficiario, IdCliente, IdSubCliente, IdBeneficiario_Relacionado, 
		Tipo, PrecioBase, Incremento, PorcentajeIncremento, PrecioFinal, IdGrupo, TipoRemision, FechaVigencia	 
	Into #tmp___FACT_Fuentes_De_Financiamiento__CargaMasiva
	from FACT_Fuentes_De_Financiamiento__CargaMasiva  
	Group by 
		IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, 
		PorcParticipacion, 
		--CantidadPresupuestadaPiezas, CantidadPresupuestada, CantidadAsignada, CantidadRestante, 
		Status, Actualizado, SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida, 
		CostoBase, Porcentaje_01, Porcentaje_02, Costo, Agrupacion, CostoUnitario, TasaIva, Iva, ImporteNeto, 
	
		IdEstado, IdFarmacia, Referencia_01, Referencia_02, Referencia_03, Referencia_04, Referencia_05, 
		IdBeneficiario, NombreBeneficiario, IdCliente, IdSubCliente, IdBeneficiario_Relacionado, 
		Tipo, PrecioBase, Incremento, PorcentajeIncremento, PrecioFinal, IdGrupo, TipoRemision, FechaVigencia


	
	----------------------------- Ignorar duplicados 
	Delete From FACT_Fuentes_De_Financiamiento__CargaMasiva 

	Insert Into FACT_Fuentes_De_Financiamiento__CargaMasiva 
	( 
		IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, 
		PorcParticipacion, 
		CantidadPresupuestadaPiezas, CantidadPresupuestada, CantidadAsignada, CantidadRestante, 
		Status, Actualizado, SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida, 
		CostoBase, Porcentaje_01, Porcentaje_02, Costo, Agrupacion, CostoUnitario, TasaIva, Iva, ImporteNeto, 
	
		IdEstado, IdFarmacia, Referencia_01, Referencia_02, Referencia_03, Referencia_04, Referencia_05, 
		IdBeneficiario, NombreBeneficiario, IdCliente, IdSubCliente, IdBeneficiario_Relacionado, 
		Tipo, PrecioBase, Incremento, PorcentajeIncremento, PrecioFinal, IdGrupo, TipoRemision, FechaVigencia
	) 
	select 
		IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, 
		PorcParticipacion, 
		CantidadPresupuestadaPiezas, CantidadPresupuestada, CantidadAsignada, CantidadRestante, 
		Status, Actualizado, SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida, 
		CostoBase, Porcentaje_01, Porcentaje_02, Costo, Agrupacion, CostoUnitario, TasaIva, Iva, ImporteNeto, 
	
		IdEstado, IdFarmacia, Referencia_01, Referencia_02, Referencia_03, Referencia_04, Referencia_05, 
		IdBeneficiario, NombreBeneficiario, IdCliente, IdSubCliente, IdBeneficiario_Relacionado, 
		Tipo, PrecioBase, Incremento, PorcentajeIncremento, PrecioFinal, IdGrupo, TipoRemision, FechaVigencia
	From #tmp___FACT_Fuentes_De_Financiamiento__CargaMasiva 
	----------------------------- Ignorar duplicados 




	Update FACT_Fuentes_De_Financiamiento__CargaMasiva
	Set 
		IdFuenteFinanciamiento = Right ('0000' + IdFuenteFinanciamiento, 4),
		IdFinanciamiento = Right ('0000'+ IdFinanciamiento, 4),
		--ClaveSSA varchar(50) NOT NULL DEFAULT '',
		--PorcParticipacion numeric(14, 4) NOT NULL DEFAULT 100,
		--CantidadPresupuestadaPiezas int NOT NULL DEFAULT 0,
		--CantidadPresupuestada int NOT NULL DEFAULT 0,
		--CantidadAsignada int NOT NULL DEFAULT 0,
		--CantidadRestante int NOT NULL DEFAULT 0,
		--Status varchar(1) NOT NULL DEFAULT 'A',
		--Actualizado smallint NOT NULL DEFAULT 0,
		--SAT_ClaveProducto_Servicio varchar(20) NOT NULL DEFAULT '',
		--SAT_UnidadDeMedida varchar(5) NOT NULL DEFAULT '',
		--Referencia_04 varchar(100) NOT NULL DEFAULT '',
		--CostoBase numeric(14, 4) NOT NULL DEFAULT 0,
		--Porcentaje_01 numeric(14, 4) NOT NULL DEFAULT 0,
		--Porcentaje_02 numeric(14, 4) NOT NULL DEFAULT 0,
		--Costo numeric(14, 4) NOT NULL DEFAULT 0,
		--Agrupacion int NOT NULL DEFAULT 0,
		--CostoUnitario numeric(14, 4) NOT NULL DEFAULT 0,
		--TasaIva numeric(14, 4) NOT NULL DEFAULT 0,
		--Iva numeric(14, 4) NOT NULL DEFAULT 0,
		--ImporteNeto numeric(14, 4) NOT NULL DEFAULT 0,

		IdEstado = Right ('0000' + IdEstado, 2),
		IdFarmacia = Right ('0000' + IdFarmacia, 4),
		--Referencia_01 varchar(100) NOT NULL DEFAULT '',
		--Referencia_02 varchar(100) NOT NULL DEFAULT '',
		--Referencia_03 varchar(100) NOT NULL DEFAULT '',
		--Referencia_05 varchar(100) NOT NULL DEFAULT '',

		IdBeneficiario = Right ('00000000000' + IdBeneficiario, 8),
		--NombreBeneficiario varchar(200) NOT NULL DEFAULT '',
		IdCliente = Right ('0000' + IdCliente, 4),
		IdSubCliente = Right ('0000' + IdSubCliente, 4),
		IdBeneficiario_Relacionado = Right ('000000000000' + IdBeneficiario_Relacionado, 8)

		--Tipo int NOT NULL DEFAULT 0,
		--PrecioBase numeric(14, 4) NOT NULL DEFAULT 0,
		--Incremento numeric(14, 4) NOT NULL DEFAULT 0,
		--PorcentajeIncremento numeric(14, 4) NOT NULL DEFAULT 0,
		--PrecioFinal numeric(14, 4) NOT NULL DEFAULT 0

	



	Update FACT_Fuentes_De_Financiamiento_Detalles_Documentos__CargaMasiva
	Set 
		IdFuenteFinanciamiento = Right ('0000' + IdFuenteFinanciamiento, 4),
		IdFinanciamiento = Right ('0000' + IdFinanciamiento, 4),
		IdDocumento = Right ('0000' + IdDocumento, 4)
		--NombreDocumento varchar(200) NOT NULL DEFAULT '',
		--IdFuenteFinanciamiento_Relacionado varchar(4) NOT NULL DEFAULT '',
		--IdFinanciamiento_Relacionado varchar(4) NOT NULL DEFAULT '',
		--IdDocumento_Relacionado varchar(4) NOT NULL DEFAULT '',
		--EsRelacionado bit NOT NULL DEFAULT 0,
		--OrigenDeInsumo int NOT NULL DEFAULT 0,
		--TipoDeDocumento int NOT NULL DEFAULT 0,
		--TipoDeInsumo int NOT NULL DEFAULT 0,
		--ValorNominal numeric(14, 4) NOT NULL DEFAULT 0,
		--ImporteAplicado numeric(14, 4) NOT NULL DEFAULT 0,
		--ImporteAplicado_Aux numeric(14, 4) NOT NULL DEFAULT 0,
		--ImporteRestante numeric(14, 4) NOT NULL DEFAULT 0,
		--AplicaFarmacia bit NOT NULL DEFAULT 0,
		--AplicaAlmacen bit NOT NULL DEFAULT 0,
		--EsProgramaEspecial bit NOT NULL DEFAULT 0,
		--TipoDeBeneficiario int NOT NULL DEFAULT 0,
		--Status varchar(1) NOT NULL DEFAULT 'A'


	If (@Tipo = 1) --Insumo
	Begin
		
		Insert Into #Lista
		Select 'Fuente Invalida' As Descripcion

		Select F.*
		into #001_Fuente
		From FACT_Fuentes_De_Financiamiento__CargaMasiva F
		left Join FACT_Fuentes_De_Financiamiento_Detalles D (NoLock) On (F.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento) 
		Where D.Descripcion Is Null

		Insert Into #Lista
		Select 'Registros Repetidos' As Descripcion

		Select IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, Count(*) Repetidos
		into #001_Repetidos
		From FACT_Fuentes_De_Financiamiento__CargaMasiva
		Group By IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA
		Having	 Count(*) > 1

		Select * From #Lista
		Select * From #001_Fuente
		Select * From #001_Repetidos

	End

	If (@Tipo = 2)--Servicio
	Begin
		
		Insert Into #Lista
		Select 'Fuente Invalida' As Descripcion

		Select F.*
		into #002_Fuente
		From FACT_Fuentes_De_Financiamiento__CargaMasiva F
		left Join FACT_Fuentes_De_Financiamiento_Detalles D (NoLock) On (F.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento) 
		Where D.Descripcion Is Null

		Insert Into #Lista
		Select 'Registros Repetidos' As Descripcion

		Select IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, Count(*) Repetidos
		into #002_Repetidos
		From FACT_Fuentes_De_Financiamiento__CargaMasiva
		Group By IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA
		Having	 Count(*) > 1

		Select * From #Lista
		Select * From #002_Fuente
		Select * From #002_Repetidos

	End

	If (@Tipo = 3)--Documentos
	Begin
		
		Insert Into #Lista
		Select 'Fuente Invalida' As Descripcion

		Select F.*
		into #003_Fuente
		From FACT_Fuentes_De_Financiamiento_Detalles_Documentos__CargaMasiva F
		left Join FACT_Fuentes_De_Financiamiento_Detalles D (NoLock) On (F.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento) 
		Where D.Descripcion Is Null

		Insert Into #Lista
		Select 'Registros Repetidos' As Descripcion

		Select IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento, Count(*) Repetidos
		into #003_Repetidos
		From FACT_Fuentes_De_Financiamiento_Detalles_Documentos__CargaMasiva
		Group By IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento
		Having	 Count(*) > 1

		Select * From #Lista
		Select * From #003_Fuente
		Select * From #003_Repetidos

	End

	If (@Tipo = 4)--Insumo_Clave_Farmacia
	Begin
		
		Insert Into #Lista
		Select 'Fuente Invalida' As Descripcion

		Select F.*
		into #004_Fuente
		From FACT_Fuentes_De_Financiamiento__CargaMasiva F
		left Join FACT_Fuentes_De_Financiamiento_Detalles D (NoLock) On (F.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento) 
		Where D.Descripcion Is Null

		Insert Into #Lista
		Select 'Registros Repetidos' As Descripcion

		Select IdFuenteFinanciamiento, IdFinanciamiento, IdEstado, IdFarmacia, ClaveSSA, Count(*) Repetidos
		into #004_Repetidos
		From FACT_Fuentes_De_Financiamiento__CargaMasiva
		Group By IdFuenteFinanciamiento, IdFinanciamiento, IdEstado, IdFarmacia, ClaveSSA
		Having	 Count(*) > 1

		Select * From #Lista
		Select * From #004_Fuente
		Select * From #004_Repetidos

	End

	If (@Tipo = 5)--Servicio_Clave_Farmacia
	Begin
		
		Insert Into #Lista
		Select 'Fuente Invalida' As Descripcion

		Select F.*
		into #005_Fuente
		From FACT_Fuentes_De_Financiamiento__CargaMasiva F
		left Join FACT_Fuentes_De_Financiamiento_Detalles D (NoLock) On (F.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento) 
		Where D.Descripcion Is Null

		Insert Into #Lista
		Select 'Registros Repetidos' As Descripcion

		Select IdFuenteFinanciamiento, IdFinanciamiento, IdEstado, IdFarmacia, ClaveSSA, Count(*) Repetidos
		into #005_Repetidos
		From FACT_Fuentes_De_Financiamiento__CargaMasiva
		Group By IdFuenteFinanciamiento, IdFinanciamiento, IdEstado, IdFarmacia, ClaveSSA
		Having	 Count(*) > 1

		Select * From #Lista
		Select * From #005_Fuente
		Select * From #005_Repetidos

	End

	If (@Tipo = 6)--Insumo_Clave_Jurisdiccion
	Begin
		
		Insert Into #Lista
		Select 'Fuente Invalida' As Descripcion

		Select F.*
		into #006_Fuente
		From FACT_Fuentes_De_Financiamiento__CargaMasiva F
		left Join FACT_Fuentes_De_Financiamiento_Detalles D (NoLock) On (F.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento) 
		Where D.Descripcion Is Null

		Insert Into #Lista
		Select 'Registros Repetidos' As Descripcion

		Select IdFuenteFinanciamiento, IdFinanciamiento, IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario, Referencia_01, ClaveSSA, Referencia_05, Count(*) Repetidos
		into #006_Repetidos
		From FACT_Fuentes_De_Financiamiento__CargaMasiva
		Group By IdFuenteFinanciamiento, IdFinanciamiento, IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario, Referencia_01, ClaveSSA, Referencia_05
		Having	 Count(*) > 1

		Select * From #Lista
		Select * From #006_Fuente
		Select * From #006_Repetidos

	End

	If (@Tipo = 7)--Servicio_Clave_Jurisdiccion
	Begin
		
		Insert Into #Lista
		Select 'Fuente Invalida' As Descripcion

		Select F.*
		into #007_Fuente
		From FACT_Fuentes_De_Financiamiento__CargaMasiva F
		left Join FACT_Fuentes_De_Financiamiento_Detalles D (NoLock) On (F.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento) 
		Where D.Descripcion Is Null

		Insert Into #Lista
		Select 'Registros Repetidos' As Descripcion

		Select IdFuenteFinanciamiento, IdFinanciamiento, IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario, Referencia_01, ClaveSSA, Referencia_05, Count(*) Repetidos
		into #007_Repetidos
		From FACT_Fuentes_De_Financiamiento__CargaMasiva
		Group By IdFuenteFinanciamiento, IdFinanciamiento, IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario, Referencia_01, ClaveSSA, Referencia_05
		Having	 Count(*) > 1

		Select * From #Lista
		Select * From #007_Fuente
		Select * From #007_Repetidos

	End

	If (@Tipo = 8)--ExcepcionPrecios_Insumos
	Begin
		
		Insert Into #Lista
		Select 'Fuente Invalida' As Descripcion

		Select F.*
		into #008_Fuente
		From FACT_Fuentes_De_Financiamiento__CargaMasiva F
		left Join FACT_Fuentes_De_Financiamiento_Detalles D (NoLock) On (F.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento) 
		Where D.Descripcion Is Null

		Insert Into #Lista 
		Select 'Registros Repetidos' As Descripcion

		Select IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, Referencia_01, Tipo, Count(*) Repetidos
		into #008_Repetidos
		From FACT_Fuentes_De_Financiamiento__CargaMasiva
		Group By IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, Referencia_01, Tipo
		Having	 Count(*) > 1

		Select * From #Lista
		Select * From #008_Fuente 
		Select * From #008_Repetidos

	End

	If (@Tipo = 9)--ExcepcionPrecios_Servicio
	Begin
		
		Insert Into #Lista 
		Select 'Fuente Invalida' As Descripcion

		Select F.*
		into #009_Fuente
		From FACT_Fuentes_De_Financiamiento__CargaMasiva F
		left Join FACT_Fuentes_De_Financiamiento_Detalles D (NoLock) On (F.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento) 
		Where D.Descripcion Is Null

		Insert Into #Lista
		Select 'Registros Repetidos' As Descripcion

		Select IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, Referencia_01, Tipo, Count(*) Repetidos
		into #009_Repetidos
		From FACT_Fuentes_De_Financiamiento__CargaMasiva
		Group By IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, Referencia_01, Tipo
		Having	 Count(*) > 1

		Select * From #Lista
		Select * From #009_Fuente
		Select * From #009_Repetidos 

	End

	If (@Tipo = 10)--BeneficiariosJurisdiccion
	Begin
		
		Insert Into #Lista
		Select 'Fuente Invalida' As Descripcion

		Select F.*
		into #010_Fuente
		From FACT_Fuentes_De_Financiamiento__CargaMasiva F
		left Join FACT_Fuentes_De_Financiamiento_Detalles D (NoLock) On (F.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento) 
		Where D.Descripcion Is Null

		Insert Into #Lista
		Select 'Registros Repetidos' As Descripcion

		Select IdFuenteFinanciamiento, IdBeneficiario, Count(*) Repetidos
		into #010_Repetidos
		From FACT_Fuentes_De_Financiamiento__CargaMasiva
		Group By IdFuenteFinanciamiento, IdBeneficiario
		Having	 Count(*) > 1

		Select * From #Lista
		Select * From #010_Fuente
		Select * From #010_Repetidos

	End

	If (@Tipo = 11)--BeneficiariosRelacionados_Jurisdiccion
	Begin
		
		Insert Into #Lista
		Select 'Fuente Invalida' As Descripcion

		Select F.*
		into #011_Fuente
		From FACT_Fuentes_De_Financiamiento__CargaMasiva F
		left Join FACT_Fuentes_De_Financiamiento_Detalles D (NoLock) On (F.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento) 
		Where D.Descripcion Is Null

		Insert Into #Lista
		Select 'Registros Repetidos' As Descripcion

		Select IdFuenteFinanciamiento, IdBeneficiario, IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario_Relacionado, Count(*) Repetidos
		into #011_Repetidos
		From FACT_Fuentes_De_Financiamiento__CargaMasiva
		Group By IdFuenteFinanciamiento, IdBeneficiario, IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario_Relacionado
		Having	 Count(*) > 1

		Select * From #Lista
		Select * From #011_Fuente
		Select * From #011_Repetidos

	End


	If (@Tipo = 12)--BeneficiariosRelacionados_Jurisdiccion
	Begin
		
		Insert Into #Lista
		Select 'Fuente Invalida' As Descripcion

		Select F.*
		into #012_Fuente
		From FACT_Fuentes_De_Financiamiento__CargaMasiva F
		left Join FACT_Fuentes_De_Financiamiento_Detalles D (NoLock) On (F.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento) 
		Where D.Descripcion Is Null

		Insert Into #Lista
		Select 'Registros Repetidos' As Descripcion

		Select IdFuenteFinanciamiento, IdFinanciamiento, Referencia_01, IdGrupo, Count(*) Repetidos
		into #012_Repetidos
		From FACT_Fuentes_De_Financiamiento__CargaMasiva
		Group By IdFuenteFinanciamiento, IdFinanciamiento, Referencia_01, IdGrupo
		Having	 Count(*) > 1

		Select * From #Lista
		Select * From #012_Fuente
		Select * From #012_Repetidos

	End


End 
Go--#SQL

