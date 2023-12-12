



If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CFGC_Titulos_CartaCanje' and xType = 'P')
    Drop Proc spp_Mtto_CFGC_Titulos_CartaCanje
Go--#SQL
  
Create Proc spp_Mtto_CFGC_Titulos_CartaCanje 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @ExpedidoEn varchar(100), @AQuienCorresponda varchar(100),
	@MesesCaducar int, @Titulo_01 varchar(500), @Titulo_02 varchar(500), @Titulo_03 varchar(500), @Firma varchar(500) 
)
With Encryption 
As
Begin 
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint  

	
	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0
	
	-------	Select * From CFGC_Titulos_CartaCanje	
			
   If Not Exists (  Select * From CFGC_Titulos_CartaCanje (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia ) 
	  Begin 
		 Insert Into CFGC_Titulos_CartaCanje ( IdEmpresa, IdEstado, IdFarmacia, ExpedidoEn, AQuienCorresponda, MesesCaducar, 
										Titulo_01, Titulo_02, Titulo_03, Firma, Actualizado ) 
		 Select @IdEmpresa, @IdEstado, @IdFarmacia, @ExpedidoEn, @AQuienCorresponda, @MesesCaducar, 
										@Titulo_01, @Titulo_02, @Titulo_03, @Firma, @iActualizado 
      End 
   Else 
	  Begin 
	     Update CFGC_Titulos_CartaCanje 
		 Set ExpedidoEn = @ExpedidoEn, AQuienCorresponda = @AQuienCorresponda, MesesCaducar = @MesesCaducar, 
			 Titulo_01 = @Titulo_01, Titulo_02 = @Titulo_02, Titulo_03 = @Titulo_03, Firma = @Firma, Actualizado = @iActualizado
		 Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia
      End 
   Set @sMensaje = 'La información se guardo satisfactoriamente '  
	  
   
	-- Regresar la Clave Generada
    Select @sMensaje as Mensaje 
End
Go--#SQL

