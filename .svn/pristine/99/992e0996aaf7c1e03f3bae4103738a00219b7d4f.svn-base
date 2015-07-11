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
   @piUnidadNegocio      SMALLINT,
   @piEstatusTurno        SMALLINT,
   @pcUSer                CHAR(10)
)

AS

SET NOCOUNT ON

   DECLARE
      @vdFechaHoy    DATETIME,
      @vcMensaje     VARCHAR(255)

   SET @vdFechaHoy = GETDATE()
   
   IF NOT EXISTS (SELECT FIFECHA
                    FROM TACJCCTRHISTORICO WITH (NOLOCK)
                   WHERE FIFECHA = @piFecha AND FITURNOID = @piTurno AND FISTATUSTURNOID = @piEstatusTurno AND FIUNIDADNEGOCIOID = @piUnidadNegocio)
   BEGIN
      BEGIN TRAN UTurnos
         IF EXISTS (SELECT FIFECHA FROM TACJCCTRHISTORICO WITH(NOLOCK) WHERE FIFECHA = @piFecha
                       AND FITURNOID = @piTurno AND FISTATUSTURNOID = @piEstatusTurno AND FIUNIDADNEGOCIOID = @piUnidadNegocio)
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
            INSERT INTO TACJCCTRHISTORICO (FIFECHA, FITURNOID, FIUNIDADNEGOCIOID, FISTATUSTURNOID, FDACTUALIZACION, 
                                           FDFECHAINSERTA, FCUSERINSERTA)
                   VALUES (@piFecha, @piTurno, @piUnidadNegocio, @piEstatusTurno, @vdFechaHoy, @vdFechaHoy, @pcUser)

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
            AND FIUNIDADNEGOCIOID = @piUnidadNegocio

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

---~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~

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
   @pcEmpleado           CHAR(10),
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
      @vcUsuario         CHAR(10),
      @vcMensaje         VARCHAR(255),
      @viUnidadNegocio   SMALLINT,
      @viValorCero       SMALLINT,
      @viValorUno        SMALLINT,
      @viValorTres       SMALLINT,
      @viValorCuatro     SMALLINT,
      @vcSTATUSPOOL      CHAR(6),
      @viEstatusTurno    SMALLINT,
      @viRegistro        SMALLINT

   SELECT @viValorCero = 0, @viValorUno = 1, @viValorTres = 3, @viValorCuatro = 4, @vcUsuario = @pcEmpleado, 
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
          WHERE FIFECHA = @viFecHoyCorta AND FIUNIDADNEGOCIOID = @viUnidadNegocio

         IF (@piTurno IS NULL)
            SET @piTurno = @viValorUno

         --*************** Valida si existe el Turno ***************
         IF NOT EXISTS (SELECT FITURNOID FROM TACJCCTRTURNO WITH (NOLOCK) 
                         WHERE FIFECHA = @viFecHoyCorta AND FITURNOID = @piTurno AND FIUNIDADNEGOCIOID = @viUnidadNegocio)
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

            -- ******* INSERTA HISTORICO PARA ESTATUS 1,2,3(Generado,Asignado,EnAtencion *******
            WHILE @viRegistro <= @viValorTres
            BEGIN
                                   --@piFecha, @piTurno, @piEstatusTurno, @pcEmpleado
               EXEC PACJCCLUTRHistorico @viFecHoyCorta, @piTurno, @viUnidadNegocio, @viRegistro, @vcUsuario 
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
               SET FISTATUSPOOLID = @viValorCuatro
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
               AND FIUNIDADNEGOCIOID = @viUnidadNegocio

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

---~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~

IF OBJECT_ID('DBO.PACJCCLITRTurno','P') IS NOT NULL
BEGIN
	DROP PROC dbo.PACJCCLITRTurno;
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
   @pcUser               CHAR(10),  
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
       WHERE FIFECHA = @viFecHoyCorta AND FIUNIDADNEGOCIOID= @piUndNegocio 
       ORDER BY FITURNOID ASC
  
      IF (@viTurno IS NULL)  
         SET @viTurno = @viValorUno  
        
      SELECT @piTurno = @viTurno, @piFecha = @viFecHoyCorta  
  
      -- Valida si existe el Turno  
      IF NOT EXISTS (SELECT FITURNOID FROM TACJCCTRTURNO WITH (NOLOCK)   
                      WHERE FIFECHA = @viFecHoyCorta AND FITURNOID = @viTURNO AND FIUNIDADNEGOCIOID= @piUndNegocio)  
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
         INSERT INTO TACJCCTRHISTORICO (FIFECHA, FITURNOID, FIUNIDADNEGOCIOID, FISTATUSTURNOID, FDACTUALIZACION,   
                                        FDFECHAINSERTA, FCUSERINSERTA)  
                VALUES (@viFecHoyCorta, @piTurno, @piUndNegocio, @viEstatusTurno, @vdFecHoy, @vdFecHoy, @pcUser)  
  
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

EXEC DBO.SPGRANT 'DBO.PACJCCLITRTurno'
GO   

---~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~

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
   @pcEmpNo           CHAR(10),
   @piUnidadNeg       SMALLINT)
AS

SET NOCOUNT ON

   DECLARE
      @vcEmpno         CHAR(10),
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

---~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~

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
   @piTurno              INTEGER,
   @piUndNegocio         SMALLINT
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
         AND FIUNIDADNEGOCIOID = @piUndNegocio
   COMMIT TRAN STurno
   
   SET NOCOUNT OFF
   RETURN 0  

GO

EXEC DBO.SPGRANT 'DBO.PACJCCLSTRTurno'
GO

---~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-

IF OBJECT_ID('dbo.PACJCCLSTRTurnos','P') IS NOT NULL
BEGIN
	DROP PROC dbo.PACJCCLSTRTurnos;
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
   @piEstatusTurno        SMALLINT)
   
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
               ,EMPS.FCEMPNOM, EMPS.FCEMPAPP, EMPS.FCEMPAPM, P.FCPUNTOATENCION, TURNOS.FIVIRTUAL
           FROM TACJCCTRTURNO TURNOS WITH (ROWLOCK, UPDLOCK) LEFT JOIN EMPLEADO EMPS WITH (NOLOCK)
             ON TURNOS.FCEMPNOID = EMPS.FCEMPNO
             LEFT JOIN TACJCCTRPOOLATENCION P
                   ON TURNOS.FCEMPNOID = P.FCEMPNOID
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
                  ,EMPS.FCEMPNOM, EMPS.FCEMPAPP, EMPS.FCEMPAPM, P.FCPUNTOATENCION, TURNOS.FIVIRTUAL
              FROM TACJCCTRTURNO TURNOS WITH (ROWLOCK, UPDLOCK) LEFT JOIN EMPLEADO EMPS WITH (NOLOCK)
                ON TURNOS.FCEMPNOID = EMPS.FCEMPNO
                LEFT JOIN TACJCCTRPOOLATENCION P
                   ON TURNOS.FCEMPNOID = P.FCEMPNOID
              WHERE TURNOS.FIFECHA = @piFECHA
               AND TURNOS.FITURNOID > @viValorCero
               AND TURNOS.FIUNIDADNEGOCIOID = @piUndNeg
         END
         ELSE
         BEGIN
            SELECT TURNOS.FIFECHA, TURNOS.FITURNOID, TURNOS.FIUNIDADNEGOCIOID, TURNOS.FISTATUSTURNO
                  ,TURNOS.FIPRIORIDAD, TURNOS.FIORIGENID, TURNOS.FITURNOSEGUIMIENTO, TURNOS.FCEMPNOID
                  ,EMPS.FCEMPNOM, EMPS.FCEMPAPP, EMPS.FCEMPAPM, P.FCPUNTOATENCION, TURNOS.FIVIRTUAL
              FROM TACJCCTRTURNO TURNOS WITH (ROWLOCK, UPDLOCK) LEFT JOIN EMPLEADO EMPS WITH (NOLOCK)
                   ON TURNOS.FCEMPNOID = EMPS.FCEMPNO
                   LEFT JOIN TACJCCTRPOOLATENCION P
                   ON TURNOS.FCEMPNOID = P.FCEMPNOID
             WHERE TURNOS.FIFECHA = @piFECHA
               AND TURNOS.FITURNOID > @viValorCero
               AND TURNOS.FISTATUSTURNO = @piEstatusTurno
         END
      END

   COMMIT TRAN STurnos
   SET NOCOUNT OFF
   RETURN 0  
GO

EXEC DBO.SPGRANT 'DBO.PACJCCLSTRTurnos'
GO

---~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~

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
          CONVERT(INT,FLPRESTAMOS) AS FLPRESTAMOS, FCCOLOR, FCZONA, FCPREFIJO, FISTATUSUNDNEG
     FROM TCCJCCTRUNIDADNEGOCIO WITH (ROWLOCK, UPDLOCK) 
    WHERE FISTATUSUNDNEG = @viEstatusUndNeg
    ORDER BY FIUNIDADNEGOCIOID
   COMMIT TRAN SUnidadesNegocio
   SET NOCOUNT OFF
   RETURN 0  

GO
EXEC DBO.SPGRANT 'DBO.PACJCCLSTRUnidadesNegocio'
GO

---~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~

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
   @piFECHA               INTEGER,
   @piTURNO               INTEGER,
   @piUnidadNegocio       SMALLINT,
   @pcEmpleado            CHAR(10)
)

AS
SET NOCOUNT ON

   DECLARE
      @vcMensaje         VARCHAR(255),
      @viTurnoSol        INTEGER,
      @viFECHOYCORTA     INTEGER,
      @vcEmpleadoAsig    CHAR(10),
      @vcUsuario         CHAR(10),
      @vcEmpleadoSol     CHAR(10),
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
               WHERE FIFECHA = @piFECHA AND FITURNOID = @piTURNO AND FIUNIDADNEGOCIOID = @piUnidadNegocio
                 AND CHARINDEX(CONVERT(VARCHAR(6),FISTATUSTURNO),@vcEstatusTurno) >= @viValorUno)
   BEGIN
      ----- valida que el empleado tenga estatus 1 o 2 -----
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
            AND FIUNIDADNEGOCIOID = @piUnidadNegocio
            AND CHARINDEX(CONVERT(VARCHAR(6),FISTATUSTURNO),@vcEstatusTurno) >= @viValorUno

         --valida que el empleado tenga estatus 1 o 2 --
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
         IF EXISTS (SELECT FISTATUSTURNOID FROM TACJCCTRHISTORICO WITH(NOLOCK)
                     WHERE FIFECHA = @viFecHoyCorta
                       AND FITURNOID = @viTurnoSol
                       AND FIUNIDADNEGOCIOID = @piUnidadNegocio
                       AND FISTATUSTURNOID = @viValorTres)
         BEGIN
            UPDATE TACJCCTRHISTORICO 
               SET FDFECHAMODIF = @vdFECHOYLARGA
                  ,FCUSERMODIF = @vcUSUARIO
             WHERE FIFECHA = @viFecHoyCorta
               AND FITURNOID = @viTurnoSol
               AND FIUNIDADNEGOCIOID = @piUnidadNegocio
               AND FISTATUSTURNOID = @viValorTres

            IF (@@ERROR <> 0)
            BEGIN 
               SET @vcMensaje= 'Error al insertar Turno de Apropiacion' 
               GOTO CtrlErrores
            END
         END
         ELSE
         BEGIN
            INSERT INTO TACJCCTRHISTORICO (FIFECHA, FITURNOID, FIUNIDADNEGOCIOID, FISTATUSTURNOID, FDACTUALIZACION, 
                                           FDFECHAINSERTA, FCUSERINSERTA)
                   VALUES (@viFecHoyCorta, @viTurnoSol, @piUnidadNegocio, @viValorTres, @vdFECHOYLARGA, @vdFECHOYLARGA, @vcUSUARIO)
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
            AND FIUNIDADNEGOCIOID = @piUnidadNegocio

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

---~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~

IF OBJECT_ID('DBO.PACJCCLUTRAsigna','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRAsigna;
END
GO
---------------------------------------------------------------------------------------------------------      
---Responsable          : Roberto Gonzalez Figueroa
---Fecha                : Sep2013
---Descripcion          : Turnos-Sucursales; Asignacion de Turnos a Empleados
---------------------------------------------------------------------------------------------------------      
CREATE PROC dbo.PACJCCLUTRAsigna AS
SET NOCOUNT ON

   DECLARE
      @viFECHOYCORTA     INTEGER,
      @vdFECHAHOY2       DATETIME,
      @vcUSUARIO         CHAR(10),
      @viTURNOID         INTEGER,
      @vcEMPNOID         CHAR(10),
      @viIdRenglon       INTEGER, 
      @viRows            INTEGER,
      @vcMensaje         VARCHAR(255),
      @viFIFECHA         INTEGER,
      @viFIUNIDADNEGOCIO SMALLINT,
      @viValorCero       INT,
      @viValorUno        INT,
      @viValorDos        INT,
      @viValorTres       INT

   DECLARE @vtTURNOS TABLE (fiIdRenglon INT NOT NULL IDENTITY(1,1),
                            FIFECHA INTEGER, FITURNOID SMALLINT, FIUNIDADNEGOCIO SMALLINT,
                            FIPRIORIDAD SMALLINT
                            PRIMARY KEY( fiIdRenglon))

   SELECT @viValorCero = 0, @viValorUno = 1, @viValorDos = 2, @viValorTres = 3, @vcUSUARIO = SUSER_NAME(), 
          @vdFECHAHOY2 = GETDATE()
   SELECT @viFECHOYCORTA = CONVERT(CHAR(8), @vdFECHAHOY2, 112)
  
   --******************************************************
   --******** BUSCA TURNOS EN ESTATUS 1->Generado *********
   BEGIN TRAN UTurnosEmp
      IF EXISTS (SELECT FIFECHA FROM TACJCCTRTURNO WITH (ROWLOCK, UPDLOCK)
                  WHERE FIFECHA = @viFECHOYCORTA AND FISTATUSTURNO = @viValorUno
                    AND FIVIRTUAL = @viValorCero)
      BEGIN
         INSERT INTO @vtTURNOS
            SELECT FIFECHA, FITURNOID, FIUNIDADNEGOCIOID, FIPRIORIDAD
              FROM TACJCCTRTURNO WITH (NOLOCK)
             WHERE FIFECHA >= @viFECHOYCORTA AND FISTATUSTURNO = @viValorUno
               AND FIVIRTUAL = @viValorCero
             ORDER BY FIPRIORIDAD

         IF (@@ERROR <> 0)
         BEGIN 
            ROLLBACK TRAN UTurnosEmp
            SET @vcMensaje= 'Error al insertar en variable tabla @vtTURNOS.1' 
            GOTO CtrlErrores
         END

         SELECT @viIdRenglon = @viValorUno, @viRows = @viValorCero
         SELECT @viRows = COUNT(fiIdRenglon)
           FROM @vtTURNOS
          WHERE fiIdRenglon > @viValorCero

         WHILE @viIdRenglon <= @viRows
         BEGIN
            --** OBTIENE DATOS DEL REGISTRO DE LA VARIABLE TABLA
            SELECT @viFIFECHA=FIFECHA, @viTURNOID=FITURNOID, @viFIUNIDADNEGOCIO = FIUNIDADNEGOCIO
              FROM @vtTURNOS WHERE fiIdRenglon = @viIdRenglon 

            IF EXISTS (SELECT POOLA.FCEMPNOID
                         FROM TACJCCTRPOOLATENCION POOLA WITH (NOLOCK) INNER JOIN TACJCCTRCUALIDADPOOL CUALIDADP WITH (NOLOCK)
                           ON POOLA.FCEMPNOID = CUALIDADP.FCEMPNOID
                        WHERE POOLA.FISTATUSPOOLID = @viValorDos AND CUALIDADP.FCVALOR = @viFIUNIDADNEGOCIO)
            BEGIN
               --** OBTIENE FCEMPNOID
               SELECT TOP 1  @vcEMPNOID=POOLA.FCEMPNOID
                 FROM TACJCCTRPOOLATENCION POOLA WITH (NOLOCK) INNER JOIN TACJCCTRCUALIDADPOOL CUALIDADP WITH (NOLOCK)
                   ON POOLA.FCEMPNOID = CUALIDADP.FCEMPNOID
                WHERE POOLA.FISTATUSPOOLID = @viValorDos AND CUALIDADP.FCVALOR = @viFIUNIDADNEGOCIO
                ORDER BY POOLA.FDFECHAMODIF 

               -- INSERTA HISTORICO-TURNO
               INSERT INTO TACJCCTRHISTORICO (FIFECHA, FITURNOID,FIUNIDADNEGOCIOID, FISTATUSTURNOID, FDACTUALIZACION, 
                                              FDFECHAINSERTA, FCUSERINSERTA)
                    VALUES (@viFIFECHA, @viTURNOID, @viFIUNIDADNEGOCIO, @viValorDos, @vdFECHAHOY2, @vdFECHAHOY2, @vcUSUARIO)

               IF (@@ERROR <> 0)
               BEGIN 
                  ROLLBACK TRAN UTurnosEmp
                  SET @vcMensaje= 'Error al Actualizar en tabla TACJCCTRHISTORICO.1' 
                  GOTO CtrlErrores
               END

               --** ACTUALIZA ESTATUS DEL POOLATENCION A 3->OCUPADO<-
               UPDATE TACJCCTRPOOLATENCION
                  SET FISTATUSPOOLID = @viValorTres
                     ,FDFECHAMODIF = @vdFECHAHOY2
                     ,FCUSERMODIF = @vcUSUARIO
                WHERE FCEMPNOID = @vcEMPNOID

               IF (@@ERROR <> 0)
               BEGIN 
                  ROLLBACK TRAN UTurnosEmp
                  SET @vcMensaje= 'Error al Actualizar en tabla TACJCCTRPOOLATENCION.1' 
                  GOTO CtrlErrores
               END

               --** ACTUALIZA EMPNOID DE TURNO
               UPDATE TACJCCTRTURNO
                  SET FISTATUSTURNO = @viValorDos
                     ,FCEMPNOID = @vcEMPNOID
                     ,FDFECHAMODIF = @vdFECHAHOY2
                     ,FCUSERMODIF = @vcUSUARIO
                WHERE FIFECHA = @viFIFECHA
                  AND FITURNOID = @viTURNOID
                  AND FIUNIDADNEGOCIOID = @viFIUNIDADNEGOCIO

               IF (@@ERROR <> 0)
               BEGIN 
                  ROLLBACK TRAN UTurnosEmp
                  SET @vcMensaje= 'Error al Actualizar en tabla TACJCCTRTURNO.1' 
                  GOTO CtrlErrores
               END
            END
            SET @viIdRenglon = @viIdRenglon + @viValorUno
         END
      END
   COMMIT TRAN UTurnosEmp

   SET NOCOUNT OFF
   RETURN 0  

--******** CtrlErrores ***********
CtrlErrores:
   SET NOCOUNT OFF
   SET @vcMensaje = 'Error al Ejecutar el SP -> PACJCCLUTRAsigna. ' + ISNULL (@vcMensaje,'')
 
   -- Termina con error 
   RAISERROR (@vcMensaje, 18, 1) 
   RETURN -1
GO

EXEC DBO.SPGRANT 'DBO.PACJCCLUTRAsigna'
GO

---~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~

IF OBJECT_ID('DBO.PACJCCLUTRCaduca','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRCaduca;
END
GO
---------------------------------------------------------------------------------------------------------      
---Responsable          : Roberto Gonzalez Figueroa
---Fecha                : DIC2013
---Descripcion          : Caduca Turnos; Cambia el Estatus de un Turno Pospuesto a Caduco
---------------------------------------------------------------------------------------------------------      
CREATE PROC dbo.PACJCCLUTRCaduca (
   @piUnidadNegocio       SMALLINT
)

AS

SET NOCOUNT ON

   DECLARE
      @vcUSUARIO        CHAR(10),
      @viIdRenglon      INTEGER, 
      @viRows           INTEGER,
      @vdFECHOY         DATETIME,
      @viFECHOYCORTA    INTEGER,
      @vcMensaje        VARCHAR(255),
      @viUnidadNegocio  SMALLINT,
      @viStatusCaduco   SMALLINT,
      @viStatusPosp     SMALLINT,
      @viValorCero      INT,
      @viValorUno       INT,
      @viTURNOID        INTEGER,
      @viUndNeg         SMALLINT,
      @viModulo         SMALLINT,
      @viFolioConfig    SMALLINT,
      @vcValor          VARCHAR(255),
      @viContadorTurnos INTEGER

   DECLARE @vtPOSPUESTOS TABLE (fiIdRenglon INT NOT NULL IDENTITY(1,1),
                                FITURNOID INTEGER, FIUNIDADNEGOCIO SMALLINT
                                PRIMARY KEY( fiIdRenglon))

   SELECT @vdFECHOY = GETDATE(), @viValorCero = 0, @viValorUno = 1, @viModulo = 104, @viFolioConfig = 2
   SELECT @viFECHOYCORTA = CONVERT(CHAR(8), @vdFECHOY, 112), @vcUSUARIO = SUSER_NAME(),
          @viUnidadNegocio = ISNULL(@piUnidadNegocio,0), @viStatusPosp = 7, @viStatusCaduco = 5

   SELECT @vcValor = FCVALOR 
     FROM catcajconfiguracion WITH (NOLOCK)
    where fimodulo = @viModulo and fifolio = @viFolioConfig

   ------ Obtiene los Turnos a Caducar de una o varias Unidades de Negocio ------
   BEGIN TRAN UTRCaduca
      IF @viUnidadNegocio = 0
      BEGIN
         IF EXISTS(SELECT FITURNOID FROM TACJCCTRTURNO WITH (NOLOCK)
                    WHERE FIUNIDADNEGOCIOID > @viUnidadNegocio
                      AND FISTATUSTURNO = @viStatusPosp
                      AND FIFECHA = @viFECHOYCORTA)
         BEGIN
            INSERT INTO @vtPOSPUESTOS
               SELECT FITURNOID, FIUNIDADNEGOCIOID
                 FROM TACJCCTRTURNO WITH (NOLOCK)
                WHERE FIUNIDADNEGOCIOID > @viUnidadNegocio 
                 AND FISTATUSTURNO = @viStatusPosp
                 AND FIFECHA = @viFECHOYCORTA

            IF (@@ERROR <> 0)
            BEGIN 
               SET @vcMensaje= 'Error al insertar en variable tabla @vtPOSPUESTOS.1' 
               GOTO CtrlErrores
            END
         END
      END
      ELSE
      BEGIN
         IF EXISTS(SELECT FITURNOID FROM TACJCCTRTURNO WITH (NOLOCK)
                    WHERE FIUNIDADNEGOCIOID = @viUnidadNegocio
                      AND FISTATUSTURNO = @viStatusPosp
                      AND FIFECHA = @viFECHOYCORTA)
         BEGIN
            INSERT INTO @vtPOSPUESTOS
               SELECT FITURNOID, FIUNIDADNEGOCIOID
                 FROM TACJCCTRTURNO WITH (NOLOCK)
                WHERE FIUNIDADNEGOCIOID = @viUnidadNegocio 
                 AND FISTATUSTURNO = @viStatusPosp
                 AND FIFECHA = @viFECHOYCORTA
				 
            IF (@@ERROR <> 0)
            BEGIN 
               SET @vcMensaje= 'Error al insertar en variable tabla @vtPOSPUESTOS.2' 
               GOTO CtrlErrores
            END
         END
      END

      ------ Realiza el cambio de los Turnos a Caducos si cumplen con el parametro ------
      SELECT @viIdRenglon = @viValorUno, @viRows = @viValorCero
      SELECT @viRows = COUNT(fiIdRenglon)
        FROM @vtPOSPUESTOS
       WHERE fiIdRenglon > @viValorCero

      IF (@@ERROR <> 0)
      BEGIN 
         SET @vcMensaje= 'Error al seleccionar @vtPOSPUESTOS.2' 
         GOTO CtrlErrores
      END

      WHILE @viIdRenglon <= @viRows
      BEGIN
         BEGIN TRAN Pospuesto
            --** OBTIENE DATOS DEL REGISTRO DE LA VARIABLE TABLA
            SELECT @viTURNOID=FITURNOID, @viUndNeg=FIUNIDADNEGOCIO
              FROM @vtPOSPUESTOS WHERE fiIdRenglon = @viIdRenglon 

            SELECT @viContadorTurnos = COUNT(FITURNOID) 
              FROM TACJCCTRTURNO WITH (NOLOCK)
             WHERE FIUNIDADNEGOCIOID = @viUndNeg
               AND FITURNOID > @viTURNOID

            IF @viContadorTurnos >= @vcValor
            BEGIN
               ------ Proceso que Caduca el Turno ------
               EXEC PACJCCLUTRHistorico @viFECHOYCORTA, @viTURNOID, @viUnidadNegocio, @viStatusCaduco, @vcUSUARIO

               IF (@@ERROR <> 0)
               BEGIN 
                  ROLLBACK TRAN Pospuesto
                  SET @vcMensaje= 'Error al Caducar Turno' 
               END
            END
         COMMIT TRAN Pospuesto
         SET @viIdRenglon = @viIdRenglon + @viValorUno
      END

   COMMIT TRAN UTRCaduca
   SET NOCOUNT OFF
   RETURN 0  

--******** CtrlErrores ***********
CtrlErrores:
   SET NOCOUNT OFF
   ROLLBACK TRAN UTRCaduca
   SET @vcMensaje = 'Error al Ejecutar el SP -> PACJCCLUTRCaduca. ' + ISNULL (@vcMensaje,'')
 
   -- Termina con error 
   RAISERROR (@vcMensaje, 18, 1) 
   RETURN -1
GO

EXEC DBO.SPGRANT 'DBO.PACJCCLUTRCaduca'
GO

---~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~

IF OBJECT_ID('DBO.PACJCCLUTRCatCualidades','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRCatCualidades;
END	
GO
---------------------------------------------------------------------------------------------------------      
---Responsable          : Roberto Gonzalez Figueroa
---Fecha                : Sep2013
---Descripcion          : Origen-Turnos; Mantenimiento al Catalogo de Cualidades
---------------------------------------------------------------------------------------------------------      
CREATE PROC dbo.PACJCCLUTRCatCualidades (
   @piTienda             SMALLINT,
   @piProyectoMQ         INT,
   @piFolioMQ            INT,
   @piCualidadID         TINYINT,
   @pcDescripcion        CHAR(50),
   @pcUser               CHAR(10)
)

AS

SET NOCOUNT ON

   DECLARE
      @vdActual  SMALLDATETIME,
      @viTienda        SMALLINT,
      @viReturn        INTEGER,
      @vcMensaje       VARCHAR(255),
      @vcMensajeMQ     CHAR(70),
      @vcParametrosMQ  CHAR(200)

   SET @vdActual = GETDATE()

   -- Asigna viTienda de la tabla CONTROL
   SELECT @viTienda = fiNoTienda FROM CONTROL WITH (NOLOCK) 

   -- Valida si Existe el folio MQ 
   IF EXISTS (SELECT fiFolio FROM CAJBITMensajesMQ WITH (NOLOCK) 
               WHERE fiProyecto = @piProyectoMQ AND fiFolio = @piFolioMQ)
   BEGIN 
      -- Se sale ya que este caso no debe pasar 
      SET NOCOUNT OFF 
      RETURN 0 
   END 

   -- Valida Tienda de tabla Control vs Parametro de Tienda
   IF (@piTienda <> @viTienda) 
   BEGIN 
      SET @vcMensaje = 'El valor de la Tienda ->' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + 
                       '<- NO corresponde a la sucursal.' 
      GOTO CtrlAdvertenciaMQ 
   END
   
   BEGIN TRAN UCatCualidades
   -- Valida si Existe Cualidades del Turnador
   IF NOT EXISTS (SELECT FICUALIDADID FROM TCCJCCTRCUALIDADES WITH (NOLOCK) 
                   WHERE FICUALIDADID = @piCualidadID)
   BEGIN 
      -- Inserta Tipo Fila del Turnador
      INSERT TCCJCCTRCUALIDADES (FICUALIDADID, FCDESCRIPCION, FDFECHAINSERTA, FCUSERINSERTA)
         VALUES (@piCualidadID,@pcDescripcion, @vdActual, @pcUser)

      IF ( @@ERROR <> 0 )
      BEGIN 
	     ROLLBACK TRAN UCatCualidades
         SET @vcMensaje= 'Error al insertar en tabla TCCJCCTRCUALIDADES.' 
         GOTO CtrlErrores 
      END
   END
   ELSE
   BEGIN 
      -- Actualiza
      UPDATE TCCJCCTRCUALIDADES
         SET FCDESCRIPCION = @pcDescripcion
            ,FDFECHAMODIF = @vdActual
            ,FCUSERMODIF = @pcUser
       WHERE FICUALIDADID = @piCualidadID

      IF ( @@ERROR <> 0 )
      BEGIN 
	     ROLLBACK TRAN UCatCualidades
         SET @vcMensaje= 'Error al actualizar tabla TCCJCCTRCUALIDADES.' 
         GOTO CtrlErrores 
      END
   END

   --***************************************
   --********* Arma el mensaje para MQ *****
   SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                         '@piCualidadID = [' + CONVERT(VARCHAR, ISNULL(@piCualidadID, '')) + ']' + 
                         '@pcDescripcion = [' + @pcDescripcion + ']' + 
                         '@pcUser = [' + @pcUser + '].'

   -- Genera el mensaje de confirmacion de MQ 
   EXEC @viReturn = SPINSCAJMensajesMQ485 @piProyectoMQ, @piFolioMQ, @vcParametrosMQ, 0, ' ' 

   -- Si existe error 
   IF (@@ERROR <> 0) OR (@viReturn < 0) 
   BEGIN 
      ROLLBACK TRAN UCatCualidades
      SET @vcMensaje= 'Error al generar la confirmacion de MQ en SPINSCAJMensajesMQ485.' 
      GOTO CtrlErrores 
   END 

   COMMIT TRAN UCatCualidades
   SET NOCOUNT OFF
   RETURN 0  

--******** CtrlAdvertenciaMQ ************
CtrlAdvertenciaMQ:
   SET NOCOUNT OFF 
 
   SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                         '@piCualidadID = [' + CONVERT(VARCHAR, ISNULL(@piCualidadID, '')) + ']' + 
                         '@pcDescripcion = [' + @pcDescripcion + ']' + 
                         '@pcUser = [' + @pcUser + '].'
  
   -- Asigna el mensaje MQ 
   SET @vcMensajeMQ =CONVERT(CHAR (70), @vcMensaje) 
 
   -- Envia respuesta MQ 
   EXEC @viReturn = SPINSCAJMensajesMQ485 @piProyectoMQ, @piFolioMQ, 
                     @vcParametrosMQ, 1,  
                     @vcMensajeMQ 

   IF (@@ERROR <> 0) OR (@viReturn < 0) 
   BEGIN 
      SET @vcMensaje= 'Error al generar la confirmacion de error MQ en SPINSCAJMensajesMQ485.' 
      GOTO CtrlErrores 
   END 

   RETURN 0 

--******** CtrlErrores ***********
CtrlErrores:
   SET NOCOUNT OFF
   SET @vcMensaje = 'Error al Ejecutar el SP -> PACJCCLUTRCatCualidades. ' + ISNULL (@vcMensaje,'')
 
   SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                         '@piCualidadID = [' + CONVERT(VARCHAR, ISNULL(@piCualidadID, '')) + ']' + 
                         '@pcDescripcion = [' + @pcDescripcion + ']' + 
                         '@pcUser = [' + @pcUser + '].'
 
    -- Asigna el mensaje MQ 
   SET @vcMensajeMQ =CONVERT(CHAR(70), @vcMensaje) 
 
   -- Envia respuesta MQ 
   EXEC @viReturn = SPINSCAJMensajesMQ485 @piProyectoMQ, @piFolioMQ, 
                     @vcParametrosMQ, 0, 
                     @vcMensajeMQ 

   IF (@@ERROR <> 0) OR (@viReturn < 0) 
   BEGIN 
      SET @vcMensaje= 'Error al generar la confirmacion de error MQ en SPINSCAJMensajesMQ485.'
   END 

   -- Termina con error 
   RAISERROR (@vcMensaje, 18, 1) 
   RETURN -1
GO

EXEC DBO.SPGRANT 'DBO.PACJCCLUTRCatCualidades'
GO

---~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~

IF OBJECT_ID('DBO.PACJCCLUTRCatEstadoPool','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRCatEstadoPool;
END
GO
---------------------------------------------------------------------------------------------------------      
---Responsable          : Roberto Gonzalez Figueroa
---Fecha                : Sep2013
---Descripcion          : Origen-Turnos; Mantenimiento al Catalogo de Estatuds del Pool
---------------------------------------------------------------------------------------------------------      
CREATE PROC dbo.PACJCCLUTRCatEstadoPool (
   @piTienda             SMALLINT,
   @piProyectoMQ         INT,
   @piFolioMQ            INT,
   @piEstatusPoolID      TINYINT,
   @pcDescripcion        CHAR(50),
   @pcUser               CHAR(10)
)

AS

SET NOCOUNT ON

   DECLARE  
      @vdActual  SMALLDATETIME,
      @viTienda        SMALLINT,
      @viReturn        INTEGER,
      @vcMensaje       VARCHAR(255),
      @vcMensajeMQ     CHAR(70),
      @vcParametrosMQ  CHAR(200)

   SET @vdActual = GETDATE()
   
   -- Asigna viTienda de la tabla CONTROL
   SELECT @viTienda = fiNoTienda FROM CONTROL WITH (NOLOCK) 

   -- Valida si Existe el folio MQ 
   IF EXISTS (SELECT fiFolio FROM CAJBITMensajesMQ WITH (NOLOCK) 
               WHERE fiProyecto = @piProyectoMQ AND fiFolio = @piFolioMQ)
   BEGIN 
      -- Se sale ya que este caso no debe pasar 
      SET NOCOUNT OFF 
      RETURN 0 
   END 

   -- Valida Tienda de tabla Control vs Parametro de Tienda
   IF (@piTienda <> @viTienda) 
   BEGIN 
      SET @vcMensaje = 'El valor de la Tienda ->' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + 
                       '<- NO corresponde a la sucursal.' 
      GOTO CtrlAdvertenciaMQ 
   END

   BEGIN TRAN UCatEstadoPool
   -- Valida si Existe TipoFila del Turnador
   IF NOT EXISTS (SELECT FISTATUSPOOLID FROM TCCJCCTRESTADOPOOL WITH (NOLOCK) 
                   WHERE FISTATUSPOOLID = @piEstatusPoolID)
   BEGIN 
      -- Inserta Tipo Fila del Turnador
      INSERT TCCJCCTRESTADOPOOL (FISTATUSPOOLID, FCDESCRIPCION, FDFECHAINSERTA, FCUSERINSERTA)
         VALUES (@piEstatusPoolID,@pcDescripcion, @vdActual, @pcUser)

      IF ( @@ERROR <> 0 )
      BEGIN 
         ROLLBACK TRAN UCatEstadoPool
         SET @vcMensaje= 'Error al insertar en tabla TCCJCCTRESTADOPOOL.' 
         GOTO CtrlErrores 
      END
   END
   ELSE
   BEGIN 
      -- Actualiza
      UPDATE TCCJCCTRESTADOPOOL
         SET FCDESCRIPCION = @pcDescripcion
            ,FDFECHAMODIF = @vdActual
            ,FCUSERMODIF = @pcUser
       WHERE FISTATUSPOOLID = @piEstatusPoolID

      IF ( @@ERROR <> 0 )
      BEGIN 
         ROLLBACK TRAN UCatEstadoPool
         SET @vcMensaje= 'Error al actualizar tabla TCCJCCTRESTADOPOOL.' 
         GOTO CtrlErrores 
      END
   END

   --***************************************
   --********* Arma el mensaje para MQ *****
   SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                         '@piEstatusPoolID = [' + CONVERT(VARCHAR, ISNULL(@piEstatusPoolID, '')) + ']' + 
                         '@pcDescripcion = [' + @pcDescripcion + ']' + 
                         '@pcUser = [' + @pcUser + '].'

   -- Genera el mensaje de confirmacion de MQ 
   EXEC @viReturn = SPINSCAJMensajesMQ485 @piProyectoMQ, @piFolioMQ, @vcParametrosMQ, 0, ' ' 

   -- Si existe error 
   IF (@@ERROR <> 0) OR (@viReturn < 0) 
   BEGIN 
      ROLLBACK TRAN UCatEstadoPool
      SET @vcMensaje= 'Error al generar la confirmacion de MQ en SPINSCAJMensajesMQ485.' 
      GOTO CtrlErrores 
   END 

   COMMIT TRAN UCatEstadoPool
   SET NOCOUNT OFF
   RETURN 0  

--******** CtrlAdvertenciaMQ ************
CtrlAdvertenciaMQ:
   SET NOCOUNT OFF 
 
   SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                         '@piEstatusPoolID = [' + CONVERT(VARCHAR, ISNULL(@piEstatusPoolID, '')) + ']' + 
                         '@pcDescripcion = [' + @pcDescripcion + ']' + 
                         '@pcUser = [' + @pcUser + '].'
  
   -- Asigna el mensaje MQ 
   SET @vcMensajeMQ =CONVERT(CHAR (70), @vcMensaje) 
 
   -- Envia respuesta MQ 
   EXEC @viReturn = SPINSCAJMensajesMQ485 @piProyectoMQ, @piFolioMQ, 
                     @vcParametrosMQ, 1,  
                     @vcMensajeMQ 

   IF (@@ERROR <> 0) OR (@viReturn < 0) 
   BEGIN 
      SET @vcMensaje= 'Error al generar la confirmacion de error MQ en SPINSCAJMensajesMQ485.' 
      GOTO CtrlErrores 
   END 

   RETURN 0 

--******** CtrlErrores ***********
CtrlErrores:
   SET NOCOUNT OFF
   SET @vcMensaje = 'Error al Ejecutar el SP -> PACJCCLUTRCatEstadoPool. ' + ISNULL (@vcMensaje,'')
 
   SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                         '@piEstatusPoolID = [' + CONVERT(VARCHAR, ISNULL(@piEstatusPoolID, '')) + ']' + 
                         '@pcDescripcion = [' + @pcDescripcion + ']' + 
                         '@pcUser = [' + @pcUser + '].'
 
    -- Asigna el mensaje MQ 
   SET @vcMensajeMQ =CONVERT(CHAR(70), @vcMensaje) 
 
   -- Envia respuesta MQ 
   EXEC @viReturn = SPINSCAJMensajesMQ485 @piProyectoMQ, @piFolioMQ, 
                     @vcParametrosMQ, 0, 
                     @vcMensajeMQ 

   IF (@@ERROR <> 0) OR (@viReturn < 0) 
   BEGIN 
      SET @vcMensaje= 'Error al generar la confirmacion de error MQ en SPINSCAJMensajesMQ485.'
   END 

   -- Termina con error 
   RAISERROR (@vcMensaje, 18, 1) 
   RETURN -1
GO

EXEC DBO.SPGRANT 'DBO.PACJCCLUTRCatEstadoPool'
GO

---~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~

IF OBJECT_ID('DBO.PACJCCLUTRCatEstadoTurno','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRCatEstadoTurno;
END
GO
---------------------------------------------------------------------------------------------------------      
---Responsable          : Roberto Gonzalez Figueroa
---Fecha                : Sep2013
---Descripcion          : Origen-Turnos; Mantenimiento al Catalogo de Estatuds del Turno
---------------------------------------------------------------------------------------------------------      
CREATE PROC dbo.PACJCCLUTRCatEstadoTurno (
   @piTienda             SMALLINT,
   @piProyectoMQ         INT,
   @piFolioMQ            INT,
   @piEstatusTurnoID     TINYINT,
   @pcDescripcion        CHAR(50),
   @pcUser               CHAR(10)
)

AS

SET NOCOUNT ON

   DECLARE
      @vdActual  SMALLDATETIME,
      @viTienda        SMALLINT,
      @viReturn        INTEGER,
      @vcMensaje       VARCHAR(255),
      @vcMensajeMQ     CHAR(70),
      @vcParametrosMQ  CHAR(200)

   SET @vdActual = GETDATE()
   
   -- Asigna viTienda de la tabla CONTROL
   SELECT @viTienda = fiNoTienda FROM CONTROL WITH (NOLOCK) 

   -- Valida si Existe el folio MQ 
   IF EXISTS (SELECT fiFolio FROM CAJBITMensajesMQ WITH (NOLOCK) 
               WHERE fiProyecto = @piProyectoMQ AND fiFolio = @piFolioMQ)
   BEGIN 
      -- Se sale ya que este caso no debe pasar 
      SET NOCOUNT OFF 
      RETURN 0 
   END 

   -- Valida Tienda de tabla Control vs Parametro de Tienda
   IF (@piTienda <> @viTienda) 
   BEGIN 
      SET @vcMensaje = 'El valor de la Tienda ->' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + 
                       '<- NO corresponde a la sucursal.' 
      GOTO CtrlAdvertenciaMQ 
   END

   BEGIN TRAN UCatEstadoTurno
      -- Valida si Existe TipoFila del Turnador
      IF NOT EXISTS (SELECT FISTATUSTURNOID FROM TCCJCCTRESTADOTURNO WITH (NOLOCK) 
                      WHERE FISTATUSTURNOID = @piEstatusTurnoID)
      BEGIN 
         -- Inserta Tipo Fila del Turnador
         INSERT TCCJCCTRESTADOTURNO (FISTATUSTURNOID, FCDESCRIPCION, FDFECHAINSERTA, FCUSERINSERTA)
            VALUES (@piEstatusTurnoID,@pcDescripcion, @vdActual, @pcUser)

         IF ( @@ERROR <> 0 )
         BEGIN 
            ROLLBACK TRAN UCatEstadoTurno
            SET @vcMensaje= 'Error al insertar en tabla TCCJCCTRESTADOTURNO.' 
            GOTO CtrlErrores 
         END
      END
      ELSE
      BEGIN 
         -- Actualiza
         UPDATE TCCJCCTRESTADOTURNO
            SET FCDESCRIPCION = @pcDescripcion
               ,FDFECHAMODIF = @vdActual
               ,FCUSERMODIF = @pcUser
          WHERE FISTATUSTURNOID = @piEstatusTurnoID

         IF ( @@ERROR <> 0 )
         BEGIN 
            ROLLBACK TRAN UCatEstadoTurno
            SET @vcMensaje= 'Error al actualizar ->Nombre<- en tabla TCCJCCTRESTADOTURNO.' 
            GOTO CtrlErrores 
         END
      END

      --***************************************
      --********* Arma el mensaje para MQ *****
      SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                            '@piEstatusTurnoID = [' + CONVERT(VARCHAR, ISNULL(@piEstatusTurnoID, '')) + ']' + 
                            '@pcDescripcion = [' + @pcDescripcion + ']' + 
                            '@pcUser = [' + @pcUser + '].'

      -- Genera el mensaje de confirmacion de MQ 
      EXEC @viReturn = SPINSCAJMensajesMQ485 @piProyectoMQ, @piFolioMQ, @vcParametrosMQ, 0, ' ' 

      -- Si existe error 
      IF (@@ERROR <> 0) OR (@viReturn < 0) 
      BEGIN 
	     ROLLBACK TRAN UCatEstadoTurno
         SET @vcMensaje= 'Error al generar la confirmacion de MQ en SPINSCAJMensajesMQ485.' 
         GOTO CtrlErrores 
      END 

   COMMIT TRAN UCatEstadoTurno
   SET NOCOUNT OFF
   RETURN 0  

--******** CtrlAdvertenciaMQ ************
CtrlAdvertenciaMQ:
   SET NOCOUNT OFF 
 
   SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                         '@piEstatusTurnoID = [' + CONVERT(VARCHAR, ISNULL(@piEstatusTurnoID, '')) + ']' + 
                         '@pcDescripcion = [' + @pcDescripcion + ']' + 
                         '@pcUser = [' + @pcUser + '].'
  
   -- Asigna el mensaje MQ 
   SET @vcMensajeMQ =CONVERT(CHAR (70), @vcMensaje) 
 
   -- Envia respuesta MQ 
   EXEC @viReturn = SPINSCAJMensajesMQ485 @piProyectoMQ, @piFolioMQ, 
                     @vcParametrosMQ, 1,  
                     @vcMensajeMQ 

   IF (@@ERROR <> 0) OR (@viReturn < 0) 
   BEGIN 
      SET @vcMensaje= 'Error al generar la confirmacion de error MQ en SPINSCAJMensajesMQ485.' 
      GOTO CtrlErrores 
   END 

   RETURN 0 

--******** CtrlErrores ***********
CtrlErrores:
   SET NOCOUNT OFF
   SET @vcMensaje = 'Error al Ejecutar el SP -> PACJCCLUTRCatEstadoTurno. ' + ISNULL (@vcMensaje,'')
 
   SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                         '@piEstatusTurnoID = [' + CONVERT(VARCHAR, ISNULL(@piEstatusTurnoID, '')) + ']' + 
                         '@pcDescripcion = [' + @pcDescripcion + ']' + 
                         '@pcUser = [' + @pcUser + '].'
 
    -- Asigna el mensaje MQ 
   SET @vcMensajeMQ =CONVERT(CHAR(70), @vcMensaje) 
 
   -- Envia respuesta MQ 
   EXEC @viReturn = SPINSCAJMensajesMQ485 @piProyectoMQ, @piFolioMQ, 
                     @vcParametrosMQ, 0, 
                     @vcMensajeMQ 

   IF (@@ERROR <> 0) OR (@viReturn < 0) 
   BEGIN 
      SET @vcMensaje= 'Error al generar la confirmacion de error MQ en SPINSCAJMensajesMQ485.'
   END 

   -- Termina con error 
   RAISERROR (@vcMensaje, 18, 1) 
   RETURN -1
GO

EXEC DBO.SPGRANT 'DBO.PACJCCLUTRCatEstadoTurno'
GO

---~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~

IF OBJECT_ID('DBO.PACJCCLUTRCatOrigen','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRCatOrigen;
END
GO
---------------------------------------------------------------------------------------------------------      
---Responsable          : Roberto Gonzalez Figueroa
---Fecha                : Sep2013
---Descripcion          : Origen-Turnos; Mantenimiento al Catalogo de Origen de Turnos
---------------------------------------------------------------------------------------------------------      
CREATE PROC dbo.PACJCCLUTRCatOrigen (
   @piTienda             SMALLINT,
   @piProyectoMQ         INT,
   @piFolioMQ            INT,
   @piOrigenTurno        TINYINT,
   @pcDescripcionOrig    CHAR(50),
   @pcUser               CHAR(10)
)

AS

SET NOCOUNT ON

   DECLARE 
      @vdActual        SMALLDATETIME,
      @viTienda        SMALLINT,
      @viReturn        INTEGER,
      @vcMensaje       VARCHAR(255),
      @vcMensajeMQ     CHAR(70),
      @vcParametrosMQ  CHAR(200)

   SET @vdActual = GETDATE()

   -- Asigna viTienda de la tabla CONTROL
   SELECT @viTienda = fiNoTienda FROM CONTROL WITH (NOLOCK) 

   -- Valida si Existe el folio MQ 
   IF EXISTS (SELECT fiFolio FROM CAJBITMensajesMQ WITH (NOLOCK) 
               WHERE fiProyecto = @piProyectoMQ AND fiFolio = @piFolioMQ)
   BEGIN 
      -- Se sale ya que este caso no debe pasar 
      SET NOCOUNT OFF 
      RETURN 0 
   END 

   -- Valida Tienda de tabla Control vs Parametro de Tienda
   IF (@piTienda <> @viTienda) 
   BEGIN 
      SET @vcMensaje = 'El valor de la Tienda ->' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + 
                       '<- NO corresponde a la sucursal.' 
      GOTO CtrlAdvertenciaMQ 
   END

   BEGIN TRAN UCatOrigen
      -- Valida si Existe el Origen del Turnador
      IF NOT EXISTS (SELECT FIORIGENID FROM TCCJCCTRORIGEN WITH (NOLOCK) 
                      WHERE FIORIGENID = @piOrigenTurno)
      BEGIN 
         -- Inserta Nuevo Origen de Turnador
         INSERT TCCJCCTRORIGEN (FIORIGENID, FCDESCRIPCION, FDFECHAINSERTA, FCUSERINSERTA)
            VALUES (@piOrigenTurno,@pcDescripcionOrig, @vdActual, @pcUser)

         IF ( @@ERROR <> 0 )
         BEGIN 
            ROLLBACK TRAN UCatOrigen
            SET @vcMensaje= 'Error al insertar en tabla TCCJCCTRORIGEN.' 
            GOTO CtrlErrores 
         END
      END
      ELSE
      BEGIN 
         -- Actualiza
         UPDATE TCCJCCTRORIGEN
            SET FCDESCRIPCION = @pcDescripcionOrig
               ,FDFECHAMODIF = @vdActual
               ,FCUSERMODIF = @pcUser
          WHERE FIORIGENID = @piOrigenTurno

         IF ( @@ERROR <> 0 )
         BEGIN 
            ROLLBACK TRAN UCatOrigen
            SET @vcMensaje= 'Error al actualizar en tabla TCCJCCTRORIGEN.' 
            GOTO CtrlErrores 
         END
      END

      --***************************************
      --********* Arma el mensaje para MQ *****
      SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                            '@piOrigenTurno = [' + CONVERT(VARCHAR, ISNULL(@piOrigenTurno, '')) + ']' + 
                            '@pcDescripcionOrig = [' + @pcDescripcionOrig + ']' + 
                            '@pcUser = [' + @pcUser + '].'

      -- Genera el mensaje de confirmacion de MQ 
      EXEC @viReturn = SPINSCAJMensajesMQ485 @piProyectoMQ, @piFolioMQ, @vcParametrosMQ, 0, ' ' 

      -- Si existe error 
      IF (@@ERROR <> 0) OR (@viReturn < 0) 
      BEGIN 
         ROLLBACK TRAN UCatOrigen
         SET @vcMensaje= 'Error al generar la confirmacion de MQ en SPINSCAJMensajesMQ485.' 
         GOTO CtrlErrores 
      END 

   COMMIT TRAN UCatOrigen
   SET NOCOUNT OFF
   RETURN 0

--******** CtrlAdvertenciaMQ ************
CtrlAdvertenciaMQ:
   SET NOCOUNT OFF 
 
   SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                         '@piOrigenTurno = [' + CONVERT(VARCHAR, ISNULL(@piOrigenTurno, '')) + ']' + 
                         '@pcDescripcionOrig = [' + @pcDescripcionOrig + ']' + 
                         '@pcUser = [' + @pcUser + '].'
  
   -- Asigna el mensaje MQ 
   SET @vcMensajeMQ =CONVERT(CHAR (70), @vcMensaje) 
 
   -- Envia respuesta MQ 
   EXEC @viReturn = SPINSCAJMensajesMQ485 @piProyectoMQ, @piFolioMQ, 
                     @vcParametrosMQ, 1,  
                     @vcMensajeMQ 

   IF (@@ERROR <> 0) OR (@viReturn < 0) 
   BEGIN 
      SET @vcMensaje= 'Error al generar la confirmacion de error MQ en SPINSCAJMensajesMQ485.' 
      GOTO CtrlErrores 
   END 

   RETURN 0 

--******** CtrlErrores ***********
CtrlErrores:
   SET NOCOUNT OFF
   SET @vcMensaje = 'Error al Ejecutar el SP -> PACJCCLUTRCatOrigen. ' + ISNULL (@vcMensaje,'')
 
   SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                         '@piOrigenTurno = [' + CONVERT(VARCHAR, ISNULL(@piOrigenTurno, '')) + ']' + 
                         '@pcDescripcionOrig = [' + @pcDescripcionOrig + ']' + 
                         '@pcUser = [' + @pcUser + '].'
 
    -- Asigna el mensaje MQ 
   SET @vcMensajeMQ =CONVERT(CHAR(70), @vcMensaje) 
 
   -- Envia respuesta MQ 
   EXEC @viReturn = SPINSCAJMensajesMQ485 @piProyectoMQ, @piFolioMQ, 
                     @vcParametrosMQ, 0, 
                     @vcMensajeMQ 

   IF (@@ERROR <> 0) OR (@viReturn < 0) 
   BEGIN 
      SET @vcMensaje= 'Error al generar la confirmacion de error MQ en SPINSCAJMensajesMQ485.'
   END 

   -- Termina con error 
   RAISERROR (@vcMensaje, 18, 1) 
   RETURN -1
GO

EXEC DBO.SPGRANT 'DBO.PACJCCLUTRCatOrigen'
GO

---~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~

IF OBJECT_ID('DBO.PACJCCLUTRCatTipoFila','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRCatTipoFila;
END
GO
---------------------------------------------------------------------------------------------------------      
---Responsable          : Roberto Gonzalez Figueroa
---Fecha                : Sep2013
---Descripcion          : Origen-Turnos; Mantenimiento al Catalogo de Tipos de Fila
---------------------------------------------------------------------------------------------------------      
CREATE PROC dbo.PACJCCLUTRCatTipoFila (
   @piTienda             SMALLINT,
   @piProyectoMQ         INT,
   @piFolioMQ            INT,
   @piTipoFilaID         TINYINT,
   @pcDescripcionTP      CHAR(50),
   @pcUser               CHAR(10)
)

AS

SET NOCOUNT ON

   DECLARE
      @vdActual  SMALLDATETIME,
      @viTienda        SMALLINT,
      @viReturn        INTEGER,
      @vcMensaje       VARCHAR(255),
      @vcMensajeMQ     CHAR(70),
      @vcParametrosMQ  CHAR(200)

   SET @vdActual = GETDATE()

   -- Asigna viTienda de la tabla CONTROL
   SELECT @viTienda = fiNoTienda FROM CONTROL WITH (NOLOCK) 

   -- Valida si Existe el folio MQ 
   IF EXISTS (SELECT fiFolio FROM CAJBITMensajesMQ WITH (NOLOCK) 
               WHERE fiProyecto = @piProyectoMQ AND fiFolio = @piFolioMQ)
   BEGIN 
      -- Se sale ya que este caso no debe pasar 
      SET NOCOUNT OFF 
      RETURN 0 
   END 

   -- Valida Tienda de tabla Control vs Parametro de Tienda
   IF (@piTienda <> @viTienda) 
   BEGIN 
      SET @vcMensaje = 'El valor de la Tienda ->' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + 
                       '<- NO corresponde a la sucursal.' 
      GOTO CtrlAdvertenciaMQ 
   END

   BEGIN TRAN UCatTipoFila
      -- Valida si Existe TipoFila del Turnador
      IF NOT EXISTS (SELECT FITIPOFILAID FROM TCCJCCTRTIPOFILA WITH (NOLOCK) 
                      WHERE FITIPOFILAID = @piTipoFilaID)
      BEGIN 
         -- Inserta Tipo Fila del Turnador
         INSERT TCCJCCTRTIPOFILA (FITIPOFILAID, FCDESCRIPCION, FDFECHAINSERTA, FCUSERINSERTA)
            VALUES (@piTipoFilaID,@pcDescripcionTP, @vdActual, @pcUser)

         IF ( @@ERROR <> 0 )
         BEGIN 
            ROLLBACK TRAN UCatTipoFila
            SET @vcMensaje= 'Error al insertar en tabla TCCJCCTRTIPOFILA.' 
            GOTO CtrlErrores 
         END
      END
      ELSE
      BEGIN 
         -- Actualiza
         UPDATE TCCJCCTRTIPOFILA
            SET FCDESCRIPCION = @pcDescripcionTP
               ,FDFECHAMODIF = @vdActual
               ,FCUSERMODIF = @pcUser
          WHERE FITIPOFILAID = @piTipoFilaID

         IF ( @@ERROR <> 0 )
         BEGIN 
            ROLLBACK TRAN UCatTipoFila
            SET @vcMensaje= 'Error al actualizar ->Nombre<- en tabla TCCJCCTRTIPOFILA.' 
            GOTO CtrlErrores 
         END
      END

      --***************************************
      --********* Arma el mensaje para MQ *****
      SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                            '@piTipoFilaID = [' + CONVERT(VARCHAR, ISNULL(@piTipoFilaID, '')) + ']' + 
                            '@pcDescripcionTP = [' + @pcDescripcionTP + ']' + 
                            '@pcUser = [' + @pcUser + '].'

      -- Genera el mensaje de confirmacion de MQ 
      EXEC @viReturn = SPINSCAJMensajesMQ485 @piProyectoMQ, @piFolioMQ, @vcParametrosMQ, 0, ' ' 

      -- Si existe error 
      IF (@@ERROR <> 0) OR (@viReturn < 0) 
      BEGIN 
         ROLLBACK TRAN UCatTipoFila
         SET @vcMensaje= 'Error al generar la confirmacion de MQ en SPINSCAJMensajesMQ485.' 
         GOTO CtrlErrores 
      END 

   COMMIT TRAN UCatTipoFila
   SET NOCOUNT OFF
   RETURN 0  

--******** CtrlAdvertenciaMQ ************
CtrlAdvertenciaMQ:
   SET NOCOUNT OFF 
 
   SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                         '@piTipoFilaID = [' + CONVERT(VARCHAR, ISNULL(@piTipoFilaID, '')) + ']' + 
                         '@pcDescripcionTP = [' + @pcDescripcionTP + ']' + 
                         '@pcUser = [' + @pcUser + '].'
  
   -- Asigna el mensaje MQ 
   SET @vcMensajeMQ =CONVERT(CHAR (70), @vcMensaje) 
 
   -- Envia respuesta MQ 
   EXEC @viReturn = SPINSCAJMensajesMQ485 @piProyectoMQ, @piFolioMQ, 
                     @vcParametrosMQ, 1,  
                     @vcMensajeMQ 

   IF (@@ERROR <> 0) OR (@viReturn < 0) 
   BEGIN 
      SET @vcMensaje= 'Error al generar la confirmacion de error MQ en SPINSCAJMensajesMQ485.' 
      GOTO CtrlErrores 
   END 

   RETURN 0 

--******** CtrlErrores ***********
CtrlErrores:
   SET NOCOUNT OFF
   SET @vcMensaje = 'Error al Ejecutar el SP -> PACJCCLUTRCatTipoFila. ' + ISNULL (@vcMensaje,'')
 
   SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                         '@piTipoFilaID = [' + CONVERT(VARCHAR, ISNULL(@piTipoFilaID, '')) + ']' + 
                         '@pcDescripcionTP = [' + @pcDescripcionTP + ']' + 
                         '@pcUser = [' + @pcUser + '].'
 
    -- Asigna el mensaje MQ 
   SET @vcMensajeMQ =CONVERT(CHAR(70), @vcMensaje) 
 
   -- Envia respuesta MQ 
   EXEC @viReturn = SPINSCAJMensajesMQ485 @piProyectoMQ, @piFolioMQ, 
                     @vcParametrosMQ, 0, 
                     @vcMensajeMQ 

   IF (@@ERROR <> 0) OR (@viReturn < 0) 
   BEGIN 
      SET @vcMensaje= 'Error al generar la confirmacion de error MQ en SPINSCAJMensajesMQ485.'
   END 

   -- Termina con error 
   RAISERROR (@vcMensaje, 18, 1) 
   RETURN -1
GO

EXEC DBO.SPGRANT 'DBO.PACJCCLUTRCatTipoFila'
GO

---~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~

IF OBJECT_ID('DBO.PACJCCLUTRCatUnidadesNegocio','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRCatUnidadesNegocio;
END
GO
---------------------------------------------------------------------------------------------------------      
---Responsable          : Roberto Gonzalez Figueroa
---Fecha                : Sep2013
---Descripcion          : Origen-Turnos; Mantenimiento al Catalogo de Unidade de Negocio
---------------------------------------------------------------------------------------------------------      
CREATE PROC dbo.PACJCCLUTRCatUnidadesNegocio (
   @piTienda             SMALLINT,
   @piProyectoMQ         INT,
   @piFolioMQ            INT,
   @piUndNegocioID       TINYINT,
   @pcDescripcionOrig    CHAR(50),
   @pcRutaImagen         CHAR(250),
   @piEstatus            TINYINT,
   @plPrestamos          BIT,
   @pcValorColor         CHAR(30),
   @pcValorZona          CHAR(30),
   @pcUser               CHAR(10)
)

AS

SET NOCOUNT ON

   DECLARE
      @vdActual SMALLDATETIME,
      @viTienda        SMALLINT,
      @viReturn        INTEGER,
      @vcMensaje       VARCHAR(255),
      @vcMensajeMQ     CHAR(70),
      @vcParametrosMQ  CHAR(200),
      @vcValorColor    CHAR(30),
      @vcValorZona     CHAR(30)

   SELECT @vdActual = GETDATE(), @vcValorColor = '#378266', @vcValorZona = ''

   IF (@pcValorColor IS NOT NULL) 
      SET @vcValorColor = @pcValorColor
	  
   IF (@pcValorZona IS NOT NULL) 
      SET @vcValorZona = @pcValorZona

   -- Asigna viTienda de la tabla CONTROL
   SELECT @viTienda = fiNoTienda FROM CONTROL WITH (NOLOCK) 

   -- Valida si Existe el folio MQ 
   IF EXISTS (SELECT fiFolio FROM CAJBITMensajesMQ WITH (NOLOCK) 
               WHERE fiProyecto = @piProyectoMQ AND fiFolio = @piFolioMQ)
   BEGIN 
      -- Se sale ya que este caso no debe pasar 
      SET NOCOUNT OFF 
      RETURN 0 
   END 

   -- Valida Tienda de tabla Control vs Parametro de Tienda
   IF (@piTienda <> @viTienda) 
   BEGIN 
      SET @vcMensaje = 'El valor de la Tienda ->' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + 
                       '<- NO corresponde a la sucursal.' 
      GOTO CtrlAdvertenciaMQ 
   END

   BEGIN TRAN UManttoUndNegocio
      -- Valida si Existe la Unidad de Negocio del Turnador
      IF NOT EXISTS (SELECT FIUNIDADNEGOCIOID FROM TCCJCCTRUNIDADNEGOCIO WITH (NOLOCK) 
                      WHERE FIUNIDADNEGOCIOID = @piUndNegocioID )
      BEGIN 
         -- Inserta la Nueva Unidad de Negocio de Turnador
         INSERT TCCJCCTRUNIDADNEGOCIO (FIUNIDADNEGOCIOID, FCDESCRIPCION, FCRUTAIMAGEN, FISTATUSUNDNEG, 
                                       FLPRESTAMOS, FCCOLOR, FCZONA, FDFECHAINSERTA, FCUSERINSERTA)
                 VALUES (@piUndNegocioID, @pcDescripcionOrig, @pcRutaImagen, @piEstatus, @plPrestamos,
				         @vcValorColor, @vcValorZona, @vdActual, @pcUser)

         IF ( @@ERROR <> 0 )
         BEGIN 
	        ROLLBACK TRAN UManttoUndNegocio
            SET @vcMensaje= 'Error al insertar en tabla TCCJCCTRUNIDADNEGOCIO.' 
            GOTO CtrlErrores 
         END
      END
      ELSE
      BEGIN 
         -- Actualiza
         UPDATE TCCJCCTRUNIDADNEGOCIO
            SET FCDESCRIPCION = @pcDescripcionOrig
               ,FCRUTAIMAGEN = @pcRutaImagen
               ,FISTATUSUNDNEG = @piEstatus
               ,FLPRESTAMOS = @plPrestamos
		       ,FCCOLOR = @vcValorColor
               ,FCZONA = @vcValorZona
               ,FDFECHAMODIF = @vdActual
               ,FCUSERMODIF = @pcUser
          WHERE FIUNIDADNEGOCIOID = @piUndNegocioID 

         IF ( @@ERROR <> 0 )
         BEGIN 
	        ROLLBACK TRAN UManttoUndNegocio
            SET @vcMensaje= 'Error al actualizar ->Nombre<- en tabla TCCJCCTRUNIDADNEGOCIO.' 
            GOTO CtrlErrores 
         END
      END

      --***************************************
      --********* Arma el mensaje para MQ *****
      SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                            '@piUndNegocioID = [' + CONVERT(VARCHAR, ISNULL(@piUndNegocioID, '')) + ']' + 
                            '@pcDescripcionOrig = [' + @pcDescripcionOrig + ']' + 
                            '@pcRutaImagen = [' + @pcRutaImagen + ']' + 
                            '@piEstatus = [' + CONVERT(VARCHAR, ISNULL(@piEstatus, '')) + ']' + 
                            '@plPrestamos = [' + CONVERT(VARCHAR, CONVERT(INT,@plPrestamos)) + ']' + 
                            '@pcColor = [' + @vcValorColor + ']' + 
                            '@pcZona = [' + @vcValorZona + ']' + 
                            '@pcUser = [' + @pcUser + '].'

      -- Genera el mensaje de confirmacion de MQ 
      EXEC @viReturn = SPINSCAJMensajesMQ485 @piProyectoMQ, @piFolioMQ, @vcParametrosMQ, 0, ' ' 

      -- Si existe error 
      IF (@@ERROR <> 0) OR (@viReturn < 0) 
      BEGIN 
         ROLLBACK TRAN UManttoUndNegocio
         SET @vcMensaje= 'Error al generar la confirmacion de MQ en SPINSCAJMensajesMQ485.' 
         GOTO CtrlErrores 
      END 

   COMMIT TRAN UManttoUndNegocio

   SET NOCOUNT OFF
   RETURN 0  

--******** CtrlAdvertenciaMQ ************
CtrlAdvertenciaMQ:
   SET NOCOUNT OFF 
 
   SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                         '@piUndNegocioID = [' + CONVERT(VARCHAR, ISNULL(@piUndNegocioID, '')) + ']' + 
                         '@pcDescripcionOrig = [' + @pcDescripcionOrig + ']' + 
                         '@pcRutaImagen = [' + @pcRutaImagen + ']' + 
                         '@piEstatus = [' + CONVERT(VARCHAR, ISNULL(@piEstatus, '')) + ']' + 
                         '@plPrestamos = [' + CONVERT(VARCHAR, CONVERT(INT,@plPrestamos)) + ']' + 
                         '@pcColor = [' + @vcValorColor + ']' + 
                         '@pcZona = [' + @vcValorZona + ']' + 
                         '@pcUser = [' + @pcUser + '].'
  
   -- Asigna el mensaje MQ 
   SET @vcMensajeMQ =CONVERT(CHAR (70), @vcMensaje) 
 
   -- Envia respuesta MQ 
   EXEC @viReturn = SPINSCAJMensajesMQ485 @piProyectoMQ, @piFolioMQ, 
                     @vcParametrosMQ, 1,  
                     @vcMensajeMQ 

   IF (@@ERROR <> 0) OR (@viReturn < 0) 
   BEGIN 
      SET @vcMensaje= 'Error al generar la confirmacion de error MQ en SPINSCAJMensajesMQ485.' 
      GOTO CtrlErrores 
   END 

   RETURN 0 

--******** CtrlErrores ***********
CtrlErrores:
   SET NOCOUNT OFF
   SET @vcMensaje = 'Error al Ejecutar el SP -> PACJCCLUTRCatUnidadesNegocio. ' + ISNULL (@vcMensaje,'')
 
   SET @vcParametrosMQ = '@piTienda = [' + CONVERT(VARCHAR, ISNULL(@piTienda, '')) + ']' + 
                         '@piUndNegocioID = [' + CONVERT(VARCHAR, ISNULL(@piUndNegocioID, '')) + ']' + 
                         '@pcDescripcionOrig = [' + @pcDescripcionOrig + ']' + 
                         '@pcRutaImagen = [' + @pcRutaImagen + ']' + 
                         '@piEstatus = [' + CONVERT(VARCHAR, ISNULL(@piEstatus, '')) + ']' + 
                         '@plPrestamos = [' + CONVERT(VARCHAR, CONVERT(INT,@plPrestamos)) + ']' + 
                         '@pcColor = [' + @vcValorColor + ']' + 
                         '@pcZona = [' + @vcValorZona + ']' + 
                         '@pcUser = [' + @pcUser + '].'
 
    -- Asigna el mensaje MQ 
   SET @vcMensajeMQ =CONVERT(CHAR(70), @vcMensaje) 
 
   -- Envia respuesta MQ 
   EXEC @viReturn = SPINSCAJMensajesMQ485 @piProyectoMQ, @piFolioMQ, 
                     @vcParametrosMQ, 0, 
                     @vcMensajeMQ 

   IF (@@ERROR <> 0) OR (@viReturn < 0) 
   BEGIN 
      SET @vcMensaje= 'Error al generar la confirmacion de error MQ en SPINSCAJMensajesMQ485.'
   END 

   -- Termina con error 
   RAISERROR (@vcMensaje, 18, 1) 
   RETURN -1
GO

EXEC DBO.SPGRANT 'DBO.PACJCCLUTRCatUnidadesNegocio'
GO

---~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~


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
   @pcEmpNo              CHAR(10),
   @pcUser               CHAR(10),
   @piEstatus            SMALLINT,
   @piCualidad           SMALLINT,
   @pcValor              CHAR(250)
)

AS

SET NOCOUNT ON

   DECLARE
      @vcEmpno         CHAR(10),
      @vdFecHoy        DATETIME,
      @vcMensaje       VARCHAR(255),
	  @viEMPSTATUS     INT,
	  @vcSINVALOR      CHAR(6)

   DECLARE @vtEmpleados TABLE (FCEMPNO CHAR(10), FIDEPTOID SMALLINT)

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

---~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~

IF OBJECT_ID('DBO.PACJCCLUTRPoolUnidad','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRPoolUnidad;
END
GO
----------------------------------------------------------------------------------------------------------      
---Responsable          : Esteban Jesus Caro Guzman y Jorge Enrique Gamboa Fuentes.  
---Fecha                : Enero2014  
---Descripcion          : Turnos-Sucursales; Actualizacion del Pool de Unidades de Negocio
----------------------------------------------------------------------------------------------------------      
CREATE PROCEDURE dbo.PACJCCLUTRPoolUnidad (
   @pcEmpNo              CHAR(10),
   @piUnidadNeg          INTEGER,
   @pcUser               CHAR(10)
)

AS

SET NOCOUNT ON

   DECLARE
     
      @vdFecHoy        DATETIME,
      @vcMensaje       VARCHAR(255)
	 

   SELECT @vdFecHoy = GETDATE()--, @vcEmpno = LTRIM(RTRIM(@pcEmpNo))

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

IF OBJECT_ID('DBO.PACJCCLUTRPuntoAtencion','P') IS NOT NULL
BEGIN
	DROP PROC DBO.PACJCCLUTRPuntoAtencion;
END
GO
---------------------------------------------------------------------------------------------------------      
---Responsable          : Esteban Jesus Caro Guzman
---Fecha                : Feb2014
---Descripcion          : Turnos-Sucursales; Modificacion del punto de atencion de los empleados
---------------------------------------------------------------------------------------------------------      
CREATE PROC dbo.PACJCCLUTRPuntoAtencion (
      @pcEmpNo                  CHAR(10),
      @pcPuntoAtencion          VARCHAR(50))
AS

DECLARE

@vcMensaje  VARCHAR(255)  

SET NOCOUNT ON

   BEGIN TRAN SPuntoAtencion
   
   IF EXISTS (SELECT FCEMPNOID FROM TACJCCTRPOOLATENCION WITH(NOLOCK) WHERE FCEMPNOID = @pcEmpNo) 
   BEGIN
		 UPDATE TACJCCTRPOOLATENCION
            SET FCPUNTOATENCION = @pcpuntoatencion
          WHERE FCEMPNOID = @pcEmpNo
          
          IF @@ERROR<>0
          BEGIN
          SET @vcMensaje= 'Error al modificar la tabla TACJCCTRPOOLATENCION '
          GOTO CtrlErrores
          END
   END
   ELSE
   BEGIN
          SET @vcMensaje= 'El empleado no existe en la tabla TACJCCTRPOOLATENCION '
          GOTO CtrlErrores
   END
   
   COMMIT TRAN SPuntoAtencion
   SET NOCOUNT OFF
   RETURN 0  

--******** CtrlErrores ***********
CtrlErrores:
   ROLLBACK TRAN SPuntoAtencion	
   SET NOCOUNT OFF
   
   RAISERROR (@vcMensaje, 18, 1) 
   RETURN -1
GO

EXEC DBO.SPGRANT 'DBO.PACJCCLUTRPuntoAtencion'
GO
