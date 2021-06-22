using ProjectTest.Models;
using ProjectTest.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTest.WebApi.Services
{
    public class StockServices : IStockServices
    {
        private readonly DBtestContext _context;
        public StockServices(DBtestContext context)
        {
            _context = context;
        }

        public ResponseModel AddStock(StockEntity request)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                StockEntity data = _context.stockEntities.Where(w => w.StockId == request.StockId).FirstOrDefault();

                if (data == null)
                {
                    data = new StockEntity
                    {
                        productId = request.productId,
                        amount = request.amount
                    };

                    _context.stockEntities.Add(data);
                    _context.SaveChanges();
                }

                response.data = data;
                response.success = true;

            }
            catch (Exception ex)
            {
                throw new NullReferenceException(ex.Message);
            }

            return response;
        }

        public List<StockEntity> GetStock()
        {
            var stock = new List<StockEntity>();
            try
            {
                stock = _context.stockEntities.ToList();

                stock.ForEach(r => new StockEntity
                {
                    StockId = r.StockId,
                    productId = r.productId,
                    amount = r.amount
                });


            }
            catch (Exception ex)
            {
                throw new NullReferenceException(ex.Message);
            }
            return stock;
        }
    }
}
