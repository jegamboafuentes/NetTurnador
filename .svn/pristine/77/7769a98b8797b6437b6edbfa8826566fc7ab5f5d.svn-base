using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Baz.Caja.Commons.Util;
using Baz.Caja.Turnador.Model;

namespace Baz.Caja.Turnador.Util
{
    public class CjCRCualidadList : List<CjCRCualidad>
    {
        public const int CUALIDAD_NEGOCIO = 1;

        private int IdUnidadNegocio;
        private int IdCualidad;
        private String Valor;

        public void Add(int idCualidad, String valor)
        {
            Add(idCualidad, valor, true);
        }

        public void Add(int idCualidad, String valor, bool activa)
        {
            CjCRCualidad cualidad = new CjCRCualidad();

            cualidad.IdCualidad = idCualidad;
            cualidad.Valor = valor;
            cualidad.Activa = activa;

            Add(cualidad);
        }

        new public void Add(CjCRCualidad cualidad)
        {   
            base.Add(cualidad);
            
            SetValues(cualidad);
        }

        public CjCRCualidad GetCualidad(int idCualidad)
        {
            return Find(x => x.IdCualidad == idCualidad);
        }

        public void Remove(int idCualidad)
        {
            int index = FindIndex(x => x.IdCualidad == idCualidad);

            if (index > -1)
            {
                RemoveAt(index);
            }
        }

        public int GetIdUnidadNegocio()
        {
            return IdUnidadNegocio;
        }

        public void SetIdUnidadNegocio(int idUnidadNegocio)
        {
            this.IdUnidadNegocio = idUnidadNegocio;

            IdCualidad = CUALIDAD_NEGOCIO;
            Valor = idUnidadNegocio.ToString();
        }

        public int GetIdCualidad()
        {
            return IdCualidad;
        }

        public String GetValor()
        {
            return Valor;
        }

        private void SetValues(CjCRCualidad cualidad)
        {
            if (cualidad.Activa)
            {
                Desactivar();

                if (cualidad.IdCualidad == CUALIDAD_NEGOCIO)
                {
                    IdUnidadNegocio = CjCRParseUtils.ToInt32(cualidad.Valor, -1);
                    IdCualidad = cualidad.IdCualidad;
                    Valor = cualidad.Valor;
                }
            }
        }

        private void Desactivar()
        {
            foreach (CjCRCualidad cualidad in this)
            {
                cualidad.Activa = false;
            }
        }

        public string ValidarFecha(String fecha)
        {
            if (fecha != "0")
            {
                return fecha;
            }
            else
            {
                string day = DateTime.Now.Day.ToString();
                if (Int32.Parse(day) < 10) { day = "0" + day; }
                string mont = DateTime.Now.Month.ToString();
                if (Int32.Parse(mont) < 10) { mont = "0" + mont; }
                string year = DateTime.Now.Year.ToString();
                fecha = year + mont + day;
                return  fecha;
            }
        }

    }
}