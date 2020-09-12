using GerenciadorDeJogos.Application.Repositorios;
using GerenciadorDeJogos.Domain.Entidades;
using GerenciadorDeJogos.Infrastructure.Mapeamentos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace GerenciadorDeJogos.Infrastructure.Contexto
{
    public class JogosContexto: DbContext
    {
        public JogosContexto()
        {

        }

        public JogosContexto(DbContextOptions<JogosContexto> options) : base(options) { }

        public DbSet<Jogo> Jogos { get; set; }

        public DbSet<Amigo> Amigos { get; set; }

        public DbSet<Emprestimo> Emprestimos { get; set; }

        public DbSet<ItensEmprestados> ItensEmprestados { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AmigoMapeamento());
            modelBuilder.ApplyConfiguration(new JogoMapeamento());
            modelBuilder.ApplyConfiguration(new EmprestimoMapeamento());
            modelBuilder.ApplyConfiguration(new ItensEmprestadosMapeamento());
            modelBuilder.ApplyConfiguration(new UsuarioMapeamento());

            // Aqui estou obtendo todas as classes de configuração das entidades.
            // através da interface IEntityConfig, criada única e exclusivamente para isto.
            // Sendo assim, não precisamos lembrar de, ao criar a configuração de alguma entidade, colocar mais uma linha de código neste trecho.
            var typesToRegister = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IEntityConfig).IsAssignableFrom(x) && !x.IsAbstract).ToList();

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }

        }
    }
}
