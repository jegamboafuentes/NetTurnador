----------------------------------------------------------------------------------------------------------------------------------
---Responsable	: Esteban Jesús Caro Guzmán	
---Fecha		: Febrero 2014
---Descripcion	: Reverso del AC_Turnador
----------------------------------------------------------------------------------------------------------------------------------
DECLARE @vcMensaje			VARCHAR(200),
		@vdFecha			SMALLDATETIME,
		@viModulo			INT

SET NOCOUNT ON
BEGIN TRAN AC

	IF OBJECT_ID(N'dbo.TCCJCCTRORIGEN','U') IS NOT NULL
	BEGIN
		DELETE FROM dbo.TCCJCCTRORIGEN
		IF @@ERROR <> 0 
		BEGIN
		SET @vcMensaje = 'Error al eliminar datos de la tabla TCCJCCTRORIGEN '
		GOTO CtrlErrores
		END
	END
	
	IF OBJECT_ID(N'dbo.TCCJCCTRFILA','U') IS NOT NULL
	BEGIN
		DELETE FROM dbo.TCCJCCTRFILA
		IF @@ERROR <> 0 
		BEGIN
		SET @vcMensaje = 'Error al eliminar datos de la tabla TCCJCCTRFILA '
		GOTO CtrlErrores
		END
	END
	
	IF OBJECT_ID(N'dbo.TCCJCCTRTIPOFILA','U') IS NOT NULL
	BEGIN
		DELETE FROM dbo.TCCJCCTRTIPOFILA
		IF @@ERROR <> 0 
		BEGIN
		SET @vcMensaje = 'Error al eliminar datos de la tabla TCCJCCTRTIPOFILA '
		GOTO CtrlErrores
		END
	END
	
	IF OBJECT_ID(N'dbo.TCCJCCTRUNIDADNEGOCIO','U') IS NOT NULL
	BEGIN
		DELETE FROM dbo.TCCJCCTRUNIDADNEGOCIO
		IF @@ERROR <> 0 
		BEGIN
		SET @vcMensaje = 'Error al eliminar datos de la tabla TCCJCCTRUNIDADNEGOCIO '
		GOTO CtrlErrores
		END
	END
	
	IF OBJECT_ID(N'dbo.TCCJCCTRESTADOTURNO','U') IS NOT NULL
	BEGIN
		DELETE FROM dbo.TCCJCCTRESTADOTURNO
		IF @@ERROR <> 0 
		BEGIN
		SET @vcMensaje = 'Error al eliminar datos de la tabla TCCJCCTRESTADOTURNO '
		GOTO CtrlErrores
		END
	END
	
	IF OBJECT_ID(N'dbo.TCCJCCTRESTADOPOOL','U') IS NOT NULL
	BEGIN
		DELETE FROM dbo.TCCJCCTRESTADOPOOL
		IF @@ERROR <> 0 
		BEGIN
		SET @vcMensaje = 'Error al eliminar datos de la tabla TCCJCCTRESTADOPOOL '
		GOTO CtrlErrores
		END
	END
	
	IF OBJECT_ID(N'dbo.TCCJCCTRCUALIDADES','U') IS NOT NULL
	BEGIN
		DELETE FROM dbo.TCCJCCTRCUALIDADES
		IF @@ERROR <> 0 
		BEGIN
		SET @vcMensaje = 'Error al eliminar datos de la tabla TCCJCCTRCUALIDADES '
		GOTO CtrlErrores
		END
	END
	
	IF OBJECT_ID(N'dbo.CATCAJCONFIGURACION','U') IS NOT NULL
	BEGIN
		DELETE FROM dbo.CATCAJCONFIGURACION
		IF @@ERROR <> 0 
		BEGIN
		SET @vcMensaje = 'Error al eliminar datos de la tabla CATCAJCONFIGURACION '
		GOTO CtrlErrores
		END
	END
	
COMMIT TRAN AC
SET NOCOUNT OFF
RETURN

---------------------------------------------------------------------------------
-- MANEJO DE ERRORES
---------------------------------------------------------------------------------
CtrlErrores:
	SET NOCOUNT OFF
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN AC
	    RAISERROR ( @vcMensaje , 18 , 1 )
        RETURN