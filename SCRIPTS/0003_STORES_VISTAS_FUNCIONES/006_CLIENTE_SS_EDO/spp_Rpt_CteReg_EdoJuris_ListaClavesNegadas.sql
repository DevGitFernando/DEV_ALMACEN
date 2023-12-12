/* 

If Exists ( Select Name From tempdb..sysobjects (nolock) Where Name = '##tmp_Mes' and xType = 'U' ) 
   Drop Table tempdb..##tmp_Mes 
Go--#xxSQL 

If Exists ( Select Name From sysobjects (nolock) Where Name = '##tmpClaves_Negadas' and xType = 'U' ) 
   Drop Table ##tmpClaves_Negadas 
Go--#xxSQL 	   


*/ 
 
--		Exec spp_Rpt_CteReg_EdoJuris_ListaClavesNegadas '21', '*', '*', '2012-01-26', 0, 0, 0, 0       


--		Exec spp_Rpt_CteReg_EdoJuris_ListaClavesNegadas '21', '*', '2011-09-26', 0, 0, 0, 0     
 	   
 	   
--------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_CteReg_EdoJuris_ListaClavesNegadas' and xType = 'P' ) 
   Drop Proc spp_Rpt_CteReg_EdoJuris_ListaClavesNegadas 
Go--#SQL 

Create Proc spp_Rpt_CteReg_EdoJuris_ListaClavesNegadas 
( 
	@IdEstado varchar(2) = '21', @IdJurisdiccion varchar(3) = '*', @IdFarmacia varchar(4) = '*', 
	@FechaProceso varchar(10) = '2011-09-05', @bEsGeneral int = 0, @ProcesoPorDia int = 0, @MostrarTodo int = 0, 
	@EsRevision smallint = 1    
) 
With Encryption
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

Declare 
	@iEjecutar int 

Declare 
	@Fecha datetime, 
	@DiasMes int, 
	@Año int,  
	@Mes int   


	Select @Fecha = cast(@FechaProceso as datetime) 
	Select @Año = datepart(yy, @Fecha) 
	Select @Mes = datepart(mm, @Fecha) 

---------- Obtener lista de Farmacias a Procesas  
	Select IdEstado, @IdEstado as Estado, IdJurisdiccion, Descripcion as Jurisdiccion 
	Into #tmpJuris 
	From CatJurisdicciones 
	Where IdEstado = @IdEstado and IdJurisdiccion = @IdJurisdiccion 

	if @IdJurisdiccion = '*' 
		Begin 
			Insert Into #tmpJuris 
			Select IdEstado, @IdEstado as Estado, IdJurisdiccion as IdJuris, Descripcion as Jurisdiccion 
			From CatJurisdicciones 
			Where IdEstado = @IdEstado -- and IdJurisdiccion = @IdJurisdiccion 	   
	    End 

	
	Select F.IdEstado, F.IdJurisdiccion, F.IdFarmacia, F.NombreFarmacia as Farmacia  
	Into #tmpFarmacias 
	From CatFarmacias F (NoLock) 
	Inner Join #tmpJuris J (NoLock) On ( F.IdEstado = J.IdEstado and F.IdJurisdiccion = J.IdJurisdiccion ) 

	
--- Proceso invocado desde Farmacia 
	If @IdFarmacia <> '*' 
	   Delete From #tmpFarmacias Where IdFarmacia <> @IdFarmacia 
	
	-- where F.IdFarmacia = 188 
------------------------------------------ 

	Set @iEjecutar = 0 
	Select @iEjecutar = count(*) 
		   From Rpt_CTE_Sansiones S 
		   Inner Join #tmpFarmacias F On ( S.IdEstado = F.IdEstado and S.IdJurisdiccion = F.IdJurisdiccion and S.IdFarmacia = F.IdFarmacia ) 
		   Where S.Año = @Año and S.Mes = @Mes 

--	If @iEjecutar > 0 
----	Begin 
	If @EsRevision = 1 
	   Set @iEjecutar = 1 
	   
		Exec spp_Rpt_CteReg_EdoJuris_ListaClavesNegadas_Consulta 
			 @IdEstado, @IdJurisdiccion, @IdFarmacia, @FechaProceso, @bEsGeneral, @ProcesoPorDia, @MostrarTodo, @iEjecutar   
----	End 
	
End 
Go--#SQL 
   
