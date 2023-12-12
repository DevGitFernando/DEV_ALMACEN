If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Cheque' and xType = 'V' ) 
   Drop View vw_Cheque 
Go--#SQL

Create View vw_Cheque 
With Encryption 
As 

    Select
		Fc.IdEmpresa, NombreEmpresa, Fc.IdEstado, NombreEstado, IdCheque, Fc.Descripcion As Cheque, Fc.IdChequera, C.Descripcion As Chequera, FolioCheque, C.IdBanco, Banco,
		Fc.IdBeneficiario, B.Descripcion As Beneficiario, Cantidad, FechaRegistro, Fc.Status,
		(Case When Fc.Status = 'A' Then 'Activo' Else 'Cancelado' End) As StatusNombre
    From CNT_Cheque Fc(NoLock)
    Inner Join vw_EmpresasEstados E (NoLock) On (Fc.IdEmpresa = E.IdEmpresa And Fc.IdEstado = E.IdEstado)
    Inner Join vw_Chequeras C (NoLock) On (Fc.IdEmpresa = C.IdEmpresa And Fc.IdEmpresa = C.IdEmpresa And Fc.IdChequera = C.IdChequera)
    Inner Join CNT_CatChequesBeneficiarios B (NoLock) On (Fc.IdBeneficiario = B.IdBeneficiario)
     
Go--#SQL
