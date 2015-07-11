using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

using Common.Logging;

using Baz.Caja.Commons.Collection;
using Baz.Caja.Commons.Dao;
using Baz.Caja.Commons.Model;
using Baz.Caja.Commons.Util;
using Baz.Caja.Turnador.Dao.Mapper;
using Baz.Caja.Turnador.Dao.Statement;
using Baz.Caja.Turnador.Model;

namespace Baz.Caja.Turnador.Dao
{
    public class CjCRNegocioDao : CjCRDaoBase
    {
        private static ILog Log = LogManager.GetCurrentClassLogger();

        public CjCRUnidadNegocioMapper UnidadNegocioMapper { get; set; }
        public CjCRNegocioStatementBuilder NegocioStatementBuilder { get; set; }

        public CjCRUnidadNegocio GetUnidadNegocio(int idUnidadNegocio, CjCRCredential credential)
        {
            CjCRUnidadNegocio unidadNegocio = default(CjCRUnidadNegocio);

            try
            {
                unidadNegocio = GetUnidadNegocioExecute(idUnidadNegocio, credential);
            }
            catch (Exception ex)
            {
                Log.Error(CjCRPropertyUtils.GetFormat("sentence.execution.error", ex.Message));
            }

            return unidadNegocio;
        }

        public CjCRDictionary<Int32, CjCRUnidadNegocio> GetUnidadesNegocio(CjCRCredential credential)
        {
            CjCRDictionary<Int32, CjCRUnidadNegocio> unidadesNegocio = new CjCRDictionary<Int32, CjCRUnidadNegocio>();

            try
            {
                unidadesNegocio = GetUnidadesNegocioExecute(credential);
            }
            catch (Exception ex)
            {
                Log.Error(CjCRPropertyUtils.GetFormat("sentence.execution.error", ex.Message));
            }

            return unidadesNegocio;
        }

        private CjCRUnidadNegocio GetUnidadNegocioExecute(int idUnidadNegocio, CjCRCredential credential)
        {
            Autenticate(credential);

            return AdoTemplate.Execute<CjCRUnidadNegocio>(delegate(DbCommand command)
            {
                CjCRUnidadNegocio unidadNegocio = default(CjCRUnidadNegocio);
                DbDataReader reader;

                command.CommandText = NegocioStatementBuilder.GetUnidadNegocioStatement();

                CjCRDaoUtils.AddParam(command, "@fiunidadnegocioid", idUnidadNegocio);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    unidadNegocio = UnidadNegocioMapper.Get(reader,
                        CjCRUnidadNegocioMapper.BASIC_MODEL);
                }

                return unidadNegocio;
            });
        }

        private CjCRDictionary<Int32, CjCRUnidadNegocio> GetUnidadesNegocioExecute(CjCRCredential credential) 
        {
            Autenticate(credential);

            return AdoTemplate.Execute<CjCRDictionary<Int32, CjCRUnidadNegocio>>(delegate(DbCommand command)
            {
                CjCRDictionary<Int32, CjCRUnidadNegocio> unidadesNegocio = new CjCRDictionary<Int32, CjCRUnidadNegocio>();
                CjCRUnidadNegocio unidadNegocio;
                DbDataReader reader;

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PACJCCLSTRUnidadesNegocio";

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    unidadNegocio = UnidadNegocioMapper.Get(reader, 
                        CjCRUnidadNegocioMapper.BASIC_MODEL);

                    unidadesNegocio.Put(unidadNegocio.IdUnidadNegocio, unidadNegocio);
                }

                return unidadesNegocio;
            });
        }
    }
}
