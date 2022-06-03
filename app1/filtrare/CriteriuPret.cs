using System;
using entitati;

namespace app1 {
    public class CriteriuPret : ICriteriu<ProdusAbstract> {
        private double pret;
        private int retType = 0;

        public CriteriuPret(double pret, int retType) {
            this.pret = pret;
            this.retType = retType;
        }

        public bool IsIndeplinit(ProdusAbstract item) {
            if (item is ProdusAbstract @abstract) {
                if (retType < 0) {
                    return @abstract.Pret < this.pret;
                }
                else if (retType == 0) {
                    return @abstract.Pret == this.pret;
                }
                else {
                    return @abstract.Pret > this.pret;
                }
            }
            else {
                return false;
            }
        }

        public double Pret { get => this.pret; set => this.pret = value; }
        public int RetType { get => this.retType; set => this.retType = value; }
    }
}