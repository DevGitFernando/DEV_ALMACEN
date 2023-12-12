If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_Ctrl_Reubicaciones_Monitor' And xType = 'P' )
	Drop Proc spp_Rpt_Ctrl_Reubicaciones_Monitor
Go--#SQL

Create Procedure spp_Rpt_Ctrl_Reubicaciones_Monitor
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia Varchar(4) = '0005'
) 
With Encryption 	
As
Begin

	--Drop Table #Rpt_Ctrl_Reubicaciones
	Select Top 0 CAST('' As Varchar(30))  As Folio, CAST('' As Varchar(30))  As 'Fecha de Registro', CAST('' As Varchar(300)) 'Personal Asignado',
		CAST('' As Varchar(100))  As 'Fecha de Confirmación', CAST('' As Varchar(13)) As Status
	Into #Rpt_Ctrl_Reubicaciones


	Insert Into #Rpt_Ctrl_Reubicaciones
	Select Folio_Inv, Convert(Varchar(40), FechaRegistro, 120), PersonalAsignado, '' ,
		(Case When Confirmada = 1 then 'Confirmada' else 'Sin Confirmar' End) As Status
	From vw_Ctrl_Reubicaciones
	Where Confirmada = 0
	
	
	Insert Into #Rpt_Ctrl_Reubicaciones
	Select Folio_Inv, Convert(Varchar(40), FechaRegistro, 120), PersonalAsignado, Convert(Varchar(40), FechaConfirmacion, 120),
	(Case When Confirmada = 1 then 'Confirmada' else 'Sin Confirmar' End) As Status
	From vw_Ctrl_Reubicaciones
	Where Confirmada = 1  And DATEDIFF(MI, FechaRegistro, Getdate()) < 15


	Select *
	From #Rpt_Ctrl_Reubicaciones
	Order by 'Fecha de Registro'
	
End
Go--#SQL

