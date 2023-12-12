If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Chequeras' and xType = 'V' ) 
   Drop View vw_Chequeras 
Go--#SQL

Create View vw_Chequeras 
With Encryption 
As 

    Select IdEmpresa, IdEstado, IdChequera, C.Descripcion, C.IdBanco, B.Descripcion As Banco, FolioInicio, FolioFin, NumeroDeSerie, C.UltimoFolio, C.Status
    From CNT_CatChequeras C (NoLock)
    Inner Join CNT_CatBancos B (NoLock) On (C.IdBanco = B.IdBanco)
     
Go--#SQL