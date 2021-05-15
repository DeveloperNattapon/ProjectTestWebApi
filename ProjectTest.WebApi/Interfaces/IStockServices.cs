using ProjectTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTest.WebApi.Interfaces
{
    public interface IStockServices
    {
        public List<StockEntity> GetStock();
        public ResponseModel AddStock(StockEntity request);
    }
}
