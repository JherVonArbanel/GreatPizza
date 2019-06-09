using GreatPizza.Common.Extensions;
using GreatPizza.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GreatPizza.Server.Controllers
{
    public class PizzaController : ApiController
    {
        // GET api/pizza
        public List<PizzaDto> Get()
        {
            try
            {
                return PizzaManager.Instance.GetPizzas();
            }
            catch (Exception ex)
            {
                LogManager.Instance.Write(ex).SendAndForget<bool>();
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(GreatPizzaConstants.ErrorMessage),
                    ReasonPhrase = GreatPizzaConstants.ReasonPhrase
                });
            }
        }

        // GET api/pizza/5
        public PizzaDto Get(int id)
        {
            try
            {
                return PizzaManager.Instance.GetPizza(id);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Write(ex).SendAndForget<bool>();
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(GreatPizzaConstants.ErrorMessage),
                    ReasonPhrase = GreatPizzaConstants.ReasonPhrase
                });
            }
        }

        public async Task<int> PostPizza(PizzaDto value)
        {
            try
            {
                return await PizzaManager.Instance.SavePizza(value);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Write(ex).SendAndForget<bool>();
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(GreatPizzaConstants.ErrorMessage),
                    ReasonPhrase = GreatPizzaConstants.ReasonPhrase
                });
            }
        }

        // DELETE api/pizza/5
        public async Task<int> Delete(int id)
        {
            try
            {
               return await PizzaManager.Instance.DeletePizza(id);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Write(ex).SendAndForget<bool>();
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(GreatPizzaConstants.ErrorMessage),
                    ReasonPhrase = GreatPizzaConstants.ReasonPhrase
                });
            }
        }

        [Route("api/pizza/toping")]
        public List<TopingDto> GetTopings()
        {
            try
            {
                return PizzaManager.Instance.GetAllTopings();
            }
            catch (Exception ex)
            {
                LogManager.Instance.Write(ex).SendAndForget<bool>();
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(GreatPizzaConstants.ErrorMessage),
                    ReasonPhrase = GreatPizzaConstants.ReasonPhrase
                });
            }
        }

        [Route("api/pizza/toping")]
        public async Task<int> PostToping(TopingDto value)
        {
            try
            {
                return await PizzaManager.Instance.SaveToping(value);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Write(ex).SendAndForget<bool>();
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(GreatPizzaConstants.ErrorMessage),
                    ReasonPhrase = GreatPizzaConstants.ReasonPhrase
                });
            }
        }

        [Route("api/pizza/toping/{id}")]
        public async Task<int> DeleteToping(int id)
        {
            try
            {
                return await PizzaManager.Instance.DeleteToping(id);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Write(ex).SendAndForget<bool>();
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(GreatPizzaConstants.ErrorMessage),
                    ReasonPhrase = GreatPizzaConstants.ReasonPhrase
                });
            }
        }

        [Route("api/pizza/PizzaToping/{pizzaId}/{topingId}")]
        public async Task<int> PutPizzaToping(int pizzaId, int topingId)
        {
            try
            {
                return await PizzaManager.Instance.AddToping(pizzaId,topingId);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Write(ex).SendAndForget<bool>();
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(GreatPizzaConstants.ErrorMessage),
                    ReasonPhrase = GreatPizzaConstants.ReasonPhrase
                });
            }
        }

        [Route("api/pizza/PizzaToping/{pizzaId}/{topingId}")]
        public async Task<int> DeletePizzaToping(int pizzaId, int topingId)
        {
            try
            {
                return await PizzaManager.Instance.RemoveToping(pizzaId, topingId);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Write(ex).SendAndForget<bool>();
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(GreatPizzaConstants.ErrorMessage),
                    ReasonPhrase = GreatPizzaConstants.ReasonPhrase
                });
            }
        }
    }
}
