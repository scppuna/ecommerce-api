﻿using System;

namespace UsuariosAPI.Data.Dto.Usuario
{
    public class UpdateUsuarioDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cep { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }

    }
}
