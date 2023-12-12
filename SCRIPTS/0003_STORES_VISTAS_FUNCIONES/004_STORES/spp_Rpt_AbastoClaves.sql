-------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_AbastoClaves_Global' and xType = 'P' )
    Drop Proc spp_Rpt_AbastoClaves_Global 
Go--#SQL
  
Create Proc spp_Rpt_AbastoClaves_Global 
( 
	@IdEstado varchar(2) = '16', @IdFarmacia varchar(4) = '41', @TipoDeInsumo smallint = 0, @Concentrado smallint = 0  
)
With Encryption 
As
Begin 
Set NoCount On 

Declare 
	@EncPrincipal varchar(500), 
	@EncSecundario varchar(500), 
	@Claves numeric(14,4), @Abasto numeric(14,4),
	@PorcAbasto numeric(14,4), 
	@Tipo int, 
	@Medicamento int, 
	@MaterialDeCuracion int  

	Set @MaterialDeCuracion = 1 
	Set @Medicamento = 2 
	
	If @TipoDeInsumo = 1 
	   Set @Medicamento = 1 
	
	If @TipoDeInsumo = 2  
	   Set @MaterialDeCuracion = 2 	
		
	if @IdFarmacia <> '*' 
	Set @IdFarmacia = RIGHT('0000' + @IdFarmacia, 4) 
	
	
		Set @Tipo = 1 
		Select @EncPrincipal = EncabezadoPrincipal, @EncSecundario = EncabezadoSecundario From dbo.fg_Unidad_EncabezadoReportesClientesSSA() 	

------- Generar Perifil de Farmacia 
        Select IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA, ClaveSSA, DescripcionClave, IdTipoDeClave, TipoDeClaveDescripcion   
        Into #tmpPerfil 
        From vw_CB_CuadroBasico_Farmacias  
        Where IdEstado = @IdEstado --and IdFarmacia = @IdFarmacia 
			and StatusMiembro = 'A' 
			and StatusClave = 'A' 
			and IdTipoDeClave in ( @MaterialDeCuracion, @Medicamento ) 
		
		If @IdFarmacia <> '*'  
		Begin 
		   Set @Tipo = 2 
		   Delete From #tmpPerfil Where IdFarmacia <> @IdFarmacia 
		   
		   Update P Set IdTipoDeClave = '', TipoDeClaveDescripcion = 'GENERAL'
		   From #tmpPerfil P 
		End 	 		
		   		
		
		If @TipoDeInsumo = 0 And @Concentrado = 1 
		Begin 
		   Update P Set IdTipoDeClave = '', TipoDeClaveDescripcion = 'GENERAL'
		   From #tmpPerfil P 			
		End 
		
		   		
--      spp_Rpt_AbastoClaves_Aux 

------------------------------------------- Calcular Abasto 
		If Exists ( Select Name From Sysobjects (NoLock) Where Name = '#tmpRptAbastoClaves' and xType = 'U' )
		    Drop Table #tmpRptAbastoClaves   
		   
	   
	    Select  @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, 
				IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA, ClaveSSA, DescripcionClave, 
				IdTipoDeClave, TipoDeClaveDescripcion, 
				sum(Existencia) as Existencia, 
	           (case when sum(Existencia) > 0 then '1' else '0' end) Abasto, Cast(0 As Numeric(14,4)) as PorcAbasto  
        Into #tmpRptAbastoClaves 
	    From 
	    ( 
		    Select  P.IdEstado, P.Estado, P.IdFarmacia, -- Cast( '' As varchar(50)) As Farmacia, 
		            P.Farmacia, 
		            P.IdClaveSSA, P.ClaveSSA, P.DescripcionClave, P.IdTipoDeClave, P.TipoDeClaveDescripcion, 
		            IsNull(C.Existencia, 0) As Existencia,
		            Case When C.Existencia > 0 Then '1' Else '0' End As Abasto, Cast( 0 As Numeric(14,4)) As PorcAbasto 
		    From #tmpPerfil P (Nolock)
		    Left Join SVR_INV_Generar_Existencia_Concentrado C (Nolock) 
			    On ( P.IdEstado = C.IdEstado And P.IdFarmacia = C.IdFarmacia And P.ClaveSSA = C.ClaveSSA  )
		    -- Where P.IdEstado = @IdEstado 
		) as T 
		group by IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA, ClaveSSA, DescripcionClave, IdTipoDeClave, TipoDeClaveDescripcion  
------------------------------------------- Calcular Abasto 


----    Excepciones 
--        select T.* 
--        From #tmpRptAbastoClaves T 
--        Inner Join CFG_Claves_Excluir_NivelAbasto E On ( E.IdEstado = @IdEstado and T.IdClaveSSA = E.IdClaveSSA )
--        Where Abasto = 0 
        
        Update T Set Abasto = 2 
        From #tmpRptAbastoClaves T 
        Inner Join CFG_Claves_Excluir_NivelAbasto E On ( E.IdEstado = @IdEstado and T.IdClaveSSA = E.IdClaveSSA )

----------		spp_Rpt_AbastoClaves_Global  

----------		select * from #tmpRptAbastoClaves where Abasto = 2 
		
	
--------------    Calcular los abastos 		
----------		Set @Claves = (	Select Cast(Count(*) As Numeric(14,4)) From #tmpPerfil (Nolock)
----------						Where IdEstado = @IdEstado ) 
----------		Set @Abasto = ( Select Cast(Count(*) As Numeric(14,4)) From #tmpRptAbastoClaves (Nolock)
----------						Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Abasto in ( 1, 2 ) ) 
----------
----------		-- Set @PorcAbasto = ( Select ((@Abasto/@Claves) * 100) ) 
----------		Set @PorcAbasto = (case when @Claves = 0 then 0 else ((@Abasto/@Claves) * 100) end ) 	
	

----------		Update #tmpRptAbastoClaves Set PorcAbasto = @PorcAbasto 
----------		Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia
	

--- SALIDA FINAL	
	Select IdEstado, Estado, IdFarmacia, Farmacia, IdTipoDeClave, TipoDeClaveDescripcion, 
		   '' as Url, '' as Juris, 1 as Procesar, 
		   FechaReporte, 
		   TotalClaves, ConExistencia, SinExistencia, 
		   cast(round((case when ConExistencia = 0 then 0 else (ConExistencia/(TotalClaves*1.0)) * 100 end), 4) as numeric(14,4)) as PorcAbasto 
	Into #tmp_Final 	   
	From 
	( 
		Select IdEstado, Estado, IdFarmacia, Farmacia, IdTipoDeClave, TipoDeClaveDescripcion, getdate() as FechaReporte, 
			Count(*) As TotalClaves, 
			( Select Count(Abasto) From #tmpRptAbastoClaves X (Nolock) 
				Where X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and Abasto in ( 1, 2 ) and X.IdTipoDeClave = T.IdTipoDeClave ) As ConExistencia, 
			( Select Count(Abasto) From #tmpRptAbastoClaves Y (Nolock) 
				Where Y.IdEstado = T.IdEstado and Y.IdFarmacia = T.IdFarmacia and Abasto = 0 and Y.IdTipoDeClave = T.IdTipoDeClave ) As SinExistencia, 
			max(PorcAbasto) as PorcAbasto 
		From #tmpRptAbastoClaves T (Nolock) 
		-- Where 
		Group By IdEstado, Estado, IdFarmacia, Farmacia, IdTipoDeClave, TipoDeClaveDescripcion  
	) as D 	
	Order By IdEstado, IdFarmacia 


---------------------------------------------------------------------------------------------------------------------------
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpRptAbastoClaves' and xType = 'U' )
	    Drop Table tmpRptAbastoClaves

	Select * 
	Into tmpRptAbastoClaves 
	From #tmpRptAbastoClaves 
--- SALIDA FINAL	


	IF @Tipo = 1 
	Begin 
		Select 
			IdEstado, Estado, IdFarmacia, Farmacia, IdTipoDeClave, TipoDeClaveDescripcion, Url, Juris, Procesar, 
			FechaReporte, TotalClaves, ConExistencia, SinExistencia, PorcAbasto 
		From #tmp_Final	
		Order by IdEstado, IdFarmacia, IdTipoDeClave 
	End 
	Else 
	Begin
		Select 
			sum(TotalClaves) as TotalClaves, 
			sum(ConExistencia) as ConExistencia, 
			sum(SinExistencia) as SinExistencia, avg(PorcAbasto) as PorcAbasto, max(FechaReporte) as FechaReporte    
		From #tmp_Final				 
	End 	


End 
Go--#SQL


-------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_Rpt_AbastoClaves' and xType = 'P' )
    Drop Proc spp_Rpt_AbastoClaves
Go--#SQL

Create Proc spp_Rpt_AbastoClaves ( @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0053', @Consulta int = 1 )
With Encryption 
As
Begin 
Set NoCount On 

	Exec spp_Rpt_AbastoClaves_Global @IdEstado, @IdFarmacia 

End 
Go--#SQL 
