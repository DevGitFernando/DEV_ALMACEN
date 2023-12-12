


If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatProveedores_DiasDePlazo' And xType = 'P' )
	Drop Proc spp_Mtto_CatProveedores_DiasDePlazo
Go--#SQL

Create Procedure spp_Mtto_CatProveedores_DiasDePlazo
( 
	@IdProveedor varchar(4) = '0025', @Dias int = 60, @Status bit = 1, @Predeterminado bit = 1
)
With Encryption 
As
Begin

	If Not Exists( Select * From CatProveedores_DiasDePlazo (NoLock) Where IdProveedor = @IdProveedor And Dias = @Dias  )
		Begin
			Insert Into CatProveedores_DiasDePlazo ( IdProveedor, Dias, Status, Predeterminado )
			Select @IdProveedor, @Dias, @Status, @Predeterminado
		End
	Else
		Begin
			Update CatProveedores_DiasDePlazo
			Set Status = @Status, Predeterminado = @Predeterminado
			Where IdProveedor = @IdProveedor And Dias = @Dias
		End

	-- Regresar la Clave Generada
    Select @IdProveedor as IdProveedor

End
Go--#SQL
