IF OBJECT_ID('DBO.PACJCCLITRTurno','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLITRTurno;
END
GO
---------------------------------------------------------------------------------------------------------      
---Responsable          : Roberto Gonzalez Figueroa
---Fecha                : Sep2013
---Descripcion          : Turnos Normales-Sucursales; Generacion del Siguiente Turno Normal por Fecha
---------------------------------------------------------------------------------------------------------      
CREATE PROC dbo.PACJCCLITRTurno (
   @piOrigenTurno        INTEGER,
   @piUndNegocio         SMALLINT,
   @piFilaId             SMALLINT,
   @piSeguimiento        INTEGER,
   @pcUser               CHAR(6),
   @piTurno              INTEGER OUTPUT,
   @piFecha              INTEGER OUTPUT
)

AS

SET NOCOUNT ON

   DECLARE
      @vdFecHoy       DATETIME,
      @viFecHoyCorta  INTEGER,
      @viTURNO        INTEGER,
      @vcMensaje      VARCHAR(255),
	  @viEstatusTurno INT,
	  @viAgregaDias   INT,
      @viValorCero    SMALLINT,
      @viValorUno     SMALLINT

   SELECT @vdFecHoy = GETDATE(), @viAgregaDias = 1, @viValorCero = 0, @viValorUno = 1
   SELECT @viFecHoyCorta = CONVERT(CHAR(8), @vdFecHoy, 112), @viEstatusTurno = 1

   BEGIN TRAN ObtenITurnos
      SELECT @viTurno = ISNULL(FITURNOID,0)+@viValorUno
        FROM TACJCCTRTURNO WITH (ROWLOCK, UPDLOCK)
       WHERE FIFECHA = @viFecHoyCorta

      IF (@viTurno IS NULL)
         SET @viTurno = @viValorUno
      
      SELECT @piTurno = @viTurno, @piFecha = @viFecHoyCorta

      -- Valida si existe el Turno
      IF NOT EXISTS (SELECT FITURNOID FROM TACJCCTRTURNO WITH (NOLOCK) 
                      WHERE FIFECHA = @viFecHoyCorta AND FITURNOID = @viTURNO)
      BEGIN 
         -- INSERTA TURNO
         INSERT INTO TACJCCTRTURNO (FIFECHA, FITURNOID, FIORIGENID, FIFILAID, FIUNIDADNEGOCIOID, 
                                    FITURNOSEGUIMIENTO, FIPRIORIDAD, FISTATUSTURNO, FIVIRTUAL, 
                                    FDFECHAINSERTA, FCUSERINSERTA)
                VALUES (@viFecHoyCorta, @piTurno, @piOrigenTurno, @piFilaId, @piUndNegocio,
                        @piSeguimiento, @piTurno, @viEstatusTurno, @viValorCero, @vdFecHoy, @pcUser)

         IF (@@ERROR <> 0)
         BEGIN 
            ROLLBACK TRAN ObtenITurnos
            SET @vcMensaje= 'Error al insertar en tabla TACJCCTRTURNO.1' 
            GOTO CtrlErrores
         END

         -- INSERTA HISTORICO-TURNO
         INSERT INTO TACJCCTRHISTORICO (FIFECHA, FITURNOID, FISTATUSTURNOID, FDACTUALIZACION, 
                                        FDFECHAINSERTA, FCUSERINSERTA)
                VALUES (@viFecHoyCorta, @piTurno, @viEstatusTurno, @vdFecHoy, @vdFecHoy, @pcUser)

         IF (@@ERROR <> 0)
         BEGIN 
	        ROLLBACK TRAN ObtenITurnos
            SET @vcMensaje= 'Error al insertar en tabla TACJCCTRHISTORICO.1' 
            GOTO CtrlErrores
         END
      END
      ELSE
      BEGIN 
         ROLLBACK TRAN ObtenITurnos
         SET @vcMensaje= 'Error al insertar en tabla TACJCCTRTURNO.2' 
         GOTO CtrlErrores
      END
   COMMIT TRAN ObtenITurnos
   SET NOCOUNT OFF
   RETURN 0  

--******** CtrlErrores ***********
CtrlErrores:
   SET NOCOUNT OFF
   SET @vcMensaje = 'Error al Ejecutar el SP -> PACJCCLITRTurno. ' + ISNULL (@vcMensaje,'')
 
   -- Termina con error 
   RAISERROR (@vcMensaje, 18, 1) 
   RETURN -1
GO

EXEC DBO.SPGRANT 'dbo.PACJCCLITRTurno'
GO
