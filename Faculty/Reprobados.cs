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
        public List<Materia> Materias { get; }

        public Reprobados(Alumno alumno, List<Calificacion> calificaciones)
        {
            Alumno = alumno;
            Calificaciones = calificaciones;
            Materias = new List<Materia> ();
        }

        public float CalcularPromedio()
        {
            int promedio = 0;

            if (Calificaciones.Count > 0)
            {
                Calificaciones.ForEach(c => promedio += c.CalificacionObtenida);
                promedio /= Calificaciones.Count;
            }

            return promedio;
        }
    }
}
