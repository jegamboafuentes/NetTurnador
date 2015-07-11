IF OBJECT_ID('DBO.PACJCCLSTRTurnos','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLSTRTurnos;
END
GO
---------------------------------------------------------------------------------------------------------      
---Responsable          : Roberto Gonzalez Figueroa
---Fecha                : Sep2013
---Descripcion          : Turnos-Sucursales; Obtiene Los Turnos por UnidadNegocio y/o Estatus del Turno
---------------------------------------------------------------------------------------------------------      
CREATE PROC dbo.PACJCCLSTRTurnos (
   @piFECHA               INTEGER,
   @piUndNeg              SMALLINT,
   @piEstatusTurno        SMALLINT
)

AS

SET NOCOUNT ON

   DECLARE
      @vcMensaje     VARCHAR(255),
      @viValorCero   SMALLINT

   SET @viValorCero = 0
   
   BEGIN TRAN STurnos
      IF (@piUndNeg IS NOT NULL) AND (@piEstatusTurno IS NOT NULL)
      BEGIN
         SELECT TURNOS.FIFECHA, TURNOS.FITURNOID, TURNOS.FIUNIDADNEGOCIOID, TURNOS.FISTATUSTURNO
               ,TURNOS.FIPRIORIDAD, TURNOS.FIORIGENID, TURNOS.FITURNOSEGUIMIENTO, TURNOS.FCEMPNOID
               ,EMPS.FCEMPNOM, EMPS.FCEMPAPP, EMPS.FCEMPAPM
           FROM TACJCCTRTURNO TURNOS WITH (ROWLOCK, UPDLOCK) LEFT JOIN EMPLEADO EMPS WITH (NOLOCK)
             ON TURNOS.FCEMPNOID = EMPS.FCEMPNO
          WHERE TURNOS.FIFECHA = @piFECHA
            AND TURNOS.FITURNOID > @viValorCero
            AND TURNOS.FIUNIDADNEGOCIOID = @piUndNeg
            AND TURNOS.FISTATUSTURNO = @piEstatusTurno
      END
      ELSE
      BEGIN
         IF (@piUndNeg IS NOT NULL)
	     BEGIN
            SELECT TURNOS.FIFECHA, TURNOS.FITURNOID, TURNOS.FIUNIDADNEGOCIOID, TURNOS.FISTATUSTURNO
                  ,TURNOS.FIPRIORIDAD, TURNOS.FIORIGENID, TURNOS.FITURNOSEGUIMIENTO, TURNOS.FCEMPNOID
                  ,EMPS.FCEMPNOM, EMPS.FCEMPAPP, EMPS.FCEMPAPM
              FROM TACJCCTRTURNO TURNOS WITH (ROWLOCK, UPDLOCK) LEFT JOIN EMPLEADO EMPS WITH (NOLOCK)
                ON TURNOS.FCEMPNOID = EMPS.FCEMPNO
             WHERE TURNOS.FIFECHA = @piFECHA
               AND TURNOS.FITURNOID > @viValorCero
               AND TURNOS.FIUNIDADNEGOCIOID = @piUndNeg
         END
         ELSE
         BEGIN
            SELECT TURNOS.FIFECHA, TURNOS.FITURNOID, TURNOS.FIUNIDADNEGOCIOID, TURNOS.FISTATUSTURNO
                  ,TURNOS.FIPRIORIDAD, TURNOS.FIORIGENID, TURNOS.FITURNOSEGUIMIENTO, TURNOS.FCEMPNOID
                  ,EMPS.FCEMPNOM, EMPS.FCEMPAPP, EMPS.FCEMPAPM
              FROM TACJCCTRTURNO TURNOS WITH (ROWLOCK, UPDLOCK) LEFT JOIN EMPLEADO EMPS WITH (NOLOCK)
                ON TURNOS.FCEMPNOID = EMPS.FCEMPNO
             WHERE TURNOS.FIFECHA = @piFECHA
               AND TURNOS.FITURNOID > @viValorCero
               AND TURNOS.FISTATUSTURNO = @piEstatusTurno
         END
      END

   COMMIT TRAN STurnos
   SET NOCOUNT OFF
   RETURN 0  

--******** CtrlErrores ***********
CtrlErrores:
   SET NOCOUNT OFF
   SET @vcMensaje = 'Error al Ejecutar el SP -> PACJCCLSTRTurnos. ' + ISNULL (@vcMensaje,'')
 
   -- Termina con error 
   RAISERROR (@vcMensaje, 18, 1) 
   RETURN -1
GO

EXEC DBO.SPGRANT 'DBO.PACJCCLSTRTurnos'
GO
