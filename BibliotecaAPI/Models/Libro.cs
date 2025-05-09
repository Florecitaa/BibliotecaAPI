﻿namespace BibliotecaAPI.Models
{
    public class Libro
    { //modelo de libro segun la tabla de la base de datos 
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public string ISBN { get; set; }
        public int Anio { get; set; }
        public string Categoria { get; set; }
        public int Existencias { get; set; }
    }
}
