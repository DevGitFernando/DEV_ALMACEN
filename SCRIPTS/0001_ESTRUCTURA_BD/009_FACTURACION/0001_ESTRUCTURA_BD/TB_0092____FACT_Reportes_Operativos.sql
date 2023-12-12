
--- Drop Table FACT_Reportes_Operativos 

-----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Not Exists ( Select So.* From sysobjects So (NoLock) Where So.Name = 'FACT_Reportes_Operativos' and xType = 'U' )  
Begin 	

	Create Table FACT_Reportes_Operativos
	(
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null,
		IdReporte varchar(2) Not Null, 
		Descripcion varchar(400) Not Null, 


		Procesa_Producto bit Not Null Default 'true', 
		Procesa_Servicio bit Not Null Default 'true', 

		Procesa_Venta bit Not Null Default 'true', 
		Procesa_Consigna bit Not Null Default 'true', 
		Procesa_Medicamento bit Not Null Default 'true', 
		Procesa_MaterialDeCuracion bit Not Null Default 'true', 

		Status varchar(1) Not Null Default ''
	)

		Alter Table FACT_Reportes_Operativos Add Constraint PK_FACT_Reportes_Operativos
		Primary Key ( IdEstado, IdFarmacia, IdReporte ) 
End 
Go--#SQL


If Not Exists ( Select * From FACT_Reportes_Operativos Where IdEstado = '11' and IdFarmacia = '0001' and IdReporte = '1' )  Insert Into FACT_Reportes_Operativos (  IdEstado, IdFarmacia, IdReporte, Descripcion, Procesa_Producto, Procesa_Servicio, Procesa_Venta, Procesa_Consigna, Procesa_Medicamento, Procesa_MaterialDeCuracion, Status )  Values ( '11', '0001', '1', 'Sabana de validacion', 1, 1, 1, 1, 1, 1, 'A' )  Else Update FACT_Reportes_Operativos Set Descripcion = 'Sabana de validacion', Procesa_Producto = 1, Procesa_Servicio = 1, Procesa_Venta = 1, Procesa_Consigna = 1, Procesa_Medicamento = 1, Procesa_MaterialDeCuracion = 1, Status = 'A' Where IdEstado = '11' and IdFarmacia = '0001' and IdReporte = '1'  
If Not Exists ( Select * From FACT_Reportes_Operativos Where IdEstado = '11' and IdFarmacia = '0001' and IdReporte = '2' )  Insert Into FACT_Reportes_Operativos (  IdEstado, IdFarmacia, IdReporte, Descripcion, Procesa_Producto, Procesa_Servicio, Procesa_Venta, Procesa_Consigna, Procesa_Medicamento, Procesa_MaterialDeCuracion, Status )  Values ( '11', '0001', '2', 'Sabana de entrega', 1, 1, 1, 1, 1, 1, 'A' )  Else Update FACT_Reportes_Operativos Set Descripcion = 'Sabana de entrega', Procesa_Producto = 1, Procesa_Servicio = 1, Procesa_Venta = 1, Procesa_Consigna = 1, Procesa_Medicamento = 1, Procesa_MaterialDeCuracion = 1, Status = 'A' Where IdEstado = '11' and IdFarmacia = '0001' and IdReporte = '2'  
If Not Exists ( Select * From FACT_Reportes_Operativos Where IdEstado = '11' and IdFarmacia = '0001' and IdReporte = '31' )  Insert Into FACT_Reportes_Operativos (  IdEstado, IdFarmacia, IdReporte, Descripcion, Procesa_Producto, Procesa_Servicio, Procesa_Venta, Procesa_Consigna, Procesa_Medicamento, Procesa_MaterialDeCuracion, Status )  Values ( '11', '0001', '31', 'Resumen de remisiones', 1, 1, 1, 1, 1, 1, 'A' )  Else Update FACT_Reportes_Operativos Set Descripcion = 'Resumen de remisiones', Procesa_Producto = 1, Procesa_Servicio = 1, Procesa_Venta = 1, Procesa_Consigna = 1, Procesa_Medicamento = 1, Procesa_MaterialDeCuracion = 1, Status = 'A' Where IdEstado = '11' and IdFarmacia = '0001' and IdReporte = '31'  
If Not Exists ( Select * From FACT_Reportes_Operativos Where IdEstado = '11' and IdFarmacia = '0001' and IdReporte = '32' )  Insert Into FACT_Reportes_Operativos (  IdEstado, IdFarmacia, IdReporte, Descripcion, Procesa_Producto, Procesa_Servicio, Procesa_Venta, Procesa_Consigna, Procesa_Medicamento, Procesa_MaterialDeCuracion, Status )  Values ( '11', '0001', '32', 'Listado de remisiones', 1, 1, 1, 1, 1, 1, 'A' )  Else Update FACT_Reportes_Operativos Set Descripcion = 'Listado de remisiones', Procesa_Producto = 1, Procesa_Servicio = 1, Procesa_Venta = 1, Procesa_Consigna = 1, Procesa_Medicamento = 1, Procesa_MaterialDeCuracion = 1, Status = 'A' Where IdEstado = '11' and IdFarmacia = '0001' and IdReporte = '32'  
If Not Exists ( Select * From FACT_Reportes_Operativos Where IdEstado = '11' and IdFarmacia = '0001' and IdReporte = '51' )  Insert Into FACT_Reportes_Operativos (  IdEstado, IdFarmacia, IdReporte, Descripcion, Procesa_Producto, Procesa_Servicio, Procesa_Venta, Procesa_Consigna, Procesa_Medicamento, Procesa_MaterialDeCuracion, Status )  Values ( '11', '0001', '51', 'Reporte de costo operativo (Costo vs Venta)', 1, 0, 1, 0, 1, 1, 'A' )  Else Update FACT_Reportes_Operativos Set Descripcion = 'Reporte de costo operativo (Costo vs Venta)', Procesa_Producto = 1, Procesa_Servicio = 0, Procesa_Venta = 1, Procesa_Consigna = 0, Procesa_Medicamento = 1, Procesa_MaterialDeCuracion = 1, Status = 'A' Where IdEstado = '11' and IdFarmacia = '0001' and IdReporte = '51'  

Go--#SQL



--		sp_generainserts FACT_Reportes_Operativos ,1 

	--	Procesa_Producto, Procesa_Servicio, (case when Procesa_Producto = 1 and Procesa_Servicio = 1 then 1 else 0 end) as Procesa_Remision_Ambos, 
	--	Procesa_Venta, Procesa_Consigna, (case when Procesa_Venta = 1 and Procesa_Consigna = 1 then 1 else 0 end) as Procesa_Origen_Ambos, 
	--	Procesa_Medicamento, Procesa_MaterialDeCuracion, (case when Procesa_Medicamento = 1 and Procesa_MaterialDeCuracion = 1 then 1 else 0 end) as Procesa_Insumo_Ambos  
		
