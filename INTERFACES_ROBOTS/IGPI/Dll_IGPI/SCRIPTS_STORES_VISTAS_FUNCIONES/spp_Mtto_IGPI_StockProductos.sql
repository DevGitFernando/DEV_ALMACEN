If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_IGPI_StockProductos' and xType = 'P' ) 
   Drop Proc spp_Mtto_IGPI_StockProductos 
Go--#SQL   

Create Proc spp_Mtto_IGPI_StockProductos  
( 
	@CodigoEAN varchar(30), @Cantidad int, @Status varchar(1), @Tipo smallint = 1, @Proceso varchar(2) = ''  
)
With Encryption 
As 
Begin 
Set NoCount On 
Declare @iEfecto int  

	Set @iEfecto = 1 
	If @Tipo = 2 
	   Set @iEfecto = -1 
	   
	
	   
	Set @Cantidad = @Cantidad * @iEfecto 
	
	--Insert into IGPI_Log ( Mensaje ) 	Select cast(@Proceso as varchar) 

	
	--Set @CodigoEAN = '' 
	Select @CodigoEAN = CodigoEAN 
	From vw_Productos_CodigoEAN (NoLock) 
	Where right('00000000000000000000' + CodigoEAN, 20) =  right('00000000000000000000' + ltrim(rtrim(@CodigoEAN)), 20)	   
	Set @CodigoEAN = IsNull(@CodigoEAN, '')    
	   
	If @CodigoEAN <> '' 
	Begin    
		If Not Exists ( Select * From IGPI_StockProductos (NoLock) Where CodigoEAN = @CodigoEAN ) 
		   Begin 
			  Insert Into IGPI_StockProductos ( CodigoEAN, Existencia, ExistenciaIGPI, Status, Actualizado ) 
			  Select @CodigoEAN, 0, @Cantidad, 'A', 1  
		   End 
		Else
		   Begin 
				Update S Set ExistenciaIGPI = 
				(
					case when @Proceso = 'b' then @Cantidad 
						else 
						(case when @Proceso in ( 'a', 'i' ) then S.ExistenciaIGPI + @Cantidad else S.ExistenciaIGPI end) 
					end 
				) 
				From IGPI_StockProductos S (NoLock) 
				Where CodigoEAN = @CodigoEAN 
		   End 
	End 

End 
Go--#SQL   
  
