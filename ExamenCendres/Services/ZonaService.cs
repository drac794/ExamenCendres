using ExamenCendres.Data;
using ExamenCendres.Models;


namespace ExamenCendres.Services;

public class ZonaService
{
    private readonly ExamenCendresContext _context;

    public ZonaService(ExamenCendresContext context)
    {
        _context = context;
    }
    
    public List<Zona> GetTotes()
    {
        return _context.Zona.OrderBy(z => z.ZonaID).ToList();
    }

    private static string NomBioma(Bioma bioma)
    {
        switch (bioma)
        {
            case Bioma.BOSC: return "BOSC";
            case Bioma.CIUTAT : return "CIUTAT";
            case Bioma.DESERT: return "DESERT";
            case Bioma.NEU : return "NEU";
            case Bioma.PANTA : return "PANTA";
            default: return "VOLCA";
        }
    }
    
    public List<Zona> GetPerExplorar(int nivellMaxim)
    {
        return _context.Zona
            .Where(z => !z.Explorada && z.NivellRecomanat  <= nivellMaxim)
            .OrderBy(z => z.NivellRecomanat)
            .ToList();
    }

    public void MostraZona(int idZona)
    {
        Zona? zona = _context.Zona.FirstOrDefault(z => z.ZonaID == idZona);
        if (zona == null)
        {
            Console.WriteLine($"No existeix cap zona amb id {idZona}");
            return;
        }

        Console.WriteLine($"Zona: {zona.Nom} - {NomBioma(zona.Bioma)} - {zona.Foguera}");

   

        Console.WriteLine("Subzones:");
        foreach (Zona z in zona.Subzones.OrderBy(z => z.ZonaID))
        {
            Console.WriteLine($" - {z.Nom} - {NomBioma(z.Bioma)} - {z.Foguera}");
        }
    }
    
    public bool AssignarRegio(int idZona, int idRegio)
    {
        Zona? regio = _context.Zona.FirstOrDefault(z => z.ZonaID == idRegio);
        Zona? relacionada = _context.Zona.FirstOrDefault(z => z.ZonaID == idZona);

        if (regio == null || relacionada == null)
            return false;

        if (regio == relacionada)
            return false;


        if (relacionada.Regio == regio)
            return false;

        relacionada.Regio = regio;
        _context.SaveChanges();
        return true;
    }
    
    public List<Zona> ZonesPendents()
    {
        return _context.Zona
            .Where(z => !z.Explorada || z.Subzones.Any(s => !s.Explorada ))
            .ToList();
    }

    public void ResumBiomes(int secretsMinims)
    {
        var biomes = _context.Zona
            .GroupBy(z => z.Bioma)
            .Select(g => new
            {
                Bioma = g.Key,
                NumBiomes = g.Count(),
                Secrets = g.Sum(z => z.Secrets)
            })
            .Where(b => b.Secrets > secretsMinims)
            .OrderByDescending(e => e.Secrets)
            .ToList();

        foreach (var e in biomes)
        {
            Console.WriteLine($"{e.Bioma} - zones{e.NumBiomes} secrets: {e.Secrets} ");
        }
    }
}