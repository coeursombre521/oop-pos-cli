using System;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

namespace entitati {
    [Serializable]
    [XmlRoot("PachetParticularizat")]
    public class Pachet : ProdusAbstract {
        private ListOfPackageable elemPachet = new();
        private int maxProduse = 0;
        private int maxServicii = 0;

        public Pachet() : base() {}

        public Pachet(Int32 id, String? nume, String? codIntern, Double pret) : base(id, nume, codIntern, pret) {
            this.maxProduse = 0;
            this.maxServicii = 0;
        }

        public Pachet(Int32 id, String? nume, String? codIntern, Double pret, int maxProduse, int maxServicii) : base(id, nume, codIntern, pret) {
            this.maxProduse = maxProduse;
            this.maxServicii = maxServicii;
        }

        public void AddToPackage(IPackageable element) {
            if (element.CanBeAddedToPackage(this)) {
                elemPachet!.Add(element);
                if (element is ProdusAbstract @obj) {
                    Pret += @obj.Pret;
                }
            }
            else {
                throw new PackageElementException("Elementul nu poate fi adăugat în pachet.");
            }
        }

        public override string Descriere() {
            string fmt = $"Pachetul [{Id}]: {Nume}, cod intern: [{CodIntern}], preț: [{Pret} lei], conține:";
            foreach (ProdusAbstract i in elemPachet!) {
                fmt = fmt + "\n\t\t" + i.Descriere();
            }
            return fmt;
        }

        public override string AltaDescriere() {
            string fmt = $"Pachetul {base.AltaDescriere()},  conține:";
            foreach (ProdusAbstract i in elemPachet!) {
                fmt = fmt + "\n\t\t" + i.Descriere();
            }
            return fmt;
        }

        public override void ReadFields(int id, bool readPrice = false) {
            base.ReadFields(id, readPrice);
            void showPrompt(string prompt) {
                Console.Write($"\t[{id}] (" + this.GetType().Name + $") Câte {prompt} pot fi maximum în pachet? ");
            }

            showPrompt("produse");
            _ = int.TryParse(Console.ReadLine(), out this.maxProduse);

            if (MaxProduse < 0) MaxProduse = 0;
            if (MaxProduse == 0) {
                Console.WriteLine("\t\tNu există o constrângere pentru numărul de produse.");
            }

            showPrompt("servicii");
            _ = int.TryParse(Console.ReadLine(), out this.maxServicii);

            if (MaxServicii < 0) MaxServicii = 0;
            if (MaxServicii == 0) {
                Console.WriteLine("\t\tNu există o constrângere pentru numărul de servicii.");
            }
        }

        public void Sort() => elemPachet!.Sort();

        public override bool CanBeAddedToPackage(Pachet pachet) => false;

        public int OfTypeCount<T>() where T: ProdusAbstract, IPackageable => this.elemPachet!.OfType<T>().Count();

        [XmlElement("Packageables")]
        public ListOfPackageable ElemPachet { get => elemPachet; set => elemPachet = value; }
        [XmlElement("PachetMaximumProduse")]
        public int MaxProduse { get => this.maxProduse; set => this.maxProduse = value; }
        [XmlElement("PachetMaximumServicii")]
        public int MaxServicii { get => this.maxServicii; set => this.maxServicii = value; }
    }
}