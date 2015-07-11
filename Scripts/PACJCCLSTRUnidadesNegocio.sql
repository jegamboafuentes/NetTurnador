IF OBJECT_ID('DBO.PACJCCLSTRUnidadesNegocio','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLSTRUnidadesNegocio;
END
GO
---------------------------------------------------------------------------------------------------------      
---Responsable          : Roberto Gonzalez Figueroa
---Fecha                : Sep2013
---Descripcion          : Turnos-Sucursales; Consulta Unidades de Negocio Activas del Catalogo
---------------------------------------------------------------------------------------------------------      
CREATE PROC dbo.PACJCCLSTRUnidadesNegocio 
AS

SET NOCOUNT ON

   DECLARE
      @vdActual        SMALLDATETIME,
      @vcMensaje       VARCHAR(255),
	  @viEstatusUndNeg SMALLINT

   SELECT @vdActual = GETDATE(), @viEstatusUndNeg = 1
   
   -- Consulta
   BEGIN TRAN SUnidadesNegocio
   SELECT FIUNIDADNEGOCIOID, FCDESCRIPCION, ISNULL(RTRIM(LTRIM(FCRUTAIMAGEN)),'') AS FCRUTAIMAGEN, 
          CONVERT(INT,FLPRESTAMOS) AS FLPRESTAMOS, FCCOLOR, FCZONA, FCPREFIJO
     FROM TCCJCCTRUNIDADNEGOCIO WITH (ROWLOCK, UPDLOCK) 
    WHERE FISTATUSUNDNEG = @viEstatusUndNeg
    ORDER BY FIUNIDADNEGOCIOID
   COMMIT TRAN SUnidadesNegocio
   SET NOCOUNT OFF
   RETURN 0  

GO
EXEC DBO.SPGRANT 'DBO.PACJCCLSTRUnidadesNegocio'
GO
