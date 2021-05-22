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
            Alumno alumno = new Alumno(matricula, nombre, apellido);
            this.alumnos.Add(alumno);

            materias.ForEach(m =>
            {
                calificaciones.Add(new Calificacion(matricula, m.Clave, -1));
            });

            //Falta actualizar los datos del TXT
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
            this.calificaciones.Find(c => (c.ClaveMat == clave && c.MatriculaAl == matricula)).CalificacionObtenida = calificacion;
            //añadir al TXT
        }
    }
}
