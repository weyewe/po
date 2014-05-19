using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class ItemService : IItemService
    {
        private IItemRepository _i;
        public ItemService(IItemRepository _itemRepository)
        {
            _i = _itemRepository;
        }

        public IList<Item> GetAll()
        {
            return _i.GetAll();
        }

        public Item GetObjectById(int Id)
        {
            return _i.GetObjectById(Id);
        }

        public Item GetObjectBySku(string Sku)
        {
            return _i.GetObjectBySku(Sku);
        }

        public Item CreateObject(Item item)
        {
            return _i.CreateObject(item);
        }

        public Item UpdateObject(Item item)
        {
            return _i.UpdateObject(item);
        }

        public Item SoftDeleteObject(Item item)
        {
            return _i.SoftDeleteObject(item);
        }

        public bool DeleteObject(int Id)
        {
            return _i.DeleteObject(Id);
        }
    }
}