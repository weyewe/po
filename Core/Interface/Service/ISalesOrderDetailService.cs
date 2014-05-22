using Core.DomainModel;
using Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface ISalesOrderDetailService
    {
        IList<SalesOrderDetail> GetObjectsBySalesOrderId(int salesOrderId);
        SalesOrderDetail GetObjectById(int Id);
        SalesOrderDetail CreateObject(SalesOrderDetail salesOrderDetail);
        SalesOrderDetail UpdateObject(SalesOrderDetail salesOrderDetail);
        SalesOrderDetail SoftDeleteObject(SalesOrderDetail salesOrderDetail);
        bool DeleteObject(int Id);
        SalesOrderDetail ConfirmObject(SalesOrderDetail salesOrderDetail);
        SalesOrderDetail UnconfirmObject(SalesOrderDetail salesOrderDetail);
        SalesOrderDetail FulfilObject(SalesOrderDetail salesOrderDetail);
    }
}