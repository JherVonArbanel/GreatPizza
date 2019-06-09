using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreatPizza.Common.Dtos
{
    public class PizzaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TopingDto> Topings { get; set; }
    }
}