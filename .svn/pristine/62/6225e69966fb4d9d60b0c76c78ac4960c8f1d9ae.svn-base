IF OBJECT_ID('DBO.PACJCCLSTRPoolAtencion','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLSTRPoolAtencion;
END
GO
---------------------------------------------------------------------------------------------------------      
---Responsable          : Roberto Gonzalez Figueroa
---Fecha                : Sep2013
---Descripcion          : Turnos-Sucursales; Consulta Pool de Atencion -- solo los disponibles
---------------------------------------------------------------------------------------------------------      
CREATE PROC dbo.PACJCCLSTRPoolAtencion (
   @piOpcion          SMALLINT,
   @pcEmpNo           CHAR(6),
   @piUnidadNeg       SMALLINT)
AS

SET NOCOUNT ON

   DECLARE
      @vcEmpno         CHAR(6),
      @vcMensaje       VARCHAR(255),
	  @viCualidaId     SMALLINT,
	  @viValorCero     SMALLINT

   SELECT @vcEmpno = LTRIM(RTRIM(@pcEmpNo)), @viCualidaId = 1, @viValorCero = 0

   BEGIN TRAN SPoolAtencion
      IF (@piOpcion = 1)   --Por Empleado 
      BEGIN
         SELECT POOLA.FCEMPNOID, ISNULL(RTRIM(LTRIM(POOLA.FCRUTAIMAGEN)),''), POOLA.FISTATUSPOOLID,
                CUALIDAD.FICUALIDADID, ISNULL(CUALIDAD.FCVALOR,'')
           FROM TACJCCTRPOOLATENCION POOLA WITH (ROWLOCK, UPDLOCK) INNER JOIN TACJCCTRCUALIDADPOOL CUALIDAD WITH (ROWLOCK, UPDLOCK)
             ON POOLA.FCEMPNOID = CUALIDAD.FCEMPNOID
          WHERE POOLA.FCEMPNOID = @vcEmpno
      END
      ELSE
      BEGIN
         IF (@piOpcion = 2)   --Por Unidad de Negocio
         BEGIN
            SELECT POOLA.FCEMPNOID, ISNULL(RTRIM(LTRIM(POOLA.FCRUTAIMAGEN)),''), POOLA.FISTATUSPOOLID,
                   CUALIDAD.FICUALIDADID, ISNULL(CUALIDAD.FCVALOR,'')
              FROM TACJCCTRPOOLATENCION POOLA WITH (ROWLOCK, UPDLOCK) INNER JOIN TACJCCTRCUALIDADPOOL CUALIDAD WITH (ROWLOCK, UPDLOCK)
                ON POOLA.FCEMPNOID = CUALIDAD.FCEMPNOID
             WHERE CUALIDAD.FICUALIDADID = @viCualidaId
         END
         ELSE
         BEGIN
            IF (@piOpcion = 3)   --Todos
            BEGIN
               SELECT POOLA.FCEMPNOID, ISNULL(RTRIM(LTRIM(POOLA.FCRUTAIMAGEN)),''), POOLA.FISTATUSPOOLID,
                      CUALIDAD.FICUALIDADID, ISNULL(CUALIDAD.FCVALOR,'')
                 FROM TACJCCTRPOOLATENCION POOLA WITH (ROWLOCK, UPDLOCK) INNER JOIN TACJCCTRCUALIDADPOOL CUALIDAD WITH (ROWLOCK, UPDLOCK)
                   ON POOLA.FCEMPNOID = CUALIDAD.FCEMPNOID
                WHERE POOLA.FCEMPNOID > @viValorCero
            END
         END
      END
   COMMIT TRAN SPoolAtencion
   SET NOCOUNT OFF
   RETURN 0  
GO

EXEC DBO.SPGRANT 'DBO.PACJCCLSTRPoolAtencion'
GO