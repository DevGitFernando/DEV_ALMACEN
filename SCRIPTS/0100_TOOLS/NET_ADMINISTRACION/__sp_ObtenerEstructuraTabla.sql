If Exists ( Select * From Sysobjects Where Name = 'sp_ObtenerEstructuraTabla' and xType = 'P' )
    Drop Proc sp_ObtenerEstructuraTabla
go

Create Proc sp_ObtenerEstructuraTabla ( @sTabla varchar(100) = '', @sTablaNueva varchar(100) = '' )
With Encryption
As
Begin

Declare @sSalida varchar(8000),
        @sCampo varchar(200),
        @sCamposPK varchar(500),
        @sError varchar(200)
        -- @sTabla varchar(100)

Declare 
    @objid int,  
    @i int, 
    @indid int, 
    @cnstid int 

    Set NoCount On
    Set @sError = 'Falta proporcionar el nombre de la tabla de la cual se desea obtener la estructura'

    If len(@sTabla) = 0
    Begin
        RaisError ('Falta proporcionar el nombre de la tabla de la cual se desea obtener la estructura', 10,16 )
        Return
    End

    If len(@sTablaNueva) = 0
        Set @sTablaNueva = @sTabla

    -- Set @sTabla = 'Descripciones_De_Codigo'
    Set @sSalida = ''
    Set @sCampo = ''
    Set @objid = object_id(@sTabla) 

    If Exists ( Select Name From Sysobjects Where Name = '' + @sTabla + '' and xType = 'U' )
    Begin
        If Exists ( Select Name From tempdb..Sysobjects Where Name = '#tmpObtenerEstructura' and xType = 'U' )
            Drop table #tmpObtenerEstructura

        Select top 0 space(8000) as CampoTabla, identity(int, 1, 1) as Keyx
        Into #tmpObtenerEstructura

        Insert Into #tmpObtenerEstructura (CampoTabla) values ( 'If Exists ( Select Name From Sysobjects Where Name = ' + char(39) + @sTablaNueva + char(39) + ' and xType = ' + char(39) + 'U' + char(39) + ' )' )
        Insert Into #tmpObtenerEstructura (CampoTabla) values ( space(5) + ' Drop Table ' + @sTablaNueva )
        Insert Into #tmpObtenerEstructura (CampoTabla) values ( 'Go--#SQL' )
        Insert Into #tmpObtenerEstructura (CampoTabla) values ( ' ' )

        Insert Into #tmpObtenerEstructura (CampoTabla) values ( 'Create Table ' + @sTablaNueva )
        Insert Into #tmpObtenerEstructura (CampoTabla) values ( '(' )

        Insert Into #tmpObtenerEstructura (CampoTabla)
        Select
            space(5) + Sc.Name + ' ' + St.Name +
            ( Case When Sc.xType In ( 48, 52, 56, 59, 60, 62, 106, 108, 122, 127  ) Then -- Tipos numericos
                Case When Sc.xType In ( 106, 108 ) Then
                    '(' + cast(Sc.Prec as varchar) + ', ' + cast(Sc.scale as varchar) + ')' Else '' End
            Else
                Case When Sc.xType In ( 58, 61, 104 ) Then -- Tipos Fecha
                    ''
                Else '(' + cast(Sc.Prec as varchar) + ')' End
            End ) + ( Case When IsNullable = 0 then ' Not Null' Else ' Null' End ) + ', ' as Salida
        From Sysobjects So (NoLock)
            Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id )
            Inner Join systypes St (NoLock) On (Sc.xType = St.xType and Sc.xUserType =  St.xUserType )
            Where So.Id = @objid   ---  So.Name = '' + @sTabla + '' 
            Order by Sc.ColId

        -- Quitar la 'Coma' al ultimo campo
        Update #tmpObtenerEstructura set CampoTabla = left(CampoTabla, len(CampoTabla) - 1)
            where keyx = (Select top 1 keyx from #tmpObtenerEstructura order by keyx desc)

        Insert Into #tmpObtenerEstructura (CampoTabla) values ( ')' )
        Insert Into #tmpObtenerEstructura (CampoTabla) values ( 'Go--#SQL' )

------- Existe PK para la tabla ? 
        If Exists ( Select So.Name From Sysobjects So (NoLock) 
            Inner Join SysIndexKeys Si (NoLock) On (So.Id = Si.Id ) 
            Where So.Id = @objid  ) ---  So.Name = '' + @sTabla + '' )
        Begin
            Set @sCamposPK = ''

            Declare CamposTabla Cursor For
            Select ' ' + Sc.Name + ','
            From Sysobjects So (NoLock) 
            Inner Join SysIndexKeys Si (NoLock) On (So.Id = Si.Id )
            Inner Join Syscolumns Sc (NoLock) On (So.Id = Sc.Id and Sc.ColId = Si.ColId )
            Where So.Id = @objid   ---   So.Name = '' + @sTabla + '' 
				and Exists ( Select Name From Sysobjects (NoLock) 
							Where parent_obj = @objid and xType = 'PK' )  -- object_id(@sTabla)
            Group by So.Name, Sc.Name, Sc.ColId 
            Order by Sc.ColId 
            Open CamposTabla 
                Fetch Next From CamposTabla Into @sCampo 
                While ( @@Fetch_Status = 0 ) 
                Begin 
                    Set @sCamposPK = @sCamposPK + @sCampo 
                    Fetch Next From CamposTabla Into @sCampo 
                End
            Close CamposTabla 
            Deallocate CamposTabla

            Set @sCamposPK = left(@sCamposPK, len(@sCamposPK) - 1 ) 

	        Insert Into #tmpObtenerEstructura (CampoTabla) values ( ' ' )
            Insert Into #tmpObtenerEstructura (CampoTabla) values ( 'Alter Table ' + @sTablaNueva + ' Add Constraint Pk_' + @sTablaNueva + ' Primary Key ' )
            Insert Into #tmpObtenerEstructura (CampoTabla) values ( space(0) + '(' )
            Insert Into #tmpObtenerEstructura (CampoTabla) values ( space(8) + @sCamposPK )
            Insert Into #tmpObtenerEstructura (CampoTabla) values ( space(0) + ')' )
            Insert Into #tmpObtenerEstructura (CampoTabla) values ( 'Go--#SQL' )
        End

        Select object_id(@sTabla), @objid 
        Select id as object_id, type, name 
        From sysobjects 
        Where parent_obj = @objid and xType in ( 'UQ' ) 


    Select object_id, type, name 
    From sys.objects 
    Where parent_object_id = @objid and Type in ( 'UQ' ) 

------- Existe UNIQUE para la tabla ? 
        If Exists ( Select id, type, name From sysobjects 
                    Where parent_obj = @objid and type in ( 'UQxxx' ) ) 
        Begin
            Set @sCamposPK = '' 
            
            Declare CamposTabla Cursor For 
            Select ' ' + Sc.Name + ','
            From Sysobjects So (NoLock) 
            Inner Join SysIndexKeys Si (NoLock) On (So.Id = Si.Id )
            Inner Join Syscolumns Sc (NoLock) On (So.Id = Sc.Id and Sc.ColId = Si.ColId )
            Where So.Name = '' + @sTabla + '' 
				and Exists ( Select Name From Sysobjects (NoLock) 
							Where parent_obj = @objid and xType = 'UQ' ) ---  object_id(@sTabla) 
            Group by So.Name, Sc.Name, Sc.ColId 
            Order by Sc.ColId 
            
            Open CamposTabla 
                Fetch Next From CamposTabla Into @sCampo 
                While ( @@Fetch_Status = 0 ) 
                Begin 
                    Set @sCamposPK = @sCamposPK + @sCampo 
                    Fetch Next From CamposTabla Into @sCampo 
                End
            Close CamposTabla
            Deallocate CamposTabla

            Set @sCamposPK = left(@sCamposPK, len(@sCamposPK) - 1 )

	        Insert Into #tmpObtenerEstructura (CampoTabla) values ( ' ' )
            Insert Into #tmpObtenerEstructura (CampoTabla) values ( 'Alter Table ' + @sTablaNueva + ' Add Constraint Pk_' + @sTablaNueva + ' Primary Key ' )
            Insert Into #tmpObtenerEstructura (CampoTabla) values ( space(0) + '(' )
            Insert Into #tmpObtenerEstructura (CampoTabla) values ( space(8) + @sCamposPK )
            Insert Into #tmpObtenerEstructura (CampoTabla) values ( space(0) + ')' )
            Insert Into #tmpObtenerEstructura (CampoTabla) values ( 'Go--#SQL' )
        End

------- Agregar los Default de la tabla 
        If Exists ( Select So.Name From Sysobjects So (NoLock) Where So.parent_obj = object_id(@sTabla) and So.xType = 'D' )
        Begin
	        Insert Into #tmpObtenerEstructura (CampoTabla) values ( ' ' )
			Insert Into #tmpObtenerEstructura (CampoTabla) 
			Select 'Alter Table ' + @sTabla + ' Add Constraint ' + So.Name + ' Default ' + ltrim(rtrim(Sc.Text)) + ' For ' + Scc.Name  
			From Sysobjects So (NoLock) 
			Inner Join Syscomments Sc (NoLock) On ( So.Id = Sc.Id ) 
			Inner Join Syscolumns Scc (NoLock) on ( Sc.Id = Scc.cDefault ) 
			Where So.parent_obj = object_id(@sTabla) and So.xType = 'D' 
			
			Insert Into #tmpObtenerEstructura (CampoTabla) values ( 'Go--#SQL' )
		End

        Insert Into #tmpObtenerEstructura (CampoTabla) values ( ' ' )
        Select rtrim(CampoTabla) as CampoTabla from #tmpObtenerEstructura
    End
    Else
    Begin
        Set @sError = 'No existe la tabla :  ' + char(39) + @sTabla
        RaisError (@sError, 10,16 )
    End
End
go
