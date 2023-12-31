If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__005__BeneficiariosAtendidos' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__005__BeneficiariosAtendidos 
Go--#SQL 

Create Proc spp_BI_RPT__005__BeneficiariosAtendidos 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '88', @FechaInicial varchar(10) = '2015-01-01', @FechaFinal varchar(10) = '2015-01-31'	
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max) 

	Set @sSql = '' 


----------------------------------------------------- DATOS FILTRO 
	Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia 
	Into #vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into #vw_Farmacias ( IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia ) ' + char(13) + char(10) + 
				'Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia ' + char(13) + char(10) + 
				'From vw_Farmacias ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	If @IdMunicipio <> '' and @IdMunicipio <> '*' 
	   Set @sSql = @sSql + ' and IdMunicipio = ' + char(39) + right('0000' + @IdMunicipio, 4) + char(39) + char(13) + char(10)

	If @IdJurisdiccion <> '' and @IdJurisdiccion <> '*' 
	   Set @sSql = @sSql + ' and IdJurisdiccion = ' + char(39) + right('0000' + @IdJurisdiccion, 3) + char(39) + char(13) + char(10)
	   
	If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  


----------------------------------------------------- OBTENCION DE DATOS  
	Select 
		E.IdEstado, E.IdFarmacia, 
		E.FolioVenta, 
		-- E.FechaRegistro, 
		convert(varchar(10), E.FechaRegistro, 120) as FechaRegistro, 
		E.IdCliente, E.IdSubCliente, 
		I.IdBeneficiario, 
		cast('' as varchar(500)) as Beneficiario, 
		cast('' as varchar(100)) as Referencia  
	Into #tmp_Beneficiarios 
	From VentasEnc E (NoLock) 
	Inner Join #vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	Inner Join VentasInformacionAdicional I (NoLock) 
		On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmacia and E.FolioVenta = I.FolioVenta ) 
	Where convert(varchar(10), FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		E.IdEmpresa = @IdEmpresa 
		----and 
		----Exists 
		----( 
		----	Select * 
		----	From #vw_Farmacias F 
		----	Where E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia 
		----) 
	Order By FechaRegistro 


	Update E Set Referencia = B.FolioReferencia, 
		Beneficiario = ( B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre ) 
	From #tmp_Beneficiarios E 
	Inner Join CatBeneficiarios B (NoLock) 
		On ( E.IdEstado = B.IdEstado and E.IdFarmacia = B.IdFarmacia and E.IdCliente = B.IdCliente and E.IdSubCliente = B.IdSubCliente
			and E.IdBeneficiario = B.IdBeneficiario ) 


---------------------		spp_BI_RPT__005__BeneficiariosAtendidos


----------------------------------------------------- SALIDA FINAL 
	Select 
		-- IdFarmacia, 
		'Fecha de Atenci�n' = FechaRegistro, 
		'Nombre de beneficiario' = Beneficiario, 
		'N�mero de poliza' = Referencia     
	From #tmp_Beneficiarios 
	Group by -- IdFarmacia, 
		FechaRegistro, Beneficiario, Referencia 
	Order By   
		FechaRegistro, Beneficiario, Referencia  



End 
Go--#SQL 


