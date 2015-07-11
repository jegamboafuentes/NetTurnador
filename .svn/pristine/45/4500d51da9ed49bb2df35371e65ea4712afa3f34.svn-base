---------------------------------------------------------------------------------
--- Responsable: Esteban Jesús Caro Guzmán
--- Fecha:      Febrero 2014
--- Descripción: Reverso de la tabla TCCJCCTRORIGEN.
---------------------------------------------------------------------------------

SET NOCOUNT ON 
DECLARE	@vcMensaje	VARCHAR(255) 

	IF  Object_ID('XFKTCCJCCTRORIGEN') IS NOT NULL	
	BEGIN
		ALTER TABLE dbo.TACJCCTRTURNO DROP CONSTRAINT [XFKTCCJCCTRORIGEN]
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al eliminar la fk XFKTCCJCCTRORIGEN '
			GOTO CtrlErrores
		END		
	END		
		
	IF  Object_ID('dbo.TCCJCCTRORIGEN', 'U') IS NOT NULL	
	BEGIN					
		DROP TABLE dbo.TCCJCCTRORIGEN
		IF (@@ERROR <> 0 )
		BEGIN
			SET @vcMensaje = 'Error al eliminar la tabla TCCJCCTRORIGEN '
			GOTO CtrlErrores
		END
	END
	
SET NOCOUNT OFF
RETURN

---------------------------------------------------------------------------------
-- MANEJO DE ERRORES
---------------------------------------------------------------------------------
CtrlErrores:
	SET NOCOUNT OFF
		RAISERROR(@vcMensaje ,18,1)
        RETURN
GO

---------------------------------------------------------------------------------
--- Responsable: Esteban Jesús Caro Guzmán
--- Fecha:      Febrero 2014
--- Descripción: Reverso de la tabla TCCJCCTRESTADOPOOL.
---------------------------------------------------------------------------------

SET NOCOUNT ON 
DECLARE	@vcMensaje	VARCHAR(255) 

	IF  Object_ID('XFKTCCJCCTRESTADOPOOL') IS NOT NULL	
	BEGIN
		ALTER TABLE dbo.TACJCCTRPOOLATENCION DROP CONSTRAINT [XFKTCCJCCTRESTADOPOOL]
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al eliminar la fk XFKTCCJCCTRESTADOPOOL '
			GOTO CtrlErrores
		END		
	END		
		
	IF  Object_ID('dbo.TCCJCCTRESTADOPOOL', 'U') IS NOT NULL	
	BEGIN					
		DROP TABLE dbo.TCCJCCTRESTADOPOOL
		IF (@@ERROR <> 0 )
		BEGIN
			SET @vcMensaje = 'Error al eliminar la tabla TCCJCCTRESTADOPOOL '
			GOTO CtrlErrores
		END
	END
	
SET NOCOUNT OFF
RETURN

---------------------------------------------------------------------------------
-- MANEJO DE ERRORES
---------------------------------------------------------------------------------
CtrlErrores:
	SET NOCOUNT OFF
		RAISERROR(@vcMensaje ,18,1)
        RETURN
GO

---------------------------------------------------------------------------------
--- Responsable: Esteban Jesús Caro Guzmán
--- Fecha:      Febrero 2014
--- Descripción: Reverso de la tabla TACJCCTRPOOLATENCION.
---------------------------------------------------------------------------------

SET NOCOUNT ON 
DECLARE	@vcMensaje	VARCHAR(255) 

	IF  Object_ID('XFKTACJCCTRPOOLATENCION1') IS NOT NULL	
	BEGIN
		ALTER TABLE dbo.TACJCCTRTURNO DROP CONSTRAINT [XFKTACJCCTRPOOLATENCION1]
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al eliminar la fk XFKTACJCCTRPOOLATENCION1 '
			GOTO CtrlErrores
		END		
	END		
	
	IF  Object_ID('XFKTACJCCTRPOOLATENCION2') IS NOT NULL	
	BEGIN
		ALTER TABLE dbo.TACJCCTRCUALIDADPOOL DROP CONSTRAINT [XFKTACJCCTRPOOLATENCION2]
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al eliminar la fk XFKTACJCCTRPOOLATENCION2 '
			GOTO CtrlErrores
		END		
	END		
	
	
	IF  Object_ID('XFKTCCJCCTRESTADOPOOL') IS NOT NULL	
	BEGIN
		ALTER TABLE dbo.TACJCCTRPOOLATENCION DROP CONSTRAINT [XFKTCCJCCTRESTADOPOOL]
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al eliminar la fk XFKTCCJCCTRESTADOPOOL '
			GOTO CtrlErrores
		END		
	END		
		
	IF  Object_ID('dbo.TACJCCTRPOOLATENCION', 'U') IS NOT NULL	
	BEGIN					
		DROP TABLE dbo.TACJCCTRPOOLATENCION
		IF (@@ERROR <> 0 )
		BEGIN
			SET @vcMensaje = 'Error al eliminar la tabla TACJCCTRPOOLATENCION '
			GOTO CtrlErrores
		END
	END
	
SET NOCOUNT OFF
RETURN

---------------------------------------------------------------------------------
-- MANEJO DE ERRORES
---------------------------------------------------------------------------------
CtrlErrores:
	SET NOCOUNT OFF
		RAISERROR(@vcMensaje ,18,1)
        RETURN
GO


-----------------------------------------------------------------------------------
----- Responsable: Esteban Jesús Caro Guzmán
----- Fecha:      Febrero 2014
----- Descripción: Reverso de la tabla TCCJCCTRUNIDADNEGOCIO.
-----------------------------------------------------------------------------------

SET NOCOUNT ON 
DECLARE	@vcMensaje	VARCHAR(255) 

	IF  Object_ID('XFKTCCJCCTRUNIDADNEGOCIO') IS NOT NULL	
	BEGIN
		ALTER TABLE dbo.TCCJCCTRFILA DROP CONSTRAINT [XFKTCCJCCTRUNIDADNEGOCIO]
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al eliminar la fk XFKTCCJCCTRUNIDADNEGOCIO '
			GOTO CtrlErrores
		END		
	END		
		
	IF  Object_ID('dbo.TCCJCCTRUNIDADNEGOCIO', 'U') IS NOT NULL	
	BEGIN					
		DROP TABLE dbo.TCCJCCTRUNIDADNEGOCIO
		IF (@@ERROR <> 0 )
		BEGIN
			SET @vcMensaje = 'Error al eliminar la tabla TCCJCCTRUNIDADNEGOCIO '
			GOTO CtrlErrores
		END
	END
	
SET NOCOUNT OFF
RETURN

---------------------------------------------------------------------------------
-- MANEJO DE ERRORES
---------------------------------------------------------------------------------
CtrlErrores:
	SET NOCOUNT OFF
		RAISERROR(@vcMensaje ,18,1)
        RETURN
GO

---------------------------------------------------------------------------------
--- Responsable: Esteban Jesús Caro Guzmán
--- Fecha:      Febrero 2014
--- Descripción: Reverso de la tabla TCCJCCTRTIPOFILA.
---------------------------------------------------------------------------------

SET NOCOUNT ON 
DECLARE	@vcMensaje	VARCHAR(255) 

	IF  Object_ID('XFKTCCJCCTRTIPOFILA') IS NOT NULL	
	BEGIN
		ALTER TABLE dbo.TCCJCCTRFILA DROP CONSTRAINT [XFKTCCJCCTRTIPOFILA]
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al eliminar la fk XFKTCCJCCTRTIPOFILA '
			GOTO CtrlErrores
		END		
	END		
		
	IF  Object_ID('dbo.TCCJCCTRTIPOFILA', 'U') IS NOT NULL	
	BEGIN					
		DROP TABLE dbo.TCCJCCTRTIPOFILA
		IF (@@ERROR <> 0 )
		BEGIN
			SET @vcMensaje = 'Error al eliminar la tabla TCCJCCTRTIPOFILA '
			GOTO CtrlErrores
		END
	END
	
SET NOCOUNT OFF
RETURN

---------------------------------------------------------------------------------
-- MANEJO DE ERRORES
---------------------------------------------------------------------------------
CtrlErrores:
	SET NOCOUNT OFF
		RAISERROR(@vcMensaje ,18,1)
        RETURN
GO

---------------------------------------------------------------------------------
--- Responsable: Esteban Jesús Caro Guzmán
--- Fecha:      Febrero 2014
--- Descripción: Reverso de la tabla TCCJCCTRFILA.
---------------------------------------------------------------------------------

SET NOCOUNT ON 
DECLARE	@vcMensaje	VARCHAR(255) 

	IF  Object_ID('XFKTCCJCCTRFILA') IS NOT NULL	
	BEGIN
		ALTER TABLE dbo.TACJCCTRTURNO DROP CONSTRAINT [XFKTCCJCCTRFILA]
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al eliminar la fk XFKTCCJCCTRFILA '
			GOTO CtrlErrores
		END		
	END		
	
	IF  Object_ID('XFKTCCJCCTRUNIDADNEGOCIO') IS NOT NULL	
	BEGIN
		ALTER TABLE dbo.TCCJCCTRFILA DROP CONSTRAINT [XFKTCCJCCTRUNIDADNEGOCIO]
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al eliminar la fk XFKTCCJCCTRUNIDADNEGOCIO '
			GOTO CtrlErrores
		END		
	END		
	
	IF  Object_ID('XFKTCCJCCTRTIPOFILA') IS NOT NULL	
	BEGIN
		ALTER TABLE dbo.TCCJCCTRFILA DROP CONSTRAINT [XFKTCCJCCTRTIPOFILA]
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al eliminar la fk XFKTCCJCCTRTIPOFILA '
			GOTO CtrlErrores
		END		
	END		
		
	IF  Object_ID('dbo.TCCJCCTRFILA', 'U') IS NOT NULL	
	BEGIN					
		DROP TABLE dbo.TCCJCCTRFILA
		IF (@@ERROR <> 0 )
		BEGIN
			SET @vcMensaje = 'Error al eliminar la tabla TCCJCCTRFILA '
			GOTO CtrlErrores
		END
	END
	
SET NOCOUNT OFF
RETURN

---------------------------------------------------------------------------------
-- MANEJO DE ERRORES
---------------------------------------------------------------------------------
CtrlErrores:
	SET NOCOUNT OFF
		RAISERROR(@vcMensaje ,18,1)
        RETURN
GO

---------------------------------------------------------------------------------
--- Responsable: Esteban Jesús Caro Guzmán
--- Fecha:      Febrero 2014
--- Descripción: Reverso de la tabla TACJCCTRTURNO.
---------------------------------------------------------------------------------

SET NOCOUNT ON 
DECLARE	@vcMensaje	VARCHAR(255) 

	IF  Object_ID('XFKTACJCCTRTURNO1') IS NOT NULL	
	BEGIN
		ALTER TABLE dbo.TACJCCTRDETALLE DROP CONSTRAINT [XFKTACJCCTRTURNO1]
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al eliminar la fk XFKTACJCCTRTURNO1 '
			GOTO CtrlErrores
		END		
	END		
	
	IF  Object_ID('XFKTACJCCTRTURNO2') IS NOT NULL	
	BEGIN
		ALTER TABLE dbo.TACJCCTRHISTORICO DROP CONSTRAINT [XFKTACJCCTRTURNO2]
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al eliminar la fk XFKTACJCCTRTURNO2 '
			GOTO CtrlErrores
		END		
	END		
	
	IF  Object_ID('XFKTCCJCCTRORIGEN') IS NOT NULL	
	BEGIN
		ALTER TABLE dbo.TACJCCTRTURNO DROP CONSTRAINT [XFKTCCJCCTRORIGEN]
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al eliminar la fk XFKTCCJCCTRORIGEN '
			GOTO CtrlErrores
		END		
	END		
	
	IF  Object_ID('XFKTACJCCTRPOOLATENCION1') IS NOT NULL	
	BEGIN
		ALTER TABLE dbo.TACJCCTRTURNO DROP CONSTRAINT [XFKTACJCCTRPOOLATENCION1]
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al eliminar la fk XFKTACJCCTRPOOLATENCION1 '
			GOTO CtrlErrores
		END		
	END		
	
	IF  Object_ID('XFKTCCJCCTRFILA') IS NOT NULL	
	BEGIN
		ALTER TABLE dbo.TACJCCTRTURNO DROP CONSTRAINT [XFKTCCJCCTRFILA]
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al eliminar la fk XFKTCCJCCTRFILA '
			GOTO CtrlErrores
		END		
	END		
		
	IF  Object_ID('dbo.TACJCCTRTURNO', 'U') IS NOT NULL	
	BEGIN					
		DROP TABLE dbo.TACJCCTRTURNO
		IF (@@ERROR <> 0 )
		BEGIN
			SET @vcMensaje = 'Error al eliminar la tabla TACJCCTRTURNO '
			GOTO CtrlErrores
		END
	END
	
SET NOCOUNT OFF
RETURN

---------------------------------------------------------------------------------
-- MANEJO DE ERRORES
---------------------------------------------------------------------------------
CtrlErrores:
	SET NOCOUNT OFF
		RAISERROR(@vcMensaje ,18,1)
        RETURN
GO

---------------------------------------------------------------------------------
--- Responsable: Esteban Jesús Caro Guzmán
--- Fecha:      Febrero 2014
--- Descripción: Reverso de la tabla TACJCCTRDETALLE.
---------------------------------------------------------------------------------

SET NOCOUNT ON 
DECLARE	@vcMensaje	VARCHAR(255) 

	IF  Object_ID('XFKTACJCCTRTURNO1') IS NOT NULL	
	BEGIN
		ALTER TABLE dbo.TACJCCTRDETALLE DROP CONSTRAINT [XFKTACJCCTRTURNO1]
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al eliminar la fk XFKTACJCCTRTURNO1 '
			GOTO CtrlErrores
		END		
	END		
	
	IF  Object_ID('dbo.TACJCCTRDETALLE', 'U') IS NOT NULL	
	BEGIN					
		DROP TABLE dbo.TACJCCTRDETALLE
		IF (@@ERROR <> 0 )
		BEGIN
			SET @vcMensaje = 'Error al eliminar la tabla TACJCCTRDETALLE '
			GOTO CtrlErrores
		END
	END
	
SET NOCOUNT OFF
RETURN

---------------------------------------------------------------------------------
-- MANEJO DE ERRORES
---------------------------------------------------------------------------------
CtrlErrores:
	SET NOCOUNT OFF
		RAISERROR(@vcMensaje ,18,1)
        RETURN
GO

---------------------------------------------------------------------------------
--- Responsable: Esteban Jesús Caro Guzmán
--- Fecha:      Febrero 2014
--- Descripción: Reverso de la tabla TCCJCCTRCUALIDADES.
---------------------------------------------------------------------------------

SET NOCOUNT ON 
DECLARE	@vcMensaje	VARCHAR(255) 

	IF  Object_ID('XFKTCCJCCTRCUALIDADES') IS NOT NULL	
	BEGIN
		ALTER TABLE dbo.TACJCCTRCUALIDADPOOL DROP CONSTRAINT [XFKTCCJCCTRCUALIDADES]
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al eliminar la fk XFKTCCJCCTRCUALIDADES '
			GOTO CtrlErrores
		END		
	END		
	
	IF  Object_ID('dbo.TCCJCCTRCUALIDADES', 'U') IS NOT NULL	
	BEGIN					
		DROP TABLE dbo.TCCJCCTRCUALIDADES
		IF (@@ERROR <> 0 )
		BEGIN
			SET @vcMensaje = 'Error al eliminar la tabla TCCJCCTRCUALIDADES '
			GOTO CtrlErrores
		END
	END
	
SET NOCOUNT OFF
RETURN

---------------------------------------------------------------------------------
-- MANEJO DE ERRORES
---------------------------------------------------------------------------------
CtrlErrores:
	SET NOCOUNT OFF
		RAISERROR(@vcMensaje ,18,1)
        RETURN
GO

---------------------------------------------------------------------------------
--- Responsable: Esteban Jesús Caro Guzmán
--- Fecha:      Febrero 2014
--- Descripción: Reverso de la tabla TACJCCTRCUALIDADPOOL.
---------------------------------------------------------------------------------

SET NOCOUNT ON 
DECLARE	@vcMensaje	VARCHAR(255) 

	IF  Object_ID('XFKTACJCCTRPOOLATENCION2') IS NOT NULL	
	BEGIN
		ALTER TABLE dbo.TACJCCTRCUALIDADPOOL DROP CONSTRAINT [XFKTACJCCTRPOOLATENCION2]
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al eliminar la fk XFKTACJCCTRPOOLATENCION2 '
			GOTO CtrlErrores
		END		
	END		
	
	IF  Object_ID('XFKTCCJCCTRCUALIDADES') IS NOT NULL	
	BEGIN
		ALTER TABLE dbo.TACJCCTRCUALIDADPOOL DROP CONSTRAINT [XFKTCCJCCTRCUALIDADES]
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al eliminar la fk XFKTCCJCCTRCUALIDADES '
			GOTO CtrlErrores
		END		
	END		
	
	IF  Object_ID('dbo.TACJCCTRCUALIDADPOOL', 'U') IS NOT NULL	
	BEGIN					
		DROP TABLE dbo.TACJCCTRCUALIDADPOOL
		IF (@@ERROR <> 0 )
		BEGIN
			SET @vcMensaje = 'Error al eliminar la tabla TACJCCTRCUALIDADPOOL '
			GOTO CtrlErrores
		END
	END
	
SET NOCOUNT OFF
RETURN

---------------------------------------------------------------------------------
-- MANEJO DE ERRORES
---------------------------------------------------------------------------------
CtrlErrores:
	SET NOCOUNT OFF
		RAISERROR(@vcMensaje ,18,1)
        RETURN
GO

---------------------------------------------------------------------------------
--- Responsable: Esteban Jesús Caro Guzmán
--- Fecha:      Febrero 2014
--- Descripción: Reverso de la tabla TCCJCCTRESTADOTURNO.
---------------------------------------------------------------------------------

SET NOCOUNT ON 
DECLARE	@vcMensaje	VARCHAR(255) 

	IF  Object_ID('XFKTCCJCCTRESTADOTURNO') IS NOT NULL	
	BEGIN
		ALTER TABLE dbo.TACJCCTRHISTORICO DROP CONSTRAINT [XFKTCCJCCTRESTADOTURNO]
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al eliminar la fk XFKTCCJCCTRESTADOTURNO '
			GOTO CtrlErrores
		END		
	END		
	
	IF  Object_ID('dbo.TCCJCCTRESTADOTURNO', 'U') IS NOT NULL	
	BEGIN					
		DROP TABLE dbo.TCCJCCTRESTADOTURNO
		IF (@@ERROR <> 0 )
		BEGIN
			SET @vcMensaje = 'Error al eliminar la tabla TCCJCCTRESTADOTURNO '
			GOTO CtrlErrores
		END
	END
	
SET NOCOUNT OFF
RETURN

---------------------------------------------------------------------------------
-- MANEJO DE ERRORES
---------------------------------------------------------------------------------
CtrlErrores:
	SET NOCOUNT OFF
		RAISERROR(@vcMensaje ,18,1)
        RETURN
GO

---------------------------------------------------------------------------------
--- Responsable: Esteban Jesús Caro Guzmán
--- Fecha:      Febrero 2014
--- Descripción: Reverso de la tabla TACJCCTRHISTORICO.
---------------------------------------------------------------------------------

SET NOCOUNT ON 
DECLARE	@vcMensaje	VARCHAR(255) 

	IF  Object_ID('XFKTACJCCTRTURNO2') IS NOT NULL	
	BEGIN
		ALTER TABLE dbo.TACJCCTRHISTORICO DROP CONSTRAINT [XFKTACJCCTRTURNO2]
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al eliminar la fk XFKTACJCCTRTURNO2 '
			GOTO CtrlErrores
		END		
	END		
	
	IF  Object_ID('XFKTCCJCCTRESTADOTURNO') IS NOT NULL	
	BEGIN
		ALTER TABLE dbo.TACJCCTRHISTORICO DROP CONSTRAINT [XFKTCCJCCTRESTADOTURNO]
		IF @@ERROR <> 0
		BEGIN
			SET @vcMensaje = 'Error al eliminar la fk XFKTCCJCCTRESTADOTURNO '
			GOTO CtrlErrores
		END		
	END		
	
	IF  Object_ID('dbo.TACJCCTRHISTORICO', 'U') IS NOT NULL	
	BEGIN					
		DROP TABLE dbo.TACJCCTRHISTORICO
		IF (@@ERROR <> 0 )
		BEGIN
			SET @vcMensaje = 'Error al eliminar la tabla TACJCCTRHISTORICO '
			GOTO CtrlErrores
		END
	END
	
SET NOCOUNT OFF
RETURN

---------------------------------------------------------------------------------
-- MANEJO DE ERRORES
---------------------------------------------------------------------------------
CtrlErrores:
	SET NOCOUNT OFF
		RAISERROR(@vcMensaje ,18,1)
        RETURN
GO