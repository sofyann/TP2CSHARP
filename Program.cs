using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP2CSHARP.BO;

namespace TP2CSHARP
{
    public class Program
    {
        private static List<Auteur> ListeAuteurs = new List<Auteur>();
        private static List<Livre> ListeLivres = new List<Livre>();
        public static void Main(string[] args)
        {
            InitialiserDatas();

            Console.WriteLine("Les auteurs dont le nom commence par G sont : ");
            foreach (var auteur in ListeAuteurs.Where(auteur => auteur.Nom[0] == 'G'))
            {
                Console.WriteLine(auteur.Prenom);
            }

            Console.WriteLine("L'auteur ayant écrit le plus de livre est : ");

            var plus = from livre in ListeLivres
                       group livre by livre.Auteur into grouped
                       orderby grouped.Count() descending
                       select grouped;

            Console.WriteLine($"{plus.FirstOrDefault().Key.Nom} {plus.FirstOrDefault().Key.Prenom}");

            Console.WriteLine("Le nombre moyen de pages par livre par auteur : ");

            var auteursLivres = from livre in ListeLivres group livre by livre.Auteur into grouped select grouped;

            foreach (var value in auteursLivres)
            {
                Console.WriteLine($"{value.Key.Prenom} {value.Key.Nom} moyennes des pages={value.Average(l => l.NbPages)}");
            }

            Console.WriteLine("Le livre avec le plus de pages est : ");

            var livrePlusPage = from livre in ListeLivres orderby livre.NbPages descending select livre;

            Console.WriteLine(livrePlusPage.First().Titre);

            Console.WriteLine("Les auteurs ont gagné en moyenne (moyenne des factures) : ");

            var moyennesGains = ListeAuteurs.SelectMany(a => a.Factures).Average(c => c.Montant);
            Console.WriteLine(moyennesGains);

            Console.WriteLine("Les auteurs et leurs livres : ");

            var auteursEtLivres = from livre in ListeLivres group livre by livre.Auteur into grouped select new { Auteur = grouped.Key, Livre = grouped.ToList() };

            foreach (var auteur in auteursEtLivres)
            {
                Console.WriteLine($"{auteur.Auteur.Nom} {auteur.Auteur.Prenom} a écrit :");
                foreach (var livre in auteur.Livre)
                {
                    Console.WriteLine($"{livre.Titre}");
                }
            }

            Console.WriteLine("Les livres triés par ordre alphabétique : ");

            foreach (var livre in ListeLivres.OrderBy(l => l.Titre))
            {
                Console.WriteLine(livre.Titre);
            }

            Console.WriteLine("Les livres dont la moyenne de page est supérieure à la moyenne : ");
            var livresSuperieurMoyenne = from livre in ListeLivres where livre.NbPages > ListeLivres.Average(l => l.NbPages) select livre;

            foreach (var livre in livresSuperieurMoyenne)
            {
                Console.WriteLine(livre.Titre);
            }


            Console.WriteLine("L'auteur ayant écrit le moins de livre est : ");
            var moins = ListeAuteurs.OrderBy(a => ListeLivres.Count(l => l.Auteur == a)).FirstOrDefault();
            Console.WriteLine($"{moins.Nom} {moins.Prenom}");

            Console.ReadKey();
        }

        private static void InitialiserDatas()
        {
            ListeAuteurs.Add(new Auteur("GROUSSARD", "Thierry"));
            ListeAuteurs.Add(new Auteur("GABILLAUD", "Jérôme"));
            ListeAuteurs.Add(new Auteur("HUGON", "Jérôme"));
            ListeAuteurs.Add(new Auteur("ALESSANDRI", "Olivier"));
            ListeAuteurs.Add(new Auteur("de QUAJOUX", "Benoit"));
            ListeLivres.Add(new Livre(1, "C# 4", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 533));
            ListeLivres.Add(new Livre(2, "VB.NET", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 539));
            ListeLivres.Add(new Livre(3, "SQL Server 2008", "SQL, Transact SQL", ListeAuteurs.ElementAt(1), 311));
            ListeLivres.Add(new Livre(4, "ASP.NET 4.0 et C#", "Sous visual studio 2010", ListeAuteurs.ElementAt(3), 544));
            ListeLivres.Add(new Livre(5, "C# 4", "Développez des applications windows avec visual studio 2010", ListeAuteurs.ElementAt(2), 452));
            ListeLivres.Add(new Livre(6, "Java 7", "les fondamentaux du langage", ListeAuteurs.ElementAt(0), 416));
            ListeLivres.Add(new Livre(7, "SQL et Algèbre relationnelle", "Notions de base", ListeAuteurs.ElementAt(1), 216));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3500, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3200, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(1).addFacture(new Facture(4000, ListeAuteurs.ElementAt(1)));
            ListeAuteurs.ElementAt(2).addFacture(new Facture(4200, ListeAuteurs.ElementAt(2)));
            ListeAuteurs.ElementAt(3).addFacture(new Facture(3700, ListeAuteurs.ElementAt(3)));
        }

    }
}
