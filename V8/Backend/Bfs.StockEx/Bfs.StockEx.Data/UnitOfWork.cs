using Bfs.Core.Interfaces;
using Bfs.StockEx.Data.Interfaces;
using Bfs.StockEx.Data.Models;

using Microsoft.EntityFrameworkCore;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public StockExDbContext _context {get;}
//Template_Field_ChildrenMatrix_AddDeclareEntry

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    public UnitOfWork(StockExDbContext dbContext, IScopeData scopeData
//Template_Field_ChildrenMatrix_AddParameterEntry

//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3
    )
    {
        _context = dbContext;
//Template_Field_ChildrenMatrix_AddInitEntry

//Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4
    }

 //Template_Field_ChildrenMatrix_AddUnitOfWorkEntry

//Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5
}
