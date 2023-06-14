using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApi.WebAPI.AutoMapper
{
    public static class ConfigureAutoMapper
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                //cfg.AddProfile<DataMappingProfile>();
            });
        }
    }
}
