IF OBJECT_ID('DBO.PACJCCLUTRCatTipoFila','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRCatTipoFila;
END
GO
---------------------------------------------------------------------------------------------------------      
---Responsable          : Roberto Gonzalez Figueroa
---Fecha                : Sep2013
---Descripcion          : Origen-Turnos; Mantenimiento al Catalogo de Tipos de Fila
---------------------------------------------------------------------------------------------------------      
CREATE PROC dbo.PACJCCLUTRCatTipoFila (
   @piTienda             SMALLINT,
   @piProyectoMQ         INT,
   @piFolioMQ            INT,
   @piTipoFilaID         TINYINT,
   @pcDescripcionTP      CHAR(50),
   @pcUser               CHAR(6)
)

AS

SET NOCOUNT ON

   DECLARE
      @vdActual  SMALLDATETIME,
      @viFILA    TINYINT,
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

   BEGIN TRAN UCatTipoFila
      -- Valida si Existe TipoFila del Turnador
      IF NOT EXISTS (SELECT FITIPOFILAID FROM TCCJCCTRTIPOFILA WITH (NOLOCK) 
                      WHERE FITIPOFILAID = @piTipoFilaID)
      BEGIN 
         -- Inserta Tipo Fila del Turnador
         INSERT TCCJCCTRTIPOFILA (FITIPOFILAID, FCDESCRIPCION, FDFECHAINSERTA, FCUSERINSERTA)
            VALUES (@piTipoFilaID,@pcDescripcionTP, @vdActual, @pcUser)

         IF ( @@ERROR <> 0 )
         BEGIN 
            ROLLBACK TRAN UCatTipoFila
            SET @vcMensaje= 'Error al insertar en tabla TCCJCCTRTIPOFILA.' 
            GOTO CtrlErrores 
         END
      END
      ELSE
      BEGIN 
         -- Actualiza
         UPDATE TCCJCCTRTIPOFILA
            SET FCDESCRIPCION = @pcDescripcionTP
               ,FDFECHAMODIF = @vdActual
               ,FCUSERMODIF = @pcUser
          WHERE FITIPOFILAID = @piTipoFilaID

         IF ( @@ERROR <> 0 )
         BEGIN 
            ROLLBACK TRAN UCatTipoFila
            SET @vcMensaje= 'Error al actualizar ->Nombre<- en tabla TCCJCCTRTIPOFILA.' 
            GOTO CtrlErrores 
         END
      END

      --***************************************
      --********* Arma el mensaje para MQ *****
      SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                            '@piTipoFilaID = [' + CONVERT(VARCHAR, ISNULL(@piTipoFilaID, '')) + ']' + 
                            '@pcDescripcionTP = [' + @pcDescripcionTP + ']' + 
                            '@pcUser = [' + @pcUser + '].'

      -- Genera el mensaje de confirmacion de MQ 
      EXEC @viReturn = SPINSCAJMensajesMQ485 @piProyectoMQ, @piFolioMQ, @vcParametrosMQ, 0, ' ' 

      -- Si existe error 
      IF (@@ERROR <> 0) OR (@viReturn < 0) 
      BEGIN 
         ROLLBACK TRAN UCatTipoFila
         SET @vcMensaje= 'Error al generar la confirmación de MQ en SPINSCAJMensajesMQ485.' 
         GOTO CtrlErrores 
      END 

   COMMIT TRAN UCatTipoFila
   SET NOCOUNT OFF
   RETURN 0  

--******** CtrlAdvertenciaMQ ************
CtrlAdvertenciaMQ:
   SET NOCOUNT OFF 
 
   SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                         '@piTipoFilaID = [' + CONVERT(VARCHAR, ISNULL(@piTipoFilaID, '')) + ']' + 
                         '@pcDescripcionTP = [' + @pcDescripcionTP + ']' + 
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
   SET @vcMensaje = 'Error al Ejecutar el SP -> PACJCCLUTRCatTipoFila. ' + ISNULL (@vcMensaje,'')
 
   SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                         '@piTipoFilaID = [' + CONVERT(VARCHAR, ISNULL(@piTipoFilaID, '')) + ']' + 
                         '@pcDescripcionTP = [' + @pcDescripcionTP + ']' + 
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

EXEC DBO.SPGRANT 'DBO.PACJCCLUTRCatTipoFila'
GO