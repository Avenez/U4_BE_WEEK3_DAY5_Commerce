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
    public partial class Carrello : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {

            //Controllo Cooki login
            if (Request.Cookies["LOGIN_COOKIE"] != null)
            {
                ContenitoreProdottiCarrello.InnerHtml = "";

                //Inizializzo serializzatore
                JavaScriptSerializer ser = new JavaScriptSerializer();
                List<Prodotto> cart;


                //controllo la presenza del cookie "cart"
                if (Request.Cookies["CART_COOKIE"] != null && Request.Cookies["CART_COOKIE"]["cart"] != null)
                {
                    //se presente lo trasformo per usarlo
                    string list = Request.Cookies["CART_COOKIE"]["cart"];
                    cart = ser.Deserialize<List<Prodotto>>(list);
                }
                else
                {
                    
                    cart = new List<Prodotto>();
                }




                int totaleCarrello = 0;

                //Creo il totale del carrello
                foreach (Prodotto prodotto in cart)
                {
                    totaleCarrello += prodotto.qta * prodotto.prezzo;
                }

                contenitoreTot.InnerText = totaleCarrello.ToString() + " €";


                //Aggiungo le card dei prodotti
                foreach (Prodotto prodotto in cart)
                {
                    ContenitoreProdottiCarrello.InnerHtml += GenerateProductHtml(prodotto);
                }

                //Controllo di Aggiunta o Sottrazione dei prodotti
                //Se la querystring Sum o Sub è rpesente, eseguo il relativo comando
                string idProdottoSum = Request.QueryString["idProdottoSum"];
                if (idProdottoSum != null)
                {
                    UpdateCart(cart, idProdottoSum, true);
                    UpdateCartCookie(cart);
                    Response.Redirect("Carrello");
                }

                string idProdottoSub = Request.QueryString["idProdottoSub"];
                if (idProdottoSub != null)
                {
                    UpdateCart(cart, idProdottoSub, false);
                    UpdateCartCookie(cart);
                    Response.Redirect("Carrello");
                }
            }
            else
            {
                Response.Redirect("Login");
            }
        }


        //Card dei prodotti
        private string GenerateProductHtml(Prodotto prodotto)
        {
            return $"<div class=\"col\">\r\n" +
                   $"<div class=\"card border border-3 border-success\" style=\"width: 18rem; height: 350px\">\r\n" +
                   $"<img class=\"card-img-top h-50\" src=\"{prodotto.imgUrl}\" alt=\"fruit\">\r\n" +
                   $"<div class=\"card-body \">\r\n" +
                   $"<h4>{prodotto.qta} x {prodotto.nome}</h4>\r\n" +
                   $"<div class=\"d-flex \">\r\n" +
                   $"<a  href=\'Carrello?idProdottoSub={prodotto.id}\' class=\"btn btn-danger me-3\"> - </a>\r\n" +
                   $"<h5 class=\"me-3 \" >{prodotto.prezzo * prodotto.qta}€</h5>\r\n" +
                   $"<a href=\'Carrello?idProdottoSum={prodotto.id}\' class=\"btn btn-success\"> + </a>\r\n" +
                   $"</div>\r\n" +
                   $"</div>\r\n" +
                   $"</div>\r\n" +
                   $"</div>";
        }


        //Il metodo prende in input la lista, l'Id prodotto ed un booleano per decidere se sommare o sottrarre
        private void UpdateCart(List<Prodotto> cart, string productId, bool add)
        {
            //Cerca quale prodotto abbiamo selezionato
            foreach (Prodotto prodotto in cart)
            {
                if (prodotto.id.ToString() == productId)
                {
                    if (add)
                    {
                        prodotto.qta += 1;
                    }
                    else
                    {
                        //se la qta dell'oggetto è uguale o minore di 0 lo rimuove
                        prodotto.qta -= 1;
                        if (prodotto.qta <= 0)
                        {
                            cart.Remove(prodotto);
                        }
                    }
                    break;
                }
            }
        }


        //Metodo che aggiorna il cookie
        private void UpdateCartCookie(List<Prodotto> cart)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            string cartJson = ser.Serialize(cart);
            HttpCookie cartCookie = new HttpCookie("CART_COOKIE");
            cartCookie.Values["cart"] = cartJson;
            Response.Cookies.Add(cartCookie);
        }

        //metodo che svuota il carrello creando un nuovo cookie "cart" vuoto e aggiornando quello presente
        protected void SvuotaCarrello_Click(object sender, EventArgs e)
        {
            if (Request.Cookies["CART_COOKIE"] != null)
            {
                ArrayList cart = new ArrayList();
                HttpCookie cartCookie = new HttpCookie("CART_COOKIE");

                //creo un sconverter in json
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string cartJson = serializer.Serialize(cart);

                //aggiungo il json al cookie
                cartCookie.Values["cart"] = cartJson;
                Response.Cookies.Add(cartCookie);
            }

            Response.Redirect("Carrello");

        }
    }
}