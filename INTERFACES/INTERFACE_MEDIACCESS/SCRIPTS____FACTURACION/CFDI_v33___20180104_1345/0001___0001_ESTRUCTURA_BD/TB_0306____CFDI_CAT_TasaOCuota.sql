------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_TasaOCuota' and xType = 'U' ) 
	Drop Table CFDI_TasaOCuota    
Go--#SQL  

Create Table CFDI_TasaOCuota 
(	
	Clave varchar(10) Not Null Default '', 
	Factor varchar(10) Not Null Default '', 
	EsTraslado bit Not Null Default 'false', 
	EsRetencion bit Not Null Default 'false', 
	TipoRango varchar(20) Not Null Default '', 
	ValorMinimo numeric(14, 6) Not Null Default 0, 
	ValorMaximo numeric(14, 6) Not Null Default 0, 
	Status varchar(1) Not Null Default 'A' 
) 
Go--#SQL 

Alter Table CFDI_TasaOCuota Add Constraint PK_CFDI_TasaOCuota Primary	Key ( Clave, TipoRango, ValorMaximo ) 
Go--#SQL 


If Not Exists ( Select * From CFDI_TasaOCuota Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 0.000000 )  Insert Into CFDI_TasaOCuota (  Clave, Factor, EsTraslado, EsRetencion, TipoRango, ValorMinimo, ValorMaximo, Status )  Values ( 'IEPS', 'Tasa', 1, 0, 'Fijo', 0.000000, 0.000000, 'A' ) 
 Else Update CFDI_TasaOCuota Set Factor = 'Tasa', EsTraslado = 1, EsRetencion = 0, ValorMinimo = 0.000000, Status = 'A' Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 0.000000  
If Not Exists ( Select * From CFDI_TasaOCuota Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 0.030000 )  Insert Into CFDI_TasaOCuota (  Clave, Factor, EsTraslado, EsRetencion, TipoRango, ValorMinimo, ValorMaximo, Status )  Values ( 'IEPS', 'Tasa', 1, 0, 'Fijo', 0.030000, 0.030000, 'A' ) 
 Else Update CFDI_TasaOCuota Set Factor = 'Tasa', EsTraslado = 1, EsRetencion = 0, ValorMinimo = 0.030000, Status = 'A' Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 0.030000  
If Not Exists ( Select * From CFDI_TasaOCuota Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 0.060000 )  Insert Into CFDI_TasaOCuota (  Clave, Factor, EsTraslado, EsRetencion, TipoRango, ValorMinimo, ValorMaximo, Status )  Values ( 'IEPS', 'Tasa', 1, 1, 'Fijo', 0.060000, 0.060000, 'A' ) 
 Else Update CFDI_TasaOCuota Set Factor = 'Tasa', EsTraslado = 1, EsRetencion = 1, ValorMinimo = 0.060000, Status = 'A' Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 0.060000  
If Not Exists ( Select * From CFDI_TasaOCuota Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 0.070000 )  Insert Into CFDI_TasaOCuota (  Clave, Factor, EsTraslado, EsRetencion, TipoRango, ValorMinimo, ValorMaximo, Status )  Values ( 'IEPS', 'Tasa', 1, 1, 'Fijo', 0.070000, 0.070000, 'A' ) 
 Else Update CFDI_TasaOCuota Set Factor = 'Tasa', EsTraslado = 1, EsRetencion = 1, ValorMinimo = 0.070000, Status = 'A' Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 0.070000  
If Not Exists ( Select * From CFDI_TasaOCuota Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 0.080000 )  Insert Into CFDI_TasaOCuota (  Clave, Factor, EsTraslado, EsRetencion, TipoRango, ValorMinimo, ValorMaximo, Status )  Values ( 'IEPS', 'Tasa', 1, 1, 'Fijo', 0.080000, 0.080000, 'A' ) 
 Else Update CFDI_TasaOCuota Set Factor = 'Tasa', EsTraslado = 1, EsRetencion = 1, ValorMinimo = 0.080000, Status = 'A' Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 0.080000  
If Not Exists ( Select * From CFDI_TasaOCuota Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 0.090000 )  Insert Into CFDI_TasaOCuota (  Clave, Factor, EsTraslado, EsRetencion, TipoRango, ValorMinimo, ValorMaximo, Status )  Values ( 'IEPS', 'Tasa', 1, 1, 'Fijo', 0.090000, 0.090000, 'A' ) 
 Else Update CFDI_TasaOCuota Set Factor = 'Tasa', EsTraslado = 1, EsRetencion = 1, ValorMinimo = 0.090000, Status = 'A' Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 0.090000  
If Not Exists ( Select * From CFDI_TasaOCuota Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 0.250000 )  Insert Into CFDI_TasaOCuota (  Clave, Factor, EsTraslado, EsRetencion, TipoRango, ValorMinimo, ValorMaximo, Status )  Values ( 'IEPS', 'Tasa', 1, 1, 'Fijo', 0.250000, 0.250000, 'A' ) 
 Else Update CFDI_TasaOCuota Set Factor = 'Tasa', EsTraslado = 1, EsRetencion = 1, ValorMinimo = 0.250000, Status = 'A' Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 0.250000  
If Not Exists ( Select * From CFDI_TasaOCuota Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 0.265000 )  Insert Into CFDI_TasaOCuota (  Clave, Factor, EsTraslado, EsRetencion, TipoRango, ValorMinimo, ValorMaximo, Status )  Values ( 'IEPS', 'Tasa', 1, 1, 'Fijo', 0.265000, 0.265000, 'A' ) 
 Else Update CFDI_TasaOCuota Set Factor = 'Tasa', EsTraslado = 1, EsRetencion = 1, ValorMinimo = 0.265000, Status = 'A' Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 0.265000  
If Not Exists ( Select * From CFDI_TasaOCuota Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 0.300000 )  Insert Into CFDI_TasaOCuota (  Clave, Factor, EsTraslado, EsRetencion, TipoRango, ValorMinimo, ValorMaximo, Status )  Values ( 'IEPS', 'Tasa', 1, 1, 'Fijo', 0.300000, 0.300000, 'A' ) 
 Else Update CFDI_TasaOCuota Set Factor = 'Tasa', EsTraslado = 1, EsRetencion = 1, ValorMinimo = 0.300000, Status = 'A' Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 0.300000  
If Not Exists ( Select * From CFDI_TasaOCuota Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 0.304000 )  Insert Into CFDI_TasaOCuota (  Clave, Factor, EsTraslado, EsRetencion, TipoRango, ValorMinimo, ValorMaximo, Status )  Values ( 'IEPS', 'Tasa', 1, 1, 'Fijo', 0.304000, 0.304000, 'A' ) 
 Else Update CFDI_TasaOCuota Set Factor = 'Tasa', EsTraslado = 1, EsRetencion = 1, ValorMinimo = 0.304000, Status = 'A' Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 0.304000  
If Not Exists ( Select * From CFDI_TasaOCuota Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 0.500000 )  Insert Into CFDI_TasaOCuota (  Clave, Factor, EsTraslado, EsRetencion, TipoRango, ValorMinimo, ValorMaximo, Status )  Values ( 'IEPS', 'Tasa', 1, 1, 'Fijo', 0.500000, 0.500000, 'A' ) 
 Else Update CFDI_TasaOCuota Set Factor = 'Tasa', EsTraslado = 1, EsRetencion = 1, ValorMinimo = 0.500000, Status = 'A' Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 0.500000  
If Not Exists ( Select * From CFDI_TasaOCuota Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 0.530000 )  Insert Into CFDI_TasaOCuota (  Clave, Factor, EsTraslado, EsRetencion, TipoRango, ValorMinimo, ValorMaximo, Status )  Values ( 'IEPS', 'Tasa', 1, 1, 'Fijo', 0.530000, 0.530000, 'A' ) 
 Else Update CFDI_TasaOCuota Set Factor = 'Tasa', EsTraslado = 1, EsRetencion = 1, ValorMinimo = 0.530000, Status = 'A' Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 0.530000  
If Not Exists ( Select * From CFDI_TasaOCuota Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 1.600000 )  Insert Into CFDI_TasaOCuota (  Clave, Factor, EsTraslado, EsRetencion, TipoRango, ValorMinimo, ValorMaximo, Status )  Values ( 'IEPS', 'Tasa', 1, 1, 'Fijo', 1.600000, 1.600000, 'A' ) 
 Else Update CFDI_TasaOCuota Set Factor = 'Tasa', EsTraslado = 1, EsRetencion = 1, ValorMinimo = 1.600000, Status = 'A' Where Clave = 'IEPS' and TipoRango = 'Fijo' and ValorMaximo = 1.600000  
If Not Exists ( Select * From CFDI_TasaOCuota Where Clave = 'IEPS' and TipoRango = 'Rango' and ValorMaximo = 43.770000 )  Insert Into CFDI_TasaOCuota (  Clave, Factor, EsTraslado, EsRetencion, TipoRango, ValorMinimo, ValorMaximo, Status )  Values ( 'IEPS', 'Cuota', 1, 1, 'Rango', 0.000000, 43.770000, 'A' ) 
 Else Update CFDI_TasaOCuota Set Factor = 'Cuota', EsTraslado = 1, EsRetencion = 1, ValorMinimo = 0.000000, Status = 'A' Where Clave = 'IEPS' and TipoRango = 'Rango' and ValorMaximo = 43.770000  
If Not Exists ( Select * From CFDI_TasaOCuota Where Clave = 'ISR' and TipoRango = 'Rango' and ValorMaximo = 0.350000 )  Insert Into CFDI_TasaOCuota (  Clave, Factor, EsTraslado, EsRetencion, TipoRango, ValorMinimo, ValorMaximo, Status )  Values ( 'ISR', 'Tasa', 0, 1, 'Rango', 0.000000, 0.350000, 'A' ) 
 Else Update CFDI_TasaOCuota Set Factor = 'Tasa', EsTraslado = 0, EsRetencion = 1, ValorMinimo = 0.000000, Status = 'A' Where Clave = 'ISR' and TipoRango = 'Rango' and ValorMaximo = 0.350000  
If Not Exists ( Select * From CFDI_TasaOCuota Where Clave = 'IVA' and TipoRango = 'Fijo' and ValorMaximo = 0.000000 )  Insert Into CFDI_TasaOCuota (  Clave, Factor, EsTraslado, EsRetencion, TipoRango, ValorMinimo, ValorMaximo, Status )  Values ( 'IVA', 'Tasa', 1, 0, 'Fijo', 0.000000, 0.000000, 'A' ) 
 Else Update CFDI_TasaOCuota Set Factor = 'Tasa', EsTraslado = 1, EsRetencion = 0, ValorMinimo = 0.000000, Status = 'A' Where Clave = 'IVA' and TipoRango = 'Fijo' and ValorMaximo = 0.000000  
If Not Exists ( Select * From CFDI_TasaOCuota Where Clave = 'IVA' and TipoRango = 'Fijo' and ValorMaximo = 0.160000 )  Insert Into CFDI_TasaOCuota (  Clave, Factor, EsTraslado, EsRetencion, TipoRango, ValorMinimo, ValorMaximo, Status )  Values ( 'IVA', 'Tasa', 1, 0, 'Fijo', 0.160000, 0.160000, 'A' ) 
 Else Update CFDI_TasaOCuota Set Factor = 'Tasa', EsTraslado = 1, EsRetencion = 0, ValorMinimo = 0.160000, Status = 'A' Where Clave = 'IVA' and TipoRango = 'Fijo' and ValorMaximo = 0.160000  
If Not Exists ( Select * From CFDI_TasaOCuota Where Clave = 'IVA' and TipoRango = 'Rango' and ValorMaximo = 0.160000 )  Insert Into CFDI_TasaOCuota (  Clave, Factor, EsTraslado, EsRetencion, TipoRango, ValorMinimo, ValorMaximo, Status )  Values ( 'IVA', 'Tasa', 0, 1, 'Rango', 0.000000, 0.160000, 'A' ) 
 Else Update CFDI_TasaOCuota Set Factor = 'Tasa', EsTraslado = 0, EsRetencion = 1, ValorMinimo = 0.000000, Status = 'A' Where Clave = 'IVA' and TipoRango = 'Rango' and ValorMaximo = 0.160000  
Go--#SQL
 