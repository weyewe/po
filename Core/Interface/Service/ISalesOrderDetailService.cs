using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface ISalesOrderDetailService
    {
        ISalesOrderDetailValidator GetValidator();
        IList<SalesOrderDetail> GetObjectsBySalesOrderId(int salesOrderId);
        SalesOrderDetail GetObjectById(int Id);
        SalesOrderDetail CreateObject(SalesOrderDetail salesOrderDetail, ISalesOrderService _salesOrderService, IItemService _itemService);
        SalesOrderDetail CreateObject(int salesOrderId, int itemId, int quantity, decimal Price, ISalesOrderService _salesOrderService, IItemService _itemService);
        SalesOrderDetail UpdateObject(SalesOrderDetail salesOrderDetail, ISalesOrderService _salesOrderService, IItemService _itemService);
        SalesOrderDetail SoftDeleteObject(SalesOrderDetail salesOrderDetail);
        bool DeleteObject(int Id);
        SalesOrderDetail ConfirmObject(SalesOrderDetail salesOrderDetail, IStockMutationService _stockMutationService, IItemService _itemService);
        SalesOrderDetail UnconfirmObject(SalesOrderDetail salesOrderDetail, IDeliveryOrderDetailService _deliveryOrderDetailService, IStockMutationService _stockMutationService, IItemService _itemService);
        SalesOrderDetail FulfilObject(SalesOrderDetail salesOrderDetail);
    }
}