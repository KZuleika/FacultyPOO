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
        public List<Calificacion> Calificaciones { get; }
        public float Promedio { get; set; }
        
        public Reporte(Alumno alumno, List<Calificacion> calificaciones)
        {
            Alumno = alumno;
            Calificaciones = calificaciones;
            Promedio = -1;
        }

    }
}
