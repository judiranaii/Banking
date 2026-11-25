using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet7
{
    internal class Program{   
        static int mdp = 5690;
        static int plafond_avance = 350700;
        static int solde = 2500000;
        static int epargne = 0;
        static int avance_en_cours = 0;

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("----------------MVOLA-----------------");
            Console.WriteLine("--------------------------------------");
            Console.ResetColor();

            // Insertion de mdp
            if (!DemanderPin()) //False
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Trop de tentatives, compte bloqué.");
                Console.ReadLine();
                Console.ResetColor();
                return;
            }

            Console.Clear();
            Console.WriteLine("BIENVENUE SUR VOTRE COMPTE.\n");

            // Menu principal 
            int option;
            do
            {
                Console.WriteLine("1) Mon compte");
                Console.WriteLine("2) Transférer de l'argent");
                Console.WriteLine("3) Retrait d'argent");
                Console.WriteLine("4) Epargne ou crédit");
                Console.WriteLine("0) Quitter\n");

                option = entier_saisi();
                Console.Clear();

                switch (option)
                {
                    case 1:
                        MonCompte();
                        break;
                    case 2:
                        Transfert();
                        break;
                    case 3:
                        Retrait();
                        break;
                    case 4:
                        EpargneCredit();
                        break;
                    case 0:
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Commande invalide");
                        Console.ResetColor();
                        break;
                }

                if (option != 0)
                {
                    Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu...");
                    Console.ReadLine();
                    Console.Clear();
                }
            } while (option != 0);

            Console.WriteLine("Merci d'avoir utilisé MVOLA. Au revoir !");
            Console.ReadLine();
        }

        // Fonction pour Saisisser un entier
        static int entier_saisi()
        {
            return Convert.ToInt32(Console.ReadLine());
        }

        // Fonction pour Demander le PIN 
        static bool DemanderPin(string message = "Veuillez entrer votre code PIN")
        {
            int essai = 0;
            while (essai < 3)
            {
                Console.WriteLine(message);
                int saisi = entier_saisi();

                if (saisi == mdp)
                {
                    return true;
                }

                essai++;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Code PIN incorrect, il vous reste {3 - essai} tentative(s).");
                Console.ResetColor();
            }
            return false;
        }

        // Fonction pour Vérifier du montant 
        static bool MontantValide(int montant)
        {
            if (montant <= 0)
            {
                Console.WriteLine("Montant invalide.");
                return false;
            }
            else if (montant > solde)
            {
                Console.WriteLine("Votre solde est insuffisant.");
                return false;
            }
            return true;
        }

        // Fonction pour Demander le PIN pour une transaction
        static bool Confirmer()
        {
            bool ok = DemanderPin("Veuillez insérer votre Code PIN pour confirmer la transaction");
            if (!ok) //False
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Trop de tentatives, transaction annulée.");
                Console.ResetColor();
            }
            return ok;
        }

        // Fonction 1 : Mon compte 
        static void MonCompte()
        {
            Console.WriteLine("1) Consulter mon solde\n");
            int choix = entier_saisi();
            Console.Clear();

            if (choix == 1)
            {
                Console.WriteLine($"Votre solde est de {solde} Ar.");
                Console.WriteLine("Recharger votre compte Yas via Mvola et gagner un bonus immédiatement de 20 pourcent.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Commande invalide");
                Console.ResetColor();
            }
        }

        // Fonction 2 : Transfert 
        static void Transfert()
        {
            Console.WriteLine("Numéro du destinataire :");
            string numero = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Montant à envoyer :");
            int montant = entier_saisi();
            Console.Clear();

            if (!MontantValide(montant)) return; //False
            if (!Confirmer()) return; //False

            solde -= montant;
            Console.Clear();
            Console.WriteLine($"Transfert de {montant} Ar vers {numero} réussi.");
            Console.WriteLine($"Votre nouveau solde est de {solde} Ar.");
        }

        // Fonction 3 : Retrait 
        static void Retrait()
        {
            Console.WriteLine("Numéro du distributeur :");
            string distributeur = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Montant à retirer :");
            int montant = entier_saisi();
            Console.Clear();

            if (!MontantValide(montant)) return; //False
            if (!Confirmer()) return; //False

            solde -= montant;
            Console.Clear();
            Console.WriteLine($"Retrait de {montant} Ar auprès du distributeur {distributeur} réussi.");
            Console.WriteLine($"Votre nouveau solde est de {solde} Ar.");
        }

        // Fonction 4 : Epargne ou crédit
        static void EpargneCredit()
        {
            Console.WriteLine("1) Epargne");
            Console.WriteLine("2) Crédit\n");
            int choix = entier_saisi();
            Console.Clear();

            switch (choix)
            {
                case 1:
                    Epargne();
                    break;
                case 2:
                    Credit();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Commande invalide");
                    Console.ResetColor();
                    break;
            }
        }

        static void Epargne()
        {
            Console.WriteLine("Montant à déposer :");
            int montant = entier_saisi();
            Console.Clear();

            if (!MontantValide(montant)) return; //False
            if (!Confirmer()) return; //False

            solde -= montant;
            epargne += montant;
            Console.Clear();
            Console.WriteLine($"Dépôt de {montant} Ar sur votre épargne réussi.");
            Console.WriteLine($"Epargne totale : {epargne} Ar | Solde : {solde} Ar.");
        }

        static void Credit()
        {
            Console.WriteLine("1) Demander l'éligibilité");
            Console.WriteLine("2) Demander une avance\n");
            int choix = entier_saisi();
            Console.Clear();

            if (choix == 1)
            {
                Console.WriteLine($"Vous êtes éligible pour demander une avance de {plafond_avance} Ar.");
            }
            else if (choix == 2)
            {
                int disponible = plafond_avance - avance_en_cours;

                if (disponible <= 0)
                {
                    Console.WriteLine("Vous avez déjà utilisé toute votre avance.");
                    return;
                }

                Console.WriteLine($"Entrer le montant (maximum {disponible} Ar) :");
                int montant = entier_saisi();
                Console.Clear();

                if (montant <= 0 || montant > disponible)
                {
                    Console.WriteLine($"Montant invalide. Maximum autorisé : {disponible} Ar.");
                    return;
                }

                if (!Confirmer()) return; //False

                solde += montant;
                avance_en_cours += montant;
                Console.Clear();
                Console.WriteLine($"Avance de {montant} Ar accordée.");
                Console.WriteLine($"Votre nouveau solde est de {solde} Ar.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Commande invalide");
                Console.ResetColor();
            }
        }
    }
}