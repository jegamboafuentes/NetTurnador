IF OBJECT_ID('DBO.PACJCCLUTRCatOrigen','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRCatOrigen;
END
GO
---------------------------------------------------------------------------------------------------------      
---Responsable          : Roberto Gonzalez Figueroa
---Fecha                : Sep2013
---Descripcion          : Origen-Turnos; Mantenimiento al Catalogo de Origen de Turnos
---------------------------------------------------------------------------------------------------------      
CREATE PROC dbo.PACJCCLUTRCatOrigen (
   @piTienda             SMALLINT,
   @piProyectoMQ         INT,
   @piFolioMQ            INT,
   @piOrigenTurno        TINYINT,
   @pcDescripcionOrig    CHAR(50),
   @pcUser               CHAR(6)
)

AS

SET NOCOUNT ON

   DECLARE 
      @vdActual        SMALLDATETIME,
      @viSIGTEORIGEN   TINYINT,
      @viTienda        SMALLINT,
      @viReturn        INTEGER,
      @vcMensaje       VARCHAR(255),
      @vcMensajeMQ     CHAR(70),
      @vcParametrosMQ  CHAR(200)

   SET @vdActual = GETDATE()

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

   BEGIN TRAN UCatOrigen
      -- Valida si Existe el Origen del Turnador
      IF NOT EXISTS (SELECT FIORIGENID FROM TCCJCCTRORIGEN WITH (NOLOCK) 
                      WHERE FIORIGENID = @piOrigenTurno)
      BEGIN 
         -- Inserta Nuevo Origen de Turnador
         INSERT TCCJCCTRORIGEN (FIORIGENID, FCDESCRIPCION, FDFECHAINSERTA, FCUSERINSERTA)
            VALUES (@piOrigenTurno,@pcDescripcionOrig, @vdActual, @pcUser)

         IF ( @@ERROR <> 0 )
         BEGIN 
            ROLLBACK TRAN UCatOrigen
            SET @vcMensaje= 'Error al insertar en tabla TCCJCCTRORIGEN.' 
            GOTO CtrlErrores 
         END
      END
      ELSE
      BEGIN 
         -- Actualiza
         UPDATE TCCJCCTRORIGEN
            SET FCDESCRIPCION = @pcDescripcionOrig
               ,FDFECHAMODIF = @vdActual
               ,FCUSERMODIF = @pcUser
          WHERE FIORIGENID = @piOrigenTurno

         IF ( @@ERROR <> 0 )
         BEGIN 
            ROLLBACK TRAN UCatOrigen
            SET @vcMensaje= 'Error al actualizar en tabla TCCJCCTRORIGEN.' 
            GOTO CtrlErrores 
         END
      END

      --***************************************
      --********* Arma el mensaje para MQ *****
      SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                            '@piOrigenTurno = [' + CONVERT(VARCHAR, ISNULL(@piOrigenTurno, '')) + ']' + 
                            '@pcDescripcionOrig = [' + @pcDescripcionOrig + ']' + 
                            '@pcUser = [' + @pcUser + '].'

      -- Genera el mensaje de confirmacion de MQ 
      EXEC @viReturn = SPINSCAJMensajesMQ485 @piProyectoMQ, @piFolioMQ, @vcParametrosMQ, 0, ' ' 

      -- Si existe error 
      IF (@@ERROR <> 0) OR (@viReturn < 0) 
      BEGIN 
         ROLLBACK TRAN UCatOrigen
         SET @vcMensaje= 'Error al generar la confirmación de MQ en SPINSCAJMensajesMQ485.' 
         GOTO CtrlErrores 
      END 

   COMMIT TRAN UCatOrigen
   SET NOCOUNT OFF
   RETURN 0

--******** CtrlAdvertenciaMQ ************
CtrlAdvertenciaMQ:
   SET NOCOUNT OFF 
 
   SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                         '@piOrigenTurno = [' + CONVERT(VARCHAR, ISNULL(@piOrigenTurno, '')) + ']' + 
                         '@pcDescripcionOrig = [' + @pcDescripcionOrig + ']' + 
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
   SET @vcMensaje = 'Error al Ejecutar el SP -> PACJCCLUTRCatOrigen. ' + ISNULL (@vcMensaje,'')
 
   SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                         '@piOrigenTurno = [' + CONVERT(VARCHAR, ISNULL(@piOrigenTurno, '')) + ']' + 
                         '@pcDescripcionOrig = [' + @pcDescripcionOrig + ']' + 
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

EXEC DBO.SPGRANT 'DBO.PACJCCLUTRCatOrigen'
GO