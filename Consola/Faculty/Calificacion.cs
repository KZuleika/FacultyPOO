using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faculty
{
    class Calificacion
    {
        public int MatriculaAl { get; }
        public int ClaveMat { get; }
        public int CalificacionObtenida { get; set; }

        public Calificacion(int matricula, int clave, int calificacion)
        {
            MatriculaAl = matricula;
            ClaveMat = clave;
            CalificacionObtenida = calificacion;
        }
    }
}
