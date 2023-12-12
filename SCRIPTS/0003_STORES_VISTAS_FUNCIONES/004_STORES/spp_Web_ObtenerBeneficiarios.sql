--------------------------------------------- 
If Exists ( Select Name From SysObjects (NoLock) Where Name = 'fg_BD_Padron' and xType = 'FN' ) 
   Drop Function fg_BD_Padron 
Go--#SQL

Create Function dbo.fg_BD_Padron() 
returns varchar(100) 
With Encryption 
As 
Begin 
Declare @sBD varchar(110) 
	
	Select Top 1 @sBD = NombreBD From CFGS_BD_PADRONES (NoLock) Where Status = 'A' Order by Id 
	return IsNull(@sBD, '') 
End 
Go--#SQL 


--------------------------------------------- 
If Exists ( Select Name From SysObjects (NoLock) Where Name = 'fg_Padron_Estado' and xType = 'FN' ) 
   Drop Function fg_Padron_Estado  
Go--#SQL 

Create Function dbo.fg_Padron_Estado(@IdEstado varchar(2), @IdCliente varchar(4) = '0002') 
returns varchar(100) 
With Encryption 
As 
Begin 
Declare @sTabla varchar(110) 
	
	Select Top 1 @sTabla = NombreTabla From CFGS_PADRON_ESTADOS (NoLock) 
		Where IdEstado = @IdEstado and IdCliente = @IdCliente and Status = 'A' Order by Id 
	return IsNull(@sTabla, '') 
End 
Go--#SQL 

---------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Web_ObtenerBeneficiarios' and xType = 'P' ) 
   Drop Proc spp_Web_ObtenerBeneficiarios
Go--#SQL 

----     Exec spp_Web_ObtenerBeneficiarios '09', '2', '500021', '', '', '', '', '', '0023' 

Create Proc spp_Web_ObtenerBeneficiarios 
( 
	@IdEstado varchar(2) = '25', @TipoVigencia tinyint = 2, @sFolioRef varchar(20) = '', 
	@sNombre varchar(50) = '', @sApPat varchar(50) = '', @sApMat varchar(50) = '', 
	@sSexo varchar(1) = 'F', @sFechaNac varchar(10) = '1983-05-16', @IdCliente varchar(4) = '0002' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare @sSql varchar(7000), 
		@sWhere varchar(500),  
		@sBdPadron varchar(120),  
		@sTablaPadron varchar(120), 
		@iConVigencia tinyint,   
		@iSinVigencia tinyint  

	-- Filtro para Vigencia 
	Set @iConVigencia = 1 
	Set @iSinVigencia = 0  	

	If @TipoVigencia = 0 
	   Set @iConVigencia = 0 
	   
	If @TipoVigencia = 1 
	   Set @iSinVigencia = 1 


	--- Obtener el Origen de Datos 
	Set @sSql = '' 
	Set @sWhere = '' 
	Select @sBdPadron = IsNull(dbo.fg_BD_Padron(), '') 
	Select @sTablaPadron =  IsNull(dbo.fg_Padron_Estado(@IdEstado, @IdCliente), '')  

---    select @sBdPadron, @sTablaPadron 

	--- Crear la Tabla vacia con la Estructura requerida 
	Select Top 0 
		   FolioReferencia, FolioReferencia as Referencia, space(2) as Consecutivo, 
		   Nombre, ApPaterno, ApMaterno, Sexo, FechaNacimiento, Edad, 
		   EsVigente, FechaInicioVigencia, FechaFinVigencia -- , IdBeneficiario 
	Into #tmpBeneficiarios 
	From vw_Beneficiarios 


	--- Armar Where 
	If @sFolioRef <> '' 
	   Set @sWhere = ' Where right(replicate(' + '0' + ', 20) + Referencia, 20) like ' + char(39) + '%' + right(replicate('0',20) + @sFolioRef, 20) + '%' + char(39)
	   
	----If @sFolioRef <> '' 
	----   Set @sWhere = ' Where Folio like ' + char(39) + '%' + @sFolioRef + '%' + char(39) 
	   
	If @sFolioRef = '' 	   
	   Begin 
	      Set @sWhere = ' Where ApPaterno like ' + char(39) + '%' + @sApPat + '%' + char(39) 

	      If @sApMat <> '' 
	         Set @sWhere = @sWhere + ' and ApMaterno like ' + char(39) + '%' + @sApMat + '%' + char(39) 

	      Set @sWhere = @sWhere + ' and Nombre like ' + char(39) + '%' + @sNombre + '%' + char(39) 


	      If @sSexo <> '' 
			Set @sWhere = @sWhere + ' and Sexo = ' + char(39) + @sSexo + char(39) 
			
	      If @sFechaNac <> '' 
			Set @sWhere = @sWhere + ' and convert(varchar(10), FechaNacimiento, 120) = ' + char(39) + convert(varchar(10), @sFechaNac, 120) + char(39) 			
			
	   End 

	--- Obtener los Beneficiarios que coincidad con la busqueda solicitada 
	Set @sSql = 
		' Insert Into #tmpBeneficiarios	( 
				 FolioReferencia, Referencia, Consecutivo, Nombre, ApPaterno, ApMaterno, Sexo, FechaNacimiento, Edad, EsVigente, FechaInicioVigencia, FechaFinVigencia ) ' + char(13) + 
		' Select Referencia, Referencia, Consecutivo, Nombre, ApPaterno, ApMaterno, Sexo, FechaNacimiento, 0 as Edad, 
			(case when datediff(dd, getdate(), FechaFinVigencia) < 0 then 0 else 1 end) as EsVigente, 
			FechaInicioVigencia, FechaFinVigencia ' + char(13) + 
		' From ' + @sBdPadron + '..' + @sTablaPadron + ' (NoLock)' + char(13) + 
		'' + @sWhere  
	
	print @sSql 
	-- Asegurar de no ejecutar nada si falta alguno de estos datos 	
	If @sBdPadron <> '' and @sTablaPadron <> '' 
	Begin 
	   Exec(@sSql) 
    End 
    
--	Print @sSql 


	--- Información Enviada al Cliente que la Solicito 
	Select 
		   'Folio de Beneficiario' = FolioReferencia + '-' + right('00' + Consecutivo, 2), 
		   'Referencia' = Referencia, 
		   'Esta Vigente' = (case when EsVigente = 1 Then 'SI' Else 'NO' end), 		   
		   'Apellido Paterno' = ApPaterno, 'Apellido Materno' = ApMaterno, 'Nombre (s)' = Nombre,
		   'Sexo' = 
		   (	
				case when Sexo = 'M' then 'Masculino'		
					 when Sexo = 'F' Then 'Femenino'			
				else ' '		
				end		
			),  		   		  
		   'Fecha de Nacimiento' = convert(varchar(10), FechaNacimiento, 120), 
		   'Edad' = dbo.fg_CalcularEdad(FechaNacimiento), 		   
		   'Fecha Inicia Vigencia' = convert(varchar(10), FechaInicioVigencia, 120), 
		   'Fecha Termina Vigencia' = convert(varchar(10), FechaFinVigencia, 120), 
		   'Número de Control' = '*'		
	From #tmpBeneficiarios 
	Where EsVigente In ( @iConVigencia, @iSinVigencia )  --- Depende de donde se solicite la información 
	Order By ApPaterno, ApMaterno, Nombre  
	
		   
End 
Go--#SQL

