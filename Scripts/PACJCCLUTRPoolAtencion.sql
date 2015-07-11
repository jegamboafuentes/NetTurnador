IF OBJECT_ID('DBO.PACJCCLUTRPoolAtencion','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRPoolAtencion;
END
GO
---------------------------------------------------------------------------------------------------------      
---Responsable          : Roberto Gonzalez Figueroa
---Fecha                : Sep2013
---Descripcion          : Turnos-Sucursales; Actualizacion del Pool de Atencion
---------------------------------------------------------------------------------------------------------      
CREATE PROCEDURE dbo.PACJCCLUTRPoolAtencion (
   @pcEmpNo              CHAR(6),
   @pcUser               CHAR(6),
   @piEstatus            SMALLINT,
   @piCualidad           SMALLINT,
   @pcValor              CHAR(250)
)

AS

SET NOCOUNT ON

   DECLARE
      @vcEmpno         CHAR(6),
      @vdFecHoy        DATETIME,
      @vcMensaje       VARCHAR(255),
	  @viEMPSTATUS     INT,
	  @vcSINVALOR      CHAR(6)

   DECLARE @vtEmpleados TABLE (FCEMPNO CHAR(6), FIDEPTOID SMALLINT)

   SELECT @vdFecHoy = GETDATE(), @vcEmpno = LTRIM(RTRIM(@pcEmpNo)), @viEMPSTATUS = 1, @vcSINVALOR = ''

   BEGIN TRAN IPoolAtencion
      -- Valida si Existe el Empleado en TACJCCTRPOOLATENCION 
      IF NOT EXISTS (SELECT FCEMPNOID FROM TACJCCTRPOOLATENCION WITH (NOLOCK) 
                      WHERE FCEMPNOID = @vcEmpno)
      BEGIN 
         INSERT INTO @vtEmpleados
            SELECT FCEMPNO, FIDEPID FROM EMPLEADO WITH (NOLOCK) 
             WHERE FCEMPNO NOT IN (@vcSINVALOR)
               AND FIEMPSTATUS = @viEMPSTATUS
         IF (@@ERROR <> 0)
         BEGIN 
            ROLLBACK TRAN IPoolAtencion
            SET @vcMensaje= 'Error al insertar en tabla TACJCCTRPOOLATENCION.' 
            GOTO CtrlErrores
         END

	  -- Valida si Existe el Empleado en tabla Empleado
         IF EXISTS (SELECT FCEMPNO FROM @vtEmpleados
                     WHERE LTRIM(RTRIM(FCEMPNO)) = @vcEmpno)
         BEGIN
            -- Inserta en tabla TACJCCTRPOOLATENCION 
            INSERT INTO TACJCCTRPOOLATENCION (FCEMPNOID, FCRUTAIMAGEN, FISTATUSPOOLID, FDFECHAINSERTA, FCUSERINSERTA)
                 VALUES (@vcEmpno, @vcSINVALOR, @piEstatus, @vdFecHoy, @pcUser)
            IF (@@ERROR <> 0)
            BEGIN 
               ROLLBACK TRAN IPoolAtencion
               SET @vcMensaje= 'Error al insertar en tabla TACJCCTRPOOLATENCION.' 
               GOTO CtrlErrores
            END

            -- Inserta en tabla TACJCCTRCUALIDADPOOL
            INSERT INTO TACJCCTRCUALIDADPOOL (FCEMPNOID, FICUALIDADID, FCVALOR, FDFECHAINSERTA, FCUSERINSERTA)
                 VALUES (@vcEmpno, @piCualidad, @pcValor, @vdFecHoy, @pcUser)
 
            IF (@@ERROR <> 0)
            BEGIN 
               ROLLBACK TRAN IPoolAtencion 
               SET @vcMensaje= 'Error al insertar en tabla TACJCCTRCUALIDADPOOL.' 
               GOTO CtrlErrores
            END
         END
         ELSE
         BEGIN
            ROLLBACK TRAN IPoolAtencion 
            SET @vcMensaje= 'Error al insertar en tabla TACJCCTRPOOLATENCION, Empleado no Existe.' 
            GOTO CtrlErrores
         END
      END
      ELSE
      BEGIN 
         -- Actualiza Estatus del Pool de Atencion
         UPDATE TACJCCTRPOOLATENCION 
            SET FISTATUSPOOLID = @piEstatus
               ,FDFECHAMODIF = @vdFecHoy
               ,FCUSERMODIF = @pcUser
          WHERE FCEMPNOID = @vcEmpno

         IF (@@ERROR <> 0)
         BEGIN 
		    ROLLBACK TRAN IPoolAtencion 
            SET @vcMensaje= 'Error al Actualizar tabla TACJCCTRPOOLATENCION.' 
            GOTO CtrlErrores
         END
      END
   COMMIT TRAN IPoolAtencion 

   --CORRECTO
   SELECT 0
   
   SET NOCOUNT OFF
   RETURN 0  

--******** CtrlErrores ***********
CtrlErrores:
   SET NOCOUNT OFF
   SET @vcMensaje = 'Error al Ejecutar el SP -> PACJCCLUTRPoolAtencion. ' + ISNULL (@vcMensaje,'')
 
   -- Termina con error 
   RAISERROR (@vcMensaje, 18, 1) 
   RETURN -1
GO

EXEC DBO.SPGRANT 'dbo.PACJCCLUTRPoolAtencion'
GO
