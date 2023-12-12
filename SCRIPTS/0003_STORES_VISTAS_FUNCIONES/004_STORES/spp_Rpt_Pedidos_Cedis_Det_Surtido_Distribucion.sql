----------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_Rpt_Pedidos_Cedis_Det_Surtido_Distribucion' And xType = 'P' )
	Drop Proc spp_Rpt_Pedidos_Cedis_Det_Surtido_Distribucion
Go--#SQL

Create Procedure spp_Rpt_Pedidos_Cedis_Det_Surtido_Distribucion 
( 
	@IdEmpresa varchar(3) = '1', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '1005', @Folio Varchar(8) = '3'
) 
With Encryption 	
As 
Begin 
Set Dateformat YMD 
Declare @IdRotacion Varchar(3)


	Set @IdEmpresa = RIGHT('0000000000' + @IdEmpresa, 3) 
	Set @IdEstado = RIGHT('0000000000' + @IdEstado, 2) 
	Set @IdFarmacia = RIGHT('0000000000' + @IdFarmacia, 4) 
	Set @Folio = RIGHT('0000000000' + @Folio, 8) 



	--Select @IdRotacion = Max(IdRotacion) From CFGC_ALMN__Rotacion Where Status = 'A'  And IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia
	Set @IdRotacion = '0'
	Set @IdRotacion = Right('000' + @IdRotacion, 3)


	Select *, @IdRotacion As IdRotacion
	Into #vw_Pedidos_Cedis_Det_Surtido_Distribucion
	From vw_Pedidos_Cedis_Det_Surtido_Distribucion D (NoLock)
	Where D.IdEmpresa = @IdEmpresa And D.IdEstado = @IdEstado And D.IdFarmacia = @IdFarmacia And FolioSurtido = @Folio


	Select IdEmpresa, IdEstado, IdFarmacia, Max(IdRotacion) As IdRotacion, IdClaveSSA
	Into #CFGC_ALMN__Rotacion_Claves
	From CFGC_ALMN__Rotacion_Claves
	Where Status = 'A'  And IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia
	Group BY IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA

	UPdate D Set IdRotacion = IsNull(C.IdRotacion, D.IdRotacion)
	From #vw_Pedidos_Cedis_Det_Surtido_Distribucion D
	Left Join #CFGC_ALMN__Rotacion_Claves C (NoLock)
		On (  D.IdEmpresa = C.IdEmpresa And D.IdEstado = C.IdEstado And D.IdFarmacia = C.IdFarmacia And D.IdClaveSSA_Sal = C.IdClaveSSA)
	--Where C.IdEmpresa Is Null


	Alter Table #vw_Pedidos_Cedis_Det_Surtido_Distribucion Alter Column FarmaciaPedido Varchar(800)
	Alter Table #vw_Pedidos_Cedis_Det_Surtido_Distribucion Alter Column FarmaciaSolicita Varchar(800)
		

	------------------------------- ASIGNAR LA JURISDICCÓN A LA QUE PERTENECE EL BENEFICIARIO ( VENTAS DIRECTAS ) 
	Update P Set -- IdJurisdiccion = B.IdJurisdiccion, 
		FarmaciaPedido = B.IdBeneficiario + '.....' + (B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre),  -- B.NombreCompleto  		
		FarmaciaSolicita = B.IdBeneficiario + '.....' + (B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre) -- B.NombreCompleto  
	From #vw_Pedidos_Cedis_Det_Surtido_Distribucion P (NoLock) 
	Inner Join CatBeneficiarios B (NoLock) 
		On ( P.IdEstado = B.IdEstado And P.IdFarmacia = B.IdFarmacia and P.IdCliente = B.IdCliente and P.IdSubCliente = B.IdSubCliente  and P.IdBeneficiario = B.IdBeneficiario ) 
	where P.EsTransferencia = 0 


	------------------------------- ASIGNAR INFORMACIÓN DE LA FARMACIA QUE SOLICITA EL PEDIDO  
	Update P Set 
		-- FarmaciaPedido = B.IdBeneficiario + '.....' + (B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre),  -- B.NombreCompleto 
		IdFarmaciaPedido = B.IdFarmacia,  
		FarmaciaPedido = B.Farmacia, 
		FarmaciaSolicita = B.Farmacia -- B.NombreCompleto  
	From #vw_Pedidos_Cedis_Det_Surtido_Distribucion P (NoLock) 
	Inner Join vw_Farmacias B (NoLock) 
		On ( P.IdEstadoSolicita = B.IdEstado And P.IdFarmaciaSolicita = B.IdFarmacia ) 
	where P.EsTransferencia = 1 

--			spp_Rpt_Pedidos_Cedis_Det_Surtido_Distribucion  

---------------------------------------------	SALIDA FINAL 
	Select P.*, 'GENERICO' As NombreRotacion--R.NombreRotacion  
	From #vw_Pedidos_Cedis_Det_Surtido_Distribucion P
	--Inner Join CFGC_ALMN__Rotacion R (NoLock) On (P.IdEmpresa = R.IdEmpresa And P.IdEstado = R.IdEstado And P.IdFarmacia = R.IdFarmacia And P.IdRotacion = R.IdRotacion)
	Order By P.IdRotacion, IdOrden, IdPasillo, IdEstante, IdEntrepaño


End
Go--#SQL


--Select * From CFGC_ALMN__Rotacion