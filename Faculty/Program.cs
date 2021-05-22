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
                        ReadKey();
                        break;
                    case 2:
                        WriteLine("Opción no implementada");
                        ReadKey();
                        break;
                    case 3:
                        WriteLine("Opción no implementada");
                        ReadKey();
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

        static void OpcionMostrarAlumnos()
        {
            Clear();
            WriteLine("**********************************************************");
            WriteLine("*       SISTEMA DE CONTROL ESCOLAR (MOSTRAR ALUMNOS)     *");
            WriteLine("**********************************************************");
            WriteLine();

            WriteLine("MATRICULA - APELLIDO, NOMBRE\n");
            controlEscolar.GetAlumnos().ForEach(a => WriteLine($"{a.Matricula} - {a.NombreCompleto}"));
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
                        WriteLine("Opción no implementada");
                        ReadKey();
                        break;
                    case 2:
                        WriteLine("Opción no implementada");
                        ReadKey();
                        break;
                    case 3:
                        WriteLine("Opción no implementada");
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
    }
}
