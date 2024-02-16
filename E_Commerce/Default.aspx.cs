using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce
{
    public partial class _Default : Page
    {






        protected void Page_Load(object sender, EventArgs e)
        {
            //Al caricamento della pagona controllo la presenza del Cookie di login altrimenti redirect al login
            if (Request.Cookies["LOGIN_COOKIE"] != null)
            {
                //Svuoto i prodotti e riempio con quelli presenti nella classe statica ListaProdottiCustom
                ContenitoreProdotti.InnerHtml = "";

               
                foreach (Prodotto Prodotto in ListaProdottiCustom.listaProdotti)
                {
                    ContenitoreProdotti.InnerHtml += GenerateProductHtml(Prodotto);
                }
            }
            else
            {
                Response.Redirect("Login");
            }


            //controllo la presenza di u Id prodotto nella QueryString
            //Se è presente un idProdotto aggiorno il cookie carrello
            string idProdotto = Request.QueryString["idProdotto"];

            if (idProdotto != null)
            {
                UpdateCart(idProdotto);
            }
        }


        //metodo per storare la card dei prodotti
        private string GenerateProductHtml(Prodotto prodotto)
        {
            return $"<div class=\"col\">\r\n" +
                   $"<div class=\"card border border-3 border-success\" style=\"width: 18rem; height: 350px\">\r\n" +
                   $"<img class=\"card-img-top h-50\" src=\"{prodotto.imgUrl}\" alt=\"fruit\">\r\n" +
                   $"<div class=\"card-body \">\r\n" +
                   $"<h4>{prodotto.nome}</h4>\r\n" +
                   $"<h5>{prodotto.prezzo}€</h5>\r\n" +
                   $"<a href=\'Default?idProdotto={prodotto.id}\' class=\"btn btn-primary\"> <i class=\"bi bi-cart-plus\"></i> Add to Cart</a>\r\n" +
                   $"<a href=\'Dettagli?idProdotto={prodotto.id}\' class=\"btn btn-success  \"><i class=\"bi bi-search\"></i> Details</a>\r\n" +
                   $"</div>\r\n" +
                   $"</div>\r\n" +
                   $"</div>";
        }


        //metodo che aggiorna il cookie "cart" 
        private void UpdateCart(string idProdotto)
        {
            //serializer per trasformare in json
            JavaScriptSerializer ser = new JavaScriptSerializer();
            //lista di oggetti custom "Prodotto" 
            List<Prodotto> cart;


            //se c'è un cookie carrello lo prende e lo deserializza
            //se non c'è riempie card con una lista di oggetti Prodotto
            if (Request.Cookies["CART_COOKIE"] != null && Request.Cookies["CART_COOKIE"]["cart"] != null)
            {
                string list = Request.Cookies["CART_COOKIE"]["cart"];
                cart = ser.Deserialize<List<Prodotto>>(list);
            }
            else
            {
                
                cart = new List<Prodotto>();
            }


            //controllo se il prodotto è già presente nel cookie "cart"
            
            bool prodottoTrovato = false;

            foreach (Prodotto prodotto in cart)
            {
                //Se è presente ne aumento la qta di 1
                if (prodotto.id.ToString() == idProdotto)
                {
                    prodotto.qta += 1;
                    Response.Write($"Frutto {prodotto.nome} aggiunto con successo"  );
                    prodottoTrovato = true;

                    break;
                }
            }

            //Se non lo trova allora aggiunge l'oggetto prodotto corrispondente in "ListaProdottiCustom.listaProdotti"
            //all'interno della lista
            if (!prodottoTrovato)
            {
                cart.Add((Prodotto)ListaProdottiCustom.listaProdotti[int.Parse(idProdotto) - 1]);
            }

            //alla fine serializza in json "cart" e aggiorno il cookie
            string cartJson = ser.Serialize(cart);
            HttpCookie cartCookie = new HttpCookie("CART_COOKIE");
            cartCookie.Values["cart"] = cartJson;
            Response.Cookies.Add(cartCookie);
        }

    }
}