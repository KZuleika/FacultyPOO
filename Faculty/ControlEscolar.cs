﻿using System;
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

        public bool EstatusMateria(int matricula, int clave)
        {
            int calificacion = calificaciones.Find(c => (c.ClaveMat == clave && c.MatriculaAl == matricula)).CalificacionObtenida;
            if (calificacion >= 70) return true;
            return false;
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

            reportes.ForEach(r =>
            {
                int i = 0;
                r.Calificaciones.ForEach(c =>
                {
                    if (c.CalificacionObtenida >= 0) {
                        i += c.CalificacionObtenida;
                    }
                });
                r.Promedio = i / r.Calificaciones.Count;

            });
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

            reportes.ForEach(r =>
            {
                int i = 0;
                r.Calificaciones.ForEach(c =>
                {
                    if (c.CalificacionObtenida >= 70) {
                        i += c.CalificacionObtenida;
                    }
                });
                r.Promedio = i / r.Calificaciones.Count;
            });
            return reportes;
        }

        public List<Reprobados> GetReprobados()
        {
            List<Reprobados> reprobado = new List<Reprobados>();
            alumnos.ForEach(a =>
                              reprobado.Add(new Reprobados(a, calificaciones.FindAll(c => c.MatriculaAl == a.Matricula)))
            );
           
            reprobado.ForEach(r =>
            {
                
                r.Calificaciones.ForEach(c =>
                {
                    if (c.CalificacionObtenida < 70 && c.CalificacionObtenida>=0 )
                    {
                     r.Materias.Add(new Materia (c.ClaveMat,materias.Find(m=>(c.ClaveMat==m.Clave)).Nombre,materias.Find(m=>(c.ClaveMat==m.Clave)).Creditos));                                 
                      
                    }
                });
            }
            );


            return reprobado;
        }
    }
}
