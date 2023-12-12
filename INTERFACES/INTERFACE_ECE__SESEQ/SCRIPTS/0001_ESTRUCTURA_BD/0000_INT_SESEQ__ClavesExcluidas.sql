
---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_SESEQ__ClavesExcluidas_InfoOperacion' and xType = 'U' ) 
	Drop Table INT_SESEQ__ClavesExcluidas_InfoOperacion 
Go--#SQL  

Create Table INT_SESEQ__ClavesExcluidas_InfoOperacion
(
	ClaveSSA varchar(50) Not Null Default '' -- Unique 
)
Go--#SQL  

	Alter Table INT_SESEQ__ClavesExcluidas_InfoOperacion Add Constraint PK_INT_SESEQ__ClavesExcluidas_InfoOperacion Primary Key ( ClaveSSA ) 

Go--#SQL  


If Not Exists ( Select * From INT_SESEQ__ClavesExcluidas_InfoOperacion Where ClaveSSA = 'SC-MC-1340' )  Insert Into INT_SESEQ__ClavesExcluidas_InfoOperacion (  ClaveSSA )  Values ( 'SC-MC-1340' ) 

Go--#SQL  

----insert into INT_SESEQ__ClavesExcluidas_InfoOperacion 
----select 'SC-MC-1340' 


----	sp_generainserts 'INT_SESEQ__ClavesExcluidas_InfoOperacion', 1 
