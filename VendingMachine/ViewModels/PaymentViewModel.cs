using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineTutorial.ViewModels
{
    public class PaymentViewModel : ObservableObject
    {
        //Informação do cliente
        private double _total;
        private double _inserted;
        private double _change;

        //Informação da máquina
        private double _bankTotal;

        public double Total
        {
            get
            {
                return _total;
            }
            set
            {
                _total = value;
                OnPropertyChanged("Total");
            }
        }

        public double Inserted
        {
            get
            {
                return _inserted;
            }
            set
            {
                _inserted = value;
                OnPropertyChanged("Inserted"); // dinheiro inserido
            }
        }

        public double Change
        {
            get
            {
                return _change;
            }
            set
            {
                _change = value;
                OnPropertyChanged("Change"); // troco
            }
        }

        public double BankTotal
        {
            get
            {
                return _bankTotal;
            }
            set
            {
                _bankTotal = value;
                OnPropertyChanged("BankTotal");
            }
        }

        public PaymentViewModel()
        {
            Total = 0;
            Inserted = 0;
            Change = 0;
            BankTotal = 0;
        }

        //Insere o valor monetário
        public void Insert(double value)
        {
            Inserted += value;
        }

        //Set the total the requested item costs
        public void SelectedPrice(double value)
        {
            Total = value;
        }

        //Confirma que o pagamento pode ser feito
        public bool Confirm()
        {
            if (Inserted >= Total)
                return true;

            return false;
        }

        //Finaliza o pagamento
        public void Pay()
        {
            Change = Total - Inserted;
            BankTotal += Total;
            Inserted = 0;
            Total = 0;
        }

        public void Collect()
        {
            Console.WriteLine("Collected Payments: " + BankTotal + " €");
            BankTotal = 0;
        }

    }
}
