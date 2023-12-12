------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Beneficiarios' and xType = 'V' ) 
	Drop View vw_Beneficiarios   
Go--#SQL 	

Create View vw_Beneficiarios 
With Encryption 
As 
	Select
		   B.IdEstado, F.Estado, B.IdFarmacia, F.Farmacia, 
		   B.IdCliente, C.NombreCliente, B.IdSubCliente, C.NombreSubCliente, 
		   B.IdBeneficiario, 
		   B.Nombre, B.ApPaterno, B.ApMaterno, (B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre) as NombreCompleto,   

		   B.IdTipoDerechoHabiencia, DH.Descripcion as DerechoHabiencia, 
		   B.IdEstadoResidencia, E.Nombre as EstadoDeResidencia, E.ClaveRENAPO as ClaveRENAPO__EstadoDeResidencia, 

		   B.TipoDeBeneficiario, 
		   (	
				case when B.TipoDeBeneficiario = 1 then 'Farmacia'		
					 when B.TipoDeBeneficiario = 2 Then 'Hospital (Venta directa)'			
					 when B.TipoDeBeneficiario = 3 Then 'Jurisdicción (Venta directa)' 
				else 'NO ESPECIFICADO'		
				end		
			) as 
		   TipoDeBeneficiarioDesc, 
		   B.IdJurisdiccion, J.Descripcion As Jurisdiccion, 
		   B.Domicilio, 		 
		   B.Sexo, 
		   (	
				case when B.Sexo = 'M' then 'Masculino'		
					 when B.Sexo = 'F' Then 'Femenino'			
				else ''		
				end		
			) as 
		   SexoAux, 

		   B.FechaNacimiento, dbo.fg_CalcularEdad(B.FechaNacimiento) as Edad, 
		   B.IdTipoDeIdentificacion, I.Descripcion as TipoDeIdentificacion, 
		   B.CURP as NumeroDeIdentificacion,  
		   B.CURP, 
		   B.FolioReferencia, B.FolioReferenciaAuxiliar, 
		   B.FechaInicioVigencia, B.FechaFinVigencia, 
		   (case when datediff(dd, getdate(), B.FechaFinVigencia) < 0 then 0 else 1 end) as EsVigente, 
		   B.FechaRegistro, B.Status, B.Status as StatusBeneficiario 
	From CatBeneficiarios B (NoLock) 
	Inner Join vw_Farmacias F (NoLock) On ( B.IdEstado = F.IdEstado and B.IdFarmacia = F.IdFarmacia ) 
	Inner Join vw_Clientes_SubClientes C (NoLock) On ( B.IdCliente = C.IdCliente and B.IdSubCliente = C.IdSubCliente ) 
	Inner Join CatJurisdicciones J (NoLock) On ( B.IdEstado = J.IdEstado And B.IdJurisdiccion = J.IdJurisdiccion ) 
	Inner Join CatTiposDeIdentificaciones I (NoLock) On ( B.IdTipoDeIdentificacion = I.IdTipoDeIdentificacion ) 
	Inner Join CatTiposDeDerechohabiencia DH On ( B.IdTipoDerechoHabiencia = DH.IdTipoDerechoHabiencia ) 
	Inner Join CatEstados E (NoLock) On ( B.IdEstadoResidencia = E.IdEstado ) 

Go--#SQL	
 
