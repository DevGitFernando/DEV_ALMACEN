If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_Ctl_CierresDePeriodos' and xType = 'P' ) 
    Drop Proc spp_Mtto_Ctl_CierresDePeriodos 
Go--#SQL

---     Exec 

Create Proc spp_Mtto_Ctl_CierresDePeriodos 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0188', 
	@FolioCierre varchar(8) = '', 
	@IdPersonal varchar(4) = '0001', @FechaCierre varchar(10) = '2011-11-15',
	@IdEstadoRegistra varchar(2) = '21', @IdFarmaciaRegistra varchar(4) = '0001',  @IdPersonalRegistra varchar(4) = '0001', 
	@EsVistaPrevia bit = 0  
)
with Encryption 
As 
Begin 
--Set NoCount On 


Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint,
		@FechaInicial datetime, @FechaFinal datetime  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set DateFormat YMD	

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0
	Set @FechaInicial = getdate()
	Set @FechaFinal = getdate()

    
    -- Set @FolioCierre = '1' 
    -- If @EsVistaPrevia = 0  -- Jesus Diaz 2K111220.1741 		 
	Begin 
	    Select @FolioCierre = cast( (max(FolioCierre) + 1) as varchar)  From Ctl_CierresDePeriodos (NoLock)
	    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
    End 

	-- Asegurar que FolioCierre sea valido 
	Set @FolioCierre = IsNull(@FolioCierre, '1')
	Set @FolioCierre = right('00000000000000' + @FolioCierre, 8)  	 


    If @EsVistaPrevia = 1 
    Begin 
        Delete From Ctl_CierresDePeriodos_VP 
        Delete From Ctl_CierresPeriodosDetalles_VP         
    End 

---     select @FolioCierre as Folio  

	If Not Exists ( Select * From Ctl_CierresDePeriodos (NoLock) 
				    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioCierre = @FolioCierre ) 
		Begin 
			
			------------------------------------------------------ 		
			Update VentasEnc Set FolioCierre = @FolioCierre			
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioCierre = 0 
			    And Convert( varchar(10), FechaRegistro, 120) <= @FechaCierre

			Update Vales_EmisionEnc Set FolioCierre = @FolioCierre			
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioCierre = 0 
			    And Convert( varchar(10), FechaRegistro, 120) <= @FechaCierre

			Update ValesEnc Set FolioCierre = @FolioCierre			
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioCierre = 0 
			    And Convert( varchar(10), FechaRegistro, 120) <= @FechaCierre

			Select @FechaInicial = Min(FechaRegistro), @FechaFinal = Max(FechaRegistro) 
			From VentasEnc (Nolock)
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioCierre = @FolioCierre 
				
				
		    If @EsVistaPrevia = 1  -- Jesus Diaz 2K111220.1741 
		    Begin 
			    Insert Into Ctl_CierresDePeriodos_VP 
			        (   
			            IdEmpresa, IdEstado, IdFarmacia, FolioCierre, IdPersonal, IdEstadoRegistra, IdFarmaciaRegistra, 
			            IdPersonalRegistra, FechaRegistro, FechaCorte, FechaInicial, FechaFinal, Status, Actualizado 
			        ) 
			    Select 
			        @IdEmpresa, @IdEstado, @IdFarmacia, @FolioCierre, @IdPersonal, @IdEstadoRegistra, @IdFarmaciaRegistra, @IdPersonalRegistra,
			        getdate(), @FechaCierre, getdate(), getdate(), @sStatus, @iActualizado  
			        
			    Update Ctl_CierresDePeriodos_VP Set FechaInicial = @FechaInicial, FechaFinal = @FechaFinal 
			    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioCierre = @FolioCierre	
			End 	
			Else 
		    Begin 
			    Insert Into Ctl_CierresDePeriodos 
			        (   
			            IdEmpresa, IdEstado, IdFarmacia, FolioCierre, IdPersonal, IdEstadoRegistra, IdFarmaciaRegistra, 
			            IdPersonalRegistra, FechaRegistro, FechaCorte, FechaInicial, FechaFinal, Status, Actualizado, FechaControl 
			        ) 
			    Select 
			        @IdEmpresa, @IdEstado, @IdFarmacia, @FolioCierre, @IdPersonal, @IdEstadoRegistra, @IdFarmaciaRegistra, @IdPersonalRegistra,
			        getdate(), @FechaCierre, getdate(), getdate(), @sStatus, @iActualizado, GETDATE() 
			        
			    Update Ctl_CierresDePeriodos Set FechaInicial = @FechaInicial, FechaFinal = @FechaFinal 
			    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioCierre = @FolioCierre	
			End							
				
			-- Set @sMensaje = 'La información del Folio se Genero Satisfactoriamente ' 
			Set @sMensaje = 'El cierre se generó satisfactoriamente con el folio << ' + @FolioCierre + ' >> ' 
		End		 

    If @EsVistaPrevia = 1 
    Begin 
        Set @sMensaje = 'El Pre-cierre se generó satisfactoriamente.'     
	    --- Generar el reporte de Vista Previa 
	    Exec spp_Rpt_CierresPeriodosFacturacion @IdEstado, @IdFarmacia, @FolioCierre, @EsVistaPrevia 
	    
	        
        Update VentasEnc Set FolioCierre = 0			
        Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioCierre = @FolioCierre 
            And Convert( varchar(10), FechaRegistro, 120) <= @FechaCierre

        Update Vales_EmisionEnc Set FolioCierre = 0			
        Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioCierre = @FolioCierre 
            And Convert( varchar(10), FechaRegistro, 120) <= @FechaCierre

        Update ValesEnc Set FolioCierre = 0			
        Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioCierre = @FolioCierre 
            And Convert( varchar(10), FechaRegistro, 120) <= @FechaCierre 	   
    End  
    Else 
    Begin 	
	    Exec spp_Rpt_CierresPeriodosFacturacion @IdEstado, @IdFarmacia, @FolioCierre 
    End 



    -- Regresar el Folio Generado
    Select @FolioCierre as Folio, @sMensaje as Mensaje 
	
	
End 
Go--#SQL
   
