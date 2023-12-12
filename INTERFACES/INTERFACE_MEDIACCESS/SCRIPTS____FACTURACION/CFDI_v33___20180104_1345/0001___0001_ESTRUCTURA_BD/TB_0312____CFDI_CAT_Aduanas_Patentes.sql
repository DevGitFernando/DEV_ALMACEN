
-------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Aduanas_Patentes' and xType = 'U' ) 
	Drop Table CFDI_Aduanas_Patentes  
Go--#SQL  

Create Table CFDI_Aduanas_Patentes 
(	
	Clave varchar(4) Not Null Default '', 
	-- Descripcion varchar(100) Not Null Default '',  
	Status varchar(1) Not Null Default 'A' 
) 
Go--#SQL 

Alter Table CFDI_Aduanas_Patentes Add Constraint PK_CFDI_Aduanas_Patentes Primary	Key ( Clave ) 
Go--#SQL 


If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '0' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '0', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '0'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1000' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1000', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1000'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1001' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1001', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1001'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1002' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1002', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1002'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1003' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1003', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1003'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1004' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1004', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1004'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1005' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1005', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1005'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1006' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1006', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1006'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1007' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1007', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1007'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1008' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1008', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1008'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1009' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1009', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1009'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '101' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '101', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '101'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1010' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1010', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1010'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1011' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1011', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1011'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1012' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1012', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1012'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1013' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1013', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1013'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1014' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1014', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1014'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1015' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1015', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1015'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1016' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1016', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1016'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1017' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1017', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1017'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1018' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1018', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1018'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1019' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1019', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1019'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1020' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1020', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1020'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1021' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1021', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1021'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1022' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1022', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1022'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1023' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1023', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1023'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1024' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1024', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1024'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1025' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1025', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1025'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1026' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1026', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1026'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1027' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1027', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1027'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1028' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1028', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1028'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1029' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1029', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1029'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1030' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1030', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1030'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1031' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1031', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1031'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1032' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1032', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1032'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1033' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1033', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1033'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1034' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1034', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1034'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1035' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1035', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1035'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1036' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1036', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1036'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1037' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1037', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1037'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1038' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1038', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1038'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1039' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1039', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1039'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1040' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1040', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1040'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1041' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1041', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1041'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1042' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1042', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1042'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1043' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1043', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1043'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1044' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1044', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1044'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1045' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1045', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1045'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1046' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1046', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1046'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1047' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1047', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1047'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1048' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1048', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1048'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1049' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1049', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1049'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1050' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1050', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1050'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1051' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1051', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1051'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1052' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1052', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1052'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1054' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1054', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1054'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1055' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1055', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1055'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1056' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1056', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1056'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1057' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1057', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1057'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1058' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1058', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1058'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1059' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1059', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1059'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1060' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1060', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1060'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1061' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1061', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1061'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1062' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1062', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1062'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1063' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1063', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1063'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1064' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1064', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1064'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1065' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1065', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1065'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1066' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1066', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1066'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1067' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1067', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1067'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1068' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1068', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1068'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1069' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1069', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1069'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1070' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1070', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1070'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1071' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1071', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1071'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1072' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1072', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1072'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1073' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1073', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1073'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1074' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1074', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1074'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1075' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1075', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1075'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1076' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1076', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1076'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1077' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1077', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1077'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1078' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1078', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1078'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1079' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1079', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1079'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1080' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1080', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1080'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1081' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1081', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1081'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1082' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1082', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1082'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1084' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1084', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1084'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1085' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1085', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1085'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1086' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1086', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1086'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1087' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1087', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1087'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1088' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1088', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1088'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1089' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1089', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1089'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1090' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1090', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1090'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1092' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1092', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1092'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1094' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1094', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1094'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1095' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1095', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1095'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1096' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1096', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1096'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1097' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1097', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1097'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1098' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1098', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1098'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1099' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1099', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1099'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1100' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1100', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1100'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1102' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1102', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1102'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1103' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1103', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1103'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1104' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1104', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1104'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1105' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1105', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1105'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1106' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1106', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1106'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1107' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1107', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1107'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1108' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1108', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1108'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1109' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1109', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1109'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1111' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1111', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1111'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1112' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1112', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1112'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1113' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1113', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1113'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1114' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1114', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1114'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1115' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1115', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1115'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1116' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1116', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1116'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1117' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1117', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1117'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1120' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1120', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1120'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1121' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1121', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1121'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1122' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1122', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1122'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1123' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1123', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1123'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1124' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1124', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1124'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1125' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1125', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1125'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1126' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1126', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1126'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1127' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1127', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1127'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1128' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1128', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1128'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1129' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1129', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1129'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1130' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1130', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1130'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1132' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1132', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1132'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1136' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1136', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1136'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1137' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1137', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1137'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1138' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1138', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1138'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1139' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1139', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1139'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1141' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1141', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1141'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1142' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1142', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1142'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1144' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1144', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1144'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1145' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1145', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1145'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1148' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1148', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1148'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1149' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1149', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1149'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '115' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '115', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '115'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1150' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1150', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1150'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1152' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1152', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1152'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1153' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1153', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1153'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1154' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1154', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1154'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1155' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1155', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1155'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1156' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1156', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1156'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1157' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1157', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1157'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1158' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1158', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1158'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1159' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1159', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1159'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1160' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1160', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1160'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1161' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1161', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1161'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1162' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1162', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1162'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1163' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1163', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1163'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1164' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1164', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1164'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1165' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1165', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1165'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1166' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1166', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1166'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1167' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1167', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1167'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1168' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1168', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1168'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1169' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1169', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1169'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1170' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1170', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1170'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1171' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1171', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1171'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1172' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1172', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1172'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1173' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1173', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1173'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1174' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1174', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1174'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1175' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1175', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1175'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1176' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1176', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1176'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1177' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1177', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1177'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1178' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1178', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1178'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1179' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1179', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1179'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1181' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1181', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1181'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1182' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1182', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1182'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1186' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1186', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1186'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1187' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1187', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1187'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1188' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1188', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1188'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1189' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1189', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1189'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1190' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1190', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1190'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1191' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1191', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1191'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1192' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1192', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1192'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1193' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1193', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1193'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1194' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1194', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1194'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1195' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1195', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1195'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1196' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1196', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1196'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1197' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1197', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1197'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1198' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1198', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1198'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1199' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1199', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1199'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1200' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1200', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1200'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1201' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1201', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1201'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1202' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1202', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1202'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1203' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1203', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1203'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1204' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1204', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1204'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1205' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1205', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1205'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1206' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1206', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1206'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1207' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1207', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1207'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1208' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1208', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1208'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1209' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1209', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1209'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1210' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1210', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1210'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1211' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1211', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1211'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1212' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1212', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1212'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1213' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1213', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1213'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1214' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1214', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1214'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1215' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1215', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1215'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1216' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1216', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1216'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1217' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1217', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1217'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1218' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1218', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1218'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1219' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1219', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1219'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '122' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '122', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '122'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1220' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1220', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1220'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1221' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1221', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1221'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1222' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1222', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1222'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1223' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1223', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1223'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1224' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1224', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1224'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1225' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1225', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1225'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1226' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1226', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1226'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1227' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1227', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1227'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1228' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1228', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1228'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1229' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1229', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1229'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '123' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '123', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '123'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1230' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1230', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1230'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1231' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1231', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1231'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1232' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1232', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1232'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1233' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1233', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1233'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1234' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1234', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1234'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1235' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1235', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1235'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1236' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1236', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1236'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1237' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1237', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1237'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1238' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1238', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1238'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1239' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1239', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1239'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1240' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1240', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1240'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1241' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1241', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1241'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1242' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1242', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1242'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1243' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1243', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1243'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1244' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1244', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1244'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1245' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1245', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1245'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1246' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1246', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1246'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1247' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1247', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1247'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1248' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1248', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1248'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1249' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1249', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1249'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1250' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1250', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1250'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1251' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1251', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1251'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1252' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1252', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1252'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1253' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1253', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1253'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1254' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1254', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1254'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1255' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1255', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1255'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1256' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1256', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1256'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1257' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1257', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1257'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1258' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1258', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1258'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1259' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1259', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1259'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1260' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1260', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1260'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1261' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1261', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1261'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1262' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1262', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1262'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1263' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1263', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1263'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1264' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1264', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1264'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1265' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1265', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1265'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1266' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1266', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1266'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1267' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1267', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1267'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1268' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1268', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1268'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1269' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1269', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1269'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1270' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1270', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1270'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1271' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1271', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1271'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1272' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1272', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1272'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1273' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1273', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1273'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1274' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1274', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1274'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1275' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1275', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1275'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1276' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1276', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1276'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1277' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1277', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1277'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1278' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1278', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1278'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1279' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1279', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1279'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1280' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1280', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1280'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1281' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1281', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1281'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1282' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1282', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1282'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1283' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1283', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1283'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1284' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1284', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1284'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1285' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1285', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1285'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1286' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1286', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1286'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1287' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1287', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1287'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1288' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1288', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1288'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1289' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1289', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1289'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1290' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1290', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1290'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1291' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1291', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1291'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1293' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1293', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1293'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1294' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1294', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1294'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1295' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1295', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1295'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1296' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1296', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1296'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1297' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1297', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1297'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1298' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1298', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1298'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1299' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1299', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1299'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1300' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1300', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1300'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1301' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1301', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1301'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1302' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1302', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1302'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1303' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1303', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1303'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1304' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1304', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1304'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1305' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1305', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1305'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1306' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1306', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1306'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1307' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1307', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1307'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1308' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1308', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1308'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1309' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1309', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1309'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1311' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1311', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1311'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1312' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1312', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1312'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1313' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1313', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1313'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1314' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1314', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1314'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1315' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1315', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1315'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1316' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1316', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1316'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1317' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1317', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1317'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1318' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1318', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1318'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1319' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1319', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1319'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1320' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1320', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1320'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1321' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1321', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1321'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1322' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1322', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1322'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1323' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1323', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1323'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1324' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1324', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1324'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1325' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1325', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1325'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1326' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1326', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1326'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1327' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1327', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1327'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1328' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1328', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1328'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1329' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1329', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1329'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1330' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1330', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1330'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1331' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1331', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1331'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1332' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1332', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1332'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1333' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1333', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1333'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1334' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1334', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1334'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1335' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1335', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1335'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1336' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1336', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1336'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1337' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1337', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1337'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1338' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1338', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1338'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1339' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1339', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1339'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1340' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1340', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1340'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1341' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1341', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1341'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1342' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1342', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1342'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1343' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1343', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1343'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1344' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1344', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1344'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1345' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1345', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1345'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1346' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1346', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1346'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1347' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1347', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1347'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1348' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1348', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1348'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1349' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1349', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1349'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1350' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1350', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1350'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1351' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1351', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1351'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1352' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1352', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1352'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1353' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1353', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1353'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1354' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1354', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1354'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1355' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1355', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1355'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1356' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1356', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1356'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1357' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1357', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1357'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1358' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1358', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1358'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1359' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1359', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1359'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1360' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1360', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1360'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1361' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1361', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1361'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1362' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1362', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1362'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1363' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1363', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1363'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1364' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1364', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1364'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1365' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1365', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1365'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1368' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1368', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1368'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1369' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1369', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1369'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1370' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1370', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1370'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1371' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1371', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1371'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1372' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1372', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1372'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1373' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1373', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1373'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1374' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1374', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1374'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1375' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1375', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1375'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1376' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1376', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1376'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1377' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1377', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1377'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1378' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1378', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1378'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1379' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1379', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1379'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1380' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1380', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1380'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1381' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1381', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1381'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1382' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1382', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1382'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1383' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1383', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1383'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1384' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1384', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1384'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1385' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1385', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1385'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1386' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1386', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1386'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1387' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1387', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1387'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1388' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1388', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1388'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1389' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1389', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1389'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1390' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1390', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1390'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1391' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1391', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1391'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1392' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1392', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1392'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1393' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1393', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1393'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1394' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1394', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1394'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1395' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1395', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1395'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1396' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1396', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1396'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1397' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1397', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1397'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1398' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1398', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1398'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1399' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1399', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1399'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1400' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1400', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1400'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1401' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1401', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1401'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1402' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1402', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1402'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1403' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1403', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1403'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1404' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1404', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1404'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1405' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1405', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1405'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1406' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1406', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1406'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1407' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1407', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1407'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1408' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1408', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1408'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1409' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1409', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1409'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1410' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1410', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1410'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1411' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1411', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1411'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1412' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1412', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1412'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1413' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1413', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1413'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1414' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1414', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1414'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1415' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1415', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1415'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1416' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1416', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1416'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1417' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1417', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1417'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1418' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1418', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1418'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1419' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1419', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1419'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1420' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1420', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1420'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1421' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1421', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1421'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1422' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1422', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1422'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1423' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1423', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1423'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1424' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1424', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1424'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1425' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1425', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1425'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1426' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1426', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1426'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1428' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1428', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1428'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1429' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1429', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1429'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1430' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1430', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1430'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1431' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1431', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1431'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1432' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1432', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1432'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1433' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1433', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1433'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1434' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1434', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1434'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1435' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1435', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1435'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1436' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1436', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1436'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1437' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1437', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1437'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1438' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1438', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1438'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1439' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1439', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1439'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1440' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1440', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1440'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1441' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1441', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1441'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1442' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1442', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1442'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1443' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1443', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1443'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1444' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1444', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1444'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1445' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1445', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1445'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1446' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1446', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1446'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1447' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1447', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1447'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1448' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1448', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1448'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1449' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1449', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1449'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1450' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1450', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1450'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1451' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1451', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1451'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1452' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1452', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1452'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1453' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1453', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1453'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1454' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1454', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1454'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1455' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1455', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1455'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1456' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1456', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1456'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1457' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1457', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1457'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1458' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1458', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1458'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1459' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1459', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1459'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1460' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1460', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1460'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1461' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1461', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1461'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1462' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1462', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1462'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1463' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1463', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1463'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1464' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1464', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1464'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1465' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1465', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1465'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1466' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1466', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1466'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1467' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1467', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1467'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1468' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1468', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1468'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1469' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1469', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1469'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1470' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1470', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1470'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1471' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1471', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1471'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1472' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1472', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1472'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1473' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1473', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1473'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1474' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1474', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1474'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1475' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1475', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1475'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1476' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1476', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1476'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1477' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1477', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1477'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1478' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1478', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1478'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1479' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1479', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1479'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1480' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1480', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1480'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1481' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1481', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1481'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1482' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1482', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1482'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1483' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1483', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1483'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1484' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1484', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1484'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1485' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1485', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1485'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1486' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1486', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1486'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1487' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1487', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1487'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1488' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1488', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1488'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1489' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1489', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1489'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '149' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '149', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '149'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1490' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1490', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1490'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1491' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1491', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1491'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1492' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1492', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1492'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1493' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1493', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1493'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1494' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1494', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1494'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1495' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1495', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1495'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1496' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1496', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1496'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1497' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1497', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1497'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1498' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1498', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1498'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1499' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1499', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1499'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '150' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '150', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '150'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1500' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1500', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1500'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1501' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1501', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1501'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1502' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1502', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1502'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1503' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1503', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1503'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1504' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1504', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1504'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1505' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1505', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1505'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1506' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1506', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1506'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1507' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1507', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1507'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1508' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1508', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1508'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1509' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1509', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1509'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1510' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1510', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1510'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1511' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1511', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1511'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1512' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1512', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1512'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1513' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1513', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1513'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1514' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1514', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1514'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1515' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1515', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1515'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1516' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1516', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1516'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1517' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1517', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1517'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1518' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1518', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1518'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1519' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1519', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1519'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1520' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1520', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1520'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1521' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1521', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1521'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1522' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1522', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1522'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1523' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1523', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1523'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1524' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1524', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1524'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1525' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1525', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1525'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1526' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1526', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1526'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1527' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1527', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1527'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1528' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1528', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1528'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1529' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1529', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1529'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1530' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1530', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1530'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1531' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1531', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1531'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1532' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1532', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1532'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1533' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1533', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1533'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1534' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1534', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1534'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1535' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1535', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1535'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1536' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1536', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1536'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1537' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1537', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1537'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1538' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1538', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1538'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1539' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1539', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1539'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1540' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1540', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1540'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1541' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1541', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1541'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1542' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1542', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1542'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1543' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1543', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1543'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1544' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1544', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1544'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1545' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1545', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1545'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1546' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1546', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1546'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1547' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1547', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1547'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1548' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1548', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1548'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1549' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1549', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1549'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1550' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1550', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1550'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1551' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1551', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1551'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1552' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1552', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1552'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1553' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1553', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1553'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1554' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1554', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1554'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1555' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1555', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1555'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1556' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1556', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1556'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1557' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1557', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1557'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1558' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1558', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1558'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1559' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1559', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1559'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1560' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1560', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1560'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1561' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1561', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1561'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1562' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1562', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1562'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1563' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1563', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1563'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1564' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1564', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1564'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1565' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1565', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1565'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1566' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1566', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1566'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1567' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1567', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1567'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1568' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1568', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1568'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1569' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1569', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1569'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1570' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1570', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1570'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1571' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1571', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1571'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1572' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1572', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1572'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1573' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1573', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1573'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1574' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1574', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1574'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1575' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1575', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1575'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1576' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1576', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1576'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1577' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1577', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1577'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1578' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1578', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1578'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1579' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1579', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1579'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1580' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1580', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1580'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1581' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1581', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1581'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1582' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1582', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1582'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1583' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1583', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1583'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1584' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1584', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1584'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1585' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1585', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1585'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1586' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1586', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1586'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1587' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1587', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1587'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1588' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1588', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1588'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1589' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1589', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1589'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1590' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1590', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1590'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1591' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1591', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1591'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1592' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1592', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1592'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1593' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1593', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1593'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1594' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1594', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1594'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1595' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1595', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1595'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1596' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1596', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1596'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1597' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1597', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1597'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1598' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1598', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1598'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1599' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1599', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1599'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1600' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1600', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1600'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1601' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1601', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1601'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1602' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1602', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1602'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1603' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1603', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1603'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1604' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1604', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1604'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1605' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1605', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1605'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1606' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1606', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1606'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1607' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1607', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1607'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1608' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1608', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1608'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1609' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1609', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1609'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1610' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1610', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1610'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1611' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1611', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1611'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1612' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1612', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1612'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1613' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1613', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1613'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1614' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1614', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1614'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1615' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1615', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1615'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1616' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1616', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1616'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1617' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1617', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1617'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1618' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1618', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1618'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1619' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1619', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1619'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1620' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1620', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1620'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1621' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1621', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1621'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1622' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1622', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1622'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1623' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1623', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1623'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1624' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1624', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1624'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1625' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1625', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1625'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1626' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1626', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1626'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1627' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1627', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1627'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1628' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1628', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1628'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1629' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1629', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1629'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1630' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1630', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1630'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1631' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1631', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1631'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1632' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1632', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1632'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1633' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1633', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1633'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1634' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1634', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1634'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1635' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1635', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1635'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1636' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1636', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1636'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1637' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1637', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1637'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1638' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1638', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1638'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1639' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1639', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1639'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1640' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1640', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1640'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1641' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1641', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1641'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1642' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1642', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1642'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1643' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1643', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1643'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1644' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1644', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1644'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1645' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1645', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1645'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1646' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1646', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1646'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1647' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1647', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1647'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1648' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1648', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1648'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1649' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1649', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1649'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1650' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1650', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1650'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1651' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1651', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1651'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1652' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1652', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1652'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1653' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1653', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1653'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1654' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1654', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1654'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1655' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1655', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1655'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1656' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1656', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1656'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1657' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1657', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1657'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1658' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1658', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1658'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1659' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1659', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1659'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1660' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1660', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1660'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1661' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1661', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1661'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1662' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1662', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1662'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1663' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1663', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1663'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1664' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1664', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1664'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1665' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1665', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1665'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1666' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1666', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1666'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1667' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1667', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1667'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1668' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1668', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1668'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1669' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1669', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1669'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1670' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1670', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1670'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1671' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1671', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1671'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1672' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1672', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1672'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1673' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1673', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1673'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1674' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1674', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1674'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1675' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1675', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1675'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1676' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1676', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1676'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1677' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1677', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1677'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1678' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1678', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1678'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1679' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1679', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1679'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1680' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1680', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1680'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1681' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1681', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1681'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1682' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1682', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1682'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1683' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1683', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1683'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1684' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1684', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1684'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1685' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1685', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1685'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1686' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1686', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1686'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1687' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1687', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1687'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1688' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1688', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1688'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1689' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1689', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1689'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1690' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1690', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1690'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1691' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1691', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1691'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1692' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1692', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1692'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1693' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1693', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1693'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1694' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1694', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1694'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1695' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1695', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1695'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1696' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1696', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1696'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1697' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1697', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1697'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1698' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1698', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1698'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1699' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1699', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1699'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1700' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1700', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1700'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1701' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1701', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1701'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1702' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1702', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1702'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1703' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1703', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1703'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1704' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1704', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1704'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1705' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1705', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1705'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1706' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1706', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1706'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1707' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1707', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1707'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1708' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1708', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1708'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1709' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1709', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1709'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1710' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1710', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1710'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1711' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1711', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1711'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1712' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1712', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1712'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1713' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1713', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1713'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1714' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1714', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1714'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1715' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1715', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1715'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1716' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1716', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1716'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1717' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1717', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1717'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1730' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1730', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1730'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1731' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1731', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1731'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1733' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1733', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1733'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1734' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1734', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1734'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1735' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1735', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1735'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1739' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1739', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1739'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1740' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1740', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1740'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1741' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1741', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1741'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1742' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1742', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1742'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1743' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1743', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1743'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1744' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1744', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1744'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1746' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1746', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1746'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1747' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1747', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1747'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1748' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1748', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1748'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1749' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1749', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1749'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1750' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1750', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1750'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1751' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1751', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1751'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '1752' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '1752', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '1752'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '182' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '182', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '182'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '188' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '188', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '188'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '203' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '203', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '203'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '205' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '205', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '205'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '227' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '227', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '227'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '246' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '246', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '246'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '247' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '247', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '247'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '271' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '271', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '271'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '273' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '273', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '273'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '276' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '276', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '276'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '289' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '289', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '289'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '294' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '294', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '294'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3001' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3001', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3001'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3002' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3002', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3002'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3003' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3003', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3003'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3005' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3005', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3005'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3006' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3006', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3006'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3007' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3007', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3007'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3008' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3008', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3008'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3009' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3009', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3009'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3010' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3010', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3010'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3011' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3011', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3011'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3012' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3012', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3012'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3013' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3013', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3013'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3014' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3014', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3014'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3015' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3015', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3015'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3016' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3016', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3016'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3017' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3017', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3017'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3018' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3018', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3018'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3019' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3019', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3019'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3020' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3020', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3020'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3021' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3021', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3021'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3022' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3022', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3022'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3023' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3023', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3023'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3024' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3024', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3024'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3025' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3025', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3025'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3026' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3026', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3026'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3027' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3027', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3027'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3028' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3028', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3028'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3029' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3029', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3029'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3030' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3030', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3030'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3031' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3031', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3031'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3032' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3032', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3032'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3033' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3033', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3033'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3034' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3034', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3034'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3035' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3035', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3035'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3036' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3036', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3036'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3037' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3037', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3037'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3038' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3038', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3038'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3039' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3039', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3039'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3040' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3040', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3040'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3041' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3041', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3041'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3042' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3042', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3042'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3043' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3043', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3043'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3044' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3044', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3044'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3046' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3046', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3046'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3047' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3047', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3047'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3048' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3048', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3048'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3049' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3049', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3049'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3050' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3050', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3050'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3051' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3051', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3051'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3052' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3052', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3052'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3053' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3053', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3053'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3055' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3055', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3055'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3057' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3057', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3057'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3060' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3060', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3060'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3061' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3061', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3061'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3062' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3062', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3062'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3063' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3063', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3063'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3064' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3064', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3064'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3065' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3065', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3065'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3066' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3066', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3066'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3067' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3067', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3067'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3068' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3068', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3068'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3069' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3069', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3069'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3070' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3070', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3070'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3071' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3071', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3071'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3072' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3072', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3072'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3073' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3073', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3073'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3074' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3074', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3074'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3075' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3075', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3075'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3076' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3076', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3076'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3077' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3077', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3077'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3078' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3078', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3078'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3079' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3079', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3079'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3080' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3080', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3080'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3081' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3081', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3081'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3082' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3082', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3082'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3083' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3083', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3083'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3084' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3084', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3084'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3085' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3085', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3085'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3086' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3086', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3086'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3087' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3087', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3087'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3088' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3088', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3088'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3089' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3089', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3089'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '309' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '309', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '309'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3090' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3090', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3090'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3091' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3091', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3091'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3092' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3092', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3092'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3093' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3093', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3093'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3094' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3094', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3094'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3095' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3095', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3095'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3096' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3096', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3096'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3097' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3097', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3097'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3098' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3098', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3098'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3099' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3099', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3099'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '310' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '310', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '310'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3100' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3100', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3100'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3101' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3101', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3101'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3102' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3102', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3102'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3103' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3103', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3103'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3104' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3104', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3104'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3105' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3105', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3105'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3106' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3106', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3106'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3107' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3107', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3107'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3108' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3108', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3108'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3109' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3109', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3109'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3110' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3110', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3110'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3111' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3111', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3111'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3112' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3112', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3112'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3114' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3114', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3114'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3115' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3115', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3115'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3116' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3116', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3116'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3117' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3117', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3117'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3118' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3118', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3118'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3119' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3119', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3119'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '312' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '312', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '312'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3120' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3120', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3120'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3121' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3121', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3121'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3122' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3122', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3122'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3123' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3123', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3123'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3124' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3124', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3124'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3125' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3125', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3125'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3126' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3126', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3126'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3127' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3127', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3127'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3128' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3128', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3128'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3129' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3129', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3129'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3130' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3130', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3130'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3131' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3131', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3131'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3132' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3132', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3132'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3133' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3133', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3133'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3134' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3134', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3134'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3135' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3135', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3135'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3136' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3136', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3136'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3137' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3137', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3137'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3138' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3138', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3138'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3139' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3139', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3139'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3140' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3140', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3140'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3141' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3141', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3141'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3142' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3142', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3142'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3143' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3143', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3143'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3144' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3144', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3144'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3145' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3145', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3145'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3146' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3146', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3146'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3147' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3147', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3147'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3148' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3148', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3148'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3149' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3149', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3149'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3151' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3151', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3151'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3152' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3152', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3152'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3153' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3153', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3153'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3154' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3154', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3154'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3155' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3155', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3155'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3156' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3156', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3156'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3157' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3157', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3157'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3158' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3158', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3158'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3159' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3159', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3159'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '316' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '316', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '316'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3160' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3160', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3160'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3162' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3162', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3162'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3163' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3163', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3163'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3164' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3164', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3164'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3165' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3165', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3165'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3166' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3166', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3166'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3167' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3167', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3167'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3168' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3168', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3168'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3169' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3169', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3169'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3170' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3170', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3170'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3171' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3171', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3171'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3172' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3172', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3172'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3173' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3173', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3173'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3174' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3174', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3174'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3175' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3175', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3175'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3176' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3176', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3176'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3177' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3177', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3177'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3178' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3178', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3178'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3179' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3179', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3179'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '318' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '318', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '318'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3180' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3180', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3180'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3182' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3182', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3182'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3183' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3183', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3183'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3184' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3184', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3184'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3185' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3185', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3185'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3186' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3186', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3186'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3187' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3187', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3187'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3188' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3188', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3188'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3189' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3189', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3189'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3190' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3190', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3190'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3191' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3191', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3191'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3192' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3192', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3192'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3193' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3193', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3193'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3194' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3194', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3194'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3195' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3195', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3195'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3197' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3197', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3197'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3198' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3198', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3198'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3199' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3199', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3199'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3200' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3200', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3200'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3201' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3201', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3201'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3202' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3202', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3202'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3203' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3203', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3203'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3204' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3204', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3204'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3205' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3205', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3205'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3206' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3206', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3206'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3207' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3207', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3207'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3208' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3208', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3208'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3209' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3209', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3209'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '321' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '321', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '321'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3210' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3210', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3210'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3211' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3211', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3211'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3212' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3212', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3212'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3213' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3213', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3213'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3214' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3214', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3214'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3215' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3215', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3215'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3216' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3216', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3216'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3217' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3217', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3217'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3218' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3218', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3218'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3219' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3219', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3219'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3220' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3220', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3220'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3221' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3221', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3221'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3222' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3222', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3222'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3223' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3223', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3223'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3224' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3224', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3224'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3226' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3226', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3226'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3227' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3227', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3227'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3229' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3229', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3229'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '323' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '323', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '323'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3230' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3230', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3230'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3231' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3231', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3231'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3232' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3232', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3232'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3233' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3233', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3233'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3234' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3234', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3234'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3235' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3235', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3235'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3236' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3236', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3236'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3237' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3237', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3237'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3238' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3238', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3238'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3240' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3240', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3240'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3241' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3241', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3241'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3242' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3242', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3242'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3244' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3244', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3244'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3245' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3245', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3245'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3246' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3246', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3246'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3247' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3247', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3247'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3248' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3248', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3248'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3249' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3249', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3249'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3250' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3250', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3250'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3251' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3251', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3251'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3252' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3252', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3252'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3253' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3253', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3253'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3254' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3254', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3254'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3255' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3255', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3255'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3256' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3256', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3256'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3257' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3257', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3257'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3258' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3258', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3258'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3259' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3259', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3259'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3260' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3260', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3260'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3262' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3262', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3262'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3263' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3263', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3263'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3264' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3264', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3264'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3265' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3265', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3265'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3266' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3266', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3266'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3267' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3267', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3267'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3268' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3268', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3268'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3269' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3269', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3269'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '327' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '327', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '327'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3270' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3270', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3270'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3271' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3271', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3271'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3272' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3272', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3272'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3273' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3273', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3273'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3274' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3274', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3274'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3275' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3275', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3275'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3276' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3276', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3276'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3277' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3277', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3277'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3278' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3278', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3278'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3279' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3279', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3279'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '328' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '328', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '328'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3280' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3280', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3280'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3281' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3281', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3281'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3283' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3283', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3283'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3284' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3284', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3284'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3285' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3285', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3285'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3286' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3286', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3286'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3287' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3287', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3287'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3288' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3288', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3288'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3289' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3289', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3289'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3290' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3290', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3290'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3291' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3291', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3291'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3292' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3292', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3292'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3293' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3293', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3293'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3294' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3294', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3294'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3295' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3295', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3295'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3296' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3296', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3296'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3297' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3297', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3297'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3298' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3298', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3298'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3299' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3299', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3299'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3300' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3300', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3300'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3301' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3301', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3301'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3302' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3302', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3302'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3303' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3303', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3303'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3305' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3305', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3305'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3306' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3306', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3306'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3308' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3308', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3308'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3309' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3309', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3309'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3311' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3311', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3311'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3312' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3312', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3312'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3313' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3313', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3313'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3314' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3314', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3314'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3315' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3315', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3315'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3316' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3316', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3316'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3317' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3317', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3317'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3319' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3319', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3319'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3320' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3320', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3320'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3321' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3321', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3321'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3322' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3322', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3322'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3323' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3323', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3323'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3324' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3324', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3324'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3325' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3325', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3325'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3326' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3326', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3326'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3327' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3327', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3327'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3328' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3328', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3328'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3329' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3329', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3329'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3330' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3330', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3330'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3331' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3331', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3331'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3332' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3332', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3332'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3333' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3333', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3333'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3334' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3334', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3334'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3335' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3335', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3335'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3336' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3336', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3336'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3337' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3337', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3337'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3338' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3338', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3338'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3339' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3339', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3339'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '334' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '334', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '334'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3340' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3340', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3340'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3341' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3341', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3341'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3342' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3342', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3342'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3343' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3343', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3343'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3344' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3344', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3344'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3345' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3345', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3345'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3346' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3346', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3346'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3347' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3347', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3347'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3348' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3348', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3348'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3349' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3349', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3349'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3351' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3351', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3351'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3353' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3353', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3353'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3354' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3354', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3354'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3355' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3355', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3355'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3356' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3356', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3356'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3357' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3357', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3357'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3358' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3358', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3358'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3359' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3359', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3359'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3360' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3360', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3360'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3361' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3361', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3361'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3362' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3362', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3362'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3363' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3363', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3363'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3364' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3364', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3364'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3365' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3365', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3365'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3366' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3366', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3366'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3368' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3368', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3368'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3369' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3369', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3369'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3370' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3370', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3370'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3371' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3371', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3371'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3372' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3372', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3372'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3373' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3373', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3373'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3374' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3374', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3374'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3375' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3375', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3375'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3376' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3376', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3376'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3377' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3377', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3377'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3378' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3378', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3378'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3379' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3379', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3379'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3381' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3381', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3381'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3382' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3382', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3382'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3383' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3383', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3383'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3384' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3384', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3384'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3385' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3385', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3385'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3386' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3386', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3386'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3387' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3387', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3387'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3389' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3389', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3389'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '339' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '339', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '339'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3390' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3390', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3390'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3391' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3391', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3391'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3392' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3392', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3392'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3393' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3393', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3393'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3394' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3394', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3394'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3395' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3395', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3395'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3396' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3396', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3396'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3397' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3397', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3397'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3398' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3398', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3398'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3399' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3399', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3399'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3400' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3400', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3400'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3402' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3402', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3402'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3403' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3403', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3403'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3404' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3404', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3404'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3405' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3405', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3405'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3406' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3406', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3406'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3407' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3407', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3407'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3408' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3408', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3408'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3409' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3409', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3409'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3410' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3410', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3410'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3411' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3411', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3411'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3412' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3412', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3412'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3413' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3413', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3413'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3414' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3414', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3414'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3415' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3415', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3415'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3416' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3416', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3416'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3417' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3417', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3417'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3418' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3418', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3418'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3419' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3419', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3419'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3420' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3420', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3420'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3421' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3421', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3421'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3422' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3422', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3422'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3423' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3423', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3423'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3424' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3424', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3424'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3425' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3425', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3425'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3426' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3426', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3426'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3428' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3428', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3428'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3429' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3429', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3429'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3430' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3430', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3430'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3431' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3431', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3431'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3432' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3432', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3432'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3433' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3433', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3433'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3434' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3434', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3434'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3435' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3435', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3435'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3436' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3436', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3436'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3437' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3437', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3437'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3438' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3438', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3438'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3439' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3439', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3439'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3440' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3440', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3440'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3441' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3441', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3441'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3442' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3442', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3442'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3444' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3444', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3444'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3445' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3445', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3445'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3446' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3446', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3446'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3447' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3447', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3447'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3448' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3448', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3448'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3449' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3449', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3449'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3450' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3450', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3450'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3451' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3451', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3451'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3452' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3452', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3452'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3453' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3453', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3453'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3454' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3454', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3454'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3455' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3455', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3455'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3456' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3456', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3456'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3457' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3457', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3457'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3458' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3458', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3458'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3459' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3459', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3459'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3460' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3460', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3460'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3461' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3461', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3461'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3462' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3462', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3462'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3463' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3463', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3463'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3464' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3464', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3464'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3465' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3465', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3465'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3466' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3466', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3466'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3467' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3467', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3467'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3468' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3468', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3468'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3469' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3469', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3469'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '347' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '347', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '347'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3470' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3470', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3470'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3471' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3471', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3471'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3472' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3472', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3472'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3473' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3473', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3473'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3474' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3474', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3474'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3475' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3475', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3475'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3476' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3476', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3476'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3477' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3477', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3477'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3478' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3478', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3478'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3479' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3479', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3479'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '348' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '348', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '348'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3480' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3480', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3480'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3481' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3481', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3481'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3482' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3482', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3482'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3483' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3483', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3483'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3484' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3484', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3484'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3485' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3485', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3485'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3486' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3486', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3486'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3487' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3487', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3487'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3488' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3488', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3488'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3489' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3489', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3489'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3490' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3490', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3490'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3491' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3491', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3491'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3492' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3492', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3492'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3493' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3493', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3493'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3494' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3494', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3494'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3495' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3495', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3495'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3496' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3496', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3496'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3497' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3497', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3497'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3498' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3498', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3498'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3499' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3499', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3499'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '350' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '350', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '350'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3500' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3500', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3500'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3501' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3501', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3501'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3502' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3502', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3502'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3503' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3503', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3503'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3504' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3504', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3504'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3505' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3505', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3505'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3506' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3506', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3506'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3508' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3508', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3508'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3509' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3509', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3509'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3510' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3510', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3510'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3511' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3511', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3511'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3512' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3512', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3512'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3513' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3513', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3513'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3514' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3514', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3514'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3515' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3515', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3515'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3516' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3516', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3516'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3517' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3517', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3517'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3518' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3518', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3518'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3519' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3519', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3519'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3520' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3520', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3520'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3521' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3521', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3521'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3522' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3522', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3522'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3524' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3524', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3524'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3525' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3525', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3525'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3526' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3526', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3526'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3527' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3527', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3527'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3528' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3528', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3528'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3529' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3529', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3529'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3530' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3530', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3530'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3532' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3532', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3532'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3533' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3533', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3533'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3535' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3535', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3535'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3536' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3536', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3536'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3537' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3537', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3537'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3538' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3538', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3538'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3539' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3539', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3539'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3540' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3540', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3540'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3541' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3541', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3541'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3542' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3542', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3542'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3543' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3543', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3543'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3544' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3544', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3544'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3545' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3545', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3545'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3546' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3546', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3546'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3547' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3547', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3547'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3549' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3549', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3549'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3550' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3550', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3550'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3551' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3551', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3551'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3552' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3552', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3552'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3553' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3553', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3553'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3554' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3554', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3554'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3555' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3555', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3555'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3556' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3556', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3556'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3557' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3557', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3557'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3558' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3558', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3558'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3559' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3559', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3559'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3560' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3560', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3560'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3561' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3561', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3561'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3562' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3562', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3562'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3563' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3563', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3563'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3564' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3564', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3564'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3565' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3565', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3565'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3566' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3566', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3566'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3567' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3567', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3567'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3568' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3568', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3568'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3569' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3569', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3569'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3570' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3570', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3570'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3571' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3571', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3571'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3573' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3573', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3573'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3574' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3574', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3574'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3575' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3575', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3575'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3576' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3576', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3576'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3577' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3577', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3577'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3578' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3578', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3578'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3579' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3579', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3579'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3580' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3580', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3580'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3581' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3581', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3581'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3582' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3582', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3582'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3583' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3583', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3583'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3584' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3584', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3584'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3585' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3585', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3585'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3586' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3586', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3586'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3587' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3587', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3587'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3588' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3588', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3588'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3589' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3589', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3589'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3590' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3590', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3590'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3591' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3591', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3591'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3592' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3592', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3592'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3593' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3593', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3593'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3594' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3594', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3594'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3595' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3595', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3595'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3596' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3596', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3596'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3597' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3597', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3597'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3598' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3598', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3598'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3599' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3599', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3599'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3600' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3600', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3600'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3601' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3601', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3601'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3602' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3602', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3602'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3603' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3603', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3603'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3604' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3604', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3604'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3605' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3605', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3605'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3606' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3606', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3606'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3607' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3607', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3607'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3608' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3608', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3608'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3609' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3609', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3609'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '361' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '361', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '361'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3610' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3610', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3610'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3611' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3611', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3611'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3612' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3612', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3612'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3613' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3613', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3613'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3614' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3614', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3614'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3615' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3615', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3615'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3616' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3616', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3616'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3617' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3617', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3617'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3618' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3618', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3618'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3619' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3619', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3619'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3620' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3620', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3620'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3621' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3621', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3621'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3622' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3622', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3622'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3623' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3623', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3623'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3624' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3624', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3624'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3625' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3625', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3625'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3626' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3626', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3626'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3627' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3627', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3627'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3628' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3628', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3628'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3629' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3629', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3629'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3630' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3630', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3630'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3631' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3631', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3631'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3632' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3632', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3632'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3633' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3633', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3633'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3634' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3634', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3634'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3635' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3635', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3635'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3636' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3636', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3636'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3637' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3637', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3637'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3638' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3638', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3638'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3639' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3639', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3639'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3640' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3640', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3640'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3642' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3642', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3642'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3643' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3643', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3643'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3644' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3644', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3644'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3645' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3645', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3645'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3646' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3646', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3646'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3647' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3647', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3647'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3648' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3648', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3648'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3649' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3649', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3649'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3650' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3650', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3650'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3651' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3651', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3651'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3652' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3652', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3652'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3653' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3653', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3653'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3654' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3654', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3654'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3655' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3655', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3655'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3656' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3656', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3656'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3657' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3657', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3657'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3658' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3658', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3658'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3659' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3659', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3659'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3660' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3660', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3660'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3661' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3661', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3661'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3662' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3662', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3662'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3663' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3663', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3663'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3664' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3664', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3664'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3665' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3665', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3665'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3666' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3666', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3666'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3667' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3667', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3667'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3668' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3668', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3668'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3669' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3669', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3669'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3670' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3670', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3670'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3671' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3671', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3671'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3672' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3672', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3672'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3673' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3673', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3673'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3674' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3674', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3674'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3675' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3675', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3675'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3676' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3676', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3676'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3677' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3677', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3677'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3678' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3678', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3678'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3679' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3679', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3679'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '368' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '368', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '368'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3680' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3680', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3680'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3681' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3681', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3681'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3682' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3682', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3682'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3683' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3683', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3683'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3684' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3684', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3684'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3685' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3685', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3685'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3686' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3686', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3686'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3687' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3687', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3687'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3688' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3688', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3688'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3689' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3689', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3689'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3690' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3690', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3690'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3691' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3691', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3691'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3692' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3692', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3692'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3693' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3693', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3693'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3694' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3694', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3694'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3695' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3695', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3695'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3696' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3696', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3696'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3697' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3697', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3697'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3698' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3698', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3698'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3699' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3699', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3699'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3700' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3700', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3700'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3701' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3701', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3701'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3702' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3702', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3702'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3703' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3703', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3703'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3704' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3704', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3704'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3705' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3705', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3705'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3706' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3706', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3706'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3707' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3707', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3707'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3708' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3708', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3708'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3709' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3709', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3709'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3710' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3710', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3710'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3711' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3711', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3711'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3712' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3712', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3712'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3713' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3713', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3713'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3714' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3714', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3714'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3715' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3715', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3715'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3716' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3716', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3716'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3717' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3717', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3717'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3718' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3718', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3718'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3719' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3719', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3719'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3720' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3720', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3720'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3721' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3721', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3721'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3722' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3722', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3722'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3723' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3723', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3723'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3724' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3724', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3724'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3725' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3725', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3725'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3726' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3726', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3726'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3727' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3727', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3727'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3728' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3728', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3728'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3729' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3729', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3729'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3730' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3730', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3730'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3731' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3731', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3731'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3732' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3732', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3732'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3733' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3733', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3733'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3734' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3734', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3734'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3735' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3735', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3735'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3736' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3736', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3736'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3737' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3737', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3737'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3738' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3738', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3738'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3739' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3739', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3739'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3740' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3740', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3740'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3741' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3741', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3741'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3742' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3742', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3742'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3743' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3743', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3743'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3744' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3744', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3744'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3746' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3746', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3746'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3747' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3747', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3747'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3748' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3748', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3748'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3749' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3749', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3749'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3750' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3750', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3750'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3751' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3751', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3751'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3752' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3752', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3752'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3753' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3753', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3753'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3754' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3754', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3754'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3755' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3755', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3755'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3756' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3756', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3756'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3757' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3757', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3757'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3758' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3758', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3758'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3759' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3759', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3759'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3760' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3760', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3760'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3761' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3761', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3761'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3762' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3762', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3762'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3763' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3763', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3763'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3764' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3764', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3764'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3765' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3765', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3765'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3766' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3766', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3766'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3767' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3767', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3767'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3769' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3769', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3769'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3770' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3770', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3770'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3771' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3771', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3771'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3772' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3772', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3772'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3773' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3773', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3773'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3774' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3774', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3774'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3775' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3775', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3775'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3776' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3776', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3776'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3777' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3777', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3777'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3778' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3778', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3778'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3779' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3779', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3779'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3780' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3780', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3780'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3781' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3781', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3781'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3783' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3783', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3783'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3784' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3784', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3784'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3785' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3785', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3785'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3786' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3786', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3786'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3787' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3787', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3787'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3788' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3788', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3788'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3789' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3789', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3789'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3790' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3790', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3790'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3791' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3791', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3791'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3792' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3792', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3792'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3793' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3793', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3793'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3794' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3794', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3794'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3795' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3795', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3795'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3796' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3796', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3796'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3797' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3797', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3797'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3799' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3799', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3799'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3800' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3800', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3800'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3801' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3801', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3801'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3802' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3802', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3802'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3803' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3803', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3803'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3804' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3804', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3804'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3805' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3805', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3805'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3806' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3806', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3806'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3807' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3807', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3807'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3808' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3808', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3808'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3809' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3809', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3809'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '381' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '381', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '381'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3810' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3810', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3810'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3811' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3811', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3811'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3812' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3812', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3812'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3813' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3813', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3813'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3814' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3814', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3814'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3815' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3815', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3815'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3816' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3816', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3816'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3817' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3817', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3817'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3818' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3818', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3818'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3819' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3819', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3819'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '382' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '382', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '382'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3820' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3820', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3820'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3821' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3821', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3821'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3822' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3822', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3822'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3823' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3823', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3823'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3824' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3824', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3824'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3825' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3825', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3825'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3826' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3826', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3826'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3827' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3827', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3827'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3828' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3828', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3828'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3829' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3829', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3829'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3830' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3830', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3830'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3831' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3831', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3831'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3832' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3832', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3832'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3833' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3833', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3833'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3834' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3834', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3834'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3835' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3835', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3835'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3836' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3836', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3836'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3837' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3837', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3837'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3838' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3838', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3838'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3839' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3839', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3839'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3840' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3840', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3840'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3841' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3841', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3841'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3842' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3842', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3842'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3843' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3843', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3843'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3844' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3844', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3844'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3845' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3845', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3845'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3846' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3846', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3846'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3847' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3847', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3847'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3848' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3848', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3848'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3849' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3849', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3849'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3850' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3850', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3850'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3851' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3851', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3851'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3852' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3852', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3852'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3853' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3853', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3853'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3854' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3854', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3854'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3855' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3855', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3855'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3856' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3856', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3856'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3857' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3857', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3857'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3858' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3858', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3858'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3859' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3859', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3859'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3860' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3860', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3860'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3861' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3861', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3861'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3862' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3862', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3862'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3863' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3863', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3863'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3864' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3864', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3864'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3865' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3865', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3865'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3866' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3866', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3866'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3867' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3867', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3867'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3868' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3868', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3868'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3869' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3869', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3869'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '387' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '387', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '387'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3870' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3870', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3870'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3871' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3871', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3871'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3872' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3872', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3872'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3873' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3873', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3873'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3874' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3874', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3874'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3875' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3875', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3875'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3876' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3876', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3876'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3877' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3877', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3877'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3878' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3878', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3878'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3879' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3879', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3879'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '388' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '388', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '388'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3880' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3880', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3880'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3881' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3881', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3881'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3882' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3882', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3882'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3883' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3883', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3883'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3884' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3884', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3884'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3885' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3885', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3885'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3886' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3886', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3886'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3887' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3887', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3887'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3888' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3888', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3888'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3889' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3889', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3889'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '389' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '389', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '389'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3890' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3890', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3890'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3891' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3891', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3891'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3892' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3892', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3892'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3893' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3893', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3893'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3894' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3894', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3894'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3895' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3895', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3895'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3896' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3896', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3896'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3897' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3897', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3897'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3898' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3898', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3898'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3899' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3899', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3899'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '390' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '390', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '390'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3900' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3900', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3900'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3901' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3901', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3901'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3902' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3902', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3902'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3903' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3903', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3903'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3904' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3904', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3904'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3905' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3905', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3905'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3906' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3906', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3906'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3907' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3907', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3907'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3908' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3908', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3908'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3909' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3909', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3909'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3910' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3910', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3910'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3911' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3911', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3911'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3912' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3912', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3912'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3913' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3913', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3913'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3914' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3914', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3914'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3915' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3915', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3915'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3916' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3916', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3916'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3917' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3917', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3917'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3918' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3918', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3918'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3919' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3919', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3919'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3920' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3920', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3920'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3921' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3921', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3921'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3922' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3922', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3922'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3923' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3923', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3923'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3924' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3924', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3924'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3925' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3925', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3925'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3926' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3926', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3926'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3927' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3927', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3927'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3928' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3928', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3928'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3929' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3929', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3929'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3930' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3930', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3930'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3931' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3931', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3931'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3932' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3932', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3932'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3933' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3933', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3933'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3934' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3934', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3934'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3935' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3935', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3935'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3936' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3936', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3936'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3937' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3937', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3937'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3938' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3938', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3938'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3939' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3939', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3939'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '394' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '394', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '394'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3940' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3940', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3940'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3941' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3941', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3941'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3942' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3942', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3942'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3943' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3943', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3943'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3944' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3944', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3944'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3945' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3945', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3945'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3946' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3946', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3946'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3947' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3947', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3947'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3948' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3948', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3948'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3949' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3949', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3949'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3950' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3950', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3950'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3951' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3951', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3951'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3952' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3952', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3952'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3953' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3953', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3953'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3954' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3954', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3954'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3955' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3955', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3955'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3956' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3956', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3956'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3957' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3957', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3957'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3958' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3958', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3958'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3959' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3959', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3959'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3960' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3960', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3960'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3961' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3961', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3961'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3962' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3962', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3962'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3963' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3963', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3963'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3964' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3964', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3964'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3965' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3965', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3965'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3966' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3966', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3966'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3967' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3967', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3967'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3968' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3968', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3968'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3969' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3969', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3969'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3970' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3970', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3970'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3971' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3971', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3971'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3972' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3972', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3972'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3973' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3973', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3973'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3974' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3974', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3974'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3975' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3975', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3975'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3976' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3976', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3976'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3977' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3977', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3977'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3978' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3978', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3978'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3979' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3979', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3979'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '398' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '398', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '398'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3980' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3980', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3980'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3981' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3981', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3981'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3982' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3982', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3982'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3983' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3983', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3983'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3984' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3984', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3984'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3985' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3985', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3985'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3986' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3986', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3986'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3987' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3987', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3987'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3988' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3988', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3988'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3989' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3989', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3989'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3990' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3990', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3990'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3991' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3991', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3991'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3992' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3992', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3992'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3993' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3993', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3993'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3994' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3994', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3994'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3995' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3995', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3995'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3996' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3996', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3996'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3997' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3997', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3997'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3998' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3998', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3998'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '3999' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '3999', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '3999'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '400' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '400', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '400'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4001' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4001', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4001'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4002' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4002', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4002'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4003' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4003', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4003'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4004' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4004', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4004'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4005' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4005', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4005'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4006' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4006', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4006'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4007' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4007', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4007'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4008' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4008', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4008'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4009' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4009', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4009'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '401' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '401', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '401'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4011' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4011', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4011'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4012' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4012', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4012'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4013' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4013', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4013'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4014' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4014', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4014'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4015' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4015', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4015'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4016' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4016', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4016'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4017' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4017', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4017'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4018' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4018', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4018'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4019' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4019', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4019'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4020' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4020', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4020'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4021' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4021', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4021'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4022' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4022', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4022'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4023' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4023', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4023'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4024' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4024', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4024'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4025' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4025', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4025'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4026' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4026', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4026'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4027' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4027', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4027'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4028' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4028', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4028'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4029' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4029', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4029'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '403' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '403', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '403'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4030' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4030', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4030'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4031' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4031', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4031'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4032' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4032', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4032'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4033' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4033', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4033'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4035' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4035', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4035'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4036' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4036', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4036'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4037' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4037', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4037'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4038' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4038', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4038'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4039' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4039', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4039'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '404' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '404', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '404'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4040' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4040', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4040'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4041' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4041', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4041'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4042' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4042', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4042'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4043' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4043', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4043'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4044' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4044', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4044'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4045' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4045', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4045'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4046' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4046', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4046'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4047' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4047', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4047'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4048' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4048', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4048'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4049' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4049', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4049'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4050' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4050', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4050'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4052' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4052', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4052'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4054' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4054', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4054'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4055' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4055', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4055'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4056' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4056', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4056'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4057' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4057', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4057'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4058' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4058', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4058'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4059' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4059', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4059'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '406' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '406', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '406'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4060' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4060', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4060'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4061' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4061', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4061'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4063' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4063', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4063'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4064' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4064', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4064'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4066' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4066', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4066'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4067' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4067', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4067'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4068' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4068', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4068'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4069' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4069', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4069'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '407' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '407', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '407'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4070' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4070', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4070'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4071' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4071', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4071'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4072' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4072', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4072'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4073' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4073', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4073'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4074' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4074', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4074'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4075' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4075', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4075'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4076' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4076', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4076'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4078' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4078', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4078'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4079' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4079', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4079'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4080' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4080', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4080'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4081' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4081', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4081'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4082' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4082', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4082'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4083' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4083', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4083'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4084' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4084', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4084'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4085' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4085', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4085'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4086' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4086', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4086'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4087' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4087', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4087'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4088' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4088', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4088'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4089' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4089', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4089'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4090' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4090', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4090'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4091' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4091', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4091'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4092' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4092', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4092'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4093' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4093', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4093'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4094' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4094', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4094'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4095' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4095', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4095'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4096' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4096', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4096'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4098' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4098', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4098'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4099' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4099', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4099'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4100' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4100', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4100'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4101' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4101', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4101'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4102' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4102', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4102'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4103' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4103', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4103'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4104' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4104', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4104'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4105' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4105', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4105'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4106' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4106', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4106'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4107' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4107', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4107'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4108' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4108', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4108'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4109' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4109', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4109'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '411' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '411', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '411'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4110' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4110', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4110'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4111' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4111', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4111'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4112' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4112', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4112'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4113' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4113', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4113'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4114' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4114', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4114'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4115' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4115', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4115'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4116' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4116', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4116'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '4117' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '4117', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '4117'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '412' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '412', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '412'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '413' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '413', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '413'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '414' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '414', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '414'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '418' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '418', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '418'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '419' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '419', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '419'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '420' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '420', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '420'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '424' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '424', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '424'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '425' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '425', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '425'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '426' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '426', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '426'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '427' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '427', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '427'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '428' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '428', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '428'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '429' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '429', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '429'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '430' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '430', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '430'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '432' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '432', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '432'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '438' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '438', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '438'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '440' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '440', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '440'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '441' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '441', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '441'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '442' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '442', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '442'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '444' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '444', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '444'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '445' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '445', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '445'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '447' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '447', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '447'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '449' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '449', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '449'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '451' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '451', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '451'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '454' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '454', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '454'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '457' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '457', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '457'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '460' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '460', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '460'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '462' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '462', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '462'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '464' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '464', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '464'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '465' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '465', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '465'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '467' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '467', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '467'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '469' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '469', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '469'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '472' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '472', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '472'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '474' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '474', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '474'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '476' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '476', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '476'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '477' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '477', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '477'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '478' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '478', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '478'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '480' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '480', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '480'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '481' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '481', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '481'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '482' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '482', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '482'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '483' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '483', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '483'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '485' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '485', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '485'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '486' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '486', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '486'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '487' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '487', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '487'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '489' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '489', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '489'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '491' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '491', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '491'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '492' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '492', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '492'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '493' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '493', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '493'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '494' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '494', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '494'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '496' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '496', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '496'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '498' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '498', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '498'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '499' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '499', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '499'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '500' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '500', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '500'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5001' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5001', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5001'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5002' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5002', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5002'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5003' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5003', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5003'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5004' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5004', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5004'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5005' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5005', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5005'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5006' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5006', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5006'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5007' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5007', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5007'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5008' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5008', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5008'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5009' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5009', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5009'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '501' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '501', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '501'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5010' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5010', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5010'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5011' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5011', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5011'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5012' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5012', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5012'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5013' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5013', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5013'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5014' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5014', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5014'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5015' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5015', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5015'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5016' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5016', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5016'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5017' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5017', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5017'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5018' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5018', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5018'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5019' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5019', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5019'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5020' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5020', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5020'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5021' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5021', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5021'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5022' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5022', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5022'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5023' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5023', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5023'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5024' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5024', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5024'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5025' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5025', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5025'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5026' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5026', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5026'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5027' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5027', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5027'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5028' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5028', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5028'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5029' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5029', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5029'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '503' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '503', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '503'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5030' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5030', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5030'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5031' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5031', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5031'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5032' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5032', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5032'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5033' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5033', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5033'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5034' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5034', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5034'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5035' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5035', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5035'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5036' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5036', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5036'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5037' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5037', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5037'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5038' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5038', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5038'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5039' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5039', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5039'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '504' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '504', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '504'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5040' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5040', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5040'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5041' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5041', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5041'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5042' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5042', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5042'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5043' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5043', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5043'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5044' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5044', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5044'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5045' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5045', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5045'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5046' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5046', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5046'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5047' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5047', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5047'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5048' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5048', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5048'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5049' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5049', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5049'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5050' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5050', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5050'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5051' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5051', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5051'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5052' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5052', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5052'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5053' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5053', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5053'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5054' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5054', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5054'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5055' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5055', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5055'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5056' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5056', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5056'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5057' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5057', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5057'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5058' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5058', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5058'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5059' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5059', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5059'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '506' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '506', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '506'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5060' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5060', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5060'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5061' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5061', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5061'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5062' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5062', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5062'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5063' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5063', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5063'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5064' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5064', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5064'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5065' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5065', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5065'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5066' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5066', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5066'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5067' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5067', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5067'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5068' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5068', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5068'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5069' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5069', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5069'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5070' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5070', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5070'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5071' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5071', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5071'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5072' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5072', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5072'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5073' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5073', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5073'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5074' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5074', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5074'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5075' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5075', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5075'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5076' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5076', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5076'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5077' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5077', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5077'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5078' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5078', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5078'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5079' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5079', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5079'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '508' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '508', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '508'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5080' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5080', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5080'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5081' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5081', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5081'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5082' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5082', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5082'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5083' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5083', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5083'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5084' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5084', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5084'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5085' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5085', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5085'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5086' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5086', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5086'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5087' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5087', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5087'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5088' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5088', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5088'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5089' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5089', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5089'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '509' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '509', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '509'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5090' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5090', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5090'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5091' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5091', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5091'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5092' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5092', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5092'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5093' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5093', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5093'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5094' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5094', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5094'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5095' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5095', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5095'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5096' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5096', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5096'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5098' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5098', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5098'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5099' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5099', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5099'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '510' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '510', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '510'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5100' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5100', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5100'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5101' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5101', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5101'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5102' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5102', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5102'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5103' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5103', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5103'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5104' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5104', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5104'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5105' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5105', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5105'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5106' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5106', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5106'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5107' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5107', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5107'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5108' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5108', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5108'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5109' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5109', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5109'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5110' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5110', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5110'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5111' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5111', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5111'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5112' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5112', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5112'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5113' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5113', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5113'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5114' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5114', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5114'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5115' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5115', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5115'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5116' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5116', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5116'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5117' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5117', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5117'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5118' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5118', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5118'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5119' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5119', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5119'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '512' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '512', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '512'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5120' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5120', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5120'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5121' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5121', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5121'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5122' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5122', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5122'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5123' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5123', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5123'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5124' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5124', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5124'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5125' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5125', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5125'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5126' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5126', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5126'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5127' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5127', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5127'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5128' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5128', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5128'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5129' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5129', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5129'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '513' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '513', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '513'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5130' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5130', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5130'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5131' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5131', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5131'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5132' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5132', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5132'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5133' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5133', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5133'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5134' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5134', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5134'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5135' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5135', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5135'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5136' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5136', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5136'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5137' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5137', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5137'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5138' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5138', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5138'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5139' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5139', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5139'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5140' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5140', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5140'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5141' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5141', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5141'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5142' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5142', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5142'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5143' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5143', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5143'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5148' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5148', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5148'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5149' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5149', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5149'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5150' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5150', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5150'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5151' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5151', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5151'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5152' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5152', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5152'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5153' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5153', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5153'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5154' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5154', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5154'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5155' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5155', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5155'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5156' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5156', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5156'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5157' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5157', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5157'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5158' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5158', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5158'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5159' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5159', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5159'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5160' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5160', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5160'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5161' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5161', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5161'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5162' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5162', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5162'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5163' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5163', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5163'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5164' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5164', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5164'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5165' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5165', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5165'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5166' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5166', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5166'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5167' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5167', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5167'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5168' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5168', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5168'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5170' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5170', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5170'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5171' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5171', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5171'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5172' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5172', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5172'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5173' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5173', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5173'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5174' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5174', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5174'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5175' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5175', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5175'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5176' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5176', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5176'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5177' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5177', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5177'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5178' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5178', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5178'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5179' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5179', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5179'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5180' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5180', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5180'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5181' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5181', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5181'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5182' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5182', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5182'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5183' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5183', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5183'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5184' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5184', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5184'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5185' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5185', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5185'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5186' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5186', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5186'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5187' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5187', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5187'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5188' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5188', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5188'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5189' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5189', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5189'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '519' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '519', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '519'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5190' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5190', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5190'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5191' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5191', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5191'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5192' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5192', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5192'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5193' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5193', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5193'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5194' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5194', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5194'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5195' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5195', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5195'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5196' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5196', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5196'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5197' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5197', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5197'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5198' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5198', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5198'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5199' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5199', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5199'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '520' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '520', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '520'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5200' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5200', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5200'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5201' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5201', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5201'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5202' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5202', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5202'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5203' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5203', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5203'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5204' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5204', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5204'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5205' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5205', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5205'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5206' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5206', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5206'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5207' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5207', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5207'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5208' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5208', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5208'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5209' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5209', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5209'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '521' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '521', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '521'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5210' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5210', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5210'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5211' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5211', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5211'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5212' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5212', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5212'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5213' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5213', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5213'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5214' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5214', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5214'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5215' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5215', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5215'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5216' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5216', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5216'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5217' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5217', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5217'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5218' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5218', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5218'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5219' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5219', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5219'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '522' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '522', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '522'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5220' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5220', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5220'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5221' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5221', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5221'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5222' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5222', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5222'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5223' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5223', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5223'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5224' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5224', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5224'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5225' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5225', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5225'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5226' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5226', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5226'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5227' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5227', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5227'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5228' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5228', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5228'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5229' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5229', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5229'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '523' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '523', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '523'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5230' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5230', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5230'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5231' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5231', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5231'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5233' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5233', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5233'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5234' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5234', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5234'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5235' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5235', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5235'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5236' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5236', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5236'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5237' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5237', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5237'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5238' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5238', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5238'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5239' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5239', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5239'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '524' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '524', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '524'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5240' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5240', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5240'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5241' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5241', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5241'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5242' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5242', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5242'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5243' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5243', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5243'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5244' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5244', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5244'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5245' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5245', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5245'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5246' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5246', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5246'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5247' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5247', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5247'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5248' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5248', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5248'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5249' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5249', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5249'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '525' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '525', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '525'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5250' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5250', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5250'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5251' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5251', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5251'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5252' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5252', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5252'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5253' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5253', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5253'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5254' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5254', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5254'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5255' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5255', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5255'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5256' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5256', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5256'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5257' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5257', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5257'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5258' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5258', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5258'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5259' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5259', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5259'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '526' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '526', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '526'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5260' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5260', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5260'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5261' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5261', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5261'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5262' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5262', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5262'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5263' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5263', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5263'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5264' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5264', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5264'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5265' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5265', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5265'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5266' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5266', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5266'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5267' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5267', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5267'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5268' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5268', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5268'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5269' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5269', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5269'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '527' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '527', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '527'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5270' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5270', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5270'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5271' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5271', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5271'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5272' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5272', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5272'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5273' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5273', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5273'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5274' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5274', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5274'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5275' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5275', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5275'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5276' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5276', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5276'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5277' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5277', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5277'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5278' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5278', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5278'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '528' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '528', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '528'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5280' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5280', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5280'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5281' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5281', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5281'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5282' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5282', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5282'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5283' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5283', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5283'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5284' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5284', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5284'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5286' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5286', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5286'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5287' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5287', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5287'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5288' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5288', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5288'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5289' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5289', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5289'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '529' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '529', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '529'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5290' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5290', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5290'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5291' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5291', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5291'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5292' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5292', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5292'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5293' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5293', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5293'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5294' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5294', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5294'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5295' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5295', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5295'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5296' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5296', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5296'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5297' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5297', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5297'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5298' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5298', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5298'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5299' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5299', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5299'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5300' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5300', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5300'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5302' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5302', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5302'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5303' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5303', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5303'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5304' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5304', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5304'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5305' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5305', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5305'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5307' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5307', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5307'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5308' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5308', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5308'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5309' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5309', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5309'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '531' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '531', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '531'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5310' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5310', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5310'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5311' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5311', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5311'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5312' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5312', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5312'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5313' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5313', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5313'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5314' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5314', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5314'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5315' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5315', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5315'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5316' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5316', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5316'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5317' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5317', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5317'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5318' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5318', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5318'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5319' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5319', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5319'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '532' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '532', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '532'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5320' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5320', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5320'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5321' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5321', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5321'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5322' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5322', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5322'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5323' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5323', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5323'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5324' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5324', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5324'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5325' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5325', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5325'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5326' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5326', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5326'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5327' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5327', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5327'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5328' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5328', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5328'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5330' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5330', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5330'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5331' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5331', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5331'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5332' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5332', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5332'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5333' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5333', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5333'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5334' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5334', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5334'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5335' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5335', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5335'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5338' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5338', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5338'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5339' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5339', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5339'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '534' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '534', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '534'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5340' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5340', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5340'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5341' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5341', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5341'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5342' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5342', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5342'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5343' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5343', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5343'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5344' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5344', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5344'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5345' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5345', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5345'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5346' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5346', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5346'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5348' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5348', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5348'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '535' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '535', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '535'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5350' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5350', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5350'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5351' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5351', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5351'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5352' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5352', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5352'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5353' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5353', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5353'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5354' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5354', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5354'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5355' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5355', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5355'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5356' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5356', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5356'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5357' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5357', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5357'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5358' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5358', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5358'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5359' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5359', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5359'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5360' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5360', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5360'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5361' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5361', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5361'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5362' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5362', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5362'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5363' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5363', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5363'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5364' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5364', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5364'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5365' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5365', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5365'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5366' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5366', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5366'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5367' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5367', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5367'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5368' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5368', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5368'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5369' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5369', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5369'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '537' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '537', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '537'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5370' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5370', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5370'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5371' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5371', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5371'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5372' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5372', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5372'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5373' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5373', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5373'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5374' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5374', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5374'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5375' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5375', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5375'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5376' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5376', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5376'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5377' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5377', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5377'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5378' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5378', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5378'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5379' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5379', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5379'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '538' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '538', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '538'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5380' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5380', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5380'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5381' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5381', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5381'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5382' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5382', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5382'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5383' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5383', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5383'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5384' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5384', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5384'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5385' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5385', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5385'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5386' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5386', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5386'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5387' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5387', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5387'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5388' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5388', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5388'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5389' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5389', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5389'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '539' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '539', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '539'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5390' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5390', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5390'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5391' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5391', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5391'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5392' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5392', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5392'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5393' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5393', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5393'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5394' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5394', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5394'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5395' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5395', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5395'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5396' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5396', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5396'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5397' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5397', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5397'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5398' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5398', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5398'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5399' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5399', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5399'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '540' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '540', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '540'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5400' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5400', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5400'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5401' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5401', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5401'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5402' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5402', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5402'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5403' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5403', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5403'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5404' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5404', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5404'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5405' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5405', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5405'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5406' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5406', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5406'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5407' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5407', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5407'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5408' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5408', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5408'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5409' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5409', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5409'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5410' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5410', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5410'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5411' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5411', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5411'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5412' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5412', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5412'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5413' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5413', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5413'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5414' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5414', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5414'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5415' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5415', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5415'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5416' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5416', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5416'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5417' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5417', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5417'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5418' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5418', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5418'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5419' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5419', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5419'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5420' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5420', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5420'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5421' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5421', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5421'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5422' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5422', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5422'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5423' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5423', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5423'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5424' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5424', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5424'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5425' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5425', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5425'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5426' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5426', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5426'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5427' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5427', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5427'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5428' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5428', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5428'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5429' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5429', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5429'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5430' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5430', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5430'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5431' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5431', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5431'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5432' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5432', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5432'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5433' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5433', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5433'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5434' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5434', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5434'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5435' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5435', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5435'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5436' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5436', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5436'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5437' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5437', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5437'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5438' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5438', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5438'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5439' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5439', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5439'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '544' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '544', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '544'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5440' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5440', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5440'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5441' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5441', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5441'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5443' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5443', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5443'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5444' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5444', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5444'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5445' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5445', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5445'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5446' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5446', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5446'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5447' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5447', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5447'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5448' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5448', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5448'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5449' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5449', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5449'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5450' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5450', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5450'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5451' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5451', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5451'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5452' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5452', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5452'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5453' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5453', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5453'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5454' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5454', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5454'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5455' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5455', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5455'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5456' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5456', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5456'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5457' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5457', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5457'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5458' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5458', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5458'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5459' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5459', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5459'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '546' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '546', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '546'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5460' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5460', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5460'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5461' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5461', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5461'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5462' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5462', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5462'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5463' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5463', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5463'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5464' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5464', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5464'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5465' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5465', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5465'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5466' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5466', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5466'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5467' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5467', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5467'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5468' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5468', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5468'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5469' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5469', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5469'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '547' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '547', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '547'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5471' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5471', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5471'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5472' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5472', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5472'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5473' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5473', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5473'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5474' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5474', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5474'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5475' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5475', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5475'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5477' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5477', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5477'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5478' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5478', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5478'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5479' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5479', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5479'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '548' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '548', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '548'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5480' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5480', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5480'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5481' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5481', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5481'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5482' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5482', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5482'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5483' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5483', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5483'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5484' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5484', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5484'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5485' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5485', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5485'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5486' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5486', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5486'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5487' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5487', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5487'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5488' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5488', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5488'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5489' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5489', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5489'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '549' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '549', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '549'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5490' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5490', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5490'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5491' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5491', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5491'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5492' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5492', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5492'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5493' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5493', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5493'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5494' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5494', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5494'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5495' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5495', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5495'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5496' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5496', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5496'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5497' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5497', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5497'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5498' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5498', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5498'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5499' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5499', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5499'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5500' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5500', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5500'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5501' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5501', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5501'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5502' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5502', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5502'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5503' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5503', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5503'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5504' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5504', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5504'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5505' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5505', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5505'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5506' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5506', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5506'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5507' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5507', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5507'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5508' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5508', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5508'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5509' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5509', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5509'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '551' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '551', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '551'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5510' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5510', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5510'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5512' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5512', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5512'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5513' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5513', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5513'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5514' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5514', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5514'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5515' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5515', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5515'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5516' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5516', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5516'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5517' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5517', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5517'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5518' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5518', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5518'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5519' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5519', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5519'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '552' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '552', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '552'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5520' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5520', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5520'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5521' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5521', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5521'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5522' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5522', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5522'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5523' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5523', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5523'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5524' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5524', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5524'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5525' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5525', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5525'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5526' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5526', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5526'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5527' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5527', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5527'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5528' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5528', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5528'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5529' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5529', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5529'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '553' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '553', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '553'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5530' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5530', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5530'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5531' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5531', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5531'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5532' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5532', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5532'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5533' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5533', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5533'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5534' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5534', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5534'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5535' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5535', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5535'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5536' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5536', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5536'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5537' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5537', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5537'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5538' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5538', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5538'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5539' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5539', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5539'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '554' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '554', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '554'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5540' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5540', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5540'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5541' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5541', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5541'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5542' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5542', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5542'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5543' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5543', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5543'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5544' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5544', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5544'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5545' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5545', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5545'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5546' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5546', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5546'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5547' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5547', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5547'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5548' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5548', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5548'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5549' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5549', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5549'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '555' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '555', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '555'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5550' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5550', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5550'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5551' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5551', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5551'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5552' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5552', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5552'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5553' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5553', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5553'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5554' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5554', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5554'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5555' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5555', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5555'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5556' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5556', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5556'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5557' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5557', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5557'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5558' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5558', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5558'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5559' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5559', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5559'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '556' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '556', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '556'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5560' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5560', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5560'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5561' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5561', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5561'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5562' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5562', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5562'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5563' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5563', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5563'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5564' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5564', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5564'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5565' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5565', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5565'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5566' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5566', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5566'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5567' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5567', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5567'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5568' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5568', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5568'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5569' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5569', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5569'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '557' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '557', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '557'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5570' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5570', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5570'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5571' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5571', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5571'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5572' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5572', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5572'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5573' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5573', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5573'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5574' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5574', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5574'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5575' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5575', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5575'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5576' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5576', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5576'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5577' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5577', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5577'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5578' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5578', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5578'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5579' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5579', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5579'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '558' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '558', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '558'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5580' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5580', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5580'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5581' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5581', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5581'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5582' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5582', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5582'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5583' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5583', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5583'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5584' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5584', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5584'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5585' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5585', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5585'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5586' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5586', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5586'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5587' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5587', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5587'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5588' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5588', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5588'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5589' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5589', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5589'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '559' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '559', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '559'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5590' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5590', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5590'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5591' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5591', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5591'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5592' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5592', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5592'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5593' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5593', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5593'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5594' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5594', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5594'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5595' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5595', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5595'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5596' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5596', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5596'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5597' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5597', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5597'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5598' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5598', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5598'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5599' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5599', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5599'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5600' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5600', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5600'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5601' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5601', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5601'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5602' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5602', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5602'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5603' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5603', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5603'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5604' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5604', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5604'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5605' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5605', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5605'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5606' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5606', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5606'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5607' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5607', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5607'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5608' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5608', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5608'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5609' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5609', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5609'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5610' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5610', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5610'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5611' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5611', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5611'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5612' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5612', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5612'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5613' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5613', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5613'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5614' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5614', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5614'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5615' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5615', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5615'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5616' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5616', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5616'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5617' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5617', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5617'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5618' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5618', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5618'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5619' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5619', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5619'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '562' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '562', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '562'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5620' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5620', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5620'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5621' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5621', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5621'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5622' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5622', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5622'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5623' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5623', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5623'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5624' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5624', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5624'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5625' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5625', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5625'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5626' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5626', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5626'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5627' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5627', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5627'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5628' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5628', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5628'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5629' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5629', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5629'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '563' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '563', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '563'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5630' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5630', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5630'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5631' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5631', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5631'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5632' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5632', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5632'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5633' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5633', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5633'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5634' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5634', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5634'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5635' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5635', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5635'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5636' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5636', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5636'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5637' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5637', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5637'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '5638' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '5638', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '5638'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '565' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '565', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '565'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '566' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '566', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '566'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '569' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '569', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '569'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '570' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '570', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '570'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '571' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '571', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '571'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '572' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '572', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '572'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '573' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '573', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '573'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '574' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '574', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '574'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '575' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '575', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '575'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '576' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '576', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '576'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '578' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '578', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '578'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '579' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '579', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '579'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '580' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '580', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '580'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '581' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '581', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '581'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '582' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '582', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '582'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '584' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '584', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '584'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '586' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '586', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '586'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '587' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '587', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '587'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '588' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '588', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '588'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '589' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '589', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '589'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '590' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '590', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '590'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '592' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '592', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '592'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '595' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '595', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '595'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '596' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '596', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '596'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '597' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '597', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '597'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '598' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '598', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '598'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '599' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '599', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '599'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '600' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '600', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '600'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6001' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6001', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6001'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6002' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6002', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6002'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6003' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6003', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6003'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6004' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6004', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6004'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6005' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6005', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6005'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6007' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6007', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6007'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6008' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6008', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6008'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6009' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6009', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6009'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '601' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '601', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '601'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6010' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6010', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6010'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6011' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6011', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6011'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6012' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6012', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6012'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6013' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6013', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6013'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6014' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6014', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6014'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6015' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6015', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6015'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6016' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6016', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6016'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6017' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6017', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6017'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6018' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6018', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6018'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6019' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6019', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6019'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6020' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6020', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6020'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6021' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6021', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6021'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6022' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6022', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6022'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6023' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6023', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6023'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6024' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6024', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6024'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6025' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6025', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6025'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6026' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6026', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6026'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6027' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6027', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6027'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6028' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6028', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6028'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6029' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6029', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6029'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6030' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6030', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6030'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6031' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6031', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6031'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6032' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6032', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6032'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6033' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6033', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6033'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6034' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6034', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6034'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6035' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6035', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6035'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6036' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6036', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6036'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6037' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6037', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6037'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6038' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6038', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6038'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '604' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '604', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '604'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6040' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6040', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6040'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6041' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6041', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6041'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6042' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6042', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6042'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6043' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6043', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6043'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6044' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6044', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6044'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6045' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6045', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6045'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6046' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6046', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6046'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6047' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6047', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6047'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6048' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6048', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6048'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6049' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6049', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6049'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6050' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6050', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6050'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6051' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6051', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6051'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6052' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6052', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6052'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6053' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6053', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6053'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6054' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6054', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6054'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6055' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6055', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6055'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6056' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6056', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6056'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6057' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6057', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6057'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6058' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6058', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6058'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6059' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6059', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6059'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '606' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '606', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '606'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6060' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6060', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6060'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6061' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6061', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6061'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6062' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6062', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6062'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6063' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6063', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6063'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6064' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6064', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6064'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6065' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6065', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6065'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6066' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6066', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6066'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6067' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6067', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6067'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6068' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6068', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6068'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6069' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6069', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6069'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '607' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '607', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '607'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6070' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6070', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6070'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6071' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6071', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6071'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6072' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6072', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6072'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6073' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6073', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6073'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6074' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6074', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6074'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6075' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6075', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6075'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6076' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6076', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6076'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6077' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6077', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6077'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6078' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6078', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6078'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6079' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6079', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6079'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '608' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '608', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '608'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6080' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6080', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6080'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6081' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6081', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6081'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6082' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6082', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6082'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6083' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6083', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6083'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6084' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6084', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6084'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6085' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6085', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6085'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6086' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6086', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6086'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6087' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6087', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6087'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6088' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6088', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6088'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6089' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6089', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6089'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '609' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '609', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '609'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6090' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6090', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6090'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6091' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6091', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6091'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6092' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6092', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6092'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6093' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6093', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6093'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6094' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6094', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6094'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6095' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6095', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6095'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '6096' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '6096', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '6096'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '610' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '610', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '610'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '611' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '611', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '611'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '613' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '613', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '613'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '614' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '614', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '614'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '615' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '615', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '615'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '617' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '617', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '617'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '619' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '619', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '619'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '620' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '620', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '620'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '622' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '622', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '622'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '624' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '624', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '624'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '625' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '625', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '625'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '626' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '626', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '626'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '627' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '627', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '627'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '628' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '628', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '628'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '629' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '629', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '629'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '630' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '630', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '630'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '631' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '631', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '631'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '632' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '632', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '632'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '633' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '633', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '633'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '634' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '634', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '634'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '636' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '636', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '636'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '637' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '637', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '637'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '638' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '638', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '638'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '639' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '639', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '639'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '640' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '640', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '640'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '641' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '641', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '641'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '643' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '643', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '643'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '644' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '644', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '644'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '645' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '645', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '645'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '646' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '646', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '646'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '647' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '647', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '647'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '648' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '648', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '648'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '649' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '649', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '649'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '651' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '651', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '651'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '652' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '652', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '652'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '653' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '653', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '653'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '654' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '654', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '654'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '655' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '655', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '655'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '656' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '656', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '656'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '657' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '657', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '657'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '659' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '659', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '659'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '661' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '661', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '661'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '662' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '662', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '662'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '663' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '663', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '663'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '664' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '664', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '664'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '665' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '665', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '665'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '666' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '666', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '666'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '667' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '667', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '667'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '668' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '668', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '668'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '669' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '669', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '669'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '670' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '670', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '670'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '671' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '671', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '671'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '672' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '672', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '672'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '673' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '673', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '673'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '674' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '674', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '674'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '675' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '675', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '675'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '676' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '676', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '676'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '677' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '677', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '677'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '678' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '678', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '678'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '679' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '679', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '679'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '680' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '680', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '680'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '681' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '681', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '681'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '682' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '682', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '682'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '683' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '683', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '683'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '684' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '684', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '684'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '685' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '685', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '685'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '686' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '686', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '686'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '687' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '687', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '687'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '688' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '688', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '688'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '689' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '689', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '689'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '690' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '690', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '690'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '691' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '691', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '691'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '692' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '692', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '692'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '693' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '693', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '693'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '695' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '695', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '695'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '696' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '696', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '696'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '697' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '697', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '697'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '699' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '699', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '699'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '700' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '700', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '700'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7001' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7001', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7001'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7002' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7002', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7002'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7003' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7003', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7003'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7004' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7004', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7004'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7005' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7005', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7005'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7006' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7006', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7006'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7007' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7007', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7007'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7008' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7008', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7008'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7009' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7009', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7009'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '701' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '701', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '701'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7010' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7010', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7010'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7011' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7011', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7011'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7012' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7012', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7012'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7013' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7013', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7013'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7014' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7014', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7014'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7015' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7015', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7015'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7016' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7016', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7016'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7017' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7017', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7017'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7018' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7018', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7018'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7019' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7019', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7019'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '702' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '702', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '702'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7020' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7020', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7020'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7021' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7021', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7021'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7022' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7022', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7022'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7023' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7023', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7023'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7024' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7024', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7024'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7025' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7025', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7025'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7026' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7026', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7026'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7027' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7027', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7027'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7028' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7028', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7028'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7029' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7029', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7029'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7030' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7030', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7030'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7031' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7031', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7031'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7032' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7032', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7032'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7033' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7033', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7033'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7034' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7034', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7034'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7035' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7035', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7035'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7036' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7036', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7036'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7037' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7037', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7037'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7038' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7038', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7038'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7039' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7039', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7039'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7041' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7041', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7041'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7042' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7042', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7042'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7043' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7043', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7043'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7044' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7044', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7044'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '705' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '705', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '705'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '706' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '706', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '706'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '707' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '707', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '707'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '708' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '708', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '708'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '709' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '709', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '709'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '710' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '710', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '710'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '711' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '711', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '711'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '712' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '712', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '712'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '713' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '713', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '713'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '714' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '714', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '714'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '715' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '715', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '715'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '716' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '716', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '716'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '717' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '717', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '717'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '718' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '718', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '718'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '719' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '719', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '719'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '720' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '720', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '720'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '721' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '721', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '721'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '722' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '722', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '722'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '723' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '723', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '723'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '724' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '724', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '724'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '725' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '725', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '725'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '726' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '726', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '726'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '727' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '727', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '727'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '728' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '728', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '728'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '729' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '729', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '729'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '731' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '731', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '731'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '732' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '732', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '732'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '733' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '733', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '733'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '734' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '734', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '734'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '735' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '735', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '735'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '736' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '736', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '736'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '737' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '737', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '737'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '738' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '738', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '738'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '739' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '739', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '739'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '74' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '74', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '74'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '740' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '740', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '740'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '741' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '741', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '741'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '742' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '742', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '742'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '743' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '743', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '743'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '744' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '744', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '744'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '745' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '745', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '745'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '746' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '746', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '746'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '747' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '747', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '747'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '748' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '748', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '748'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '749' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '749', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '749'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '750' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '750', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '750'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '751' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '751', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '751'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '752' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '752', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '752'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '753' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '753', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '753'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '754' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '754', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '754'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '755' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '755', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '755'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '756' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '756', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '756'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '757' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '757', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '757'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '758' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '758', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '758'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '760' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '760', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '760'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7602' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7602', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7602'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7603' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7603', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7603'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7604' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7604', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7604'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7605' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7605', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7605'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7606' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7606', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7606'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7607' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7607', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7607'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7608' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7608', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7608'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7609' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7609', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7609'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '761' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '761', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '761'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '7610' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '7610', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '7610'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '762' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '762', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '762'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '763' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '763', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '763'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '764' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '764', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '764'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '765' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '765', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '765'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '766' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '766', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '766'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '767' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '767', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '767'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '768' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '768', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '768'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '769' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '769', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '769'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '770' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '770', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '770'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '771' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '771', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '771'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '772' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '772', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '772'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '773' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '773', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '773'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '774' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '774', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '774'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '775' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '775', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '775'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '776' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '776', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '776'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '777' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '777', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '777'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '778' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '778', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '778'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '779' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '779', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '779'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '780' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '780', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '780'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '781' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '781', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '781'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '782' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '782', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '782'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '783' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '783', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '783'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '784' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '784', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '784'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '785' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '785', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '785'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '786' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '786', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '786'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '787' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '787', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '787'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '788' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '788', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '788'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '790' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '790', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '790'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '792' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '792', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '792'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '794' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '794', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '794'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '795' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '795', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '795'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '796' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '796', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '796'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '797' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '797', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '797'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '798' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '798', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '798'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '799' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '799', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '799'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '800' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '800', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '800'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '801' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '801', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '801'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '803' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '803', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '803'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '804' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '804', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '804'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '805' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '805', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '805'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '806' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '806', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '806'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '807' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '807', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '807'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '808' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '808', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '808'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '809' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '809', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '809'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '810' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '810', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '810'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '811' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '811', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '811'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '812' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '812', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '812'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '813' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '813', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '813'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '814' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '814', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '814'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '815' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '815', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '815'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '816' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '816', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '816'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '817' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '817', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '817'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '818' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '818', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '818'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '819' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '819', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '819'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '820' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '820', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '820'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '821' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '821', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '821'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '822' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '822', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '822'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '823' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '823', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '823'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '825' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '825', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '825'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '828' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '828', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '828'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '829' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '829', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '829'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '830' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '830', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '830'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '831' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '831', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '831'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '832' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '832', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '832'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '833' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '833', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '833'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '834' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '834', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '834'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '835' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '835', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '835'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '836' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '836', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '836'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '837' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '837', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '837'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '838' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '838', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '838'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '839' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '839', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '839'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '840' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '840', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '840'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '841' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '841', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '841'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '842' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '842', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '842'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '843' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '843', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '843'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '844' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '844', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '844'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '845' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '845', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '845'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '846' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '846', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '846'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '847' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '847', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '847'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '848' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '848', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '848'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '849' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '849', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '849'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '850' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '850', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '850'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '851' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '851', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '851'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '852' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '852', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '852'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '853' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '853', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '853'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '854' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '854', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '854'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '855' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '855', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '855'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '856' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '856', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '856'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '857' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '857', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '857'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '858' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '858', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '858'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '859' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '859', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '859'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '860' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '860', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '860'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '862' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '862', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '862'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '863' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '863', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '863'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '864' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '864', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '864'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '867' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '867', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '867'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '868' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '868', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '868'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '869' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '869', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '869'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '870' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '870', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '870'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '871' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '871', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '871'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '872' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '872', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '872'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '873' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '873', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '873'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '875' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '875', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '875'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '876' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '876', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '876'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '877' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '877', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '877'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '878' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '878', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '878'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '879' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '879', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '879'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '880' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '880', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '880'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '881' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '881', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '881'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '882' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '882', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '882'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '883' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '883', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '883'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '884' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '884', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '884'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '885' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '885', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '885'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '886' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '886', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '886'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '887' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '887', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '887'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '888' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '888', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '888'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '889' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '889', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '889'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '890' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '890', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '890'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '891' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '891', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '891'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '892' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '892', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '892'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '895' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '895', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '895'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '896' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '896', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '896'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '897' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '897', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '897'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '898' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '898', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '898'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '899' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '899', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '899'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '900' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '900', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '900'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9001' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9001', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9001'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9002' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9002', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9002'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9003' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9003', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9003'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9004' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9004', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9004'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9005' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9005', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9005'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9006' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9006', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9006'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9007' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9007', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9007'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9008' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9008', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9008'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9009' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9009', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9009'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '901' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '901', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '901'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9010' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9010', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9010'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9011' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9011', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9011'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9012' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9012', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9012'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9013' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9013', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9013'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9014' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9014', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9014'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9015' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9015', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9015'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9016' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9016', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9016'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9017' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9017', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9017'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9018' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9018', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9018'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9019' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9019', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9019'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '902' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '902', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '902'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9020' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9020', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9020'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9021' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9021', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9021'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9022' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9022', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9022'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9023' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9023', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9023'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9024' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9024', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9024'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9025' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9025', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9025'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9026' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9026', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9026'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9027' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9027', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9027'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9028' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9028', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9028'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9029' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9029', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9029'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '903' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '903', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '903'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9030' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9030', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9030'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9031' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9031', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9031'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9033' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9033', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9033'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9034' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9034', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9034'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9035' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9035', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9035'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9037' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9037', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9037'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9038' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9038', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9038'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9039' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9039', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9039'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '904' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '904', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '904'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9040' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9040', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9040'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9041' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9041', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9041'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9042' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9042', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9042'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9043' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9043', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9043'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9044' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9044', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9044'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9045' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9045', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9045'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9046' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9046', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9046'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9047' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9047', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9047'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9048' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9048', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9048'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9049' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9049', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9049'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '905' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '905', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '905'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9050' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9050', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9050'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9051' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9051', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9051'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9052' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9052', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9052'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9053' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9053', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9053'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9054' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9054', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9054'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9055' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9055', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9055'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '906' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '906', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '906'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '907' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '907', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '907'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '908' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '908', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '908'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '909' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '909', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '909'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '910' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '910', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '910'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '911' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '911', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '911'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '912' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '912', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '912'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '913' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '913', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '913'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '914' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '914', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '914'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '915' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '915', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '915'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '916' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '916', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '916'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '917' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '917', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '917'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '918' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '918', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '918'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '919' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '919', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '919'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '920' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '920', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '920'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '921' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '921', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '921'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '922' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '922', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '922'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '925' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '925', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '925'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '926' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '926', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '926'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '927' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '927', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '927'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '928' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '928', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '928'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '929' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '929', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '929'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '930' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '930', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '930'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '931' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '931', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '931'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '932' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '932', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '932'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '933' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '933', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '933'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '934' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '934', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '934'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '935' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '935', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '935'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '936' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '936', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '936'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '937' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '937', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '937'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '938' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '938', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '938'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '939' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '939', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '939'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '940' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '940', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '940'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '941' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '941', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '941'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '942' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '942', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '942'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '943' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '943', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '943'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '944' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '944', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '944'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '945' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '945', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '945'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '946' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '946', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '946'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '947' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '947', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '947'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '948' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '948', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '948'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '949' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '949', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '949'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '950' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '950', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '950'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '951' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '951', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '951'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '952' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '952', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '952'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '953' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '953', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '953'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '954' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '954', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '954'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '955' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '955', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '955'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '956' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '956', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '956'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '957' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '957', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '957'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '958' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '958', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '958'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '959' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '959', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '959'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '960' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '960', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '960'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '961' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '961', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '961'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '962' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '962', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '962'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '963' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '963', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '963'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '964' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '964', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '964'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '965' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '965', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '965'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '966' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '966', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '966'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '967' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '967', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '967'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '968' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '968', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '968'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '969' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '969', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '969'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '970' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '970', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '970'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '971' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '971', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '971'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '972' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '972', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '972'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '973' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '973', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '973'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '974' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '974', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '974'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '975' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '975', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '975'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '976' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '976', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '976'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '977' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '977', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '977'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '978' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '978', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '978'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '979' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '979', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '979'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '980' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '980', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '980'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '981' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '981', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '981'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '982' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '982', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '982'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '983' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '983', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '983'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '984' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '984', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '984'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '985' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '985', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '985'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '986' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '986', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '986'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '987' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '987', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '987'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '988' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '988', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '988'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '989' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '989', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '989'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '990' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '990', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '990'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '991' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '991', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '991'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '992' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '992', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '992'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '993' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '993', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '993'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '994' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '994', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '994'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '995' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '995', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '995'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '997' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '997', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '997'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '999' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '999', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '999'  
If Not Exists ( Select * From CFDI_Aduanas_Patentes Where Clave = '9999' )  Insert Into CFDI_Aduanas_Patentes (  Clave, Status )  Values ( '9999', 'A' ) 
 Else Update CFDI_Aduanas_Patentes Set Status = 'A' Where Clave = '9999'  
Go--#SQL 

