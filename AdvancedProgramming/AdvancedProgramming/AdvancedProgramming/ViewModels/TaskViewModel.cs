using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdvancedProgramming.Data;

namespace AdvancedProgramming.ViewModels
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }  // Ejemplo: Alta, Media, Baja
        public string Status { get; set; }  // Ejemplo: Pendiente, En Proceso, Finalizada, Fallida
        public DateTime ExecutionDate { get; set; }
        public int UserId { get; set; }  // Relación con Usuarios
        public User User { get; set; }  // Relación con la tabla Usuarios
    }

}