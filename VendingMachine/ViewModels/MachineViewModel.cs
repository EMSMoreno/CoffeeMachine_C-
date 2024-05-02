using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineTutorial.ViewModels
{
    public class MachineViewModel : ObservableObject
    {
        public ObservableCollection<ProductViewModel> Items { get; private set; }
        public PaymentViewModel Bank { get; private set; }

        public MachineViewModel()
        {
            Bank = new PaymentViewModel();
            Items = new ObservableCollection<ProductViewModel>()
            {
                new ProductViewModel(1, "Café", 0.25),
                new ProductViewModel(2, "Cappuccino", 0.30),
                new ProductViewModel(3, "Chocolate", 0.35),
                new ProductViewModel(4, "Chá", 0.20),
            };
        }

        public void Purchase(object item)
        {
            var requestedItem = item as ProductViewModel;
            Bank.SelectedPrice(requestedItem.Information.Price);

            if(Bank.Confirm())
            {
                if(requestedItem.Dispense())
                {
                    Bank.Pay();
                    Console.WriteLine("Obrigado pela compra!");
                }
            }
        }

        public void InsertChange(double value)
        {
            Bank.Insert(value);
        }

        public void CollectPayments()
        {
            Bank.Collect();
        }

        public void Refill()
        {
            foreach(var i in Items)
            {
                i.Refill();
            }
            Console.WriteLine("A máquina foi reabastecida de stock!");
        }

        public void Empty()
        {
            foreach (var i in Items)
            {
                i.Empty();
            }
            Console.WriteLine("A máquina foi esvaziada!");
        }
    }
}
