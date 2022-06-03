using System;
using System.Xml;
using System.Xml.Serialization;

namespace entitati {
    [Serializable]
    [XmlRoot("ServiciuParticularizat")]
    public class Serviciu : ProdusAbstract {
        public Serviciu() : base() {}

        public Serviciu(Int32 id, String? nume, String? codIntern, Double pret) : base(id, nume, codIntern, pret) {}

        public override bool Equals(object? obj) {
            return obj is Serviciu serviciu &&
                   base.Equals(serviciu);
        }

        public override int GetHashCode() {
            return HashCode.Combine(base.GetHashCode());
        }

        public override string Descriere() {
            return $"Serviciul [{Id}]: {Nume}, cod intern: [{CodIntern}], preț: [{Pret} lei]";
        }

        public override string AltaDescriere() {
            return "Serviciul " + base.AltaDescriere();
        }

        public override void ReadFields(int id, bool readPrice = true) {
            base.ReadFields(id, readPrice);
        }

        public override bool CanBeAddedToPackage(Pachet pachet)
        => pachet.OfTypeCount<Serviciu>() < pachet.MaxServicii || pachet.MaxServicii == 0;
    }
}
