using Microsoft.EntityFrameworkCore;
using System;

namespace PYP_Task_VCard_Simple_Project;

public class PYP_Task_VCard_DbContext : DbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=TABRIZ\\SQLEXPRESS;Database=VCard;Trusted_Connection=True;");
    }
    public DbSet<VCard> vCards{ get; set; }

}
