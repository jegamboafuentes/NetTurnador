----------------------------------------------------------------------------------------------------------------------------------
---Responsable	: 
---Fecha		: Enero 2014
---Descripcion	: REVERSO DE PR00
----------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON
DECLARE @vcMensaje VARCHAR(255)

BEGIN TRAN

IF OBJECT_ID('DBO.PACJCCSNegocioEmpleado','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCSNegocioEmpleado
	IF @@ERROR <> 0
	BEGIN
		SET @vcMensaje = 'Error al eliminar PACJCCSNegocioEmpleado'
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
