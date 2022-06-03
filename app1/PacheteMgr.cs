using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text.Json;
using entitati;

namespace app1 {
    public class PacheteMgr : ProduseAbstractMgr {
        public Int32 ReadPachet() {
            Pachet? tmp = null;
            bool found = false;
            do {
                found = false;
                tmp = new Pachet(elemente!.Count, "", "", 0.0, 0, 0);
                tmp.ReadFields(elemente.Count);

                if (found = elemente.Contains(tmp!)) {
                    Console.WriteLine("\t\tEroare! Pachetul există deja!");
                }
            } while (found);

            elemente.Add(tmp!);

            ProduseMgr pMgr = new();
            ServiciiMgr sMgr = new();

            Console.Write($"\t[{elemente.Count}] Câte produse în {tmp.Nume}? ");
            _ = int.TryParse(Console.ReadLine(), out int nrProd);
            Console.Write($"\t[{elemente.Count}] Câte servicii în {tmp.Nume}? ");
            _ = int.TryParse(Console.ReadLine(), out int nrServ);

            var prodIds = pMgr.ReadProduse(nrProd);
            var servIds = sMgr.ReadServicii(nrServ);

            foreach (var i in prodIds) {
                try {
                    tmp.AddToPackage(GetProdusAbstractById(i));
                }
                catch (PackageElementException) {}
            }

            foreach (var i in servIds) {
                try {
                    tmp.AddToPackage(GetProdusAbstractById(i));
                }
                catch (PackageElementException) {}
            }

            return tmp.Id;
        }

        public List<Int32> ReadPachete(int n) {
            List<Int32> l = new();
            for (int i = 0; i < n; ++i) {
                l.Add(ReadPachet());
            }
            return l;
        }

        public void AddToPachet(int id, IPackageable item) {
            if (GetProdusAbstractById(id) is Pachet obj) {
                try {
                    obj.AddToPackage(item);
                } catch (PackageElementException) {
                    throw new PackageElementException("Elementul nu se poate adăuga în pachet.");
                }
            }
            else {
                throw new InvalidOperationException("Elementul cu ID-ul ales nu este pachet.");
            }
        }
    }
}