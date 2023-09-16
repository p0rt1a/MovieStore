using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.MovieOperations.Commands.CreateMovie;
using WebApi.Application.MovieOperations.Commands.DeleteMovie;
using WebApi.Application.MovieOperations.Commands.UpdateMovie;
using WebApi.Application.MovieOperations.Queries.GetMovie;
using WebApi.Application.MovieOperations.Queries.GetMovies;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public MoviesController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll() {
            GetMoviesQuery query = new(_context, _mapper);
            var result = query.Handle();
            
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            GetMovieQuery query = new(_context, _mapper);
            query.MovieId = id;

            GetMovieQueryValidator validator = new();
            validator.ValidateAndThrow(query);

            var result = query.Handle();

            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody]CreateMovieModel model)
        {
            CreateMovieCommand command = new(_context, _mapper);
            command.Model = model;

            CreateMovieCommandValidator validator = new();
            validator.ValidateAndThrow(command); 

            command.Handle();

            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update([FromBody]UpdateMovieModel model, int id)
        {
            UpdateMovieCommand command = new(_context);
            command.Model = model;
            command.MovieId = id;

            UpdateMovieCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            
            command.Handle();

            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DeleteMovieCommand command = new(_context);
            command.MovieId = id;

            DeleteMovieCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }
    }
}