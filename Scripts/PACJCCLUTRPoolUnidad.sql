IF OBJECT_ID('DBO.PACJCCLUTRPoolUnidad','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRPoolUnidad;
END
GO
----------------------------------------------------------------------------------------------------------      
---Responsable          : Esteban Jesús Caro Guzmán y Jorge Enrique Gamboa Fuentes.  
---Fecha                : Enero2014  
---Descripcion          : Turnos-Sucursales; Actualizacion del Pool de Unidades de Negocio
----------------------------------------------------------------------------------------------------------      
CREATE PROCEDURE dbo.PACJCCLUTRPoolUnidad (
   @pcEmpNo              CHAR(6),
   @piUnidadNeg          INTEGER,
   @pcUser               CHAR(6)
)

AS

SET NOCOUNT ON

   DECLARE
      @vcEmpno         CHAR(6),
      @vdFecHoy        DATETIME,
      @vcMensaje       VARCHAR(255),
	  @viEMPSTATUS     INT,
	  @vcSINVALOR      CHAR(6)

   SELECT @vdFecHoy = GETDATE(), @vcEmpno = LTRIM(RTRIM(@pcEmpNo))

   BEGIN TRAN CambiaUnidad  
      -- Valida si Existe el Empleado en TACJCCTRCUALIDADPOOL 
      IF EXISTS (SELECT FCEMPNOID FROM TACJCCTRCUALIDADPOOL WITH (NOLOCK)   
                  WHERE FCEMPNOID = @pcEmpNo)  
      BEGIN
         UPDATE TACJCCTRCUALIDADPOOL 
		    SET FCVALOR = @piUnidadNeg
               ,FCUSERMODIF = @pcUser
			   ,FDFECHAMODIF = @vdFecHoy
          WHERE FCEMPNOID = @pcEmpNo;

         IF (@@ERROR <> 0)
         BEGIN 
            SET @vcMensaje= 'Error al Actualizar en tabla TACJCCTRCUALIDADPOOL.' 
            GOTO CtrlErrores
         END
      END
      --colocas el ELSE si te pidieron que mandaras error cuando no localizas al usuario
	  --ELSE
	  --BEGIN
      --     SET @vcMensaje= 'Error: No se localizo el Empleado indicado.' 
      --     GOTO CtrlErrores
	  --END

   COMMIT TRAN CambiaUnidad 

   --CORRECTO
   SELECT 0
   
   SET NOCOUNT OFF
   RETURN 0  

--******** CtrlErrores ***********
CtrlErrores:
   SET NOCOUNT OFF
   ROLLBACK TRAN CambiaUnidad
   SET @vcMensaje = 'Error al Ejecutar el SP -> PACJCCLUTRPoolUnidad. ' + ISNULL (@vcMensaje,'')
 
   -- Termina con error 
   RAISERROR (@vcMensaje, 18, 1) 
   RETURN -1
GO

EXEC DBO.SPGRANT 'dbo.PACJCCLUTRPoolUnidad'
GO
