If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_CFDI__Bancos' and xType = 'U' ) 
   Drop Table FACT_CFDI__Bancos 
Go--#SQL 

Create Table FACT_CFDI__Bancos 
( 
	Clave varchar(4) Not Null, 
	RFC varchar(15) Not Null Default '' , 
	NombreCorto varchar(100) Not Null Default '',  -- Unique 
	NombreRazonSocial varchar(500) Not Null Default '' -- Unique 	
) 
Go--#SQL 

Alter Table FACT_CFDI__Bancos Add Constraint PK_FACT_CFDI__Bancos Primary Key ( RFC )
Go--#SQL 

--	sp_generainserts 'FACT_CFDI__Bancos' ,1 



If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_002' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '002', 'RFC_x_002', 'BANAMEX', 'Banco Nacional de México, S.A., Institución de Banca Múltiple, Grupo Financiero Banamex' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '002', NombreCorto = 'BANAMEX', NombreRazonSocial = 'Banco Nacional de México, S.A., Institución de Banca Múltiple, Grupo Financiero Banamex' Where RFC = 'RFC_x_002'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_006' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '006', 'RFC_x_006', 'BANCOMEXT', 'Banco Nacional de Comercio Exterior, Sociedad Nacional de Crédito, Institución de Banca de Desarrollo' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '006', NombreCorto = 'BANCOMEXT', NombreRazonSocial = 'Banco Nacional de Comercio Exterior, Sociedad Nacional de Crédito, Institución de Banca de Desarrollo' Where RFC = 'RFC_x_006'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_009' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '009', 'RFC_x_009', 'BANOBRAS', 'Banco Nacional de Obras y Servicios Públicos, Sociedad Nacional de Crédito, Institución de Banca de Desarrollo' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '009', NombreCorto = 'BANOBRAS', NombreRazonSocial = 'Banco Nacional de Obras y Servicios Públicos, Sociedad Nacional de Crédito, Institución de Banca de Desarrollo' Where RFC = 'RFC_x_009'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_012' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '012', 'RFC_x_012', 'BBVA BANCOMER', 'BBVA Bancomer, S.A., Institución de Banca Múltiple, Grupo Financiero BBVA Bancomer' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '012', NombreCorto = 'BBVA BANCOMER', NombreRazonSocial = 'BBVA Bancomer, S.A., Institución de Banca Múltiple, Grupo Financiero BBVA Bancomer' Where RFC = 'RFC_x_012'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_014' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '014', 'RFC_x_014', 'SANTANDER', 'Banco Santander (México), S.A., Institución de Banca Múltiple, Grupo Financiero Santander' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '014', NombreCorto = 'SANTANDER', NombreRazonSocial = 'Banco Santander (México), S.A., Institución de Banca Múltiple, Grupo Financiero Santander' Where RFC = 'RFC_x_014'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_019' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '019', 'RFC_x_019', 'BANJERCITO', 'Banco Nacional del Ejército, Fuerza Aérea y Armada, Sociedad Nacional de Crédito, Institución de Banca de Desarrollo' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '019', NombreCorto = 'BANJERCITO', NombreRazonSocial = 'Banco Nacional del Ejército, Fuerza Aérea y Armada, Sociedad Nacional de Crédito, Institución de Banca de Desarrollo' Where RFC = 'RFC_x_019'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_020' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '020', 'RFC_x_020', 'HSBC', 'HSBC México, S.A., institución De Banca Múltiple, Grupo Financiero HSBC' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '020', NombreCorto = 'HSBC', NombreRazonSocial = 'HSBC México, S.A., institución De Banca Múltiple, Grupo Financiero HSBC' Where RFC = 'RFC_x_020'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_030' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '030', 'RFC_x_030', 'BAJIO', 'Banco del Bajío, S.A., Institución de Banca Múltiple' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '030', NombreCorto = 'BAJIO', NombreRazonSocial = 'Banco del Bajío, S.A., Institución de Banca Múltiple' Where RFC = 'RFC_x_030'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_032' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '032', 'RFC_x_032', 'IXE', 'IXE Banco, S.A., Institución de Banca Múltiple, IXE Grupo Financiero' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '032', NombreCorto = 'IXE', NombreRazonSocial = 'IXE Banco, S.A., Institución de Banca Múltiple, IXE Grupo Financiero' Where RFC = 'RFC_x_032'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_036' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '036', 'RFC_x_036', 'INBURSA', 'Banco Inbursa, S.A., Institución de Banca Múltiple, Grupo Financiero Inbursa' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '036', NombreCorto = 'INBURSA', NombreRazonSocial = 'Banco Inbursa, S.A., Institución de Banca Múltiple, Grupo Financiero Inbursa' Where RFC = 'RFC_x_036'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_037' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '037', 'RFC_x_037', 'INTERACCIONES', 'Banco Interacciones, S.A., Institución de Banca Múltiple' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '037', NombreCorto = 'INTERACCIONES', NombreRazonSocial = 'Banco Interacciones, S.A., Institución de Banca Múltiple' Where RFC = 'RFC_x_037'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_042' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '042', 'RFC_x_042', 'MIFEL', 'Banca Mifel, S.A., Institución de Banca Múltiple, Grupo Financiero Mifel' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '042', NombreCorto = 'MIFEL', NombreRazonSocial = 'Banca Mifel, S.A., Institución de Banca Múltiple, Grupo Financiero Mifel' Where RFC = 'RFC_x_042'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_044' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '044', 'RFC_x_044', 'SCOTIABANK', 'Scotiabank Inverlat, S.A.' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '044', NombreCorto = 'SCOTIABANK', NombreRazonSocial = 'Scotiabank Inverlat, S.A.' Where RFC = 'RFC_x_044'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_058' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '058', 'RFC_x_058', 'BANREGIO', 'Banco Regional de Monterrey, S.A., Institución de Banca Múltiple, Banregio Grupo Financiero' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '058', NombreCorto = 'BANREGIO', NombreRazonSocial = 'Banco Regional de Monterrey, S.A., Institución de Banca Múltiple, Banregio Grupo Financiero' Where RFC = 'RFC_x_058'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_059' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '059', 'RFC_x_059', 'INVEX', 'Banco Invex, S.A., Institución de Banca Múltiple, Invex Grupo Financiero' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '059', NombreCorto = 'INVEX', NombreRazonSocial = 'Banco Invex, S.A., Institución de Banca Múltiple, Invex Grupo Financiero' Where RFC = 'RFC_x_059'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_060' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '060', 'RFC_x_060', 'BANSI', 'Bansi, S.A., Institución de Banca Múltiple' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '060', NombreCorto = 'BANSI', NombreRazonSocial = 'Bansi, S.A., Institución de Banca Múltiple' Where RFC = 'RFC_x_060'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_062' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '062', 'RFC_x_062', 'AFIRME', 'Banca Afirme, S.A., Institución de Banca Múltiple' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '062', NombreCorto = 'AFIRME', NombreRazonSocial = 'Banca Afirme, S.A., Institución de Banca Múltiple' Where RFC = 'RFC_x_062'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_072' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '072', 'RFC_x_072', 'BANORTE', 'Banco Mercantil del Norte, S.A., Institución de Banca Múltiple, Grupo Financiero Banorte' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '072', NombreCorto = 'BANORTE', NombreRazonSocial = 'Banco Mercantil del Norte, S.A., Institución de Banca Múltiple, Grupo Financiero Banorte' Where RFC = 'RFC_x_072'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_102' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '102', 'RFC_x_102', 'THE ROYAL BANK', 'The Royal Bank of Scotland México, S.A., Institución de Banca Múltiple' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '102', NombreCorto = 'THE ROYAL BANK', NombreRazonSocial = 'The Royal Bank of Scotland México, S.A., Institución de Banca Múltiple' Where RFC = 'RFC_x_102'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_103' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '103', 'RFC_x_103', 'AMERICAN EXPRESS', 'American Express Bank (México), S.A., Institución de Banca Múltiple' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '103', NombreCorto = 'AMERICAN EXPRESS', NombreRazonSocial = 'American Express Bank (México), S.A., Institución de Banca Múltiple' Where RFC = 'RFC_x_103'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_106' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '106', 'RFC_x_106', 'BAMSA', 'Bank of America México, S.A., Institución de Banca Múltiple, Grupo Financiero Bank of America' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '106', NombreCorto = 'BAMSA', NombreRazonSocial = 'Bank of America México, S.A., Institución de Banca Múltiple, Grupo Financiero Bank of America' Where RFC = 'RFC_x_106'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_108' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '108', 'RFC_x_108', 'TOKYO', 'Bank of Tokyo-Mitsubishi UFJ (México), S.A.' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '108', NombreCorto = 'TOKYO', NombreRazonSocial = 'Bank of Tokyo-Mitsubishi UFJ (México), S.A.' Where RFC = 'RFC_x_108'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_110' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '110', 'RFC_x_110', 'JP MORGAN', 'Banco J.P. Morgan, S.A., Institución de Banca Múltiple, J.P. Morgan Grupo Financiero' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '110', NombreCorto = 'JP MORGAN', NombreRazonSocial = 'Banco J.P. Morgan, S.A., Institución de Banca Múltiple, J.P. Morgan Grupo Financiero' Where RFC = 'RFC_x_110'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_112' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '112', 'RFC_x_112', 'BMONEX', 'Banco Monex, S.A., Institución de Banca Múltiple' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '112', NombreCorto = 'BMONEX', NombreRazonSocial = 'Banco Monex, S.A., Institución de Banca Múltiple' Where RFC = 'RFC_x_112'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_113' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '113', 'RFC_x_113', 'VE POR MAS', 'Banco Ve Por Mas, S.A. Institución de Banca Múltiple' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '113', NombreCorto = 'VE POR MAS', NombreRazonSocial = 'Banco Ve Por Mas, S.A. Institución de Banca Múltiple' Where RFC = 'RFC_x_113'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_116' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '116', 'RFC_x_116', 'ING', 'ING Bank (México), S.A., Institución de Banca Múltiple, ING Grupo Financiero' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '116', NombreCorto = 'ING', NombreRazonSocial = 'ING Bank (México), S.A., Institución de Banca Múltiple, ING Grupo Financiero' Where RFC = 'RFC_x_116'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_124' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '124', 'RFC_x_124', 'DEUTSCHE', 'Deutsche Bank México, S.A., Institución de Banca Múltiple' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '124', NombreCorto = 'DEUTSCHE', NombreRazonSocial = 'Deutsche Bank México, S.A., Institución de Banca Múltiple' Where RFC = 'RFC_x_124'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_126' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '126', 'RFC_x_126', 'CREDIT SUISSE', 'Banco Credit Suisse (México), S.A. Institución de Banca Múltiple, Grupo Financiero Credit Suisse (México)' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '126', NombreCorto = 'CREDIT SUISSE', NombreRazonSocial = 'Banco Credit Suisse (México), S.A. Institución de Banca Múltiple, Grupo Financiero Credit Suisse (México)' Where RFC = 'RFC_x_126'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_127' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '127', 'RFC_x_127', 'AZTECA', 'Banco Azteca, S.A. Institución de Banca Múltiple' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '127', NombreCorto = 'AZTECA', NombreRazonSocial = 'Banco Azteca, S.A. Institución de Banca Múltiple' Where RFC = 'RFC_x_127'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_128' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '128', 'RFC_x_128', 'AUTOFIN', 'Banco Autofin México, S.A. Institución de Banca Múltiple' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '128', NombreCorto = 'AUTOFIN', NombreRazonSocial = 'Banco Autofin México, S.A. Institución de Banca Múltiple' Where RFC = 'RFC_x_128'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_129' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '129', 'RFC_x_129', 'BARCLAYS', 'Barclays Bank México, S.A., Institución de Banca Múltiple, Grupo Financiero Barclays México' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '129', NombreCorto = 'BARCLAYS', NombreRazonSocial = 'Barclays Bank México, S.A., Institución de Banca Múltiple, Grupo Financiero Barclays México' Where RFC = 'RFC_x_129'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_130' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '130', 'RFC_x_130', 'COMPARTAMOS', 'Banco Compartamos, S.A., Institución de Banca Múltiple' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '130', NombreCorto = 'COMPARTAMOS', NombreRazonSocial = 'Banco Compartamos, S.A., Institución de Banca Múltiple' Where RFC = 'RFC_x_130'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_131' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '131', 'RFC_x_131', 'BANCO FAMSA', 'Banco Ahorro Famsa, S.A., Institución de Banca Múltiple' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '131', NombreCorto = 'BANCO FAMSA', NombreRazonSocial = 'Banco Ahorro Famsa, S.A., Institución de Banca Múltiple' Where RFC = 'RFC_x_131'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_132' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '132', 'RFC_x_132', 'BMULTIVA', 'Banco Multiva, S.A., Institución de Banca Múltiple, Multivalores Grupo Financiero' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '132', NombreCorto = 'BMULTIVA', NombreRazonSocial = 'Banco Multiva, S.A., Institución de Banca Múltiple, Multivalores Grupo Financiero' Where RFC = 'RFC_x_132'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_133' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '133', 'RFC_x_133', 'ACTINVER', 'Banco Actinver, S.A. Institución de Banca Múltiple, Grupo Financiero Actinver' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '133', NombreCorto = 'ACTINVER', NombreRazonSocial = 'Banco Actinver, S.A. Institución de Banca Múltiple, Grupo Financiero Actinver' Where RFC = 'RFC_x_133'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_134' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '134', 'RFC_x_134', 'WAL-MART', 'Banco Wal-Mart de México Adelante, S.A., Institución de Banca Múltiple' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '134', NombreCorto = 'WAL-MART', NombreRazonSocial = 'Banco Wal-Mart de México Adelante, S.A., Institución de Banca Múltiple' Where RFC = 'RFC_x_134'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_135' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '135', 'RFC_x_135', 'NAFIN', 'Nacional Financiera, Sociedad Nacional de Crédito, Institución de Banca de Desarrollo' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '135', NombreCorto = 'NAFIN', NombreRazonSocial = 'Nacional Financiera, Sociedad Nacional de Crédito, Institución de Banca de Desarrollo' Where RFC = 'RFC_x_135'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_136' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '136', 'RFC_x_136', 'INTERBANCO', 'Inter Banco, S.A. Institución de Banca Múltiple' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '136', NombreCorto = 'INTERBANCO', NombreRazonSocial = 'Inter Banco, S.A. Institución de Banca Múltiple' Where RFC = 'RFC_x_136'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_137' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '137', 'RFC_x_137', 'BANCOPPEL', 'BanCoppel, S.A., Institución de Banca Múltiple' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '137', NombreCorto = 'BANCOPPEL', NombreRazonSocial = 'BanCoppel, S.A., Institución de Banca Múltiple' Where RFC = 'RFC_x_137'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_138' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '138', 'RFC_x_138', 'ABC CAPITAL', 'ABC Capital, S.A., Institución de Banca Múltiple' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '138', NombreCorto = 'ABC CAPITAL', NombreRazonSocial = 'ABC Capital, S.A., Institución de Banca Múltiple' Where RFC = 'RFC_x_138'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_139' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '139', 'RFC_x_139', 'UBS BANK', 'UBS Bank México, S.A., Institución de Banca Múltiple, UBS Grupo Financiero' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '139', NombreCorto = 'UBS BANK', NombreRazonSocial = 'UBS Bank México, S.A., Institución de Banca Múltiple, UBS Grupo Financiero' Where RFC = 'RFC_x_139'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_140' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '140', 'RFC_x_140', 'CONSUBANCO', 'Consubanco, S.A. Institución de Banca Múltiple' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '140', NombreCorto = 'CONSUBANCO', NombreRazonSocial = 'Consubanco, S.A. Institución de Banca Múltiple' Where RFC = 'RFC_x_140'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_141' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '141', 'RFC_x_141', 'VOLKSWAGEN', 'Volkswagen Bank, S.A., Institución de Banca Múltiple' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '141', NombreCorto = 'VOLKSWAGEN', NombreRazonSocial = 'Volkswagen Bank, S.A., Institución de Banca Múltiple' Where RFC = 'RFC_x_141'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_143' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '143', 'RFC_x_143', 'CIBANCO', 'CIBanco, S.A.' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '143', NombreCorto = 'CIBANCO', NombreRazonSocial = 'CIBanco, S.A.' Where RFC = 'RFC_x_143'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_145' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '145', 'RFC_x_145', 'BBASE', 'Banco Base, S.A., Institución de Banca Múltiple' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '145', NombreCorto = 'BBASE', NombreRazonSocial = 'Banco Base, S.A., Institución de Banca Múltiple' Where RFC = 'RFC_x_145'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_166' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '166', 'RFC_x_166', 'BANSEFI', 'Banco del Ahorro Nacional y Servicios Financieros, Sociedad Nacional de Crédito, Institución de Banca de Desarrollo' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '166', NombreCorto = 'BANSEFI', NombreRazonSocial = 'Banco del Ahorro Nacional y Servicios Financieros, Sociedad Nacional de Crédito, Institución de Banca de Desarrollo' Where RFC = 'RFC_x_166'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_168' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '168', 'RFC_x_168', 'HIPOTECARIA FEDERAL', 'Sociedad Hipotecaria Federal, Sociedad Nacional de Crédito, Institución de Banca de Desarrollo' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '168', NombreCorto = 'HIPOTECARIA FEDERAL', NombreRazonSocial = 'Sociedad Hipotecaria Federal, Sociedad Nacional de Crédito, Institución de Banca de Desarrollo' Where RFC = 'RFC_x_168'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_600' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '600', 'RFC_x_600', 'MONEXCB', 'Monex Casa de Bolsa, S.A. de C.V. Monex Grupo Financiero' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '600', NombreCorto = 'MONEXCB', NombreRazonSocial = 'Monex Casa de Bolsa, S.A. de C.V. Monex Grupo Financiero' Where RFC = 'RFC_x_600'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_601' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '601', 'RFC_x_601', 'GBM', 'GBM Grupo Bursátil Mexicano, S.A. de C.V. Casa de Bolsa' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '601', NombreCorto = 'GBM', NombreRazonSocial = 'GBM Grupo Bursátil Mexicano, S.A. de C.V. Casa de Bolsa' Where RFC = 'RFC_x_601'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_602' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '602', 'RFC_x_602', 'MASARI', 'Masari Casa de Bolsa, S.A.' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '602', NombreCorto = 'MASARI', NombreRazonSocial = 'Masari Casa de Bolsa, S.A.' Where RFC = 'RFC_x_602'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_605' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '605', 'RFC_x_605', 'VALUE', 'Value, S.A. de C.V. Casa de Bolsa' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '605', NombreCorto = 'VALUE', NombreRazonSocial = 'Value, S.A. de C.V. Casa de Bolsa' Where RFC = 'RFC_x_605'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_606' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '606', 'RFC_x_606', 'ESTRUCTURADORES', 'Estructuradores del Mercado de Valores Casa de Bolsa, S.A. de C.V.' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '606', NombreCorto = 'ESTRUCTURADORES', NombreRazonSocial = 'Estructuradores del Mercado de Valores Casa de Bolsa, S.A. de C.V.' Where RFC = 'RFC_x_606'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_607' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '607', 'RFC_x_607', 'TIBER', 'Casa de Cambio Tiber, S.A. de C.V.' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '607', NombreCorto = 'TIBER', NombreRazonSocial = 'Casa de Cambio Tiber, S.A. de C.V.' Where RFC = 'RFC_x_607'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_608' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '608', 'RFC_x_608', 'VECTOR', 'Vector Casa de Bolsa, S.A. de C.V.' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '608', NombreCorto = 'VECTOR', NombreRazonSocial = 'Vector Casa de Bolsa, S.A. de C.V.' Where RFC = 'RFC_x_608'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_610' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '610', 'RFC_x_610', 'B&B', 'B y B, Casa de Cambio, S.A. de C.V.' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '610', NombreCorto = 'B&B', NombreRazonSocial = 'B y B, Casa de Cambio, S.A. de C.V.' Where RFC = 'RFC_x_610'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_614' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '614', 'RFC_x_614', 'ACCIVAL', 'Acciones y Valores Banamex, S.A. de C.V., Casa de Bolsa' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '614', NombreCorto = 'ACCIVAL', NombreRazonSocial = 'Acciones y Valores Banamex, S.A. de C.V., Casa de Bolsa' Where RFC = 'RFC_x_614'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_615' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '615', 'RFC_x_615', 'MERRILL LYNCH', 'Merrill Lynch México, S.A. de C.V. Casa de Bolsa' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '615', NombreCorto = 'MERRILL LYNCH', NombreRazonSocial = 'Merrill Lynch México, S.A. de C.V. Casa de Bolsa' Where RFC = 'RFC_x_615'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_616' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '616', 'RFC_x_616', 'FINAMEX', 'Casa de Bolsa Finamex, S.A. de C.V.' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '616', NombreCorto = 'FINAMEX', NombreRazonSocial = 'Casa de Bolsa Finamex, S.A. de C.V.' Where RFC = 'RFC_x_616'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_617' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '617', 'RFC_x_617', 'VALMEX', 'Valores Mexicanos Casa de Bolsa, S.A. de C.V.' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '617', NombreCorto = 'VALMEX', NombreRazonSocial = 'Valores Mexicanos Casa de Bolsa, S.A. de C.V.' Where RFC = 'RFC_x_617'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_618' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '618', 'RFC_x_618', 'UNICA', 'Unica Casa de Cambio, S.A. de C.V.' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '618', NombreCorto = 'UNICA', NombreRazonSocial = 'Unica Casa de Cambio, S.A. de C.V.' Where RFC = 'RFC_x_618'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_619' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '619', 'RFC_x_619', 'MAPFRE', 'MAPFRE Tepeyac, S.A.' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '619', NombreCorto = 'MAPFRE', NombreRazonSocial = 'MAPFRE Tepeyac, S.A.' Where RFC = 'RFC_x_619'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_620' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '620', 'RFC_x_620', 'PROFUTURO', 'Profuturo G.N.P., S.A. de C.V., Afore' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '620', NombreCorto = 'PROFUTURO', NombreRazonSocial = 'Profuturo G.N.P., S.A. de C.V., Afore' Where RFC = 'RFC_x_620'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_621' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '621', 'RFC_x_621', 'CB ACTINVER', 'Actinver Casa de Bolsa, S.A. de C.V.' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '621', NombreCorto = 'CB ACTINVER', NombreRazonSocial = 'Actinver Casa de Bolsa, S.A. de C.V.' Where RFC = 'RFC_x_621'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_622' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '622', 'RFC_x_622', 'OACTIN', 'OPERADORA ACTINVER, S.A. DE C.V.' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '622', NombreCorto = 'OACTIN', NombreRazonSocial = 'OPERADORA ACTINVER, S.A. DE C.V.' Where RFC = 'RFC_x_622'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_623' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '623', 'RFC_x_623', 'SKANDIA', 'Skandia Vida, S.A. de C.V.' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '623', NombreCorto = 'SKANDIA', NombreRazonSocial = 'Skandia Vida, S.A. de C.V.' Where RFC = 'RFC_x_623'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_626' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '626', 'RFC_x_626', 'CBDEUTSCHE', 'Deutsche Securities, S.A. de C.V. CASA DE BOLSA' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '626', NombreCorto = 'CBDEUTSCHE', NombreRazonSocial = 'Deutsche Securities, S.A. de C.V. CASA DE BOLSA' Where RFC = 'RFC_x_626'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_627' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '627', 'RFC_x_627', 'ZURICH', 'Zurich Compañía de Seguros, S.A.' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '627', NombreCorto = 'ZURICH', NombreRazonSocial = 'Zurich Compañía de Seguros, S.A.' Where RFC = 'RFC_x_627'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_628' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '628', 'RFC_x_628', 'ZURICHVI', 'Zurich Vida, Compañía de Seguros, S.A.' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '628', NombreCorto = 'ZURICHVI', NombreRazonSocial = 'Zurich Vida, Compañía de Seguros, S.A.' Where RFC = 'RFC_x_628'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_629' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '629', 'RFC_x_629', 'SU CASITA', 'Hipotecaria Su Casita, S.A. de C.V. SOFOM ENR' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '629', NombreCorto = 'SU CASITA', NombreRazonSocial = 'Hipotecaria Su Casita, S.A. de C.V. SOFOM ENR' Where RFC = 'RFC_x_629'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_630' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '630', 'RFC_x_630', 'CB INTERCAM', 'Intercam Casa de Bolsa, S.A. de C.V.' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '630', NombreCorto = 'CB INTERCAM', NombreRazonSocial = 'Intercam Casa de Bolsa, S.A. de C.V.' Where RFC = 'RFC_x_630'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_631' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '631', 'RFC_x_631', 'CI BOLSA', 'CI Casa de Bolsa, S.A. de C.V.' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '631', NombreCorto = 'CI BOLSA', NombreRazonSocial = 'CI Casa de Bolsa, S.A. de C.V.' Where RFC = 'RFC_x_631'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_632' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '632', 'RFC_x_632', 'BULLTICK CB', 'Bulltick Casa de Bolsa, S.A., de C.V.' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '632', NombreCorto = 'BULLTICK CB', NombreRazonSocial = 'Bulltick Casa de Bolsa, S.A., de C.V.' Where RFC = 'RFC_x_632'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_633' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '633', 'RFC_x_633', 'STERLING', 'Sterling Casa de Cambio, S.A. de C.V.' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '633', NombreCorto = 'STERLING', NombreRazonSocial = 'Sterling Casa de Cambio, S.A. de C.V.' Where RFC = 'RFC_x_633'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_634' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '634', 'RFC_x_634', 'FINCOMUN', 'Sterling Casa de Cambio, S.A. de C.V.' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '634', NombreCorto = 'FINCOMUN', NombreRazonSocial = 'Sterling Casa de Cambio, S.A. de C.V.' Where RFC = 'RFC_x_634'  
If Not Exists ( Select * From FACT_CFDI__Bancos Where RFC = 'RFC_x_636' )  Insert Into FACT_CFDI__Bancos (  Clave, RFC, NombreCorto, NombreRazonSocial )  Values ( '636', 'RFC_x_636', 'HDI SEGUROS', 'HDI Seguros, S.A. de C.V.' ) 
 Else Update FACT_CFDI__Bancos Set Clave = '636', NombreCorto = 'HDI SEGUROS', NombreRazonSocial = 'HDI Seguros, S.A. de C.V.' Where RFC = 'RFC_x_636'  
Go--#SQL 
