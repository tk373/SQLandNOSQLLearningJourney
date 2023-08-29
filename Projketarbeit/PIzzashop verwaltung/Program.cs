using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.Json;
using System.Text.Json.Serialization;
using MongoDB.Bson.IO;
using Newtonsoft.Json.Linq;
using MongoDB.Bson.Serialization;
using System.Text.RegularExpressions;

namespace PizzashopVerwaltung
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationRun();
        }

        public static void ApplicationRun() {

            Begruessung();

            MenuAuswahl();

            SwitchCaseOptionsMainMenue();

        }

        public static void Begruessung() {
            Console.WriteLine("|-----------------------------------------------------|");
            Console.WriteLine("|--                                                 --|");
            Console.WriteLine("|--  Willkommen bei Ihrem Pizzashop des Vertrauens  --|");
            Console.WriteLine("|--                                                 --|");
            Console.WriteLine("|-----------------------------------------------------|");
            Console.WriteLine("|--       Drücken Sie ENTER zum Fortfahren          --|");
            Console.WriteLine("|-----------------------------------------------------|");

            Console.ReadLine();
        }

        public static void MenuAuswahl() {
            Console.Clear();

            Console.WriteLine("Wählen Sie aus was Sie tun möchten:");
            Console.WriteLine("");
            Console.WriteLine("1. Bestellungen verwalten");
            Console.WriteLine("2. Kunden verwalten");
            Console.WriteLine("3. Pizzen verwalten");
            Console.WriteLine("");
        }

        public static void SwitchCaseOptionsMainMenue() {
            int i = 0;
            switch (ChosenOption(0))
            {
                case 1:
                    Bestellungen();
                    break;
                case 2:
                    Kunden();
                    break;
                case 3:
                    Pizzen();
                    break;
            }
        }

        public static void Bestellungen() {
            Console.Clear();
            Console.WriteLine("Wählen Sie eine der folgenden Optionen:");
            Console.WriteLine("");
            Console.WriteLine("1. Bestellung hinzufügen");
            Console.WriteLine("2. Bestellung löschen");
            Console.WriteLine("3. Bestellung Anpassen");
            Console.WriteLine("4. Bestllungen Anzeigen");
            Console.WriteLine("");

            SwitchCaseBestellungen();
        }

        public static void Kunden() {
            Console.Clear();
            Console.WriteLine("Wählen Sie eine der folgenden Optionen:");
            Console.WriteLine("");
            Console.WriteLine("1. Kunde hinzufügen");
            Console.WriteLine("2. Kunde löschen");
            Console.WriteLine("3. Kunde Anpassen");
            Console.WriteLine("4. Kunden anzeigen");
            Console.WriteLine("5. Zurück zum Hauptmenue");
            Console.WriteLine("");

            SwitchCaseKunden();
        }

        public static void Pizzen() {
            Console.Clear();
            Console.WriteLine("Wählen Sie eine der folgenden Optionen:");
            Console.WriteLine("");
            Console.WriteLine("1. Pizza hinzufügen");
            Console.WriteLine("2. Pizza löschen");
            Console.WriteLine("3. Pizza Anpassen");
            Console.WriteLine("4. Pizzen Anzeigen");
            Console.WriteLine("5. Hauptmenue");
            Console.WriteLine("");

            SwitchCasePizzen();
        }

        public static void SwitchCaseBestellungen() {
            switch (ChosenOption(1)) {
                case 1:
                    BestellungHinzufügen();
                    break;
                case 2:
                    BestellungLoeschen();
                    break;
                case 3:

                    break;
                case 4:
                    BestellungenAnzeigen();
                    break;
            }
        }

        public static void SwitchCaseKunden()
        {
            switch (ChosenOption(3))
            {
                case 1:
                    KundeHinzufügen();
                    break;
                case 2:
                    KundeLöschen();
                    break;
                case 3:
                    KundeAnpassen();
                    break;
                case 4:
                    KundenAnzeigen();
                    break;
                case 5:
                    MenuAuswahl();
                    SwitchCaseOptionsMainMenue();
                    break;
            }
        }

        public static void SwitchCasePizzen()
        {
            switch (ChosenOption(2))
            {
                case 1:
                    PizzaHinzufügen();
                    break;
                case 2:
                    Pizzalöschen();
                    break;
                case 3:
                    PizzaAnpassen();
                    break;
                case 4:
                    PizzenAnzeigen();
                    break;
                case 5:
                    MenuAuswahl();
                    SwitchCaseOptionsMainMenue();
                    break;
            }
        }

        public static MongoClient mongoClient()
        {
            MongoClient dbClient = new MongoClient("mongodb://localhost:27017");
            return dbClient;
        }

        public static int ChosenOption(int i)
        {
            int Auswahl = 0;
            try {
                Auswahl = Convert.ToInt32(Console.ReadLine().Trim());
            }
            catch {
                Console.WriteLine("Ungültige Eingabe! Geben Sie nur eine der oben angegebenen Zahlen ein:");
                switch (i)
                {
                    case 0:
                        SwitchCaseOptionsMainMenue();
                        break;
                    case 1:
                        SwitchCaseBestellungen();
                        break;
                    case 2:
                        SwitchCasePizzen();
                        break;
                    case 3:
                        SwitchCaseKunden();
                        break;
                    case 4:
                        KundeLöschen();
                        break;
                    case 5:
                        KundeAnpassen();
                        break;
                    case 6:
                        Case1KundeBearbeiten();
                        break;
                    case 40:
                        KundenAnzeigen();
                        break;
                    case 101:
                        Console.WriteLine("Geben Sie bitte nur eine passende Nummer ein!");
                        KundeHinzufügen();
                        break;
                    case 102:
                        MenuAuswahl();
                        SwitchCaseOptionsMainMenue();
                        break;
                    case 103:
                        MenuAuswahl();
                        SwitchCaseOptionsMainMenue();
                        break;
                }
            }
            return Auswahl;
        }

        public static void BestellungHinzufügen() {
            Console.Clear();

            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<Bestellung>("Orders");
            var Bestellungen = db.GetCollection<BsonDocument>("Orders");

            Bestellung bestellung = new Bestellung();
            Kunde kundeFound = new Kunde();
            Bestellposition position = new Bestellposition();

            kundeFound = BestellungKundenIdentifizieren(kundeFound);

            bestellung.Bestelldatum = DateTime.Now;

            bestellung.kunde = kundeFound;

            var filter = Builders<BsonDocument>.Filter.Eq($"id", -1);

            Random rnd = new Random();
            int i = rnd.Next(100000, 1000000);

            bestellung.BestellNummer = i;

            List<Bestellposition> bestellpositionen = new List<Bestellposition>();

            BestellpositionenErfassen(bestellpositionen);

            bestellung.bestellpositionen = bestellpositionen.ToArray();

            double totalcalcultion = 0;

            foreach (var doc in bestellpositionen) {
                totalcalcultion = totalcalcultion + doc.Preis;
            }
            bestellung.Total = totalcalcultion;

            try
            {
                Console.Clear();
                collection.InsertOne(bestellung);
                Console.WriteLine("Bestellung erfolgreich aufgegeben!");
            }
            catch
            {
                Console.WriteLine("Bestellung konnte nicht aufgegeben werden werden/ERROR");
            }
        }

        public static Kunde BestellungKundenIdentifizieren(Kunde kundeFound) {
            Console.WriteLine("Wer gibt diese Bestellung auf?");
            Console.WriteLine("");
            Console.WriteLine("1. Neuer Kunde");
            Console.WriteLine("2. Kunde, welcher schon im System ist");

            switch (ChosenOption(333))
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("1. Um einen neuen Kunden hinzuzufügen!");
                    Console.WriteLine("2. Hauptmenue");

                    switch (ChosenOption(444))
                    {
                        case 1:
                            KundeHinzufügen();
                            break;
                        case 2:
                            MenuAuswahl();

                            SwitchCaseOptionsMainMenue();
                            break;
                    }
                    break;
                case 2:
                    SearchForCustomerAuswahl();

                    String x = "";
                    String y = "";

                    switch (ChosenOption(999))
                    {
                        case 1:
                            kundeFound = SearchForCustomerCase1();
                            break;
                        case 2:
                            x = "Strasse und Nummer";
                            y = "StrasseUndNUmmer";
                            kundeFound = deafultcaseselectKunde(x, y);
                            break;
                        case 3:
                            x = "PLZ";
                            y = "PLZ";
                            kundeFound = deafultcaseselectKunde(x, y);
                            break;
                        case 4:
                            x = "Ort";
                            y = "Ort";
                            kundeFound = deafultcaseselectKunde(x, y);
                            break;
                        case 5:
                            x = "Telefon nummer";
                            y = "Telefonnrm";
                            kundeFound = deafultcaseselectKunde(x, y);
                            break;
                        case 6:
                            x = "Email";
                            y = "Email";
                            kundeFound = deafultcaseselectKunde(x, y);
                            break;
                    }
                    break;
            }
            return kundeFound;
        }

        public static void PizzaHinzufügen() {
            Console.Clear();

            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<Pizza>("Pizza");

            Pizza pizza = new Pizza();

            Console.WriteLine("Name");
            pizza.name = Console.ReadLine().Trim();

            Console.WriteLine("Zutaten: xxxx, xxxxx, xxxx");
            pizza.ingredients = Console.ReadLine().Trim();

            Console.WriteLine("Wie viele grössen wollen Sie anbieten? max.3");
            int auswahl = Convert.ToInt32(Console.ReadLine());

            List<PizzaSizes> sizeList = new List<PizzaSizes>();

            PizzaeinfügenVariationen(sizeList, auswahl);

            pizza.sizes = sizeList.ToArray();

            try
            {
                Console.Clear();
                collection.InsertOne(pizza);
                Console.WriteLine("Pizza erfolgreich eingefügt!");
            } catch {
                Console.WriteLine("Pizza konnte nicht eingefügt werden/ERROR");
            }

        }

        public static List<Bestellposition> BestellpositionenErfassen(List<Bestellposition> bestellpositionen)
        {
            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<BsonDocument>("Pizza");
            var pizzen = db.GetCollection<BsonDocument>("Pizza");
            var result = pizzen.Find(new BsonDocument()).ToList();
            var customers = db.GetCollection<BsonDocument>("Pizza");

            Console.Clear();

            Console.Clear();

            Console.WriteLine("Wie viele verschiedene Pizzen möchten Sie Bestellen");
            int Pizzenanzahl = Convert.ToInt32(Console.ReadLine());

            for (int x = 0; x < Pizzenanzahl; x++) {

                Bestellposition position = new Bestellposition();

                int i = 1;

                int options = result.Count();

                Console.Clear();

                Console.WriteLine("Sie haben " + options + " Optionen:");
                Console.WriteLine("");

                List<Pizza> pizzas = new List<Pizza>();

                foreach (var doc in result)
                {
                    Pizza pizza = BsonSerializer.Deserialize<Pizza>(doc);

                    string customersToDelete = i + ". " + pizza.ToString();

                    Console.WriteLine(customersToDelete);

                    pizzas.Add(pizza);

                    i++;
                }

                Console.WriteLine("");
                Console.WriteLine("Welche Pizza möchten Sie wählen?");

                Pizza PizzaFromDatabase = new Pizza();

                int Auswahl = Convert.ToInt32(Console.ReadLine()) - 1;

                PizzaFromDatabase = pizzas[Auswahl];

                var filterToDelete = Builders<BsonDocument>.Filter.Eq("_id", PizzaFromDatabase.Id);

                var resultToDelete = customers.Find(filterToDelete).FirstOrDefault();

                PizzaFromDatabase = BsonSerializer.Deserialize<Pizza>(resultToDelete);

                Console.Clear();

                int e = 0;
                int r = 1;

                List<PizzaSizes> pizzaSizes = new List<PizzaSizes>();

                foreach (var doc in PizzaFromDatabase.sizes)
                {
                    Console.WriteLine(r + ". " + PizzaFromDatabase.sizes[e].ToString());

                    pizzaSizes.Add(PizzaFromDatabase.sizes[e]);

                    r++;
                    e++;
                }

                Console.WriteLine("Welche Variation der Pizza " + PizzaFromDatabase.name + " wählen Sie: ");

                int auswahlPizzaVariant = Convert.ToInt32(Console.ReadLine()) - 1;

                List<PizzaSizes> sizes = new List<PizzaSizes>();

                Pizza pizzaToPutInOrder = new Pizza();

                pizzaToPutInOrder.name = PizzaFromDatabase.name.ToString();

                string id = PizzaFromDatabase.Id.ToString();
                var userid = new ObjectId(id);

                pizzaToPutInOrder.Id = userid;

                pizzaToPutInOrder.ingredients = PizzaFromDatabase.ingredients.ToString();

                Console.WriteLine("Wie viele Pizzen in dieser Ausführung wollen Sie bestellen?");
                int auswhalPizzaAnzahl = Convert.ToInt32(Console.ReadLine());

                double preis = pizzaSizes[auswahlPizzaVariant].price;

                position.Preis = preis * auswhalPizzaAnzahl;

                position.Stückzahl = auswhalPizzaAnzahl;

                sizes.Add(pizzaSizes[auswahlPizzaVariant]);

                pizzaToPutInOrder.sizes = sizes.ToArray();

                position.pizza = pizzaToPutInOrder;

                bestellpositionen.Add(position);
            }

            return bestellpositionen;
        }

        public static List<PizzaSizes> PizzaeinfügenVariationen(List<PizzaSizes> sizeList, int AnzhalOptionen) {

            for (int i = 0; i < AnzhalOptionen; i++)
            {
                PizzaSizes size = new PizzaSizes();

                Console.Clear();

                Console.WriteLine("Die Grösse der Pizza: large, medium, small");
                size.name = Console.ReadLine().Trim();

                Console.WriteLine("Der Umfang der Pizza: xxxx CM");
                size.diamater = Convert.ToDouble(Console.ReadLine().Trim());

                Console.WriteLine("Die kcal der Pizza: xxxx KCAL");
                size.kcal = Convert.ToDouble(Console.ReadLine().Trim());

                Console.WriteLine("Preis der Pizza: xxxx CHF");
                size.price = Convert.ToDouble(Console.ReadLine().Trim());

                sizeList.Add(size);
            }

            return sizeList;
        }

        public static void Pizzalöschen() {
            PizzaLöschenAuswahl();

            String i = "";
            String x = "";

            switch (ChosenOption(707)) {
                case 1:
                    i = "name";
                    x = "Name";
                    DeletePizzamethod(i, x);
                    break;
                case 2:
                    i = "ingredients";
                    x = "Zutaten";
                    DeletePizzamethod(i, x);
                    break;
            }
        }

        public static void PizzaLöschenAuswahl() {
            Console.Clear();

            Console.WriteLine("Wie möchten Sie nach der Pizza suchen?");
            Console.WriteLine("");
            Console.WriteLine("1. name");
            Console.WriteLine("2. ingredients");
            Console.WriteLine("");
        }

        public static void PizzaAnpassen() {
            Console.Clear();

            String i = "";
            String x = "";

            PizzaAnpassenAuswahl();

            switch (ChosenOption(888))
            {
                case 1:
                    i = "name";
                    x = "Name";
                    PizzaAnpassenDefaultCase(i, x);
                    break;
                case 2:
                    i = "ingredients";
                    x = "Zutaten";
                    PizzaAnpassenDefaultCase(i, x);
                    break;
            }
        }

        public static void PizzaAnpassenDefaultCase(String Suchattribut, String lololol) {
            Console.Clear();

            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<Pizza>("Pizza");

            Console.WriteLine($"{lololol} der Pizza nach der Sie suchen:");
            String placeholder = Console.ReadLine().Trim();

            var pizzen = db.GetCollection<BsonDocument>("Pizza");
            var filter = Builders<BsonDocument>.Filter.Eq($"{Suchattribut}", placeholder);
            var result = pizzen.Find(filter).ToList();

            int i = 1;

            int options = result.Count();

            Console.WriteLine("Sie haben " + options + " Optionen:");
            Console.WriteLine("");

            List<Pizza> pizzenList = new List<Pizza>();

            foreach (var doc in result)
            {
                Pizza pizza = BsonSerializer.Deserialize<Pizza>(doc);

                string customersToDelete = i + ". " + pizza.ToString();

                Console.WriteLine(customersToDelete);

                pizzenList.Add(pizza);

                i++;
            }

            Console.WriteLine("");
            Console.WriteLine("Welche Pizza möchten Sie bearbeiten?");

            Pizza pizzaToEdit = new Pizza();

            int Auswahl = Convert.ToInt32(Console.ReadLine()) - 1;

            pizzaToEdit = pizzenList[Auswahl];

            var filterToDelete = Builders<BsonDocument>.Filter.Eq("_id", pizzaToEdit.Id);

            var resultToEdit = pizzen.Find(filterToDelete).FirstOrDefault();

            Console.Clear();

            TryToExtractAPizzaToEdit(resultToEdit);
        }

        public static void TryToExtractAPizzaToEdit(BsonDocument resultToEdit){
            Console.Clear();
            Console.WriteLine("Der Kunde nach dem Sie gesucht haben:");
            Console.WriteLine("");

            Pizza pizza = BsonSerializer.Deserialize<Pizza>(resultToEdit);

            Console.WriteLine(pizza.ToString());
            Console.WriteLine("");

            Console.WriteLine("Wollen Sie den oben gennanten Pizza editieren? 1. Ja 2. Nein");

            switch (ChosenOption(6))
            {
                case 1:
                    VerbesserungsAuswahl(pizza);
                    switch (ChosenOption(7))
                    {
                        case 1:
                            EditPizzaCase1(pizza);
                            break;
                        case 2:
                            EditPizzaCase2(pizza);
                            break;
                        case 3:
                            EditPizzaCase3(pizza);
                            break;
                    }
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("1. zurück zum Hauptmenu");
                    Console.WriteLine("2. Neue Person zum editieren auswählen");
                    switch (ChosenOption(8))
                    {
                        case 1:
                            MenuAuswahl();
                            SwitchCaseOptionsMainMenue();
                            break;
                        case 2:
                            KundeAnpassen();
                            break;
                    }
                    break;
            }
        }

        public static void EditPizzaCase1(Pizza pizza) {
            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<Pizza>("Pizza");

            Console.WriteLine("Neuer Name der Pizza");
            String placeholder = Console.ReadLine().Trim();

            var filter = Builders<Pizza>.Filter.Eq("name", pizza.name);
            var update = Builders<Pizza>.Update.Set("name", placeholder);

            try
            {
                var result = collection.UpdateMany(filter, update);
                Console.WriteLine("Update Succesfull");
            }
            catch
            {
                Console.WriteLine("");
                Console.WriteLine("      Update failed/Error");
                Console.WriteLine("***********************************");
                Console.WriteLine(" Zurück zum Hauptmenue mit ENTER");
                Console.ReadLine();

                MenuAuswahl();
                SwitchCaseOptionsMainMenue();
            }

            Console.WriteLine("1. zurück zum Hauptmenu");
            Console.WriteLine("2. Neue Pizza zum editieren auswählen");

            switch (ChosenOption(28))
            {
                case 1:
                    MenuAuswahl();
                    SwitchCaseOptionsMainMenue();
                    break;
                case 2:
                    PizzaAnpassen();
                    break;
            }
        }

        public static void EditPizzaCase2(Pizza pizza)
        {
            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<Pizza>("Pizza");

            Console.WriteLine("Neuer Name der Pizza");
            String placeholder = Console.ReadLine().Trim();

            var filter = Builders<Pizza>.Filter.Eq("ingredients", pizza.ingredients);
            var update = Builders<Pizza>.Update.Set("ingredients", placeholder);

            try
            {
                var result = collection.UpdateMany(filter, update);
                Console.WriteLine("Update Succesfull");
            }
            catch
            {
                Console.WriteLine("");
                Console.WriteLine("      Update failed/Error");
                Console.WriteLine("***********************************");
                Console.WriteLine(" Zurück zum Hauptmenue mit ENTER");
                Console.ReadLine();

                MenuAuswahl();
                SwitchCaseOptionsMainMenue();
            }

            Console.WriteLine("1. zurück zum Hauptmenu");
            Console.WriteLine("2. Neue Pizza zum editieren auswählen");

            switch (ChosenOption(28))
            {
                case 1:
                    MenuAuswahl();
                    SwitchCaseOptionsMainMenue();
                    break;
                case 2:
                    PizzaAnpassen();
                    break;
            }
        }

        public static void EditPizzaCase3(Pizza pizza) {
            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<Pizza>("Pizza");

            Console.Clear();
            Console.WriteLine("Welche Size wollen Sie bearbeiten?");

            int i = 1;
            int x = 0;
            foreach (PizzaSizes sizes in pizza.sizes)
            {
                Console.WriteLine(i + ". " + pizza.sizes[x]);
                i++;
                x++;
            }

            PizzaSizes size = new PizzaSizes();

            int ausgewählteSize = Convert.ToInt32(Console.ReadLine());

            ausgewählteSize = ausgewählteSize - 1;

            Console.Clear();

            Console.WriteLine("Neuer Sizename:");
            size.name = Console.ReadLine().Trim();
            Console.WriteLine("Neuer Diamater:");
            size.diamater = Convert.ToDouble(Console.ReadLine().Trim());
            Console.WriteLine("Neue kcal anzahl:");
            size.kcal = Convert.ToDouble(Console.ReadLine().Trim());
            Console.WriteLine("Neuer Preis:");
            size.price = Convert.ToDouble(Console.ReadLine().Trim());

            pizza.sizes[ausgewählteSize] = size;

            var filter = Builders<Pizza>.Filter.Eq("name", pizza.name);
            var update = Builders<Pizza>.Update.Set("sizes", pizza.sizes);

            try
            {
                var result = collection.UpdateOne(filter, update);
                Console.WriteLine("Update Succesfull");
            }
            catch
            {
                Console.WriteLine("");
                Console.WriteLine("      Update failed/Error");
                Console.WriteLine("***********************************");
                Console.WriteLine(" Zurück zum Hauptmenue mit ENTER");
                Console.ReadLine();

                MenuAuswahl();
                SwitchCaseOptionsMainMenue();
            }

            Console.WriteLine("1. zurück zum Hauptmenu");
            Console.WriteLine("2. Neue Pizza zum editieren auswählen");

            switch (ChosenOption(28))
            {
                case 1:
                    MenuAuswahl();
                    SwitchCaseOptionsMainMenue();
                    break;
                case 2:
                    PizzaAnpassen();
                    break;
            }
        }

        public static void VerbesserungsAuswahl(Pizza pizza) {
            Console.Clear();
            Console.WriteLine("Welches Attribut der Pizza möchten Sie bearbeiten?");
            Console.WriteLine("");
            Console.WriteLine("1. " + pizza.name);
            Console.WriteLine("2. " + pizza.ingredients);
            Console.WriteLine("3. Pizza sizes");
        }

        public static void PizzaAnpassenAuswahl() {
            Console.Clear();

            Console.WriteLine("Wie möchten Sie nach der Pizza suchen?");
            Console.WriteLine("");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. ingredients");
            Console.WriteLine("");
        }

        public static void PizzenAnzeigen() {
            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<BsonDocument>("Pizza");

            var pizzen = db.GetCollection<ICollection<Pizza>>("Pizza");

            var result = pizzen.Find(new BsonDocument()).ToList();

            Console.Clear();

            Console.WriteLine("Alle Pizzen und ihre Varianten:");

            foreach (var doc in result)
            {
             //   Pizza pizza = BsonSerializer.Deserialize<Pizza>(doc);

              //  Console.WriteLine(pizza.ToString());
            }
        }

        public static void SearchForCustomerAuswahl() {
            Console.Clear();

            Console.WriteLine("Wie möchten Sie nachdem Kunden suchen?");
            Console.WriteLine("");
            Console.WriteLine("1. Vorname + Nachname");
            Console.WriteLine("2. Strasse & Nr");
            Console.WriteLine("3. PLZ");
            Console.WriteLine("4. Ort");
            Console.WriteLine("5. TelefonNmr");
            Console.WriteLine("");
        }

        public static Kunde SearchForCustomerCase1() {
            Console.Clear();

            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<Kunde>("customers");

            Console.WriteLine("Vorname und Nachname der Person nach der Sie suchen:");
            String eingabe = Console.ReadLine().Trim();

            //var vorname = Regex.Match(eingabe, @"^([\w\-]+)"); <- Füllt erstes Wort in String var ab
            var vorname = eingabe.Split(' ')[0];
            var nachname = eingabe.Split(' ')[1];

            var customers = db.GetCollection<BsonDocument>("customers");
            var filter = Builders<BsonDocument>.Filter.Eq("Vorname", vorname) & Builders<BsonDocument>.Filter.Eq("Nachname", nachname);
            var result = customers.Find(filter).ToList();

            int i = 1;

            int options = result.Count();

            Console.WriteLine("Sie haben " + options + " Optionen:");
            Console.WriteLine("");

            List<Kunde> kunden = new List<Kunde>();

            foreach (var doc in result)
            {
                Kunde kunde = BsonSerializer.Deserialize<Kunde>(doc);

                string customersToDelete = i + ". " + kunde.ToString();

                Console.WriteLine(customersToDelete);

                kunden.Add(kunde);

                i++;
            }

            Console.WriteLine("");
            Console.WriteLine("Welchen Kunden möchten Sie wählen?");

            Kunde kundeToDelete = new Kunde();

            int Auswahl = Convert.ToInt32(Console.ReadLine()) - 1;

            kundeToDelete = kunden[Auswahl];

            var filterToDelete = Builders<BsonDocument>.Filter.Eq("_id", kundeToDelete.Id);

            var resultToDelete = customers.Find(filterToDelete).FirstOrDefault();

            Kunde kundeFound = BsonSerializer.Deserialize<Kunde>(resultToDelete);

            return kundeFound;
        }

        public static Kunde deafultcaseselectKunde(String lololol, String Suchattribut)
        {
            Console.Clear();

            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<Kunde>("customers");

            Console.WriteLine($"{lololol} der Person nach der Sie suchen:");
            String placeholder = Console.ReadLine().Trim();

            var customers = db.GetCollection<BsonDocument>("customers");
            var filter = Builders<BsonDocument>.Filter.Eq($"{Suchattribut}", placeholder);
            var result = customers.Find(filter).ToList();

            int i = 1;

            int options = result.Count();

            Console.WriteLine("Sie haben " + options + " Optionen:");
            Console.WriteLine("");

            List<Kunde> kunden = new List<Kunde>();

            foreach (var doc in result)
            {
                Kunde kunde = BsonSerializer.Deserialize<Kunde>(doc);

                string customersToDelete = i + ". " + kunde.ToString();

                Console.WriteLine(customersToDelete);

                kunden.Add(kunde);

                i++;
            }

            Console.WriteLine("");
            Console.WriteLine("Welchen Kunden möchten Sie wählen?");

            Kunde kundeTOEdit = new Kunde();

            int Auswahl = Convert.ToInt32(Console.ReadLine()) - 1;

            kundeTOEdit = kunden[Auswahl];

            return kundeTOEdit;
        }

        public static void KundelöschenAuswahl() {
            Console.Clear();

            Console.WriteLine("Wie möchten Sie nachdem Kunden suchen?");
            Console.WriteLine("");
            Console.WriteLine("1. Vorname + Nachname");
            Console.WriteLine("3. Strasse & Nr");
            Console.WriteLine("4. PLZ");
            Console.WriteLine("5. Ort");
            Console.WriteLine("6. TelefonNmr");
            Console.WriteLine("");

        }

        public static void DeletePizzaCase1() {
            Console.Clear();

            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<Kunde>("Pizza");

            Console.WriteLine("Name der Pizza nach der Sie suchen:");
            String name = Console.ReadLine().Trim();

            var customers = db.GetCollection<BsonDocument>("Pizza");

            var filter = Builders<BsonDocument>.Filter.Eq("name", name);
            var result = customers.Find(filter).ToList();

            int i = 1;

            int options = result.Count();

            Console.WriteLine("Sie haben " + options + " Optionen:");
            Console.WriteLine("");

            List<Pizza> pizzen = new List<Pizza>();

            foreach (var doc in result)
            {
                Pizza pizza = BsonSerializer.Deserialize<Pizza>(doc);

                string customersToDelete = i + ". " + pizza.ToString();

                Console.WriteLine(customersToDelete);

                pizzen.Add(pizza);

                i++;
            }

            Console.WriteLine("");
            Console.WriteLine("Welche Pizza möchten Sie löschen?");

            Pizza pizzaToDelete = new Pizza();

            int Auswahl = Convert.ToInt32(Console.ReadLine()) - 1;

            pizzaToDelete = pizzen[Auswahl];

            var filterToDelete = Builders<BsonDocument>.Filter.Eq("_id", pizzaToDelete.Id);

            var resultToDelete = customers.Find(filterToDelete).FirstOrDefault();

            collection.DeleteOne(resultToDelete);

            Console.Clear();

            Console.WriteLine("Sie haben:\n" + pizzaToDelete.ToString() + "\ngelöscht!");

            EscapeAtTheEndOfDeletePIzza();
        }

        public static void DeletePizzamethod(String Suchattribut, String lololol) {
            Console.Clear();

            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<Kunde>("Pizza");

            Console.WriteLine($"{lololol} der Pizza nach der Sie suchen:");
            String placeholder = Console.ReadLine().Trim();

            var customers = db.GetCollection<BsonDocument>("Pizza");

            var filter = Builders<BsonDocument>.Filter.Eq($"{Suchattribut}", placeholder);
            var result = customers.Find(filter).ToList();

            int i = 1;

            int options = result.Count();

            Console.WriteLine("Sie haben " + options + " Optionen:");
            Console.WriteLine("");

            List<Pizza> pizzen = new List<Pizza>();

            foreach (var doc in result)
            {
                Pizza pizza = BsonSerializer.Deserialize<Pizza>(doc);

                string customersToDelete = i + ". " + pizza.ToString();

                Console.WriteLine(customersToDelete);

                pizzen.Add(pizza);

                i++;
            }

            Console.WriteLine("");
            Console.WriteLine("Welche Pizza möchten Sie löschen?");

            Pizza pizzaToDelete = new Pizza();

            int Auswahl = Convert.ToInt32(Console.ReadLine()) - 1;

            pizzaToDelete = pizzen[Auswahl];

            var filterToDelete = Builders<BsonDocument>.Filter.Eq("_id", pizzaToDelete.Id);

            var resultToDelete = customers.Find(filterToDelete).FirstOrDefault();

            collection.DeleteOne(resultToDelete);

            Console.Clear();

            Console.WriteLine("Sie haben:\n" + pizzaToDelete.ToString() + "\ngelöscht!");

            EscapeAtTheEndOfDeletePIzza();
        }

        public static void DeletePizzaCase2() {
            Console.Clear();

            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<Kunde>("Pizza");

            Console.WriteLine("Zutaten der Pizza nach der Sie suchen:");
            String ingredients = Console.ReadLine().Trim();

            var customers = db.GetCollection<BsonDocument>("Pizza");

            var filter = Builders<BsonDocument>.Filter.Eq("ingredients", ingredients);
            var result = customers.Find(filter).ToList();

            int i = 1;

            int options = result.Count();

            Console.WriteLine("Sie haben " + options + " Optionen:");
            Console.WriteLine("");

            List<Pizza> pizzen = new List<Pizza>();

            foreach (var doc in result)
            {
                Pizza pizza = BsonSerializer.Deserialize<Pizza>(doc);

                string customersToDelete = i + ". " + pizza.ToString();

                Console.WriteLine(customersToDelete);

                pizzen.Add(pizza);

                i++;
            }

            Console.WriteLine("");
            Console.WriteLine("Welche Pizza möchten Sie löschen?");

            Pizza pizzaToDelete = new Pizza();

            int Auswahl = Convert.ToInt32(Console.ReadLine()) - 1;

            pizzaToDelete = pizzen[Auswahl];

            var filterToDelete = Builders<BsonDocument>.Filter.Eq("_id", pizzaToDelete.Id);

            var resultToDelete = customers.Find(filterToDelete).FirstOrDefault();

            collection.DeleteOne(resultToDelete);

            Console.Clear();

            Console.WriteLine("Sie haben:\n" + pizzaToDelete.ToString() + "\ngelöscht!");
        }

        public static void KundeHinzufügen() {
            Console.Clear();

            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<Kunde>("customers");

            Kunde kunde = new Kunde();

            Console.WriteLine("Vorname:");
            kunde.Vorname = Console.ReadLine().Trim();
            Console.WriteLine("Nachme:");
            kunde.Nachname = Console.ReadLine().Trim();
            Console.WriteLine("Strasse + Nr:");
            kunde.StrasseUndNUmmer = Console.ReadLine().Trim();
            Console.WriteLine("Plz:");
            kunde.PLZ = Console.ReadLine().Trim();
            Console.WriteLine("Ort:");
            kunde.Ort = Console.ReadLine().Trim();
            Console.WriteLine("Telefon:");
            kunde.Telefonnrm = Console.ReadLine().Trim();
            Console.WriteLine("Email:");
            kunde.Email = Console.ReadLine().Trim();
            Console.WriteLine("Geburtstag: dd.MM.yyyy ");
            kunde.Birthday = Convert.ToDateTime(Console.ReadLine());
            //DateTime fixedBirthday = geburtstag.AddDays(1); //fix für den einen Tag den die Datenbank abzieht??? better fix found by converting time back to local time
            
            GespeichertenKundenAusgeben(kunde);

            switch (ChosenOption(101)) {
                case 1:
                    try {
                        collection.InsertOne(kunde);
                        Console.Clear();
                        Console.WriteLine("Der Kunde wurde erfolgreich in die Datenbank eingefügt!");

                        Console.WriteLine("1. zurück zum Hauptmenu");
                        Console.WriteLine("2. Neue Person hinzufügen");

                        switch (ChosenOption(103))
                        {
                            case 1:
                                MenuAuswahl();
                                SwitchCaseOptionsMainMenue();
                                break;
                            case 2:
                                KundeHinzufügen();
                                break;
                        }
                    }
                    catch {
                        Console.WriteLine("");
                        Console.WriteLine("      Error");
                        Console.WriteLine("***********************************");
                        Console.WriteLine(" Zurück zum Hauptmenue mit ENTER");
                        Console.ReadLine();

                        MenuAuswahl();
                        SwitchCaseOptionsMainMenue();
                    }
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("1. zurück zum Hauptmenu");
                    Console.WriteLine("2. Neue Person hinzufügen");

                    switch (ChosenOption(102))
                    {
                        case 1:
                            MenuAuswahl();
                            SwitchCaseOptionsMainMenue();
                            break;
                        case 2:
                            KundeHinzufügen();
                            break;
                    }
                    break;
            }
        }

        public static void GespeichertenKundenAusgeben(Kunde kunde) {
            Console.Clear();

            Console.WriteLine("Möchten Sie diesen Kunden speichern?");
            Console.WriteLine("");
            Console.WriteLine("Vorname: " + kunde.Vorname);
            Console.WriteLine("Nachname: " + kunde.Nachname);
            Console.WriteLine("Strasse und Nmr: " + kunde.StrasseUndNUmmer);
            Console.WriteLine("PLZ " + kunde.PLZ);
            Console.WriteLine("Ort: " + kunde.Ort);
            Console.WriteLine("TelefonNmr: " + kunde.Telefonnrm);
            Console.WriteLine("Email " + kunde.Email);
            Console.WriteLine("Geburtstag" + kunde.Birthday.ToLocalTime().ToShortDateString());
            Console.WriteLine("");
            Console.WriteLine("1. Ja  oder 2. Nein");
        }

        public static void KundeLöschen() {
            KundelöschenAuswahl();

            String i = "";
            String x = "";

            switch (ChosenOption(4)) {
                case 1:
                    DeleteCustomerCase1();
                    break;
                case 2:
                    i = "StrasseUndNUmmer";
                    x = "Strasse und Nmr.";
                    DeleteCustomerCaseDefault(i, x);
                    break;
                case 3:
                    i = "PLZ";
                    x = "PLZ";
                    DeleteCustomerCaseDefault(i, x);
                    break;
                case 4:
                    i = "Ort";
                    x = "Ort";
                    DeleteCustomerCaseDefault(i, x);
                    break;
                case 5:
                    i = "Telefonnrm";
                    x = "Telefonnrm";
                    DeleteCustomerCaseDefault(i, x);
                    break;
                case 6:
                    i = "Email";
                    x = "Email";
                    DeleteCustomerCaseDefault(i, x);
                    break;
            }
        }

        public static void KundeAnpassen() {
            Console.Clear();

            KundenAnpassAuswahl();

            String i = "";
            String x = "";

            switch (ChosenOption(5))
            {
                case 1:
                    Case1KundeBearbeiten();
                    break;
                case 2:
                    i = "StrasseUndNUmmer";
                    x = "Strasse und Nmr.";
                    DefaultCaseKundeBearbeiten(i, x);
                    break;
                case 3:
                    i = "PLZ";
                    x = "PLZ";
                    DefaultCaseKundeBearbeiten(i, x);
                    break;
                case 4:
                    i = "Ort";
                    x = "Ort";
                    DefaultCaseKundeBearbeiten(i, x);
                    break;
                case 5:
                    i = "Telefonnrm";
                    x = "Telefonnrm";
                    DefaultCaseKundeBearbeiten(i, x);
                    break;
                case 6:
                    i = "Email";
                    x = "Email";
                    DefaultCaseKundeBearbeiten(i, x);
                    break;
                case 7:
                    Case7KundeBearbeiten();
                    break;
            }

        }

        public static void KundenAnpassAuswahl() {
            Console.Clear();
            Console.WriteLine("Wie möchten Sie nachdem Kunden suchen?");
            Console.WriteLine("");
            Console.WriteLine("1. Vorname und Nachname");
            Console.WriteLine("2. Strasse & Nr");
            Console.WriteLine("3. PLZ");
            Console.WriteLine("4. Ort");
            Console.WriteLine("5. TelefonNmr");
            Console.WriteLine("6. Email");
            Console.WriteLine("7. Geburtstag");
            Console.WriteLine("");
        }

        public static void KundenAnzeigen() {

            KundenAnzeigenMenue();

            switch (ChosenOption(40)) {
                case 1:
                    KundenAnzeigenCase1();
                    break;
                case 2:
                    MenuAuswahl();
                    SwitchCaseOptionsMainMenue();
                    break;
            }
        }

        public static void KundenAnzeigenMenue() {
            Console.Clear();
            Console.WriteLine("1. Alle Kunden anziegen");
            Console.WriteLine("2. Hauptmenue");
        }

        public static void KundenAnzeigenCase1() {
            Console.Clear();

            var db = mongoClient().GetDatabase("ShopDb");

            var customers = db.GetCollection<BsonDocument>("customers");

            var result = customers.Find(new BsonDocument()).ToList();

            Console.WriteLine("Alle Kunden:");
            Console.WriteLine("------------");


            foreach (var doc in result) {
                Kunde kunde = BsonSerializer.Deserialize<Kunde>(doc);

                Console.WriteLine(kunde.ToString());
            }
        }

        public static void BestellungenAnzeigen() {
            Console.Clear();

            var db = mongoClient().GetDatabase("ShopDb");

            var orders = db.GetCollection<BsonDocument>("Orders");

            var result = orders.Find(new BsonDocument()).ToList();

            Console.WriteLine("Alle Bestellungen:");
            Console.WriteLine("------------");

            foreach (var doc in result)
            {
                Bestellung bestellung = BsonSerializer.Deserialize<Bestellung>(doc);

                Console.WriteLine(bestellung.ToString());
            }
        }

        public static void BestellungBearbeiten() {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Leider hatte ich keine Zeit für diese Funktion(Matheprüfung)"); //LOL
        }

        public static void BestellungLoeschen() {
            Console.Clear();

            var db = mongoClient().GetDatabase("ShopDb");

            var orders = db.GetCollection<BsonDocument>("Orders");

            var result = orders.Find(new BsonDocument()).ToList();

            int i = 1;

            List<Bestellung> bestllungen = new List<Bestellung>();

            foreach (var doc in result)
            {
                Bestellung bestellung = BsonSerializer.Deserialize<Bestellung>(doc);

                Console.WriteLine(i + ". " +bestellung.ToString());

                bestllungen.Add(bestellung);

                i++;
            }

            Console.WriteLine("");
            Console.WriteLine("Welche Bestellung möchten Sie löschen?");

            Bestellung Deletesmt = new Bestellung();

            int Auswahl = Convert.ToInt32(Console.ReadLine()) - 1;

            Deletesmt = bestllungen[Auswahl];

            var filterToDelete = Builders<BsonDocument>.Filter.Eq("_id", Deletesmt.Id);

            var resultToDelete = orders.Find(filterToDelete).FirstOrDefault();

            orders.DeleteOne(resultToDelete);

            Console.WriteLine("Sie haben die Bestellung mit der Nummer " + Deletesmt.BestellNummer + "erfolgreich gelöscht!");
        }

        public static void EscapeAtTheEndOfDeleCustomer()
        {
            Console.WriteLine("");
            Console.WriteLine("1. mehr Kunden löschen");
            Console.WriteLine("2. zum Hauptmenue");

            switch (ChosenOption(666))
            {
                case 1:
                    KundelöschenAuswahl();

                    KundeLöschen();
                    break;
                case 2:
                    MenuAuswahl();

                    SwitchCaseOptionsMainMenue();
                    break;
            }
        }

        public static void EscapeAtTheEndOfDeletePIzza() {
            Console.WriteLine("");
            Console.WriteLine("1. mehr Pizzen löschen");
            Console.WriteLine("2. zum Hauptmenue");

            switch (ChosenOption(666))
            {
                case 1:
                    PizzaLöschenAuswahl();

                    Pizzalöschen();
                    break;
                case 2:
                    MenuAuswahl();

                    SwitchCaseOptionsMainMenue();
                    break;
            }
        }

        public static void DeleteCustomerCase1() {
            Console.Clear();

            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<Kunde>("customers");

            Console.WriteLine("Vorname und Nachname der Person nach der Sie suchen:");
            String eingabe = Console.ReadLine().Trim();

            //var vorname = Regex.Match(eingabe, @"^([\w\-]+)"); <- Füllt erstes Wort in String var ab
            var vorname = eingabe.Split(' ')[0];
            var nachname = eingabe.Split(' ')[1];

            var customers = db.GetCollection<BsonDocument>("customers");
            var filter = Builders<BsonDocument>.Filter.Eq("Vorname", vorname) & Builders<BsonDocument>.Filter.Eq("Nachname", nachname);
            var result = customers.Find(filter).ToList();

            int i = 1;

            int options = result.Count();

            Console.WriteLine("Sie haben " + options + " Optionen:");
            Console.WriteLine("");

            List<Kunde> kunden = new List<Kunde>();

            foreach (var doc in result)
            {
                Kunde kunde = BsonSerializer.Deserialize<Kunde>(doc);

                string customersToDelete = i + ". " + kunde.ToString();

                Console.WriteLine(customersToDelete);

                kunden.Add(kunde);

                i++;
            }

            Console.WriteLine("");
            Console.WriteLine("Welchen Kunden möchten Sie löschen?");

            Kunde kundeToDelete = new Kunde();

            int Auswahl = Convert.ToInt32(Console.ReadLine()) - 1;

            kundeToDelete = kunden[Auswahl];

            var filterToDelete = Builders<BsonDocument>.Filter.Eq("_id", kundeToDelete.Id);

            var resultToDelete = customers.Find(filterToDelete).FirstOrDefault();

            collection.DeleteOne(resultToDelete);

            Console.Clear();

            Console.WriteLine("Sie haben:\n" + kundeToDelete.ToString() + "\ngelöscht!");

            EscapeAtTheEndOfDeleCustomer();
        }

        public static void DeleteCustomerCaseDefault(String Suchattribut, String lololol) {
            Console.Clear();

            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<Kunde>("customers");

            Console.WriteLine($"{lololol} der Person nach der Sie suchen:");
            String placeholder = Console.ReadLine().Trim();

            var customers = db.GetCollection<BsonDocument>("customers");
            var filter = Builders<BsonDocument>.Filter.Eq($"{Suchattribut}", placeholder);
            var result = customers.Find(filter).ToList();

            int i = 1;

            int options = result.Count();

            Console.WriteLine("Sie haben " + options + " Optionen:");
            Console.WriteLine("");

            List<Kunde> kunden = new List<Kunde>();

            foreach (var doc in result)
            {
                Kunde kunde = BsonSerializer.Deserialize<Kunde>(doc);

                string customersToDelete = i + ". " + kunde.ToString();

                Console.WriteLine(customersToDelete);

                kunden.Add(kunde);

                i++;
            }

            Console.WriteLine("");
            Console.WriteLine("Welchen Kunden möchten Sie löschen?");

            Kunde kundeToDelete = new Kunde();

            int Auswahl = Convert.ToInt32(Console.ReadLine()) - 1;

            kundeToDelete = kunden[Auswahl];

            var filterToDelete = Builders<BsonDocument>.Filter.Eq("_id", kundeToDelete.Id);

            var resultToDelete = customers.Find(filterToDelete).FirstOrDefault();

            collection.DeleteOne(resultToDelete);

            Console.Clear();

            Console.WriteLine("Sie haben:\n" + kundeToDelete.ToString() + "\ngelöscht!");

            EscapeAtTheEndOfDeleCustomer();
        }

        public static void TryToExtractACustomerToEdit(BsonDocument resultToEdit) {
            Console.Clear();
            Console.WriteLine("Der Kunde nach dem Sie gesucht haben:");
            Console.WriteLine("");

            Kunde kunde = BsonSerializer.Deserialize<Kunde>(resultToEdit);

            Console.WriteLine(kunde.ToString());
            Console.WriteLine("");

            Console.WriteLine("Wollen Sie den oben gennanten Kunden editieren? 1. Ja 2. Nein");

            switch (ChosenOption(6))
            {
                case 1:
                    EditCustomerMenu(kunde);
                    switch (ChosenOption(7))
                    {
                        case 1:
                            EditUserCase1(kunde);
                            break;
                        case 2:
                            EditUserCase2(kunde);
                            break;
                        case 3:
                            EditUserCase3(kunde);
                            break;
                        case 4:
                            EditUserCase4(kunde);
                            break;
                        case 5:
                            EditUserCase5(kunde);
                            break;
                        case 6:
                            EditUserCase6(kunde);
                            break;
                        case 7:
                            EditUserCase7(kunde);
                            break;
                        case 8:
                            editUserCase8(kunde);
                            break;
                    }
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("1. zurück zum Hauptmenu");
                    Console.WriteLine("2. Neue Person zum editieren auswählen");
                    switch (ChosenOption(8))
                    {
                        case 1:
                            MenuAuswahl();
                            SwitchCaseOptionsMainMenue();
                            break;
                        case 2:
                            KundeAnpassen();
                            break;
                    }
                    break;
            }
        }

        public static FilterDefinition<BsonDocument> ShowAllCustomersToEdit(List<BsonDocument> result) {

            int i = 1;

            int options = result.Count();

            Console.WriteLine("Sie haben " + options + " Optionen:");
            Console.WriteLine("");

            List<Kunde> kunden = new List<Kunde>();

            foreach (var doc in result)
            {
                Kunde kunde = BsonSerializer.Deserialize<Kunde>(doc);

                string customersToDelete = i + ". " + kunde.ToString();

                Console.WriteLine(customersToDelete);

                kunden.Add(kunde);

                i++;
            }

            Console.WriteLine("");
            Console.WriteLine("Welchen Kunden möchten Sie bearbeiten?");

            Kunde kundeToDelete = new Kunde();

            int Auswahl = Convert.ToInt32(Console.ReadLine()) - 1;

            kundeToDelete = kunden[Auswahl];

            var filterToDelete = Builders<BsonDocument>.Filter.Eq("_id", kundeToDelete.Id);

            return filterToDelete;
        }

        public static void Case1KundeBearbeiten(){
            Console.Clear();

            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<Kunde>("customers");

            Console.WriteLine("Vorname und Nachname der Person, welche Sie bearbeiten möchten:");
            String eingabe = Console.ReadLine().Trim();

            //var vorname = Regex.Match(eingabe, @"^([\w\-]+)"); <- Füllt erstes Wort in String var ab
            var vorname = eingabe.Split(' ')[0];
            var nachname = eingabe.Split(' ')[1];

            var customers = db.GetCollection<BsonDocument>("customers");
            var filter = Builders<BsonDocument>.Filter.Eq("Vorname", vorname) & Builders<BsonDocument>.Filter.Eq("Nachname", nachname);
            var result = customers.Find(filter).ToList();

            var filterToEdit = ShowAllCustomersToEdit(result);

            var resultToEdit = customers.Find(filterToEdit).FirstOrDefault();

            try
            {
                TryToExtractACustomerToEdit(resultToEdit);
            }
            catch{
                Console.WriteLine("Kunde konnte nicht gefunden werden/Error");
            }
        }

        public static void DefaultCaseKundeBearbeiten(String Suchattribut, String lololol) {
            Console.Clear();

            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<Kunde>("customers");

            Console.WriteLine($"{lololol} der Person, welche Sie bearbeiten wollen:");
            String placeholder = Console.ReadLine().Trim();

            var customers = db.GetCollection<BsonDocument>("customers");
            var filter = Builders<BsonDocument>.Filter.Eq($"{Suchattribut}", placeholder);
            var result = customers.Find(filter).ToList();

            var filterToEdit = ShowAllCustomersToEdit(result);

            var resultToEdit = customers.Find(filterToEdit).FirstOrDefault();

            try
            {
                TryToExtractACustomerToEdit(resultToEdit);
            }
            catch
            {
                Console.WriteLine("Kunde konnte nicht gefunden werden/Error");
            }
        }

        public static void Case7KundeBearbeiten()
        {
            Console.Clear();

            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<Kunde>("customers");

            Console.WriteLine("Geburtstag nach der Sie suchen wollen: dd.MM.yyyy");
            DateTime birthday = Convert.ToDateTime(Console.ReadLine());

            var customers = db.GetCollection<BsonDocument>("customers");
            var filter = Builders<BsonDocument>.Filter.Eq("Birthday", birthday);
            var result = customers.Find(filter).ToList();

            var filterToEdit = ShowAllCustomersToEdit(result);

            var resultToEdit = customers.Find(filterToEdit).FirstOrDefault();

            try
            {
                TryToExtractACustomerToEdit(resultToEdit);
            }
            catch
            {
                Console.Clear();
                Console.WriteLine(" Kunde konnte nicht gefunden werden/Error");
                Console.WriteLine("*******************************************");
                Console.WriteLine("    Zurück zum Hauptmenue mit ENTER");
                Console.ReadLine();

                MenuAuswahl();
                SwitchCaseOptionsMainMenue();
            }
        }

        public static void EditCustomerMenu(Kunde kunde) {
            Console.Clear();
            Console.WriteLine("Welches Attribut wollen Sie an dem Kunden verbessern");
            Console.WriteLine("");
            Console.WriteLine("1. " + kunde.Vorname);
            Console.WriteLine("2. " + kunde.Nachname);
            Console.WriteLine("3. " + kunde.StrasseUndNUmmer);
            Console.WriteLine("4. " + kunde.PLZ);
            Console.WriteLine("5. " + kunde.Ort);
            Console.WriteLine("6. " + kunde.Telefonnrm);
            Console.WriteLine("7. " + kunde.Email);
            Console.WriteLine("8. " + kunde.Birthday.ToLocalTime().ToShortDateString());
        }

        public static void EditUserCase1(Kunde kunde) {
            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<Kunde>("customers");

            Console.WriteLine("Neuer Vorname:");
            String placeholder = Console.ReadLine().Trim();

            var filter = Builders<Kunde>.Filter.Eq("Vorname", kunde.Vorname);
            var update = Builders<Kunde>.Update.Set("Vorname", placeholder);

            try{
                var result = collection.UpdateMany(filter, update);
                Console.WriteLine("Update Succesfull");
            }
            catch {
                Console.WriteLine("");
                Console.WriteLine("      Update failed/Error");
                Console.WriteLine("***********************************");
                Console.WriteLine(" Zurück zum Hauptmenue mit ENTER");
                Console.ReadLine();

                MenuAuswahl();
                SwitchCaseOptionsMainMenue();
            }

            Console.WriteLine("1. zurück zum Hauptmenu");
            Console.WriteLine("2. Neue Person zum editieren auswählen");

            switch (ChosenOption(28)) {
                case 1:
                    MenuAuswahl();
                    SwitchCaseOptionsMainMenue();
                    break;
                case 2:
                 KundeAnpassen();
                    break;
            }
        }

        public static void EditUserCase2(Kunde kunde){
            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<Kunde>("customers");

            Console.WriteLine("Geben Sie bitte den neuen Nachnamen ein:");
            String newName = Console.ReadLine().Trim();

            var filter = Builders<Kunde>.Filter.Eq("Nachname", kunde.Nachname);
            var update = Builders<Kunde>.Update.Set("Nachname", newName);

            try
            {
                var result = collection.UpdateMany(filter, update);
                Console.WriteLine("Update Succesfull");
            }
            catch
            {
                Console.WriteLine("");
                Console.WriteLine("      Update failed/Error");
                Console.WriteLine("***********************************");
                Console.WriteLine(" Zurück zum Hauptmenue mit ENTER");
                Console.ReadLine();

                MenuAuswahl();
                SwitchCaseOptionsMainMenue();
            }

            Console.WriteLine("1. zurück zum Hauptmenu");
            Console.WriteLine("2. Neue Person zum editieren auswählen");

            switch (ChosenOption(27))
            {
                case 1:
                    MenuAuswahl();
                    SwitchCaseOptionsMainMenue();
                    break;
                case 2:
                    KundeAnpassen();
                    break;
            }
        }

        public static void EditUserCase3(Kunde kunde)
        {
            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<Kunde>("customers");

            Console.WriteLine("Geben Sie bitte die neue Adressstrasse + Nummer ein:");
            String strasseUndNmr = Console.ReadLine().Trim();

            var filter = Builders<Kunde>.Filter.Eq("StrasseUndNUmmer", kunde.StrasseUndNUmmer);
            var update = Builders<Kunde>.Update.Set("StrasseUndNUmmer", strasseUndNmr);

            try
            {
                var result = collection.UpdateMany(filter, update);
                Console.WriteLine("Update Succesfull");
            }
            catch
            {
                Console.WriteLine("");
                Console.WriteLine("      Update failed/Error");
                Console.WriteLine("***********************************");
                Console.WriteLine(" Zurück zum Hauptmenue mit ENTER");
                Console.ReadLine();

                MenuAuswahl();
                SwitchCaseOptionsMainMenue();
            }

            Console.WriteLine("1. zurück zum Hauptmenu");
            Console.WriteLine("2. Neue Person zum editieren auswählen");

            switch (ChosenOption(29))
            {
                case 1:
                    MenuAuswahl();
                    SwitchCaseOptionsMainMenue();
                    break;
                case 2:
                    KundeAnpassen();
                    break;
            }
        }

        public static void EditUserCase4(Kunde kunde)
        {
            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<Kunde>("customers");

            Console.WriteLine("Geben Sie bitte die neue PLZ ein:");
            String plz = Console.ReadLine().Trim();

            var filter = Builders<Kunde>.Filter.Eq("PLZ", kunde.PLZ);
            var update = Builders<Kunde>.Update.Set("PLZ", plz);

            try
            {
                var result = collection.UpdateMany(filter, update);
                Console.WriteLine("Update Succesfull");
            }
            catch
            {
                Console.WriteLine("");
                Console.WriteLine("      Update failed/Error");
                Console.WriteLine("***********************************");
                Console.WriteLine(" Zurück zum Hauptmenue mit ENTER");
                Console.ReadLine();

                MenuAuswahl();
                SwitchCaseOptionsMainMenue();
            }

            Console.WriteLine("1. zurück zum Hauptmenu");
            Console.WriteLine("2. Neue Person zum editieren auswählen");

            switch (ChosenOption(30))
            {
                case 1:
                    MenuAuswahl();
                    SwitchCaseOptionsMainMenue();
                    break;
                case 2:
                    KundeAnpassen();
                    break;
            }
        }

        public static void EditUserCase5(Kunde kunde)
        {
            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<Kunde>("customers");

            Console.WriteLine("Geben Sie bitte den neuen Ort ein:");
            String ort = Console.ReadLine().Trim();

            var filter = Builders<Kunde>.Filter.Eq("Ort", kunde.Ort);
            var update = Builders<Kunde>.Update.Set("Ort", ort);

            try
            {
                var result = collection.UpdateMany(filter, update);
                Console.WriteLine("Update Succesfull");
            }
            catch
            {
                Console.WriteLine("");
                Console.WriteLine("      Update failed/Error");
                Console.WriteLine("***********************************");
                Console.WriteLine(" Zurück zum Hauptmenue mit ENTER");
                Console.ReadLine();

                MenuAuswahl();
                SwitchCaseOptionsMainMenue();
            }

            Console.WriteLine("1. zurück zum Hauptmenu");
            Console.WriteLine("2. Neue Person zum editieren auswählen");

            switch (ChosenOption(31))
            {
                case 1:
                    MenuAuswahl();
                    SwitchCaseOptionsMainMenue();
                    break;
                case 2:
                    KundeAnpassen();
                    break;
            }
        }

        public static void EditUserCase6(Kunde kunde)
        {
            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<Kunde>("customers");

            Console.WriteLine("Geben Sie bitte den neuen Vornamen ein:");
            String telefonNmr = Console.ReadLine().Trim();

            var filter = Builders<Kunde>.Filter.Eq("Telefonnrm", kunde.Telefonnrm);
            var update = Builders<Kunde>.Update.Set("Telefonnrm", telefonNmr);

            try
            {
                var result = collection.UpdateMany(filter, update);
                Console.WriteLine("Update Succesfull");
            }
            catch
            {
                Console.WriteLine("");
                Console.WriteLine("      Update failed/Error");
                Console.WriteLine("***********************************");
                Console.WriteLine(" Zurück zum Hauptmenue mit ENTER");
                Console.ReadLine();

                MenuAuswahl();
                SwitchCaseOptionsMainMenue();
            }

            Console.WriteLine("1. zurück zum Hauptmenu");
            Console.WriteLine("2. Neue Person zum editieren auswählen");

            switch (ChosenOption(32))
            {
                case 1:
                    MenuAuswahl();
                    SwitchCaseOptionsMainMenue();
                    break;
                case 2:
                    KundeAnpassen();
                    break;
            }
        }

        public static void EditUserCase7(Kunde kunde)
        {
            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<Kunde>("customers");

            Console.WriteLine("Geben Sie bitte den neuen Vornamen ein:");
            String email = Console.ReadLine().Trim();

            var filter = Builders<Kunde>.Filter.Eq("Email", kunde.Email);
            var update = Builders<Kunde>.Update.Set("Email", email);

            try
            {
                var result = collection.UpdateMany(filter, update);
                Console.WriteLine("Update Succesfull");
            }
            catch
            {
                Console.WriteLine("");
                Console.WriteLine("      Update failed/Error");
                Console.WriteLine("***********************************");
                Console.WriteLine(" Zurück zum Hauptmenue mit ENTER");
                Console.ReadLine();

                MenuAuswahl();
                SwitchCaseOptionsMainMenue();
            }

            Console.WriteLine("1. zurück zum Hauptmenu");
            Console.WriteLine("2. Neue Person zum editieren auswählen");

            switch (ChosenOption(33))
            {
                case 1:
                    MenuAuswahl();
                    SwitchCaseOptionsMainMenue();
                    break;
                case 2:
                    KundeAnpassen();
                    break;
            }
        }

        public static void editUserCase8(Kunde kunde) {
            var db = mongoClient().GetDatabase("ShopDb");
            var collection = db.GetCollection<Kunde>("customers");

            Console.WriteLine("Geben Sie bitte das neue Geburtsdatum ein:");
            DateTime geburtstag = Convert.ToDateTime(Console.ReadLine().Trim());

            var filter = Builders<Kunde>.Filter.Eq("Birthday", kunde.Birthday);
            var update = Builders<Kunde>.Update.Set("Birthday", geburtstag.ToLocalTime().ToShortDateString());

            try
            {
                var result = collection.UpdateMany(filter, update);
                Console.WriteLine("Update Succesfull");
            }
            catch
            {
                Console.WriteLine("");
                Console.WriteLine("      Update failed/Error");
                Console.WriteLine("***********************************");
                Console.WriteLine(" Zurück zum Hauptmenue mit ENTER");
                Console.ReadLine();

                MenuAuswahl();
                SwitchCaseOptionsMainMenue();
            }

            Console.WriteLine("1. zurück zum Hauptmenu");
            Console.WriteLine("2. Neue Person zum editieren auswählen");

            switch (ChosenOption(33))
            {
                case 1:
                    MenuAuswahl();
                    SwitchCaseOptionsMainMenue();
                    break;
                case 2:
                    KundeAnpassen();
                    break;
            }
        }
    }
}
