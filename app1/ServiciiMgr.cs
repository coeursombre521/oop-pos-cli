using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text.Json;
using entitati;

namespace app1 {
    public class ServiciiMgr : ProduseAbstractMgr {
        public Int32 ReadServiciu() {
            Serviciu? tmp;
            bool found = false;
            do {
                found = false;
                tmp = new Serviciu(elemente!.Count, "", "", 0.0);
                tmp.ReadFields(elemente.Count);

                if (found = elemente.Contains(tmp!)) {
                    Console.WriteLine("\t\tEroare! Serviciul exista deja!");
                }
            } while (found);
            elemente.Add(tmp!);
            return tmp.Id;
        }

        public List<Int32> ReadServicii(int n) {
            List<Int32> l = new();
            for (int i = 0; i < n; ++i) {
                l.Add(ReadServiciu());
            }
            return l;
        }

        public void PrintServiciiCareIncepCu(String? str) {
            var inter =
                from item in elemente!.OfType<Serviciu>()
                where item.Nume!.StartsWith(str!)
                orderby item.Nume
                select item;

            foreach (var i in inter) {
                Console.WriteLine("\t" + i.Descriere());
            }
            Console.Write("\n");
        }

        public void PrintDoarServicii() {
            var inter =
                from item in elemente!.OfType<Serviciu>()
                select item;

            foreach (var i in inter) {
                Console.WriteLine("\t" + i.Descriere());
            }
            Console.Write("\n");
        }

        public void PrintServiciiCrescatorDupaPret() {
            var inter =
                from prod in elemente!.OfType<Serviciu>()
                orderby prod.Pret
                select prod;

            foreach (var i in inter) {
                Console.WriteLine("\t" + i.Descriere());
            }
            Console.Write("\n");
        }
    }
}