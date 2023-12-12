

If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_PedidosDet_Consignacion' and xType = 'P')
    Drop Proc spp_Mtto_PedidosDet_Consignacion
Go--#SQL

  
Create Proc spp_Mtto_PedidosDet_Consignacion (@IdEmpresa varchar(5), @IdEstado varchar(4), @IdFarmacia varchar(6), @Folio varchar(32))
With Encryption 
As
Begin
Set NoCount On


Insert Into PedidosDet_Consignacion (IdEmpresa, IdEstado, IdFarmacia, Folio, IdClaveSSA, ClaveSSA, DescripcionClaveSSA, Costo, Cantidad, Iva, Status, Actualizado)
Select @IdEmpresa, @IdEstado, @IdFarmacia, @Folio, IdClaveSSA, ClaveSSA, DescripcionClaveSSA, Costo, Cantidad, Iva, 'A' As Status, 0 As Actualizado
	From Pedidos_CargaMasiva

End
Go--#SQL 	
