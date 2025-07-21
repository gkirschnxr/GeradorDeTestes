using System.ComponentModel.DataAnnotations;

namespace GeradorDeTestes.Dominio.ModuloMateria;

public enum TipoSerie
{
    [Display(Name = "5º ano do fundamental")]QuintoAno,
    [Display(Name = "6º ano do fundamental")] SextoAno,
    [Display(Name = "7º ano do fundamental")] SetimoAno,
    [Display(Name = "8º ano do fundamental")] OitavoAno,
    [Display(Name = "9º ano do fundamental")] NonoAno,
    [Display(Name = "1º ano do ensino médio")] Primeirao,
    [Display(Name = "2º ano do ensino médio")] Segundao,
    [Display(Name = "3º ano do ensino médio")] Terceirao
}