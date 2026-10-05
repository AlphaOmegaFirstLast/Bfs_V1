using Bfs.StockEx.Contracts;
using Bfs.StockEx.Domain.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.Middleware;
using Bfs.Core.Services.AI;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Text.Json;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1
namespace Bfs.StockEx.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class OperationsController
{
    private readonly IOperationsService _operationsService;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
    public OperationsController(IOperationsService operationsService
//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3    
    )
    {
        _operationsService = operationsService;
//Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4
    }
//Template_Field_ChildrenMatrix_AddControllerEntry
//Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5
}

