using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce
{
    public class Prodotto
    {
        public int id { get; set; }
        public int qta { get; set; }
        public string nome { get; set; }

        public int prezzo { get; set; }
        public string descrizione { get; set; }
        public string imgUrl { get; set; }


        public Prodotto() { }
        public Prodotto(int id ,int qta, string nome, int prezzo, string descrizione, string imgUrl )
        {
            this.id = id;
            this.qta = qta;
            this.nome = nome;
            this.prezzo = prezzo;
            this.descrizione = descrizione;
            this.imgUrl = imgUrl;
            
           
        }
    }
}