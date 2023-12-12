If Exists ( Select Name From sysobjects (NoLock) Where Name = 'spp_Mtto_LCTCN_Cotizaciones' and xType = 'P' ) 
   Drop Proc spp_Mtto_LCTCN_Cotizaciones 
Go--#SQL    

--	CatClavesSSA_SeguroPopular_Anual



Create Proc spp_Mtto_LCTCN_Cotizaciones 
( 
	@FolioCotizacion varchar(8) = '*', @IdEmpresa varchar(3) = '001', @NombreCliente varchar(100) = '', @Licitacion varchar(200) = '',
	@SubTotalSinGrabar_Min numeric(14,4) = 0, @SubTotalSinGrabar_Max numeric(14,4) = 0, 
	@SubTotalGrabado_Min numeric(14,4) = 0, @SubTotalGrabado_Max numeric(14,4) = 0, @Iva_Min numeric(14,4) = 0, @Iva_Max numeric(14,4) = 0, 
	@Total_Min numeric(14,4) = 0, @Total_Max numeric(14,4) = 0, 
	@Tipo smallint = 1, @Observaciones varchar(500), @Opcion tinyint = 0 
) 
With Encryption
As 
Begin 
Set NoCount On 
Declare @sMsj varchar(200), @Status varchar(1)

	Set @Status = 'A'

	If @FolioCotizacion = '*'
	   Select @FolioCotizacion = max(cast(FolioCotizacion as int)) + 1 From LCTCN_Cotizaciones (NoLock) 	
	
	Set @sMsj = '' 
	Set @FolioCotizacion = IsNull(@FolioCotizacion, '1') 
	Set @FolioCotizacion = right(replicate('0', 8) + @FolioCotizacion, 8) 
	
	If @Opcion = 1
		Begin
			
			Delete From LCTCN_Cotizaciones_Claves Where FolioCotizacion = @FolioCotizacion

			If Not Exists ( Select * From LCTCN_Cotizaciones (NoLock) Where FolioCotizacion = @FolioCotizacion ) 
				Begin 
					Insert Into LCTCN_Cotizaciones 
						( 
							FolioCotizacion, IdEmpresa, NombreCliente, Licitacion, SubTotalSinGrabar_Min, SubTotalSinGrabar_Max, SubTotalGrabado_Min, SubTotalGrabado_Max, 
							Iva_Min, Iva_Max, Total_Min, Total_Max, Tipo, Observaciones, FechaRegistro, Status, Actualizado ) 
					Select  @FolioCotizacion, @IdEmpresa, @NombreCliente, @Licitacion, @SubTotalSinGrabar_Min, @SubTotalSinGrabar_Max, @SubTotalGrabado_Min, @SubTotalGrabado_Max, 
					@Iva_Min, @Iva_Max, @Total_Min, @Total_Max, @Tipo, @Observaciones, getdate() as FechaRegistro, @Status, 0 as Actualizado 
					Set @sMsj = 'Cotización generada satisfactoriamente con el folio  [ ' + @FolioCotizacion + ' ]' 
				End 
			Else 
				Begin 
					Update C Set  
						Observaciones = @Observaciones, 
						SubTotalSinGrabar_Min = @SubTotalSinGrabar_Min, SubTotalSinGrabar_Max = @SubTotalSinGrabar_Max, 
						SubTotalGrabado_Min = @SubTotalGrabado_Min, SubTotalGrabado_Max = @SubTotalGrabado_Max, Iva_Min = @Iva_Min, Iva_Max = @Iva_Max, 
						Total_Min = @Total_Min, Total_Max = @Total_Max, Status = @Status 
					From LCTCN_Cotizaciones C (NoLock) 
					Where FolioCotizacion = @FolioCotizacion 
					
					Set @sMsj = 'Cotización con el folio [ ' + @FolioCotizacion + ' ] actualizada satisfactoriamente. ' 			
					If @Status = 'T'
						Set @sMsj = 'Cotización con el folio [ ' + @FolioCotizacion + ' ] terminada satisfactoriamente. ' 			
								
				End  
		End
	If @Opcion = 2 --- Cancelar Cotizacion
		Begin
			Set @Status = 'C'
			Update LCTCN_Cotizaciones Set Status = @Status Where FolioCotizacion = @FolioCotizacion
			Set @sMsj = 'Cotización Cancelada satisfactoriamente con el folio  [ ' + @FolioCotizacion + ' ]'
		End

	If @Opcion = 3 --- Bloquear Cotizacion
		Begin
			Set @Status = 'B'
			Update LCTCN_Cotizaciones Set Status = @Status Where FolioCotizacion = @FolioCotizacion
			Set @sMsj = 'Cotización Bloqueada satisfactoriamente con el folio  [ ' + @FolioCotizacion + ' ]'
		End


	Select @FolioCotizacion as Folio, @sMsj as Mensaje 

--	sp_listacolumnas LCTCN_Cotizaciones

End 
Go--#SQL 

-------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From sysobjects (NoLock) Where Name = 'spp_Mtto_LCTCN_Cotizaciones_Claves' and xType = 'P' ) 
   Drop Proc spp_Mtto_LCTCN_Cotizaciones_Claves  
Go--#SQL  

Create Proc spp_Mtto_LCTCN_Cotizaciones_Claves 
( 
	@FolioCotizacion varchar(8) = null, @IdClaveSSA varchar(4) = '0001', @Partida int = 1, 
	@CantidadMinima numeric(14,4) = 0, @CantidadMaxima numeric(14,4) = 0, 
	@TipoManejo smallint = 1, @ContenidoPaquete numeric(14,4) = 0, @CostoCompra numeric(14, 4) = 0, 
	@PrecioPaquete numeric(14,4) = 0, @PrecioPieza numeric(14,4) = 0, 
	@Porcentaje numeric(14,4) = 0, @EsCause smallint = 0, @Admon smallint = 0, @Status varchar(1) = 'A'  
) 
With Encryption
As 
Begin 
Set NoCount On 

--- Asegurar que no exista el Folio-Clave 	
	Delete From LCTCN_Cotizaciones_Claves Where FolioCotizacion = @FolioCotizacion and IdClaveSSA = @IdClaveSSA and Partida = @Partida 

---	Agregar la información requerida 
	Insert Into LCTCN_Cotizaciones_Claves 
		( FolioCotizacion, IdClaveSSA, Partida, CantidadMinima, CantidadMaxima, TipoManejo, 
		  ContenidoPaquete, CostoCompra, PrecioPaquete, PrecioPieza, EsCause, Admon, Status, Actualizado ) 
	Select @FolioCotizacion, @IdClaveSSA, @Partida, @CantidadMinima, @CantidadMaxima, @TipoManejo, 
		  @ContenidoPaquete, @CostoCompra, @PrecioPaquete, @PrecioPieza, @EsCause, @Admon, @Status, 0 as Actualizado

End 
Go--#SQL 

-- sp_listacolumnas LCTCN_Cotizaciones_Claves 
