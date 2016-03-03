﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using System.Security.Claims;
using TodoListService.Models;
using System.Collections.Concurrent;
using Microsoft.AspNet.Authorization;

namespace TodoListService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class TodoListController : Controller
    {
        static ConcurrentBag<TodoItem> todoStore = new ConcurrentBag<TodoItem>();

        // GET: api/todolist
        [HttpGet]
        public IEnumerable<TodoItem> Get()
        {
            string owner = (ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier))?.Value;
            return todoStore.Where(t => t.Owner == owner).ToList();
        }
    
        // POST api/todolist
        [HttpPost]
        public void Post(string Title)
        {
            string owner = (ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier))?.Value;
            todoStore.Add(new TodoItem { Owner = owner, Title = Title });
        }
    }
}
