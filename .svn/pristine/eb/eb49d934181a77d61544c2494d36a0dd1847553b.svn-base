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
      @vcUSUARIO         CHAR(6),
      @viTURNOID         INTEGER,
      @viUNDNEGOCIO      SMALLINT,
      @vcEMPNOID         CHAR(6),
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
            SELECT @viFIFECHA=FIFECHA, @viTURNOID=FITURNOID, @viFIUNIDADNEGOCIO=FIUNIDADNEGOCIO
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
               INSERT INTO TACJCCTRHISTORICO (FIFECHA, FITURNOID, FISTATUSTURNOID, FDACTUALIZACION, 
                                              FDFECHAINSERTA, FCUSERINSERTA)
                    VALUES (@viFIFECHA, @viTURNOID, @viValorDos, @vdFECHAHOY2, @vdFECHAHOY2, @vcUSUARIO)

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