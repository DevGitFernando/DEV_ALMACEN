If Exists ( Select Name From SysObjects (NoLock) Where Name = 'spp_Mtto_CFGS_BD_PADRONES' and xType = 'P' ) 
   Drop Proc spp_Mtto_CFGS_BD_PADRONES  
Go--#SQL

Create Proc spp_Mtto_CFGS_BD_PADRONES 
(
     @NombreBD varchar(100), 
     @Status bit = 0
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare @sStatus varchar(1) 

    Set @sStatus = 'C'
    If @Status =  1 
        Set @sStatus = 'A'
    
--- Solo debe haber un Registro activo     
    Delete From CFGS_PADRON_ESTADOS 
    Delete From CFGS_BD_PADRONES     
    Insert Into CFGS_BD_PADRONES ( NombreBD, Status ) values ( @NombreBD, @sStatus )  

End 
Go--#SQL

----------------------------------------------------------------------------------------------------------
If Exists ( Select Name From SysObjects (NoLock) Where Name = 'spp_Mtto_CFGS_PADRON_ESTADOS' and xType = 'P' ) 
   Drop Proc spp_Mtto_CFGS_PADRON_ESTADOS  
Go--#SQL

Create Proc spp_Mtto_CFGS_PADRON_ESTADOS 
(
    @IdEstado varchar(2) = '25', @IdCliente varchar(4) = '0002', 
    @NombreBD varchar(100) = '', @NombrePadron varchar(200) = '', 
    @Status bit = 0, 
    @EsLocal bit = 0     
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare @sStatus varchar(1) 

    Set @sStatus = 'C'
    If @Status =  1 
        Set @sStatus = 'A' 
        
    If Not Exists ( Select * From CFGS_PADRON_ESTADOS (NoLock) Where IdEstado = @IdEstado and NombreBD = @NombreBD and NombreTabla = @IdEstado ) 
       Insert Into CFGS_PADRON_ESTADOS ( IdEstado, IdCliente, NombreBD, NombreTabla, Status, EsLocal ) 
       Select @IdEstado, @IdCliente, @NombreBD, @NombrePadron, @sStatus, @EsLocal 
    Else 
       Update CFGS_PADRON_ESTADOS Set Status = @sStatus, EsLocal = @EsLocal 
       Where IdEstado = @IdEstado and NombreBD = @NombreBD and NombreTabla = @IdEstado
                
        
End 
Go--#SQL

       