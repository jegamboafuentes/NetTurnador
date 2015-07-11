IF OBJECT_ID('DBO.PACJCCLUTRHistorico','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRHistorico;
END
GO
---------------------------------------------------------------------------------------------------------      
---Responsable          : Roberto Gonzalez Figueroa
---Fecha                : Sep2013
---Descripcion          : Turnos-Sucursales; Inserta(Actualiza) el nuevo Estatus del Turno
---------------------------------------------------------------------------------------------------------      
CREATE PROC dbo.PACJCCLUTRHistorico (
   @piFecha               INTEGER,
   @piTurno               INTEGER,
   @piEstatusTurno        SMALLINT,
   @pcUSer                CHAR(6)
)

AS

SET NOCOUNT ON

   DECLARE
      @vdFechaHoy    DATETIME,
      @vcMensaje     VARCHAR(255)

   SET @vdFechaHoy = GETDATE()
   
   IF NOT EXISTS (SELECT FIFECHA
                    FROM TACJCCTRHISTORICO WITH (NOLOCK)
                   WHERE FIFECHA = @piFecha AND FITURNOID = @piTurno AND FISTATUSTURNOID = @piEstatusTurno)
   BEGIN
      BEGIN TRAN UTurnos
         IF EXISTS (SELECT FIFECHA FROM TACJCCTRHISTORICO WHERE FIFECHA = @piFecha
                       AND FITURNOID = @piTurno AND FISTATUSTURNOID = @piEstatusTurno)
		 BEGIN
            UPDATE TACJCCTRHISTORICO 
               SET FDFECHAINSERTA = @vdFechaHoy
                  ,FCUSERINSERTA = @pcUser
             WHERE FIFECHA = @piFecha
               AND FITURNOID = @piTurno
               AND FISTATUSTURNOID = @piEstatusTurno

            IF (@@ERROR <> 0)
            BEGIN 
               ROLLBACK TRAN UTurnos
               SET @vcMensaje= 'Error al actualizar TACJCCTRHISTORICO' 
               GOTO CtrlErrores
            END
         END
         ELSE
         BEGIN
            -- INSERTA HISTORICO-TURNO
            INSERT INTO TACJCCTRHISTORICO (FIFECHA, FITURNOID, FISTATUSTURNOID, FDACTUALIZACION, 
                                           FDFECHAINSERTA, FCUSERINSERTA)
                   VALUES (@piFecha, @piTurno, @piEstatusTurno, @vdFechaHoy, @vdFechaHoy, @pcUser)

            IF (@@ERROR <> 0)
            BEGIN 
               ROLLBACK TRAN UTurnos
               SET @vcMensaje= 'Error al insertar el estatus del Turno.TACJCCTRHISTORICO' 
               GOTO CtrlErrores
            END
         END

         --** ACTUALIZA FISTATUSTURNO DE TURNO
         UPDATE TACJCCTRTURNO
            SET FISTATUSTURNO = @piEstatusTurno
               ,FDFECHAMODIF = @vdFechaHoy
               ,FCUSERMODIF = @pcUser
          WHERE FIFECHA = @piFecha
            AND FITURNOID = @piTurno

         IF (@@ERROR <> 0)
         BEGIN 
            ROLLBACK TRAN UTurnos
            SET @vcMensaje= 'Error al Actualizar en tabla TACJCCTRTURNO.1' 
            GOTO CtrlErrores
         END
      COMMIT TRAN UTurnos
      SELECT 0
   END
   ELSE
   BEGIN
      SELECT -1
   END

   SET NOCOUNT OFF
   RETURN 0  

--******** CtrlErrores ***********
CtrlErrores:
   SET NOCOUNT OFF
   SET @vcMensaje = 'Error al Ejecutar el SP -> PACJCCLUTRHistorico. ' + ISNULL (@vcMensaje,'')
 
   -- Termina con error 
   RAISERROR (@vcMensaje, 18, 1) 
   RETURN -1
GO

EXEC DBO.SPGRANT 'DBO.PACJCCLUTRHistorico'
GO