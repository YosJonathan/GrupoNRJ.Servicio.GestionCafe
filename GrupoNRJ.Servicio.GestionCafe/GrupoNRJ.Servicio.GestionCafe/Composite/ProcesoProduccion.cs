namespace GrupoNRJ.Servicio.GestionCafe.Composite
{
    // Componente base
    public abstract class ProcesoProduccion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public abstract void Mostrar(int nivel = 0);
        public abstract double CalcularAvance();
    }

    // Hoja (tarea simple)
    public class Tarea : ProcesoProduccion
    {
        public double Avance { get; set; } // 0–100%

        public override void Mostrar(int nivel = 0)
        {
            Console.WriteLine($"{new string('-', nivel * 2)} Tarea: {Nombre}, Avance: {Avance}%");
        }

        public override double CalcularAvance()
        {
            return Avance;
        }
    }

    // Compuesto (conjunto de procesos/tareas)
    public class Proceso : ProcesoProduccion
    {
        private List<ProcesoProduccion> componentes = new List<ProcesoProduccion>();

        public void Agregar(ProcesoProduccion componente) => componentes.Add(componente);
        public void Eliminar(ProcesoProduccion componente) => componentes.Remove(componente);

        public override void Mostrar(int nivel = 0)
        {
            Console.WriteLine($"{new string('-', nivel * 2)} Proceso: {Nombre}");
            foreach (var c in componentes)
                c.Mostrar(nivel + 1);
        }

        public override double CalcularAvance()
        {
            if (!componentes.Any()) return 0;
            return componentes.Average(c => c.CalcularAvance());
        }
    }

}
