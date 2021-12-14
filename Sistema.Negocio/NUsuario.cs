﻿using Sistema.Datos;
using Sistema.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Negocio
{
   public class NUsuario
    {
        public static DataTable Listar()
        {
            DUsuario Datos = new DUsuario();
            return Datos.Listar();
        }
        public static DataTable Buscar(string valor)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Buscar(valor);
        }

        public static string Insertar(int IdRol, string Nombre, string TipoDocumento, string NumDocumento, string Direccion, string Telefono, string Email, string Clave)
        {
            DUsuario Datos = new DUsuario();

            string Existe = Datos.Existe(Email);
            if (Existe.Equals("1"))
            {
                return "El usuario con este email ya existe.";
            }
            else
            {
                Usuario Obj = new Usuario();
                Obj.IdRol = IdRol;
                Obj.Nombre = Nombre;
                Obj.TipoDocumento = TipoDocumento;
                Obj.NumDocumento = NumDocumento;
                Obj.Direccion = Direccion;
                Obj.Telefono = Telefono;
                Obj.Email = Email;
                Obj.Clave = Clave;
                return Datos.Insertar(Obj);
            }


        }
        public static string Actualizar(int Id, int IdRol, string Nombre, string TipoDocumento, string NumDocumento, string Direccion, string Telefono, string EmailAnterior,string Email, string Clave)
        {
            DUsuario Datos = new DUsuario();
            Usuario Obj = new Usuario();

            if (EmailAnterior.Equals(Email))
            {
                Obj.IdUsuario = Id;
                Obj.IdRol = IdRol;
                Obj.Nombre = Nombre;
                Obj.TipoDocumento = TipoDocumento;
                Obj.NumDocumento = NumDocumento;
                Obj.Direccion = Direccion;
                Obj.Telefono = Telefono;
                Obj.Email = Email;
                Obj.Clave = Clave;
                return Datos.Actualizar(Obj);
            }
            else
            {
                string Existe = Datos.Existe(Email);
                if (Existe.Equals("1"))
                {
                    return "El usuario con este email ya existe.";
                }
                else
                {
                    Obj.IdUsuario = Id;
                    Obj.IdRol = IdRol;
                    Obj.Nombre = Nombre;
                    Obj.TipoDocumento = TipoDocumento;
                    Obj.NumDocumento = NumDocumento;
                    Obj.Direccion = Direccion;
                    Obj.Telefono = Telefono;
                    Obj.Email = Email;
                    Obj.Clave = Clave;
                    return Datos.Actualizar(Obj);
                }
            }

        }
        public static string Eliminar(int Id)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Eliminar(Id);
        }
        public static string Activar(int Id)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Activar(Id);
        }
        public static string Desactivar(int Id)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Desactivar(Id);
        }

    }
}
