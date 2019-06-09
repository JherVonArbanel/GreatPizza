using AutoMapper;
using GreatPizza.Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreatPizza.Common.Dtos;

namespace GreatPizza.Server
{
    public class PizzaManager
    {
        private volatile static PizzaManager innerInstance = null;
        private static object locker = new object();

        protected internal PizzaManager()
        {

        }

        public static PizzaManager Instance
        {
            get
            {
                if (innerInstance == null)
                {
                    lock (locker)
                    {
                        if (innerInstance == null)
                        {
                            innerInstance = new PizzaManager();
                        }
                    }
                }
                return innerInstance;
            }
        }

        public List<PizzaDto> GetPizzas()
        {
            List<PizzaDto> result = new List<PizzaDto>();
            using (var dbContext = new GreatPizza.Server.Models.GreatPizzaDbContext())
            {
                var pizzas = dbContext.Pizza.ToList();
                pizzas.ForEach(x => x.PizzaTopings = x.PizzaTopings
                                                      .Where(t => !t.Toping.Deleted)
                                                      .ToList());

                result = pizzas.Select(x => Mapper.Map<PizzaDto>(x))
                               .ToList();
            }
            return result;
        }

        public PizzaDto GetPizza(int id)
        {
            PizzaDto result = null;
            using (var dbContext = new GreatPizza.Server.Models.GreatPizzaDbContext())
            {
                var pizza = dbContext.Pizza.FirstOrDefault(x => x.Id == id);
                if (pizza != null)
                {
                    result = Mapper.Map<PizzaDto>(pizza);
                }
            }
            return result;
        }

        public async Task<int> DeletePizza(int id)
        {
            using (var dbContext = new GreatPizza.Server.Models.GreatPizzaDbContext())
            {
                var pizza = dbContext.Pizza.FirstOrDefault(x => x.Id == id);
                if (pizza == null)
                {
                    return 0;
                }
                pizza.Deleted = true;
                return await dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> SavePizza(PizzaDto pizza)
        {
            using (var dbContext = new GreatPizza.Server.Models.GreatPizzaDbContext())
            {
                var newPizza = new Pizza
                {
                    Name = pizza.Name
                };
                dbContext.Pizza.Add(newPizza);
                dbContext.SaveChanges();
                foreach (var toping in pizza.Topings)
                {
                    var selectedToping = dbContext.Toping.FirstOrDefault(x => x.Id == toping.Id);
                    if (selectedToping == null)
                    {
                        continue;
                    }
                    dbContext.PizzaTopings.Add(new PizzaTopings
                    {
                        Pizza = newPizza,
                        Toping = selectedToping
                    });
                }
                return await dbContext.SaveChangesAsync();
            }
        }

        public List<TopingDto> GetTopings(int pizzaId)
        {
            var result = new List<TopingDto>();
            using (var dbContext = new GreatPizzaDbContext())
            {
                result = dbContext.PizzaTopings
                                  .Where(x => x.Pizza.Id == pizzaId && !x.Toping.Deleted)
                                  .ToList()
                                  .Select(y => Mapper.Map<TopingDto>(y.Toping))
                                  .ToList();
            }
            return result;
        }

        public List<TopingDto> GetAllTopings()
        {
            var result = new List<TopingDto>();
            using (var dbContext = new GreatPizzaDbContext())
            {
                result = dbContext.Toping
                                  .Where(x=>!x.Deleted)
                                  .ToList()
                                  .Select(y => Mapper.Map<TopingDto>(y))
                                  .ToList();
            }
            return result;
        }

        public async Task<int> AddToping(int pizzaId, int topingId)
        {
            using (var dbContext = new GreatPizza.Server.Models.GreatPizzaDbContext())
            {
                var pizza = dbContext.Pizza.FirstOrDefault(x => x.Id == pizzaId);
                var toping = dbContext.Toping.FirstOrDefault(x => x.Id == topingId);
                if (pizza == null || toping == null)
                {
                    return 0;
                }
                var pizzaToping = dbContext.PizzaTopings.Add(new PizzaTopings
                {
                    Pizza = pizza,
                    Toping = toping
                });
                return await dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> SaveToping(TopingDto toping)
        {
            using (var dbContext = new GreatPizza.Server.Models.GreatPizzaDbContext())
            {
                dbContext.Toping.Add(new Toping
                {
                    Name = toping.Name
                });
                return await dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteToping(int topingId)
        {
            using (var dbContext = new GreatPizza.Server.Models.GreatPizzaDbContext())
            {
                var toping = dbContext.Toping.FirstOrDefault(x => x.Id == topingId);
                if (toping == null)
                {
                    return 0;
                }
                toping.Deleted = true;
                return await dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> RemoveToping(int pizzaId, int topingId)
        {
            using (var dbContext = new GreatPizza.Server.Models.GreatPizzaDbContext())
            {
                var pizza = dbContext.Pizza.FirstOrDefault(x => x.Id == pizzaId);
                var toping = dbContext.Toping.FirstOrDefault(x => x.Id == topingId);

                var pizzaTopings = dbContext.PizzaTopings.Where(x => x.Pizza.Id == pizza.Id &&
                                                               x.Toping.Id == topingId)
                                                   .ToList();
                dbContext.PizzaTopings.RemoveRange(pizzaTopings);
                return await dbContext.SaveChangesAsync();
            }
        }
    }
}
