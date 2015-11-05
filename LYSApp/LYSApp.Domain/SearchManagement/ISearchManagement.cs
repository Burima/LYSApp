using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYSApp.Model;

namespace LYSApp.Domain.SearchManagement
{
    public interface ISearchManagement
    {
        IList<House> getHouses(SearchViewModel serchViewModel);

        IList<SearchAreaViewModel> getAreaList(string term);
    }

}