
-------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'CFDI_TiposDeMoneda' and xType = 'U' ) 
	Drop Table CFDI_TiposDeMoneda  
Go--#SQL  

Create Table CFDI_TiposDeMoneda 
(	
	Clave varchar(4) Not Null Default '', 
	Descripcion varchar(100) Not Null Default '', -- Unique, 
	Decimales int Not Null Default 2, 
	PorcentajeVariacion numeric(14,4) Not Null Default 0, 
	Status varchar(1) Not Null Default 'A' 
) 
Go--#SQL 

Alter Table CFDI_TiposDeMoneda Add Constraint PK_CFDI_TiposDeMoneda Primary	Key ( Clave ) 
Go--#SQL 


If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'AED' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'AED', 'Dirham de EAU', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dirham de EAU', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'AED'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'AFN' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'AFN', 'Afghani', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Afghani', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'AFN'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'ALL' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'ALL', 'Lek', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Lek', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'ALL'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'AMD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'AMD', 'Dram armenio', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dram armenio', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'AMD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'ANG' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'ANG', 'Florín antillano neerlandés', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Florín antillano neerlandés', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'ANG'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'AOA' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'AOA', 'Kwanza', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Kwanza', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'AOA'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'ARS' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'ARS', 'Peso Argentino', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Peso Argentino', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'ARS'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'AUD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'AUD', 'Dólar Australiano', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dólar Australiano', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'AUD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'AWG' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'AWG', 'Aruba Florin', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Aruba Florin', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'AWG'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'AZN' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'AZN', 'Azerbaijanian Manat', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Azerbaijanian Manat', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'AZN'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'BAM' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'BAM', 'Convertibles marca', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Convertibles marca', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'BAM'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'BBD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'BBD', 'Dólar de Barbados', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dólar de Barbados', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'BBD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'BDT' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'BDT', 'Taka', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Taka', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'BDT'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'BGN' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'BGN', 'Lev búlgaro', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Lev búlgaro', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'BGN'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'BHD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'BHD', 'Dinar de Bahrein', 3, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dinar de Bahrein', Decimales = 3, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'BHD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'BIF' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'BIF', 'Burundi Franc', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Burundi Franc', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'BIF'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'BMD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'BMD', 'Dólar de Bermudas', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dólar de Bermudas', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'BMD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'BND' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'BND', 'Dólar de Brunei', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dólar de Brunei', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'BND'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'BOB' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'BOB', 'Boliviano', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Boliviano', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'BOB'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'BOV' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'BOV', 'Mvdol', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Mvdol', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'BOV'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'BRL' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'BRL', 'Real brasileño', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Real brasileño', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'BRL'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'BSD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'BSD', 'Dólar de las Bahamas', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dólar de las Bahamas', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'BSD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'BTN' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'BTN', 'Ngultrum', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Ngultrum', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'BTN'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'BWP' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'BWP', 'Pula', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Pula', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'BWP'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'BYR' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'BYR', 'Rublo bielorruso', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Rublo bielorruso', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'BYR'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'BZD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'BZD', 'Dólar de Belice', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dólar de Belice', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'BZD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'CAD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'CAD', 'Dolar Canadiense', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dolar Canadiense', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'CAD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'CDF' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'CDF', 'Franco congoleño', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Franco congoleño', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'CDF'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'CHE' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'CHE', 'WIR Euro', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'WIR Euro', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'CHE'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'CHF' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'CHF', 'Franco Suizo', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Franco Suizo', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'CHF'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'CHW' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'CHW', 'Franc WIR', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Franc WIR', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'CHW'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'CLF' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'CLF', 'Unidad de Fomento', 4, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Unidad de Fomento', Decimales = 4, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'CLF'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'CLP' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'CLP', 'Peso chileno', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Peso chileno', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'CLP'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'CNY' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'CNY', 'Yuan Renminbi', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Yuan Renminbi', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'CNY'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'COP' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'COP', 'Peso Colombiano', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Peso Colombiano', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'COP'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'COU' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'COU', 'Unidad de Valor real', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Unidad de Valor real', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'COU'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'CRC' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'CRC', 'Colón costarricense', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Colón costarricense', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'CRC'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'CUC' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'CUC', 'Peso Convertible', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Peso Convertible', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'CUC'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'CUP' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'CUP', 'Peso Cubano', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Peso Cubano', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'CUP'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'CVE' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'CVE', 'Cabo Verde Escudo', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Cabo Verde Escudo', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'CVE'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'CZK' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'CZK', 'Corona checa', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Corona checa', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'CZK'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'DJF' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'DJF', 'Franco de Djibouti', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Franco de Djibouti', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'DJF'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'DKK' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'DKK', 'Corona danesa', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Corona danesa', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'DKK'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'DOP' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'DOP', 'Peso Dominicano', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Peso Dominicano', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'DOP'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'DZD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'DZD', 'Dinar argelino', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dinar argelino', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'DZD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'EGP' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'EGP', 'Libra egipcia', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Libra egipcia', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'EGP'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'ERN' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'ERN', 'Nakfa', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Nakfa', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'ERN'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'ETB' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'ETB', 'Birr etíope', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Birr etíope', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'ETB'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'EUR' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'EUR', 'Euro', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Euro', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'EUR'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'FJD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'FJD', 'Dólar de Fiji', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dólar de Fiji', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'FJD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'FKP' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'FKP', 'Libra malvinense', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Libra malvinense', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'FKP'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'GBP' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'GBP', 'Libra Esterlina', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Libra Esterlina', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'GBP'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'GEL' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'GEL', 'Lari', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Lari', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'GEL'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'GHS' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'GHS', 'Cedi de Ghana', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Cedi de Ghana', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'GHS'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'GIP' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'GIP', 'Libra de Gibraltar', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Libra de Gibraltar', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'GIP'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'GMD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'GMD', 'Dalasi', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dalasi', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'GMD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'GNF' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'GNF', 'Franco guineano', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Franco guineano', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'GNF'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'GTQ' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'GTQ', 'Quetzal', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Quetzal', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'GTQ'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'GYD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'GYD', 'Dólar guyanés', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dólar guyanés', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'GYD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'HKD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'HKD', 'Dolar De Hong Kong', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dolar De Hong Kong', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'HKD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'HNL' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'HNL', 'Lempira', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Lempira', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'HNL'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'HRK' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'HRK', 'Kuna', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Kuna', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'HRK'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'HTG' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'HTG', 'Gourde', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Gourde', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'HTG'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'HUF' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'HUF', 'Florín', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Florín', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'HUF'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'IDR' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'IDR', 'Rupia', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Rupia', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'IDR'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'ILS' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'ILS', 'Nuevo Shekel Israelí', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Nuevo Shekel Israelí', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'ILS'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'INR' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'INR', 'Rupia india', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Rupia india', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'INR'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'IQD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'IQD', 'Dinar iraquí', 3, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dinar iraquí', Decimales = 3, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'IQD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'IRR' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'IRR', 'Rial iraní', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Rial iraní', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'IRR'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'ISK' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'ISK', 'Corona islandesa', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Corona islandesa', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'ISK'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'JMD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'JMD', 'Dólar Jamaiquino', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dólar Jamaiquino', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'JMD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'JOD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'JOD', 'Dinar jordano', 3, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dinar jordano', Decimales = 3, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'JOD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'JPY' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'JPY', 'Yen', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Yen', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'JPY'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'KES' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'KES', 'Chelín keniano', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Chelín keniano', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'KES'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'KGS' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'KGS', 'Som', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Som', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'KGS'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'KHR' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'KHR', 'Riel', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Riel', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'KHR'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'KMF' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'KMF', 'Franco Comoro', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Franco Comoro', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'KMF'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'KPW' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'KPW', 'Corea del Norte ganó', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Corea del Norte ganó', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'KPW'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'KRW' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'KRW', 'Won', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Won', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'KRW'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'KWD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'KWD', 'Dinar kuwaití', 3, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dinar kuwaití', Decimales = 3, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'KWD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'KYD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'KYD', 'Dólar de las Islas Caimán', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dólar de las Islas Caimán', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'KYD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'KZT' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'KZT', 'Tenge', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Tenge', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'KZT'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'LAK' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'LAK', 'Kip', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Kip', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'LAK'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'LBP' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'LBP', 'Libra libanesa', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Libra libanesa', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'LBP'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'LKR' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'LKR', 'Rupia de Sri Lanka', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Rupia de Sri Lanka', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'LKR'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'LRD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'LRD', 'Dólar liberiano', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dólar liberiano', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'LRD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'LSL' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'LSL', 'Loti', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Loti', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'LSL'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'LYD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'LYD', 'Dinar libio', 3, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dinar libio', Decimales = 3, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'LYD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'MAD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'MAD', 'Dirham marroquí', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dirham marroquí', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'MAD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'MDL' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'MDL', 'Leu moldavo', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Leu moldavo', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'MDL'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'MGA' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'MGA', 'Ariary malgache', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Ariary malgache', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'MGA'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'MKD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'MKD', 'Denar', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Denar', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'MKD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'MMK' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'MMK', 'Kyat', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Kyat', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'MMK'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'MNT' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'MNT', 'Tugrik', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Tugrik', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'MNT'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'MOP' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'MOP', 'Pataca', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Pataca', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'MOP'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'MRO' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'MRO', 'Ouguiya', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Ouguiya', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'MRO'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'MUR' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'MUR', 'Rupia de Mauricio', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Rupia de Mauricio', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'MUR'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'MVR' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'MVR', 'Rupia', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Rupia', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'MVR'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'MWK' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'MWK', 'Kwacha', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Kwacha', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'MWK'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'MXN' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'MXN', 'Peso Mexicano', 2, 500.0000, 'A' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Peso Mexicano', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'A' Where Clave = 'MXN'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'MXV' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'MXV', 'México Unidad de Inversión (UDI)', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'México Unidad de Inversión (UDI)', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'MXV'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'MYR' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'MYR', 'Ringgit malayo', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Ringgit malayo', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'MYR'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'MZN' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'MZN', 'Mozambique Metical', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Mozambique Metical', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'MZN'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'NAD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'NAD', 'Dólar de Namibia', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dólar de Namibia', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'NAD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'NGN' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'NGN', 'Naira', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Naira', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'NGN'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'NIO' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'NIO', 'Córdoba Oro', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Córdoba Oro', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'NIO'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'NOK' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'NOK', 'Corona noruega', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Corona noruega', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'NOK'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'NPR' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'NPR', 'Rupia nepalí', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Rupia nepalí', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'NPR'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'NZD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'NZD', 'Dólar de Nueva Zelanda', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dólar de Nueva Zelanda', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'NZD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'OMR' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'OMR', 'Rial omaní', 3, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Rial omaní', Decimales = 3, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'OMR'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'PAB' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'PAB', 'Balboa', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Balboa', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'PAB'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'PEN' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'PEN', 'Nuevo Sol', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Nuevo Sol', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'PEN'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'PGK' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'PGK', 'Kina', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Kina', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'PGK'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'PHP' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'PHP', 'Peso filipino', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Peso filipino', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'PHP'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'PKR' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'PKR', 'Rupia de Pakistán', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Rupia de Pakistán', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'PKR'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'PLN' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'PLN', 'Zloty', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Zloty', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'PLN'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'PYG' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'PYG', 'Guaraní', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Guaraní', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'PYG'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'QAR' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'QAR', 'Qatar Rial', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Qatar Rial', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'QAR'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'RON' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'RON', 'Leu rumano', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Leu rumano', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'RON'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'RSD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'RSD', 'Dinar serbio', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dinar serbio', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'RSD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'RUB' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'RUB', 'Rublo ruso', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Rublo ruso', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'RUB'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'RWF' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'RWF', 'Franco ruandés', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Franco ruandés', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'RWF'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'SAR' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'SAR', 'Riyal saudí', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Riyal saudí', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'SAR'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'SBD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'SBD', 'Dólar de las Islas Salomón', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dólar de las Islas Salomón', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'SBD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'SCR' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'SCR', 'Rupia de Seychelles', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Rupia de Seychelles', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'SCR'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'SDG' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'SDG', 'Libra sudanesa', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Libra sudanesa', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'SDG'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'SEK' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'SEK', 'Corona sueca', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Corona sueca', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'SEK'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'SGD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'SGD', 'Dolar De Singapur', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dolar De Singapur', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'SGD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'SHP' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'SHP', 'Libra de Santa Helena', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Libra de Santa Helena', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'SHP'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'SLL' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'SLL', 'Leona', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Leona', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'SLL'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'SOS' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'SOS', 'Chelín somalí', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Chelín somalí', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'SOS'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'SRD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'SRD', 'Dólar de Suriname', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dólar de Suriname', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'SRD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'SSP' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'SSP', 'Libra sudanesa Sur', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Libra sudanesa Sur', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'SSP'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'STD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'STD', 'Dobra', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dobra', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'STD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'SVC' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'SVC', 'Colon El Salvador', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Colon El Salvador', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'SVC'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'SYP' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'SYP', 'Libra Siria', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Libra Siria', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'SYP'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'SZL' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'SZL', 'Lilangeni', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Lilangeni', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'SZL'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'THB' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'THB', 'Baht', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Baht', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'THB'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'TJS' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'TJS', 'Somoni', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Somoni', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'TJS'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'TMT' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'TMT', 'Turkmenistán nuevo manat', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Turkmenistán nuevo manat', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'TMT'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'TND' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'TND', 'Dinar tunecino', 3, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dinar tunecino', Decimales = 3, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'TND'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'TOP' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'TOP', 'Paanga', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Paanga', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'TOP'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'TRY' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'TRY', 'Lira turca', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Lira turca', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'TRY'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'TTD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'TTD', 'Dólar de Trinidad y Tobago', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dólar de Trinidad y Tobago', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'TTD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'TWD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'TWD', 'Nuevo dólar de Taiwán', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Nuevo dólar de Taiwán', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'TWD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'TZS' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'TZS', 'Shilling tanzano', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Shilling tanzano', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'TZS'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'UAH' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'UAH', 'Hryvnia', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Hryvnia', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'UAH'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'UGX' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'UGX', 'Shilling de Uganda', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Shilling de Uganda', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'UGX'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'USD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'USD', 'Dolar americano', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dolar americano', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'USD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'USN' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'USN', 'Dólar estadounidense (día siguiente)', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dólar estadounidense (día siguiente)', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'USN'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'UYI' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'UYI', 'Peso Uruguay en Unidades Indexadas (URUIURUI)', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Peso Uruguay en Unidades Indexadas (URUIURUI)', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'UYI'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'UYU' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'UYU', 'Peso Uruguayo', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Peso Uruguayo', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'UYU'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'UZS' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'UZS', 'Uzbekistán Sum', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Uzbekistán Sum', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'UZS'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'VEF' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'VEF', 'Bolívar', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Bolívar', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'VEF'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'VND' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'VND', 'Dong', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dong', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'VND'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'VUV' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'VUV', 'Vatu', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Vatu', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'VUV'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'WST' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'WST', 'Tala', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Tala', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'WST'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'XAF' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'XAF', 'Franco CFA BEAC', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Franco CFA BEAC', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'XAF'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'XAG' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'XAG', 'Plata', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Plata', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'XAG'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'XAU' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'XAU', 'Oro', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Oro', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'XAU'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'XBA' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'XBA', 'Unidad de Mercados de Bonos Unidad Europea Composite (EURCO)', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Unidad de Mercados de Bonos Unidad Europea Composite (EURCO)', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'XBA'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'XBB' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'XBB', 'Unidad Monetaria de Bonos de Mercados Unidad Europea (UEM-6)', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Unidad Monetaria de Bonos de Mercados Unidad Europea (UEM-6)', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'XBB'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'XBC' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'XBC', 'Mercados de Bonos Unidad Europea unidad de cuenta a 9 (UCE-9)', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Mercados de Bonos Unidad Europea unidad de cuenta a 9 (UCE-9)', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'XBC'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'XBD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'XBD', 'Mercados de Bonos Unidad Europea unidad de cuenta a 17 (UCE-17)', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Mercados de Bonos Unidad Europea unidad de cuenta a 17 (UCE-17)', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'XBD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'XCD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'XCD', 'Dólar del Caribe Oriental', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Dólar del Caribe Oriental', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'XCD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'XDR' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'XDR', 'DEG (Derechos Especiales de Giro)', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'DEG (Derechos Especiales de Giro)', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'XDR'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'XOF' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'XOF', 'Franco CFA BCEAO', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Franco CFA BCEAO', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'XOF'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'XPD' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'XPD', 'Paladio', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Paladio', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'XPD'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'XPF' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'XPF', 'Franco CFP', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Franco CFP', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'XPF'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'XPT' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'XPT', 'Platino', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Platino', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'XPT'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'XSU' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'XSU', 'Sucre', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Sucre', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'XSU'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'XTS' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'XTS', 'Códigos reservados específicamente para propósitos de prueba', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Códigos reservados específicamente para propósitos de prueba', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'XTS'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'XUA' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'XUA', 'Unidad ADB de Cuenta', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Unidad ADB de Cuenta', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'XUA'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'XXX' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'XXX', 'Los códigos asignados para las transacciones en que intervenga ninguna moneda', 0, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Los códigos asignados para las transacciones en que intervenga ninguna moneda', Decimales = 0, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'XXX'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'YER' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'YER', 'Rial yemení', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Rial yemení', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'YER'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'ZAR' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'ZAR', 'Rand', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Rand', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'ZAR'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'ZMW' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'ZMW', 'Kwacha zambiano', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Kwacha zambiano', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'ZMW'  
If Not Exists ( Select * From CFDI_TiposDeMoneda Where Clave = 'ZWL' )  Insert Into CFDI_TiposDeMoneda (  Clave, Descripcion, Decimales, PorcentajeVariacion, Status )  Values ( 'ZWL', 'Zimbabwe Dólar', 2, 500.0000, 'C' ) 
 Else Update CFDI_TiposDeMoneda Set Descripcion = 'Zimbabwe Dólar', Decimales = 2, PorcentajeVariacion = 500.0000, Status = 'C' Where Clave = 'ZWL'  
Go--#SQL 

