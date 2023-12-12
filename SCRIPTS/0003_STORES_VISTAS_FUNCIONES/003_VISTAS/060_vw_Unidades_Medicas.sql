If Exists ( Select Name From sysobjects (nolock) Where Name = 'vw_Unidades_Medicas' and xType = 'V' ) 
   Drop View vw_Unidades_Medicas 
Go--#SQL 

Create View vw_Unidades_Medicas 
With Encryption 
As 
    Select U.IdEstado, IsNull(E.Nombre, 'GENERAL') as Estado, 
           U.IdJurisdiccion, IsNull(J.Descripcion, 'GENERAL') as Jurisdiccion, 
           U.IdUmedica, U.CLUES, U.NombreUnidadMedica as NombreUMedica, U.Status  
    From CatUnidadesMedicas U (NoLock) 
    Left Join CatEstados E (NoLock) On ( U.IdEstado = E.IdEstado ) 
    Left Join CatJurisdicciones J (Nolock) On ( U.IdEstado = J.IdEstado and U.IdJurisdiccion = J.IdJurisdiccion ) 

Go--#SQL 

        