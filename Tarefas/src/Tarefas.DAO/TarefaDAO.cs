using Dapper;
using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Tarefas.DTO;
using System.Collections.Generic;

namespace Tarefas.DAO
{
    public class TarefaDAO : BaseDAO, ITarefaDAO
    {        
    public void Criar(TarefaDTO tarefa)
    {
        using (var con = Connection)
        {
            con.Open();
            con.Execute(
                @"INSERT INTO Tarefa
                (Titulo, Descricao, Concluida, UsuarioId) VALUES
                (@Titulo, @Descricao, @Concluida, @UsuarioId);", tarefa
            );
        }
    }


        public List<TarefaDTO> Consultar(int usuarioId)
        {
            using (var con = Connection)
            {
                con.Open();
                var result = con.Query<TarefaDTO>(
                    @"SELECT Id, Titulo, Descricao, Concluida, UsuarioId FROM Tarefa Where UsuarioId = @UsuarioId", new { usuarioId }
                ).ToList();
                return result;
            }
        }


        public TarefaDTO Consultar(int id, int usuarioId)
        {
            using (var con = Connection)
            {
                con.Open();
                TarefaDTO result = con.Query<TarefaDTO>
                (
                    @"SELECT Id, Titulo, Descricao, Concluida FROM Tarefa
                    WHERE Id = @Id", new { id }
                ).First();
                return result;
            }
        }

        public void Atualizar(TarefaDTO tarefa)
        {
            using (var con = Connection)
            {
                con.Open();
                con.Execute(
                    @"UPDATE Tarefa 
                    SET Titulo = @Titulo, Descricao = @Descricao, Concluida = @Concluida
                    WHERE Id = @Id;", tarefa
                );
            }
        }

        public void Excluir(int id, int usuarioId)
        {
            using (var con = Connection)
            {
                con.Open();
                con.Execute(
                    @"DELETE FROM Tarefa
                    WHERE Id = @Id", new { id }
                );
            }
        }
    }
}