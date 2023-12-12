If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_INT_MA_Ventas_Importes' and xType = 'P' ) 
   Drop Proc spp_Mtto_INT_MA_Ventas_Importes
Go--#SQL

Create Proc spp_Mtto_INT_MA_Ventas_Importes
(
	 @IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', 
	 @Folio varchar(8) = '', 
	 @SubTotal_SinGravar numeric(14, 4) = 0, @SubTotal_Gravado numeric(14, 4) = 0, 
	 @DescuentoCopago numeric(14, 4) = 0,  
	 
	 @Importe_SinGravar numeric(14, 4) = 0, @Importe_Gravado numeric(14, 4) = 0, 	
	 @Iva numeric(14, 4) = 0, @Importe_Neto numeric(14, 4) = 0 	  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare @sStatus varchar(1), @iActualizado smallint 
		
	Set @sStatus = 'A'
	Set @iActualizado = 0

	----Set @Importe_SinGravar = @Importe_SinGravar - @DescuentoCopago 
	----Set @Importe_Neto = @Importe_Neto - @DescuentoCopago 

	Insert Into INT_MA_Ventas_Importes 
	( 
		IdEmpresa, IdEstado, IdFarmacia, FolioVenta, SubTotal_SinGravar, SubTotal_Gravado, DescuentoCopago, 
		Importe_SinGravar, Importe_Gravado, Iva, Importe_Neto 
	) 
	Select 
		@IdEmpresa, @IdEstado, @IdFarmacia, @Folio, @SubTotal_SinGravar, @SubTotal_Gravado, @DescuentoCopago, 
		@Importe_SinGravar, @Importe_Gravado, @Iva, @Importe_Neto

     
End 
Go--#SQL
