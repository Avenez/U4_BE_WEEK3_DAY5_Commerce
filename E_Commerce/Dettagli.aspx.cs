using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                    $"</div>";
        }

    }
    
}