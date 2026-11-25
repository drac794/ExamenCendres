using System.ComponentModel.DataAnnotations;

namespace ExamenCendres.Models;

public class Foguera
{
    public int FogueraID { get; set; }
    [Required, MaxLength(60)]

    public string Nom { get; set; }
    public int Descansos { get; set; }
    
    public virtual Zona Zona { get; set; }

    
    public Foguera()
    {
        Nom = "";
        
    }

    public Foguera(string nom, int descansos) : this()
    {
        Nom = nom;
        Descansos = descansos;
   
    }

    public override string ToString()
    {
        return $"{FogueraID} - {Nom} | {Descansos} | {Zona.Nom}";
    }
}