using Sistema.Datos;
using Sistema.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Negocio
{
    public class NArticulo
    {

        public static DataTable Listar()
        {
            DArticulo Datos = new DArticulo();
            return Datos.Listar();
        }
        public static DataTable Buscar(string valor)
        {
            DArticulo Datos = new DArticulo();
            return Datos.Buscar(valor);
        }

        public static string Insertar(int IdCategoria, string Codigo, string Nombre, decimal PrecioVenta, int Stock, string Descripcion, string Imagen)
        {
            DArticulo Datos = new DArticulo();

            string Existe = Datos.Existe(Nombre);
            if (Existe.Equals("1"))
            {
                return "El articulo ya existe";
            }
            else
            {
                Articulo Obj = new Articulo();
                Obj.IdCategoria = IdCategoria;
                Obj.Codigo = Codigo;
                Obj.Nombre = Nombre;
                Obj.PrecioVenta = PrecioVenta;
                Obj.Stock = Stock;
                Obj.Descripcion = Descripcion;
                Obj.Imagen = Imagen;

                return Datos.Insertar(Obj);
            }


        }
        public static string Actualizar(int Id, int IdCategoria, string Codigo, string NombreAnterior, string Nombre, decimal PrecioVenta, int Stock, string Descripcion, string Imagen)
        {
            DArticulo Datos = new DArticulo();
            Articulo Obj = new Articulo();

            if (NombreAnterior.Equals(Nombre))
            {
                Obj.IdArticulo = Id;
                Obj.IdCategoria = IdCategoria;
                Obj.Codigo = Codigo;
                Obj.Nombre = Nombre;
                Obj.PrecioVenta = PrecioVenta;
                Obj.Stock = Stock;
                Obj.Descripcion = Descripcion;
                Obj.Imagen = Imagen;
                return Datos.Actualizar(Obj);
            }
            else
            {
                string Existe = Datos.Existe(Nombre);
                if (Existe.Equals("1"))
                {
                    return "El articulo ya existe";
                }
                else
                {
                    Obj.IdArticulo = Id;
                    Obj.IdCategoria = IdCategoria;
                    Obj.Codigo = Codigo;
                    Obj.Nombre = Nombre;
                    Obj.PrecioVenta = PrecioVenta;
                    Obj.Stock = Stock;
                    Obj.Descripcion = Descripcion;
                    Obj.Imagen = Imagen;
                    return Datos.Actualizar(Obj);
                }
            }

        }
        public static string Eliminar(int Id)
        {
            DArticulo Datos = new DArticulo();
            return Datos.Eliminar(Id);
        }
        public static string Activar(int Id)
        {
            DArticulo Datos = new DArticulo();
            return Datos.Activar(Id);
        }
        public static string Desactivar(int Id)
        {
            DArticulo Datos = new DArticulo();
            return Datos.Desactivar(Id);
        }

    }
}
