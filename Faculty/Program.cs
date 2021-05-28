using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Faculty
{
    class Program
    {
        static ControlEscolar controlEscolar;
        static void Main(string[] args)
        {
            controlEscolar = new ControlEscolar();
            int opcion = -1;

            do
            {
                Clear();
                WriteLine("**********************************************************");
                WriteLine("*        SISTEMA DE CONTROL ESCOLAR (MENU PRINCIPAL)     *");
                WriteLine("**********************************************************");
                WriteLine();
                WriteLine("\t1. Mostrar");
                WriteLine("\t2. Alta de Alumnos");
                WriteLine("\t3. Asignar Calificaciones");
                WriteLine("\t4. Reportes");
                WriteLine("\n\t0. Salir");
                WriteLine("Elige una opción:");

                opcion = Convert.ToInt32(ReadLine());

                switch (opcion)
                {
                    case 1:
                        SubmenuMostrar();
                        break;
                    case 2:
                        OpcionAltaAlumnos();
                        break;
                    case 3:
                        OpcionAsignarCalificaciones();
                        break;
                    case 4:
                        SubmenuReportes();
                        ReadKey();
                        break;
                    case 0:
                        WriteLine("\n¡Gracias por utilizar el programa!");
                        break;
                    default:
                        WriteLine("\n¡OPCIÓN NO VÁLIDA!");
                        ReadKey();
                        break;
                }
            } while (opcion != 0);
        }

        static void OpcionAltaAlumnos()
        {
            Clear();
            WriteLine("**********************************************************");
            WriteLine("*       SISTEMA DE CONTROL ESCOLAR (ALTA DE ALUMNO)      *");
            WriteLine("**********************************************************");
            WriteLine();

            Write("Ingrese la Matricula: "); int matricula = Convert.ToInt32(ReadLine());
            
            if (!controlEscolar.ValidarMatricula(matricula))
            {
                Write("Ingrese el Nombre: "); string nombre = ReadLine();
                Write("Ingrese el Apellido: "); string apellido = ReadLine();
                controlEscolar.NuevoAlumno(matricula, nombre, apellido);

                WriteLine("\n\nALUMNO INGRESADO CON ÉXITO\n");
            }
            else
            {
                WriteLine("MATRICULA EN USO\nLa matricula que ingresó ya está en uso por lo que no es válida. ");
            }
            WriteLine();
            ReadKey();
        }
        static void OpcionAsignarCalificaciones()
        {
            Clear();
            WriteLine("**********************************************************");
            WriteLine("*  SISTEMA DE CONTROL ESCOLAR (ASIGNAR CALIFICACIONES)   *");
            WriteLine("**********************************************************");
            WriteLine();

            Write("Ingrese la Matricula: "); int matricula = Convert.ToInt32(ReadLine());

            if (controlEscolar.ValidarMatricula(matricula))
            {
                Write("Ingrese la Clave de la materia: "); int clave = Convert.ToInt32(ReadLine());
                if (controlEscolar.ValidarClave(clave))
                {
                    if(!controlEscolar.EstatusMateria(matricula, clave))
                    {
                        Write("Ingrese la Calificación obtenida: "); int calificacion = Convert.ToInt32(ReadLine());
                        controlEscolar.AsignarCalificacion(matricula, clave, calificacion);

                        WriteLine("\n\nCALIFICACIÓN ASIGNADA CON ÉXITO\n\n");
                    }
                    else
                    {
                        WriteLine("\n\nERROR");
                        WriteLine("Solo se puede asignar una calificación a una materia no aprobada");
                    }
                }
                else
                {
                    WriteLine("CLAV DE NO VÁLIDA");
                    WriteLine("\nLa clave de la materia que ingresó NO está en uso por lo que no es válida. ");
                }
            }
            else
            {
                WriteLine("MATRICULA NO VÁLIDA");
                WriteLine("\nLa matricula que ingresó NO está en uso por lo que no es válida. ");
            }
            WriteLine();
            ReadKey();
        }

        static void SubmenuMostrar()
        {
            int opcion = -1;

            do
            {
                Clear();
                WriteLine("**********************************************************");
                WriteLine("*       SISTEMA DE CONTROL ESCOLAR (SUBMENU MOSTRAR)     *");
                WriteLine("**********************************************************");
                WriteLine();
                WriteLine("\t1. Alumnos");
                WriteLine("\t2. Materias");
                WriteLine("\n\t0. Regresar al Menú principal");
                WriteLine("Elige una opción:");

                opcion = Convert.ToInt32(ReadLine());

                switch (opcion)
                {
                    case 1:
                        OpcionMostrarAlumnos();
                        break;
                    case 2:
                        OpcionMostrarMaterias();
                        break;
                    case 0:
                        break;
                    default:
                        WriteLine("\n¡OPCIÓN NO VÁLIDA!");
                        ReadKey();
                        break;
                }
            } while (opcion != 0);
        }

        static void OpcionMostrarAlumnos()
        {
            Clear();
            WriteLine("**********************************************************");
            WriteLine("*       SISTEMA DE CONTROL ESCOLAR (MOSTRAR ALUMNOS)     *");
            WriteLine("**********************************************************");
            WriteLine();

            WriteLine("MATRICULA - APELLIDO, NOMBRE\n");
            controlEscolar.GetAlumnos().ForEach(a => 
                WriteLine($"{a.Matricula} - {a.NombreCompleto}"));
            WriteLine();
            ReadKey();
        }

        static void OpcionMostrarMaterias()
        {
            Clear();
            WriteLine("**********************************************************");
            WriteLine("*      SISTEMA DE CONTROL ESCOLAR (MOSTRAR MATERIAS)     *");
            WriteLine("**********************************************************");
            WriteLine();

            WriteLine("CLAVE\tDESCRIPCION-CREDITOS\n");
            controlEscolar.GetMaterias().ForEach(m => WriteLine($"{m.Clave}\t{m.Nombre} - {m.Creditos}"));
            WriteLine();
            ReadKey();
        }

        static void SubmenuReportes()
        {
            int opcion = -1;

            do
            {
                Clear();
                WriteLine("**********************************************************");
                WriteLine("*      SISTEMA DE CONTROL ESCOLAR (SUBMENU REPORTES)     *");
                WriteLine("**********************************************************");
                WriteLine();
                WriteLine("\t1. Promedio total de alumnos");
                WriteLine("\t2. Promedio parcial de alumnos");
                WriteLine("\t3. Alumnos con materias reprobadas");
                WriteLine("\t4. Extraordinarios");
                WriteLine("\n\t0. Regresar al Menú principal");
                WriteLine("Elige una opción:");

                opcion = Convert.ToInt32(ReadLine());

                switch (opcion)
                {
                    case 1:
                        PromedioTotaldeAlumnos();
                        ReadKey();
                        break;
                    case 2:
                        PromedioParcialdeAlumnos();
                        ReadKey();
                        break;
                    case 3:
                        AlumnosReprobados();
                        ReadKey();
                        break;
                    case 4:
                        WriteLine("Opción no implementada");
                        ReadKey();
                        break;
                    case 0:
                        break;
                    default:
                        WriteLine("\n¡OPCIÓN NO VÁLIDA!");
                        ReadKey();
                        break;
                }
            } while (opcion != 0);
        }
        static void PromedioTotaldeAlumnos()
        {
            Clear();
            WriteLine("**********************************************************");
            WriteLine("* SISTEMA DE CONTROL ESCOLAR (PROMEDIO TOTAL DE ALUMNOS) *");
            WriteLine("**********************************************************");
            WriteLine();

            WriteLine("MATRICULA\tAPELLIDO, NOMBRE\tPROMEDIO GENERAL\n");
            controlEscolar.GetPromedio().ForEach(r => {
                WriteLine($"{r.Alumno.Matricula} - {r.Alumno.NombreCompleto} - {r.Promedio}");
            });
            WriteLine();
            ReadKey();
        }
        static void PromedioParcialdeAlumnos()
        {
            Clear();
            WriteLine("**********************************************************");
            WriteLine("*SISTEMA DE CONTROL ESCOLAR (PROMEDIO PARCIAL DE ALUMNOS)*");
            WriteLine("**********************************************************");
            WriteLine();

            WriteLine("MATRICULA\tAPELLIDO, NOMBRE\tPROMEDIO PARCIAL\n");
            controlEscolar.GetPromedioParcial().ForEach(r => {
                WriteLine($"{r.Alumno.Matricula} - {r.Alumno.NombreCompleto} - {r.Promedio}");
            });
            WriteLine();
            ReadKey();
        }
        static void AlumnosReprobados()
        {
            Clear();
            WriteLine("**********************************************************");
            WriteLine("*     SISTEMA DE CONTROL ESCOLAR (ALUMNOS REPROBADOS)    *");
            WriteLine("**********************************************************");
            WriteLine();

            WriteLine("MATRICULA\tAPELLIDO, NOMBRE\tMaterias reprobadas\n");
            controlEscolar.GetReprobados().ForEach(r => {
                WriteLine($"\n\n{r.Alumno.Matricula} - {r.Alumno.NombreCompleto}");
                r.Materias.ForEach(m => {
                   Write($"\n\t\t\t\t{m.Nombre}"); 
                });
            });
            WriteLine();
            ReadKey();
        }
    }
}
