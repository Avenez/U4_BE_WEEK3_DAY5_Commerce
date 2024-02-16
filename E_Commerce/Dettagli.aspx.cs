using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce
{
    public partial class Dettagli : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string idProdotto = Request.QueryString["idProdotto"];
            
            if (Request.Cookies["LOGIN_COOKIE"] != null)
            {
                //Svuoto i prodotti e riempio con quelli presenti nella classe statica ListaProdottiCustom
                ProductDetails.InnerHtml = "";

                Prodotto prodotto = (Prodotto)ListaProdottiCustom.listaProdotti[int.Parse(idProdotto) - 1];
                

                ProductDetails.InnerHtml += GenerateProductHtml(prodotto);
                
            }
            else
            {
                Response.Redirect("Login");
            }

            string idProdottoSum = Request.QueryString["idProdotto"];

            if (idProdottoSum != null)
            {
                UpdateCart(idProdotto);
            }

        }

        private string GenerateProductHtml(Prodotto prodotto)
        {
            return $"<div class=\"col-4\">\r\n" +
                        $"<img src=\"{prodotto.imgUrl}\" alt=\"Fruit Image\" style=\"width: 650px; height: 500px\" />\r\n" +
                            $"</div>\r\n" +
                                $"<div class=\"col\">\r\n" +
                                    $"<div class=\"row-col-1\">\r\n" +
                                        $"<div class=\"col-10\">\r\n" +
                                        $"<h1 class=\"display-1 text-white bg-dark ps-5 pb-2 rounded-pill\">{prodotto.nome}</h1>\r\n" +
                                        $"</div>\r\n" +

                                        $"<div class=\"col-5\">\r\n" +
                                        $"<h2 class=\"display-2 text-white bg-dark ps-5 pb-2 rounded-pill\">{prodotto.prezzo} €</h2>\r\n" +
                                        $"</div>\r\n" +

                                        $"<div class=\"col-12\">\r\n" +
                                        $"<h2 class=\"display-2 ps-5\">{prodotto.descrizione}</h2>\r\n" +
                                        $"</div>\r\n" +


                       
                            $"</div>\r\n\r\n" +
                            $"<a href=\'Dettagli?idProdotto={prodotto.id}&idProdottoSum={prodotto.id}\' class=\"btn btn-primary\"> <i class=\"bi bi-cart-plus\"></i> Add to Cart</a>\r\n" +
                    $"</div>";
        }

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
                    Response.Write($"Frutto {prodotto.nome} aggiunto con successo");
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