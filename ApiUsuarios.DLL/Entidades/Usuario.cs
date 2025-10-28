using System;
using System.Collections.Generic;

namespace ApiUsuarios.DLL.Entidades;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public int? Edad { get; set; }
}
