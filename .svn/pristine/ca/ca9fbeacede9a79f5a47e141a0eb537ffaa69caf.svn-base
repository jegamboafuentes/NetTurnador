----------------------------------------------------------------------------------------------------------------------------------
---Responsable	: Esteban Jesús Caro Guzmán
---Fecha		: Febrero 2014
---Descripcion	: REVERSO DE PR_Turnador
----------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON
DECLARE @vcMensaje VARCHAR(255)

BEGIN TRAN

IF OBJECT_ID('DBO.PACJCCLUTRHistorico','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRHistorico;
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al eliminar PACJCCLUTRHistorico '
		GOTO CtrlErrores 
	END
END

IF OBJECT_ID('DBO.PACJCCLITRAsignaVirtual','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLITRAsignaVirtual;
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al eliminar PACJCCLITRAsignaVirtual '
		GOTO CtrlErrores 
	END
END

IF OBJECT_ID('DBO.PACJCCLITRTurno','P') IS NOT NULL
BEGIN
	DROP PROC dbo.PACJCCLITRTurno;
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al eliminar PACJCCLITRTurno '
		GOTO CtrlErrores 
	END
END

IF OBJECT_ID('DBO.PACJCCLSTRPoolAtencion','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLSTRPoolAtencion;
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al eliminar PACJCCLSTRPoolAtencion '
		GOTO CtrlErrores 
	END
END

IF OBJECT_ID('DBO.PACJCCLSTRTurno','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLSTRTurno;
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al eliminar PACJCCLSTRTurno '
		GOTO CtrlErrores 
	END
END

IF OBJECT_ID('dbo.PACJCCLSTRTurnos','P') IS NOT NULL
BEGIN
	DROP PROC dbo.PACJCCLSTRTurnos;
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al eliminar PACJCCLSTRTurnos '
		GOTO CtrlErrores 
	END
END

IF OBJECT_ID('DBO.PACJCCLSTRUnidadesNegocio','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLSTRUnidadesNegocio;
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al eliminar PACJCCLSTRUnidadesNegocio '
		GOTO CtrlErrores 
	END
END

IF OBJECT_ID('DBO.PACJCCLUTRApropiar','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRApropiar;
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al eliminar PACJCCLUTRApropiar '
		GOTO CtrlErrores 
	END
END

IF OBJECT_ID('DBO.PACJCCLUTRAsigna','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRAsigna;
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al eliminar PACJCCLUTRAsigna '
		GOTO CtrlErrores 
	END
END

IF OBJECT_ID('DBO.PACJCCLUTRCaduca','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRCaduca;
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al eliminar PACJCCLUTRCaduca '
		GOTO CtrlErrores 
	END
END

IF OBJECT_ID('DBO.PACJCCLUTRCatCualidades','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRCatCualidades;
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al eliminar PACJCCLUTRCatCualidades '
		GOTO CtrlErrores 
	END
END

IF OBJECT_ID('DBO.PACJCCLUTRCatEstadoPool','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRCatEstadoPool;
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al eliminar PACJCCLUTRCatEstadoPool '
		GOTO CtrlErrores 
	END
END

IF OBJECT_ID('DBO.PACJCCLUTRCatEstadoTurno','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRCatEstadoTurno;
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al eliminar PACJCCLUTRCatEstadoTurno '
		GOTO CtrlErrores 
	END
END

IF OBJECT_ID('DBO.PACJCCLUTRCatOrigen','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRCatOrigen;
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al eliminar PACJCCLUTRCatOrigen '
		GOTO CtrlErrores 
	END
END

IF OBJECT_ID('DBO.PACJCCLUTRCatTipoFila','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRCatTipoFila;
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al eliminar PACJCCLUTRCatTipoFila '
		GOTO CtrlErrores 
	END
END

IF OBJECT_ID('DBO.PACJCCLUTRCatUnidadesNegocio','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRCatUnidadesNegocio;
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al eliminar PACJCCLUTRCatUnidadesNegocio '
		GOTO CtrlErrores 
	END
END

IF OBJECT_ID('DBO.PACJCCLUTRPoolAtencion','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRPoolAtencion;
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al eliminar PACJCCLUTRPoolAtencion '
		GOTO CtrlErrores 
	END
END

IF OBJECT_ID('DBO.PACJCCLUTRPoolUnidad','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRPoolUnidad;
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al eliminar PACJCCLUTRPoolUnidad '
		GOTO CtrlErrores 
	END
END

IF OBJECT_ID('DBO.PACJCCLUTRPuntoAtencion','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRPuntoAtencion;
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al eliminar PACJCCLUTRPuntoAtencion '
		GOTO CtrlErrores 
	END
END

COMMIT TRAN
SET NOCOUNT OFF  
RETURN   
-----------------------------------------------------------------------------------------  
--Manejo de Errores  
-----------------------------------------------------------------------------------------  
CtrlErrores:  
	SET NOCOUNT OFF
	IF @@TRANCOUNT>0
		ROLLBACK TRAN

    RAISERROR ( @vcMensaje , 18 , 1 )  

RETURN 
