If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Ventas_Vales' and xType = 'P' )
    Drop Proc spp_Mtto_Ventas_Vales
Go--#SQL
  
Create Proc spp_Mtto_Ventas_Vales 
(	@IdEmpresa varchar(3) = '001', @IdEstado varchar(4) = '25', @IdFarmacia varchar(6) = '0002', 
	@FolioVale varchar(32) = '*', @FolioVenta varchar(32) = '00000006', @FechaCanje varchar(10) = '', 
	@IdPersonal varchar(6) = '0009', @iOpcion smallint = '1'
 )
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint
		

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0	
	
	If @FolioVale = '*' 
	  Begin
		Select @FolioVale = cast( (max(FolioVale) + 1) as varchar)  From Ventas_ValesEnc (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	  End

	-- Asegurar que FolioVale sea valido y formatear la cadena 
	Set @FolioVale = IsNull(@FolioVale, '1')
	Set @FolioVale = right(replicate('0', 8) + @FolioVale, 8) 

	-- Se crea la tabla de los detalles del Vale
	Select Top 0 IdEmpresa, IdEstado, IdFarmacia, @FolioVale as FolioVale, IdClaveSSA, CantidadRequerida as Cantidad
	Into #tmpClaves
	From VentasEstadisticaClavesDispensadas(NoLock) 

	-- Se agrega la columna Renglon
	Alter Table #tmpClaves Add Renglon int identity(1,1)   

	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From Ventas_ValesEnc (NoLock) 
						   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVale = @FolioVale ) 
			  Begin 

				-- Se inserta el Encabezado.
				Insert Into Ventas_ValesEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVale, FolioVenta, FechaCanje, IdPersonal, Status, Actualizado ) 
				Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioVale, @FolioVenta, @FechaCanje, @IdPersonal, @sStatus, @iActualizado 			
				
				-- Se inserta el Detalle
				Insert Into Ventas_ValesDet (  IdEmpresa, IdEstado, IdFarmacia, FolioVale, IdClaveSSA_Sal, Cantidad, Status, Actualizado ) 
				Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioVale, IdClaveSSA, ( CantidadRequerida - CantidadEntregada ) as Cantidad, @sStatus, @iActualizado  
                From VentasEstadisticaClavesDispensadas(NoLock) 
                Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta 
                And CantidadRequerida <> CantidadEntregada	
				Order By IdClaveSSA

              End 
		   Else 
			  Begin 
				Set @sStatus = 'R'

				Update Ventas_ValesEnc Set
					FechaCanje = @FechaCanje, Status = @sStatus, Actualizado = @iActualizado
				Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVale = @FolioVale 

              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Folio ' + @FolioVale 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update Ventas_ValesEnc Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVale = @FolioVale
 
		   Set @sMensaje = 'La información del Vale ' + @FolioVale + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioVale as Clave, @sMensaje as Mensaje 
End
Go--#SQL	
