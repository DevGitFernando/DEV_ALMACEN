


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Rpt_Admon_Claves_Existencia_Unidades_Estado' and xType = 'P' )
   Drop Proc sp_Rpt_Admon_Claves_Existencia_Unidades_Estado 
Go--#SQL

Create Proc sp_Rpt_Admon_Claves_Existencia_Unidades_Estado 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11'  
) 
With Encryption 
As 
Begin 
Declare @sSql varchar(7500), 
	@sIdFarmacia varchar(4), 
	@sFarmacia varchar(100),
	@IdFarmacia varchar(4)
	
	
	Set @IdFarmacia = '0001' 
	
	
	If @IdEstado = '21'
	Begin
		Set @IdFarmacia = '1000'
	End
	
	If @IdEstado = '20'
	Begin
		Set @IdFarmacia = '0100'
	End  
	
	---	Farmacias Involucradas
	Select Distinct identity(int, 1, 1) as Keyx, IdFarmacia, Farmacia 
	Into #tmpFarmaciasEstado 	
	From SVR_INV_Generar_Existencia_Concentrado
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia >= @IdFarmacia
	Order By IdFarmacia

	----- listado de claves ssa
	Select Distinct ClaveSSA, DescripcionSal, Presentacion_ClaveSSA, 
	0 as ContenidoPaquete, sum(Existencia) as ExistenciaGeneral 
	Into #tmpClaves_Existencia 
	From SVR_INV_Generar_Existencia_Concentrado
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia >= @IdFarmacia
	Group By  ClaveSSA, DescripcionSal, Presentacion_ClaveSSA
	Order by DescripcionSal
	
	---- Concentrado de la existencia por clave por unidad
	Select IdFarmacia, ClaveSSA, sum(Existencia) as Existencia 
	Into #tmpConcentrado_Claves_Unidad 
	From SVR_INV_Generar_Existencia_Concentrado
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia >= @IdFarmacia
	Group By IdFarmacia, ClaveSSA
	
	
	
    Declare tmpCol Cursor For Select IdFarmacia, Farmacia from #tmpFarmaciasEstado order by keyx	
    Open tmpCol
    FETCH NEXT FROM tmpCol Into @sIdFarmacia, @sFarmacia 
        WHILE @@FETCH_STATUS = 0
        BEGIN
           -- print  @sIdFarmacia + '   ' + @sFarmacia 
           --- Agregar la Columna Correspondiente 
           Set @sSql = ' Alter Table ' + '#tmpClaves_Existencia' + ' Add [_' + (@sIdFarmacia + '-' + @sFarmacia) + '] numeric(14,4) not null Default 0 ' 
           Exec(@sSql)
           
           --- Agregar la Informacion Relacionada 
		   Set @sSql = ' Update E Set [_' + (@sIdFarmacia + '-' + @sFarmacia) + '] = P.Existencia 
				From #tmpClaves_Existencia E  
				Inner join #tmpConcentrado_Claves_Unidad P (NoLock) ' + 
				'	On ( P.IdFarmacia = ' + char(39) + @sIdFarmacia + char(39) + ' and E.ClaveSSA = P.ClaveSSA ) '
           Exec(@sSql)
           	           
           
           FETCH NEXT FROM tmpCol Into @sIdFarmacia, @sFarmacia  
        END
    Close tmpCol
    Deallocate tmpCol 
    
    
    Update T Set ContenidoPaquete = S.ContenidoPaquete
    From #tmpClaves_Existencia T
    Inner Join CatClavesSSA_Sales S On ( T.ClaveSSA = S.ClaveSSA )
    
    
    Select * 
    From #tmpClaves_Existencia 
    
    Select max(keyx) as NumFarmacias, GETDATE() as FechaImpresion From #tmpFarmaciasEstado
    
End 
Go--#SQL    	
