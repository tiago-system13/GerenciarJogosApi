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

namespace GerenciadorDeJogos.Test.Servico
{
    public class AmigoServicoTest
    {
        private ConfigTest _config;

        private AmigoServico servicoMack;

        private Mock<IAmigoRepositorio> repositorioMock;

        [SetUp]

        public void Setup()
        {
            _config = new ConfigTest().CreateMapper(AutoMapperConfiguration.RegisterMappings());
           
            repositorioMock = new Mock<IAmigoRepositorio>();

            repositorioMock.Setup(a => a.Inserir(It.IsAny<Amigo>())).Returns(GetAmigosMock().FirstOrDefault);

            servicoMack = new AmigoServico(repositorioMock.Object, _config.Mappe);

        }


        [Test]
        public void Inserir_retornaAmigoResult()
        {
            var amigoRequest = CriarAmigoRequest("Tiago");

            var amigoResult = servicoMack.InserirAsync(amigoRequest).Result;

            repositorioMock.Verify(a => a.Inserir(It.IsAny<Amigo>()), Times.Once);

            Assert.NotNull(amigoResult);
            Assert.True(GetAmigosMock().Any(x => x.Nome == amigoRequest.Nome));

        }

        [Test]
        public void Atualizar_retornaAmigoResult()
        {
            var amigoMock = GetAmigosMock().First();
            amigoMock.Nome = "João";

            repositorioMock.Setup(a => a.Existe(It.IsAny<Guid>())).Returns(true);
            repositorioMock.Setup(a => a.Atualizar(It.IsAny<Amigo>())).Returns(amigoMock);

            var amigoRequest = CriarAmigoRequest("João");
            amigoRequest.Id = amigoMock.Id;

            var amigoResult = servicoMack.AtualizarAsync(amigoRequest).Result;

            repositorioMock.Verify(a => a.Atualizar(It.IsAny<Amigo>()), Times.Once);

            Assert.NotNull(amigoResult);
            Assert.AreEqual(amigoMock.Nome, amigoResult.Nome);

        }

        [Test]
        public void Atualizar_Exception_AmigoNaoEncontrado()
        {
            var amigoMock = GetAmigosMock().First();
        
            repositorioMock.Setup(a => a.Existe(It.IsAny<Guid>())).Returns(false);
           
            var amigoRequest = CriarAmigoRequest("João");
            amigoRequest.Id = amigoMock.Id;

            var ex = Assert.ThrowsAsync<ArgumentException>(() => servicoMack.AtualizarAsync(amigoRequest));
     
            Assert.That(ex.Message, Is.EqualTo("Amigo Não Encontrado!"));
            repositorioMock.Verify(a => a.Atualizar(It.IsAny<Amigo>()), Times.Never);

        }

        [Test]

        public void BuscarPorId_RetornaAmigoResult()
        {
            var amigoMock = GetAmigosMock().First();

            repositorioMock.Setup(a => a.BuscarPorId(It.IsAny<Guid>())).Returns(amigoMock);

            var amigoResult = servicoMack.BuscarPorIdAsync(It.IsAny<Guid>()).Result;

            Assert.NotNull(amigoResult);
            Assert.AreEqual(amigoMock.Nome, amigoResult.Nome);
            repositorioMock.Verify(a => a.BuscarPorId(It.IsAny<Guid>()), Times.Once);
        }

        [Test]

        public void BuscarPorNome_RetornaAmigoResult()
        {
            
            repositorioMock.Setup(a => a.ListarTodos()).Returns(GetAmigosMock().AsQueryable());

            var amigosResult = servicoMack.BuscarPorNome("Tiago").Result;

            Assert.IsNotEmpty(amigosResult);
            Assert.True(amigosResult.Any(a=> a.Nome == "Tiago"));
            Assert.AreEqual(amigosResult.Count, 1);
            repositorioMock.Verify(a => a.ListarTodos(), Times.Once);
        }

        [Test]

        public void Perquisar_RetornaListaAmigoResult()
        {
            var amigos = GetAmigosMock();
            repositorioMock.Setup(a => a.ListarTodos()).Returns(amigos.AsQueryable());

            var pesquisa = new PesquisaResquest()
            {
                Nome = string.Empty,
                IndiceDePagina = 1,
                Ordenacao = Domain.Enum.TipoDeOrdenacao.ASC,
                RegistrosPorPagina = 10
            };

            var amigosResult = servicoMack.PesquisarAsync(pesquisa).Result;

            Assert.IsNotEmpty(amigosResult);
            Assert.AreEqual(amigosResult.Count, amigos.Count);
            repositorioMock.Verify(a => a.ListarTodos(), Times.Once);
        }

        [Test]

        public void Perquisar_Comfiltro_RetornaListaAmigoResult()
        {
            var amigos = GetAmigosMock();
            repositorioMock.Setup(a => a.ListarTodos()).Returns(amigos.AsQueryable());

            var pesquisa = new PesquisaResquest()
            {
                Nome = "Tiago",
                IndiceDePagina = 1,
                Ordenacao = Domain.Enum.TipoDeOrdenacao.ASC,
                RegistrosPorPagina = 10
            };

            var amigosResult = servicoMack.PesquisarAsync(pesquisa).Result;

            Assert.IsNotEmpty(amigosResult);
            Assert.AreEqual(amigosResult.Count, 1);
            repositorioMock.Verify(a => a.ListarTodos(), Times.Once);
        }

        [Test]

        public void Excluir_ComSucesso()
        {
            var amigoMock = GetAmigosMock().First();

            repositorioMock.Setup(a => a.Existe(It.IsAny<Guid>())).Returns(true);

            var excluido = servicoMack.ExcluirAsync(amigoMock.Id).Result;

            Assert.NotNull(excluido);
            Assert.True(excluido);
            repositorioMock.Verify(a => a.Excluir(It.IsAny<Guid>()), Times.Once);
        }

        [Test]

        public void ExcluirAmigo_Excecption_AmigoNaoEncontrado()
        {
            var amigoMock = GetAmigosMock().First();

            repositorioMock.Setup(a => a.Existe(It.IsAny<Guid>())).Returns(false);

            var ex = Assert.ThrowsAsync<ArgumentException>(() => servicoMack.ExcluirAsync(amigoMock.Id));

            Assert.That(ex.Message, Is.EqualTo("Amigo Não Encontrado!"));
            repositorioMock.Verify(a => a.Excluir(It.IsAny<Guid>()), Times.Never);
        }

        private List<Amigo> GetAmigosMock()
        {
            return new List<Amigo>()
            {
                CriarAmigoMock(Guid.NewGuid(),"Tiago"),
                CriarAmigoMock(Guid.NewGuid(),"Mário"),
                CriarAmigoMock(Guid.NewGuid(),"André"),
                CriarAmigoMock(Guid.NewGuid(),"Alisson")
            };
        }

        private AmigoRequest CriarAmigoRequest(string nome)
        {
            return new AmigoRequest()
            {
                Nome = nome,
                Telefone = "79998099087"
            };
        }

        private Amigo CriarAmigoMock(Guid id, string nome)
        {
            return new Amigo()
            {
                Id = id,
                Nome = nome,
                Telefone = "79998099087"
            };
        }

    }
}
