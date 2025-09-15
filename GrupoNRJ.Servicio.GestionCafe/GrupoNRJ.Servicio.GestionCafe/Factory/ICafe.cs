using Microsoft.Data.SqlClient;
using System.Data;

namespace GrupoNRJ.Servicio.GestionCafe.Factory
{



    //Archivo utilizado para la creacion de nuevos cafes
    public interface ICafe
    {
        void creaLote(decimal cantidad);
    }
    //arabica, robusta, blends 
    //claro, medio, oscuro
    public class cafeArabicaClaro : ICafe
    {

        private readonly EjecutarSP _sp;

        public cafeArabicaClaro(EjecutarSP sp)
        {
            this._sp = sp;
        }

        public void creaLote(decimal cantidad)
        {
            var parametros = new Dictionary<string, object>
        {
            { "@P_CANTIDAD", cantidad },
            { "@P_GRANO", "ARÁBICA" },
            { "@P_TUESTE", "CLARO" }
        };

            bool exito = _sp.ExecuteNonQuery("SP_I_NUEVO_LOTE", parametros);

            if (!exito)
                throw new Exception("Fallo al ejecutar el procedimiento almacenado.");
        }




    }

    public class cafeArabicaMedio : ICafe
    {

        private readonly EjecutarSP _sp;

        public cafeArabicaMedio(EjecutarSP sp)
        {
            this._sp = sp;
        }

        public void creaLote(decimal cantidad)
        {
            var parametros = new Dictionary<string, object>
        {
            { "@P_CANTIDAD", cantidad },
            { "@P_GRANO", "ARÁBICA" },
            { "@P_TUESTE", "MEDIO" }
        };

            bool exito = _sp.ExecuteNonQuery("SP_I_NUEVO_LOTE", parametros);

            if (!exito)
                throw new Exception("Fallo al ejecutar el procedimiento almacenado.");
        }

    }

    public class cafeArabicaOscuro : ICafe
    {

        private readonly EjecutarSP _sp;

        public cafeArabicaOscuro(EjecutarSP sp)
        {
            this._sp = sp;
        }

        public void creaLote(decimal cantidad)
        {
            var parametros = new Dictionary<string, object>
            {
                { "@P_CANTIDAD", cantidad },
                { "@P_GRANO", "ARÁBICA" },
                { "@P_TUESTE", "OSCURO" }
            };

            bool exito = _sp.ExecuteNonQuery("SP_I_NUEVO_LOTE", parametros);

            if (!exito)
                throw new Exception("Fallo al ejecutar el procedimiento almacenado.");
        }

    }


    public class cafeRobustaClaro : ICafe
    {

        private readonly EjecutarSP _sp;

        public cafeRobustaClaro(EjecutarSP sp)
        {
            this._sp = sp;
        }

        public void creaLote(decimal cantidad)
        {
            var parametros = new Dictionary<string, object>
            {
                { "@P_CANTIDAD", cantidad },
                { "@P_GRANO", "ROBUSTA" },
                { "@P_TUESTE", "CLARO" }
            };

            bool exito = _sp.ExecuteNonQuery("SP_I_NUEVO_LOTE", parametros);

            if (!exito)
                throw new Exception("Fallo al ejecutar el procedimiento almacenado.");
        }

    }

    public class cafeRobustaMedio : ICafe
    {

        private readonly EjecutarSP _sp;

        public cafeRobustaMedio(EjecutarSP sp)
        {
            this._sp = sp;
        }

        public void creaLote(decimal cantidad)
        {
            var parametros = new Dictionary<string, object>
            {
                { "@P_CANTIDAD", cantidad },
                { "@P_GRANO", "ROBUSTA" },
                { "@P_TUESTE", "MEDIO" }
            };

            bool exito = _sp.ExecuteNonQuery("SP_I_NUEVO_LOTE", parametros);

            if (!exito)
                throw new Exception("Fallo al ejecutar el procedimiento almacenado.");
        }

    }

    public class cafeRobustaOscuro : ICafe
    {

        private readonly EjecutarSP _sp;

        public cafeRobustaOscuro(EjecutarSP sp)
        {
            this._sp = sp;
        }

        public void creaLote(decimal cantidad)
        {
            var parametros = new Dictionary<string, object>
            {
                { "@P_CANTIDAD", cantidad },
                { "@P_GRANO", "ROBUSTA" },
                { "@P_TUESTE", "OSCURO" }
            };

            bool exito = _sp.ExecuteNonQuery("SP_I_NUEVO_LOTE", parametros);

            if (!exito)
                throw new Exception("Fallo al ejecutar el procedimiento almacenado.");
        }

    }


    public class cafeBlendsClaro : ICafe
    {

        private readonly EjecutarSP _sp;

        public cafeBlendsClaro(EjecutarSP sp)
        {
            this._sp = sp;
        }

        public void creaLote(decimal cantidad)
        {
            var parametros = new Dictionary<string, object>
            {
                { "@P_CANTIDAD", cantidad },
                { "@P_GRANO", "BLENDS" },
                { "@P_TUESTE", "CLARO" }
            };

            bool exito = _sp.ExecuteNonQuery("SP_I_NUEVO_LOTE", parametros);

            if (!exito)
                throw new Exception("Fallo al ejecutar el procedimiento almacenado.");
        }

    }

    public class cafeBlendsMedio : ICafe
    {

        private readonly EjecutarSP _sp;

        public cafeBlendsMedio(EjecutarSP sp)
        {
            this._sp = sp;
        }

        public void creaLote(decimal cantidad)
        {
            var parametros = new Dictionary<string, object>
            {
                { "@P_CANTIDAD", cantidad },
                { "@P_GRANO", "BLENDS" },
                { "@P_TUESTE", "MEDIO" }
            };

            bool exito = _sp.ExecuteNonQuery("SP_I_NUEVO_LOTE", parametros);

            if (!exito)
                throw new Exception("Fallo al ejecutar el procedimiento almacenado.");
        }

    }

    public class cafeBlendsOscuro : ICafe
    {

        private readonly EjecutarSP _sp;

        public cafeBlendsOscuro(EjecutarSP sp)
        {
            this._sp = sp;
        }

        public void creaLote(decimal cantidad)
        {
            var parametros = new Dictionary<string, object>
            {
                { "@P_CANTIDAD", cantidad },
                { "@P_GRANO", "BLENDS" },
                { "@P_TUESTE", "OSCURO" }
            };

            bool exito = _sp.ExecuteNonQuery("SP_I_NUEVO_LOTE", parametros);

            if (!exito)
                throw new Exception("Fallo al ejecutar el procedimiento almacenado.");
        }

    }
}
