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
      @vcUSUARIO        CHAR(6),
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
               EXEC PACJCCLUTRHistorico @viFECHOYCORTA, @viTURNOID, @viStatusCaduco, @vcUSUARIO

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