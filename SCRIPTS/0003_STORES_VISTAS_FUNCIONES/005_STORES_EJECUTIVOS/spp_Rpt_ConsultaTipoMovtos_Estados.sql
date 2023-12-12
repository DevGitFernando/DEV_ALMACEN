

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_ConsultaTipoMovtos_Estados' and xType = 'P' ) 
   Drop Proc spp_Rpt_ConsultaTipoMovtos_Estados 
Go--#SQL 

Create Proc spp_Rpt_ConsultaTipoMovtos_Estados
( 
	@IdEstado varchar(2) = '21', @Fecha varchar(10) = '2011-09-05'   
) 
With Encryption
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

Declare 
	@DiasMes int, 
	@Año int,  
	@Mes int   


	Select @Año = datepart(yy, @Fecha) 
	Select @Mes = datepart(mm, @Fecha) 

--	Select * From Rpt_Claves_TipoMovimientos_Estados (Nolock)

	Select 'Id SubFarmacia' = IdSubFarmacia, 'SubFarmacia' = SubFarmacia, 'Tipo Movimiento' = IdTipoMovto,
	'Descripción Movimiento' = DescripcionTipoMovto, 'Año' = Año, 'Núm. Mes' = Mes,
	'Mes' = NombreMes, 'Piezas' = Piezas, 'Claves' = TotalClaves 
	From Rpt_Claves_TipoMovimientos_Estados (Nolock)
	Where IdEstado = @IdEstado And Año = @Año And Mes = @Mes
	Order By IdSubFarmacia, Año, Mes, Efecto_Movto

	
End 
Go--#SQL 
   
