using System;
using System.Collections.Generic;

namespace DatabaseFirstDrinks.Models;

public partial class Drink
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Price { get; set; }
}
