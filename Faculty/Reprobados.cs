using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faculty
{
    class Reprobados
    {
        public Alumno Alumno { get; }
        public List<Calificacion> Calificaciones { get; }
        public List<Materia> MateriasReprobadas { get; }

        public Reprobados(Alumno alumno, List<Calificacion> calificaciones)
        {
            Alumno = alumno;
            Calificaciones = calificaciones;
            MateriasReprobadas = MateriasReprobadas;
        }
    }
}
