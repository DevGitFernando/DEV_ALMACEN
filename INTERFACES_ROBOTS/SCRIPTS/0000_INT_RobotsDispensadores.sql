Set NoCount On
Go--#SQL   

-----		Drop Table    INT_RobotDispensador_Interfaces 
-----		Drop Table    INT_RobotDispensador 

------------------------------------------------------------------------------------------------------------ 
If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_RobotDispensador_Interfaces' and xType = 'U' ) 
Begin 
	Create Table INT_RobotDispensador_Interfaces 
	(
		Interface smallint Not Null Default 0, 
		NombreInterface varchar(100) Not Null Default '' UNIQUE, 
		Status varchar(1) Not Null Default 'A'  
	)

	Alter Table INT_RobotDispensador_Interfaces 
		Add Constraint PK_INT_RobotDispensador_Interfaces Primary Key ( Interface ) 
End 
Go--#SQL 

If Not Exists ( Select * From INT_RobotDispensador_Interfaces Where Interface = 1 )  Insert Into INT_RobotDispensador_Interfaces (  Interface, NombreInterface, Status )  Values ( 1, 'Medimat', 'A' )  Else Update INT_RobotDispensador_Interfaces Set NombreInterface = 'Medimat', Status = 'A' Where Interface = 1  
If Not Exists ( Select * From INT_RobotDispensador_Interfaces Where Interface = 2 )  Insert Into INT_RobotDispensador_Interfaces (  Interface, NombreInterface, Status )  Values ( 2, 'ATP2', 'A' )  Else Update INT_RobotDispensador_Interfaces Set NombreInterface = 'ATP2', Status = 'A' Where Interface = 2  
If Not Exists ( Select * From INT_RobotDispensador_Interfaces Where Interface = 3 )  Insert Into INT_RobotDispensador_Interfaces (  Interface, NombreInterface, Status )  Values ( 3, 'IGPI', 'A' )  Else Update INT_RobotDispensador_Interfaces Set NombreInterface = 'IGPI', Status = 'A' Where Interface = 3  
Go--#SQL 


--	 drop table INT_RobotDispensador 

--	 drop table INT_RobotDispensador_Interfaces 




------------------------------------------------------------------------------------------------------------ 
If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_RobotDispensador' and xType = 'U' ) 
Begin 
	Create Table INT_RobotDispensador 
	(
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		Interface smallint Not Null Default 0, 
		NombreInterface varchar(100) Not Null Default '' UNIQUE, 
		Status varchar(1) Not Null Default 'A'  
	)

	Alter Table INT_RobotDispensador 
		Add Constraint PK_INT_RobotDispensador Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Interface ) 

	Alter Table INT_RobotDispensador 
		Add Constraint FK_INT_RobotDispensador___INT_RobotDispensador_Interfaces 
		Foreign Key ( Interface ) References INT_RobotDispensador_Interfaces ( Interface ) 

End 
Go--#SQL 


Set  NoCount Off 

Declare 
	@sEmpresa varchar(3), 
	@sEstado varchar(2), 
	@sFarmacia varchar(4), 
	@iInterface int, 
	@sNombreInterface varchar(100)    

	Set @sEmpresa = '002' 
	Set @sEstado = '09' 
	Set @sFarmacia = '0104' 
	Set @iInterface = 0 
	Set @sNombreInterface = '' 


	If exists ( Select * From sysobjects (NoLock) Where Name = 'tmp_ListaFarmacias__Robots' and xType = 'U' ) Drop Table tmp_ListaFarmacias__Robots 

	Select top 0 @sEmpresa as Empresa, @sEstado as Estado, @sFarmacia as Farmacia, 0 as Interface, cast('' as varchar(100)) as NombreInterface   
	Into tmp_ListaFarmacias__Robots 



	Insert Into tmp_ListaFarmacias__Robots Select '001', '13', '0114', 3, 'IGPI'  
--	Insert Into tmp_ListaFarmacias__Robots Select '001', '11', '0088', 1, 'MEDIMAT' 
--	Insert Into tmp_ListaFarmacias__Robots Select '001', '21', '3188', 1, 'MEDIMAT' 
--	Insert Into tmp_ListaFarmacias__Robots Select '001', '21', '3224', 1, 'MEDIMAT' 
--	Insert Into tmp_ListaFarmacias__Robots Select '001', '21', '3366', 1, 'MEDIMAT' 
--	Insert Into tmp_ListaFarmacias__Robots Select '001', '21', '3406', 1, 'MEDIMAT' 
--	Insert Into tmp_ListaFarmacias__Robots Select '001', '21', '3412', 1, 'MEDIMAT' 
--	Insert Into tmp_ListaFarmacias__Robots Select '002', '09', '0104', 1, 'MEDIMAT' 



	Declare #cursorBeneficiarios  
	Cursor For 
		Select 
			Empresa, Estado, Farmacia, Interface, NombreInterface   
		From tmp_ListaFarmacias__Robots D (NoLock) 
	Open #cursorBeneficiarios 
	FETCH NEXT FROM #cursorBeneficiarios Into @sEmpresa, @sEstado, @sFarmacia, @iInterface, @sNombreInterface  
		WHILE @@FETCH_STATUS = 0 
			Begin 
				---- Print @sFarmacia 
				If Exists ( Select top 1 * From FarmaciaProductos (NoLock) Where IdEmpresa = @sEmpresa and IdEstado = @sEstado and IdFarmacia = @sFarmacia ) 
				Begin  

					If Not Exists ( Select * From INT_RobotDispensador Where IdEmpresa = @sEmpresa and IdEstado = @sEstado and IdFarmacia = @sFarmacia  and Interface = @iInterface )  
						Insert Into INT_RobotDispensador (  IdEmpresa, IdEstado, IdFarmacia, Interface, NombreInterface, Status )  Values ( @sEmpresa, @sEstado, @sFarmacia, @iInterface, @sNombreInterface, 'A' )  
					Else 
						Update INT_RobotDispensador Set NombreInterface = @sNombreInterface, Status = 'A' Where IdEmpresa = @sEmpresa and IdEstado = @sEstado and IdFarmacia = @sFarmacia and Interface = @iInterface   

				End 
				
				FETCH NEXT FROM #cursorBeneficiarios Into @sEmpresa, @sEstado, @sFarmacia, @iInterface, @sNombreInterface  
			End 
	Close #cursorBeneficiarios 
	Deallocate #cursorBeneficiarios 
		


	If exists ( Select * From sysobjects (NoLock) Where Name = 'tmp_ListaFarmacias__Robots' and xType = 'U' ) Drop Table tmp_ListaFarmacias__Robots 

Go--#SQL 


