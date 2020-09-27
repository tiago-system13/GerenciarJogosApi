using AutoMapper;
using GerenciadorDeJogos.Application.Models.Responses;
using GerenciadorDeJogos.Domain.Entidades;

namespace GerenciadorDeJogos.Application.AutoMapper
{
    public class DomainToResponseMappingProfile : Profile
    {
        public DomainToResponseMappingProfile()
        {
            CreateMap<Amigo, AmigoResponse>();

            CreateMap<Usuario, UsuarioResponse>();

            CreateMap<Jogo, JogoResponse>()
            .ForMember(x => x.NomeProprietario, opt => opt.MapFrom(src => src.Proprietario.Nome));

            CreateMap<ItensEmprestados, ItensEmprestadosResponse>()
            .ForMember(x => x.NomeJogo, opt => opt.MapFrom(src => src.Jogo.Nome));

            CreateMap<Emprestimo, EmprestimoResponse>()
            .ForMember(x => x.NomeAmigo, opt => opt.MapFrom(src => src.Amigo.Nome));


        }
    }
}
