﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SynergieCardStore.Migrations;
using SynergieCardStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace SynergieCardStore.EF
{
    public class DataInitializer : MigrateDatabaseToLatestVersion<SynergieEntities, Configuration>
    {

        public static void SeedSynergieData(SynergieEntities context)
        {
            // Some Initializing Data
            //  This method will be called after migrating to the latest version.
            var categories = new List<Category>
            {
                new Category() { CategoryId=1, CategoryName = "Książki i karty", CategoryDescription = "W kategorii znajdują się wszystkie książki i karty dotyczące tarota" },
                new Category() { CategoryId=2, CategoryName = "E-booki", CategoryDescription = "Kategoria prezentuje nasze produkty w postaci E-booków" }
            };
            categories.ForEach(c => context.Categories.AddOrUpdate(c));
            context.SaveChanges();


            var products = new List<Product>
            {
                new Product {ProductId=1, ProductTitle="Tarot zwierciadło duszy - Książka + 78 Kart", CategoryId=1, ProductAuthor="Gerd Ziegler", ProductDescription="Z podstawowego podręcznika popularnego mentora, terapeuty i na­uczy­ciela duchowego Gerda Zieglera dowiesz się, jak posługiwać się, interpretować i głębiej zrozumieć Tarota Thota Aleistera Crowley’a. Pomoże Ci on w pracy z kartami i posłuży jako przewodnik po Twoich wewnętrznych ścieżkach; będzie drogowskazem w rozwiązywaniu sytuacji bez wyjścia lub przy podejmowaniu trudnych decyzji. Oprócz tego zawiera kilka sposobów interpretacji, dokładny opis i interpretacje poszczególnych kart, dodatkowe pytania, kluczowe słowa, odniesienia, zalecenia i afirmacje do każdej karty. Ten idealny na prezent komplet jest odpowiedni również dla początkujących i zawiera wszystko, czego potrzebujesz, byś mógł interpretować karty Tarota dla siebie i innych.", ProductShortDescription="Tarot Thota Aleistera Crowley’a na swoich ob­ra­zach przedstawia szerokie spek­trum symboli. Rzuca światło na związki pomiędzy astrologią, nume­ro­logią i symboliką najróżniejszych kierunków duchowych. Jest wręcz stworzony do tego, aby stał się źródłem poznania. Komplet składa się z 78 kart o standardowych wymiarach.",VideoYTUrl="https://www.youtube.com/embed/QQUXtSPOy2c", AddedDate=DateTime.Now, ProductPrice=129,ISBN="978-80-7370-191-8", PublishingHouse="Synergie", CardDimensions="7 x 11 cm", Bestseller=true, Preview=false, Old=false,Hidden=false,ImageFileName="product_26.jpg", ImagesName="zwierciadlo-zestaw"},
                new Product {ProductId=2, ProductTitle="Tarot Snów", CategoryId=1, ProductAuthor="Lee Bursten - Marchetti Ciro", ProductDescription="Tarot, który masz przed sobą, jest niczym echo, wspomnienie snów zbiorowych z czasów, gdy znaczenie snów było o wiele większe niż dziś. Karty Tarota Snów przypominają nam o baśniach, mitach, legendach, fikcyjnych i historycznych postaciach, miejscach i przedmiotach, które pozostają w świadomości całej ludzkości. Zatem chcąc uzyskać świeże spojrzenie, musimy odwołać się do snów i do Tarota.Ze względu na swą uniwersalność, Tarot Snów jest idealny zarówno dla początkujących, jak i jego zaawansowanych miłośników. Oprócz poznania swojego wnętrza, dzięki Tarotowi można udzielać porad innym osobom, pomagając im w poznaniu samych siebie i manifestowaniu ich pragnień.Znaczenie poszczególnych kart zostało tu przedstawione w niezwykłej formie. Misterne nadanie kształtu obrazom pozwoli na doznanie bezpośredniego przeżycia wewnętrznego - intensywniejszego niż w wypadku innych talii Tarota. Autor pragnął stworzyć zbiór obrazów Tarota, które będą miały taki sam efekt, co przywołany nagle sen. Nic w Tarocie nie jest ostateczne, a odpowiedzi musisz szukać we własnym umyśle i sercu... i w swoich snach.Zestaw w twardym pudełku: książka z opisem kart oraz 79 kart o wymiarach 9x14cm, karty mają złocone brzegi.", ProductShortDescription="Witamy w świecie snów...", AddedDate=DateTime.Now, ProductPrice=129,ISBN="978-80-7370-150-5", PublishingHouse="Synergie", CardDimensions="9x14cm", Bestseller=true, Preview=false, Old=false,Hidden=false,ImageFileName="product_12.jpg", ImagesName="tarot-snow"},
                new Product {ProductId=3, ProductTitle="Mistrzowie Duchowi", CategoryId=1, ProductAuthor="Dr Doreen Virtue", ProductDescription="Mistrzowie duchowi to oświeceni przewodnicy, którzy pomogą Ci zrozumieć Twoje życiowe cele, zebrać odwagę do podjęcia znaczących zmian oraz rozwinąć Twoje zdolności jasnowidzenia i umiejętność manifestacji. Na każdej karcie przedstawiono przepiękny obraz Mistrza Duchowego (np. Saint Germain, Ganesh, Pallas Atena czy Merlin) oraz przesłanie lub odpowiedź dla Ciebie. Dołączona książka pomoże Ci poznać każdego Mistrza, doprowadzi Cię, krok po kroku, do umiejętności czytania kart dla siebie i innych, a także wyjaśni dodatkowe znaczenie każdej karty.", ProductShortDescription="Otwórz się na przyjęcie pełnego miłości przesłania od potężnych Duchowych Nauczycieli i Uzdrowicieli.", AddedDate=DateTime.Now, ProductPrice=49,ISBN="978-80-7370-047-8", PublishingHouse="Synergie", CardDimensions="12,5x9cm", Bestseller=false, Preview=false, Old=false,Hidden=false,ImageFileName="product_1.jpg", ImagesName="mistrzowie-duchowi"},
                new Product {ProductId=4, ProductTitle="Tarot Thota", CategoryId=1, ProductAuthor="ALEISTER CROWLEY", ProductDescription="Lady Frieda Harris stworzyła serię 78 obrazów według rysunków i wskazówek znanego okulisty Aleistera Crowley'a (1875-1947). Temu wielkiemu dziełu, które planowała stworzyć przez kilka miesięcy, ostatecznie poświęciła pięć lat swojego życia.★ Najsłynniejszy, bardzo piękny i tajemny Tarot stworzony przez Aleistera Crowley'a, głęboko osadzony w tradycji thelemicznej.★ Jest to jeden z najpiękniejszych, najgłębszych, najbardziej tajemniczych i mistycznych talii kart Tarota jakie powstały w przeciągu kilkuset lat od powstania tych magicznych kart.★ Jest to wręcz niezbędna talia do pracy dla każdego, kto interesuje się magią, twórczością Aleistera Crowleya, okultyzmem i rozwojem duchowym.★ Do kart została napisana przez Crowleya książka - Book of Thoth, Księga Thota - będąca jednym z najlepszych podręczników do Tarota.", ProductShortDescription="Tarot Thota w swych obrazach przedstawia szerokie spektrum symboli. Nakreśla związki, jakie zachodzą pomiędzy astrologią, numerologią i symboliką różnych kierunków rozwoju duchowego. Właśnie dlatego Tarot Thota stanowi źródło głębokieo poznania.", AddedDate=DateTime.Now, ProductPrice=69,ISBN="978-80-7370-179-6", PublishingHouse="Synergie", CardDimensions="11x7cm", Bestseller=false, Preview=false, Old=false,Hidden=false,ImageFileName="product_2.jpg", ImagesName="tarot-thota"},
                new Product {ProductId=5, ProductTitle="Cud jednorożców", CategoryId=1, ProductAuthor="COOPER DIANA", ProductDescription="Tak jak wydana w latach dziewięćdziesiątych XX w. książka A Little Light ON Angels pomogła czytelnikom nawiązać łączność z ich Aniołami Stróżami, tak Cud Jednorożców pomoże Ci odnaleźć swojego własnego Jednorożca dzięki medytacjom, rytuałom i ceremoniom.Te magiczne, świetliste stworzenia, które przez wiele lat stuleci były nieobecne na Ziemi, powracają teraz, aby poprowadzić ludzkość ściezką duchowego rozwoju. Praca ze swoim Jednorożcem pozwoli Ci osiągnąć wyższy poziom energetyczny i będzie pomagać w wędrówce ku oświeceniu. Książka zawiera wiele prawdziwych historii i anegdot opowiedzianych Autorce przez ludzi, którzy na własne oczy widzieli Jednorożce i doświadczyli spotkania z nimi. Ta książka zainspiruje Cię i wzmocni w duchowej wędrówce.", ProductShortDescription="Dowiedz się, jak interpretować mity i legendy - krążące o Jednorożcach - z wyższej, duchowej perspektywy, czytając fascynujący przewodnik o tych niezwykłych, energetycznych stworzeniach.", AddedDate=DateTime.Now, ProductPrice=30,ISBN="978-80-7370-151-2", PublishingHouse="Synergie", CardDimensions="20,5x13,5cm", Bestseller=false, Preview=false, Old=false,Hidden=false,ImageFileName="product_3.jpg", ImagesName="cud-jednorozcow"}
            };
            products.ForEach(p => context.Products.AddOrUpdate(p));

            //Products.AddOrUpdate(p => p.ProductName, n);
            //SaveChanges();

            context.SaveChanges();

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }

        public static void SeedUsers(SynergieEntities db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            const string name = "emil.saracyn@gmail.com";
            const string password = "Czujek@22";
            const string roleName = "Admin";

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name, UserData = new UserData() };
                var result = userManager.Create(user, password);
            }

            // utworzenie roli Admin jeśli nie istnieje 
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            // dodanie uzytkownika do roli Admin jesli juz nie jest w roli
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }
    }
}