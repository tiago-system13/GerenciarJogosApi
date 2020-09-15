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
    public class EmprestimoServicoTest
    {
        private ConfigTest _config;

        private EmprestimoServico servicoMock;

        private Mock<IEmprestimoRepositorio> repositorioMock;

        [SetUp]

        public void Setup()
        {
            _config = new ConfigTest().CreateMapper(AutoMapperConfiguration.RegisterMappings());

            repositorioMock = new Mock<IEmprestimoRepositorio>();

            servicoMock = new EmprestimoServico(repositorioMock.Object, _config.Mappe);

        }

        [Test]
        public void Emprestar_RetornaEmprestimoResult()
        {
            var emprestimoMock = GetEmprestimoMock().FirstOrDefault();

            var emprestimoRequest = CriarEmprestimoRequest(emprestimoMock.Id,5, 1);

            repositorioMock.Setup(a => a.Inserir(It.IsAny<Emprestimo>())).Returns(emprestimoMock);

            var emprestimoResult = servicoMock.EmprestarAsync(emprestimoRequest).Result;

            Assert.NotNull(emprestimoResult);
            Assert.AreEqual(emprestimoRequest.DataPrevistaDeVolucao.AddDays(emprestimoRequest.QuantidadeDeDias), emprestimoResult.DataPrevistaDeVolucao.AddDays(emprestimoResult.QuantidadeDeDias));
            repositorioMock.Verify(a => a.Inserir(It.IsAny<Emprestimo>()), Times.Once);


        }

        [Test]
        public void Devolver_RetornaEmprestimoResult()
        {
            var emprestimoMock = GetEmprestimoMock().Last();

            repositorioMock.Setup(a => a.BuscarPorId(It.IsAny<int>())).Returns(emprestimoMock);

            repositorioMock.Setup(a => a.Atualizar(It.IsAny<Emprestimo>())).Returns(emprestimoMock);

            var devolucaoRequest = CriarDevolucaoRequest(emprestimoMock.Id, true , emprestimoMock.ItensEmprestados.Last().JogoId);

            var devolucaoResult = servicoMock.DevolverAsync(devolucaoRequest).Result;

            repositorioMock.Verify(a => a.Atualizar(It.IsAny<Emprestimo>()), Times.Once);

            Assert.NotNull(devolucaoResult);
            Assert.AreEqual(emprestimoMock.AmigoId, devolucaoResult.AmigoId);

        }

        [Test]
        public void Devolver_Exception_EmprestimoNaoEncontrado()
        {
            var emprestimoMock = GetEmprestimoMock().First();

            repositorioMock.Setup(a => a.BuscarPorId(It.IsAny<int>())).Returns((Emprestimo)null);

            var devolucaoRequest = CriarDevolucaoRequest(emprestimoMock.Id, true, emprestimoMock.ItensEmprestados.Last().JogoId);

            var ex = Assert.ThrowsAsync<ArgumentException>(() => servicoMock.DevolverAsync(devolucaoRequest));

            Assert.That(ex.Message, Is.EqualTo("Emprestimo não encontrado!"));
            repositorioMock.Verify(a => a.Atualizar(It.IsAny<Emprestimo>()), Times.Never);

        }

        private List<Emprestimo> GetEmprestimoMock()
        {
            return new List<Emprestimo>()
            {
                CriarEmprestimoMock(1,5,1),
                CriarEmprestimoMock(1,8,1),
                CriarEmprestimoMock(1,10,1,true,DateTime.Now.AddDays(8)),
                CriarEmprestimoMock(1,7,1,true,DateTime.Now.AddDays(7))
            };
        }

        private EmprestimoRequest CriarEmprestimoRequest(int emprestimoId,int quantidadeDias, int amigoId)
        {
            return new EmprestimoRequest()
            {
                Id = emprestimoId,
                QuantidadeDeDias = quantidadeDias,
                AmigoId = amigoId,
                ItensEmprestados = new List<ItensEmprestadosRequest>()
                {
                    CriarItensEmprestadosRequest(emprestimoId),
                    CriarItensEmprestadosRequest(emprestimoId),
                    CriarItensEmprestadosRequest(emprestimoId)
                }
            };
        }

        private DevolucaoRequest CriarDevolucaoRequest(int emprestimoId, bool devolvido, int jogoId)
        {
            return new DevolucaoRequest()
            {
                Id= emprestimoId,
                ItensDevolvidos = new List<ItensDevolvidosRequest>()
                {
                    CriarItensDevolvidosRequest(emprestimoId,jogoId,devolvido)
                }
            };
        }

        private ItensEmprestadosRequest CriarItensEmprestadosRequest(int JogoId)
        {
            return new ItensEmprestadosRequest()
            {
                Id =1,
                JogoId = JogoId
            };

        }

        private ItensDevolvidosRequest CriarItensDevolvidosRequest(int emprestimoId,int JogoId, bool? devolvido = null)
        {
            return new ItensDevolvidosRequest()
            {
                EmprestimoId = emprestimoId,
                JogoId = JogoId,
                Devolvido = devolvido,
            };

        }

        private ItensEmprestados CriarItensEmprestadosMock(int JogoId, int emprestimoId, bool? devolvido = null, DateTime? dataDevolucao = null)
        {
            return new ItensEmprestados()
            {
                Id =1,
                JogoId = JogoId,
                Devolvido = devolvido,
                DataDevolucao = dataDevolucao,
                EmprestimoId = emprestimoId
            };

        }

        private Emprestimo CriarEmprestimoMock(int id, int quantidadeDias, int amigoId, bool? devolvido = null, DateTime? dataDevolucao = null)
        {

            var jogoId = 1;

            return new Emprestimo()
            {
                Id = id,
                QuantidadeDeDias = quantidadeDias,
                AmigoId = amigoId,
                ItensEmprestados = new List<ItensEmprestados>()
                {
                    CriarItensEmprestadosMock(jogoId,id),
                    CriarItensEmprestadosMock(jogoId,id),
                    CriarItensEmprestadosMock(jogoId,id,true,DateTime.Now.AddDays(quantidadeDias)),
                    CriarItensEmprestadosMock(jogoId,id,true,DateTime.Now.AddDays(quantidadeDias))
                }
            };
        }
    }
}
