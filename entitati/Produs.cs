using System;
using System.Xml;
using System.Xml.Serialization;

namespace entitati {
    [Serializable]
    [XmlRoot("ProdusParticularizat")]
    public class Produs : ProdusAbstract {
        private String? producator;
        private String? categorie;

        public Produs() : base() {
            this.producator = "";
            this.categorie = "";
        }

        public Produs(Int32 id, String? nume, String? codIntern, String? producator, Double pret, String? categorie) : base(id, nume, codIntern, pret) {
            this.producator = producator!;
            this.categorie = categorie!;
        }

        public override bool Equals(object? obj) {
            return obj is Produs produs &&
                   base.Equals(produs) &&
                   producator == produs.producator;
        }

        public override int GetHashCode() {
            return HashCode.Combine(base.GetHashCode(), producator);
        }

        public override string Descriere() {
            return $"Produsul [{Id}]: {Nume}, cod intern: [{CodIntern}], " +
                   $"producător: [{Producator}], preț: [{Pret} lei], categorie: [{Categorie}]";
        }

        public override string AltaDescriere() {
            return "Produsul " + base.AltaDescriere() + " producător: [" + Producator + "] categorie: [" + Categorie + "]";
        }

        public override void ReadFields(int id, bool readPrice = true) {
            base.ReadFields(id, readPrice);
            bool fieldsRead;
            void showPrompt(string prompt) {
                Console.Write($"\t[{id}] (" + this.GetType().Name + $") Introdu {prompt}: ");
            }

            do {
                showPrompt("producător"); Producator = Console.ReadLine();
                showPrompt("categorie"); Categorie = Console.ReadLine();

                fieldsRead = !String.IsNullOrEmpty(Producator) && !String.IsNullOrWhiteSpace(Producator);
                fieldsRead = !String.IsNullOrEmpty(Categorie) && !String.IsNullOrWhiteSpace(Categorie);

                if (!fieldsRead) {
                    Console.WriteLine("\t\tEroare! Una dintre valorile introduse este vidă.");
                }
            } while (!fieldsRead);
        }

        public override bool CanBeAddedToPackage(Pachet pachet)
        => pachet.OfTypeCount<Produs>() < pachet.MaxProduse || pachet.MaxProduse == 0;

        [XmlElement("ProducatorProdus")]
        public String? Producator { get => producator!; set => producator = value; }
        [XmlElement("CategorieProdus")]
        public String? Categorie { get => categorie!; set => categorie = value; }
    }
}