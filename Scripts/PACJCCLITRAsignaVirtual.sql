IF OBJECT_ID('DBO.PACJCCLITRAsignaVirtual','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLITRAsignaVirtual;
END
GO
---------------------------------------------------------------------------------------------------------      
---Responsable          : Roberto Gonzalez Figueroa
---Fecha                : Dic2013
---Descripcion          : AsignaTurnosVirtuales-Sucursales; Asignacion de Turnos Virtuales a Empleados
---------------------------------------------------------------------------------------------------------      
CREATE PROC dbo.PACJCCLITRAsignaVirtual (
   @pcEmpleado           CHAR(6),
   @piUnidadNegocio      SMALLINT,
   @piTurno              INTEGER OUTPUT,
   @piFecha              INTEGER OUTPUT,
   @piStatus             SMALLINT OUTPUT
)

AS
SET NOCOUNT ON

   DECLARE
      @viFECHOYCORTA     INTEGER,
      @vdFECHAHOY2       DATETIME,
      @vcUsuario         CHAR(6),
      @vcMensaje         VARCHAR(255),
      @viUnidadNegocio   SMALLINT,
      @viValorCero       SMALLINT,
      @viValorUno        SMALLINT,
      @viValorTres       SMALLINT,
      @vcSTATUSPOOL      CHAR(6),
      @viEstatusTurno    SMALLINT,
      @viRegistro        SMALLINT

   SELECT @viValorCero = 0, @viValorUno = 1, @viValorTres = 3, @vcUsuario = @pcEmpleado, 
          @viUnidadNegocio = @piUnidadNegocio, @vdFECHAHOY2 = GETDATE(), @viEstatusTurno = @viValorUno,
          @vcSTATUSPOOL = '1,2', @viRegistro = @viValorUno
   SELECT @viFECHOYCORTA = CONVERT(CHAR(8), @vdFECHAHOY2, 112)
   SET @piFecha = @viFECHOYCORTA
  
   --*********************************************************************
   --******************** Valida USUARIO no sea NULO *********************
   IF @vcUsuario IS NULL
   BEGIN
      SET @vcMensaje= 'Sin Usuario para Asignar Turno Virtual' 
      GOTO CtrlErrores
   END

   --******** Valida estatus de empleado que genara TURNO VIRTUAL *********
   IF EXISTS (SELECT FCEMPNOID 
                FROM TACJCCTRPOOLATENCION POOLA WITH (NOLOCK)
               WHERE FCEMPNOID = @vcUsuario 
                 AND CHARINDEX(CONVERT(VARCHAR(6),FISTATUSPOOLID),@vcSTATUSPOOL) >= 1)
   BEGIN
      --*************** INICIA BLOQUE TRANSACCIONAL **************
      BEGIN TRAN IAsignaVirtual
         --*************** OBTIENE EL TURNO SIGUIENTE ***************
         SELECT @piTurno = ISNULL(FITURNOID,0)+@viValorUno
           FROM TACJCCTRTURNO WITH (ROWLOCK, UPDLOCK)
          WHERE FIFECHA = @viFecHoyCorta

         IF (@piTurno IS NULL)
            SET @piTurno = @viValorUno

         --*************** Valida si existe el Turno ***************
         IF NOT EXISTS (SELECT FITURNOID FROM TACJCCTRTURNO WITH (NOLOCK) 
                         WHERE FIFECHA = @viFecHoyCorta AND FITURNOID = @piTurno)
         BEGIN 
            -- ***************** INSERTA TURNO VIRTUAL *******************
            INSERT INTO TACJCCTRTURNO (FIFECHA, FITURNOID, FIORIGENID, FIFILAID, FIUNIDADNEGOCIOID, 
                                       FITURNOSEGUIMIENTO, FIPRIORIDAD, FISTATUSTURNO, FIVIRTUAL, 
                                       FDFECHAINSERTA, FCUSERINSERTA)
                   VALUES (@viFecHoyCorta, @piTurno, @viValorCero, @viValorUno, @viUnidadNegocio,
                           @viValorCero, @piTurno, @viEstatusTurno, @viValorUno, @vdFECHAHOY2, @vcUsuario)

            IF (@@ERROR <> 0)
            BEGIN 
               ROLLBACK TRAN IAsignaVirtual
               SET @vcMensaje= 'Error al insertar Turno Virtual en tabla TACJCCTRTURNO.1' 
               GOTO CtrlErrores
            END

            -- ******* INSERTA HISTORICO PARA ESTATUS 1,2,3(Generado,Asignado,EnAtención *******
            WHILE @viRegistro <= @viValorTres
            BEGIN
                                   --@piFecha, @piTurno, @piEstatusTurno, @pcEmpleado
               EXEC PACJCCLUTRHistorico @viFecHoyCorta, @piTurno, @viRegistro, @vcUsuario
               IF (@@ERROR <> 0)
               BEGIN 
                  ROLLBACK TRAN IAsignaVirtual
                  SET @vcMensaje= 'Error al insertar Turno Virtual en tabla TACJCCTRTURNO.1' 
                  GOTO CtrlErrores
               END
               SET @viRegistro = @viRegistro + 1
            END

            -- ******* ACTUALIZA ESTATUS DEL POOLATENCION A 3->OCUPADO<- *******
            UPDATE TACJCCTRPOOLATENCION
               SET FISTATUSPOOLID = @viValorTres
                  ,FDFECHAMODIF = @vdFECHAHOY2
                  ,FCUSERMODIF = @vcUsuario
             WHERE FCEMPNOID = @vcUsuario

            IF (@@ERROR <> 0)
            BEGIN 
               ROLLBACK TRAN IAsignaVirtual
               SET @vcMensaje= 'Error al Actualizar en tabla TACJCCTRPOOLATENCION.1' 
               GOTO CtrlErrores
            END

            -- ******* ACTUALIZA EMPNOID DE TURNO *******
            UPDATE TACJCCTRTURNO
               SET FISTATUSTURNO = @viValorTres
                  ,FCEMPNOID = @vcUsuario
                  ,FDFECHAMODIF = @vdFECHAHOY2
                  ,FCUSERMODIF = @vcUsuario
             WHERE FIFECHA = @viFecHoyCorta
               AND FITURNOID = @piTurno

            SET @piStatus = @viValorTres

            IF (@@ERROR <> 0)
            BEGIN 
               ROLLBACK TRAN IAsignaVirtual
               SET @vcMensaje= 'Error al Actualizar en tabla TACJCCTRTURNO.1' 
               GOTO CtrlErrores
            END
         END
         ELSE
         BEGIN 
            ROLLBACK TRAN IAsignaVirtual
            SET @vcMensaje= 'Error al insertar TurnoVirtual en tabla TACJCCTRTURNO.2' 
            GOTO CtrlErrores
         END

      -- ******* TERMINA CORRECTAMENTE BLOQUE TRANSACCIONAL *******
      COMMIT TRAN IAsignaVirtual
   END
   ELSE
   BEGIN
      SET @vcMensaje= 'Error: Empleado no puede tomar este Turno' 
      GOTO CtrlErrores
   END 

   SET NOCOUNT OFF
   RETURN 0  

--******** CtrlErrores ***********
CtrlErrores:
   SET NOCOUNT OFF
   SET @vcMensaje = 'Error al Ejecutar el SP -> PACJCCLITRAsignaVirtual. ' + ISNULL (@vcMensaje,'')
 
   -- Termina con error 
   RAISERROR (@vcMensaje, 18, 1) 
   RETURN -1
GO

EXEC DBO.SPGRANT 'DBO.PACJCCLITRAsignaVirtual'
GO