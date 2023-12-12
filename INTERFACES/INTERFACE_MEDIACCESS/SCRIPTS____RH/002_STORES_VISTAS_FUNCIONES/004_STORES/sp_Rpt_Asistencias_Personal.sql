If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Rpt_Asistencias_Personal' and xType = 'P' )
   Drop Proc sp_Rpt_Asistencias_Personal 
Go--#SQL 

		----		Exec sp_Rpt_Asistencias_Personal '09', '*', '2016-11-20', '2016-11-22' 

		----		Exec sp_Rpt_Asistencias_Personal '11', '00001302', '2014-05-26', '2014-05-30'

Create Proc sp_Rpt_Asistencias_Personal 
(
	@IdEstado varchar(2) = '11', @IdPersonal varchar(10) = '*', @FechaInicio varchar(10) = '2014-02-01', @FechaFin varchar(10) = '2014-05-30'
) 
With Encryption 
As 
Begin
SET LANGUAGE Español

Declare @sSql varchar(7500), @sIdPersonal varchar(4), @NombreCol varchar(100), 
		@FechaIni datetime, @FechaFinal datetime, @sDiaSemana varchar(30),
		@iDomingos int, @iDiasRevision int, @NombreMes varchar(20)

	Set @sSql = ''
	Set @sIdPersonal = '' 
	Set @NombreCol = ''
	Set @FechaIni = CONVERT(datetime, @FechaInicio, 120) 
	Set @FechaFinal = CONVERT(datetime, @FechaFin, 120) 	
	Set @sDiaSemana = ''
	
	Set @iDomingos = 0
	Set @iDiasRevision = 0
	
	set @NombreMes = ''
	
	
	-----  Se calculan los dias de revision y cuantos domingos  ----------------------
	Set @iDomingos = (Select dbo.fg_NumeroDeDomingos(@FechaIni, @FechaFinal))
	
	--Set @iDiasRevision = (( Select DATEDIFF(DAY, @FechaIni, @FechaFinal) + 1 ) - @iDomingos )
	Set @iDiasRevision = (( Select DATEDIFF(DAY, @FechaIni, @FechaFinal) + 1 ) )	
	-----------------------------------------------------------------------
	
	
	Select Distinct C.IdPersonal, P.NombreCompleto, P.Puesto, P.Departamento, P.Farmacia
	Into #tmpRpt_Asistencias_Final
	From Checador_Personal C (Nolock)
	Inner Join vw_Personal P (Nolock) On (C.IdPersonal = P.IdPersonal)
	Where P.IdEstado = @IdEstado
	and CONVERT(varchar(10), C.FechaRegistro, 120) Between @FechaInicio and @FechaFin
	--and C.IdPersonal = '00001215' 
	
	
	If @IdPersonal <> '*'
	Begin
		Delete From #tmpRpt_Asistencias_Final Where IdPersonal <> @IdPersonal
	End
	
	Select Distinct C.IdPersonal
	Into #tmpPersonal
	From Checador_Personal C (Nolock)
	Inner Join vw_Personal P (Nolock) On (C.IdPersonal = P.IdPersonal)
	Where P.IdEstado = @IdEstado
	and CONVERT(varchar(10), C.FechaRegistro, 120) Between @FechaInicio and @FechaFin
	--and C.IdPersonal = '00001215' 
    
    
    --Declare tmpCol Cursor For Select IdPersonal from #tmpPersonal 	
    --Open tmpCol
    --FETCH NEXT FROM tmpCol Into @sIdPersonal 
        --WHILE @@FETCH_STATUS = 0
        WHILE @FechaIni <= @FechaFinal
        BEGIN
           
           --if @FechaIni <= @FechaFinal
           --Begin
			   Set @NombreCol = ''
			   set @NombreMes = ''
	           
	           set @NombreMes = ( Select dbo.fg_NombresDeMes(@FechaIni) + '_' )
	           
			   Set @NombreCol = ( (@NombreMes) + ( Select DATENAME (DW, @FechaIni) ) + '_' + (CONVERT(varchar(2), datepart(dd, @FechaIni))))
			   Set @sDiaSemana = ( Select DATENAME (DW, @FechaIni) )
			   
			   --- Agregar la Columna Correspondiente 
			   Set @sSql = ' Alter Table ' + '#tmpRpt_Asistencias_Final' + ' Add ' + (@NombreCol) + ' varchar(50) not null Default ' + CHAR(39) + '' + CHAR(39) 
			   Exec(@sSql)
	           
	    --       If @sDiaSemana = 'Domingo'
					--Begin
					--	--- Agregar la Informacion Relacionada 
					--   Set @sSql = ' Update E Set E.' + (@NombreCol) + ' = ' + char(39) + 'Domingo' + char(39) + ' 
					--		From #tmpRpt_Asistencias_Final E '						
					--   Exec(@sSql)
					--   --print(@sSql)
					--End
	    --       Else
					--Begin
						--- Agregar la Informacion Relacionada 
					   Set @sSql = ' Update E Set E.' + (@NombreCol) + ' = ' + CHAR(39) + 'E : ' + CHAR(39) + ' + ' + 'CONVERT(varchar(10), P.FechaRegistro, 108)
							From #tmpRpt_Asistencias_Final E  
							Inner join Checador_Personal P (NoLock) ' + 
							'	On ( E.IdPersonal =  P.IdPersonal ) ' + 
							' Where P.TipoRegistro = 1 AND CONVERT(varchar(10), P.FechaRegistro, 120) = ' + 
							CHAR(39) + (CONVERT(varchar(10), @FechaIni, 120)) + CHAR(39)
					   Exec(@sSql)
					   --print(@sSql)
					   
					   Set @sSql = ' Update E Set E.' + (@NombreCol) + ' = ( E.' + (@NombreCol) + ' + ' + 
							CHAR(39) + '   S : ' + CHAR(39) + ' + ' + 'CONVERT(varchar(10), P.FechaRegistro, 108) )
							From #tmpRpt_Asistencias_Final E  
							Inner join Checador_Personal P (NoLock) ' + 
							'	On ( E.IdPersonal =  P.IdPersonal ) ' + 
							' Where P.TipoRegistro = 2 AND CONVERT(varchar(10), P.FechaRegistro, 120) = ' + 
							CHAR(39) + (CONVERT(varchar(10), @FechaIni, 120)) + CHAR(39)
					   Exec(@sSql)
					   --print(@sSql)
					   
					   Set @sSql = ' Update E Set E.' + (@NombreCol) + ' = P.Incidencia
							From #tmpRpt_Asistencias_Final E  
							Inner join vw_Personal_Incidencias P (NoLock) ' + 
							'	On ( E.IdPersonal =  P.IdPersonal ) ' + 
							' Where ' + CHAR(39) + (CONVERT(varchar(10), @FechaIni, 120)) + CHAR(39) + 
							' Between CONVERT(varchar(10), P.FechaInicio, 120) and CONVERT(varchar(10), P.FechaFin, 120)  '
					   Exec(@sSql)
					   --print(@sSql)
					   
					--End
			   
			   Set @sSql = ' Update E Set E.' + (@NombreCol) + ' = ' + char(39) + 'Falta' + char(39) + ' 
							 From #tmpRpt_Asistencias_Final E ' +
					       ' Where E.' + (@NombreCol) + ' = ' + char(39) + '' + char(39) 
			   Exec(@sSql)
			   --print(@sSql)
			   
			   Set @FechaIni = @FechaIni + 1
           	--End          
           
           --FETCH NEXT FROM tmpCol Into @sIdPersonal 
        END
    --Close tmpCol
    --Deallocate tmpCol 
    
    ---			sp_Rpt_Asistencias_Personal
    
    
    ---- PARA SACAR ASISTENCIAS  ------------------------------------
	Select DISTINCT C.IdPersonal, Count(DISTINCT convert(varchar(10), C.FechaRegistro, 120)) as Asistencias
	Into #tmpAsistencias_Personal
	From Checador_Personal C (Nolock)
	Inner Join CatPersonal P (Nolock) On (C.IdPersonal = P.IdPersonal)
	Where P.IdEstado = @IdEstado and CONVERT(varchar(10), C.FechaRegistro, 120) Between @FechaInicio and @FechaFin
	and DATEPART(DW, C.FechaRegistro ) not in (7)	 	
	Group By C.IdPersonal
    
    ---- PARA SACAR RETARDOS ------------------------------------
	Select DISTINCT C.IdPersonal, Count(DISTINCT convert(varchar(10), C.FechaRegistro, 120)) as Retardos
	Into #tmpRetardos_Personal
	From Checador_Personal C (Nolock)
	Inner Join CatPersonal P (Nolock) On (C.IdPersonal = P.IdPersonal)
	Where P.IdEstado = @IdEstado and CONVERT(varchar(10), C.FechaRegistro, 120) Between @FechaInicio and @FechaFin
	and CONVERT(time, C.FechaRegistro) >= '09:16:00.0000000' and C.TipoRegistro = 1
	Group By C.IdPersonal
	
	-----  SE AGREGAN LOS CAMPOS DE ASISTENCIAS, RETARDOS Y FALTAS A LA TABLA FINAL  -----
	Alter Table #tmpRpt_Asistencias_Final  Add Asistencias int not null Default 0
	Alter Table #tmpRpt_Asistencias_Final  Add Retardos int not null Default 0
	Alter Table #tmpRpt_Asistencias_Final  Add Faltas int not null Default 0
	
	
	------ SE ACTUALIZAN LAS ASISTENCIAS TOTALES POR PERSONAL EN LA TABLA FINAL  -----------
	Update T Set T.Asistencias = A.Asistencias
	From #tmpRpt_Asistencias_Final T (NOLOCK)
	Inner Join #tmpAsistencias_Personal A (NOLOCK) On ( T.IdPersonal =  A.IdPersonal )
	
	------ SE ACTUALIZAN LOS RETARDOS TOTALES POR PERSONAL EN LA TABLA FINAL  -----------
	Update T Set T.Retardos = A.Retardos
	From #tmpRpt_Asistencias_Final T (NOLOCK)
	Inner Join #tmpRetardos_Personal A (NOLOCK) On ( T.IdPersonal =  A.IdPersonal )
	
	
	------ SE ACTUALIZAN LAS FALTAS TOTALES POR PERSONAL EN LA TABLA FINAL  -----------
	Update T Set T.Faltas = ( @iDiasRevision - T.Asistencias )
	From #tmpRpt_Asistencias_Final T (NOLOCK)
	
	------  SE OBTIENEN LAS INCIDENCIAS DEL PERSONAL  --------------------------------
	Select Distinct I.IdPersonal, COUNT(I.IdIncidencia) as Faltas
	Into #tmpFaltasJustificadas
	From CatPersonal_Incidencias I (NoLock) 
	Inner Join #tmpRpt_Asistencias_Final F (Nolock) On ( I.IdPersonal =  F.IdPersonal )
	Where ( convert(varchar(10), I.FechaInicio, 120) Between @FechaInicio and @FechaFin
	OR convert(varchar(10), I.FechaFin, 120) Between @FechaInicio and @FechaFin )
	Group By I.IdPersonal
	
	
	---------  SE RESTAN LAS INCIDENCIAS A LAS FALTAS QUE SE JUSTIFICAN   ----------------
	
	Update T Set T.Faltas = ( T.Faltas - A.Faltas )
	From #tmpRpt_Asistencias_Final T (NOLOCK)
	Inner Join #tmpFaltasJustificadas A (NOLOCK) On ( T.IdPersonal =  A.IdPersonal )
	
	------------------------------------------------------------------------------------------------------------------	
    
    Select * From #tmpRpt_Asistencias_Final (Nolock)
    
    ---			sp_Rpt_Asistencias_Personal
    
End 
Go--#SQL    	
