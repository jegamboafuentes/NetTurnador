IF OBJECT_ID('DBO.PACJCCLSTRTurno','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLSTRTurno;
END
GO
---------------------------------------------------------------------------------------------------------      
---Responsable          : Roberto Gonzalez Figueroa
---Fecha                : Sep2013
---Descripcion          : Turnos-Sucursales; Obtiene el Turno indicado
---------------------------------------------------------------------------------------------------------      
CREATE PROC dbo.PACJCCLSTRTurno (
   @piFecha              INTEGER,
   @piTurno              INTEGER
)

AS

SET NOCOUNT ON

   DECLARE
      @vcMensaje     VARCHAR(255)

   -- Valida si existe el Turno
   BEGIN TRAN STurno
      SELECT FIFECHA, FITURNOID, FIUNIDADNEGOCIOID, FISTATUSTURNO
            ,FIPRIORIDAD, FIORIGENID, FITURNOSEGUIMIENTO, FCEMPNOID
        FROM TACJCCTRTURNO WITH (ROWLOCK, UPDLOCK)
       WHERE FIFECHA = @piFecha 
         AND FITURNOID = @piTurno
   COMMIT TRAN STurno
   
   SET NOCOUNT OFF
   RETURN 0  

GO

EXEC DBO.SPGRANT 'DBO.PACJCCLSTRTurno'
GO