
	If Exists ( Select * from sysobjects  (NoLock) Where Name = 'Lista__Tablas_Operativas' and xType = 'U' ) Drop Table Lista__Tablas_Operativas
Go--#SQL 


	select * 
	Into Lista__Tablas_Operativas 
	from sysobjects (nolock) 
	Where xtype = 'U' 
		and ( name not like 'cat%' and name not like 'tmp%' and name not like 'temp%' 
			and name not like 'CFG%'  
			and name not like 'COM%'  
			and name not like 'COM_OCEN%'  
			and name not like 'FP_%'  
			and name not like 'INT_ND%'  
			and name not like 'INT_RE%'  
			and name not like 'INT_Rece%'  
			and name not like 'INT_Ro%'  
			and name not like 'NET%'  
			and name not like 'RptAdmon%'  
			and name not like 'Rpt%'  
			and name not like '%PRCS%VLDCN%'  
			and name not like 'ctl%erro%'  
			and name not like 'ALMJ%'  
			and name not like 'ctereg%'  
		) 
	order by name 

Go--#SQL 


	select L.Name, L.crdate, C.NombreTabla 
	from Lista__Tablas_Operativas L   
	left Join CFGC_EnvioDetalles C (NoLock) on ( L.name = C.nombreTabla ) 



