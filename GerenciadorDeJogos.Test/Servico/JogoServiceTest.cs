using GerenciadorDeJogos.Application.AutoMapper;
using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Application.Repositorios;
using GerenciadorDeJogos.Application.Servicos;
using GerenciadorDeJogos.Domain.Entidades;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GerenciadorDeJogos.Test.Servico
{
    public class JogoServiceTest
    {
        private ConfigTest _config;

        private JogoServico servicoMock;

        private Mock<IJogoRepositorio> repositorioMock;

        [SetUp]

        public void Setup()
        {
            _config = new ConfigTest().CreateMapper(AutoMapperConfiguration.RegisterMappings());

            repositorioMock = new Mock<IJogoRepositorio>();

            servicoMock = new JogoServico(repositorioMock.Object, _config.Mappe);

        }

        [Test]
        public void Inserir_retornaJogoResult()
        {
            var jogoRequest = CriarJogoRequest("Top Guia",Guid.NewGuid());

            repositorioMock.Setup(a => a.Inserir(It.IsAny<Jogo>())).Returns(GetJogosMock().FirstOrDefault());

            var jogoResult = servicoMock.InserirAsync(jogoRequest).Result;

            repositorioMock.Verify(a => a.Inserir(It.IsAny<Jogo>()), Times.Once);

            Assert.NotNull(jogoResult);
            Assert.True(GetJogosMock().Any(x => x.Nome == jogoRequest.Nome));

        }

        [Test]
        public void Atualizar_retornaJogoResult()
        {
            var jogoMock = GetJogosMock().First();
            jogoMock.Nome = "Top Guia";

            repositorioMock.Setup(a => a.Existe(It.IsAny<Guid>())).Returns(true);
            repositorioMock.Setup(a => a.Atualizar(It.IsAny<Jogo>())).Returns(jogoMock);

            var jogoRequest = CriarJogoRequest("Top Guia", Guid.NewGuid());
            jogoRequest.Id = jogoMock.Id;

            var jogoResult = servicoMock.AtualizarAsync(jogoRequest).Result;

            repositorioMock.Verify(a => a.Atualizar(It.IsAny<Jogo>()), Times.Once);

            Assert.NotNull(jogoResult);
            Assert.AreEqual(jogoMock.Nome, jogoResult.Nome);

        }

        [Test]
        public void Atualizar_Exception_JogoNaoEncontrado()
        {
            var jogoMock = GetJogosMock().First();

            repositorioMock.Setup(a => a.Existe(It.IsAny<Guid>())).Returns(false);

            var joRequest = CriarJogoRequest("Top Guia", Guid.NewGuid());
            joRequest.Id = jogoMock.Id;

            var ex = Assert.ThrowsAsync<ArgumentException>(() => servicoMock.AtualizarAsync(joRequest));

            Assert.That(ex.Message, Is.EqualTo("Jogo Não Encontrado!"));
            repositorioMock.Verify(a => a.Atualizar(It.IsAny<Jogo>()), Times.Never);

        }

        [Test]

        public void BuscarPorId_RetornaJogoResult()
        {
            var jogoMock = GetJogosMock().First();

            repositorioMock.Setup(a => a.BuscarPorId(It.IsAny<Guid>())).Returns(jogoMock);

            var jogoResult = servicoMock.BuscarPorIdAsync(It.IsAny<Guid>()).Result;

            Assert.NotNull(jogoResult);
            Assert.AreEqual(jogoMock.Nome, jogoResult.Nome);
            repositorioMock.Verify(a => a.BuscarPorId(It.IsAny<Guid>()), Times.Once);
        }

        [Test]

        public void BuscarPorNome_RetornaJogoResult()
        {

            repositorioMock.Setup(a => a.ListarTodos()).Returns(GetJogosMock().AsQueryable());

            var jogosResult = servicoMock.BuscarPorNome("Top Guia").Result;

            Assert.IsNotEmpty(jogosResult);
            Assert.True(jogosResult.Any(a => a.Nome == "Top Guia"));
            Assert.AreEqual(jogosResult.Count, 1);
            repositorioMock.Verify(a => a.ListarTodos(), Times.Once);
        }

        [Test]

        public void Perquisar_RetornaListaJogoResult()
        {
            var jogos = GetJogosMock();
            repositorioMock.Setup(a => a.TodosIncluindo(x => x.Proprietario)).Returns(jogos.AsQueryable());

            var pesquisa = new PesquisaResquest()
            {
                Nome = string.Empty,
                IndiceDePagina = 1,
                Ordenacao = Domain.Enum.TipoDeOrdenacao.ASC,
                RegistrosPorPagina = 10
            };

            var jogosResult = servicoMock.PesquisarAsync(pesquisa).Result;

            Assert.IsNotEmpty(jogosResult);
            Assert.AreEqual(jogosResult.Count, jogos.Count);
            repositorioMock.Verify(a => a.TodosIncluindo(x => x.Proprietario), Times.Once);
        }

        [Test]

        public void Perquisar_ComFiltro_RetornaListaJogoResult()
        {
            var jogos = GetJogosMock();
            repositorioMock.Setup(a => a.TodosIncluindo(x => x.Proprietario)).Returns(jogos.AsQueryable());

            var pesquisa = new PesquisaResquest()
            {
                Nome = "Top Guia",
                IndiceDePagina = 1,
                Ordenacao = Domain.Enum.TipoDeOrdenacao.ASC,
                RegistrosPorPagina = 10
            };

            var jogosResult = servicoMock.PesquisarAsync(pesquisa).Result;

            Assert.IsNotEmpty(jogosResult);
            Assert.AreEqual(jogosResult.Count, 1);
            repositorioMock.Verify(a => a.TodosIncluindo(x => x.Proprietario), Times.Once);
        }

        [Test]

        public void Excluir_ComSucesso()
        {
            var jogoMock = GetJogosMock().First();

            repositorioMock.Setup(a => a.Existe(It.IsAny<Guid>())).Returns(true);

            var excluido = servicoMock.ExcluirAsync(jogoMock.Id).Result;

            Assert.NotNull(excluido);
            Assert.True(excluido);
            repositorioMock.Verify(a => a.Excluir(It.IsAny<Guid>()), Times.Once);
        }

        [Test]

        public void Excluir_Excecption_JogoNaoEncontrado()
        {
            var jogoMock = GetJogosMock().First();

            repositorioMock.Setup(a => a.Existe(It.IsAny<Guid>())).Returns(false);

            var ex = Assert.ThrowsAsync<ArgumentException>(() => servicoMock.ExcluirAsync(jogoMock.Id));

            Assert.That(ex.Message, Is.EqualTo("Jogo Não Encontrado!"));
            repositorioMock.Verify(a => a.Excluir(It.IsAny<Guid>()), Times.Never);
        }

        private List<Jogo> GetJogosMock()
        {
            return new List<Jogo>()
            {
                CriarJogoMock(Guid.NewGuid(),"Top Guia",Guid.NewGuid()),
                CriarJogoMock(Guid.NewGuid(),"Super Mário",Guid.NewGuid()),
                CriarJogoMock(Guid.NewGuid(),"Fifa 2020",Guid.NewGuid()),
                CriarJogoMock(Guid.NewGuid(),"Teken",Guid.NewGuid())
            };
        }

        private JogoRequest CriarJogoRequest(string nome, Guid proprietarioId)
        {
            return new JogoRequest()
            {
                Nome = nome,
                ProprietarioId = proprietarioId
            };
        }

        private Jogo CriarJogoMock(Guid id, string nome, Guid proprietarioId)
        {
            return new Jogo()
            {
                Id = id,
                Nome = nome,
                ProprietarioId = proprietarioId,
                Proprietario = new Amigo() { Id= proprietarioId, Nome = "Tiago"}
            };
        }
    }
}
