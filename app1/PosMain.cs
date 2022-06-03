using System;
using entitati;

namespace app1 {
    public static class PosMain {
        public const int EXIT_CHOICE = 8;
        private static readonly ProduseMgr prodMgr = new();
        private static readonly ServiciiMgr servMgr = new();
        private static readonly PacheteMgr pacMgr = new();

        public static void ShowMenu() {
            string fmt =
                "Selectați o opțiune:\n\n" +
                "1.  Introduceți (de la tastatură) unul sau mai multe produse.\n" +
                "2.  Introduceți (de la tastatură) unul sau mai multe servicii.\n" +
                "3.  Introduceți (de la tastatură) unul sau mai multe pachete.\n" +
                "4.  Încărcați dintr-un XML lista de articole.\n" +
                "5.  Salvați într-un XML lista de articole.\n" +
                "6.  Afișați lista de articole.\n" +
                "7.  Afișați un element având un ID.\n" +
                "8.  Ieșire din program.\n\n";
            Console.Write(fmt);
        }

        public static int GetUserChoice() {
            Console.Write(">>> ");
            bool ret = int.TryParse(Console.ReadLine(), out int choice);
            return ret ? choice : -1;
        }

        public static void ExecuteTaskForChoice(int choice) {
            bool retParse;
            string filename;
            int objId;
            switch(choice) {
                // orice altă alegere
                default:
                    Console.WriteLine("\nAlegere invalidă.\n");
                    break;
                // citire produse
                case 1:
                    Console.Write("\nCâte produse? ");
                    retParse = int.TryParse(Console.ReadLine(), out int nrProd);
                    if (!retParse) {
                        Console.WriteLine("Valoare invalidă. Se va citi un produs.");
                        nrProd = 1;
                    }
                    _ = prodMgr.ReadProduse(nrProd);
                    break;
                // citire servicii
                case 2:
                    Console.Write("\nCâte servicii? ");
                    retParse = int.TryParse(Console.ReadLine(), out int nrServ);
                    if (!retParse) {
                        Console.WriteLine("Valoare invalidă. Se va citi un serviciu.");
                        nrServ = 1;
                    }
                    _ = servMgr.ReadServicii(nrServ);
                    break;
                // citire pachete
                case 3:
                    Console.Write("\nCâte pachete? ");
                    retParse = int.TryParse(Console.ReadLine(), out int nrPac);
                    if (!retParse) {
                        Console.WriteLine("Valoare invalidă. Se va citi un pachet.");
                        nrPac = 1;
                    }
                    _ = pacMgr.ReadPachete(nrPac);
                    break;
                // citire din XML
                case 4:
                    Console.Write("\nCum se numește fișierul? ");
                    filename = Console.ReadLine()!;
                    prodMgr.LoadFromXML(filename);
                    break;
                // salvare în XML
                case 5:
                    Console.Write("\nCum se va numi fișierul? ");
                    filename = Console.ReadLine()!;
                    prodMgr.SaveToXML(filename);
                    break;
                // afișare listă
                case 6:
                    Console.Write("\n");
                    prodMgr.Write2Console();
                    break;
                // afișare după ID
                case 7:
                    Console.Write("\nIntroduceți un ID: ");
                    retParse = int.TryParse(Console.ReadLine(), out objId);
                    if (!retParse) {
                        Console.WriteLine("Valoare invalidă. Se va afișa primul ID (0).");
                        objId = 0;
                    }
                    try {
                        var pa = prodMgr.GetProdusAbstractById(objId);
                        if (pa != null) {
                            Console.Write($"\n{pa.Descriere()}\n\n");
                        }
                        else {
                            Console.WriteLine("Element negăsit.\n");
                        }
                    } catch (NullReferenceException) {
                        Console.WriteLine("Argument invalid.\n");
                    }
                    break;
                // ceau!
                case EXIT_CHOICE:
                    Console.WriteLine("\nLa revedere!");
                    break;
            }
        }
    }
}