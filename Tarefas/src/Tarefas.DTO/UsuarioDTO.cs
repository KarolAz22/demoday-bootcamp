using System;
using System.Diagnostics.CodeAnalysis;

namespace Tarefas.DTO
{
    public class UsuarioDTO
    {        
        public string Email { get; set; }     
        public string Senha { get; set; }     
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int Id { get; set; }
    }
}
