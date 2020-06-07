using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoMVC.Models;

namespace TodoMVC
{
    public class TodoModel
    {
        public string Key { get; } = "blazor-todos";

        public List<Todo> Todos { get; set; }

        private List<Action> OnChanges { get; set; } = new List<Action>();

        private readonly ILocalStorageService localStorage;

        public TodoModel(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        public void Subscribe(Action onChange)
        {
            OnChanges.Add(onChange);
        }

        private async Task Inform()
        {
            await localStorage.SetItemAsync(Key, Todos);
            OnChanges.ForEach(action => action());
        }

        public async Task Fetch()
        {
            Todos = await localStorage.GetItemAsync<List<Todo>>(Key);
            Todos ??= Enumerable.Empty<Todo>().ToList();
        }

        public async Task AddTodo(string todoTitle)
        {
            var todo = new Todo(Guid.NewGuid(), todoTitle);
            Todos.Add(todo);
            await Inform();
        }

        public async Task ClearCompleted()
        {
            Todos = Todos?.Where(todo => !todo.Completed).ToList();
            await Inform();
        }

        public async Task ToggleAll(bool isCompleted)
        {
            Todos = Todos.Select(todo => new Todo(todo.Id, todo.Title, isCompleted)).ToList();
            await Inform();
        }

        public async Task Toggle(Todo todoToToggle)
        {
            Todos = Todos.Select(todo => !todo.Id.Equals(todoToToggle.Id) ? todo : new Todo(todo.Id, todo.Title, !todo.Completed)).ToList();
            await Inform();
        }

        public async Task Destroy(Todo todo) {
            Todos = Todos.Where(candidate => !candidate.Id.Equals(todo.Id)).ToList();
            await Inform();
        }

        public async Task Save(Todo todo, string text) {
            todo.Title = text;
            Todos = Todos.Select(oldTodo => oldTodo.Id.Equals(todo.Id) ? new Todo(oldTodo.Id, text) : oldTodo).ToList();
            await Inform();
        }
    }
}