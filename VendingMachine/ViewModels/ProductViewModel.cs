using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VendingMachineTutorial.Models;

namespace VendingMachineTutorial.ViewModels
{
    public class ProductViewModel : ObservableObject
    {
        private VendingItem _model;
        //campo privado que armazena o modelo do produto que é o que o "ProductViewModel" representa

        private const int _maxQuantity = 10;
        //campo privado que define o numero max de stock permitido para os produtos no "ProductViewModel" com um valor fixo de 10
        
        private int _quantity;
        //campo privato que armazena a qt atual de produtos no "ProductViewModel", portanto o que está em stock

        public int Id
        {
            get
            {
                return _model.Id; //retorna o id do produto
            }
        }

        public int Quantity
        {
            get
            {
                return _quantity; //retorna o valor da variável privada que representa a qt
            }
            private set
            {
                _quantity = value; //define o valor quando a propriedade é atribuída
                OnPropertyChanged("OutOfStockMessage");
                OnPropertyChanged("InventoryDisplay");
                OnPropertyChanged("Quantity");
                // estas três linhas de código são eventos que são usados para notificar qualquer outra parte do código que esteja a observar (observáveis) esta propriedade que houve uma alteração nos seus respetivos valores. As notificações são enviadas para as respetivas propriedades "OutOfStockMessage", "InventoryDisplay" e "Quantity", o que reage dinamicamente às mudanças feitas na qt de stock dos produtos.
            }
        }

        //retorna uma mensagem formatada com o nome do produto e a qt atual
        //exemplo: "Chocolate: 7 (em stock)"
        public string InventoryDisplay
        {
            get
            {
                return _model.Name + ": " + _quantity;
            }
        }

        //retorna uma cópia do modelo do produto
        public VendingItem Information
        {
            get
            {
                return _model;
            }
        }

        //determina se preciso de mostrar a mensagem "Fora de Stock"
        public Visibility OutOfStockMessage
        {
            get
            {
                if (Quantity > 0)
                    return Visibility.Hidden;

                return Visibility.Visible;
            }
        }

        public ProductViewModel(int id, string name, double price) 
            //construtor que recebe o id, o nome e o preço, inicia o modelo do produto com esses valores e define a qt inicial a zero.
        {
            _model = new VendingItem(id, name, price);
            Quantity = 0;
        }

        public int Refill() //método p/ reabastecer stock até ao max permitido e retorna qt de produtos adicionados
        {
            var amount = _maxQuantity - Quantity;
            Quantity += amount;
            return amount;
        }

        public int Empty() //método p/ esvaziar stock e retorna a qt de produtos removidos
        {
            var amount = Quantity;
            Quantity = 0;
            return amount;
        }

        public bool Dispense() //método p/ decrementar a qt em 1 e retorna true caso exista stock disponivel ou false se não tiver stock
        {
            if(Quantity > 0)
            {
                Quantity--;
                return true;
            }

            return false;
        }
    }
}
