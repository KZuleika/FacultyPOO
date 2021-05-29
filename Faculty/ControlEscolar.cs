using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Faculty
{
    class ControlEscolar
    {
        private List<Alumno> alumnos;
        private List<Materia> materias;
        private List<Calificacion> calificaciones;
       
        public ControlEscolar()
        {
            alumnos = EasyFile<Alumno>.LoadDataFromFile("alumnos.txt",
                tokens => new Alumno(Convert.ToInt32(tokens[0]), tokens[1], tokens[2]));
            materias = EasyFile<Materia>.LoadDataFromFile("materias.txt", 
                tokens => new Materia(Convert.ToInt32(tokens[0]), tokens[1], Convert.ToInt32(tokens[2])));
            calificaciones = EasyFile<Calificacion>.LoadDataFromFile("calificaciones.txt",
               tokens => new Calificacion(Convert.ToInt32(tokens[0]), Convert.ToInt32(tokens[1]), Convert.ToInt32(tokens[2])));
        }

        public List<Alumno> GetAlumnos()
        {
            List<Alumno> alumnos = new List<Alumno>(this.alumnos);
            alumnos.Sort((a1, a2) => a1.Matricula.CompareTo(a2.Matricula));
            return alumnos;
        }

        public List<Materia> GetMaterias()
        {
            List<Materia> materias = new List<Materia>(this.materias);
            materias.Sort((m1, m2) => m1.Clave.CompareTo(m2.Clave));
            return materias;
        }

        public bool ValidarMatricula(int matricula) =>
            alumnos.Exists(a => a.Matricula == matricula);

        public void NuevoAlumno(int matricula, string nombre, string apellido)
        {
            alumnos.Add(new Alumno(matricula, nombre, apellido));
            EasyFile<Alumno>.SaveDataToFile("alumnos.txt",
                                                new string[]{"Matricula","Nombre","Apellido"},
                                                alumnos);
            materias.ForEach(m =>
            {
                calificaciones.Add(new Calificacion(matricula, m.Clave, -1));
                EasyFile<Calificacion>.SaveDataToFile("calificaciones.txt",
                                                new []{"MatriculaAl","ClaveMat","CalificacionObtenida"},
                                                calificaciones);
            });
        }

        public bool ValidarClave(int clave) =>
            materias.Exists(m => m.Clave == clave);

        public int EstatusMateria(int matricula, int clave)
        {
            int calificacion = calificaciones.Find(c => (c.ClaveMat == clave && c.MatriculaAl == matricula)).CalificacionObtenida;
            if (calificacion >= 70) return 1; //aprobado
            if (calificacion >= 0) return 0; //reprobado
            return -1; //no cursada
        }



        public void AsignarCalificacion(int matricula, int clave, int calificacion)
        {
            calificaciones.Find(c => (c.ClaveMat == clave && c.MatriculaAl == matricula)).CalificacionObtenida = calificacion;
            EasyFile<Calificacion>.SaveDataToFile("calificaciones.txt",
                                                new []{"MatriculaAl","ClaveMat","CalificacionObtenida"},
                                                calificaciones);
        }

        public List<Reporte> GetPromedio()
        {
            List<Reporte> reportes=new List<Reporte>();
            alumnos.ForEach(a =>
            {
                reportes.Add(new Reporte(a,calificaciones.FindAll(c=>c.MatriculaAl==a.Matricula && c.CalificacionObtenida >= 0)));
            });

            reportes.RemoveAll(r => r.Calificaciones.Count < 1);

           
            reportes.Sort((r1, r2) => r1.Alumno.Matricula.CompareTo(r2.Alumno.Matricula));
            return reportes;
        }
      
        public List<Reporte> GetPromedioParcial()
        {
            List<Reporte> reportes = new List<Reporte>();
            alumnos.ForEach(a =>
            {
                reportes.Add(new Reporte(a, calificaciones.FindAll(c => c.MatriculaAl == a.Matricula && c.CalificacionObtenida>=70))) ;
            });

            reportes.RemoveAll(r => r.Calificaciones.Count < 1);
            reportes.Sort((r1, r2) => r1.Alumno.Matricula.CompareTo(r2.Alumno.Matricula));

            return reportes;
        }

        public List<Reporte> GetReprobados()
        {
            List<Reporte> reprobados = new List<Reporte>();
            alumnos.ForEach(a =>
                reprobados.Add(new Reporte(a, calificaciones.FindAll(c => 
                    c.MatriculaAl == a.Matricula && EstatusMateria(a.Matricula, c.ClaveMat)==0)))
            );

            reprobados.RemoveAll(r => r.Calificaciones.Count < 1);

            reprobados.ForEach(r => 
                r.Calificaciones.ForEach(c => 
                    r.Materias.Add(materias.Find(m => m.Clave == c.ClaveMat))));
            reprobados.Sort((r1, r2) => r1.Alumno.Matricula.CompareTo(r2.Alumno.Matricula));

            return reprobados;
        }

        public int NumeroReprobados(Materia materia)
        {
            int nreprobados = 0;
            calificaciones.FindAll(c => c.ClaveMat == materia.Clave).ForEach(c =>
            {
                if (EstatusMateria(c.MatriculaAl, c.ClaveMat) == 0) nreprobados++;
            });
            return nreprobados;
        }

        public List<Materia> GetExtraordinarios()
        {
            List<Materia> extraordinarios = new List<Materia>();

            materias.ForEach(m =>
            {
                if (calificaciones.Exists(c => c.ClaveMat == m.Clave && EstatusMateria(c.MatriculaAl, c.ClaveMat) == 0))
                    extraordinarios.Add(m);
            });

            return extraordinarios;
        }
    }
}
