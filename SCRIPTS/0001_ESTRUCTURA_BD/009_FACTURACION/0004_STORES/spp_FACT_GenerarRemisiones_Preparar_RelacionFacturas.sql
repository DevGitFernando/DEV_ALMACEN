---------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_GenerarRemisiones_Preparar_RelacionFacturas' and xType = 'P' )
    Drop Proc spp_FACT_GenerarRemisiones_Preparar_RelacionFacturas  
Go--#SQL 
 
/*

			--------------- ASEGURAR QUE LOS DATOS DE LA FACTURA SE ENCUENTREN EN LA TABLA DE CONTROL 
			Exec spp_FACT_GenerarRemisiones_Preparar_RelacionFacturas 
				@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, 

				@IdFuenteFinanciamiento_FacturaRelacionada = @IdFuenteFinanciamiento_FacturaRelacionada, 
				@IdFinanciamiento_FacturaRelacionada = @IdFinanciamiento_FacturaRelacionada, 

				@EsRelacionFacturaPrevia = @EsRelacionFacturaPrevia, 
				@FacturaPreviaEnCajas = @FacturaPreviaEnCajas, 
				@Serie = @Serie, @Folio = @Folio, 
				@EsRelacionMontos = @EsRelacionMontos 

*/ 

Create Proc spp_FACT_GenerarRemisiones_Preparar_RelacionFacturas 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '28', 
	@IdFarmaciaGenera varchar(4) = '0001', 

	@IdFuenteFinanciamiento_FacturaRelacionada varchar(4) = '', 
	@IdFinanciamiento_FacturaRelacionada varchar(4) = '', 

	@EsRelacionFacturaPrevia bit = 1, 
	@FacturaPreviaEnCajas int = 0, 
	@Serie varchar(10) = '', @Folio int = 0, 
	@EsRelacionMontos bit = 0 	 
) 
With Encryption		
As 
Begin 
Set NoCount On 

/* 
	----------------------------------	Validar el tipo de proceso de remisionado, Ventas por Anticipado 
	If @EsRelacionFacturaPrevia = 1  
	Begin 

		If @EsRelacionMontos = 1 
		Begin 

			Insert Into FACT_Remisiones__RelacionFacturas_x_Importes (  IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, FechaRegistro, Importe_Facturado, Importe_Distribuido ) 
			Select D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.Serie, D.Folio, getdate() as FechaRegistro, sum(Total) as Importe_Facturado, 0 as Importe_Distribuido 
			From FACT_CFD_Documentos_Generados_Detalles D (NoLock) 
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and Serie = @Serie and Folio = @Folio
				and Not Exists 
				( 
					Select * 
					From FACT_Remisiones__RelacionFacturas_x_Importes E (NoLock) 
					Where D.IdEmpresa = E.IdEmpresa and D.IdEstado = E.IdEstado and D.IdFarmacia = E.IdFarmacia and D.Serie = E.Serie and D.Folio = E.Folio 
				) 
			Group by  D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.Serie, D.Folio 
		End 

		----------------------------------- Leer configuracion en base a facturas-claves 
		If @EsRelacionMontos = 0  
		Begin 


			Insert Into FACT_Remisiones__RelacionFacturas 
			(	IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, IdFuenteFinanciamiento, IdFinanciamiento, 
				FechaRegistro, ClaveSSA, ContenidoPaquete, 
				Cantidad_Facturada, Cantidad_Distribuida, 
				CantidadAgrupada_Facturada, CantidadAgrupada_Distribuida ) 
			Select 
				D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.Serie, D.Folio, 
				@IdFuenteFinanciamiento_FacturaRelacionada as IdFuenteFinanciamiento, @IdFinanciamiento_FacturaRelacionada as IdFinanciamiento, 
				getdate() as FechaRegistro, 
				D.Clave as ClaveSSA, 
				max(C.ContenidoPaquete) as ContenidoPaquete,  
				(case when @FacturaPreviaEnCajas = 1 Then sum(Cantidad * C.ContenidoPaquete) else sum(Cantidad) end)as Cantidad_Facturada, 
				0 as Cantidad_Distribuida, 
				sum(Cantidad) as CantidadAgrupada_Facturada, 
				0 as CantidadAgrupada_Distribuida 
			From FACT_CFD_Documentos_Generados_Detalles D (NoLock) 
			Inner Join vw_ClavesSSA_Sales C (NoLock) On ( D.Clave = C.ClaveSSA ) 
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and Serie = @Serie and Folio = @Folio 
				and @IdFuenteFinanciamiento_FacturaRelacionada <> '' and @IdFinanciamiento_FacturaRelacionada <> '' 
				and Not Exists 
				( 
					Select * 
					From FACT_Remisiones__RelacionFacturas R (NoLock) 
					Where D.IdEmpresa = R.IdEmpresa and D.IdEstado = R.IdEstado and D.IdFarmacia = R.IdFarmacia 
						and D.Serie = R.Serie and D.Folio = R.Folio and D.Clave = R.ClaveSSA 
				)  
			Group by IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, Clave


			--select * from FACT_Remisiones__RelacionFacturas where Serie = @serie and folio = @folio 

		End 

	End 
	----------------------------------	Validar el tipo de proceso de remisionado, Ventas por Anticipado 

*/ 

End 
Go--#SQL 
