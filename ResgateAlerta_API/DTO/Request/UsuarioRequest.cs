﻿namespace ResgateAlerta.DTO.Request
{
    public class UsuarioRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string TipoUsuario { get; set; }  // "Pessoa Física" ou "Pessoa Jurídica"

    }
}
