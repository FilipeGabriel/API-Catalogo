﻿using APICatalogo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Context;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<Categoria>? Categorias { get; set; }

    public DbSet<Produto>? Produtos { get; set; }

}
