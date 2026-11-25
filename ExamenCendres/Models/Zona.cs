using System.ComponentModel.DataAnnotations;

namespace ExamenCendres.Models;

public class Zona
{
    public int ZonaID { get; set; }
    [Required, MaxLength(60)]
    public string Nom { get; set; }
    public Bioma Bioma { get; set; }
    [Range(1, 150)]           
    public int NivellRecomanat { get; set; }
    public bool Explorada { get; set; }
    public int Secrets { get; set; }
    
    [Required]
    public virtual required Foguera Foguera { get; set; }
    
    public virtual Zona? Regio { get; set; }
    public virtual ICollection<Zona> Subzones { get; set; }

    public Zona()
    {
        Nom = "";
        
        Subzones = new HashSet<Zona>();
    }

    public Zona(string nom, Bioma bioma, int nivellRecomanat,
        bool explorada, int secrets) : this()
    {
        Nom = nom;
        Bioma = bioma;
        NivellRecomanat = nivellRecomanat;
        Explorada = explorada;
        Secrets = secrets;
    }

    public override string ToString()
    {
        return $"{ZonaID} - {Nom} | {Bioma} | {NivellRecomanat} | Explorada: {Explorada} | {Secrets} secrets | {Foguera.Nom}";
    }
}