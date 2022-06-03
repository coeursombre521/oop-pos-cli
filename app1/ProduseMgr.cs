using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text.Json;
using entitati;

namespace app1 {
    public class ProduseMgr : ProduseAbstractMgr {
        public Int32 ReadProdus() {
            Produs? tmp;
            bool found = false;
            do {
                found = false;
                tmp = new Produs(elemente!.Count, "", "", "", 0.0, "");
                tmp.ReadFields(elemente.Count);

                if (found = elemente.Contains(tmp!)) {
                    Console.WriteLine("\t\tEroare! Produsul exista deja!");
                }
            } while (found);
            elemente.Add(tmp!);
            return tmp.Id;
        }

        public List<Int32> ReadProduse(int n) {
            List<Int32> l = new();
            for (int i = 0; i < n; ++i) {
                l.Add(ReadProdus());
            }
            return l;
        }

        public IEnumerable<ProdusAbstract> FiltrareCategorie(string categorie) {
            CriteriuCategorie critCateg = new(categorie);
            return base.Filtrare(elemente!, critCateg);
        }

        public void PrintProduseCareIncepCu(String? str) {
            var inter =
                from item in elemente!.OfType<Produs>()
                where item.Nume!.StartsWith(str!)
                orderby item.Nume
                select item;

            foreach (var i in inter) {
                Console.WriteLine("\t" + i.Descriere());
            }
            Console.Write("\n");
        }

        public void PrintProduseDupaCategorie() {
            var inter =
                from prod in elemente!.OfType<Produs>()
                group prod by prod.Categorie into newProd
                orderby newProd.Key
                select newProd;

            foreach (var i in inter) {
                Console.WriteLine("\tCategoria: " + i.Key);
                foreach (var j in i) {
                    Console.WriteLine("\t\t" + j.Descriere());
                }
                Console.Write("\n");
            }
        }

        public void PrintProduseDescrescatorDupaPret() {
            var inter =
                from prod in elemente!.OfType<Produs>()
                orderby prod.Pret descending
                select prod;

            foreach (var i in inter) {
                Console.WriteLine("\t" + i.Descriere());
            }
            Console.Write("\n");
        }
    }
}