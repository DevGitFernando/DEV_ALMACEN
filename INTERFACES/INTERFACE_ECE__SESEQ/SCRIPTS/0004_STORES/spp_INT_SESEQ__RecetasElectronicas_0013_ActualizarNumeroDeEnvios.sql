-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__RecetasElectronicas_0013_ActualizarNumeroDeEnvios' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__RecetasElectronicas_0013_ActualizarNumeroDeEnvios
Go--#SQL 

Create Proc spp_INT_SESEQ__RecetasElectronicas_0013_ActualizarNumeroDeEnvios 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', 
	@IdFarmacia varchar(4) = '2193', 
	@FolioInterface varchar(20) = '000000000008', 
	@FolioSurtido varchar(20) = '00000001' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 
----Declare 
----	@FechaActual varchar(20),  
----	@sFolio varchar(20), 
----	@sMensaje varchar(200), 
----	@sIdBeneficiario varchar(10), 
----	@sNombreBeneficiario varchar(500), 
----	@sIdMedico varchar(10), 
----	@sNombreMedico varchar(500), 
----	@NombreB varchar(100), @ApPatB varchar(100), @ApMatB varchar(100), @Sexo varchar(10), 
----	@NumReferencia varchar(100), 
----	@FechaVigencia_Inicia varchar(20), @FechaVigencia_Termina varchar(20),  
----	@NombreM varchar(100), @ApPatM varchar(100), @ApMatM varchar(100), 
----	@sNumeroCedula varchar(20), @sIdEspecialidad varchar(10)   
	


------------------------------------------------- OBTENER LA INFORMACION   
	Update G Set IntentosDeEnvio = IntentosDeEnvio + 1  
	From INT_SESEQ__RecetasElectronicas_XML X (NoLock) 
	Inner Join INT_SESEQ__RecetasElectronicas_0001_General G (NoLock) 
		On ( X.IdEmpresa = G.IdEmpresa and X.IdEstado = G.IdEstado and X.IdFarmacia = G.IdFarmacia and X.Folio = G.FolioXML ) 	
	Where G.EsSurtido = 0 and G.Procesado = 0 and G.Folio = @FolioInterface 
		and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 


	
End 
Go--#SQL 
	
--		select * from INT_SESEQ__RecetasElectronicas_0001_General 
	
	