using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce
{
    public static class ListaProdottiCustom
    {
        public static ArrayList listaProdotti = new ArrayList();

        static ListaProdottiCustom()
        {

            listaProdotti.Add(new Prodotto(1 ,1, "Arancia", 30 , "Un frutto ricco di vitamina C e aspro come la vita.", "./foto/arancia.jpg"));
            listaProdotti.Add(new Prodotto(2, 1, "Frutti Rossi", 20, "Frutti ricchi di antiossidanti. Peccato non siano velenosi.", "./foto/frutti_rossi.jpg"));
            listaProdotti.Add(new Prodotto(3, 1, "Kiwi", 10, "Un frutto che tutti dicono essere molto buono. La gente non capisce un ca...", "./foto/kiwi.jpg"));
            listaProdotti.Add(new Prodotto(4, 1, "Mango", 40, "Un frutto dal sapore incredibile e inconfondibile. Dio ti abbia in gloaria Solero al mango !", "./foto/mango.jpg"));
            listaProdotti.Add(new Prodotto(5, 1, "Mela", 15, "Un frutto basic. Se esiste qualcosa più vanilla della vaniglia, quella è la Mela.", "./foto/mela.jpg"));
            listaProdotti.Add(new Prodotto(6, 1, "Melone", 25, "Al 60% acqua è un frutto estivo. Si può dire sia la Zucca di Agosto.", "./foto/melone.jpg"));
            listaProdotti.Add(new Prodotto(7, 1, "Melone Cubico", 35, "Un frutto perfetto per tutti quelli che non sanno mangare il melone normale. Cosa non si inventano questi Giapponesi.", "./foto/melone_cubo.jpg"));
        }
    }
}