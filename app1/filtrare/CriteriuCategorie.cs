using System;
using entitati;

namespace app1 {
    public class CriteriuCategorie : ICriteriu<ProdusAbstract> {
        private string? categorie;

        public CriteriuCategorie(string? categorie) {
            this.categorie = categorie;
        }

        public bool IsIndeplinit(ProdusAbstract item) {
            if (item is Produs prod) {
                return prod.Categorie == this.categorie;
            }
            else {
                return false;
            }
        }

        public string? Categorie { get => this.categorie; set => this.categorie = value!; }
    }
}