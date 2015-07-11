/********************************************************************************************************/
/* Responsable          : 														*/		
/* Fecha                : Enero 2014    																*/
/* Descripcion          : Reverso del folio:															*/		
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

