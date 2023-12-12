-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_RE_SIGHO__RecetasElectronicas_0011_MarcarRecetaSurtido' and xType = 'P' ) 
   Drop Proc spp_INT_RE_SIGHO__RecetasElectronicas_0011_MarcarRecetaSurtido
Go--#SQL 

Create Proc spp_INT_RE_SIGHO__RecetasElectronicas_0011_MarcarRecetaSurtido 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', 
	@IdFarmacia varchar(4) = '2193', 
	@FolioInterface varchar(50) = '000000000008', 
	@FolioSurtido varchar(20) = '00000001' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 
Declare 
	@FechaActual varchar(20),  
	@sFolio varchar(20), 
	@sMensaje varchar(200), 
	@sIdBeneficiario varchar(10), 
	@sNombreBeneficiario varchar(500), 
	@sIdMedico varchar(10), 
	@sNombreMedico varchar(500), 
	@NombreB varchar(100), @ApPatB varchar(100), @ApMatB varchar(100), @Sexo varchar(10), 
	@NumReferencia varchar(100), 
	@FechaVigencia_Inicia varchar(20), @FechaVigencia_Termina varchar(20),  
	@NombreM varchar(100), @ApPatM varchar(100), @ApMatM varchar(100), 
	@sNumeroCedula varchar(20), @sIdEspecialidad varchar(10)   
	


------------------------------------------------- OBTENER LA INFORMACION   
	Update X Set EsSurtido = 1, FolioSurtido = @FolioSurtido, FechaDeSurtido = getdate() 
	From INT_RE_SIGHO__RecetasElectronicas_0001_General X (NoLock)
	Where x.EsSurtido = 0 and x.Procesado = 0 and X.Folio = @FolioInterface 
		and x.IdEmpresa = @IdEmpresa and x.IdEstado = @IdEstado and x.IdFarmacia = @IdFarmacia 

	Select ClaveSSA, Sum(CantidadVendida) As Cantidad
	Into #Claves
	From VentasDet D (NoLock)
	Inner JOin vw_productos_CodigoEAN P (NoLock) On ( D.CodigoEAN = P.CodigoEAN )
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia And FolioVenta = @FolioSurtido
	Group By ClaveSSA

	Update X Set CantidadEntregada = Cantidad
	From INT_RE_SIGHO__RecetasElectronicas_0004_Insumos X
	Inner Join #Claves C (NoLock) On (X.ClaveSSA = C.ClaveSSA)
	Where X.Folio = @FolioInterface And x.IdEmpresa = @IdEmpresa And x.IdEstado = @IdEstado And x.IdFarmacia = @IdFarmacia 
	
End 
Go--#SQL 
	
--		select * from INT_RE_SIGHO__RecetasElectronicas_0001_General 
	
	