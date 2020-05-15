using AutoMapper;
using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {

            var config = new MapperConfigurationExpression();
            config.AddProfile<AdminToViewModel>();
            config.AddProfile<AdminFromViewModel>();
            config.AddProfile<ToViewModel>();
            config.AddProfile<FromViewModel>();
            Mapper.Initialize(config);
        }
    }
}