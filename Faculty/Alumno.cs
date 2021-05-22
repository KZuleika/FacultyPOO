using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faculty
{
    class Alumno
    {
        public int Matricula { get; }
        public string Nombre { get; }
        public string Apellido { get; }

        public string NombreCompleto => $"{Apellido}, {Nombre}";

        public Alumno(int matricula, string nombre, string apellido)
        {
            Matricula = matricula;
            Nombre = nombre;
            Apellido = apellido;
        }

    }
}
