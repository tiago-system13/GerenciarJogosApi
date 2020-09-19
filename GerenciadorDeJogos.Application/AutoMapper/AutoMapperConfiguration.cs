using AutoMapper;

namespace GerenciadorDeJogos.Application.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(ps =>
            {
                ps.AddProfile(new DomainToResponseMappingProfile());
                ps.AddProfile(new RequestToDomainMappingProfile());
            });
        }

    }
}
