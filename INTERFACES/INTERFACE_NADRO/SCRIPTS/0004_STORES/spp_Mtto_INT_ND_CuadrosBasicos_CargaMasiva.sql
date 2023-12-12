------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_INT_ND_CuadrosBasicos_CargaMasiva' and xType = 'P') 
    Drop Proc spp_Mtto_INT_ND_CuadrosBasicos_CargaMasiva
Go--#SQL 
  
--  Exec spp_Mtto_INT_ND_CuadrosBasicos_CargaMasiva '001', '11', '0003', '0001', '0001', 1, '2014-08-20'
  
---		sp_listacolumnas__stores spp_Mtto_INT_ND_CuadrosBasicos_CargaMasiva , 1    
  
Create Proc spp_Mtto_INT_ND_CuadrosBasicos_CargaMasiva 
(   
    @IdEstado varchar(2) = '', @Contrato varchar(max) = '', 
    @Prioridad varchar(max) = '', @NombrePrograma varchar(max) = '', 
    @IdAnexo varchar(max) = '', @NombreAnexo varchar(max) = '', 
    @ClaveSSA_ND varchar(max) = '', @ClaveSSA_Mascara varchar(max) = '',  
	@ManejaIva varchar(max) = '', @PrecioVenta varchar(max) = '0', @PrecioServicio varchar(max) = '0', 
	@Descripcion_Mascara varchar(max) = '', 
	@Lote varchar(30) = '', @UnidadDeMedida varchar(500) = '', @ContenidoPaquete int = 1, 
	@Vigencia varchar(10) = ''  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD  

	Set @Descripcion_Mascara = replace(@Descripcion_Mascara, '´´´', char(39)) 
	Set @ContenidoPaquete = (case when @ContenidoPaquete <= 0 then 1 else @ContenidoPaquete end)

	Insert Into INT_ND_CFG_CB_CuadrosBasicos_CargaMasiva 
	( IdEstado, Contrato, Prioridad, NombrePrograma, IdAnexo, NombreAnexo, ClaveSSA_ND, ClaveSSA_Mascara, 
		ManejaIva, PrecioVenta, PrecioServicio, Descripcion_Mascara, Lote, UnidadDeMedida, ContenidoPaquete, Vigencia ) 
    Select @IdEstado, @Contrato, @Prioridad, @NombrePrograma, @IdAnexo, @NombreAnexo, @ClaveSSA_ND, @ClaveSSA_Mascara, 
		@ManejaIva, @PrecioVenta, @PrecioServicio, @Descripcion_Mascara, @Lote, @UnidadDeMedida, @ContenidoPaquete, @Vigencia  
    

End  
Go--#SQL 

