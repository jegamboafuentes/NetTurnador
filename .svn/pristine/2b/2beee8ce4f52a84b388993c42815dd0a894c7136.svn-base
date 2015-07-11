--------------------------------------------------------------------------------------------------
---Responsable : 
---Fecha       : Enero 2014
---Descripcion : Inserta en catálogos de funciones
--------------------------------------------------------------------------------------------------
SET NOCOUNT ON	
DECLARE @vcMensaje	VARCHAR(255)

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocio WITH(NOLOCK) WHERE fiNegocioID = 1)
BEGIN
	INSERT INTO TCCJCCNegocio (fiNegocioID, fcDescripcion)
		VALUES(1, 'Prestamos')
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocio fiNegocioID 1'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocio WITH(NOLOCK) WHERE fiNegocioID = 2)
BEGIN
	INSERT INTO TCCJCCNegocio (fiNegocioID, fcDescripcion)
		VALUES(2, 'Captación')
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocio fiNegocioID 2'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocio WITH(NOLOCK) WHERE fiNegocioID = 3)
BEGIN
	INSERT INTO TCCJCCNegocio (fiNegocioID, fcDescripcion)
		VALUES(3, 'Mercancias')
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocio fiNegocioID 3'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocio WITH(NOLOCK) WHERE fiNegocioID = 4)
BEGIN
	INSERT INTO TCCJCCNegocio (fiNegocioID, fcDescripcion)
		VALUES(4, 'Telefonia y Computo')
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocio fiNegocioID 4'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocio WITH(NOLOCK) WHERE fiNegocioID = 5)
BEGIN
	INSERT INTO TCCJCCNegocio (fiNegocioID, fcDescripcion)
		VALUES(5, 'Italika')
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocio fiNegocioID 5'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocio WITH(NOLOCK) WHERE fiNegocioID = 6)
BEGIN
	INSERT INTO TCCJCCNegocio (fiNegocioID, fcDescripcion)
		VALUES(6, 'SIPA')
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocio fiNegocioID 6'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocio WITH(NOLOCK) WHERE fiNegocioID = 7)
BEGIN
	INSERT INTO TCCJCCNegocio (fiNegocioID, fcDescripcion)
		VALUES(7, 'Centro Comercial')
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocio fiNegocioID 7'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocio WITH(NOLOCK) WHERE fiNegocioID = 8)
BEGIN
	INSERT INTO TCCJCCNegocio (fiNegocioID, fcDescripcion)
		VALUES(8, 'Afore y Seguros')
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocio fiNegocioID 8'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocio WITH(NOLOCK) WHERE fiNegocioID = 9)
BEGIN
	INSERT INTO TCCJCCNegocio (fiNegocioID, fcDescripcion)
		VALUES(9, 'Presta Prenda')
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocio fiNegocioID 9'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocio WITH(NOLOCK) WHERE fiNegocioID = 10)
BEGIN
	INSERT INTO TCCJCCNegocio (fiNegocioID, fcDescripcion)
		VALUES(10, 'MicroNegocio')
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocio fiNegocioID 10'
		GOTO CtrlErrores
	END
END

---------------------------
--------- Funcion X Negocio
---------------------------

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocioXFuncion WITH(NOLOCK) WHERE fiFuncionID = 1772 AND fiNegocioID = 1)
BEGIN
	INSERT INTO TCCJCCNegocioXFuncion (fiFuncionID, fiNegocioID)
		VALUES(1772, 1)
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocioXFuncion fiFuncionID 1772'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocioXFuncion WITH(NOLOCK) WHERE fiFuncionID = 1987 AND fiNegocioID = 1)
BEGIN
	INSERT INTO TCCJCCNegocioXFuncion (fiFuncionID, fiNegocioID)
		VALUES(1987, 1)
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocioXFuncion fiFuncionID 1987'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocioXFuncion WITH(NOLOCK) WHERE fiFuncionID = 1910 AND fiNegocioID = 1)
BEGIN
	INSERT INTO TCCJCCNegocioXFuncion (fiFuncionID, fiNegocioID)
		VALUES(1910, 1)
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocioXFuncion fiFuncionID 1910'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocioXFuncion WITH(NOLOCK) WHERE fiFuncionID = 1781 AND fiNegocioID = 2)
BEGIN
	INSERT INTO TCCJCCNegocioXFuncion (fiFuncionID, fiNegocioID)
		VALUES(1781, 2)
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocioXFuncion fiFuncionID 1781'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocioXFuncion WITH(NOLOCK) WHERE fiFuncionID = 1991 AND fiNegocioID = 2)
BEGIN
	INSERT INTO TCCJCCNegocioXFuncion (fiFuncionID, fiNegocioID)
		VALUES(1991, 2)
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocioXFuncion fiFuncionID 1991'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocioXFuncion WITH(NOLOCK) WHERE fiFuncionID = 1778 AND fiNegocioID = 3)
BEGIN
	INSERT INTO TCCJCCNegocioXFuncion (fiFuncionID, fiNegocioID)
		VALUES(1778, 3)
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocioXFuncion fiFuncionID 1778'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocioXFuncion WITH(NOLOCK) WHERE fiFuncionID = 1988 AND fiNegocioID = 3)
BEGIN
	INSERT INTO TCCJCCNegocioXFuncion (fiFuncionID, fiNegocioID)
		VALUES(1988, 3)
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocioXFuncion fiFuncionID 1988'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocioXFuncion WITH(NOLOCK) WHERE fiFuncionID = 1795 AND fiNegocioID = 4)
BEGIN
	INSERT INTO TCCJCCNegocioXFuncion (fiFuncionID, fiNegocioID)
		VALUES(1795, 4)
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocioXFuncion fiFuncionID 1795'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocioXFuncion WITH(NOLOCK) WHERE fiFuncionID = 1990 AND fiNegocioID = 4)
BEGIN
	INSERT INTO TCCJCCNegocioXFuncion (fiFuncionID, fiNegocioID)
		VALUES(1990, 4)
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocioXFuncion fiFuncionID 1990'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocioXFuncion WITH(NOLOCK) WHERE fiFuncionID = 1777 AND fiNegocioID = 5)
BEGIN
	INSERT INTO TCCJCCNegocioXFuncion (fiFuncionID, fiNegocioID)
		VALUES(1777, 5)
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocioXFuncion fiFuncionID 1777'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocioXFuncion WITH(NOLOCK) WHERE fiFuncionID = 1992 AND fiNegocioID = 5)
BEGIN
	INSERT INTO TCCJCCNegocioXFuncion (fiFuncionID, fiNegocioID)
		VALUES(1992, 5)
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocioXFuncion fiFuncionID 1992'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocioXFuncion WITH(NOLOCK) WHERE fiFuncionID = 2005 AND fiNegocioID = 6)
BEGIN
	INSERT INTO TCCJCCNegocioXFuncion (fiFuncionID, fiNegocioID)
		VALUES(2005, 6)
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocioXFuncion fiFuncionID 2005'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocioXFuncion WITH(NOLOCK) WHERE fiFuncionID = 1996 AND fiNegocioID = 6)
BEGIN
	INSERT INTO TCCJCCNegocioXFuncion (fiFuncionID, fiNegocioID)
		VALUES(1996, 6)
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocioXFuncion fiFuncionID 1996'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocioXFuncion WITH(NOLOCK) WHERE fiFuncionID = 2006 AND fiNegocioID = 7)
BEGIN
	INSERT INTO TCCJCCNegocioXFuncion (fiFuncionID, fiNegocioID)
		VALUES(2006, 7)
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocioXFuncion fiFuncionID 2006'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocioXFuncion WITH(NOLOCK) WHERE fiFuncionID = 2010 AND fiNegocioID = 7)
BEGIN
	INSERT INTO TCCJCCNegocioXFuncion (fiFuncionID, fiNegocioID)
		VALUES(2010, 7)
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocioXFuncion fiFuncionID 2010'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocioXFuncion WITH(NOLOCK) WHERE fiFuncionID = 1768 AND fiNegocioID = 8)
BEGIN
	INSERT INTO TCCJCCNegocioXFuncion (fiFuncionID, fiNegocioID)
		VALUES(1768, 8)
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocioXFuncion fiFuncionID 1768'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocioXFuncion WITH(NOLOCK) WHERE fiFuncionID = 1993 AND fiNegocioID = 8)
BEGIN
	INSERT INTO TCCJCCNegocioXFuncion (fiFuncionID, fiNegocioID)
		VALUES(1993, 8)
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocioXFuncion fiFuncionID 1993'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocioXFuncion WITH(NOLOCK) WHERE fiFuncionID = 2007 AND fiNegocioID = 9)
BEGIN
	INSERT INTO TCCJCCNegocioXFuncion (fiFuncionID, fiNegocioID)
		VALUES(2007, 9)
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocioXFuncion fiFuncionID 2007'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocioXFuncion WITH(NOLOCK) WHERE fiFuncionID = 2011 AND fiNegocioID = 9)
BEGIN
	INSERT INTO TCCJCCNegocioXFuncion (fiFuncionID, fiNegocioID)
		VALUES(2011, 9)
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocioXFuncion fiFuncionID 2011'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocioXFuncion WITH(NOLOCK) WHERE fiFuncionID = 1986 AND fiNegocioID = 10)
BEGIN
	INSERT INTO TCCJCCNegocioXFuncion (fiFuncionID, fiNegocioID)
		VALUES(1986, 10)
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocioXFuncion fiFuncionID 1986'
		GOTO CtrlErrores
	END
END

IF NOT EXISTS(SELECT fiNegocioID FROM TCCJCCNegocioXFuncion WITH(NOLOCK) WHERE fiFuncionID = 1995 AND fiNegocioID = 10)
BEGIN
	INSERT INTO TCCJCCNegocioXFuncion (fiFuncionID, fiNegocioID)
		VALUES(1995, 10)
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al insertar en TCCJCCNegocioXFuncion fiFuncionID 1995'
		GOTO CtrlErrores
	END
END


SET NOCOUNT OFF
RETURN
-----------------------------------------------------------------------------------------
--- Manejo de Errores
-----------------------------------------------------------------------------------------
CtrlErrores:
	SET NOCOUNT OFF
	IF ISNULL(@vcMensaje, '') = ''
	BEGIN  
		SET @vcMensaje = 'Ocurrió un error al ejecutar AC'
		RAISERROR( @vcMensaje, 18, 1)
	END
	ELSE
	BEGIN
		RAISERROR( @vcMensaje, 18, 1)
	END
RETURN	
GO