IF OBJECT_ID('DBO.PACJCCSNegocioEmpleado','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCSNegocioEmpleado
END
GO
/*---------------------------------------------------------------------------------------------------------    
---Responsable          :  
---Fecha                : Enero 2014
---Descripcion          : Informacion sobre Unidad de Negocio del Empleado.
---------------------------------------------------------------------------------------------------------    */

CREATE PROCEDURE dbo.PACJCCSNegocioEmpleado(        
	@pcEmpleado  VARCHAR(10)) AS    

	DECLARE
		@vcMensaje			VARCHAR(255),
		@vcFuncionID		VARCHAR(8),
		@vcNoCero			VARCHAR(1),
		@viNegocioID		TINYINT,
		@vcDescripcion		VARCHAR(255),
		@vcDescripcionUN	VARCHAR(255)
		 
SET NOCOUNT ON
 
	IF EXISTS (SELECT fcStell FROM gdiEmpleado WITH(NOLOCK) WHERE fcEmpNo = @pcEmpleado)
	BEGIN
		IF EXISTS(SELECT fcEmpNo FROM EMPLEADO WITH(NOLOCK) WHERE fcEmpNo = @pcEmpleado AND fiEmpStatus = 1)
		BEGIN
			--buscar su fcStell en la tabla gdiEmpleado
			SET @vcFuncionID = (SELECT fcStell FROM gdiEmpleado WITH(NOLOCK) WHERE fcEmpNo = @pcEmpleado)
			
			--proceso para quitar los ceros de la izquierda del fcStell
			SET @vcNoCero = SUBSTRING(@vcFuncionID,1,1)
			WHILE (@vcNoCero = '0')
			BEGIN
				SET @vcFuncionID = SUBSTRING(@vcFuncionID,2, LEN(@vcFuncionID) - 1)
				SET @vcNoCero = SUBSTRING(@vcFuncionID,1,1)		
			END
			
			SELECT @viNegocioID = fiNegocioID FROM TCCJCCNegocioXFuncion WITH(NOLOCK) WHERE fiFuncionID = CONVERT(INT,@vcFuncionID)
			SELECT @vcDescripcionUN = RTRIM(LTRIM(fcDescripcion)) FROM TCCJCCNegocio WITH(NOLOCK) WHERE fiNegocioID = @viNegocioID
			SELECT @vcDescripcion = RTRIM(LTRIM(fcDescripcion)) FROM TCCJCFFuncionesSAP WITH(NOLOCK) WHERE fiFuncionID = CONVERT(INT,@vcFuncionID)
			
			SELECT @viNegocioID UnidadNegocio, @vcDescripcionUN DescripcionUN, @vcDescripcion Descripcion
		END
		ELSE
		BEGIN
			SET @vcMensaje = 'El empleado no se encuentra activo'
			GOTO ERROR
		END
	END
	ELSE
	BEGIN
		SET @vcMensaje = 'El empleado no existe en gdiEmpleado, favor de validar con Desarrollo Tecnologico'
		GOTO ERROR
	END

	SET NOCOUNT OFF  
	RETURN 0

------------------------------------------------------------------------------------------------------------------------------------

ERROR:  
	SET NOCOUNT OFF	
	IF ISNULL(@vcMensaje, '') = ''
	BEGIN
		SET @vcMensaje = 'OCURRIO UN ERROR EN EL SP -> PACJCCSNegocioEmpleado. ' 
		RAISERROR( @vcMensaje, 18, 1)    
	END
	ELSE
	BEGIN
	  RAISERROR( @vcMensaje, 18, 1)    
	END
 
 RETURN -1

GO

EXEC dbo.SPGRANT 'DBO.PACJCCSNegocioEmpleado'

GO
