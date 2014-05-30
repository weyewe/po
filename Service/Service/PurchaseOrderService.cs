using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using Core.Interface.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private IPurchaseOrderRepository _p;
        private IPurchaseOrderValidator _validator;

        public PurchaseOrderService(IPurchaseOrderRepository _purchaseOrderRepository, IPurchaseOrderValidator _purchaseOrderValidator)
        {
            _p = _purchaseOrderRepository;
            _validator = _purchaseOrderValidator;
        }

        public IPurchaseOrderValidator GetValidator()
        {
            return _validator;
        }

        public IList<PurchaseOrder> GetAll()
        {
            return _p.GetAll();
        }

        public PurchaseOrder GetObjectById(int Id)
        {
            return _p.GetObjectById(Id);
        }

        public IList<PurchaseOrder> GetObjectsByContactId(int contactId)
        {
            return _p.GetObjectsByContactId(contactId);
        }

        public PurchaseOrder CreateObject(PurchaseOrder purchaseOrder, IContactService _contactService)
        {
            purchaseOrder.Errors = new HashSet<string>();
            return (_validator.ValidCreateObject(purchaseOrder, _contactService) ? _p.CreateObject(purchaseOrder) : purchaseOrder);
        }

        public PurchaseOrder CreateObject(int contactId, DateTime purchaseDate, IContactService _contactService)
        {
            PurchaseOrder po = new PurchaseOrder
            {
                CustomerId = contactId,
                PurchaseDate = purchaseDate
            };
            return this.CreateObject(po, _contactService);
        }

        public PurchaseOrder UpdateObject(PurchaseOrder purchaseOrder, IContactService _contactService)
        {
            return (_validator.ValidUpdateObject(purchaseOrder, _contactService) ? _p.UpdateObject(purchaseOrder) : purchaseOrder);
        }

        public PurchaseOrder SoftDeleteObject(PurchaseOrder purchaseOrder, IPurchaseOrderDetailService _purchaseOrderDetailService)
        {
            return (_validator.ValidDeleteObject(purchaseOrder, _purchaseOrderDetailService) ? _p.SoftDeleteObject(purchaseOrder) : purchaseOrder);
        }

        public bool DeleteObject(int Id)
        {
            return _p.DeleteObject(Id);
        }

        public PurchaseOrder ConfirmObject(PurchaseOrder purchaseOrder, IPurchaseOrderDetailService _purchaseOrderDetailService,
                                    IStockMutationService _stockMutationService, IItemService _itemService)
        {
            if (_validator.ValidConfirmObject(purchaseOrder, _purchaseOrderDetailService))
            {
                _p.ConfirmObject(purchaseOrder);
                IList<PurchaseOrderDetail> details = _purchaseOrderDetailService.GetObjectsByPurchaseOrderId(purchaseOrder.Id);
                foreach (var detail in details)
                {
                    _purchaseOrderDetailService.ConfirmObject(detail, _stockMutationService, _itemService);
                }
            }
            return purchaseOrder;
        }

        public PurchaseOrder UnconfirmObject(PurchaseOrder purchaseOrder, IPurchaseOrderDetailService _purchaseOrderDetailService,
                                    IPurchaseReceivalDetailService _purchaseReceivalDetailService, IStockMutationService _stockMutationService, IItemService _itemService)
        {
            if (_validator.ValidUnconfirmObject(purchaseOrder, _purchaseOrderDetailService, _purchaseReceivalDetailService, _itemService))
            {
                _p.UnconfirmObject(purchaseOrder);
                IList<PurchaseOrderDetail> details = _purchaseOrderDetailService.GetObjectsByPurchaseOrderId(purchaseOrder.Id);
                foreach (var detail in details)
                {
                    _purchaseOrderDetailService.UnconfirmObject(detail, _purchaseReceivalDetailService, _stockMutationService, _itemService);
                }
            }
            return purchaseOrder;
        }
    }
}