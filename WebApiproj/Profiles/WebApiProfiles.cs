using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiproj.Dtos;
using WebApiproj.Models;

namespace WebApiproj.Profiles
{
    public class WebApiProfiles:Profile
    {
        public WebApiProfiles()
        {
            CreateMap<WebApiModel, WebApiReadDto>();
            CreateMap<WebApiCreateDto,WebApiModel>();
            CreateMap<WebApiReadDto, WebApiModel>();
            CreateMap<WebApiModel, WebApiCreateDto>();
        }
    }
}
