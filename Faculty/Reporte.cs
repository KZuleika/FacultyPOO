using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faculty
{
    class Reporte
    {
        public Alumno Alumno { get; }
        public Materia Materia { get; }
        public Calificacion Calificacion { get; }

        public Reporte(Alumno alumno, Materia materia, Calificacion calificacion)
        {
            Alumno = alumno;
            Materia = materia;
            Calificacion = calificacion;
        }
    }
}
