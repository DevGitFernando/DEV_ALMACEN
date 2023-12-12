/* 

--    select * from tmpCatPersonal P

--    Insert Into tmpCatPersonal ( IdEstado, IdFarmacia, IdPersonal, Nombre, ApPaterno, ApMaterno, FechaRegistro, Status, Actualizado ) 
    Select IdEstado, IdFarmacia, IdPersonal, Nombre, ApPaterno, ApMaterno, FechaRegistro, Status, Actualizado
    From 
    ( 
        Select P.IdEstado, F.IdFarmacia, P.IdPersonal, P.Nombre, P.ApPaterno, P.ApMaterno, P.FechaRegistro, P.Status, P.Actualizado 
        From tmpCatPersonal P, tmpCatFarmacias F 
    ) as T 
    Where IdFarmacia >= 6 
----          and Not Exists ( Select * From tmpCatPersonal R (NoLock) 
----                           Where T.IdEstado = R.IdEstado and T.IdFarmacia = R.IdFarmacia and T.IdPersonal = R.IdPersonal )
    

--    select top 1 * from tmpNet_Usuarios 

    Insert Into tmpNet_Usuarios 
    Select * 
    From 
    (         
        Select P.IdEstado, F.IdFarmacia, P.IdPersonal, P.LoginUser, '' as Password, P.Status, P.Actualizado, getdate() as FechaUpdate  
        From tmpNet_Usuarios P, tmpCatFarmacias F 
     ) As T 
    Where IdFarmacia >= 6 
                


--  select * from tmpNet_Grupos_De_Usuarios 

    Select * 
    From 
    (         
        Select P.IdEstado, F.IdFarmacia, P.IdGrupo, P.NombreGrupo, P.Status, P.Actualizado, getdate() as FechaUpdate  
        From tmpNet_Grupos_De_Usuarios P, tmpCatFarmacias F 
     ) As T 
    Where IdFarmacia >= 6 
    

------------------- 
    sp_listacolumnas tmpMovtos_Inv_Tipos_Farmacia  ,1 
    
    select top 100 * 
    from Movtos_Inv_Tipos 
    order by Keyx 
    
    select top 0 * into  tmpMovtos_Inv_Tipos_Farmacia from Movtos_Inv_Tipos_Farmacia 
    
    alter table tmpMovtos_Inv_Tipos_Farmacia add constraint PK_tmpMovtos_Inv_Tipos_Farmacia Primary Key ( IdEstado, IdFarmacia, IdTipoMovto_Inv )
    

    
    select * 
    from tmpMovtos_Inv_Tipos_Farmacia 


    insert into tmpMovtos_Inv_Tipos_Farmacia     
    Select * 
    From 
    (         
        Select F.IdEstado, F.IdFarmacia, P.IdTipoMovto_Inv, 0 as Consecutivo, 'A' as Status, 1 as Actualizado 
        From Movtos_Inv_Tipos P, tmpCatFarmacias F 
     ) As T 
    Where IdFarmacia >= 4  

---------------------------------------------------------

--      delete from tmpNet_Grupos_Usuarios_Miembros

    select * 
--    into     tmpNet_Grupos_Usuarios_Miembros
    from Net_Grupos_Usuarios_Miembros 
    Where IdEstado = 30 
    
--  alter table tmpNet_Grupos_Usuarios_Miembros Add Constraint PK_tmpNet_Grupos_Usuarios_Miembros Primary Key ( IdEstado, IdSucursal, IdGrupo, IdPersonal )     
    
--- Insert Into  tmpNet_Grupos_Usuarios_Miembros        
    Select * 
    From 
    (         
        Select P.IdEstado, F.IdFarmacia, P.IdGrupo, P.IdPersonal, P.LoginUser, P.Status, P.Actualizado, getdate() as FechaUpdate  
        From tmpNet_Grupos_Usuarios_Miembros P, tmpCatFarmacias F 
     ) As T 
    Where IdFarmacia >= 6 
        
        
    insert into tmpMovtos_Inv_Tipos_Farmacia     
    Select * 
    From 
    (         
        Select F.IdEstado, F.IdFarmacia, P.IdTipoMovto_Inv, 0 as Consecutivo, 'A' as Status, 1 as Actualizado 
        From Movtos_Inv_Tipos P, tmpCatFarmacias F 
--        where F.IdEstado = 25 --- and IdFarmacia = 8 
     ) As T 
    Where T.IdFarmacia >= 6 and 
        not exists 
        ( 
            Select * 
            From tmpMovtos_Inv_Tipos_Farmacia F 
            Where T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia and T.IdTipoMovto_Inv = F.IdTipoMovto_Inv 
        ) 

*/ 
        
    sp_generainserts 'tmpMovtos_Inv_Tipos_Farmacia', 1  
               