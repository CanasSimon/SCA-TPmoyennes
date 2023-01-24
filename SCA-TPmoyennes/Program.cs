using System;
using System.Collections.Generic;
using System.Linq;

// Classes fournies par HNI Institut
class Note
{
    public int matiere { get; private set; }
    public float note { get; private set; }
    public Note(int m, float n)
    {
        matiere = m;
        note = n;
    }
}

class Eleve
{
    public string prenom { get; private set; }
    public string nom { get; private set; }
    public List<Note> notes { get; private set; } // Max 200

    public Eleve(string p, string n)
    {
        prenom = p;
        nom = n;
        notes = new List<Note>();
    }

    public void ajouterNote(Note note)
    {
        if (notes.Count >= 200)
        {
            Console.Write("Nombre de notes max atteint!\n");
            return;
        }

        notes.Add(note);
    }

    public float Moyenne() // Moyenne generale
    {
        float moyenne = 0.0f;
        foreach (var note in notes)
        {
            moyenne += note.note;
        }

        moyenne /= notes.Count;
        return MathF.Round(moyenne * 100f) / 100f;
    }

    public float Moyenne(int v) // Moyenne matiere
    {
        int nbrNotes = 0;
        float moyenne = 0.0f;
        foreach (var note in notes)
        {
            if (note.matiere == v)
            {
                moyenne += note.note;
                nbrNotes++;
            }
        }

        if (nbrNotes != 0)
        {
            moyenne /= nbrNotes;
            return MathF.Round(moyenne * 100f) / 100f;
        }
        else
        {
            Console.Write("Cet eleve n'a pas de notes dans cette matière!\n");
            return -1;
        }
    }
}

class Classe
{
    public string nomClasse { get; private set; }
    public List<Note> notesMatieres { get; private set; } // Max 10
    public List<string> matieres { get; private set; } // Max 10
    public List<Eleve> eleves { get; private set; } // Max 30

    public Classe(string nom)
    {
        nomClasse = nom;
        notesMatieres = new List<Note>();
        matieres = new List<string>();
        eleves = new List<Eleve>();
    }

    public void ajouterEleve(string prenom, string nom)
    {
        if (eleves.Count >= 30)
        {
            Console.Write("Classe pleine!\n");
            return;
        }

        eleves.Add(new Eleve(prenom, nom));
    }

    public void ajouterMatiere(string matiere)
    {
        if (matieres.Count >= 10)
        {
            Console.Write("Nombre de matières max atteint!\n");
            return;
        }

        matieres.Add(matiere);
    }

    public float Moyenne() // Moyenne generale
    {
        float moyenne = 0.0f;
        foreach (var eleve in eleves)
        {
            moyenne += eleve.Moyenne();
        }

        moyenne /= eleves.Count;
        return MathF.Round(moyenne * 100f) / 100f;
    }

    public float Moyenne(int v) // Moyenne matiere
    {
        int nbrNotes = 0;
        float moyenne = 0.0f;
        foreach (var eleve in eleves)
        {
            float moyenneEleve = eleve.Moyenne(v);
            if (moyenneEleve != 0.0f)
            {
                moyenne += eleve.Moyenne(v);
                nbrNotes++;
            }
        }

        if (nbrNotes != 0)
        {
            moyenne /= nbrNotes;
            return MathF.Round(moyenne * 100f) / 100f;
        }
        else
        {
            Console.Write("Aucune note pour cette matière!\n");
            return -1;
        }
    }
}

namespace TPMoyennes
{
    class Program
    {
        static void afficherMoyenne(Classe classe, Eleve eleve, int matiere)
        {
            Console.Write(eleve.prenom + " " + eleve.nom + ", Moyenne en " + classe.matieres[matiere] + " : " +
                eleve.Moyenne(matiere) + "\n");
        }

        static void afficherMoyenne(Classe classe, Eleve eleve)
        {
            Console.Write(eleve.prenom + " " + eleve.nom + ", Moyenne Generale : " + eleve.Moyenne() + "\n");
        }

        static void afficherMoyenne(Classe classe, int matiere)
        {
            Console.Write("Classe de " + classe.nomClasse + ", Moyenne en " + classe.matieres[matiere] + " : " +
                classe.Moyenne(matiere) + "\n");
        }

        static void afficherMoyenne(Classe classe)
        {
            Console.Write("Classe de " + classe.nomClasse + ", Moyenne Generale : " + classe.Moyenne() + "\n");
        }

        static void Main(string[] args)
        {
            // Création d'une classe
            Classe sixiemeA = new Classe("6eme A");

            // Ajout des élèves à la classe
            sixiemeA.ajouterEleve("Jean", "RAGE");
            sixiemeA.ajouterEleve("Paul", "HAAR");
            sixiemeA.ajouterEleve("Sibylle", "BOQUET");
            sixiemeA.ajouterEleve("Annie", "CROCHE");
            sixiemeA.ajouterEleve("Alain", "PROVISTE");
            sixiemeA.ajouterEleve("Justin", "TYDERNIER");
            sixiemeA.ajouterEleve("Sacha", "TOUILLE");
            sixiemeA.ajouterEleve("Cesar", "TICHO");
            sixiemeA.ajouterEleve("Guy", "DON");

            // Ajout de matières étudiées par la classe
            sixiemeA.ajouterMatiere("Francais");
            sixiemeA.ajouterMatiere("Anglais");
            sixiemeA.ajouterMatiere("Physique/Chimie");
            sixiemeA.ajouterMatiere("Histoire");

            Random random = new Random();
            // Ajout de 5 notes à chaque élève et dans chaque matière
            for (int ieleve = 0; ieleve < sixiemeA.eleves.Count; ieleve++)
            {
                for (int matiere = 0; matiere < sixiemeA.matieres.Count; matiere++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        float note = (float)((6.5 + random.NextDouble() * 34)) / 2.0f;
                        sixiemeA.eleves[ieleve].ajouterNote(new Note(matiere, note));
                        // Note minimale = 3

                        // For debugging
                        //Console.Write("Eleve: " + sixiemeA.eleves[ieleve].prenom + " | Matière: " + sixiemeA.matieres[matiere] + " | Note: " + note + "\n");
                    }
                }
            }

            sixiemeA.ajouterMatiere("Test");

            Console.Write("\n-------------------------------------------\n");
            foreach (var eleve in sixiemeA.eleves)
            {
                // Afficher la moyenne d'un élève dans une matière
                for (int i = 0; i < sixiemeA.matieres.Count; i++)
                {
                    afficherMoyenne(sixiemeA, eleve, i);
                }

                // Afficher la moyenne générale du même élève
                afficherMoyenne(sixiemeA, eleve);
            }

            // Afficher la moyenne de la classe dans une matière
            for (int i = 0; i < sixiemeA.matieres.Count; i++)
            {
                afficherMoyenne(sixiemeA, i);
            }

            // Afficher la moyenne générale de la classe
            afficherMoyenne(sixiemeA);
            //Console.Read();
        }
    }
}


