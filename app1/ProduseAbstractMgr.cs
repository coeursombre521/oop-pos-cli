using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Text.Json;
using entitati;

namespace app1 {
    public abstract class ProduseAbstractMgr : FiltruCriterii<ProdusAbstract> {
        private protected static List<ProdusAbstract>? elemente = new();

        public void Sort() {
            try {
                elemente!.Sort();
            } catch (NotImplementedException e) {
                Console.WriteLine(e.Message);
            }
        }

        private Type[] GetDerivedTypes() {
            var types =
                from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                where type.IsSubclassOf(typeof(ProdusAbstract)) && !type.IsAbstract
                select type;
            return types.ToArray();
        }

        public void LoadFromXML(string filename) {
            Type[] types = GetDerivedTypes();
            XmlSerializer xs = new(typeof(List<ProdusAbstract>), types);
            try {
                FileStream fs = File.OpenRead(filename);
                elemente = (List<ProdusAbstract>) xs.Deserialize(fs)!;
                fs.Close();
            } catch (IOException e) {
                Console.WriteLine($"Citirea fișierului XML a eșuat.\n{e.Message}");
                return;
            };
        }

        public void LoadFromJSON(string filename) {
            try {
                FileStream fs = File.OpenRead(filename);
                List<Produs>? tmp = JsonSerializer.Deserialize<List<Produs>>(fs);
                foreach (var i in tmp!) {
                    elemente!.Add(i);
                }
                fs.Close();
                tmp = null;
            } catch (IOException e) {
                Console.WriteLine($"Citirea fișierului JSON a eșuat.\n{e.Message}");
                return;
            }   
        }

        public void SaveToXML(string filename) {
            Type[] types = GetDerivedTypes();
            XmlSerializer xs = new(typeof(List<ProdusAbstract>), types);
            try {
                using FileStream fs = File.Create(filename);
                using (var xmlWriter = XmlWriter.Create(fs, new XmlWriterSettings()
                {
                    Indent = true,
                    IndentChars = "    "
                })) {
                    xs.Serialize(xmlWriter, elemente);
                }
                fs.Close();
            } catch (IOException e) {
                Console.WriteLine($"Crearea fișierului XML a eșuat.\n{e.Message}");
                return;
            };
        }

        public void SaveToJSON(string filename) {
            try {
                FileStream fs = File.Create(filename);
                JsonSerializer.Serialize(fs, elemente!.OfType<Produs>(), new JsonSerializerOptions() {
                    WriteIndented = true
                });
                fs.Close();
            } catch (IOException e) {
                Console.WriteLine($"Crearea fișierului JSON a eșuat.\n{e.Message}");
                return;
            }
        }

        public ProdusAbstract GetProdusAbstractById(Int32 id)
        => elemente!.Find(x => x.Id == id)!;

        public IEnumerable<ProdusAbstract> FiltrarePret(double pret, int compare) {
            CriteriuPret critPret = new(pret, compare);
            return base.Filtrare(elemente!, critPret);
        }
        
        public void Write2Console() {
            foreach (var i in elemente!) {
                Console.WriteLine("\t" + i.Descriere());
            }
            Console.Write("\n");
        }
    }
}