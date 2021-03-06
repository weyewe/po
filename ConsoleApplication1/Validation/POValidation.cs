﻿using ConsoleApp.DataAccess;
using Core.DomainModel;
using Core.Interface.Service;
using Core.Interface.Validation;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validation.Validation;

namespace ConsoleApp.Validation
{
    public class POValidation
    {
        private PurchaseOrderValidator pov;
        private PurchaseOrderDetailValidator podv;
        private IContactService _c;
        private IItemService _i;
        private IPurchaseOrderService _po;
        private IPurchaseOrderDetailService _pod;
        private IPurchaseReceivalService _pr;
        private IPurchaseReceivalDetailService _prd;
        private ISalesOrderService _so;
        private ISalesOrderDetailService _sod;
        private IDeliveryOrderService _do;
        private IDeliveryOrderDetailService _dod;
        private IStockMutationService _sm;

        public POValidation(PurchaseOrderValidator _pov, PurchaseOrderDetailValidator _podv, IContactService c, IItemService i, IStockMutationService sm,
                                 IPurchaseOrderService po, IPurchaseReceivalService pr,
                                 ISalesOrderService so, IDeliveryOrderService d,
                                 IPurchaseOrderDetailService pod, IPurchaseReceivalDetailService prd,
                                 ISalesOrderDetailService sod, IDeliveryOrderDetailService dod)
        {
            pov = _pov;
            podv = _podv;
            _c = c;
            _i = i;
            _sm = sm;
            _po = po;
            _pr = pr;
            _so = so;
            _do = d;
            _pod = pod;
            _prd = prd;
            _sod = sod;
            _dod = dod;            
        }

        public void POValidation12()
        {
            Console.WriteLine("[Test 12] Create valid Purchase Order for Michaelangelo");
            PurchaseOrder po = _po.CreateObject(_c.GetObjectByName("Michaelangelo Buanorotti").Id, DateTime.Now,_c);
            if (po.Errors.Any()) { Console.WriteLine(_po.GetValidator().PrintError(po)); }
        }

        public int POValidation13()
        {
            Console.WriteLine("[Test 13] Create valid Purchase Order Detail for Michaelangelo");
            PurchaseOrderDetail pod1 = _pod.CreateObject(_po.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault().Id, _i.GetObjectByName("Buku Tulis Kiky A5").Id, 100, (decimal) 35000.00, _po, _i);
            if (pod1.Errors.Any()) { Console.WriteLine(_pod.GetValidator().PrintError(pod1)); return 0; }
            return pod1.Id;
        }

        public void POValidation14()
        {
            Console.WriteLine("[Test 14] Create invalid Purchase Order (wrong contact id)");
            PurchaseOrder po = _po.CreateObject(0, DateTime.Now, _c);
            if (po.Errors.Any()) { Console.WriteLine(_po.GetValidator().PrintError(po)); }
        }

        public void POValidation15()
        {
            Console.WriteLine("[Test 15] Create valid Purchase Order");
            PurchaseOrder po = _po.CreateObject(_c.GetObjectByName("Andy Robinson").Id, new DateTime(2000, 2, 28), _c);
            if (po.Errors.Any()) { Console.WriteLine(_po.GetValidator().PrintError(po)); }
        }

        public int POValidation16()
        {
            Console.WriteLine("[Test 16] Create invalid Purchase Order Detail for Michaelangelo with wrong contact");
            PurchaseOrderDetail pod1 = _pod.CreateObject(0, _i.GetObjectByName("Mini Garuda Indonesia").Id, 100, (decimal) 111000.00, _po, _i);
            if (pod1.Errors.Any()) { Console.WriteLine(_pod.GetValidator().PrintError(pod1)); return 0; }
            return pod1.Id;
        }

        public int POValidation17()
        {
            Console.WriteLine("[Test 17] Create invalid Purchase Order Detail for Michaelangelo with exact same item");
            PurchaseOrderDetail pod1 = _pod.CreateObject(_po.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault().Id, _i.GetObjectByName("Buku Tulis Kiky A5").Id, 50, (decimal) 32000.00, _po, _i);
            if (pod1.Errors.Any()) { Console.WriteLine(_pod.GetValidator().PrintError(pod1)); return 0; }
            return pod1.Id;
        }

        public int POValidation18()
        {
            Console.WriteLine("[Test 18] Create valid Purchase Order Detail for Michaelangelo");
            PurchaseOrderDetail pod1 = _pod.CreateObject(_po.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault().Id, _i.GetObjectByName("Mini Garuda Indonesia").Id, 50, (decimal) 107000.00, _po, _i);
            if (pod1.Errors.Any()) { Console.WriteLine(_pod.GetValidator().PrintError(pod1)); return 0; }
            return pod1.Id;
        }

        public void POValidation19a()
        {
            Console.WriteLine("[Test 19a] Confirm PO and POD for Michaelangelo");
            PurchaseOrder po = _po.ConfirmObject(_po.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault(), _pod, _sm, _i);
            if (po.Errors.Any()) { Console.WriteLine(_po.GetValidator().PrintError(po)); }
        }

        public void POValidation19b()
        {
            Console.WriteLine("[Test 19b] Unconfirm PO and POD for Michaelangelo");
            PurchaseOrder po = _po.UnconfirmObject(_po.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault(), _pod, _prd, _sm, _i);
            if (po.Errors.Any()) { Console.WriteLine(_po.GetValidator().PrintError(po)); }
        }

        public void POValidation28()
        {
            Console.WriteLine("[Test 28] Unconfirm PO and POD for Michaelangelo that already has confirmed PR");
            PurchaseOrder po = _po.UnconfirmObject(_po.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault(), _pod, _prd, _sm, _i);
            if (po.Errors.Any()) { Console.WriteLine(_po.GetValidator().PrintError(po)); }
        }
    }
}
