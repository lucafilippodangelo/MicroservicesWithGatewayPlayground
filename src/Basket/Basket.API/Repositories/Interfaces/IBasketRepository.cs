using Basket.API.Entities;
using System.Threading.Tasks;

namespace Basket.API.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        Task<BasketCart> GetBasket(string userName); //LD uses this as redis key
        Task<BasketCart> UpdateBasket(BasketCart basket); //LD uses this as value
        Task<bool> DeleteBasket(string userName);
    }
}
