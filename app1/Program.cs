using System;
using System.Runtime.InteropServices;
using entitati;

namespace app1 {
    public class Program {
        public static void Main() {
            // în Windows, diacriticele în consolă nu se pot afișa
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
            }

            int choice;
            do {
                PosMain.ShowMenu();
                choice = PosMain.GetUserChoice();
                PosMain.ExecuteTaskForChoice(choice);
            } while (choice != PosMain.EXIT_CHOICE);
        }
    }
}