
select * From tbl_Workspace (nolock) where WorkspaceName like '%edgar%' -- WorkspaceId = 520000001 


	if exists ( select * from sysobjects (nolock) Where name = 'tmpAreas' and xtype = 'u' )
	   drop table tmpAreas 
	
	select A.WorkspaceId, A.WorkspaceName, A.CreationDate 
	into tmpAreas 
	From tbl_Workspace A (nolock) 
	Where convert(varchar(10),A.CreationDate, 120) <= '2017-06-18' 
	
	select * from tmpAreas 



------------------------------------------------- 
begin tran 

	Delete from tbl_LocalVersion Where WorkspaceId in ( Select WorkspaceId From tmpAreas ) 

	--Delete from tbl_Lock Where WorkspaceId in ( '680000001', '700000001' ) 	

	Delete from tbl_WorkspaceMapping Where WorkspaceId in ( Select WorkspaceId From tmpAreas ) 
	Delete from tbl_WorkingFolder Where WorkspaceId in ( Select WorkspaceId From tmpAreas ) 	

	Delete from tbl_Workspace Where WorkspaceId in ( Select WorkspaceId From tmpAreas ) 

	------------ Bloqueos huerbanos 
	-- Delete From tbl_PendingChange 

--		rollback tran 

--		commit tran 

