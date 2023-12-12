If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Net_Modulos' and xType = 'P' ) 
   Drop Proc sp_Net_Modulos
Go--#SQL   

---  Exec sp_Net_Modulos 'SC_SolutionsSystem', 'dll', '3.0.4.0', 'asdfghjklñ'	


Create Proc sp_Net_Modulos 
( 
	@Nombre varchar(100), @Extension varchar(10), @Version varchar(10), @FileVersion varchar(50), 
	@MD5 varchar(40), @EmpacadoModulo Text, @Tamaño numeric(14,4), @Status varchar(1) = 'A'
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare @IdModulo varchar(6)
 	
	If Not Exists ( Select * From Net_Modulos (NoLock) Where Nombre = @Nombre ) 
	   Begin 
		   Select @IdModulo = right('000000' + cast(max(IdModulo) + 1 as varchar), 6) From Net_Modulos (NoLock)
		   Set @IdModulo = IsNull(@IdModulo, '000001')  
		   
	       Insert Into Net_Modulos ( IdModulo, Nombre, Extension, Version, VersionArchivo, MD5, EmpacadoModulo, Tamaño ) 
	       Select @IdModulo, @Nombre, @Extension, @Version, @FileVersion, @MD5, @EmpacadoModulo, @Tamaño 
	       
	   End 
	Else 
	   Begin 
	      Select @IdModulo = IdModulo From Net_Modulos (NoLock) Where Nombre = @Nombre 
	      Update Net_Modulos Set Status = @Status, Actualizado = 0, 
	            Version = @Version, VersionArchivo = @FileVersion, MD5 = @MD5, EmpacadoModulo = @EmpacadoModulo, Tamaño = @Tamaño, 
	            FechaActualizacion = getdate()  
	      Where IdModulo = @IdModulo  
	   End    
	   
	Insert Into Net_Modulos_Historico ( IdModulo, Nombre, Extension, Version, VersionArchivo, MD5, EmpacadoModulo, Tamaño ) 
	Select @IdModulo, @Nombre, @Extension, @Version, @FileVersion, @MD5, @EmpacadoModulo, @Tamaño 
	       	   
End 
Go--#SQL   

/* 
	IdModulo varchar(6) Not Null, 
	Nombre varchar(100) Not Null Unique, 

	Extension varchar(10) Not Null Default '',  
	Version varchar(10) Not Null Default '', 
	MD5 varchar(40) Not Null Default '', 	
	
	sp_listacolumnas Net_Modulos 
	
*/ 	