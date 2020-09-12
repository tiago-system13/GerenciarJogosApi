using AutoMapper;
using GerenciadorDeJogos.Application.Models.Result;
using GerenciadorDeJogos.Domain.Entidades;

namespace GerenciadorDeJogos.Application.AutoMapper
{
    public class DomainToResultMappingProfile : Profile
    {
        public DomainToResultMappingProfile()
        {
            CreateMap<Amigo, AmigoResult>();

            CreateMap<Jogo, JogoResult>()
            .ForMember(x => x.NomeProprietario, opt => opt.MapFrom(src => src.Proprietario.Nome));

            CreateMap<ItensEmprestados, ItensEmprestadosResult>()
            .ForMember(x => x.NomeJogo, opt => opt.MapFrom(src => src.Jogo.Nome));

            CreateMap<Emprestimo, EmprestimoResult>()
            .ForMember(x => x.NomeAmigo, opt => opt.MapFrom(src => src.Amigo.Nome));

        }
    }
}
