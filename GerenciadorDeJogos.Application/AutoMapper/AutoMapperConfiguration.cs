﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDeJogos.Application.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(ps =>
            {
                ps.AddProfile(new DomainToResultMappingProfile());
                ps.AddProfile(new RequestToDomainMappingProfile());
            });
        }

    }
}
