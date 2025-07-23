using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloMateria;
public class RepositorioMateriaEmOrm : RepositorioBaseEmOrm<Materia>, IRepositorioMateria
{
    public RepositorioMateriaEmOrm(GeradorDeTestesDbContext contexto) : base(contexto) { }

    public override Materia? SelecionarRegistroPorId(Guid idRegistro) {
        return registros
            .Include(m => m.Disciplina)
            .FirstOrDefault(m => m.Id.Equals(idRegistro));
    }

    public override List<Materia> SelecionarRegistros() {
        return registros
            .Include(m => m.Disciplina)
            .ToList();
    }
}
