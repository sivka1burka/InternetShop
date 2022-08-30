using WebApplication6.Models;

namespace WebApplication6.ViewModels
{
    public class SortViewModel
    {
        public SortState NameSort { get; private set; } // значение для сортировки по имени
        public SortState PriceSort { get; private set; }    // значение для сортировки по возрасту
        public SortState CountSort { get; private set; }   // значение для сортировки по компании
        public SortState Current { get; private set; }     // текущее значение сортировки

        public SortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            PriceSort = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
            CountSort = sortOrder == SortState.CountAsc ? SortState.CountDesc : SortState.CountAsc;
            Current = sortOrder;
        }
    }
}
