using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiproj.Data;
using WebApiproj.Dtos;
using WebApiproj.Models;

namespace WebApiproj.Controllers
{
    [Route("api/WebApi")]
    [ApiController]

    public class WebApiController : ControllerBase
    {
        private readonly IWebApiRepo _repository;
        private readonly IMapper _mapper;
       
        public WebApiController(IWebApiRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            
        }

        [HttpGet]
        public ActionResult<IEnumerable<WebApiModel>> GetAllCommands()
        {
            
            var CommandItems = _repository.GetAllCommands();

            var response = _mapper.Map<IEnumerable<WebApiReadDto>>(CommandItems);
          
            return Ok(response);
            

        }

        [HttpGet("{id}")]
        public ActionResult<WebApiModel> GetCommandById(int id)
        {
            var CommandItem = _repository.GetCommandById(id);
            if (CommandItem != null)
            {

                return Ok(_mapper.Map<WebApiReadDto>(CommandItem));
            }
            return NotFound();
        }

        [HttpPost]
        //public Task<ActionResult<WebApiReadDto>> CreateCommand(WebApiCreateDto webApiCreateDto)
        //{
        //    var commandModel = _mapper.Map<WebApiModel>(webApiCreateDto);
        //    _repository.CreateCommand(commandModel);
        //    _repository.SaveChanges();

        //    var commandReadDto = _mapper.Map<WebApiReadDto>(commandModel);
        //    return CreatedAtRoute(nameof(GetCommandById), new { ID = commandReadDto.Id }, commandReadDto);
        //}
        public ActionResult<WebApiReadDto> CreateCommand(WebApiCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<WebApiModel>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();



            var commandReadDto = _mapper.Map<WebApiReadDto>(commandModel);



            return CreatedAtRoute(nameof(GetCommandById), new { ID = commandReadDto.id }, commandReadDto);
            //return Ok(commandReadDto); 
        }
    }


}
