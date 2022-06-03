using System;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

namespace entitati {
    [Serializable]
    [XmlRoot("ArticolPosParticularizat")]
    public abstract class ProdusAbstract : IComparable<ProdusAbstract>, IPackageable {
        private Int32 id;
        private String? nume;
        private String? codIntern;
        private Double pret;

        public ProdusAbstract() {
            this.id = 0;
            this.nume = "";
            this.codIntern = "";
            this.pret = 0.0;
        }

        protected ProdusAbstract(Int32 id, String? nume, String? codIntern, Double pret) {
            this.id = id;
            this.nume = nume!;
            this.codIntern = codIntern!;
            this.pret = pret;
        }

        public override bool Equals(object? obj) {
            return obj is ProdusAbstract @abstract &&
                   nume == @abstract.nume &&
                   codIntern == @abstract.codIntern;
        }

        public override int GetHashCode() {
            return HashCode.Combine(id, nume, codIntern);
        }

        public int CompareTo(ProdusAbstract? obj) {
            int ret = -1;
            if (obj is ProdusAbstract @abstract) {
                ret = String.Compare(this.nume, @abstract.nume);
            }
            else {
                throw new ArgumentException("Obiectul nu face parte din clasa ProdusAbstract.");
            }
            return ret;
        }

        public abstract string Descriere();

        public virtual string AltaDescriere() {
            return $"[{Id}]: {Nume}, cod intern: [{CodIntern}], preț: [{Pret}]";
        }

        public virtual void ReadFields(Int32 id, bool readPrice) {
            bool fieldsRead = false;
            void showPrompt(string prompt) {
                Console.Write($"\t[{id}] (" + this.GetType().Name + $") Introdu {prompt}: ");
            }

            do {
                Id = id;
                showPrompt("nume"); Nume = Console.ReadLine();
                showPrompt("cod intern"); CodIntern = Console.ReadLine();
                if (readPrice) {
                    showPrompt("preț"); _ = Double.TryParse(Console.ReadLine(), out this.pret);
                }
                else {
                    Pret = 0;
                }

                fieldsRead = !String.IsNullOrEmpty(Nume) && !String.IsNullOrWhiteSpace(Nume);
                fieldsRead = !String.IsNullOrEmpty(CodIntern) && !String.IsNullOrWhiteSpace(CodIntern);

                if (!fieldsRead) {
                    Console.WriteLine("\t\tEroare! Una dintre valorile introduse este vidă.");
                }
            } while (!fieldsRead);
        }

        public abstract bool CanBeAddedToPackage(Pachet pachet);

        [XmlElement("PosId")]
        public Int32 Id { get => id; set => id = value; }
        [XmlElement("PosNume")]
        public String? Nume { get => nume!; set => nume = value; }
        [XmlElement("PosCodIntern")]
        public String? CodIntern { get => codIntern!; set => codIntern = value; }
        [XmlElement("PosPret")]
        public Double Pret { get => pret; set => pret = value; }
    }
}