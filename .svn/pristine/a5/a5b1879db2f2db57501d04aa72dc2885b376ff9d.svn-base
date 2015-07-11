IF OBJECT_ID('DBO.PACJCCLUTRApropiar','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRApropiar;
END
GO
---------------------------------------------------------------------------------------------------------      
---Responsable          : Roberto Gonzalez Figueroa
---Fecha                : DIC2013
---Descripcion          : Apropiar Turnos; Apropiar Turnos de otro Empleado
---------------------------------------------------------------------------------------------------------      
CREATE PROC dbo.PACJCCLUTRApropiar (
   @piFECHA    INTEGER,
   @piTURNO    INTEGER,
   @pcEmpleado CHAR(6)
)

AS
SET NOCOUNT ON

   DECLARE
      @vcMensaje         VARCHAR(255),
      @viTurnoSol        INTEGER,
      @viFECHOYCORTA     INTEGER,
      @vcEmpleadoAsig    CHAR(6),
      @vcUsuario         CHAR(6),
      @vcEmpleadoSol     CHAR(6),
      @vdFECHOYLARGA     DATETIME,
      @viValorCero       SMALLINT,
      @viValorUno        SMALLINT,
      @viValorDos        SMALLINT,
      @viValorTres       SMALLINT,
      @viValorCuatro     SMALLINT,
      @vcEstatusTurno    VARCHAR(10),
      @vcEstatusPool     VARCHAR(10)

   SELECT @viValorCero = 0, @viValorUno = 1, @viValorDos = 2, @viValorTres = 3, @viValorCuatro = 4, 
          @vcUSUARIO = SUSER_NAME(), @vdFECHOYLARGA = GETDATE()
   SELECT @vcEstatusTurno = '2,7', @vcEstatusPool = '1,2',       --2,7 - Asignado, Pospuesto --1,2 -
          @viFECHOYCORTA = CONVERT(CHAR(8), @vdFECHOYLARGA, 112)

BEGIN TRAN UApropiar
   ------ valida que el turno este en estatus 2 -------
   IF EXISTS (SELECT FITURNOID FROM TACJCCTRTURNO WITH (ROWLOCK, UPDLOCK)
               WHERE FIFECHA = @piFECHA AND FITURNOID = @piTURNO 
                 AND CHARINDEX(CONVERT(VARCHAR(6),FISTATUSTURNO),@vcEstatusTurno) >= @viValorUno)
   BEGIN
      ----- valida que el empleado tenga estatus 1 ó 2 -----
      IF EXISTS (SELECT FCEMPNOID
                   FROM TACJCCTRPOOLATENCION WITH (ROWLOCK, UPDLOCK)
                  WHERE FCEMPNOID = @pcEmpleado 
                    AND CHARINDEX(CONVERT(VARCHAR(6),FISTATUSPOOLID),@vcEstatusPool) >= @viValorUno)
      BEGIN
         --valida que el turno este en estatus 2 --
         SELECT @viTurnoSol = FITURNOID, @vcEmpleadoAsig = FCEMPNOID
           FROM TACJCCTRTURNO WITH (NOLOCK)
          WHERE FIFECHA = @piFECHA 
            AND FITURNOID = @piTURNO 
            AND CHARINDEX(CONVERT(VARCHAR(6),FISTATUSTURNO),@vcEstatusTurno) >= @viValorUno

         --valida que el empleado tenga estatus 1 ó 2 --
         SELECT @vcEmpleadoSol = FCEMPNOID
           FROM TACJCCTRPOOLATENCION  WITH (NOLOCK)
          WHERE FCEMPNOID = @pcEmpleado 
            AND CHARINDEX(CONVERT(VARCHAR(6),FISTATUSPOOLID),@vcEstatusPool) >= 1

         --****** CAMBIA ESTATUS DEL EMPLEADO SOLICITANTE A OCUPADO (4) *************
         UPDATE TACJCCTRPOOLATENCION
            SET FISTATUSPOOLID = @viValorCuatro
               ,FDFECHAMODIF = @vdFECHOYLARGA
               ,FCUSERMODIF = @vcUsuario
          WHERE FCEMPNOID = @vcEmpleadoSol

         IF (@@ERROR <> 0)
         BEGIN 
            SET @vcMensaje= 'Error al Actualizar en tabla TACJCCTRPOOLATENCION.1' 
            GOTO CtrlErrores
         END

          --****** CAMBIA ESTATUS DEL EMPLEADO ASIGNADO A NODISPONIBLE (1) *************
         UPDATE TACJCCTRPOOLATENCION
            SET FISTATUSPOOLID = @viValorUno
               ,FDFECHAMODIF = @vdFECHOYLARGA
               ,FCUSERMODIF = @vcUsuario
          WHERE FCEMPNOID = @vcEmpleadoAsig

         IF (@@ERROR <> 0)
         BEGIN 
            SET @vcMensaje= 'Error al Actualizar en tabla TACJCCTRPOOLATENCION.2' 
            GOTO CtrlErrores
         END

         --****** EL TURNO PASA A "EN ATENCION" Y ACTUALIZA AL EMPLEADO QUE LO VA A ATENDER *******
         -- INSERTA HISTORICO-TURNO
         IF EXISTS (SELECT FISTATUSTURNOID FROM TACJCCTRHISTORICO 
                     WHERE FIFECHA = @viFecHoyCorta
                       AND FITURNOID = @viTurnoSol
                       AND FISTATUSTURNOID = @viValorTres)
         BEGIN
            UPDATE TACJCCTRHISTORICO 
               SET FDFECHAMODIF = @vdFECHOYLARGA
                  ,FCUSERMODIF = @vcUSUARIO
             WHERE FIFECHA = @viFecHoyCorta
               AND FITURNOID = @viTurnoSol
               AND FISTATUSTURNOID = @viValorTres

            IF (@@ERROR <> 0)
            BEGIN 
               SET @vcMensaje= 'Error al insertar Turno de Apropiacion' 
               GOTO CtrlErrores
            END
         END
         ELSE
         BEGIN
            INSERT INTO TACJCCTRHISTORICO (FIFECHA, FITURNOID, FISTATUSTURNOID, FDACTUALIZACION, 
                                           FDFECHAINSERTA, FCUSERINSERTA)
                   VALUES (@viFecHoyCorta, @viTurnoSol, @viValorTres, @vdFECHOYLARGA, @vdFECHOYLARGA, @vcUSUARIO)
            IF (@@ERROR <> 0)
            BEGIN 
               SET @vcMensaje= 'Error al insertar Turno de Apropiacion' 
               GOTO CtrlErrores
            END
         END

         --****** CAMBIA ESTATUS DEL TURNO A "EN ATENCION" (3) *************
         UPDATE TACJCCTRTURNO
            SET FCEMPNOID = @vcEmpleadoSol
               ,FISTATUSTURNO = @viValorTres
               ,FDFECHAMODIF = @vdFECHOYLARGA
               ,FCUSERMODIF = @vcUsuario
          WHERE FIFECHA = @viFecHoyCorta
            AND FITURNOID = @viTurnoSol

         IF (@@ERROR <> 0)
         BEGIN 
            SET @vcMensaje= 'Error al Actualizar en tabla TACJCCTRPOOLATENCION.2' 
            GOTO CtrlErrores
         END
      END
      ELSE
      BEGIN
         SET @vcMensaje= 'Error: Empleado NoDisponible/Disponible para Apropiarse del Turno' 
         GOTO CtrlErrores
      END
   END
   ELSE
   BEGIN
      SET @vcMensaje= 'Error: El Turno no esta Asignado y no se puede Apropiar' 
      GOTO CtrlErrores
   END

   COMMIT TRAN UApropiar

   SET NOCOUNT OFF
   RETURN 0  

--******** CtrlErrores ***********
CtrlErrores:
   SET NOCOUNT OFF
   ROLLBACK TRAN UApropiar
   SET @vcMensaje = 'Error al Ejecutar el SP -> PACJCCLUTRApropiar. ' + ISNULL (@vcMensaje,'')
 
   -- Termina con error 
   RAISERROR (@vcMensaje, 18, 1) 
   RETURN -1
GO

EXEC DBO.SPGRANT 'DBO.PACJCCLUTRApropiar'
GO
