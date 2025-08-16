using Microsoft.EntityFrameworkCore;

namespace EducaOnline.Financeiro.API.Data
{
    public class FinanceiroContext : DbContext
    {
        public FinanceiroContext(DbContextOptions<FinanceiroContext> options) : base(options) { }
    }
}
