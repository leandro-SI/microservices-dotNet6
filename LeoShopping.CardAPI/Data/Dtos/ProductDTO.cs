﻿using LeoShopping.CartAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeoShopping.CartAPI.Model
{
    public class ProductDTO
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } 
        public string CategoryName { get; set; }
        public string ImageURL { get; set; }


    }
}
