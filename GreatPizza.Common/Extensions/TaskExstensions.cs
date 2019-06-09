using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GreatPizza.Common.Extensions
{
    public static class TaskExstensions
    {
        public static async void SendAndForget<T>(this Task<T> task)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}