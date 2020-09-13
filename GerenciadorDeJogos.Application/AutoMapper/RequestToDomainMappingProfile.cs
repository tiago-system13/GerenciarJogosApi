using AutoMapper;
using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Domain.Entidades;
using GerenciadorDeJogos.Domain.Entidades.Base;

namespace GerenciadorDeJogos.Application.AutoMapper
{
    public class RequestToDomainMappingProfile: Profile
    {
        public RequestToDomainMappingProfile()
        {
            CreateMap<AmigoRequest, Amigo>();

            CreateMap<JogoRequest, Jogo>();

            CreateMap<ItensEmprestadosRequest, ItensEmprestados>();

            CreateMap<EmprestimoRequest, Emprestimo>();

            CreateMap<ItensDevolvidosRequest, ItensEmprestados>();
                 
            CreateMap<DevolucaoRequest, Emprestimo>();

            CreateMap<PesquisaResquest, Pesquisa>();
        }
    }
}
