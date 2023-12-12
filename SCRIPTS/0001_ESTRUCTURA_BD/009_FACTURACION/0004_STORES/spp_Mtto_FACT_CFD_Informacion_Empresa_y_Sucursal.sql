----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Mtto_FACT_CFD_Informacion_Empresa_y_Sucursal' and xType = 'P' ) 
   Drop Proc spp_Mtto_FACT_CFD_Informacion_Empresa_y_Sucursal
Go--#SQL    

Create Proc spp_Mtto_FACT_CFD_Informacion_Empresa_y_Sucursal 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0001' 
) 
As 
Begin 
Set NoCount On 

    Select  
		E.IdEmpresa, E.Nombre, 
		P.Usuario as RFC, P.Password as KeyLicencia, 
		C.IdPAC, 	
		C.NombrePAC as NombreProveedor, 
		(case when P.EnProduccion = 1 Then C.UrlProduccion Else C.UrlPruebas End ) as DireccionUrl, P.EnProduccion, 	
		E.Telefonos, E.Status, E.Actualizado  
    From FACT_CFD_Empresas E (NoLock)  
    Inner Join FACT_CFDI_Emisores_PAC P On ( E.IdEmpresa = P.IdEmpresa ) 
    Inner Join FACT_CFDI_PACs C On ( P.IdPAC = C.IdPAC ) 
    Where E.IdEmpresa = @IdEmpresa 

    Select 
		IdEmpresa, IdEstado, IdFarmacia, Nombre, RFC, Status, Actualizado  
    From FACT_CFD_Sucursales (NoLock)  
    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia  
      
    Select 
		Pais, Estado, Municipio, Localidad, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia 
    From FACT_CFD_Sucursales_Domicilio (NoLock) 
    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia  
                

End 
Go--#SQL    



