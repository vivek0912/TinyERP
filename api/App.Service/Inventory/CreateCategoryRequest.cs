﻿namespace App.Service.Inventory
{
    using System;

    public class CreateCategoryRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public CreateCategoryRequest(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}
