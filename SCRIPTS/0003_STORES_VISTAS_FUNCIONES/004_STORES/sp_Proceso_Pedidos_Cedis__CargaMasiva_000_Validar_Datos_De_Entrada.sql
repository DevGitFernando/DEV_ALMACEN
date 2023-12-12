-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'sp_Proceso_Pedidos_Cedis__CargaMasiva_000_Validar_Datos_De_Entrada' and xType = 'P' ) 
   Drop Proc sp_Proceso_Pedidos_Cedis__CargaMasiva_000_Validar_Datos_De_Entrada 
Go--#SQL 

Create Proc sp_Proceso_Pedidos_Cedis__CargaMasiva_000_Validar_Datos_De_Entrada
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '1005', @Tipo int = 2, @GUID Varchar(200) = 'f345e08e-da5c-44c2-a704-84c8e0a41b3d', 
	@Desglozar int = 2, @TipoTransferencia int = 0    
) 
With Encryption 
As 
Begin 
Set NoCount On 
-- Set Ansi_Warnings Off  --- Especial, peligroso 
Declare 
	@sPoliza_Salida varchar(8), 
	@sPoliza_Entrada varchar(8), 	
	@Observaciones varchar(500), 
	@SubTotal numeric(14,4), @Iva numeric(14,4), @Total numeric(14,4), 
	@PolizaAplicada varchar(1), @iOpcion smallint, 
	@iMostrarResultado int, 
	@ManejaUbicaciones tinyint,
	@Registros Int 


	Set @sPoliza_Salida = '*'  
	Set @sPoliza_Entrada = '*'  	
	Select @Observaciones = '' 
	Select @SubTotal = 0, @Iva = 0 , @Total = 0  
	Select @PolizaAplicada = 'N', @iOpcion = 1
	Set @iMostrarResultado = 0  
	Set @ManejaUbicaciones = 1 


	Select top 0 Cast('' As Varchar(200)) As Descripcion
	Into #Lista
	

	Update Pedidos_Cedis__CargaMasiva
	Set IdEmpresa = RIGHT('000' + IdEmpresa, 3), IdEstado  = RIGHT('00' + IdEstado, 2), IdFarmacia = RIGHT('0000' + IdFarmacia, 4), 
		IdEstadoSolicita = RIGHT('0000' + IdEstadoSolicita, 2),		
		IdFarmaciaSolicita = RIGHT('0000' + IdFarmaciaSolicita, 4),
		IdCliente = RIGHT('0000' + IdCliente, 4), IdSubCliente = RIGHT('0000' + IdSubCliente, 4), IdPrograma = RIGHT('0000' + IdPrograma, 4), IdSubPrograma = RIGHT('0000' + IdSubPrograma, 4),
		IdBeneficiario = RIGHT('00000000' + IdBeneficiario, 8)--, IdPersonal = RIGHT('00000000' + IdPersonal, 4)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And GUID = @GUID

	--Delete Pedidos_Cedis__CargaMasiva Where Cantidad <= 0


	------------------------------------------------------------------------------------------------------------------------ 
	----------------- SOLO SEPARAR EN GRUPDS  

	----select * 
	----From Pedidos_Cedis__CargaMasiva M 
	----Where ClaveSSA In ( Select ClaveSSA From vw_ClavesSSA_Sales (Nolock) )

---		sp_Proceso_Pedidos_Cedis__CargaMasiva_000_Validar_Datos_De_Entrada  

	--Select *
	UpDate M Set IdClaveSSA = S.IdClaveSSA_Sal, ContenidoPaquete = S.ContenidoPaquete
	From Pedidos_Cedis__CargaMasiva M (NoLock)
	Inner Join vw_ClavesSSA_Sales S (NoLock) On (M.ClaveSSA = S.ClaveSSA)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And GUID = @GUID  

	--Select * From Pedidos_Cedis__CargaMasiva Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia

	Update M Set TipoPedido = '1' 
	From Pedidos_Cedis__CargaMasiva M
	Where ClaveSSA In ( Select ClaveSSA From vw_ClavesSSA_Sales (Nolock) Where TipoDeClave = '01' )
		And M.IdEmpresa = @IdEmpresa And M.IdEstado = @IdEstado And M.IdFarmacia = @IdFarmacia And GUID = @GUID 
	
	Update M Set TipoPedido = '2' 
	From Pedidos_Cedis__CargaMasiva M  
	Where ClaveSSA In ( Select ClaveSSA From vw_ClavesSSA_Sales (Nolock) Where TipoDeClave = '02' )
		And M.IdEmpresa = @IdEmpresa And M.IdEstado = @IdEstado And M.IdFarmacia = @IdFarmacia And GUID = @GUID 
	
	Update M Set TipoPedido = '3' 
	From Pedidos_Cedis__CargaMasiva M 
	Where ClaveSSA In ( Select ClaveSSA From vw_ClavesSSA_Sales (Nolock) Where TipoDeClave = '02' and EsControlado = 1 )
		And M.IdEmpresa = @IdEmpresa And M.IdEstado = @IdEstado And M.IdFarmacia = @IdFarmacia And GUID = @GUID 
	

	Update M Set TipoPedido = '4' 
	From Pedidos_Cedis__CargaMasiva M
	Where ClaveSSA In ( Select ClaveSSA From vw_ClavesSSA_Sales (Nolock) Where TipoDeClave = '02' and EsAntibiotico = 1 )
		And M.IdEmpresa = @IdEmpresa And M.IdEstado = @IdEstado And M.IdFarmacia = @IdFarmacia And GUID = @GUID

	Update M Set TipoPedido = '5' 
	From Pedidos_Cedis__CargaMasiva M
	Where ClaveSSA In ( Select ClaveSSA From vw_ClavesSSA_Sales (Nolock) Where TipoDeClave = '02' and EsRefrigerado = 1 )
		And M.IdEmpresa = @IdEmpresa And M.IdEstado = @IdEstado And M.IdFarmacia = @IdFarmacia And GUID = @GUID  

	----------------- SOLO SEPARAR EN GRUPDS  
	------------------------------------------------------------------------------------------------------------------------ 


	------------------------------------------------------------------------------------------------------------------------ 
	----------------- SOLO SEPARAR EN 2 GRUPDS  ... MEDICAMENTO | MATERIAL DE CURACION 
	If @Desglozar = 1 
	Begin 

		Update M Set TipoPedido = '1' From Pedidos_Cedis__CargaMasiva M
		Where ClaveSSA In ( Select ClaveSSA From vw_ClavesSSA_Sales (Nolock) Where TipoDeClave = '01' )
			And M.IdEmpresa = @IdEmpresa And M.IdEstado = @IdEstado And M.IdFarmacia = @IdFarmacia And GUID = @GUID 

		Update M Set TipoPedido = '2' From Pedidos_Cedis__CargaMasiva M
		Where ClaveSSA In ( Select ClaveSSA From vw_ClavesSSA_Sales (Nolock) Where TipoDeClave = '02' ) 
			And M.IdEmpresa = @IdEmpresa And M.IdEstado = @IdEstado And M.IdFarmacia = @IdFarmacia And GUID = @GUID 

	End 

	----------------- SOLO SEPARAR EN 2 GRUPDS  ... MEDICAMENTO | MATERIAL DE CURACION 
	------------------------------------------------------------------------------------------------------------------------ 



	If (@Tipo = 1)  
		Begin

			Update M Set IdFarmaciaSolicita = IdFarmacia
			From Pedidos_Cedis__CargaMasiva M (NoLock)
			Where M.IdEmpresa = @IdEmpresa And M.IdEstado = @IdEstado And M.IdFarmacia = @IdFarmacia And GUID = @GUID
			----------------Select M.*
			----------------From Pedidos_Cedis__CargaMasiva M (NoLock)
			----------------Left Join vw_Farmacias_Clientes_SubClientes C (NoLock) On (M.IdEstado = C.IdEstado And M.IdFarmacia = C.IdFarmacia And M.IdCliente = C.IdCliente And M.IdSubCliente = C.IdSubCliente)
			----------------Where C.NombreCliente Is Null Or StatusRelacion = 'C' Or StatusSubCliente = 'C'

			----------Cliente-Programa
			Select distinct M.*
			Into #Cliente
			From Pedidos_Cedis__CargaMasiva M (NoLock)
			Left Join vw_Farmacias_Programas_SubPrograma_Clientes C (NoLock)
				On (M.IdEstado = C.IdEstado And M.IdFarmacia = C.IdFarmacia And
					M.IdCliente = C.IdCliente And M.IdSubCliente = C.IdSubCliente And M.IdPrograma = C.IdPrograma And M.IdSubPrograma = C.IdSubPrograma)
			Where M.IdEmpresa = @IdEmpresa And M.IdEstado = @IdEstado And M.IdFarmacia = @IdFarmacia And GUID = @GUID And
				  (C.NombreCliente Is Null Or StatusRelacion = 'C' Or StatusSubCliente = 'C' Or StatusSubPrograma = 'C')
			----------Cliente-Programa

			----------Beneficiario
			Select M.* 
			Into #Beneficiario
			From Pedidos_Cedis__CargaMasiva M (NoLock)
			Left Join CatBeneficiarios C (NoLock)
				On (M.IdEstado = C.IdEstado And M.IdFarmacia = C.IdFarmacia And M.IdCliente = C.IdCliente And M.IdSubCliente = C.IdSubCliente And M.IdBeneficiario = C.IdBeneficiario)
			Where M.IdEmpresa = @IdEmpresa And M.IdEstado = @IdEstado And M.IdFarmacia = @IdFarmacia And GUID = @GUID And
				  (C.Nombre Is Null Or Status = 'C' Or C.FechaFinVigencia < GETDATE())
			----------Beneficiario

			----------Observaciones
			Select IdEmpresa, IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, IdBeneficiario, Count(Distinct(Observaciones)) As Num
			Into #TmpObservaciones
			From Pedidos_Cedis__CargaMasiva M (NoLock)
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And GUID = @GUID
			Group By IdEmpresa, IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, IdBeneficiario
			Having Count(Distinct(Observaciones)) > 2

			Select M.*
			Into #Observaciones
			From Pedidos_Cedis__CargaMasiva M (NoLock)
			Inner Join #TmpObservaciones C (NoLock) 
			On (M.IdEstado = C.IdEstado And M.IdFarmacia = C.IdFarmacia And M.IdCliente = C.IdCliente And
				M.IdSubCliente = C.IdSubCliente And M.IdBeneficiario = C.IdBeneficiario)
			Where M.IdEmpresa = @IdEmpresa And M.IdEstado = @IdEstado And M.IdFarmacia = @IdFarmacia And GUID = @GUID
			----------Observaciones

			----------ReferenciaInterna
			Select IdEmpresa, IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, IdBeneficiario, Count(Distinct(ReferenciaInterna)) As Num
			Into #TmpReferenciaInterna
			From Pedidos_Cedis__CargaMasiva M (NoLock)
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And GUID = @GUID And ReferenciaInterna <> ''
			Group By IdEmpresa, IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, IdBeneficiario
			Having Count(Distinct(ReferenciaInterna)) > 1

			Select M.*
			Into #Referencia
			From Pedidos_Cedis__CargaMasiva M (NoLock) 
			Inner Join #TmpReferenciaInterna C (NoLock) 
			On (M.IdEstado = C.IdEstado And M.IdFarmacia = C.IdFarmacia And M.IdCliente = C.IdCliente And
				M.IdSubCliente = C.IdSubCliente And M.IdBeneficiario = C.IdBeneficiario)
			Where M.IdEmpresa = @IdEmpresa And M.IdEstado = @IdEstado And M.IdFarmacia = @IdFarmacia And GUID = @GUID

			Insert Into #Referencia
			Select M.*
			From Pedidos_Cedis__CargaMasiva M (NoLock)
			Where M.IdEmpresa = @IdEmpresa And M.IdEstado = @IdEstado And M.IdFarmacia = @IdFarmacia And GUID = @GUID And ReferenciaInterna = ''
			----------ReferenciaInterna


			----------FechaEntrega 
			Select IdEmpresa, IdEstado, IdFarmacia, IdFarmaciaSolicita, IdBeneficiario, Count(Distinct(FechaEntrega)) As Num
			Into #TMPFechaEntrega_Venta
			From Pedidos_Cedis__CargaMasiva M (NoLock) 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And GUID = @GUID
			Group By IdEmpresa, IdEstado, IdFarmacia, IdFarmaciaSolicita, IdBeneficiario 
			Having Count(Distinct(FechaEntrega)) > 1  

			Select 1 as Tipo, M.*
			Into #FechaEntrega_Venta
			From Pedidos_Cedis__CargaMasiva M (NoLock) 
			Inner Join #TMPFechaEntrega_Venta C (NoLock) On (M.IdEstado = C.IdEstado And M.IdFarmacia = C.IdFarmacia And M.IdFarmaciaSolicita = C.IdFarmaciaSolicita)
			Where M.IdEmpresa = @IdEmpresa And M.IdEstado = @IdEstado And M.IdFarmacia = @IdFarmacia And GUID = @GUID

			Insert Into #FechaEntrega_Venta 
			Select 2 as Tipo, M.*
			From Pedidos_Cedis__CargaMasiva M (NoLock)
			Where M.IdEmpresa = @IdEmpresa And M.IdEstado = @IdEstado And M.IdFarmacia = @IdFarmacia And GUID = @GUID And Convert(Varchar(10), FechaEntrega, 120) < Convert(Varchar(10), GETDATE(), 120)
			----------FechaEntrega

			Insert Into #Lista 
			Select 'Cliente - Programa' Union All
			Select 'Beneficiario'Union All
			Select 'Observaciones' Union All
			Select 'Referencia' Union All
			Select 'Fecha Entrega'

		End
	Else
		Begin

			Update M Set IdCliente = '0000', IdSubCliente = '0000', IdPrograma = '0000', IdSubPrograma = '0000', IdBeneficiario = ''
			From Pedidos_Cedis__CargaMasiva M (NoLock)
			Where M.IdEmpresa = @IdEmpresa And M.IdEstado = @IdEstado And M.IdFarmacia = @IdFarmacia And GUID = @GUID
			----------Farmacia
			Select M.*
			Into #Farmacia
			From Pedidos_Cedis__CargaMasiva M (NoLock)
			Left Join CatFarmacias C (NoLock) 
				On (M.IdEstado = C.IdEstado And M.IdFarmaciaSolicita = C.IdFarmacia)
			Where (C.NombreFarmacia Is Null Or C.Status = 'C' ) And M.IdEmpresa = @IdEmpresa And M.IdEstado = @IdEstado And M.IdFarmacia = @IdFarmacia And GUID = @GUID
				 
			----------Farmacia
			
			----------Observaciones
			Select IdEmpresa, IdEstado, IdFarmacia, IdFarmaciaSolicita, Count(Distinct(Observaciones)) As Num
			Into #TmpObservaciones_Trans
			From Pedidos_Cedis__CargaMasiva M (NoLock)
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And GUID = @GUID
			Group By IdEmpresa, IdEstado, IdFarmacia, IdFarmaciaSolicita
			Having Count(Distinct(Observaciones)) > 1

			Select M.*
			Into #Observaciones_Trans
			From Pedidos_Cedis__CargaMasiva M (NoLock)
			Inner Join #TmpObservaciones_Trans C (NoLock) On (M.IdEstado = C.IdEstado And M.IdFarmacia = C.IdFarmacia And M.IdFarmaciaSolicita = C.IdFarmaciaSolicita)
			Where M.IdEmpresa = @IdEmpresa And M.IdEstado = @IdEstado And M.IdFarmacia = @IdFarmacia And GUID = @GUID
			----------Observaciones

			----------ReferenciaInterna
			Select IdEmpresa, IdEstado, IdFarmacia, IdFarmaciaSolicita, Count(Distinct(ReferenciaInterna)) As Num
			Into #TmpReferenciaInterna_Trans
			From Pedidos_Cedis__CargaMasiva M (NoLock)
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And GUID = @GUID And ReferenciaInterna <> ''
			Group By IdEmpresa, IdEstado, IdFarmacia, IdFarmaciaSolicita
			Having Count(Distinct(ReferenciaInterna)) > 1

			Select M.*
			Into #ReferenciaInterna_Trans
			From Pedidos_Cedis__CargaMasiva M (NoLock)
			Inner Join #TmpReferenciaInterna_Trans C (NoLock) On (M.IdEstado = C.IdEstado And M.IdFarmacia = C.IdFarmacia And M.IdFarmaciaSolicita = C.IdFarmaciaSolicita)
			Where M.IdEmpresa = @IdEmpresa And M.IdEstado = @IdEstado And M.IdFarmacia = @IdFarmacia And GUID = @GUID

			Insert Into #ReferenciaInterna_Trans
			Select M.*
			From Pedidos_Cedis__CargaMasiva M (NoLock)
			Where M.IdEmpresa = @IdEmpresa And M.IdEstado = @IdEstado And M.IdFarmacia = @IdFarmacia And GUID = @GUID And ReferenciaInterna = ''
			----------ReferenciaInterna

			----------FechaEntrega
			Select IdEmpresa, IdEstado, IdFarmacia, IdFarmaciaSolicita, Count(Distinct(FechaEntrega)) As Num
			Into #TMPFechaEntrega_Trans
			From Pedidos_Cedis__CargaMasiva M (NoLock)
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And GUID = @GUID
			Group By IdEmpresa, IdEstado, IdFarmacia, IdFarmaciaSolicita
			Having Count(Distinct(FechaEntrega)) > 1

			Select M.*
			Into #FechaEntrega_Trans
			From Pedidos_Cedis__CargaMasiva M (NoLock)
			Inner Join #TMPFechaEntrega_Trans C (NoLock) On (M.IdEstado = C.IdEstado And M.IdFarmacia = C.IdFarmacia And M.IdFarmaciaSolicita = C.IdFarmaciaSolicita)
			Where M.IdEmpresa = @IdEmpresa And M.IdEstado = @IdEstado And M.IdFarmacia = @IdFarmacia And GUID = @GUID

			Insert Into #FechaEntrega_Trans
			Select M.*
			From Pedidos_Cedis__CargaMasiva M (NoLock)
			Where M.IdEmpresa = @IdEmpresa And M.IdEstado = @IdEstado And M.IdFarmacia = @IdFarmacia And GUID = @GUID And Convert(Varchar(10), FechaEntrega, 120) < Convert(Varchar(10), GETDATE(), 120)
			----------FechaEntrega

			Insert Into #Lista
			Select 'Farmacia' Union All 
			Select 'Observaciones' Union All 
			Select 'Referencia' Union All 
			Select 'Fecha Entrega' 
			
		End

		----------ClaveSSA
		Select M.*
		Into #ClaveSSA
		From Pedidos_Cedis__CargaMasiva M (NoLock)
		Left Join vw_ClavesSSA_Sales S (NoLock) On (M.ClaveSSA = S.ClaveSSA)
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And GUID = @GUID And S.DescripcionClave Is Null
		----------ClaveSSA

		----------Cantidad
		Select M.*
		Into #Cantidad
		From Pedidos_Cedis__CargaMasiva M (NoLock)
		Where M.Cantidad <= 0 And IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And GUID = @GUID
		----------Cantidad

		Insert Into #Lista 
		Select 'ClaveSSA' Union All
		Select 'Cantidad'


		------------------------------- SALIDA FINAL 
		Select * From #Lista
		
		If (@Tipo = 1)
			Begin
				Select * From #Cliente 
				Select * From #Beneficiario 
				Select * From #Observaciones 
				Select * From #Referencia  
				Select * From #FechaEntrega_Venta 
			End
		Else
			Begin
				Select * From #Farmacia 
				Select * From #Observaciones_Trans 
				Select * From #ReferenciaInterna_Trans 
				Select * From #FechaEntrega_Trans 
			End


		Select * From #ClaveSSA 
		Select * From #Cantidad 


		--Select TipoPedido, count(*) as Registros  
		--From Pedidos_Cedis__CargaMasiva M
		--group by TipoPedido 

		------------------------------- SALIDA FINAL 

End 
Go--#SQL

