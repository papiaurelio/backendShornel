﻿namespace WebApi.Dtos
{
    public class UsuarioDto
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }

        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Imagen { get; set; }
    }
}
