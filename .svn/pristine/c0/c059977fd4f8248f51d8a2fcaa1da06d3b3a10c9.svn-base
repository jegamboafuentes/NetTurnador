IF OBJECT_ID('DBO.PACJCCLUTRCatUnidadesNegocio','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRCatUnidadesNegocio;
END
GO
---------------------------------------------------------------------------------------------------------      
---Responsable          : Roberto Gonzalez Figueroa
---Fecha                : Sep2013
---Descripcion          : Origen-Turnos; Mantenimiento al Catalogo de Unidade de Negocio
---------------------------------------------------------------------------------------------------------      
CREATE PROC dbo.PACJCCLUTRCatUnidadesNegocio (
   @piTienda             SMALLINT,
   @piProyectoMQ         INT,
   @piFolioMQ            INT,
   @piUndNegocioID       TINYINT,
   @pcDescripcionOrig    CHAR(50),
   @pcRutaImagen         CHAR(250),
   @piEstatus            TINYINT,
   @plPrestamos          BIT,
   @pcValorColor         CHAR(30),
   @pcValorZona          CHAR(30),
   @pcUser               CHAR(6)
)

AS

SET NOCOUNT ON

   DECLARE
      @vdActual SMALLDATETIME,
      @viTienda        SMALLINT,
      @viReturn        INTEGER,
      @vcMensaje       VARCHAR(255),
      @vcMensajeMQ     CHAR(70),
      @vcParametrosMQ  CHAR(200),
      @vcValorColor    CHAR(30),
      @vcValorZona     CHAR(30)

   SELECT @vdActual = GETDATE(), @vcValorColor = '#378266', @vcValorZona = ''

   IF (@pcValorColor IS NOT NULL) 
      SET @vcValorColor = @pcValorColor
	  
   IF (@pcValorZona IS NOT NULL) 
      SET @vcValorZona = @pcValorZona

   -- Asigna viTienda de la tabla CONTROL
   SELECT @viTienda = fiNoTienda FROM CONTROL WITH (NOLOCK) 

   -- Valida si Existe el folio MQ 
   IF EXISTS (SELECT fiFolio FROM CAJBITMensajesMQ WITH (NOLOCK) 
               WHERE fiProyecto = @piProyectoMQ AND fiFolio = @piFolioMQ)
   BEGIN 
      -- Se sale ya que este caso no debe pasar 
      SET NOCOUNT OFF 
      RETURN 0 
   END 

   -- Valida Tienda de tabla Control vs Parametro de Tienda
   IF (@piTienda <> @viTienda) 
   BEGIN 
      SET @vcMensaje = 'El valor de la Tienda ->' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + 
                       '<- NO corresponde a la sucursal.' 
      GOTO CtrlAdvertenciaMQ 
   END

   BEGIN TRAN UManttoUndNegocio
      -- Valida si Existe la Unidad de Negocio del Turnador
      IF NOT EXISTS (SELECT FIUNIDADNEGOCIOID FROM TCCJCCTRUNIDADNEGOCIO WITH (NOLOCK) 
                      WHERE FIUNIDADNEGOCIOID = @piUndNegocioID )
      BEGIN 
         -- Inserta la Nueva Unidad de Negocio de Turnador
         INSERT TCCJCCTRUNIDADNEGOCIO (FIUNIDADNEGOCIOID, FCDESCRIPCION, FCRUTAIMAGEN, FISTATUSUNDNEG, 
                                       FLPRESTAMOS, FCCOLOR, FCZONA, FDFECHAINSERTA, FCUSERINSERTA)
                 VALUES (@piUndNegocioID, @pcDescripcionOrig, @pcRutaImagen, @piEstatus, @plPrestamos,
				         @vcValorColor, @vcValorZona, @vdActual, @pcUser)

         IF ( @@ERROR <> 0 )
         BEGIN 
	        ROLLBACK TRAN UManttoUndNegocio
            SET @vcMensaje= 'Error al insertar en tabla TCCJCCTRUNIDADNEGOCIO.' 
            GOTO CtrlErrores 
         END
      END
      ELSE
      BEGIN 
         -- Actualiza
         UPDATE TCCJCCTRUNIDADNEGOCIO
            SET FCDESCRIPCION = @pcDescripcionOrig
               ,FCRUTAIMAGEN = @pcRutaImagen
               ,FISTATUSUNDNEG = @piEstatus
               ,FLPRESTAMOS = @plPrestamos
		       ,FCCOLOR = @vcValorColor
               ,FCZONA = @vcValorZona
               ,FDFECHAMODIF = @vdActual
               ,FCUSERMODIF = @pcUser
          WHERE FIUNIDADNEGOCIOID = @piUndNegocioID 

         IF ( @@ERROR <> 0 )
         BEGIN 
	        ROLLBACK TRAN UManttoUndNegocio
            SET @vcMensaje= 'Error al actualizar ->Nombre<- en tabla TCCJCCTRUNIDADNEGOCIO.' 
            GOTO CtrlErrores 
         END
      END

      --***************************************
      --********* Arma el mensaje para MQ *****
      SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                            '@piUndNegocioID = [' + CONVERT(VARCHAR, ISNULL(@piUndNegocioID, '')) + ']' + 
                            '@pcDescripcionOrig = [' + @pcDescripcionOrig + ']' + 
                            '@pcRutaImagen = [' + @pcRutaImagen + ']' + 
                            '@piEstatus = [' + CONVERT(VARCHAR, ISNULL(@piEstatus, '')) + ']' + 
                            '@plPrestamos = [' + CONVERT(VARCHAR, CONVERT(INT,@plPrestamos)) + ']' + 
                            '@pcColor = [' + @vcValorColor + ']' + 
                            '@pcZona = [' + @vcValorZona + ']' + 
                            '@pcUser = [' + @pcUser + '].'

      -- Genera el mensaje de confirmacion de MQ 
      EXEC @viReturn = SPINSCAJMensajesMQ485 @piProyectoMQ, @piFolioMQ, @vcParametrosMQ, 0, ' ' 

      -- Si existe error 
      IF (@@ERROR <> 0) OR (@viReturn < 0) 
      BEGIN 
         ROLLBACK TRAN UManttoUndNegocio
         SET @vcMensaje= 'Error al generar la confirmación de MQ en SPINSCAJMensajesMQ485.' 
         GOTO CtrlErrores 
      END 

   COMMIT TRAN UManttoUndNegocio

   SET NOCOUNT OFF
   RETURN 0  

--******** CtrlAdvertenciaMQ ************
CtrlAdvertenciaMQ:
   SET NOCOUNT OFF 
 
   SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                         '@piUndNegocioID = [' + CONVERT(VARCHAR, ISNULL(@piUndNegocioID, '')) + ']' + 
                         '@pcDescripcionOrig = [' + @pcDescripcionOrig + ']' + 
                         '@pcRutaImagen = [' + @pcRutaImagen + ']' + 
                         '@piEstatus = [' + CONVERT(VARCHAR, ISNULL(@piEstatus, '')) + ']' + 
                         '@plPrestamos = [' + CONVERT(VARCHAR, CONVERT(INT,@plPrestamos)) + ']' + 
                         '@pcColor = [' + @vcValorColor + ']' + 
                         '@pcZona = [' + @vcValorZona + ']' + 
                         '@pcUser = [' + @pcUser + '].'
  
   -- Asigna el mensaje MQ 
   SET @vcMensajeMQ =CONVERT(CHAR (70), @vcMensaje) 
 
   -- Envia respuesta MQ 
   EXEC @viReturn = SPINSCAJMensajesMQ485 @piProyectoMQ, @piFolioMQ, 
                     @vcParametrosMQ, 1,  
                     @vcMensajeMQ 

   IF (@@ERROR <> 0) OR (@viReturn < 0) 
   BEGIN 
      SET @vcMensaje= 'Error al generar la confirmación de error MQ en SPINSCAJMensajesMQ485.' 
      GOTO CtrlErrores 
   END 

   RETURN 0 

--******** CtrlErrores ***********
CtrlErrores:
   SET NOCOUNT OFF
   SET @vcMensaje = 'Error al Ejecutar el SP -> PACJCCLUTRCatUnidadesNegocio. ' + ISNULL (@vcMensaje,'')
 
   SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                         '@piUndNegocioID = [' + CONVERT(VARCHAR, ISNULL(@piUndNegocioID, '')) + ']' + 
                         '@pcDescripcionOrig = [' + @pcDescripcionOrig + ']' + 
                         '@pcRutaImagen = [' + @pcRutaImagen + ']' + 
                         '@piEstatus = [' + CONVERT(VARCHAR, ISNULL(@piEstatus, '')) + ']' + 
                         '@plPrestamos = [' + CONVERT(VARCHAR, CONVERT(INT,@plPrestamos)) + ']' + 
                         '@pcColor = [' + @vcValorColor + ']' + 
                         '@pcZona = [' + @vcValorZona + ']' + 
                         '@pcUser = [' + @pcUser + '].'
 
    -- Asigna el mensaje MQ 
   SET @vcMensajeMQ =CONVERT(CHAR(70), @vcMensaje) 
 
   -- Envia respuesta MQ 
   EXEC @viReturn = SPINSCAJMensajesMQ485 @piProyectoMQ, @piFolioMQ, 
                     @vcParametrosMQ, 0, 
                     @vcMensajeMQ 

   IF (@@ERROR <> 0) OR (@viReturn < 0) 
   BEGIN 
      SET @vcMensaje= 'Error al generar la confirmación de error MQ en SPINSCAJMensajesMQ485.'
   END 

   -- Termina con error 
   RAISERROR (@vcMensaje, 18, 1) 
   RETURN -1
GO

EXEC DBO.SPGRANT 'DBO.PACJCCLUTRCatUnidadesNegocio'
GO
