﻿using Core.DomainModel;
using Core.Interface.Service;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.DataAccess
{
    class StockMutationDb
    {
        public static void Delete(StockControlEntities db, IStockMutationService _sm)
        {
            var items = _sm.GetAll();

            foreach (var item in items)
            {
                _sm.DeleteObject(item.Id);
            }
        }

        public static void Display(StockControlEntities db, IStockMutationService _sm)
        {
            var items = _sm.GetAll();

            Console.WriteLine("All items in the database:");
            foreach (var item in items)
            {
                Console.WriteLine("Stock Mutation for: " + item.ItemId + ", Mutation Case: " + item.Status + ", Item Case: " + item.ItemCase);
            }
            Console.WriteLine();
        }
    }
}
