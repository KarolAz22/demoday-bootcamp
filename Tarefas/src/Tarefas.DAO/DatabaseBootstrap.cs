using Dapper;
using System;
using System.Data.SQLite;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Tarefas.DTO;
using Tarefas.DAO;

namespace Tarefas.DAO
{
    public class DatabaseBootstrap : BaseDAO, IDatabaseBootstrap
    {        
        public DatabaseBootstrap()
        {
            
        }

        public void Setup()
        {
            if(!File.Exists(DataSourceFile))
            {
                using (var con = Connection)
                {   
                    con.Execute(
                        @"CREATE TABLE Tarefa
                        (
                            Id          integer primary key autoincrement,
                            Titulo      varchar(100) not null,
                            Descricao   varchar(100) not null,
                            Concluida   bool not null,
                            UsuarioId   integer,
                            FOREIGN KEY(UsuarioId) REFERENCES Usuario(Id)
                        )"

                    );

                    con.Execute(
                        @"CREATE TABLE Usuario
                        (
                            Id          integer primary key autoincrement,
                            Email       varchar(100) not null,
                            Senha       varchar(100) not null,
                            Nome        varchar(100) not null,
                            Ativo       bool not null
                        )"
                    );  

                    InsertDefaultData(con);
                }
            }
        }

        private void InsertDefaultData(SQLiteConnection con)
    {
    var usuario1 = new UsuarioDTO()
    {

        Email = "andre@gmail.com",
        Senha = "biscoito",
        Nome = "André Paulovich",
        Ativo = true
    };

    var usuario2 = new UsuarioDTO()
    {
        Email = "ivan@gmail.com",
        Senha = "bolacha",
        Nome = "Ivan Paulovich",
        Ativo = true
    };

    con.Execute(
        @"INSERT INTO Usuario
        (Email, Senha, Nome, Ativo) VALUES
        (@Email, @Senha, @Nome, @Ativo);", usuario1
    );

    con.Execute(
        @"INSERT INTO Usuario
        (Email, Senha, Nome, Ativo) VALUES
        (@Email, @Senha, @Nome, @Ativo);", usuario2
    );

    var tarefa1 = new TarefaDTO()
    {
        Titulo = "Comprar pão",
        Descricao = "Passar na padaria da Vovó Alice e comprar 12 pães",
        Concluida = false,
        UsuarioId = 1
    };

    var tarefa2 = new TarefaDTO()
    {
        Titulo = "Levar o cachorro para pasear",
        Descricao = "Levar a Bia e a Pretinha para dar uma voltinha na Lagoa. Não esquecer de levar água para elas.",
        Concluida = false,
        UsuarioId = 1
    };

    var tarefa3 = new TarefaDTO()
    {
        Titulo = "Lavar o carro",
        Descricao = "Lavar e aspirar o carro. Lembrar que aspirar o porta-malas que está cheio de areia da praia.",
        Concluida = false,
        UsuarioId = 2
    };

    con.Execute(
        @"INSERT INTO Tarefa
        (Titulo, Descricao, Concluida, UsuarioId) VALUES
        (@Titulo, @Descricao, @Concluida, 1);", tarefa1
    );

    con.Execute(
        @"INSERT INTO Tarefa
        (Titulo, Descricao, Concluida, UsuarioId) VALUES
        (@Titulo, @Descricao, @Concluida, 1);", tarefa2
    );

    con.Execute(
        @"INSERT INTO Tarefa
        (Titulo, Descricao, Concluida, UsuarioId) VALUES
        (@Titulo, @Descricao, @Concluida, 2);", tarefa3
    );
}

    }
}