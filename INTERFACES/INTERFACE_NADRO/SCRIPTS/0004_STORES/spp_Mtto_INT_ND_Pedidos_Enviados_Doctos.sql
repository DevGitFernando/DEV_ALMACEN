------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_INT_ND_Pedidos_Enviados_Doctos' and xType = 'P')
    Drop Proc spp_Mtto_INT_ND_Pedidos_Enviados_Doctos
Go--#SQL

---		Select * From INT_ND_Pedidos_Enviados_Doctos

/*
		Exec spp_Mtto_INT_ND_Pedidos_Enviados_Doctos ( @IdEmpresa = '', @IdEstado = '', @IdFarmacia = '', @FolioPedido = '', 
			@IdPersonal = '', @Observaciones = '', @NombreDocumento = '', @ContenidoDocumento = '', @SegmentoDocumento = ''
			
*/
  
Create Proc spp_Mtto_INT_ND_Pedidos_Enviados_Doctos ( @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @FolioPedido varchar(8), 
			@IdPersonal varchar(4), @Observaciones varchar(200), @NombreDocumento varchar(200), @ContenidoDocumento text, @SegmentoDocumento varchar(100) )
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000)  

	

	Set @sMensaje = ''
	
	If Not Exists ( Select * From INT_ND_Pedidos_Enviados_Doctos (NoLock) Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado
					and IdFarmacia = @IdFarmacia and FolioPedido = @FolioPedido ) 
	  Begin 
		 Insert Into INT_ND_Pedidos_Enviados_Doctos ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, IdPersonal, Observaciones, 
													NombreDocumento, ContenidoDocumento, SegmentoDocumento ) 
		 Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioPedido, @IdPersonal, @Observaciones, 
		 @NombreDocumento, @ContenidoDocumento, @SegmentoDocumento 
	  End 

	Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioPedido 
	   

	-- Regresar la Clave Generada
	Select @FolioPedido as Folio, @sMensaje as Mensaje 
End
Go--#SQL
