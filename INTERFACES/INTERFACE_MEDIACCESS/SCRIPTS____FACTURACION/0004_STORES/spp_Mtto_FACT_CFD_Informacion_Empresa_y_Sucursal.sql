----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Mtto_FACT_CFD_Informacion_Empresa_y_Sucursal' and xType = 'P' ) 
   Drop Proc spp_Mtto_FACT_CFD_Informacion_Empresa_y_Sucursal
Go--#SQL    

Create Proc spp_Mtto_FACT_CFD_Informacion_Empresa_y_Sucursal 
(
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '09', @IdFarmacia varchar(4) = '0001' 
) 
As 
Begin 
Set NoCount On 

---------------- Informacion del Emisor 
	Select 
		E.IdEmpresa, E.NombreFiscal as Nombre, E.NombreFiscal, E.NombreComercial, E.RFC, 	
		E.Telefonos, E.Fax, E.Email, E.DomExpedicion_DomFiscal, E.Status, 
		P.IdPAC, C.NombrePAC as NombreProveedor, 
		P.Usuario, P.Password, P.Password as KeyLicencia, P.EnProduccion, 
		C.UrlProduccion as DireccionUrl, 0 as Actualizado 
	From CFDI_Emisores E (NoLock) 
	Inner Join CFDI_Emisores_PAC P (NoLock) On ( E.IdEmpresa = P.IdEmpresa ) 
	Inner Join CFDI_PACs C (NoLock) On ( P.IdPAC = C.IdPAC ) 
	Where E.IdEmpresa = @IdEmpresa   

    Select 
		IdEmpresa, IdEstado, IdFarmacia, NombreFiscal as Nombre, RFC, Status, 0 as Actualizado  
	From vw_CFDI_Emisores_Sucursales (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
      
    Select 
		Pais, Estado, Municipio, Municipio as Localidad, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia 
	From vw_CFDI_Emisores_Sucursales (NoLock) 
    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia  
                

End 
Go--#SQL    



