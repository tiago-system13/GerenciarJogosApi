using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDeJogos.Test
{
    public partial class ConfigTest
    {
        public IMapper Mappe { get; private set; }

        public ConfigTest()
        {

        }

        public ConfigTest CreateMapper(MapperConfiguration mapperConfig)
        {
            Mappe = mapperConfig.CreateMapper();
            return this;
        }

    }
}
