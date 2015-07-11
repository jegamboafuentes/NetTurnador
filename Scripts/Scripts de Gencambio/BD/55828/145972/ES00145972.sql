/********************************************************************************************************/
/* Responsable          : 														*/		
/* Fecha                : Enero 2014    																*/
/* Descripcion          : Creacion de la tabla TCCJCCNegocio									*/		
/********************************************************************************************************/
SET NOCOUNT ON
DECLARE	@vcMensaje	VARCHAR(255) 
IF Object_ID('DBO.TCCJCCNegocio','U') IS NOT NULL
BEGIN
	IF Object_ID('tempdb..#vtTCCJCCNegocio') IS NOT NULL			
		DROP TABLE #vtTCCJCCNegocio		
	
	SELECT	fiNegocioID,
			fcDescripcion
	INTO #vtTCCJCCNegocio
	FROM TCCJCCNegocio WITH(NOLOCK)				
		   	   
	IF @@ERROR <> 0
	BEGIN				
		SET @vcMensaje = 'No se pudo crear respaldo de la tabla TCCJCCNegocio'
		DROP TABLE #vtTCCJCCNegocio		
		GOTO CtrlErrores
	END
	
	IF  Object_ID('FK001TCCJCCNegocioXFuncion') IS NOT NULL	
	BEGIN
		ALTER TABLE dbo.TCCJCCNegocioXFuncion DROP CONSTRAINT FK001TCCJCCNegocioXFuncion
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al eliminar la fk FK001TCCJCCNegocioXFuncion'
			GOTO CtrlErrores
		END		
	END	

	IF  Object_ID('FK002TCCJCCNegocioXFuncion') IS NOT NULL	
	BEGIN
		ALTER TABLE dbo.TCCJCCNegocioXFuncion DROP CONSTRAINT FK002TCCJCCNegocioXFuncion
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al eliminar la fk FK002TCCJCCNegocioXFuncion'
			GOTO CtrlErrores
		END		
	END	
	
	
	DROP TABLE dbo.TCCJCCNegocio
	

RETURN
SET NOCOUNT OFF 
---------------------------------------------------------------------------------
-- MANEJO DE ERRORES
---------------------------------------------------------------------------------
CtrlErrores:
	SET NOCOUNT OFF
		SET @vcMensaje = 'Error durante creacion de TCCJCCNegocio ' +  ISNULL(@vcMensaje, '')        
        RAISERROR ( @vcMensaje , 18 , 1 )
		RETURN
	
END
GO

DECLARE	@vcMensaje	VARCHAR(255) 
BEGIN TRAN ES

CREATE TABLE dbo.TCCJCCNegocio
(		
		fiNegocioID			TINYINT			NOT NULL,
		fcDescripcion		VARCHAR(255)	NOT NULL
		
		CONSTRAINT PKTCCJCCNegocio PRIMARY KEY CLUSTERED (fiNegocioID),
)

IF Object_ID ('tempdb..#vtTCCJCCNegocio')  IS NOT NULL
BEGIN
	INSERT INTO dbo.TCCJCCNegocio
			   (fiNegocioID,
				fcDescripcion)
		 SELECT fiNegocioID,
				fcDescripcion
		   FROM #vtTCCJCCNegocio
		   	   
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al insertar respaldo de la tabla TCCJCCNegocio'
			GOTO CtrlErrores
		END	

								
END

COMMIT TRAN ES
SET NOCOUNT OFF
RETURN
---------------------------------------------------------------------------------
-- MANEJO DE ERRORES
---------------------------------------------------------------------------------
CtrlErrores:
	SET NOCOUNT OFF
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN ES

		SET @vcMensaje = 'Error durante creacion de estructuras ' +  ISNULL(@vcMensaje, '')        
        RAISERROR ( @vcMensaje , 18 , 1 )                
        DROP TABLE 	#vtTCCJCCNegocio
		RETURN

GO

EXEC dbo.SPGRANT 'dbo.TCCJCCNegocio'
GO


/********************************************************************************************************/
/* Responsable          : 														*/		
/* Fecha                : Enero 2014    																*/
/* Descripcion          : Creacion de la tabla TCCJCCNegocioXFuncion									*/		
/********************************************************************************************************/
SET NOCOUNT ON


DECLARE	@vcMensaje	VARCHAR(255) 
IF Object_ID('DBO.TCCJCCNegocioXFuncion','U') IS NOT NULL
BEGIN
	IF Object_ID('tempdb..#vtTCCJCCNegocioXFuncion') IS NOT NULL			
		DROP TABLE #vtTCCJCCNegocioXFuncion		
	
	SELECT	fiFuncionID,
			fiNegocioID
	INTO #vtTCCJCCNegocioXFuncion
	FROM TCCJCCNegocioXFuncion WITH(NOLOCK)				
		   	   
	IF @@ERROR <> 0
	BEGIN				
		SET @vcMensaje = 'No se pudo crear respaldo de la tabla TCCJCCNegocioXFuncion'
		DROP TABLE #vtTCCJCCNegocioXFuncion		
		GOTO CtrlErrores
	END

	DROP TABLE dbo.TCCJCCNegocioXFuncion
	

RETURN
SET NOCOUNT OFF 
---------------------------------------------------------------------------------
-- MANEJO DE ERRORES
---------------------------------------------------------------------------------
CtrlErrores:
	SET NOCOUNT OFF
		SET @vcMensaje = 'Error durante creacion de TCCJCCNegocioXFuncion ' +  ISNULL(@vcMensaje, '')        
        RAISERROR ( @vcMensaje , 18 , 1 )
		RETURN
	
END
GO

DECLARE	@vcMensaje	VARCHAR(255) 
BEGIN TRAN ES

CREATE TABLE dbo.TCCJCCNegocioXFuncion
(		
		fiFuncionID			INT	NOT NULL,
		fiNegocioID			TINYINT		NOT NULL
		
		CONSTRAINT PKTCCJCCNegocioXFuncion PRIMARY KEY CLUSTERED (fiFuncionID),
		CONSTRAINT FK001TCCJCCNegocioXFuncion FOREIGN KEY (fiFuncionID) REFERENCES dbo.TCCJCFFuncionesSAP (fiFuncionID),
		CONSTRAINT FK002TCCJCCNegocioXFuncion FOREIGN KEY (fiNegocioID) REFERENCES dbo.TCCJCCNegocio (fiNegocioID),
)

IF Object_ID ('tempdb..#vtTCCJCCNegocioXFuncion')  IS NOT NULL
BEGIN
	INSERT INTO dbo.TCCJCCNegocioXFuncion
			   (fiFuncionID,
				fiNegocioID)
		 SELECT fiFuncionID,
				fiNegocioID
		   FROM #vtTCCJCCNegocioXFuncion
		   	   
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al insertar respaldo de la tabla TCCJCCNegocioXFuncion'
			GOTO CtrlErrores
		END	

								
END

COMMIT TRAN ES
SET NOCOUNT OFF
RETURN
---------------------------------------------------------------------------------
-- MANEJO DE ERRORES
---------------------------------------------------------------------------------
CtrlErrores:
	SET NOCOUNT OFF
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN ES

		SET @vcMensaje = 'Error durante creacion de estructuras ' +  ISNULL(@vcMensaje, '')        
        RAISERROR ( @vcMensaje , 18 , 1 )                
        DROP TABLE 	#vtTCCJCCNegocioXFuncion
		RETURN

GO

EXEC dbo.SPGRANT 'dbo.TCCJCCNegocioXFuncion'
GO
